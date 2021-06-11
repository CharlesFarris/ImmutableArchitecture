using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Restaurant : ValueObject
    {
        //--------------------------------------------------
        public Restaurant([NotNull] Name name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        [NotNull] public Name Name { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new[] {this.Name};
        }

        public override string ToString()
        {
            return $"Restaurant:{this.Name.Value}";
        }
    }
}