using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using WorkBookLib.DlkSystem;

namespace WorkBookLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES
        string toolTip_XPath = "//div[contains(@class,'ui-tooltip-content')]";
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
        }

        public void ClearField(IWebElement targetElement)
        {
            if(DlkEnvironment.mBrowser != "safari")
            {
                targetElement.SendKeys(Keys.Control + "a");
                targetElement.SendKeys(Keys.Delete);
            }
            else
            {
                targetElement.SendKeys(Keys.Command + "a");
                targetElement.SendKeys(Keys.Delete);
            }
            
        }
        #endregion

        #region KEYWORDS
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                DlkBaseControl targetTextBox = new DlkBaseControl("TextBox", mElement);
                targetTextBox.MouseOver();
                ClearField(mElement);
                targetTextBox.ClickUsingJavaScript();
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                    //Check if loading state after setting of value
                    DlkWorkBookFunctionHandler.WaitScreenGetsReady();
                }
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
                DlkBaseControl targetTextBox = new DlkBaseControl("TextBox", mElement);
                targetTextBox.MouseOver();
                ClearField(mElement);
                targetTextBox.ClickUsingJavaScript();
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                    mElement.SendKeys(Keys.Return);
                    //Check if loading state after setting of value
                    DlkWorkBookFunctionHandler.WaitScreenGetsReady();
                }
                DlkLogger.LogInfo("Successfully executed SetAndEnter()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                Thread.Sleep(2000);
                string ActualValue = !String.IsNullOrEmpty(mElement.Text) ? mElement.Text :
                    !String.IsNullOrEmpty(new DlkBaseControl("TextBox", mElement).GetValue().Trim()) ?
                    new DlkBaseControl("TextBox", mElement).GetValue().Trim() :
                    !String.IsNullOrEmpty(mElement.GetAttribute("value").Trim()) ?
                    mElement.GetAttribute("value").Trim() : null;
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetText", new String[] { "1|text|Value|SampleValue" })]
        public void GetText(String VariableName)
        {
            try
            {
                Initialize();
                Thread.Sleep(2000);
                string ActualValue = !String.IsNullOrEmpty(mElement.Text) ? mElement.Text :
                    !String.IsNullOrEmpty(new DlkBaseControl("TextBox", mElement).GetValue().Trim()) ?
                    new DlkBaseControl("TextBox", mElement).GetValue().Trim() :
                    !String.IsNullOrEmpty(mElement.GetAttribute("value").Trim()) ?
                    mElement.GetAttribute("value").Trim() : null;
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetText() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
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

        [Keyword("VerifyExistTooltip", new String[] { "" })]
        public void VerifyExistTooltip(String TrueOrFalse)
        {
            try
            {
                bool ExpectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out ExpectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                Thread.Sleep(1000);
                IWebElement toolTipElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(toolTip_XPath));
                bool IsToolTipExist = toolTipElement != null ? true : false;
                DlkAssert.AssertEqual("VerifyExistTooltip()", ExpectedValue, IsToolTipExist);
                DlkLogger.LogInfo("VerifyExistTooltip() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextTooltip", new String[] { "" })]
        public void VerifyTextTooltip(String ExpectedValue)
        {
            try
            {
                Initialize();
                Thread.Sleep(1000);
                IWebElement toolTipElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(toolTip_XPath));
                string toolTipValue = toolTipElement != null ? toolTipElement.Text.Trim() : string.Empty;
                DlkAssert.AssertEqual("VerifyTextTooltip()", ExpectedValue, toolTipValue);
                DlkLogger.LogInfo("VerifyTextTooltip() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialTextTooltip", new String[] { "" })]
        public void VerifyPartialTextTooltip(String PartialValue)
        {
            try
            {
                Initialize();
                Thread.Sleep(1000);
                IWebElement toolTipElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(toolTip_XPath));
                string toolTipValue = toolTipElement != null ? toolTipElement.Text.Trim() : string.Empty;
                DlkAssert.AssertEqual("VerifyPartialTextTooltip()", PartialValue, toolTipValue, true);
                DlkLogger.LogInfo("VerifyPartialTextTooltip() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialTextTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTextTooltip", new String[] { "" })]
        public void GetTextTooltip(String VariableName)
        {
            try
            {
                Initialize();
                Thread.Sleep(1000);
                IWebElement toolTipElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(toolTip_XPath));
                string toolTipValue = toolTipElement != null ? toolTipElement.Text.Trim() : string.Empty;
                DlkVariable.SetVariable(VariableName, toolTipValue);
                DlkLogger.LogInfo("[" + toolTipValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetTextTooltip() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetTextTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExistTooltip", new String[] { "" })]
        public void GetVerifyExistTooltip(String VariableName)
        {
            try
            {
                Initialize();
                Thread.Sleep(1000);
                IWebElement toolTipElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(toolTip_XPath));
                bool IsToolTipExist = toolTipElement != null ? true : false;
                DlkVariable.SetVariable(VariableName, IsToolTipExist.ToString());
                DlkLogger.LogInfo("[" + IsToolTipExist.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExistTooltip() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExistTooltip() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
