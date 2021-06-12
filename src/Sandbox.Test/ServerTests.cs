using System;
using NUnit.Framework;
using Sandbox.Facts;
using Sandbox.Shared;

namespace Sandbox.Test
{
    /// <summary>
    /// Tests for the <see cref="Server"/> class.
    /// </summary>
    internal static class ServerTests
    {
        //--------------------------------------------------
        public static void Ctor_ValidateBehavior()
        {
            // use case: invalid name
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Server(id: 1, name: null);
                });
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            }

            // use case: valid construction
            {
                var name = new Name(value: "name");
                var server = new Server(id: 1, name);
                Assert.That(server.Id, Is.EqualTo(1));
                Assert.That(server.Name, Is.EqualTo(name));
            }
        }
    }
}