using DevExpress.XtraEditors.Mask;

namespace XML
{
    public class HTMLStringControl : StringControl
    {
        public HTMLStringControl()
        {
            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = @".*[.]html";
        }
    }
}