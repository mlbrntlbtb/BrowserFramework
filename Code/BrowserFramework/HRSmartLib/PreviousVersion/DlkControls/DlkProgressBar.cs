using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("ProgressBar")]
    public class DlkProgressBar : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkProgressBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

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

        [Keyword("VerifyValue")]
        public void VerifyValue(string Value)
        {
            try
            {
                initialize();
                string actualResult = mElement.GetAttribute("aria-valuenow").Trim();
                DlkAssert.AssertEqual("VerifyValue : ", Value, actualResult);
                DlkLogger.LogInfo("VerifyValue() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyValue() execution failed. " + ex.Message, ex);
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
