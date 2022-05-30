using System;
using System.Collections.Generic;
using System.Reflection;

namespace XML
{
    public class Property : BaseProperty
    {
        public Property(IBusinessObject businessObject, PropertyInfo propertyInfo, string name, string caption, Type controlType = null) : base (businessObject, name ?? propertyInfo.Name, caption ?? propertyInfo.Name, propertyInfo.PropertyType, controlType)
        {
            PropertyInfo = propertyInfo;
        }

        protected PropertyInfo PropertyInfo { get; set; }

        public override Type Type => PropertyInfo.PropertyType;

        public override object Value
        {
            get => PropertyInfo.GetValue(BusinessObject);
            set
            {
                PropertyInfo.SetValue(BusinessObject, value);
                OnValueChanged();
            }
        }

        public override IEnumerable<T> GetCustomAttributes<T>()
        {
            return PropertyInfo.GetCustomAttributes<T>();
        }
    }
}