using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XML
{
    public class ButtonBusinessObject : BaseBusinessObject
    {
        public ButtonBusinessObject(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; protected set; }

        private IList<IProperty> _properties;
        public override IList<IProperty> GetProperties()
        {
            if (_properties != null)
                return _properties;

            _properties = new List<IProperty>();
            foreach (var parameter in MethodInfo.GetParameters())
            {
                var attribute = parameter.GetCustomAttribute<ControlAttribute>();
                if (attribute == null)
                    throw new InvalidOperationException();

                _properties.Add(new Parameter(this, parameter, attribute.Name, attribute.Caption, attribute.ControlType));
            }
            return _properties;
        }

        private IList<IMethod> _methods;
        public override IList<IMethod> GetMethods()
        {
            if (_methods != null)
                return _methods;

            _methods = new List<IMethod>();
            return _methods;
        }

        public object[] GetParameters()
        {
            return _properties?.Select(e => e.Value)?.ToArray() ?? new object[] { };
        }
    }
}