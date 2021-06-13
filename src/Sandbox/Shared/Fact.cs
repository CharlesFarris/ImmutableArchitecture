using System.Collections.Immutable;

namespace Sandbox.Shared
{
    public abstract class Fact : ValueObject
    {
        //--------------------------------------------------
        protected Fact(int id, ImmutableList<Fact> predecessors)
        {
            this.Id = id;
            this.Predecessors = predecessors;
        }
            
        public int Id { get; }
        
        public ImmutableList<Fact> Predecessors { get; }
    }
}