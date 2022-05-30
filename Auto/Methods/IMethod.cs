using System;

namespace XML
{
    public interface IMethod
    {
        string Name { get; }

        string Caption { get; }

        Type ControlType { get; }

        IButtonControl Control { get; set; }

        void Execute();
    }
}