using System;
using System.Diagnostics.CodeAnalysis;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Server
    {
        //--------------------------------------------------
        public Server([NotNull] Name name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        [NotNull] public Name Name { get; }
    }
}