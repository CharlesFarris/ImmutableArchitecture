using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlFact
    {
        //-------------------------------------------------
        private FmlFact([NotNull] FmlIdentifier identifier, ImmutableList<FmlField> keyFields, bool isUnique)
        {
            this.Identifier = identifier;
            this.KeyFields = keyFields;
            this.IsUnique = isUnique;
        }
        
        [NotNull] public FmlIdentifier Identifier { get; }
        
        public ImmutableList<FmlField> KeyFields { get; }

        public bool IsUnique { get; }

        //-------------------------------------------------
        public static FmlFact Create([NotNull] FmlIdentifier identifier, [NotNull] ImmutableList<FmlField> keyFields, bool isUnique)
        {
            return new(identifier, keyFields, isUnique);
        }
    }
}