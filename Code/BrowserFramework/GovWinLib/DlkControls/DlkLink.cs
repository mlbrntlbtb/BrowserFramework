using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkLink(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkLink(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        [Keyword("Click")]//
        public new void Click()
        {
            try
            {
                Initialize();
                //String sHRef = GetAttributeValue("href");
                //if (sHRef != null && sHRef.Contains("javascript"))
                //{
                //    DlkEnvironment.CaptureUrl();
                //    mElement.SendKeys(OpenQA.Selenium.Keys.Enter);
                //    DlkEnvironment.WaitUrlUpdate();
                //    DlkLogger.LogInfo("Successfully executed Click().");
                //}
                //else
                //{
                mElement.Click();
                //}
            }
            catch (InvalidOperationException invalid)
            {
                //InvalidOperationCan be due to alert dialog from WaitUrlUpdate call above
                if (invalid.Message.Contains("Modal dialog present"))
                {
                    try
                    {
                        if (DlkAlert.DoesAlertExist(3))
                        {
                            DlkAlert.Accept();
                        }
                    }
                    catch(Exception acceptAlert)
                    {
                        throw new Exception("Exception caught in DlkLink Click method accept alert. " + acceptAlert.Message);                        
                    }
                }
                else
                {
                   throw new Exception(string.Format("Exception of type {0} caught in Click method. " + invalid.Message, invalid.GetType()));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Exception caught in DlkLink Click method. " + e.Message);
            }
        }

        #region Verify methods
        [RetryKeyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            String expectedValue = ExpectedValue;

            this.PerformAction(() =>
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyText() " + mControlName, expectedValue, GetValue());
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyTitle", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyTitle(String ExpectedTitle)
        {
            String expectedValue = ExpectedTitle;

            this.PerformAction(() =>
            {
                Initialize();
                String title = mElement.GetAttribute("title");
                DlkAssert.AssertEqual("VerifyTitle() " + mControlName, expectedValue, title);
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyExists(Convert.ToBoolean(expectedValue));

                    //Boolean bExists = Exists();
                    //if (bExists == Convert.ToBoolean(ExpectedValue))
                    //{
                    //    DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + ExpectedValue);
                    //}
                    //else
                    //{
                    //    DlkLogger.LogException("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + ExpectedValue);
                    //}
                }, new String[] { "retry" });
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyIsDisabled", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIsDisabled(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;
            Boolean actualValue = false;

            this.PerformAction(() =>
            {
                Initialize();
                String strClass = mElement.GetAttribute("class");

                if (strClass == "")
                    actualValue = true;
                if (strClass == "enabled")
                    actualValue = false;

                DlkAssert.AssertEqual("VerifyIsDisabled() " + mControlName, Convert.ToBoolean(expectedValue), actualValue);

            }, new String[] { "retry" });
        }
        #endregion
    }
}

