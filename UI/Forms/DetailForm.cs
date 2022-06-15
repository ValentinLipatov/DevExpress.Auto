using System;

namespace XML
{
    public class DetailForm : BaseForm
    {
        public DetailForm(IEntity entity)
        {
            Entity = entity;

            Name = Entity.Name;
            Text = Entity.Caption;

            SetIcon("Icon");
            SetSkin("Office 2010 Blue");

            Initialize();
        }

        protected IEntity Entity { get; set; }

        protected override void CreateControls()
        {
            foreach (var property in Entity.Fields)
                property.Control = (IFieldControl)AddControl(property.ControlType, property.Name, property.Caption);

            foreach (var method in Entity.Actions)
                method.Control = (IActionControl)AddControl(method.ControlType, method.Name, method.Caption);
        }

        protected override void CreateLayouts()
        {
            foreach (var group in Entity.Groups)
                AddGroup(group.Name, group.Caption);
        }
    }
}