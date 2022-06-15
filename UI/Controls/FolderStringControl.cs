using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Windows.Forms;

namespace XML
{
    public class FolderStringControl : ButtonEdit, IFieldControl
    {
        public FolderStringControl()
        {
            ButtonClick += (s, e) =>
            {
                var folderBrowserDialog = new XtraFolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    Text = folderBrowserDialog.SelectedPath;
            };

            Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            Leave += OnLeave;
        }

        object IFieldControl.Value
        {
            get => EditValue;
            set => EditValue = value;
        }

        public event EventHandler ValueChanged;

        private void OnLeave(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }
    }
}