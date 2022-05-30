using System;

namespace XML
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ControlAttribute : Attribute
    {
        public ControlAttribute(string caption)
        {
            Caption = caption;
        }

        public string Name { get; set; }

        public string Caption { get; set; }

        public Type ControlType { get; set; }
    }
}