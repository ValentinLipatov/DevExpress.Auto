using System;

namespace XML
{
    public interface IValidationRule
    {
        string ErrorText { get; }

        bool InvertResult { get; }

        string[] Methods { get; }

        string[] ExcludeMethods { get; }

        bool IsValid(string methodName);

        bool IsSuitableMethod(string methodName);
    }
}