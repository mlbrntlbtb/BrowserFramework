using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace StormWebLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        private Boolean IsInit = false;

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
            if (!IsInit)
            {
                DlkStormWebFunctionHandler.WaitScreenGetsReady();
                FindElement();
                this.ScrollIntoViewUsingJavaScript(true);
                IsInit = true;
            }
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

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        new public void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                String mValue = this.GetValue().TrimEnd();
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
            try
            {
                Initialize();

                string sClass = mElement.GetAttribute("class");
                if (sClass == "popupBtn" || sClass.Contains("quick-edit") || sClass.Contains("icon-edit") || sClass.Contains("gridImage")
                    || sClass.Contains("icon-filter") || sClass.Contains("design") || sClass.Contains("help-icon"))
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
                else
                {
                    //check for existing dialog boxes
                    bool dialogExist = false;
                    var dialogList = mElement.FindElements(By.XPath("//*[@role='dialog']")).ToList();
                    if (dialogList != null && dialogList.Where(x => x.Displayed).Count() > 0)
                    {
                        dialogExist = true;
                    }

                    //Known issue with Chrome driver. When an element overlaps the desired element to be clicked, it will render the desired element to be unclickable.
                    //Offset doesn't work for very small elements such as info buttons since applying offset will move the clickable area outside the element.
                    //Only use javascript on elements without the button tag to minimize effect on other controls.
                    if (((DlkEnvironment.mBrowser.ToLower().Contains("chrome")) || (DlkEnvironment.mBrowser.ToLower() == "ie")) 
                        && (mElement.TagName != "button")
                        && (!dialogExist || (mControlName == "SortingOptions"))
                        && (!mElement.GetAttribute("class").Contains("overlay"))
                        && (!mElement.GetAttribute("style").Contains("inline-block"))
                        && (!mElement.GetAttribute("class").Contains("page-mode"))
                        && (!mElement.GetAttribute("class").Contains("action-text"))
                        && (!mElement.GetAttribute("class").Contains("toolTipButton"))
                       )
                    {
                        ClickUsingJavaScript();                       
                    }
                    // Few instances on some buttons that selenium click triggers spinners/app blockers then cancels button's function in adhoc run but cannot be reproduced by manual testing.
                    // Using ClickUsingJavaScript instead. Observe if no adverse effects.
                    else if ((mElement.TagName == "button") && (mElement.GetAttribute("type").Contains("button")))
                    {
                        ClickUsingJavaScript();
                    }
                    else
                    {
                        //will throw an exception if element is unclickable/overlaps with another element
                        ScrollIntoViewUsingJavaScript(true);
                        mElement.Click();
                    }
                }
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                    throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickIfExists", new String[] { "1|text|Expected Error|TRUE or FALSE" })]
        //Ported from DlkLink. True means it is expecting the control to exist. False means the control may not exist and will proceed to next step if not found.
        public void ClickIfExists(String TrueOrFalse)
        {
            Boolean bError = Convert.ToBoolean(TrueOrFalse);
            try
            {
                if (Exists())
                    Click(1.5);
            }
            catch (Exception e)
            {
                if (bError)
                {
                    throw new Exception("Click() failed : " + e.Message, e);
                }
                else
                {
                    DlkLogger.LogInfo("Control does not exist. Control: " + mControlName);
                }
            }

        }

        [Keyword("MouseOver")]
        public new void MouseOver()
        {
            Initialize();
            if (mElement.GetAttribute("class").Contains("toolTipButton") && DlkEnvironment.mBrowser.Equals("Chrome"))
            {
                MouseOverUsingJavaScript();
            }
            else
            {
                base.MouseOver();
            }
            /* Sleeping for 3000ms to make sure MouseOver successfully executed then..
             Hovering away from the current element, essential esp for screens with multiple info bubble */
            Thread.Sleep(3000);
            MouseOverOffset(0,0);
        }

        public String GetText()
        {
           // Initialize();
            String mText = "";
            mText = GetAttributeValue("textContent");
            return mText;
        }

        [Keyword("GetButtonState")]
        public void GetButtonState(String VariableName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(VariableName)) throw new ArgumentException("VariableName must not be empty.");
                Initialize();

                String state = "disabled";
                if (!mElement.GetAttribute("class").ToLower().Contains("disabled") && mElement.Enabled)
                {
                    state = "enabled";
                }
                DlkVariable.SetVariable(VariableName, state);
                DlkLogger.LogInfo(String.Format("Assigned {0} to {1}", state, VariableName));
            }
            catch (Exception e)
            {
                    throw new Exception("GetButtonState() failed : " + e.Message, e);
            }

        }


        [Keyword("GetSwitchState", new String[] { "1|text|Resource name|Resource Name", 
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetSwitchState(string VariableName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(VariableName)) throw new ArgumentException("VariableName must not be empty.");
                Initialize();
                String ActValue = GetSwitchValue(mElement);
                DlkVariable.SetVariable(VariableName, ActValue);
                DlkLogger.LogInfo(String.Format("Assigned {0} to {1}", ActValue, VariableName));
            }
            catch (Exception e)
            {
                throw new Exception("GetSwitchState() failed : " + e.Message);
            }
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

        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyToolTip(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActToolTip = String.Empty;
                String sClass = String.Empty;
                sClass = mElement.GetAttribute("class");
                DlkBaseControl ctlButton = new DlkBaseControl("Button", mElement);
                IWebElement mSub = ctlButton.GetParent();
                if ((sClass.Contains("statusIndicator")) || (sClass.Contains("ui-dialog-titlebar")))
                {
                    ActToolTip = mElement.GetAttribute("title");
                }
                else if (sClass.Contains("icon-listview") || sClass.Contains("icon-formview"))
                {
                    ActToolTip = mSub.GetAttribute("title");
                }
                else if (sClass.Contains("toolTipButton"))
                {
                    MouseOver();
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'tpd-content')]")).Count > 0)
                    {
                        ActToolTip = DlkString.RemoveCarriageReturn(new DlkButton("Tooltip", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@class,'tpd-content')]"))).GetValue());
                    }
                    else
                    {
                        ActToolTip = mElement.GetAttribute("data-original-title");
                    }
                }
                else
                {
                    DlkBaseControl ctlSub = new DlkBaseControl("Parent", mSub);
                    IWebElement mMain = ctlSub.GetParent();
                    ActToolTip = mMain.GetAttribute("title");
                    if (ActToolTip=="")
                    {
                        ActToolTip = mElement.GetAttribute("title");
                    }
                }            
                DlkAssert.AssertEqual("Verify tooltip for button: " + mControlName, ExpectedValue, ActToolTip);
                DlkLogger.LogInfo("VerifyToolTip() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolTip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyImageOverlayText", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyImageOverlayText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActValue = mElement.Text.Trim();;
                DlkAssert.AssertEqual("Verify image overlay text for button: " + mControlName, ExpectedValue, ActValue);
                DlkLogger.LogInfo("VerifyImageOverlayText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyImageOverlayText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySwitchState", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySwitchState(String ExpectedValue)
        {
            try
            {
                Initialize();   
                String ActValue = GetSwitchValue(mElement);
                DlkAssert.AssertEqual("Verify state for switch button: " + mControlName, ExpectedValue, ActValue);
                DlkLogger.LogInfo("VerifySwitchState() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySwitchState() failed : " + e.Message, e);
            }
        }

        #region PRIVATE METHODS

        private new String IsReadOnly()
        {
            String sValue = "";
            if (mElement == null)
            {
                FindElement();
            }
            
            sValue = mElement.GetAttribute("class");
            if (sValue.Contains("disabled"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            sValue = mElement.GetAttribute("readonly");
            if (sValue != null)
            {
                DlkLogger.LogInfo("readonly");
                if (sValue == "readonly")
                {
                    sValue = "TRUE";
                }
                return sValue;
            }

            sValue = mElement.GetAttribute("readOnly");
            if (sValue != null)
            {
                DlkLogger.LogInfo("readOnly");
                return sValue;
            }

            sValue = mElement.GetAttribute("disabled");
            if (sValue != null)
            {
                DlkLogger.LogInfo("disabled");
                return sValue;
            }

            sValue = mElement.GetAttribute("isDisabled");
            if (sValue != null)
            {
                DlkLogger.LogInfo("isDisabled");
                return sValue;
            }

            sValue = mElement.GetAttribute("contenteditable");
            if (sValue != null && sValue.Contains("false"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            sValue = mElement.GetAttribute("src");
            if (sValue != null && sValue.ToLower().Contains("disabled"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }
           
            return "false";
        }

        private new void MouseOverUsingJavaScript()
        {
            FindElement();
            String mouseOverScript = "if(document.createEvent){" +
                                        " var evObj = document.createEvent('MouseEvents');" +
                                        " evObj.initEvent('mouseover', true, false);" +
                                        " arguments[0].dispatchEvent(evObj);" +
                                        "}" +
                                        "else if(document.createEventObject){" +
                                        " arguments[0].fireEvent('onmouseover');" +
                                        "}";
            //String mouseOverScript = "return arguments[0].mouseover();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            js.ExecuteScript(mouseOverScript, mElement);
            DlkLogger.LogInfo("MouseOverUsingJavaScript() completed.");
        }

        private String GetSwitchValue(IWebElement SwitchButton)
        {
            string switchState = "off";
            string switchType = string.Empty;
            IWebElement mSwitch = null;

            //check switch button type.
            if (SwitchButton.GetAttribute("class").Contains("slider"))
                switchType = "slider";
            if (SwitchButton.GetAttribute("id").ToLower().Contains("totalswitch"))
                switchType = "totalswitch";

            switch (switchType)
            {
                case "slider":
                    mSwitch = SwitchButton.FindElement(By.XPath(".//preceding-sibling::input[1]"));
                    switchState = mSwitch.GetAttribute("checked") == "true" ? "on" : "off";
                    break;
                case "totalswitch":
                    mSwitch = SwitchButton.FindElement(By.Id("totalsSwitch"));
                    switchState = mSwitch.GetAttribute("class").ToLower().Contains("on") ? "on" : "off";
                    break;
                default:
                    throw new Exception("Unidentified switch button type.");
            }
            return switchState;
        }
        #endregion
    }
}
