using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class RequestTable : Fact
    {
        //--------------------------------------------------
        public RequestTable(int fact, [NotNull] Restaurant restaurant, [NotNull] Name name, int partySize,
            DateTime when)
            : base(fact)
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

        public DateTime When { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.Restaurant, this.Name, this.PartySize, this.When};
        }
    }
}