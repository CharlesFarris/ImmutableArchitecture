using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlIdentifier
    {
        public FmlIdentifier([NotNull] string name)
        {
            this.Name = name;
        }
        
        [NotNull] public string Name { get; }
    }
}