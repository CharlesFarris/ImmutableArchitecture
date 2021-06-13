using System;
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

            return model.Facts.Count + 1;
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
                builder.Append($"{wo.Id}->{wo.RequestTable.Id}");
            }

            builder.AppendLine("}");
            return builder.ToString();
        }

        //--------------------------------------------------
        public static Restaurant GetRestaurant([NotNull] this Model model, string name)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model.Facts.OfType<Restaurant>().FirstOrDefault(r => r.Name.Value.Equals(name));
        }

        //--------------------------------------------------
        public static Model CreateTable([NotNull] this Model model, [NotNull] Restaurant restaurant, int number,
            int capacity)
        {
            var (updatedModel, _) = Table.Create(model, restaurant, number, capacity);
            return updatedModel;
        }

        //--------------------------------------------------
        public static Model CreateRequestTable([NotNull] this Model model, [NotNull] Restaurant restaurant, string name,
            int partySize,
            ITimeProvider timeProvider)
        {
            var (updatedModel, requestTable) = RequestTable.Create(model, restaurant, new Name(name), partySize, timeProvider);

            var tables = updatedModel.Facts
                .OfType<Table>()
                .Where(t => Equals(t.Restaurant, restaurant) && partySize <= t.Capacity);
            if (!tables.Any())
            {
                updatedModel = updatedModel.CreateWalkout(requestTable);
            }

            return updatedModel;
        }

        //--------------------------------------------------
        public static Model CreateWalkout([NotNull] this Model model, [NotNull] RequestTable requestTable)
        {
            var (updatedModel, _) = WalkOut.Create(model, requestTable);
            return updatedModel;
        }
    }
}