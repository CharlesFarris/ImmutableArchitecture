using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Table : ValueObject
    {
        //--------------------------------------------------
        public Table([NotNull] Restaurant restaurant, int number, int capacity)
        {
            this.Restaurant = restaurant ?? throw new ArgumentNullException(nameof(restaurant));
            this.Number = number;
            this.Capacity = capacity > 0 ? capacity : throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        public int Number { get; }

        public int Capacity { get; }

        [NotNull] public Restaurant Restaurant { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.Restaurant, this.Number, this.Capacity};
        }

        public override string ToString()
        {
            return $"Table: {this.Number}";
        }
    }
}