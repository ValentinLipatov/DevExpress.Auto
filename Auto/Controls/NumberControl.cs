using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using System;

namespace XML
{
    public class NumberControl : TextEdit, IFieldControl
    {
        public NumberControl()
        {
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @"[0-9]+";
            EditValueChanged += OnEditValueChanged;
        }

        public object ControlValue
        {
            get => Int32.Parse((string)EditValue);
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