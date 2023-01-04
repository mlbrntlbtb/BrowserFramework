using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;
using CommonLib.DlkUtility;

namespace StormWebLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkTextBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "input")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        new public void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                String mValue = this.GetValue().TrimEnd();
                if (mElement.GetAttribute("class").Contains("date-time"))
                {
                    mValue = mElement.GetAttribute("value").Trim();
                }
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("Click")]
        public new void Click()
        {
            base.Click();
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
                    case "opendate":
                        if (mElement.FindElements(By.XPath(".//following-sibling::*[@class='tap-target']")).Count > 0)
                            ctlBtn = mElement.FindElement(By.XPath(".//following-sibling::*[@class='tap-target']"));
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

        private void SetText(String Value)
        {
            string ActualText = "";

            if (DlkEnvironment.mBrowser.ToLower() == "safari")
            {
                //OSX does not support native key events, so the FXDriver simulates all key presses. 
                //This means the key press is contained within the "conteFnt" window, so the browser 
                //probably never sees the COMMAND+A as it would from a native event.
                mElement.Clear();
            }
            else
            {
                mElement.SendKeys(Keys.Control + "a");
                mElement.SendKeys(Keys.Delete);
            }

            if (!string.IsNullOrEmpty(Value))
            {
                if (mElement.GetAttribute("class").Contains("filter"))
                {
                    DlkLogger.LogInfo("Class is filter. Setting per character and adding delay...");
                    char[] characters = Value.ToCharArray();
                    foreach (char c in characters)
                    {
                        mElement.SendKeys(c.ToString());
                        Thread.Sleep(300);
                    }
                }
                else
                {
                    mElement.SendKeys(Value);
                }

                if (!mElement.GetAttribute("class").Contains("numberGridInput"))
                {
                    ActualText = mElement.GetAttribute("value");
                    if (ActualText == null)
                    {
                        ActualText = mElement.Text;
                    }

                    if (ActualText.ToLower() != Value.ToLower())
                    {
                        //Clear the initial contents of the cell
                        if (DlkEnvironment.mBrowser.ToLower() == "safari")
                        {
                            //OSX does not support native key events, so the FXDriver simulates all key presses. 
                            //This means the key press is contained within the "conteFnt" window, so the browser 
                            //probably never sees the COMMAND+A as it would from a native event.
                            mElement.Clear();
                        }
                        else
                        {
                            //In few instances, mElement.Clear() disables the control
                            mElement.SendKeys(Keys.Control + "a");
                            Thread.Sleep(100);
                            mElement.SendKeys(Keys.Delete);
                        }

                        mElement.SendKeys(Value);
                    }
                }
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                SetText(Value);
                ClickBanner();

                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter (String Value)
        {
            try
            {
                Initialize();
                string ActualText = "";

                //Clear the initial contents of the cell
                //In few instances, mElement.Clear() disables the control
                mElement.SendKeys(Keys.Control + "a");
                Thread.Sleep(100);
                mElement.SendKeys(Keys.Delete);
                
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                    Thread.Sleep(3000);
                    mElement.SendKeys(Keys.Enter);

                    ActualText = mElement.GetAttribute("value");
                    if (ActualText == null)
                    {
                        ActualText = mElement.Text;
                    }
                    
                    if (ActualText.ToLower() != Value.ToLower())
                    {
                        //Clear the initial contents of the cell
                        //In few instances, mElement.Clear() disables the control
                        mElement.SendKeys(Keys.Control + "a");
                        Thread.Sleep(100);
                        mElement.SendKeys(Keys.Delete);

                        mElement.SendKeys(Value);
                        Thread.Sleep(3000);
                        mElement.SendKeys(Keys.Enter);
                    }
                }
                //mElement.SendKeys(Keys.Shift + Keys.Tab);
                DlkLogger.LogInfo("Successfully executed SetAndEnter()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
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

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            Initialize();
            String ActValue = new DlkBaseControl("Textbox", mElement).GetValue();
            if (mElement.GetAttribute("class").Contains("date-time"))
            {
                ActValue = mElement.GetAttribute("value");
            }
            else if(mElement.GetAttribute("class").Contains("text-editor"))
            {
                ActValue = ActValue.Replace("<br>", "");
            }

            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);            
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActValue = new DlkBaseControl("Textbox", mElement).GetValue();
                if (mElement.GetAttribute("class").Contains("date-time"))
                {
                    ActValue = mElement.GetAttribute("value");
                }
                DlkAssert.AssertEqual("VerifyTextContains()", ExpectedValue, ActValue, true);
            }
            catch 
            {
                throw;
            }
           
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]    
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (Convert.ToBoolean(TrueOrFalse) == true)
                    Initialize();

                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");  
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
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

        [Keyword("VerifyRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRequired(String TrueOrFalse)
        {
            try
            {
                Initialize();
                /* also check for parent or parent-of-parent labels for "required" attribute */
                DlkBaseControl labelParent = new DlkBaseControl("Label", mElement.FindElement(By.XPath("..")));
                DlkBaseControl labelGrandParent = new DlkBaseControl("Label", mElement.FindElement(By.XPath("../..")));
                bool hasRequired = (GetAttributeValue("class").ToLower().Contains("required") || labelParent.GetAttributeValue("class").ToLower().Contains("required")
                                   || labelGrandParent.GetAttributeValue("class").ToLower().Contains("required"))
                                   || (mElement.FindElements(By.XPath("./ancestor::div[contains(@class,'core-component')]")).Count > 0 && mElement.FindElement(By.XPath("./ancestor::div[contains(@class,'core-component')]")).GetAttribute("class").Contains("required"));
                DlkAssert.AssertEqual("VerifyRequired() : " + mControlName, Convert.ToBoolean(TrueOrFalse), hasRequired);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRequired() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyErrorMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyErrorMessage(String ErrorMessage)
        {
            try
            {
                Initialize();
                IWebElement errorElm = null;
                //If class doesn't contain core-component, try searching for parent that contains core-component... then search for core-error
                errorElm = !mElement.GetAttribute("class").Contains("core-component") ?
                    errorElm = mElement.FindElement(By.XPath("./ancestor::div[contains(@class,'core-component')][1]//*[@class='core-error']")) :
                    errorElm = mElement.FindElement(By.XPath(".//*[@class='core-error']"));
                                
                if (errorElm == null || !errorElm.Displayed)
                {
                    throw new Exception("Error message not found.");
                }
                DlkAssert.AssertEqual("VerifyErrorMessage() : " + mControlName, ErrorMessage, errorElm.Text.Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyErrorMessage() failed : " + e.Message, e);
            }            
        }

        [Keyword("ClearField")]
        public void ClearField()
        {
            try
            {
                Initialize();
                //Clear the initial contents of the cell
                //In few instances, mElement.Clear() disables the control
                mElement.SendKeys(Keys.Control + "a");
                Thread.Sleep(100);
                mElement.SendKeys(Keys.Delete);

                DlkLogger.LogInfo("Successfully executed ClearField()");
            }
            catch (Exception e)
            {
                throw new Exception("ClearField() failed : " + e.Message, e);
            }
        }

        public void ClickBanner()
        {
            try
            {
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@role='dialog'][not(contains(@class,'fullscreen'))]")).Count > 0)
                {
                    DlkLogger.LogInfo("Performing Click on dialog to remove focus on textbox.");
                    DlkBaseControl dialogBody = new DlkBaseControl("Banner", "XPATH_DISPLAY", "//*[@role='dialog'][not(contains(@class,'fullscreen'))]/*[contains(@class,'content')]");
                    dialogBody.Click(5,5);
                }
                else if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='banner']")).Count > 0)
                {
                    DlkLogger.LogInfo("Performing Click on Banner to remove focus on textbox.");
                    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", "XPATH_DISPLAY","//div[@class='banner']");
                    bannerCtrl.Click();
                }                
            }
            catch
            {
                //Do nothing -- there might be instances that setting a text or value would display a dialog message
                //Placing a log instead for tracking
                DlkLogger.LogInfo("Problem performing Click on Banner. Proceeding...");
            }
        }
    }
}
