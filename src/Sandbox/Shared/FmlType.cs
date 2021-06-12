using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlType
    {
        //-------------------------------------------------
        public FmlType([NotNull] FmlIdentifier identifier)
        {
            this._identifier = identifier;
        }

        //-------------------------------------------------
        public FmlType([NotNull] FmlNativeType nativeType)
        {
            this._nativeType = nativeType;
        }
        
        private FmlIdentifier _identifier;

        private FmlNativeType _nativeType;
    }
}