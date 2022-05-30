using System;
using System.Collections.Generic;

namespace XML
{
    public interface IProperty
    {
        IBusinessObject BusinessObject  { get; }

        string Name { get; }

        string Caption { get; }

        Type Type { get; }

        Type ControlType { get; }

        IFieldControl Control { get; set; }

        object Value { get; set; }

        IList<IValidationRule> GetValidationRules();

        event EventHandler ValueChanged;

        void OnValueChanged();
    }
}