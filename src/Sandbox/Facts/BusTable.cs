using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class BusTable : Fact
    {
        //--------------------------------------------------
        public BusTable(int id, [NotNull] SeatParty seatParty)
            : base(id, ImmutableList<Fact>.Empty.Add(seatParty))
        {
            this.SeatParty = seatParty ?? throw new ArgumentNullException(nameof(seatParty));
        }

        [NotNull] public SeatParty SeatParty { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.SeatParty};
        }

        //--------------------------------------------------
        public static (Model, BusTable) Create(Model model, SeatParty seatParty)
        {
            var existing = model.Facts
                .OfType<BusTable>()
                .FirstOrDefault(bt => bt.SeatParty.Id == seatParty.Id);
            if (existing is not null)
            {
                return (model, existing);
            }

            var busTable = new BusTable(model.NextId(), seatParty);
            return (model.InsertFact(busTable), busTable);
        }
    }
}