using DevExpress.XtraEditors.Mask;

namespace XML
{
    public class HTMLTextControl : TextControl
    {
        public HTMLTextControl()
        {
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @".*[.]html";
        }
    }
}