using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WebTimeClockLib.DlkSystem;
using System.Threading;

namespace WebTimeClockLib.DlkControls
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
            FindElement();
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

        [Keyword("ClickIfExists")]
        public void ClickIfExists(string Wait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(Wait, out wait))
                    throw new Exception("[" + Wait + "] is not a valid input for parameter Wait.");
                
                if (Exists(wait))
                {
                    mElement.Click();
                    DlkLogger.LogInfo("Control found. Executing click on current control... ");
                }
                else
                {
                    DlkLogger.LogInfo("Control not found. Proceeding... ");
                }
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickIfExists() failed : " + e.Message, e);
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

        #endregion
    }
}