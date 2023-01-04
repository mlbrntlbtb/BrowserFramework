using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Widget")]
    public class DlkWidget : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkWidget(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {

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

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
