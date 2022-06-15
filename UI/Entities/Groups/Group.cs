using System;

namespace XML
{
    public class Group : IGroup
    {
        public Group(string name, string caption)
        {
            Name = name;
            Caption = caption;
        }

        public string Name { get; set; }

        public string Caption { get; set; }
    }
}