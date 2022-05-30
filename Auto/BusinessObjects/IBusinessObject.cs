using System.Collections.Generic;

namespace XML
{
    public interface IBusinessObject
    {
        IForm Form { get; set; }

        IList<IProperty> GetProperties();

        IList<IMethod> GetMethods();

        void CreateControls();

        void CreateLayouts();

        bool Validate(string methodName);
    }
}