using System;
using System.Collections.Generic;

namespace XML
{
    public abstract class BaseBusinessObject : IBusinessObject
    {
        public IForm Form { get; set; }

        public abstract IList<IProperty> GetProperties();

        public abstract IList<IMethod> GetMethods();

        public virtual void CreateControls()
        {

        }

        public virtual void CreateLayouts()
        {

        }

        public bool Validate(string methodName)
        {
            var result = true;
            foreach (var property in GetProperties())
            {
                string text = "";
                foreach (var validationRule in property.GetValidationRules())
                {
                    if (!validationRule.IsValid(methodName))
                    {
                        text += text == "" ? validationRule.ErrorText : Environment.NewLine + validationRule.ErrorText;
                        result = false;
                    }
                }

                if (text != "")
                    property.Control.ControlErrorText = text;
            }
            return result;
        }

        public void CheckControls()
        {
            // Visible

            // Enabled
        }

        public virtual void OnValueChanged()
        {

        }
    }
}