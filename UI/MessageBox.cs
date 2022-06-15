using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace XML
{
    public class MessageBox
    {
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return XtraMessageBox.Show(text, caption, buttons, icon);
        }
    }
}