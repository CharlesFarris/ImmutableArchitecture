using System;
using NUnit.Framework;
using Sandbox.Facts;
using Sandbox.Shared;

namespace Sandbox.Test
{
    /// <summary>
    /// Tests for the <see cref="Table"/> class.
    /// </summary>
    internal static class TableTests
    {
        //--------------------------------------------------
        [Test]
        public static void Ctor_ValidatesBehavior()
        {
            var restaurant = new Restaurant(new Name(value: "name"));

            // use case: invalid restaurant
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Table(restaurant: null!, number: 0, capacity: 1);
                });
                Assert.That(ex.ParamName, Is.EqualTo("restaurant"));
            }

            // use case: invalid capacity
            {
                var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var _ = new Table(restaurant, number: 0, capacity: 0);
                });
                Assert.That(ex.ParamName, Is.EqualTo("capacity"));
            }

            // use case: valid construction
            {
                const int number = 1234;
                const int capacity = 3;
                var table = new Table(restaurant, number, capacity);
                Assert.That(table.Restaurant, Is.EqualTo(restaurant));
                Assert.That(table.Number, Is.EqualTo(number));
                Assert.That(table.Capacity, Is.EqualTo(capacity));
            }
        }

    }
}