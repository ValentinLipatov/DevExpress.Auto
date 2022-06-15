using DevExpress.XtraEditors;
using System;

namespace XML
{
    public class BooleanControl : CheckEdit, IFieldControl
    {
        public BooleanControl()
        {
            EditValueChanged += OnEditValueChanged;
        }

        object IFieldControl.Value
        {
            get => EditValue;
            set => EditValue = value;
        }

        public event EventHandler ValueChanged;

        private void OnEditValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }
    }
}