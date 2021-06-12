using NUnit.Framework;
using Sandbox.Facts;
using Sandbox.Shared;

namespace Sandbox.Test
{
    internal static class ExtensionsTests
    {
        [Test]
        public static void GenerateGraphviz_ValidatesBehavior()
        {
            var model = Model.Empty;
            var tuple1 = Restaurant.Create(model, "Pizza Hut");
            var tuple11 = Table.Create(tuple1.Item2, tuple1.Item1, 1, 4);
            var tuple12 = Table.Create(tuple11.Item2)
            
            var tuple2 = Restaurant.Create(tuple1.Item2, "Wendy's");

            var graphviz = tuple2.Item2.ToGraphviz();
            TestContext.WriteLine(graphviz);
        }
    }
}