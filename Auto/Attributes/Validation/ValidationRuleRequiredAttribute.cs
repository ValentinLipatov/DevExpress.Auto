using System;

namespace XML
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ValidationRuleRequiredAttribute : BaseValidationRuleAttribute
    {
        public ValidationRuleRequiredAttribute () : base ("Поле не заполнено")
        {

        }
    }
}