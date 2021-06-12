using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlStructure
    {
        //-------------------------------------------------
        public FmlStructure([NotNull] IEnumerable<FmlSimpleField> fields)
        {
            this.Fields = fields
                .OrderBy(x => x.Identifier.Name)
                .ToImmutableList();
        }
        
        [NotNull] [ItemNotNull] public ImmutableList<FmlSimpleField> Fields { get; }
    }
}