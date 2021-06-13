using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Assignment : Fact
    {
        //--------------------------------------------------
        public Assignment(int id, [NotNull] Server server, [NotNull] Table table)
            : base(id, ImmutableList<Fact>.Empty.Add(server).Add(table))
        {
            this.Server = server ?? throw new ArgumentNullException(nameof(server));
            this.Table = table ?? throw new ArgumentNullException(nameof(table));
        }

        [NotNull] public Server Server { get; }

        [NotNull] public Table Table { get; }

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] {this.Server, this.Table};
        }
    }
}