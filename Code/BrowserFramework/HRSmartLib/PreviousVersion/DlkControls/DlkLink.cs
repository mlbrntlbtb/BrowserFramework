using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using HRSmartLib.DlkControls;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : CommonLib.DlkControls.DlkBaseControl
    {
        #region Declarations

        #endregion

        #region Constructors

        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        public DlkLink(String ControlName, IWebElement ExistingElement)
            : base(ControlName, ExistingElement)
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
                initialize();
                base.Click(4.5);
                DlkLogger.LogInfo("Successfully executed Click()");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [RetryKeyword("ClickRetryIfValidationFails")]
        public void ClickRetryIfValidationFails(string WizardStepOrHeaderName)
        {
            this.PerformAction(() =>
            {
                Click();
                string actualResult = DlkCommon.DlkCommonFunction.GetWizardStepOrHeader();
                DlkAssert.AssertEqual("ClickRetryIfValidationFails", WizardStepOrHeaderName, actualResult);
            }, new String[] { "retry" });
        }

        [RetryKeyword("ClickRetryIfDialogNotExist")]
        public void ClickRetryIfDialogNotExist()
        {
            this.PerformAction(() =>
            {
                Click();
                bool actualResult = DlkCommon.DlkCommonFunction.HasOpenMessageDialog();
                DlkAssert.AssertEqual("ClickRetryIfDialogNotExist", true, actualResult);
            }, new String[] { "retry" });
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }


        public bool IsAttributeExists(string attributeName, IWebElement element = null)
        {
            if (element == null)
            {
                if (mElement == null)
                {
                    FindElement();
                }
                element = mElement;
            }
            try
            {
                string attributeValue = element.GetAttribute(attributeName);

                if (attributeValue == null)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
