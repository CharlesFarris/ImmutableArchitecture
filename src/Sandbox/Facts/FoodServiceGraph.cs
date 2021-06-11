using System;
using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sandbox.Facts
{
    public sealed class FoodServiceGraph
    {
        //--------------------------------------------------
        private FoodServiceGraph(
            ImmutableList<Restaurant> restaurants,
            ImmutableList<Table> tables,
            ImmutableList<RequestTable> requestTables,
            ImmutableList<SeatParty> seatParties,
            ImmutableList<WalkOut> walkOuts,
            ImmutableList<BusTable> busTables)
        {
            this.Restaurants = restaurants;
            this.Tables = tables;
            this.RequestTables = requestTables;
            this.SeatParties = seatParties;
            this.WalkOuts = walkOuts;
            this.BusTables = busTables;
        }

        public static readonly FoodServiceGraph Empty = new FoodServiceGraph(
            ImmutableList<Restaurant>.Empty,
            ImmutableList<Table>.Empty,
            ImmutableList<RequestTable>.Empty,
            ImmutableList<SeatParty>.Empty,
            ImmutableList<WalkOut>.Empty,
            ImmutableList<BusTable>.Empty);

        //--------------------------------------------------
        public static FoodServiceGraph InsertFact([CanBeNull] FoodServiceGraph graph, [CanBeNull] object fact)
        {
            var validGraph = graph ?? FoodServiceGraph.Empty;
            var restaurants = validGraph.Restaurants;
            var tables = validGraph.Tables;
            var requestTables = validGraph.RequestTables;
            var seatParties = validGraph.SeatParties;
            var walkouts = validGraph.WalkOuts;
            var busTables = validGraph.BusTables;
            switch (fact)
            {
                case Restaurant restaurant:
                    restaurants = restaurants.Add(restaurant);
                    break;
                case Table table:
                    tables = tables.Add(table);
                    break;
                case RequestTable requestTable:
                    requestTables = requestTables.Add(requestTable);
                    break;
                case SeatParty seatParty:
                    seatParties = seatParties.Add(seatParty);
                    break;
                case WalkOut walkOut:
                    walkouts = walkouts.Add(walkOut);
                    break;
                case BusTable busTable:
                    busTables = busTables.Add(busTable);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return new FoodServiceGraph(
                restaurants,
                tables,
                requestTables,
                seatParties,
                walkouts,
                busTables);
        }

        [NotNull] public ImmutableList<Restaurant> Restaurants { get; }

        [NotNull] public ImmutableList<Table> Tables { get; }

        [NotNull] public ImmutableList<RequestTable> RequestTables { get; }

        [NotNull] public ImmutableList<SeatParty> SeatParties { get; }

        [NotNull] public ImmutableList<WalkOut> WalkOuts { get; }

        [NotNull] public ImmutableList<BusTable> BusTables { get; }

    }
}