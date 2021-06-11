using System;
using NUnit.Framework;
using Sandbox.Facts;
using Sandbox.Shared;

namespace Sandbox.Test
{
    /// <summary>
    /// Tests for the <see cref="Assignment"/> class.
    /// </summary>
    internal static class AssignmentTests
    {
        //--------------------------------------------------
        [Test]
        public static void Ctor_ValidatesBehavior()
        {
            var restaurant = new Restaurant(new Name(value: "name"));
            var table = new Table(restaurant, number: 0, capacity: 3);
            var server = new Server(new Name(value: "name"));

            // use case: invalid server
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Assignment(server: null!, table);
                });
                Assert.That(ex.ParamName, Is.EqualTo("server"));
            }

            // use case: invalid table
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Assignment(server, table: null!);
                });
                Assert.That(ex.ParamName, Is.EqualTo("table"));
            }
            
            // use case: valid construction
            {
                var assignment = new Assignment(server, table);
                Assert.That(assignment.Server, Is.EqualTo(server));
                Assert.That(assignment.Table, Is.EqualTo(table));
            }
        }
    }
}