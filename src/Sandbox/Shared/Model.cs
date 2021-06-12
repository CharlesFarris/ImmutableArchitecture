using System;
using System.Collections.Immutable;
using JetBrains.Annotations;
using Sandbox.Facts;

namespace Sandbox.Shared
{
    public sealed class Model
    {
        //--------------------------------------------------
        private Model([NotNull] ImmutableList<Fact> facts)
        {
            this.Facts = facts;
        }

        public static readonly Model Empty = new(ImmutableList<Fact>.Empty);

        [NotNull] public ImmutableList<Fact> Facts { get; }

        //--------------------------------------------------
        public static Model InsertFact([NotNull] Model model, [NotNull] Fact fact)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (fact is null)
            {
                throw new ArgumentNullException(nameof(fact));
            }

            if (fact.Id != model.NextId())
            {
                throw new InvalidOperationException();
            }

            return new Model(model.Facts.Add(fact));
        }

    }
}