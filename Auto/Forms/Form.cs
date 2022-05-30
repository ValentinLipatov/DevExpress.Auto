using System.Reflection;

namespace XML
{
    public class Form : BaseForm
    {
        public Form()
        {
            SetIcon("Icon");
            SetSkin("Office 2010 Blue");
        }

        protected IBusinessObject BusinessObject;

        public void Show<T>(T businessObject) where T : IBusinessObject
        {
            var type = businessObject.GetType();
            var attribute = type.GetCustomAttribute<ControlAttribute>();
            Show(businessObject, attribute?.Name ?? type.Name, attribute?.Caption ?? type.Name);
        }

        public void Show<T>(T businessObject, string name, string caption) where T : IBusinessObject
        {
            Name = name;
            Text = caption;
            BusinessObject = businessObject;
            BusinessObject.Form = this;
            Initialize();
            Show();
        }

        public void ShowDialog<T>(T businessObject) where T : IBusinessObject
        {
            var type = businessObject.GetType();
            var attribute = type.GetCustomAttribute<ControlAttribute>();
            ShowDialog(businessObject, attribute?.Name ?? type.Name, attribute?.Caption ?? type.Name);
        }

        public void ShowDialog<T>(T businessObject, string name, string caption) where T : IBusinessObject
        {
            Name = name;
            Text = caption;
            BusinessObject = businessObject;
            BusinessObject.Form = this;
            Initialize();
            ShowDialog();
        }

        public override void OnClose()
        {
            base.OnClose();
            BusinessObject.Form = null;
            BusinessObject = null;
            Dispose();
        }

        protected override void CreateControls()
        {
            BusinessObject.CreateControls();

            foreach (var property in BusinessObject.GetProperties())
            {
                var control = AddControl(property.ControlType, property.Name, property.Caption);
                property.Control = (IFieldControl)control;
            }

            foreach (var method in BusinessObject.GetMethods())
            {
                var control = AddControl(method.ControlType, method.Name, method.Caption);
                method.Control = (IButtonControl)control;
            }
        }

        protected override void CreateLayouts()
        {
            BusinessObject.CreateLayouts();
        }
    }
}