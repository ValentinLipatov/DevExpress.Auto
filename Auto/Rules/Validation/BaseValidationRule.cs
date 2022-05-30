using System;
using System.Linq;

namespace XML
{
    public abstract class BaseValidationRule : IValidationRule
    {
        public BaseValidationRule(string errorText, bool invertResult, string[] methods, string[] excludeMethods)
        {
            ErrorText = errorText;
            InvertResult = invertResult;
            Methods = methods ?? new string[] { };
            ExcludeMethods = excludeMethods ?? new string[] { };
        }

        public string ErrorText { get; protected set; }

        public bool InvertResult { get; protected set; }

        public string[] Methods { get; protected set; }

        public string[] ExcludeMethods { get; protected set; }

        public abstract bool IsValid(string methodName);

        public bool IsSuitableMethod(string methodName)
        {
            return (Methods.Contains(methodName) || Methods.Length == 0) && !ExcludeMethods.Contains(methodName);
        }
    }
}