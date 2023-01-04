using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace MaconomyNavigatorLib.DlkControls
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

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify="")
        {
            try
            {
                Initialize();
                new DlkBaseControl("Label", mElement).ScrollIntoViewUsingJavaScript();
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyTextContains(String TextToEnter)
        {
            try
            {
                FindElement();
                String ActualResult = String.Empty;

                ActualResult = mElement.Text.Trim();
                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActualResult.Contains(TextToEnter));

            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
        }

        [Keyword("AssignPartialValueToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public override void AssignPartialValueToVariable(string VariableName, string StartIndex, string Length)
        {
            String txtValue = base.GetAttributeValue("innerText");
            if (string.IsNullOrEmpty(txtValue))
            {
                DlkVariable.SetVariable(VariableName, string.Empty);
            }
            else
            {
                DlkVariable.SetVariable(VariableName, txtValue.Substring(int.Parse(StartIndex), int.Parse(Length)));
            }
            DlkLogger.LogInfo("Successfully executed AssignPartialValueToVariable().");
        }

    }
}
