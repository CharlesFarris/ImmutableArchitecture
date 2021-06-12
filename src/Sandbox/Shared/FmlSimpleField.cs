using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlSimpleField
    {
        public FmlSimpleField(FmlIdentifier identifier, FmlType type, [NotNull] object value)
        {
            this.Identifier = identifier;
            this.Type = type;
            this.Values = ImmutableList<object>.Empty.Add(value);
        }
        
        public FmlIdentifier Identifier { get; }
        
        public FmlType Type { get; }
        
        public ImmutableList<object> Values { get; }
    }
}