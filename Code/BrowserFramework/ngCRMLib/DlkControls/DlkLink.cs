using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace ngCRMLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkLink(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "a")
            {
                return true;
            }
            else if(mElement.TagName == "span")
            {
                if (GetAttributeValue("class").ToLower() == "linked-text")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [Keyword("Click")]
        public new void Click()
         {
             
            try
            {
                Initialize();
                this.ClickByObjectCoordinates();
                //ClickUsingJavaScript();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            Initialize();
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetValue());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));                
        }

       private new Boolean Exists()
        {
            Boolean bExists = false;
            bExists = Exists(2);
            return bExists;
        }
        
        [Keyword("ClickIfExists",new String[] { "1|text|Expected Error|TRUE or FALSE" })]
        //True = catch an error
        //false = do not log an error
       public void ClickIfExists(String TrueOrFalse)
        {
            Boolean bError = Convert.ToBoolean(TrueOrFalse);
            try
            {
                if (Exists())
                    Click(1.5);
            }
            catch (Exception e)
            {
                if (bError)
                {
                    throw new Exception("Click() failed : " + e.Message, e);
                }
                else
                {
                    DlkLogger.LogInfo("Control does not exist. Control: " + mControlName);
                }
            }
           
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool isReadOnly = mElement.GetAttribute("class").ToLower().Contains("disabled");
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), isReadOnly);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyReadOnly failed : ", ex);
            }
        }
    }
}
