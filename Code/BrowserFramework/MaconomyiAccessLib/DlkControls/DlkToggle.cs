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

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Toggle")]
    public class DlkToggle : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private Boolean IsInit = false;

        #endregion

        #region CONSTRUCTORS

        public DlkToggle(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToggle(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToggle(String ControlName, IWebElement ExistingWebElement)
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
        [Keyword("Toggle")]
        public void Toggle(String OnOrOff)
        {
            try
            {
                Initialize();
                ScrollIntoViewUsingJavaScript();
                if (OnOrOff.ToLower() != GetToggleState())
                {
                    Click(4.3);                    
                }
                else
                {
                    DlkLogger.LogInfo("Toggle: " + mControlName + " is already in [" + OnOrOff.ToLower() + "] state. No action was made.");
                }
                DlkLogger.LogInfo("Toggle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Toggle() failed : " + e.Message, e);
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

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValue(String OnOrOff)
        {
            try
            {
                Initialize();                
                DlkAssert.AssertEqual("Verify value for Toggle: " + mControlName, OnOrOff.ToLower(), GetToggleState());
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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

        #region PRIVATE METHODS
        private String GetToggleState()
        {
            String toggleState = "off";
            toggleState = mElement.GetAttribute("class").ToLower().Contains("on") ? "on" : "off";
            return toggleState;
        }
        #endregion
    }
}
