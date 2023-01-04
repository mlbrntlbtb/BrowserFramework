using System;
using System.Collections.Generic;
using System.Threading;
using AjeraLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("ImageButton")]
    class DlkImageButton: DlkAjeraBaseControl
    {
        
        #region DECLARATIONS
        private Boolean IsInit;
        private int retryLimit = 3;
        #endregion

        #region CONSTRUCTOR

        public DlkImageButton(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) {}

        public DlkImageButton(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues){}

        public DlkImageButton(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement){}

        public DlkImageButton(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkImageButton(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector){}


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
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                MouseOver();
                
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    ClickUsingJavaScript();
                }
                else
                {                
                    Click(4.5);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToLower().Contains("the http request to the remote webdriver server for url"))
                    throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        //[Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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

        //[Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }
        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST

        [Keyword("ClickByRow", new String[] { "1|text|Value|SampleValue" })]
        public void ClickByRow(String RowNumber)
        {
            try
            {
                bool bFound = false;
                int curRetry = 0;
                InitializeRow(RowNumber);

                DlkImageButton button = new DlkImageButton("Image Button", mElement);

                while (++curRetry <= retryLimit && !bFound)
                {
                    if (button.Exists())
                    {
                        button.Click();
                        bFound = true;

                        DlkLogger.LogInfo("Successfully executed ClickByRow()");
                    }
                }

                if (!bFound)
                {
                    throw new Exception("ClickByRow() failed. Control : " + mControlName + " : '" + RowNumber +
                                       "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickByRow() failed : " + e.Message, e);
            }
        }

        //[Keyword("VerifyEnabledByRow", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyEnabledByRow(String RowNumber, String IsTrueOrFalse)
        {
            try
            {
                InitializeRow(RowNumber);
                string ActValue = Exists().ToString();
                DlkAssert.AssertEqual("VerifyEnabledByRow()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyEnabledByRow() failed : " + e.Message, e);
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
            if (mElement.TagName == "button" || mElement.TagName == "img")
            {
                return true;
            }
            return false;
        }

        public String GetText()
        {
            Initialize();
            String mText = "";
            mText = GetAttributeValue("value");
            return mText;
        }

        #endregion

    }
}
