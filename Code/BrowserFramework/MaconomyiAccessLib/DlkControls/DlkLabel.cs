using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;


namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        #endregion

        #region KEYWORDS

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify = "")
        {
            try
            {
                Initialize();
                new DlkBaseControl("Label", mElement).ScrollIntoViewUsingJavaScript();

                String ActualValue = DlkString.RemoveCarriageReturn(mElement.Text.Trim());
                DlkLogger.LogInfo("Actual Text Value: [" + ActualValue + "]");
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify.ToLower(), ActualValue.ToLower());
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
                Initialize();
                new DlkBaseControl("Label", mElement).ScrollIntoViewUsingJavaScript();
                String ActualValue = DlkString.RemoveCarriageReturn(mElement.Text.Trim());
                DlkLogger.LogInfo("Actual Text Value: [" + ActualValue + "]");
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActualValue.ToLower().Contains(TextToEnter.ToLower()));
                DlkLogger.LogInfo("VerifyTextContains() passed");
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

        [Keyword("VerifyExactText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyExactText(String TextToVerify = "")
        {
            try
            {
                Initialize();
                new DlkBaseControl("Label", mElement).ScrollIntoViewUsingJavaScript();

                String ActualValue = DlkString.RemoveCarriageReturn(mElement.Text.Trim());
                DlkLogger.LogInfo("Actual Text Value: [" + ActualValue + "]");
                DlkAssert.AssertEqual("VerifyExactText() : " + mControlName, TextToVerify, ActualValue);
                DlkLogger.LogInfo("VerifyExactText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactTextContains", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExactTextContains(String TextToEnter)
        {
            try
            {
                FindElement();
                Initialize();
                new DlkBaseControl("Label", mElement).ScrollIntoViewUsingJavaScript();
                String ActualValue = DlkString.RemoveCarriageReturn(mElement.Text.Trim());
                DlkLogger.LogInfo("Actual Text Value: [" + ActualValue + "]");
                DlkAssert.AssertEqual("VerifyExactTextContains() : " + mControlName, true, ActualValue.Contains(TextToEnter));
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
