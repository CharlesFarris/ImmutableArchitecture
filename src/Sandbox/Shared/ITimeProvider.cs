using System;

namespace Sandbox.Shared
{
    public interface ITimeProvider
    {
        public DateTimeOffset Now { get; }
    }
}