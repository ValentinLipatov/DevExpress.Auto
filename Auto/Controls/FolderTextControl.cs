using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Windows.Forms;

namespace XML
{
    public class FolderTextControl : ButtonEdit, IFieldControl
    {
        public FolderTextControl()
        {
            ButtonClick += (s, e) =>
            {
                var folderBrowserDialog = new XtraFolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    Text = folderBrowserDialog.SelectedPath;
            };

            Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            EditValueChanged += OnEditValueChanged;
        }

        public object ControlValue
        {
            get => EditValue;
            set => EditValue = value;
        }

        public event EventHandler ControlValueChanged;

        private void OnEditValueChanged(object sender, EventArgs e)
        {
            ControlValueChanged?.Invoke(sender, e);
        }

        public string ControlErrorText
        {
            get => ErrorText;
            set => ErrorText = value;
        }

        public bool ControlVisible
        {
            get => Visible;
            set => Visible = value;
        }

        public bool ControlEnabled
        {
            get => Enabled;
            set => Enabled = value;
        }
    }
}