using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using FieldEaseLib.DlkSystem;

namespace FieldEaseLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        public Boolean GetState()
        {
            Initialize();
            mElement = mElement.TagName.Contains("input") ? mElement : mElement.FindElements(By.TagName("input")).Count > 0 ?
                mElement.FindElement(By.TagName("input")) : throw new Exception("Checkbox input field not found.");
            Boolean currentVal;
            IJavaScriptExecutor jE = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            string cellVal = jE.ExecuteScript("return arguments[0].checked", mElement).ToString().ToLower();
            currentVal = (cellVal.Contains("true")) ? true : false;
            return currentVal;
        }
        #endregion

        #region KEYWORDS

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

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String TrueOrFalse)
        {
            try
            {
                Boolean ExpectedValue = Convert.ToBoolean(TrueOrFalse);
                Boolean ActualValue = GetState();
                DlkAssert.AssertEqual("VerifyValue()", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|TRUE" })]
        public void Set(String TrueOrFalse)
        {
            try
            {
                Initialize();
                Boolean ExpectedValue = Convert.ToBoolean(TrueOrFalse);
                if (ExpectedValue != GetState())
                {
                    mElement.Click();
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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
