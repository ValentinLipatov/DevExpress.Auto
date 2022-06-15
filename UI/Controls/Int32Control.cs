using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using System;

namespace XML
{
    public class Int32Control : TextEdit, IFieldControl
    {
        public Int32Control()
        {
            IsValueChanged = false;
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @"-?[0-9]+";
            ValueChanged += OnEditValueChanged;
            Leave += OnLeave;
        }

        object IFieldControl.Value
        {
            get => Int32.Parse(EditValue.ToString());
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