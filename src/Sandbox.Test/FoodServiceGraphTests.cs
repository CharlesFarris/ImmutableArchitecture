using System;
using NUnit.Framework;
using Sandbox.Facts;
using Sandbox.Shared;

namespace Sandbox.Test
{
    internal static class FoodServiceGraphTests
    {
        [Test]
        public static void GenerateGraphviz_ValidatesBehavior()
        {
            var graph = FoodServiceGraph.Empty
                .InsertFact(new Restaurant(new Name("Pizza Hut")))
                .InsertFact(new Restaurant(new Name("Kentucky Fried Chicken")));

            var graphviz = graph.ToGraphviz();
            Console.WriteLine(graphviz);
            
        }
    }
}