using System.Collections.Generic;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class WalkOut : Fact
    {
        //--------------------------------------------------
        public WalkOut(int id, [NotNull] RequestTable requestTable)
            : base(id)
        {
            this.RequestTable = requestTable;
        }

        [NotNull] public RequestTable RequestTable { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new[] {this.RequestTable};
        }
    }
}