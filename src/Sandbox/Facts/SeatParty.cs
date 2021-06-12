using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class SeatParty : Fact
    {
        //--------------------------------------------------
        public SeatParty(int id, [NotNull] RequestTable requestTable, [NotNull] Table table, DateTime when)
            : base(id)
        {
            this.RequestTable = requestTable ?? throw new ArgumentNullException(nameof(requestTable));
            this.Table = table ?? throw new ArgumentNullException(nameof(table));
            this.When = when;
        }

        [NotNull] public RequestTable RequestTable { get; }

        [NotNull] public Table Table { get; }

        [NotNull] public DateTime When { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.RequestTable, this.Table, this.When};
        }
    }
}