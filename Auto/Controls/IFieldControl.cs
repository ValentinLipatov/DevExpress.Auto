using System;

namespace XML
{
    public interface IFieldControl
    {
        event EventHandler ControlValueChanged;

        object ControlValue { get; set; }

        string ControlErrorText { get; set; }

        bool ControlVisible { get; set; }

        bool ControlEnabled { get; set; }
    }
}