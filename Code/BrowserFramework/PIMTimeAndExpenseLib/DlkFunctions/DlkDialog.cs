using System;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace PIMTimeAndExpenseLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
        private static DlkBaseControl GetMessageBox()
        {
            DlkBaseControl ctlMessageBox = new DlkBaseControl("MessageBox", "ID", @"android:id/contentPanel");
            return ctlMessageBox;
        }


        [Keyword("ClickDialogButton", new String[] { "1|text|ButtonCaption|OK" })]
        public static void ClickDialogButton(String ButtonCaption)
        {

        }
    }
}
