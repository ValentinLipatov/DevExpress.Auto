using System;

namespace XML
{
    public interface IButtonControl
    {
        event EventHandler ControlClick;

        bool ControlVisible { get; set; }

        bool ControlEnabled { get; set; }
    }
}