using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlPublishField
    {
        public FmlPublishField([NotNull] FmlIdentifier identifier, [NotNull] FmlType type, [NotNull] object value)
        {
            this.Identifier = identifier;
            this.Type = type;
            this.Values = ImmutableList<object>.Empty.Add(value);
        }
        
        [NotNull] public FmlIdentifier Identifier { get; }
        
        [NotNull] public FmlType Type { get; }
        
        [NotNull] public ImmutableList<object> Values { get; }
    }
}