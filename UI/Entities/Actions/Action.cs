using System;

namespace XML
{
    public class Action : BaseElement, IAction
    {
        public Action(IEntity entity, string name, string caption, Type controlType = null)
        {
            Entity = entity;
            Name = name;
            Caption = caption;
            ControlType = controlType ?? typeof(ButtonControl);
            Visible = true;
            Enabled = true;
        }

        public IEntity Entity { get; protected set; }

        public string Name { get; set; }

        public virtual string Caption { get; set; }

        public virtual bool Visible { get; set; }

        public virtual bool Enabled { get; set; }

        public Type ControlType { get; protected set; }

        private IActionControl _control;
        public IActionControl Control
        {
            get => _control;
            set
            {
                if (_control != null)
                    _control.Click -= OnControlClick;

                _control = value;

                if (_control != null)
                    _control.Click += OnControlClick;
            }
        }

        private void OnControlClick(object sender, EventArgs e)
        {
            Entity.Execute(this);
        }

        public IInteractionResult Validate()
        {
            return Validate(new InteractionResult());
        }

        public virtual IInteractionResult Validate(IInteractionResult interactionResult)
        {
            return interactionResult;
        }

        public virtual void Execute()
        {

        }

        protected override void Refresh()
        {
            Control.Enabled = Enabled;
            Control.Visible = Visible;
        }
    }
}