using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Image")]
    public class DlkImage : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                base.Click();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("Click() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
