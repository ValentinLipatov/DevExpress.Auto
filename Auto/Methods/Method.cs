using System;
using System.Reflection;

namespace XML
{
    public class Method : IMethod
    {
        public Method(IBusinessObject businessObject, MethodInfo methodInfo, string name, string caption, Type controlType = null)
        {
            BusinessObject = businessObject;
            MethodInfo = methodInfo;
            Name = name ?? methodInfo.Name;
            Caption = caption ?? methodInfo.Name;
            ControlType = controlType ?? typeof(ButtonControl);
        }

        protected IBusinessObject BusinessObject { get; set; }

        protected MethodInfo MethodInfo { get; set; }

        public string Name { get; protected set; }

        public string Caption { get; protected set; }

        public Type ControlType { get; protected set; }

        private IButtonControl _control;
        public IButtonControl Control
        {
            get => _control;
            set
            {
                if (_control != null)
                    _control.ControlClick -= OnControlClick;

                _control = value;

                if (_control != null)
                    _control.ControlClick += OnControlClick;
            }
        }

        private void OnControlClick(object sender, EventArgs e)
        {
            Execute();
        }

        public void Execute()
        {
            if (BusinessObject.Validate(Name))
            {
                if (MethodInfo.GetParameters().Length > 0)
                {
                    var form = new Form();
                    var methodBusinessObject = new ButtonBusinessObject(MethodInfo);
                    form.ShowDialog(methodBusinessObject, Name, Caption);
                    MethodInfo.Invoke(BusinessObject, methodBusinessObject.GetParameters());
                }
                else
                {
                    MethodInfo.Invoke(BusinessObject, new object[] { });
                }
            }
        }
    }
}