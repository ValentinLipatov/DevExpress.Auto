using DevExpress.XtraEditors;
using System;

namespace XML
{
    public class ButtonControl : SimpleButton, IButtonControl
    {
        public ButtonControl()
        {
            Click += OnClick;
        }

        public event EventHandler ControlClick;

        private void OnClick(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
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