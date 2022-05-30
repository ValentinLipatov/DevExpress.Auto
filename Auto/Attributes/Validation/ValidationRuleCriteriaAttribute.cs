using System;

namespace XML
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class ValidationRuleCriteriaAttribute : BaseValidationRuleAttribute
    {
        public ValidationRuleCriteriaAttribute(string criteria, string errorText) : base (errorText)
        {
            Criteria = criteria;
        }

        public string Criteria { get; set; }
    }
}