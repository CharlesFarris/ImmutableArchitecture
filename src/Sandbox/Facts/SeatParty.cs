using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class SeatParty : Fact
    {
        //--------------------------------------------------
        public SeatParty(int id, [NotNull] RequestTable requestTable, [NotNull] Table table, DateTimeOffset when)
            : base(id, ImmutableList<Fact>.Empty.Add(requestTable).Add(table))
        {
            this.RequestTable = requestTable ?? throw new ArgumentNullException(nameof(requestTable));
            this.Table = table ?? throw new ArgumentNullException(nameof(table));
            this.When = when;
        }

        [NotNull] public RequestTable RequestTable { get; }

        [NotNull] public Table Table { get; }

        public DateTimeOffset When { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.RequestTable, this.Table, this.When};
        }

        //--------------------------------------------------
        public static (Model, SeatParty) Create(Model model, RequestTable requestTable, Table table,
            ITimeProvider timeProvider)
        {
            if (requestTable.Restaurant.Id != table.Restaurant.Id)
            {
                throw new InvalidOperationException();
            }
            var existing = model.Facts
                .OfType<SeatParty>()
                .FirstOrDefault(sp => sp.RequestTable.Id == requestTable.Id && sp.Table.Id == table.Id);
            if (existing is not null)
            {
                return (model, existing);
            }

            var seatParty = new SeatParty(model.NextId(), requestTable, table, timeProvider.Now);
            return (model.InsertFact(seatParty), seatParty);

        }
    }
}