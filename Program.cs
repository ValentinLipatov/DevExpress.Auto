using System;
using System.Windows.Forms;

namespace XML
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new Form();
            form.Show(new XML());
            Application.Run(form);
        }
    }
}