using System;
using NUnit.Framework;
using Sandbox.Shared;

namespace Sandbox.Test
{
    /// <summary>
    /// Tests for the <see cref="Name"/> class;
    /// </summary>
    internal static class NameTests
    {
        //--------------------------------------------------
        [Test]
        public static void Ctor_ValidatesBehavior()
        {
            // use case: invalid value (null)
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Name(value: null!);
                });
                Assert.That(ex.ParamName, Is.EqualTo("value"));
            }
            
            // use case: invalid value (empty)
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Name(value: string.Empty);
                });
                Assert.That(ex.ParamName, Is.EqualTo("value"));
            }

            // use case: invalid value (whitespace)
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Name(value: "    ");
                });
                Assert.That(ex.ParamName, Is.EqualTo("value"));
            }
        }
    }
}