using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private Boolean IsInit = false;

        #endregion

        #region CONSTRUCTORS

        public DlkLink(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
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

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "a")
            {
                return true;
            }
            else if (mElement.TagName == "span")
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

        #endregion

        #region KEYWORDS

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Click(1.5);
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
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue.ToLower(), GetValue().Trim().ToLower());
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

        [Keyword("ClickIfExists", new String[] { "1|text|Expected Error|TRUE or FALSE" })]

        public void ClickIfExists(String error)
        {
            Boolean bError = Convert.ToBoolean(error);
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

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextContains(String PartialText)
        {
            try
            {
                Initialize();
                String ActualText = new DlkBaseControl("Target Link", mElement).GetValue();
                DlkAssert.AssertEqual("Verify text contains for link: " + mControlName, PartialText.ToLower(), ActualText.ToLower(), true);
                DlkLogger.LogInfo("VerifyTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyExactText(String ExpectedValue)
        {
            Initialize();
            DlkAssert.AssertEqual("VerifyExactText()", ExpectedValue, GetValue().Trim());
        }

        [Keyword("VerifyExactTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactTextContains(String PartialText)
        {
            try
            {
                Initialize();
                String ActualText = new DlkBaseControl("Target Link", mElement).GetValue();
                DlkAssert.AssertEqual("Verify exact text contains for link: " + mControlName, PartialText, ActualText, true);
                DlkLogger.LogInfo("VerifyExactTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactTextContains() failed : " + e.Message, e);
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
