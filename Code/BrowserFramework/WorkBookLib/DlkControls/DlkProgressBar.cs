using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;

namespace WorkBookLib.DlkControls
{
    [ControlType("ProgressBar")]
    public class DlkProgressBar : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private static String mProgressBarWidget_XPath = ".//span[contains(@class,'wbProgressControlFront')]";
        //private static String mProgressBarInput_XPath = ".//input[contains(@class,'wbProgressControlInputOuter)]";
        private IWebElement mProgressBarWidget;
        #endregion

        #region CONSTRUCTORS
        public DlkProgressBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkProgressBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkProgressBar(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkProgressBar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            mProgressBarWidget = mElement.FindElements(By.XPath(mProgressBarWidget_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mProgressBarWidget_XPath)) : throw new Exception("Progress bar widget not found.");
        }
        #endregion

        #region KEYWORDS
        [Keyword("VerifyValue")]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IJavaScriptExecutor jE = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                string actualValue = jE.ExecuteScript("return arguments[0].style.width", mProgressBarWidget).ToString();
                DlkAssert.AssertEqual("VerifyValue() :", ExpectedValue, actualValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("Set")]
        public void Set(String PercentageValue)
        {
            try
            {
                int percentageValue = 0;
                if (!int.TryParse(PercentageValue, out percentageValue))
                    throw new Exception("[" + PercentageValue + "] is not a valid input for parameter PercentageValue.");

                Initialize();
                IJavaScriptExecutor jE = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jE.ExecuteScript("arguments[0].style.width='" + percentageValue.ToString() + "%'", mProgressBarWidget);
                DlkLogger.LogInfo("Set() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue")]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();
                IJavaScriptExecutor jE = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                string actualValue = jE.ExecuteScript("return arguments[0].style.width", mProgressBarWidget).ToString();
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");
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