using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using HRSmartLib.PreviousVersion.DlkControls;
using HRSmartLib.DlkControls;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {          
            FindElement();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "button" || mElement.TagName == "img")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }


        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();

                if (mElement.GetAttribute("class") == "popupBtn")
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(1.5);
                    }
                }
                else
                {                
                    Click(1.5);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToLower().Contains("the http request to the remote webdriver server for url"))
                    throw new Exception("Click() failed : " + e.Message, e);
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

        [Keyword("DoubleClick")]
        public new void DoubleClick()
        {
            try
            {
                Initialize();
                base.DoubleClick();
            }
            catch(Exception ex)
            {
                throw new Exception("DoubleClick() execution failed. " + ex.Message, ex);
            }
        }

        public String GetText()
        {
            Initialize();
            String mText = "";
            mText = GetAttributeValue("value");
            if (string.IsNullOrEmpty(mText))
            {
                mText = GetValue().Trim();
            }
            return mText;
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", TrueOrFalse.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }



    }
}
