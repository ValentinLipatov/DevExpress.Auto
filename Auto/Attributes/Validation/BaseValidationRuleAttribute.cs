using System;

namespace XML
{
    public class BaseValidationRuleAttribute : Attribute
    {
        public BaseValidationRuleAttribute(string errorText)
        {
            ErrorText = errorText;
        }

        public string ErrorText { get; set; }

        public string[] Methods { get; set; }

        public string[] ExcludeMethods { get; set; }

        public bool InvertResult { get; set; }
    }
}