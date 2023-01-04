using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        
        public DlkLink(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();        
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                FindElement(5); 
                
                try  { mElement.Click(); }
                catch {
                    ClickUsingJavaScript(false);
                    Thread.Sleep(1500);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                if (ActValue == "false")
                {
                    ActValue = mElement.GetAttribute("dis") != null ? "true" : "false";
                }
                DlkAssert.AssertEqual("VerifyAttribute()", strExpectedValue.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("NativeClick")]
        public void NativeClick()
        {
            try
            {
                DlkEnvironment.mIsMobile = true;
                DlkEnvironment.SetContext("NATIVE");
                Initialize();
                mElement.Click();
                DlkLogger.LogInfo("NativeClick() passed");
            }
            catch (Exception e)
            {
                throw new Exception("NativeClick() failed : " + e.Message, e);
            }
            finally
            {
                DlkEnvironment.SetContext("WEBVIEW");
                DlkEnvironment.mIsMobile = false;
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }
    }
}
