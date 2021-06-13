using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class RequestTable : Fact
    {
        //--------------------------------------------------
        public RequestTable(int id, [NotNull] Restaurant restaurant, [NotNull] Name name, int partySize,
            DateTimeOffset when)
            : base(id, ImmutableList<Fact>.Empty.Add(restaurant))
        {
            this.Restaurant = restaurant ?? throw new ArgumentNullException(nameof(restaurant));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.PartySize = partySize > 0
                ? partySize
                : throw new ArgumentOutOfRangeException(nameof(partySize));
            this.When = when;
        }

        [NotNull] public Restaurant Restaurant { get; }

        [NotNull] public Name Name { get; }

        public int PartySize { get; }

        public DateTimeOffset When { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.Restaurant, this.Name, this.PartySize, this.When};
        }

        //--------------------------------------------------
        public static (Model, RequestTable) Create(
            [NotNull] Model model,
            [NotNull] Restaurant restaurant,
            [NotNull] Name name,
            int partySize,
            ITimeProvider timeProvider)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (restaurant is null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var now = timeProvider.Now;
            var existing = model.Facts.OfType<RequestTable>().FirstOrDefault(rp =>
                rp.Restaurant.Id == restaurant.Id
                && rp.Name.Equals(name)
                && rp.PartySize.Equals(partySize)
                && rp.When.Equals(now));

            if (existing is not null)
            {
                return (model, existing);
            }

            var requestTable = new RequestTable(model.NextId(), restaurant, name, partySize, now);
            return (model.InsertFact(requestTable), requestTable);
        }
    }
}