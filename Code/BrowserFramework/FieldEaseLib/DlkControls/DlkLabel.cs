using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Threading;
using FieldEaseLib.DlkSystem;

namespace FieldEaseLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
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

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            FindElement();
            String ActualResult = "";
            ActualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "");
            ExpectedValue = DlkString.ReplaceCarriageReturn(ExpectedValue.Trim(), "");
            DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualResult);
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            FindElement();
            String ActualResult = "";
            ActualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "");
            ExpectedValue = DlkString.ReplaceCarriageReturn(ExpectedValue.Trim(), "");
            DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, ExpectedValue, ActualResult, true);
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }
        #endregion


    }
}
