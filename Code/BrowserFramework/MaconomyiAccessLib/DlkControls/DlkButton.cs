using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {

        #region PRIVATE VARIABLES

        private Boolean IsInit = false;

        #endregion

        #region CONSTRUCTORS

        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkButton(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                IsInit = true;
            }
        }

        #endregion

        #region KEYWORDS

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                ScrollIntoViewUsingJavaScript();
                if (mElement.GetAttribute("class") == "popupBtn")
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
                    if (mSearchType.ToLower().Contains("iframe"))
                    {
                        mElement.Click();
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = new DlkBaseControl("TargetButton", mElement).GetValue().Trim();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue.ToLower(), ActText.ToLower());
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
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    String tempValue = mElement.GetAttribute("class");
                    if (tempValue.Contains("disabled"))
                    {
                        DlkLogger.LogInfo("disabled");
                        ActValue = "true";
                    }
                }
                DlkAssert.AssertEqual("VerifyAttribute()", ExpectedValue.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextContains(String PartialText)
        {
            try
            {
                Initialize();
                String ActualText = new DlkBaseControl("TargetButton", mElement).GetValue();
                DlkAssert.AssertEqual("Verify text contains for button: " + mControlName, PartialText.ToLower(), ActualText.ToLower(), true);
                DlkLogger.LogInfo("VerifyTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("GetReadOnlyState", new String[] { "1|text|VariableName|SampleVar" })]
        public void GetReadOnlyState(String VariableName)
        {
            try
            {
                Initialize();
                ScrollIntoViewUsingJavaScript();

                string state = !mElement.Enabled ? "True" : "False";
                DlkVariable.SetVariable(VariableName, state);
                DlkLogger.LogInfo("[" + state + "] assigned to Variable Name: [" + VariableName + "]");
            }
            catch (Exception e)
            {
                throw new Exception("GetReadOnlyState() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExactText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = new DlkBaseControl("TargetButton", mElement).GetValue().Trim();
                DlkAssert.AssertEqual("Verify exact text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyExactText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactTextContains(String PartialText)
        {
            try
            {
                Initialize();
                String ActualText = new DlkBaseControl("TargetButton", mElement).GetValue();
                DlkAssert.AssertEqual("Verify exact text contains for button: " + mControlName, PartialText, ActualText, true);
                DlkLogger.LogInfo("VerifyExactTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("ButtonFileUpload", new String[] { "1|text|Expected Value|SampleValue" })]
        public void ButtonFileUpload(String FilePath)
        {
            try
            {
                Initialize();
                if (mElement.TagName.ToLower().Equals("input") &&
                    mElement.GetAttribute("type").Contains("file"))
                {
                    mElement.SendKeys(FilePath);
                }
                else
                {
                    throw new Exception("Button does not support file upload.");
                }

                DlkLogger.LogInfo("ButtonFileUpload() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ButtonFileUpload() failed : " + e.Message, e);
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

        [Keyword("HoverClickHold")]
        public void HoverClickHold(String SecondsToHold)
        {
            try
            {
                if (!int.TryParse(SecondsToHold, out int sec) || sec == 0)
                    throw new Exception("[" + SecondsToHold + "] is not a valid input for parameter SecondsToHold.");

                Initialize();
                Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(mElement).ClickAndHold(mElement).Build().Perform();
                Thread.Sleep(sec * 1000);
                mAction.MoveToElement(mElement).Release();
                DlkLogger.LogInfo("HoverClickHold() passed");
            }
            catch (Exception e)
            {
                throw new Exception("HoverClickHold() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
