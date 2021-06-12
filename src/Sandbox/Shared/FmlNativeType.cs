using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlNativeType
    {
        //-------------------------------------------------
        private FmlNativeType([NotNull] FmlIdentifier identifier)
        {
            this.Identifier = identifier;
        }

        public FmlIdentifier Identifier { get; }

        public static readonly FmlNativeType Int = new FmlNativeType(new FmlIdentifier("int"));

        public static readonly FmlNativeType Float = new FmlNativeType(new FmlIdentifier("float"));

        public static readonly FmlNativeType Char = new FmlNativeType(new FmlIdentifier("char"));

        public static readonly FmlNativeType String = new FmlNativeType(new FmlIdentifier("string"));

        public static readonly FmlNativeType Date = new FmlNativeType(new FmlIdentifier("date"));

        public static readonly FmlNativeType Time = new FmlNativeType(new FmlIdentifier("time"));
    }
}