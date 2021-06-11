using System;
using System.Text;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public static class Extensions
    {
        //--------------------------------------------------
        public static FoodServiceGraph AddRestaurant( this FoodServiceGraph graph, [CanBeNull] string name)
        {
            var restaurant = new Restaurant(new Name(name));
            return graph.Restaurants.Contains(restaurant) 
                ? graph 
                : graph.InsertFact(restaurant);
        }

        public static FoodServiceGraph AddTable(
            [NotNull] this FoodServiceGraph graph, 
            string restaurantName, 
            string tableNumber,
            int tableCapacity)
        {
            // TODO
            return graph;
        }

        
        //--------------------------------------------------
        public static FoodServiceGraph InsertFact([CanBeNull] this FoodServiceGraph graph, [CanBeNull] object fact)
        {
            return FoodServiceGraph.InsertFact(graph, fact);
        }
        
        //--------------------------------------------------
        public static string ToGraphviz([NotNull] this FoodServiceGraph graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            
            var builder = new StringBuilder();
            builder.AppendLine("digraph {");
            foreach (var r in graph.Restaurants)
            {
                builder.Append(r);
                builder.AppendLine(";");
            }

            foreach (var t in graph.Tables)
            {
                builder.Append($"Table: {t}->{t.Restaurant}");
                builder.AppendLine(";");
            }

            builder.AppendLine("}");
            return builder.ToString();

        }

    }
}