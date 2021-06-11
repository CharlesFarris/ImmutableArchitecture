using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class SeatParty : ValueObject
    {
        //--------------------------------------------------
        public SeatParty([NotNull] RequestTable requestTable, [NotNull] Table table)
        {
            this.RequestTable = requestTable ?? throw new ArgumentNullException(nameof(requestTable));
            this.Table = table ?? throw new ArgumentNullException(nameof(table));
        }
        
        [NotNull] public RequestTable RequestTable { get; }
        
        [NotNull] public Table Table { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.RequestTable, this.Table};
        }
    }
}