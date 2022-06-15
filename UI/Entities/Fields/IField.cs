using System;

namespace XML
{
    public interface IField
    {
        IEntity Entity { get; }

        string Name { get; set; }

        string Caption { get; set; }

        bool Visible { get; set; }

        bool Enabled { get; set; }

        bool IsMandatory { get; set; }

        bool IsValueSet { get; }

        bool IsValueChanged { get; }

        object Value { get; set; }

        Type ControlType { get; }

        IFieldControl Control { get; set; }

        IInteractionResult Validate(IAction action);

        IInteractionResult Validate(IAction action, IInteractionResult interactionResult);

        void SetValue(object value, bool updateControlValue, bool trackingValueChange);
    }
}