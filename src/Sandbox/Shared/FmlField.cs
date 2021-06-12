using JetBrains.Annotations;

namespace Sandbox.Shared
{
    public sealed class FmlField
    {
        //-------------------------------------------------
        public FmlField([NotNull] FmlSimpleField simpleField)
        {
            this._simpleField = simpleField;
        }

        //-------------------------------------------------
        public FmlField([NotNull] FmlPublishField publishField)
        {
            this._publishField = publishField;
        }

        public readonly FmlSimpleField _simpleField;

        public readonly FmlPublishField _publishField;
    }
}