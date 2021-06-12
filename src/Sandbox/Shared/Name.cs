using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class Name : ValueObject
    {
        //--------------------------------------------------
        public Name([CanBeNull] string value)
        {
            var validValue = (value ?? string.Empty).Trim();
            this.Value = string.IsNullOrEmpty(validValue)
                ? throw new ArgumentNullException(nameof(value))
                : validValue;
        }
        
        [NotNull] public string Value;

        //--------------------------------------------------
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new[] {this.Value};
        }
    }
}