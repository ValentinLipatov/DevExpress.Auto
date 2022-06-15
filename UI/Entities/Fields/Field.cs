using System;

namespace XML
{
    public class Field<T> : BaseElement, IField
    {
        public Field(IEntity entity, string name, string caption, Type controlType = null, bool isMandatory = false)
        {
            Entity = entity;
            Name = name;
            Caption = caption;
            ControlType = controlType ?? GetControlType(typeof(T));
            IsMandatory = isMandatory;
            Visible = true;
            Enabled = true;
            IsValueChanged = false;
        }

        public IEntity Entity { get; protected set; }

        public string Name { get; set; }

        public virtual string Caption { get; set; }

        public virtual bool Visible { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual bool IsMandatory { get; set; }

        public virtual bool IsValueSet => Value != null;

        public virtual bool IsValueChanged { get; protected set; }

        private T _value;
        public virtual T Value
        {
            get => _value;
            set => SetValue(value, true, true);
        }

        object IField.Value
        {
            get => Value;
            set => SetValue(value, true, true);
        }

        public Type ControlType { get; protected set; }

        private IFieldControl _control;
        public IFieldControl Control
        {
            get => _control;
            set
            {
                if (_control != null)
                    _control.ValueChanged -= OnControlValueChanged;

                _control = value;

                if (_control != null)
                    _control.ValueChanged += OnControlValueChanged;
            }
        }

        public void SetValue(object value, bool updateControlValue, bool trackingValueChange)
        {
            SetValue((T)value, updateControlValue, trackingValueChange);
        }

        private void SetValue(T value, bool updateControlValue, bool trackingValueChange)
        {
            _value = value;

            if (updateControlValue)
                Control.Value = Value;

            if (trackingValueChange)
                IsValueChanged = true;
        }

        private void OnControlValueChanged(object sender, EventArgs e)
        {
            SetValue(Control.Value, false, true);
        }

        public IInteractionResult Validate(IAction action)
        {
            return Validate(action, new InteractionResult());
        }

        public virtual IInteractionResult Validate(IAction action, IInteractionResult interactionResult)
        {
            if (IsMandatory && !IsValueSet)
                interactionResult.AddError(this, "Поле не заполнено");

            return interactionResult;
        }

        public static Type GetControlType(Type propertyType)
        {
            if (propertyType == typeof(String))
                return typeof(StringControl);
            if (propertyType == typeof(Boolean))
                return typeof(BooleanControl);
            if (propertyType == typeof(Int32) || propertyType == typeof(Nullable<Int32>))
                return typeof(Int32Control);
            if (propertyType == typeof(UInt32) || propertyType == typeof(Nullable<UInt32>))
                return typeof(UInt32Control);
            else
                throw new InvalidOperationException();
        }

        protected override void Refresh()
        {
            Control.Enabled = Enabled;
            Control.Visible = Visible;
        }
    }
}