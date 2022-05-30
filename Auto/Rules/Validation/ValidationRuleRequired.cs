using System;

namespace XML
{
    public class ValidationRuleRequired : BaseValidationRule
    {
        public ValidationRuleRequired(IProperty property, string text, bool invertResult, string[] methods, string[] excludeMethods) : base (text, invertResult, methods, excludeMethods)
        {
            Property = property;
        }

        public IProperty Property { get; protected set; }

        public override bool IsValid(string methodName)
        {
            if (!IsSuitableMethod(methodName))
                return true;

            var result = Property.Value == null;
            if (InvertResult)
                result = !result;

            if (result)
                return false;

            return true;
        }
    }
}