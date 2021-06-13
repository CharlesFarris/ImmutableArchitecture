using System.Collections.Generic;
using System.Linq;
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

        //--------------------------------------------------
        public static (Model, WalkOut) Create(Model model, RequestTable requestTable)
        {
            var existing = model.Facts.OfType<WalkOut>().FirstOrDefault(wo => wo.RequestTable.Equals(requestTable));
            if (existing is not null)
            {
                return (model, existing);
            }

            var walkout = new WalkOut(model.NextId(), requestTable);
            return (model.InsertFact(walkout), walkout);
        }
    }
}