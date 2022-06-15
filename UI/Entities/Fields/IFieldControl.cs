using System;

namespace XML
{
    public interface IFieldControl
    {
        event EventHandler ValueChanged;

        object Value { get; set; }

        string ErrorText { get; set; }

        bool Visible { get; set; }

        bool Enabled { get; set; }
    }
}