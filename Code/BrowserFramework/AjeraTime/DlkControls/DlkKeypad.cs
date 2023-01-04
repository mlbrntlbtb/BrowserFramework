using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AjeraTimeLib.DlkSystem;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Threading;

namespace AjeraTimeLib.DlkControls
{
    [ControlType("Keypad")]
    public class DlkKeypad : DlkAjeraTimeBaseControl
    {
        #region DECLARATIONS
        #endregion

        #region CONSTRUCTOR
         public DlkKeypad(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkKeypad(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkKeypad(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkKeypad(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

        #endregion

        #region KEYWORDS
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectKey", new String[] { "1|text|Key Value|0" })]
        public void SelectKey(String Key)
        {
            try
            {
                bool bFound = false;
                DlkBaseControl dlkKey = null;

                Initialize();
                
                mElementList = mElement.FindElements(By.TagName("a"));
                foreach(IWebElement key in mElementList)
                {
                    if(key.Text.Trim() == Key.Trim())
                    {
                        dlkKey = new DlkBaseControl("Key", key);
                        bFound = true;
                        break;
                    }
                }
                if(!bFound)
                {
                    throw new Exception("SelectKey failed. Control: " + mControlName + " : '" + Key +
                                       "' cannot be found.");
                }
                
                dlkKey.MouseOver();
                dlkKey.Click();
                Thread.Sleep(1000);
                
                DlkLogger.LogInfo(string.Format("SelectKey() passed. Key {0} was clicked.", Key));
            }
            catch (Exception e)
            {
                throw new Exception("SelectKey() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyKeyExists", new String[] { "1|text|Key Value|0", "2|text|Expected Value|TRUE" })]
        public void VerifyKeyExists(String Key, String IsTrueOrFalse)
        {
            try
            {
                bool bFound = false;

                Initialize();

                mElementList = mElement.FindElements(By.TagName("a"));
                foreach (IWebElement key in mElementList)
                {
                    if (key.Text.Trim() == Key)
                    {
                        bFound = true;
                    }
                }

                DlkAssert.AssertEqual("VerifyKeyExists()", Convert.ToBoolean(IsTrueOrFalse), bFound);
                DlkLogger.LogInfo("VerifyKeyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyKeyExists() failed : " + e.Message, e);
            }
        }

        #endregion 
    }
}
