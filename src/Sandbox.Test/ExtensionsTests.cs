﻿using NUnit.Framework;
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
                    .CreateRequestTable(restaurant, "Smith", 3, DefaultTimeProvider.Instance)
                    .CreateRequestTable(restaurant, "Doe", 5, DefaultTimeProvider.Instance)
                    .CreateRequestTable(restaurant, "Biggie", 15, DefaultTimeProvider.Instance);
            }

            var graphviz = model.ToGraphviz();
            TestContext.WriteLine(graphviz);
        }
    }
}