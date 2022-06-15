using System.Collections.Generic;

namespace XML
{
    public interface IEntity
    {
        string Name { get; }

        string Caption { get; }

        IList<IField> Fields { get; }

        IList<IAction> Actions { get; }

        IList<IGroup> Groups { get; }

        void Execute(IAction action);

        IInteractionResult Validate(IAction action);

        IInteractionResult Validate(IAction action, IInteractionResult interactionResult);
    }
}