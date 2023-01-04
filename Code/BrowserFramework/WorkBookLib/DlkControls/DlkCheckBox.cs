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
using WorkBookLib.DlkSystem;

namespace WorkBookLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private string mClass = "";
        private const string iCheckBoxClass = "icheckbox";
        private const string cCheckBoxClass = "wj-cell";
        #endregion

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
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();

            string sClass = GetAttributeValue("class");

            //Exclude using scroll into checkbox cells to avoid affecting fixed scroll position
            if(!sClass.Contains(cCheckBoxClass))
                this.ScrollIntoViewUsingJavaScript();

            if (sClass.Contains(iCheckBoxClass))
            {
                mClass = iCheckBoxClass;
            }
            else if(sClass.Contains(cCheckBoxClass))
            {
                mClass = cCheckBoxClass;
            }
        }

        public Boolean GetState()
        {
            Initialize();
            Boolean currentVal;

            switch (mClass)
            {
                case iCheckBoxClass:
                    string iClass = GetAttributeValue("class");
                    currentVal = (iClass.Contains("checked")) ? true : false;
                    break;
                case cCheckBoxClass:
                    IJavaScriptExecutor jE = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    IWebElement targetInput = mElement.FindElement(By.TagName("input"));
                    string cellVal = jE.ExecuteScript("return arguments[0].checked", targetInput).ToString().ToLower();
                    currentVal = (cellVal.Contains("true")) ? true : false;
                    break;
                default:
                    IJavaScriptExecutor defJE = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    IWebElement defTargetInput = mElement.FindElement(By.TagName("input"));
                    string defCellVal = defJE.ExecuteScript("return arguments[0].checked", defTargetInput).ToString().ToLower();
                    currentVal = (defCellVal.Contains("true")) ? true : false;
                    break;
            }
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
                    switch (mClass)
                    {
                        case cCheckBoxClass:
                            IWebElement targetInput = mElement.FindElement(By.TagName("input"));
                            targetInput.Click();
                            break;
                        default:
                            IWebElement defTargetInput = mElement.FindElement(By.TagName("input"));
                            try
                            {
                                defTargetInput.Click();
                            }
                            catch
                            {
                                mElement.Click();
                            }
                            break;
                    }
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

        [Keyword("VerifyDisabled", new String[] { "1|text|Value|TRUE" })]
        public void VerifyDisabled(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool expectedValue = Convert.ToBoolean(TrueOrFalse);
                bool isDisabled = mElement.GetAttribute("class").Contains("disabled");
                DlkAssert.AssertEqual("VerifyDisabled(): ", expectedValue, isDisabled);
                DlkLogger.LogInfo("VerifyDisabled() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDisabled() failed : " + e.Message, e);
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

        #endregion
    }
}
