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
            var timeProvider = DefaultTimeProvider.Instance;
            var model = Model.Empty;
            {
                var (updatedModel, restaurant) = Restaurant.Create(model, "Pizza Hut");
                model = updatedModel
                    .CreateTable(restaurant, 1, 4)
                    .CreateTable(restaurant, 2, 6)
                    .CreateTable(restaurant, 3, 2)
                    .CreateTable(restaurant, 4, 2);
            }

            {
                var (updatedModel, restaurant) = Restaurant.Create(model, "Wendy's");
                model = updatedModel
                    .CreateTable(restaurant, 1, 4)
                    .CreateTable(restaurant, 2, 4)
                    .CreateTable(restaurant, 3, 6)
                    .CreateTable(restaurant, 4, 8)
                    .CreateTable(restaurant, 5, 2)
                    .CreateTable(restaurant, 6, 2)
                    .CreateRequestTable(restaurant, "Smith", 3, timeProvider)
                    .CreateRequestTable(restaurant, "Doe", 5, timeProvider)
                    .CreateRequestTable(restaurant, "Biggie", 15, timeProvider)
                    .TrySeatParties(restaurant, timeProvider)
                    .TrySeatParties(restaurant, timeProvider)
                    .TrySeatParties(restaurant, timeProvider)
                    .TryBusTable(restaurant, timeProvider)
                    .TryBusTable(restaurant, timeProvider)
                    .TryBusTable(restaurant, timeProvider)
                    .CreateRequestTable(restaurant, "Jones", 5, timeProvider)
                    .TrySeatParties(restaurant, timeProvider)
                    .TryBusTable(restaurant, timeProvider)
                    .CreateRequestTable(restaurant, "Holmes", 5, timeProvider)
                    .TrySeatParties(restaurant, timeProvider)
                    .CreateRequestTable(restaurant, "Ames", 1, timeProvider);
            }

            var graphviz = model.ToGraphviz();
            TestContext.WriteLine(graphviz);
        }
    }
}