using System;

namespace XML
{
    public interface IAction
    {
        IEntity Entity { get; }

        string Name { get; set; }

        string Caption { get; set; }

        bool Visible { get; set; }

        bool Enabled { get; set; }
 
        Type ControlType { get; }

        IActionControl Control { get; set; }

        IInteractionResult Validate();

        IInteractionResult Validate(IInteractionResult interactionResult);

        void Execute();
    }
}