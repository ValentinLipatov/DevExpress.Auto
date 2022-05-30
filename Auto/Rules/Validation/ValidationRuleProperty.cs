using System.Reflection;

namespace XML
{
    public class ValidationRuleProperty : BaseValidationRule
    {
        public ValidationRuleProperty(IProperty property, string criteriaPropertyName, string text, bool invertResult, string[] methods, string[] excludeMethods) : base (text, invertResult, methods, excludeMethods)
        {
            Property = property;
            CriteriaPropertyName = criteriaPropertyName;
        }

        public IProperty Property { get; protected set; }

        public string CriteriaPropertyName { get; protected set; }

        public override bool IsValid(string methodName)
        {
            if (!IsSuitableMethod(methodName))
                return true;

            if (CriteriaPropertyName != null)
            {
                var criteriaProperty = Property.BusinessObject.GetType().GetProperty(CriteriaPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (criteriaProperty.PropertyType == typeof(bool))
                {
                    if ((bool)criteriaProperty.GetValue(Property.BusinessObject))
                        return false;
                }
            }

            return true;
        }
    }
}