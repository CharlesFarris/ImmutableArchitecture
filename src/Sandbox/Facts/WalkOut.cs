using JetBrains.Annotations;

namespace Sandbox.Facts
{
    public sealed class WalkOut
    {
        //--------------------------------------------------
        public WalkOut([NotNull] RequestTable requestTable)
        {
            this.RequestTable = requestTable;
        }
        
        [NotNull] public RequestTable RequestTable { get; }
    }
}