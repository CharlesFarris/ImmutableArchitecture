using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Restaurant : Fact
    {
        //--------------------------------------------------
        public Restaurant(int id, [NotNull] Name name)
            : base(id, ImmutableList<Fact>.Empty)
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

        //--------------------------------------------------
        public static (Model, Restaurant) Create([NotNull] Model model, [CanBeNull] string name)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var validName = new Name(name);
            var existing = model.Facts.OfType<Restaurant>().FirstOrDefault(r => r.Name.Equals(validName));
            if (existing is not null)
            {
                return (model, existing);
            }

            var restaurant = new Restaurant(model.NextId(), validName);
            return (model.InsertFact(restaurant), restaurant);
        }
    }
}