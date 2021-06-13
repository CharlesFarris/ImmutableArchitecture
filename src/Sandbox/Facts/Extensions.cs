using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public static class Extensions
    {
        //--------------------------------------------------
        public static int NextId([NotNull] this Model model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model.Facts.IsEmpty 
                ? 1 
                : model.Facts.Max(f => f.Id) + 1;
        }

        //--------------------------------------------------
        public static Model InsertFact([NotNull] this Model model, [NotNull] Fact fact)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return Model.InsertFact(model, fact);
        }

        //--------------------------------------------------
        public static string ToGraphviz([NotNull] this Model model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var builder = new StringBuilder();
            builder.AppendLine("digraph {");
            builder.AppendLine("rankdir=\"BT\"");
            builder.AppendLine("node [shape=record];");
            foreach (var r in model.Facts.OfType<Restaurant>())
            {
                builder.AppendLine($"{r.Id} [label=\"Restaurant\\lName: {r.Name.Value}\"];");
            }

            foreach (var t in model.Facts.OfType<Table>())
            {
                builder.AppendLine($"{t.Id} [label=\"Table\\lNumber: {t.Number}\\lCapacity:{t.Capacity}\"];");
                builder.AppendLine($"{t.Id}->{t.Restaurant.Id}");
            }

            foreach (var rt in model.Facts.OfType<RequestTable>())
            {
                builder.AppendLine(
                    $"{rt.Id} [label=\"Table Request\\lName: {rt.Name.Value}\\lParty Size: {rt.PartySize}\"];");
                builder.AppendLine($"{rt.Id}->{rt.Restaurant.Id}");
            }

            foreach (var wo in model.Facts.OfType<WalkOut>())
            {
                builder.AppendLine($"{wo.Id} [label=\"Walk Out\"];");
                builder.AppendLine($"{wo.Id}->{wo.RequestTable.Id}");
            }

            foreach (var sp in model.Facts.OfType<SeatParty>())
            {
                builder.AppendLine($"{sp.Id} [label=\"Seat Party\"];");
                builder.AppendLine($"{sp.Id}->{sp.Table.Id}");
                builder.AppendLine($"{sp.Id}->{sp.RequestTable.Id}");
            }
            
            foreach (var bt in model.Facts.OfType<BusTable>())
            {
                builder.AppendLine($"{bt.Id} [label=\"Bus Table\"];");
                builder.AppendLine($"{bt.Id}->{bt.SeatParty.Id}");
            }

            builder.AppendLine("}");
            return builder.ToString();
        }

        //--------------------------------------------------
        [CanBeNull]
        public static T Get<T>([NotNull] this Model model, int id) where T : Fact
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model.Facts.OfType<T>().FirstOrDefault(f => f.Id == id);
        }

        //--------------------------------------------------
        [NotNull]
        public static Model CreateTable([NotNull] this Model model, [NotNull] Restaurant restaurant, int number,
            int capacity)
        {
            var (updatedModel, _) = Table.Create(model, restaurant, number, capacity);
            return updatedModel;
        }

        //--------------------------------------------------
        [NotNull]
        public static Model CreateRequestTable([NotNull] this Model model, [NotNull] Restaurant restaurant, string name,
            int partySize,
            ITimeProvider timeProvider)
        {
            var (updatedModel, requestTable) =
                RequestTable.Create(model, restaurant, new Name(name), partySize, timeProvider);

            if (updatedModel.GetTablesByCapacity(restaurant, partySize).IsEmpty)
            {
                updatedModel = updatedModel.CreateWalkout(requestTable);
            }

            return updatedModel;
        }

        //--------------------------------------------------
        [NotNull]
        public static Model CreateWalkout([NotNull] this Model model, [NotNull] RequestTable requestTable)
        {
            var (updatedModel, _) = WalkOut.Create(model, requestTable);
            return updatedModel;
        }

        //--------------------------------------------------
        public static ImmutableList<Table> GetTablesByCapacity(this Model model, Restaurant restaurant,
            int minimumCapacity)
        {
            return model.Facts
                .OfType<Table>()
                .Where(t => t.Restaurant.Id == restaurant.Id && minimumCapacity <= t.Capacity)
                .ToImmutableList();
        }

        //--------------------------------------------------
        public static ImmutableList<RequestTable> GetWaitingTableRequests(this Model model, Restaurant restaurant)
        {
            return model.Facts
                .OfType<RequestTable>()
                .Where(rt => rt.Restaurant.Id == restaurant.Id
                             && model.Facts.OfType<WalkOut>().All(wo => wo.RequestTable.Id != rt.Id)
                             && model.Facts.OfType<SeatParty>().All(sp => sp.RequestTable.Id != rt.Id))
                .ToImmutableList();
        }

        //--------------------------------------------------
        public static ImmutableList<Table> GetAvailableTables(this Model model, Restaurant restaurant,
            int minimumCapacity)
        {
            return model.Facts
                .OfType<Table>()
                .Aggregate(
                    seed: ImmutableList<Table>.Empty,
                    func: (availableTables, table) =>
                    {
                        if (table.Restaurant.Id != restaurant.Id)
                        {
                            return availableTables;
                        }

                        if (table.Capacity < minimumCapacity)
                        {
                            return availableTables;
                        }

                        var seatPartyCount = model.Facts.OfType<SeatParty>().Count(sp => sp.Table.Id == table.Id);
                        var busCount = model.Facts.OfType<BusTable>()
                            .Count(bt => bt.SeatParty.Table.Id == table.Id);
                        return seatPartyCount == busCount
                            ? availableTables.Add(table)
                            : availableTables;
                    });
        }

        //--------------------------------------------------
        public static Model TrySeatParties(this Model model, Restaurant restaurant, ITimeProvider timeProvider)
        {
            var tableRequests = model.GetWaitingTableRequests(restaurant);
            if (tableRequests.IsEmpty)
            {
                return model;
            }

            var oldestTableRequest = tableRequests.OrderBy(rt => rt.When).First();
            var availableTables = model.GetAvailableTables(restaurant, oldestTableRequest.PartySize);
            if (availableTables.IsEmpty)
            {
                return model;
            }

            var smallestAvailableTable = availableTables.OrderBy(t => t.Capacity).First();
            var (updatedModel, _) = SeatParty.Create(model, oldestTableRequest, smallestAvailableTable, timeProvider);
            return updatedModel;
        }
        
        //--------------------------------------------------
        public static ImmutableList<SeatParty> GetSeatedParties(this Model model, Restaurant restaurant)
        {
            return model.Facts
                .OfType<SeatParty>()
                .Where(sp => sp.Table.Restaurant.Id == restaurant.Id
                             && model.Facts.OfType<BusTable>().All(bt => bt.SeatParty.Id != sp.Id))
                .ToImmutableList();
        }

        
        //--------------------------------------------------
        public static Model TryBusTable(this Model model, Restaurant restaurant, ITimeProvider timeProvider)
        {
            var seatedParties = model.GetSeatedParties(restaurant);
            if (seatedParties.IsEmpty)
            {
                return model;
            }

            var oldestSeatedParty = seatedParties.OrderBy(sp => sp.When).First();
            var (updatedModel, _) = BusTable.Create(model, oldestSeatedParty);
            return updatedModel;
        }
    }
}