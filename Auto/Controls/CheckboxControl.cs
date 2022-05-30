using DevExpress.XtraEditors;
using System;

namespace XML
{
    public class CheckboxControl : CheckEdit, IFieldControl
    {
        public CheckboxControl()
        {
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