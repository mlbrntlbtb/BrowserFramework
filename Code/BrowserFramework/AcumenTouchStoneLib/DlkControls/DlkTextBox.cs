using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                SetText(Value);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter(String Value)
        {
            try
            {
                Initialize();
                SetTextAndEnter(Value);
                DlkLogger.LogInfo("Successfully executed SetAndEnter()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyErrorMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyErrorMessage(String ErrorMessage)
        {
            try
            {
                Initialize();
                IWebElement errorElm = null;
                errorElm = !mElement.GetAttribute("class").Contains("core-component") ?
                    errorElm = mElement.FindElement(By.XPath("./ancestor::div[contains(@class,'core-component')][1]//*[@class='core-error']")) :
                    errorElm = mElement.FindElement(By.XPath(".//*[@class='core-error']"));

                if (errorElm == null || !errorElm.Displayed)
                {
                    throw new Exception("Error message not found.");
                }
                DlkAssert.AssertEqual("VerifyErrorMessage() : " + mControlName, ErrorMessage, errorElm.Text.Trim());
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyErrorMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("ShowAutoComplete", new String[] { "1|text|LookupString|AAA" })]
        public void ShowAutoComplete(String LookupValue)
        {
            try
            {
                Initialize();
                if (string.IsNullOrEmpty(LookupValue))
                {
                    LookupValue = Keys.Backspace;
                }
                mElement.Clear();
                mElement.SendKeys(LookupValue);
                Thread.Sleep(3000);
                try
                {
                    if (mElement.Displayed == true)
                    {
                        mElement.SendKeys(Keys.Tab);
                    }
                }
                catch
                {
                    //nothing
                }
                // mElement.SendKeys(Keys.Enter);

            }
            catch (Exception e)
            {
                throw new Exception("ShowAutoComplete() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTextOnly", new String[] { "1|text|Value|SampleValue" })]
        public void SetTextOnly(String Value)
        {
            try
            {
                Initialize();
                SetText(Value);

                DlkLogger.LogInfo("Successfully executed SetTextOnly()");
            }
            catch (Exception e)
            {
                throw new Exception("SetTextOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
            ClickBanner();
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            Initialize();
            String ActValue = new DlkBaseControl("Textbox", mElement).GetValue();
            if (mElement.GetAttribute("class").Contains("date-time"))
            {
                ActValue = mElement.GetAttribute("value");
            }
            else if (mElement.GetAttribute("class").Contains("text-editor"))
            {
                ActValue = ActValue.Replace("<br>", "");
            }

            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
            ClickBanner();
        }

        [Keyword("PressTab")]
        public void PressTab()
        {
            try
            {
                Initialize();
                mElement.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("Successfully executed PressTab()");
            }
            catch (Exception e)
            {
                throw new Exception("PressTab() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTextboxButton")]
        public void ClickTextboxButton(String ButtonName)
        {
            try
            {
                Initialize();
                IWebElement ctlBtn = null;
                switch (ButtonName.ToLower())
                {
                    case "showcalendar":
                        if (mElement.FindElements(By.XPath(".//following-sibling::*[contains (@class, 'tap-target')]")).Count > 0)
                            ctlBtn = mElement.FindElement(By.XPath(".//following-sibling::*[contains (@class, 'tap-target')]"));
                        else
                            ctlBtn = mElement.FindElement(By.XPath(".//following-sibling::*[contains(@class,'date-icon')]"));
                        break;
                    case "clear":
                        ctlBtn = mElement.FindElement(By.XPath(".//span[contains(@class,'clear-icon')]"));
                        break;
                    case "search":
                        if (mElement.FindElements(By.XPath(".//span[contains(@class,'tap-target')]")).Count > 0)
                            ctlBtn = mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
                        else
                            ctlBtn = mElement.FindElement(By.XPath(".//following-sibling::*[contains(@class,'tap-target')]"));
                        break;
                    case "edit":
                        ctlBtn = mElement.FindElement(By.XPath("./ancestor-or-self::*[contains(@class,'core-component')]/following-sibling::*[contains(@class,'edit-icon')]"));
                        break;
                    default:
                        throw new Exception("ClickTextboxButton() failed : Button name " + ButtonName + " not recognized");
                        //break; to remove 'unreachable code detected' warning
                }

                (new DlkBaseControl("Textbox Button", ctlBtn)).Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextboxButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        private void SetText(String Value)
        {
            mElement.SendKeys(Keys.Control + "a");
            mElement.SendKeys(Keys.Delete);
            if (!string.IsNullOrEmpty(Value))
            {
                mElement.SendKeys(Value);
                Thread.Sleep(500);
                mElement.SendKeys(Keys.Tab);
            }
        }

        private void SetTextAndEnter(String Value)
        {
            mElement.SendKeys(Keys.Control + "a");
            mElement.SendKeys(Keys.Delete);
            if (!string.IsNullOrEmpty(Value))
            {
                mElement.SendKeys(Value);
                Thread.Sleep(500);
                mElement.SendKeys(Keys.Enter);
                Thread.Sleep(500);
                mElement.SendKeys(Keys.Tab);
            }
        }

        /// <summary>
        /// Click on banner to avoid timeout
        /// </summary>
        public void ClickBanner()
        {
            /*Removed this functionality after adjusting timeout recurrence to 30mins*/

            //try
            //{
            //    DlkLogger.LogInfo("Performing Click on Banner to avoid timeout.");
            //    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='banner']")));
            //    bannerCtrl.Click();
            //}
            //catch
            //{
            //    //Do nothing -- there might be instances that setting a text or value would display a dialog message
            //    //Placing a log instead for tracking
            //    DlkLogger.LogInfo("Problem performing Click on Banner. Trying dialog click...");
            //    try
            //    {
            //        DlkBaseControl dialogCtrl = new DlkBaseControl("Dialog", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@role='dialog'][not(contains(@style,'display: none'))][not(contains(@style,'display:none'))][not(contains(@class,'hidden'))]")));
            //        dialogCtrl.Click();
            //    }
            //    catch
            //    {
            //        DlkLogger.LogInfo("Problem performing Click on Dialog. Proceeding...");
            //    }
            //}
        }
    }
}
