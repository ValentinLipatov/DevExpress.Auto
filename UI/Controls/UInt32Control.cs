using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using System;

namespace XML
{
    public class UInt32Control : TextEdit, IFieldControl
    {
        public UInt32Control()
        {
            IsValueChanged = false;
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @"[0-9]+";
            ValueChanged += OnEditValueChanged;
            Leave += OnLeave;
        }

        object IFieldControl.Value
        {
            get => UInt32.Parse(EditValue.ToString());
            set => EditValue = value;
        }

        public event EventHandler ValueChanged;

        public bool IsValueChanged { get; set; }

        private void OnEditValueChanged(object sender, EventArgs e)
        {
            IsValueChanged = true;
        }

        private void OnLeave(object sender, EventArgs e)
        {
            if (IsValueChanged)
            {
                ValueChanged?.Invoke(sender, e);
                IsValueChanged = false;
            }
        }
    }
}