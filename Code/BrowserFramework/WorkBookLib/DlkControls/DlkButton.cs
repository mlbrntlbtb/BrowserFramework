using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;

namespace WorkBookLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
        }

        public bool isIgnoreReadOnly()
        {
            bool hasIgnoreReadOnly = false;
            if (mElement.GetAttribute("class") != null & mElement.GetAttribute("class").ToLower().Contains("ignorereadonly"))
                hasIgnoreReadOnly = true;
            return hasIgnoreReadOnly;
        }
        #endregion

        #region KEYWORDS
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                mElement.Click();
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue")]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkBaseControl button = new DlkBaseControl("Button", mElement);
                string ActualValue = mElement.GetAttribute("title") != null ?
                    DlkString.ReplaceCarriageReturn(mElement.GetAttribute("title").Trim(), "") :
                    DlkString.ReplaceCarriageReturn(button.GetValue().Trim(), "") != "" ?
                    DlkString.ReplaceCarriageReturn(button.GetValue().Trim(), "") : "";
                DlkAssert.AssertEqual("VerifyValue() :", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue")]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();
                DlkBaseControl button = new DlkBaseControl("Button", mElement);
                string ActualValue = mElement.GetAttribute("title") != null ?
                    DlkString.ReplaceCarriageReturn(mElement.GetAttribute("title").Trim(), "") :
                    DlkString.ReplaceCarriageReturn(button.GetValue().Trim(), "") != "" ?
                    DlkString.ReplaceCarriageReturn(button.GetValue().Trim(), "") : "";
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                bool ActualValue = Convert.ToBoolean(IsReadOnly()) ? isIgnoreReadOnly() ? false : true : false;
                DlkAssert.AssertEqual("VerifyReadOnly() : ", Convert.ToBoolean(ExpectedValue.ToLower()), ActualValue);
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("ButtonFileUpload", new String[] { "1|text|Expected Value|SampleValue" })]
        public void ButtonFileUpload(String FilePath)
        {
            try
            {
                IWebElement inputButton = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSearchValues[0]));

                if (inputButton.TagName.ToLower().Equals("input") &&
                    inputButton.GetAttribute("type").Contains("file"))
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    js.ExecuteScript("arguments[0].style.visibility='visible'", inputButton);
                    inputButton.SendKeys(FilePath);
                }
                else
                {
                    throw new Exception("Button does not support file upload. It should contain 'type' attribute with 'file' value.");
                }
                DlkLogger.LogInfo("ButtonFileUpload() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ButtonFileUpload() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}