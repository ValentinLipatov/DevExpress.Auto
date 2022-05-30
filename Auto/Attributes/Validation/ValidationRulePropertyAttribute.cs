using System;

namespace XML
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class ValidationRulePropertyAttribute : BaseValidationRuleAttribute
    {
        public ValidationRulePropertyAttribute(string criteriaPropertyName, string errorText) : base (errorText)
        {
            CriteriaPropertyName = criteriaPropertyName;
        }

        public string CriteriaPropertyName { get; set; }
    }
}