namespace Sandbox.Shared
{
    public abstract class Fact : ValueObject
    {
        //--------------------------------------------------
        protected Fact(int id)
        {
            this.Id = id;
        }
            
        public int Id { get; }
    }
}