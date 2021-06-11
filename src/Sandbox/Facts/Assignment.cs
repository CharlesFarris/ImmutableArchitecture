using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sandbox.Shared;

namespace Sandbox.Facts
{
    public sealed class Assignment : ValueObject
    {
        //--------------------------------------------------
        public Assignment([NotNull] Server server, [NotNull] Table table)
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