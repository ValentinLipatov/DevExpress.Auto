using System;

namespace XML
{
    public interface IActionControl
    {
        event EventHandler Click;

        bool Visible { get; set; }

        bool Enabled { get; set; }
    }
}