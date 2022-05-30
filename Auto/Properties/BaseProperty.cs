using System;
using System.Collections.Generic;

namespace XML
{
    public abstract class BaseProperty : IProperty
    {
        public BaseProperty(IBusinessObject businessObject, string name, string caption, Type type, Type controlType = null)
        {
            BusinessObject = businessObject;
            Name = name;
            Caption = caption;
            ControlType = controlType ?? GetControlType(type);
        }

        public event EventHandler ValueChanged;

        public void OnValueChanged() => ValueChanged?.Invoke(this, EventArgs.Empty);

        public IBusinessObject BusinessObject { get; protected set; }

        public string Name { get; protected set; }

        public string Caption { get; protected set; }

        public abstract Type Type { get; }

        public Type ControlType { get; protected set; }

        private IFieldControl _control;
        public IFieldControl Control
        {
            get => _control;
            set
            {
                if (_control != null)
                    _control.ControlValueChanged -= OnControlValueChanged;

                _control = value;

                if (_control != null)
                    _control.ControlValueChanged += OnControlValueChanged;
            }
        }

        private void OnControlValueChanged(object sender, EventArgs e)
        {
            Value = _control.ControlValue;
        }

        public abstract object Value { get; set; }

        public static Type GetControlType(Type propertyType)
        {
            if (propertyType == typeof(String))
                return typeof(TextControl);
            if (propertyType == typeof(Boolean))
                return typeof(CheckboxControl);
            if (propertyType == typeof(Int32))
                return typeof(NumberControl);
            if (propertyType == typeof(Nullable<Int32>))
                return typeof(NumberControl);
            else
                throw new InvalidOperationException();
        }

        public abstract IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;

        private IList<IValidationRule> _validationRules;
        public IList<IValidationRule> GetValidationRules()
        {
            if (_validationRules != null)
                return _validationRules;

            _validationRules = new List<IValidationRule>();
            foreach (var attribute in GetCustomAttributes<ValidationRuleCriteriaAttribute>())
                _validationRules.Add(new ValidationRuleCriteria(this, attribute.Criteria, attribute.ErrorText, attribute.InvertResult, attribute.Methods, attribute.ExcludeMethods));
            foreach (var attribute in GetCustomAttributes<ValidationRuleRequiredAttribute>())
                _validationRules.Add(new ValidationRuleRequired(this, attribute.ErrorText, attribute.InvertResult, attribute.Methods, attribute.ExcludeMethods));
            foreach (var attribute in GetCustomAttributes<ValidationRulePropertyAttribute>())
                _validationRules.Add(new ValidationRuleProperty(this, attribute.CriteriaPropertyName, attribute.ErrorText, attribute.InvertResult, attribute.Methods, attribute.ExcludeMethods));
            return _validationRules;
        }
    }
}