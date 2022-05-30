using System;

namespace XML
{
    public class ValidationRuleCriteria : BaseValidationRule
    {
        public ValidationRuleCriteria(IProperty property, string criteria, string text, bool invertResult, string[] methods, string[] excludeMethods) : base (text, invertResult, methods, excludeMethods)
        {
            Property = property;
            Criteria = criteria;
        }

        public IProperty Property { get; protected set; }

        public string Criteria { get; protected set; }

        public override bool IsValid(string methodName)
        {
            if (!IsSuitableMethod(methodName))
                return true;

            if (Criteria != null)
            {
                var result = DynamicLinq.Invoke<bool>(Criteria, "value", Property.Type, Property.Value);
                if (InvertResult)
                    result = !result;

                if (result)
                    return false;
            }

            return true;
        }
    }
}