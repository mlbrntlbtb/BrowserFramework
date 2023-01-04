using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace ngCRMLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        #region Constructors
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {

            FindElement();


        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "div" || mElement.TagName == "span")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            FindElement();
            String ActualResult = "";

            // Below style does not work on IE
            //ActualResult = mElement.GetAttribute("textContent").Trim();
            if(!mElement.Displayed)
            {
                DlkBaseControl ctlText = new DlkBaseControl("Text", mElement);
                ctlText.ScrollIntoViewUsingJavaScript();
            }
            if(mElement.GetAttribute("class").Equals("right edit"))
            {
                ActualResult = new DlkBaseControl("Text", mElement.FindElement(By.XPath("./input"))).GetValue();
            }
            else
            {
                ActualResult = mElement.Text.Trim();
                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
            }
       
            DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualResult);
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            FindElement();
            String ActualResult = "";

            // Below style does not work on IE
            //ActualResult = mElement.GetAttribute("textContent").Trim();

            ActualResult = mElement.Text.Trim();
            if (ActualResult.Contains("\r\n"))
            {
                ActualResult = ActualResult.Replace("\r\n", "<br>");
            }
            DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActualResult.Contains(ExpectedValue));
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
        }

        [Keyword("FieldExists", new String[] { "1|text|VariableName|TrueFalse" })]
        public void FieldExists(String VariableName)
        {
           // Initialize();
            if (this.Exists())
            {
                DlkVariable.SetVariable(VariableName, true.ToString());
            }
            else
            {
                DlkVariable.SetVariable(VariableName, false.ToString());
            }
        }

        [Keyword("VerifyRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRequired(String TrueOrFalse)
        {
            DlkAssert.AssertEqual("VerifyRequired() : " + mControlName, Convert.ToBoolean(TrueOrFalse), GetAttributeValue("class").ToLower().Contains("require"));
        }

        [Keyword("Click")]
        public new void Click()
        {
           // Initialize();
            base.Click();
        }

        //This keyword for removing the Multiselect field in Custom Search dialog
        [Keyword("RemoveField", new String[] { "1|text|Value|ItemToDelete" })]
        public void RemoveField()
        {
            Initialize();
            try
            {
                MouseOver();
                IWebElement mField = this.mElement.FindElement(By.ClassName("deleteable"));
                DlkBaseControl ctl = new DlkBaseControl("element", mField);
                //ctl.Click();
                ctl.ClickUsingJavaScript();
                DlkLogger.LogInfo("RemoveField() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("RemoveField() failed : " + e.Message, e);
            }
        }

    }
}
