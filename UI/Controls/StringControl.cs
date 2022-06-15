using DevExpress.XtraEditors;
using System;

namespace XML
{
    public class StringControl : TextEdit, IFieldControl
    {
        public StringControl()
        {
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
            if (IsModified)
                ValueChanged?.Invoke(sender, e);
        }
    }
}