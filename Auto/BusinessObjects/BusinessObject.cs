using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XML
{
    public class BusinessObject : BaseBusinessObject
    {
        private IList<IProperty> _properties;
        public override IList<IProperty> GetProperties()
        {
            if (_properties != null)
                return _properties;

            _properties = new List<IProperty>();
            foreach (var property in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = property.GetCustomAttribute<ControlAttribute>();
                if (attribute == null)
                    continue;

                var p = new Property(this, property, attribute.Name, attribute.Caption, attribute.ControlType);
                p.ValueChanged += (s, e) => OnValueChanged();
                _properties.Add(p);
            }
            return _properties;
        }

        private IList<IMethod> _methods;
        public override IList<IMethod> GetMethods()
        {
            if (_methods != null)
                return _methods;

            _methods = new List<IMethod>();
            foreach (var method in GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = method.GetCustomAttribute<ControlAttribute>();
                if (attribute == null)
                    continue;

                _methods.Add(new Method(this, method, attribute.Name, attribute.Caption, attribute.ControlType));
            }
            return _methods;
        }

        public void SetValue<T>(string name, ref T property, T value)
        {
            property = value;

            var p = _properties?.FirstOrDefault(e => e.Name == name);
            if (p.Control != null)
                p.Control.ControlValue = value;
            p.OnValueChanged();
        }
    }
}