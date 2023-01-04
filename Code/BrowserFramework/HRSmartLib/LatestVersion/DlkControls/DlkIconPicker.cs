using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("IconPicker")]
    public class DlkIconPicker : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkIconPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {

        }

        public DlkIconPicker(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {

        }

        #endregion

        #region Keywords
        [Keyword("SelectIcon")]
        public void SelectIcon(string IconName)
        {
            try
            {
                initialize();
                string prefix = ".fa fa-";
                string title = prefix + IconName.ToLower().Trim();

                IWebElement icon = mElement.FindElement(By.XPath(".//a[@title='" + title +"']"));
                icon.Click();

                DlkLogger.LogInfo("Successfully executed SelectIcon(): " + mControlName);
            }
            catch (Exception ex)
            {
                throw new Exception("SelectIcon() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
        }
        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }
        #endregion

        #region Properties
        #endregion
    }
}
