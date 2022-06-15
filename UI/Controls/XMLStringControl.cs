using DevExpress.XtraEditors.Mask;

namespace XML
{
    public class XMLStringControl : StringControl
    {
        public XMLStringControl()
        {
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @".*[.]xml";
        }
    }
}