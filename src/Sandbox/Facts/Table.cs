using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Table : Fact
    {
        //--------------------------------------------------
        public Table(int id, [NotNull] Restaurant restaurant, int number, int capacity)
            : base(id, ImmutableList<Fact>.Empty.Add(restaurant))
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

        //--------------------------------------------------
        public override string ToString()
        {
            return $"Table: {this.Number}";
        }

        //--------------------------------------------------
        public static (Model, Table) Create([NotNull] Model model, [NotNull] Restaurant restaurant, int number, int capacity)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (restaurant is null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            if (!model.Facts.Contains(restaurant))
            {
                throw new InvalidOperationException();
            }

            var existing = model.Facts.OfType<Table>().FirstOrDefault(t => t.Number == number && Equals(t.Restaurant, restaurant));
            if (existing is not null)
            {
                return (model, existing);
            }

            var table = new Table(model.Facts.Count + 1, restaurant, number, capacity);
            return (model.InsertFact(table), table);
        }
    }
}