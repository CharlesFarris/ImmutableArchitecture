using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public static class FactExtensions
    {
        [NotNull,ItemNotNull]
        public static ImmutableList<Fact> GetAllPredecessors([NotNull] Fact fact)
        {
            if (fact is null)
            {
                throw new ArgumentNullException(nameof(fact));
            }
            var facts = ImmutableList<Fact>.Empty;
            var stack = new Stack<Fact>();
            stack.Push(fact);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (facts.Contains(current))
                {
                    continue;
                }
                facts = facts.Add(current);
                foreach (var predecessor in current.Predecessors)
                {
                    stack.Push(predecessor);
                }
            }

            return facts;
        }
    }
}