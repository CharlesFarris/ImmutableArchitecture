using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class Model
    {
        //-------------------------------------------------
        private Model([NotNull] ImmutableList<FmlFact> facts)
        {
            this.Facts = facts;
        }
        
        public ImmutableList<FmlFact> Facts { get; }

        public static readonly Model Empty = new Model(ImmutableList<FmlFact>.Empty);

        //-------------------------------------------------
        public static Model InsertFact([NotNull] Model model, [NotNull] FmlFact fact)
        {
            return new Model(model.Facts.Add(fact));
        }
    }
}