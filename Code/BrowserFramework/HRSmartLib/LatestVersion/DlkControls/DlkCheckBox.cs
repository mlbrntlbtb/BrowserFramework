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
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        #region Declarations

        #endregion

        #region Constructors

        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {

        }

        public DlkCheckBox(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {

        }

        #endregion

        #region Keywords

        [Keyword("Set")]
        public void Set(string IsChecked)
        {
            if (IsChecked.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping set since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    bool bIsChecked = Convert.ToBoolean(IsChecked);
                    bool bCurrentValue = isChecked;
                    if (bCurrentValue != bIsChecked)
                    {
                        this.Click();
                    }
                    DlkLogger.LogInfo("Successfully executed Set(): " + mControlName);
                }
                catch (ElementClickInterceptedException)
                {
                    this.ClickUsingJavaScript();
                    DlkLogger.LogInfo("Successfully executed Set(): " + mControlName);
                }
                catch (Exception ex)
                {
                    throw new Exception("Set( ) failed : " + ex.Message, ex);
                }
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

        [Keyword("VerifyText")]
        public void VerifyText(string Text)
        {
            try
            {
                initialize();
                DlkLabel parentLabelControl = new DlkLabel("Parent Label", mElement.FindElement(By.XPath("./..")));
                DlkAssert.AssertEqual("VerifyText() : ", Text.ToLower(), parentLabelControl.GetValue().ToLower());
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTooltipText")]
        public void VerifyTooltipText(string Text)
        {
            try
            {
                initialize();
                string actual = mElement.GetAttribute("data-bs-original-title").Trim();

                DlkAssert.AssertEqual("VerifyTooltipText() : ", Text, actual);
                DlkLogger.LogInfo("VerifyTooltipText() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTooltipText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyValue")]
        public void VerifyValue(string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = isChecked;
                DlkAssert.AssertEqual("VerifyValue() : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyValue() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyValue() execution failed. : " + ex.Message, ex);
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

        private bool isChecked
        {
            get { return mElement.Selected; }
        }

        #endregion
    }
}
