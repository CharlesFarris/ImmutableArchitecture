using System.Collections.Generic;
using NUnit.Framework;
using Sandbox.Shared;

namespace Sandbox.Test
{
    /// <summary>
    /// Tests for the <see cref="Fact"/> class.
    /// </summary>
    internal static class FactTests
    {
        //--------------------------------------------------
        public static void Ctor_ValidatesBehavior()
        {
            var fact = new MockFact(1234);
            Assert.That(fact.Id, Is.EqualTo(1234));
        }


        private class MockFact : Fact
        {
            //--------------------------------------------------
            public MockFact(int id)
                : base(id)
            {
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                return new object[] {this.Id};
            }
        }
    }
}
