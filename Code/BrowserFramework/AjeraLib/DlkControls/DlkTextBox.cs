using System;
using AjeraLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace AjeraLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkAjeraBaseControl
    {
        #region DECLARATIONS

        private Boolean IsInit = false;
        private int retryLimit = 3;

        #endregion

        #region CONSTRUCTORS

        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
        }


        #endregion

        #region KEYWORDS

        //[Keyword("Click")]
        public new void Click()
        {
            try
            {
                base.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
            
        }

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

        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST

        [Keyword("SetByRow", new String[] { "1|text|Value|SampleValue" })]
        public void SetByRow(String RowNumber, String Value)
        {
            try
            {
                bool bFound = false;
                int curRetry = 0;
                InitializeRow(RowNumber);
                DlkTextBox mtextBox = new DlkTextBox("TextBox", mElement);

                while (++curRetry <= retryLimit && !bFound)
                {
                    if (mtextBox.Exists())
                    {
                        mtextBox.Set(Value);
                        bFound = true;
                        DlkLogger.LogInfo("Successfully executed SetByRow()");
                    }
                }

                if (!bFound)
                {
                    throw new Exception("SetByRow() failed. Control : " + mControlName + " : '" + RowNumber +
                                       "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyTextByRow(string RowNumber, string IsTrueOrFalse)
        {
            try
            {
                InitializeRow(RowNumber);
                if (mElement.Displayed)
                {
                    string actualValue = GetValue();
                    DlkAssert.AssertEqual("VerifyTextByRow()", IsTrueOrFalse.ToLower(),
                        DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                    DlkLogger.LogInfo("VerifyTextByRow() passed");
                }
                else
                {
                    throw new Exception("VerifyTextByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                       "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnlyByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyReadOnlyByRow(string RowNumber, string IsTrueOrFalse)
        {
            try
            {
                InitializeRow(RowNumber);
                if (mElement.Displayed)
                {
                    string actualValue = IsReadOnly();
                    DlkAssert.AssertEqual("VerifyReadOnlyByRow()", IsTrueOrFalse.ToLower(),
                        DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                    DlkLogger.LogInfo("VerifyReadOnlyByRow() passed");
                }
                else
                {
                    throw new Exception("VerifyTextByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                        "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnlyByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExistsByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExistsByRow(string RowNumber, string IsTrueOrFalse)
        {
            try
            {
                string actualValue = SelectedElementExists(RowNumber).ToString();
                DlkAssert.AssertEqual("VerifyExistsByRow()", IsTrueOrFalse.ToLower(), DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                DlkLogger.LogInfo("VerifyExistsByRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsByRow() failed : " + e.Message, e);
            }

        }
        #endregion

        #region METHODS

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "input")
            {
                return true;
            }
            return false;
        }

        private void SetText(String sTextToEnter)
        {
            switch (DlkEnvironment.mBrowser.ToLower())
            {
                case "safari":
                    mElement.Clear();
                    mElement.SendKeys(sTextToEnter);
                    break;
                default:
                    mElement.Clear();
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.SendKeys(sTextToEnter).Build().Perform();
                    break;
            }
        }
        #endregion

    }
}
