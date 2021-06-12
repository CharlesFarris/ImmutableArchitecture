using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Server : Fact
    {
        //--------------------------------------------------
        public Server(int id, [NotNull] Name name)
            : base(id)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        [NotNull] public Name Name { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new[] {this.Name};
        }
    }
}