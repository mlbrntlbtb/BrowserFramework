using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Text.RegularExpressions;

namespace HRSmartLib.LatestVersion.DlkControls
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

            ActualResult = mElement.Text.Trim();
            if (ActualResult.Contains("\r\n"))
            {
                //trim NBSP and change Carriage return to \n
                ActualResult = ActualResult.Replace("\r\n", "\n");
                string[] textItems = ActualResult.Split('\n');
                ActualResult = string.Empty;
                int textItemsLen = textItems.Length - 1;
                for (int i = 0; i < textItemsLen; i++)
                { 
                    ActualResult = string.Concat(ActualResult, textItems[i].Trim(), "\n");
                }
                ActualResult = string.Concat(ActualResult, textItems[textItemsLen]);
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
                ActualResult = ActualResult.Replace("\r\n", "\n");
            }
            DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActualResult.Contains(ExpectedValue));
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }
        //[Keyword("VerifyRequired", new String[] { "1|text|Expected Value|TRUE" })]
        //public void VerifyRequired(String TrueOrFalse)
        //{
        //    DlkAssert.AssertEqual("VerifyRequired() : " + mControlName, Convert.ToBoolean(TrueOrFalse), GetAttributeValue("class").ToLower().Contains("require"));
        //}

    }
}
