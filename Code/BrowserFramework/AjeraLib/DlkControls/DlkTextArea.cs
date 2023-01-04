using System;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("TextArea")]
    class DlkTextArea:DlkBaseControl
    {
        #region DECLARATIONS

        private Boolean IsInit = false;

        #endregion

        #region CONSTRUCTOR

        public DlkTextArea(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkTextArea(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkTextArea(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkTextArea(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkTextArea(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector) {}

        public void Initialize()
        {

            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }

        }
        #endregion

        #region KEYWORDS

        //[Keyword("Click")]
        //public new void Click()
        //{
        //    try
        //    {
        //        base.Click();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Click() failed : " + e.Message, e);
        //    }

        //}

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();

                mElement.Clear();
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);

                    if (mElement.GetAttribute("value").ToLower() != Value.ToLower())
                    {
                        mElement.Clear();
                        mElement.SendKeys(Value);
                    }
                }
                //mElement.SendKeys(Keys.Shift + Keys.Tab);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                String ActValue = GetAttributeValue("value");
                if (ActValue.Contains("\r\n"))
                {
                    ActValue = ActValue.Replace("\r\n", "\n");
                }
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }

        }

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


        [Keyword("AppendText", new String[] {"1|text|Cursor Position|Start", 
                                                "2|text|Text to Append|Additional text" })]
        public void AppendText(String CursorPosition, String TextToAppend)
        {
            try
            {
                Initialize();
                string strCurrentNotes = mElement.GetAttribute("value");

                if (CursorPosition.ToLower() == "start")
                {
                    mElement.Clear();
                    mElement.SendKeys(TextToAppend + strCurrentNotes);
                    DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the start of the text.");
                }
                else if (CursorPosition.ToLower() == "end")
                {
                    mElement.Clear();
                    mElement.SendKeys(strCurrentNotes + TextToAppend);
                    DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the end of the text.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("AppendText() failed " + e.Message, e);
            }
        }

        #endregion
    }
}
