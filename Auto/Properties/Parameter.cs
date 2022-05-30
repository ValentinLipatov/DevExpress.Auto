using System;
using System.Collections.Generic;
using System.Reflection;

namespace XML
{
    public class Parameter : BaseProperty
    {
        public Parameter(IBusinessObject businessObject, ParameterInfo parameterInfo, string name, string caption, Type controlType = null) : base(businessObject, name ?? parameterInfo.Name, caption ?? parameterInfo.Name, parameterInfo.ParameterType, controlType)
        {
            ParameterInfo = parameterInfo;
        }

        protected ParameterInfo ParameterInfo { get; set; }


        private object _value;
        public override object Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged();
            }
        }

        public override Type Type => ParameterInfo.ParameterType;

        public override IEnumerable<T> GetCustomAttributes<T>()
        {
            return ParameterInfo.GetCustomAttributes<T>();
        }
    }
}