using System;

namespace Sandbox.Shared
{
    public sealed class DefaultTimeProvider : ITimeProvider
    {
        private DefaultTimeProvider()
        {
        }
        
        public DateTimeOffset Now => DateTimeOffset.Now;
        
        public static readonly DefaultTimeProvider Instance = new DefaultTimeProvider();
    }
}