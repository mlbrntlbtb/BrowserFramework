using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("ToolTip")]
    public class DlkToolTip : DlkBaseControl
    {
        public DlkToolTip(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolTip(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }
        
        [Keyword("Hover", new String[] { "1|text|Button Caption|Save", "2|text|Expected Value|SampleValue" })]
        public void Hover(String ButtonCaption)
        {
            try
            {
                String pStrButtonCaption = ButtonCaption;
                Initialize();

                //Admin Portal toolbar
                IWebElement toolbarElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='adminTabs']/div[@id=contains(.,'tabs')]/ul"));

                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(toolbarElement.FindElement(By.XPath("//a[contains(.,'" + pStrButtonCaption + "')]"))).Build();

                DlkEnvironment.CaptureUrl();
                String oldUrl = DlkEnvironment.LastUrl;

                while (true)
                {
                    try
                    {
                        mAction.Perform();
                        toolbarElement.FindElement(By.XPath("//a[contains(.,'" + pStrButtonCaption + "')]")).Click();
                        DlkEnvironment.CaptureUrl();
                        String newUrl = DlkEnvironment.LastUrl;
                        if (oldUrl.Equals(newUrl)) break;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    
                    break;
                }

                DlkLogger.LogInfo("Successfully executed Hover().");
            }
            catch (InvalidOperationException invalid)
            {
                //InvalidOperationCan be due to alert dialog from WaitUrlUpdate call above
                if (invalid.Message.Contains("Modal dialog present"))
                {
                    if (DlkAlert.DoesAlertExist(3))
                    {
                        DlkAlert.Accept();
                        Click();
                    }
                }
                else
                {
                    throw new Exception(string.Format("Exception of type {0} caught in button Hover() method.", invalid.GetType()));
                }
            }
            catch (Exception e)
            {

                throw e;

            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                String expectedResult = ExpectedValue;
                Initialize();

                DlkAssert.AssertEqual("VerifyText()", expectedResult, mElement.Text);
                DlkLogger.LogInfo("Successfully executed VerifyText().");
            }
            catch (InvalidOperationException invalid)
            {
                //InvalidOperationCan be due to alert dialog from WaitUrlUpdate call above
                if (invalid.Message.Contains("Modal dialog present"))
                {
                    if (DlkAlert.DoesAlertExist(3))
                    {
                        DlkAlert.Accept();
                        Click();
                    }
                }
                else
                {
                    throw new Exception(string.Format("Exception of type {0} caught in button VerifyText() method.", invalid.GetType()));
                }
            }
            catch (Exception e)
            {

                throw e;

            }

        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String sExpectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                VerifyExists(Convert.ToBoolean(sExpectedValue));
            }, new String[]{"retry"});
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
    }
}
