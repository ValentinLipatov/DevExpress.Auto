using System;
using System.Collections.Generic;

namespace XML
{
    public class Entity : IEntity
    {
        public Entity(string name, string caption)
        {
            Name = name;
            Caption = caption;

            CreateFields();
            CreateActions();
            CreateGroups();
        }

        public virtual string Name { get; protected set; }

        public virtual string Caption { get; protected set; }

        public IList<IField> Fields { get; protected set; } = new List<IField>();

        public IList<IAction> Actions { get; protected set; } = new List<IAction>();

        public IList<IGroup> Groups { get; protected set; } = new List<IGroup>();

        protected virtual void CreateFields()
        {

        }


        protected virtual void CreateActions()
        {

        }

        protected virtual void CreateGroups()
        {

        }

        protected T AddField<T>(T field) where T : IField
        {
            Fields.Add(field);
            return field;
        }

        protected T AddAction<T>(T action) where T : IAction
        {
            Actions.Add(action);
            return action;
        }

        protected T AddGroup<T>(T group) where T : IGroup
        {
            Groups.Add(group);
            return group;
        }

        public void Execute(IAction action)
        {
            var result = action.Validate(Validate(action));
            if (result.Success)
            {
                action.Execute();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    if (string.IsNullOrEmpty(item.Field.Control.ErrorText))
                        item.Field.Control.ErrorText = item.Description;
                    else
                        item.Field.Control.ErrorText += Environment.NewLine + item.Description;
                }
            }
        }

        public IInteractionResult Validate(IAction action)
        {
            var result = new InteractionResult();
            foreach (var field in Fields)
                field.Validate(action, result);

            return Validate(action, result);
        }

        public virtual IInteractionResult Validate(IAction action, IInteractionResult interactionResult)
        {
            return interactionResult;
        }
    }
}