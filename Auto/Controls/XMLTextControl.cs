using DevExpress.XtraEditors.Mask;

namespace XML
{
    public class XMLTextControl : TextControl
    {
        public XMLTextControl()
        {
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @".*[.]xml";
        }
    }
}