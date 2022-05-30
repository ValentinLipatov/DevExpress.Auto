using System.Windows.Forms;

namespace XML
{
    public interface IForm : IWin32Window
    {
        T AddControl<T>(T control, string name, string caption) where T : Control;

        void AddGroup(string name, string caption);

        void AddTabbedGroup(string name, string caption);

        void AddLabel(string name, string caption);

        void SetValue(string name, object value);
    }
}