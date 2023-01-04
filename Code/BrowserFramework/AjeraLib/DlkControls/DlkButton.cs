using System;
using AjeraLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace AjeraLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkAjeraBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;
        private int retryLimit = 3;
        #endregion

        #region CONSTRUCTOR
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
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
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                if (mElement.GetAttribute("class") == "popupBtn")
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
                else
                {
                    try { ClickUsingJavaScript(); }
                    catch (Exception e) { DlkLogger.LogWarning(e.Message); }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("Enter")]
        public void Enter()
        {
            try
            {
                Initialize();
                IWebElement currentElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                IWebElement firstElement = currentElement;
                while (!currentElement.Location.Equals(mElement.Location))
                {
                    if (currentElement.Text != "" && currentElement.Text.Equals(mElement.Text))
                        break;

                    new Actions(DlkEnvironment.AutoDriver).SendKeys(Keys.Tab).Perform();
                    currentElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();

                    if (currentElement.Equals(firstElement))
                        break;

                }

                if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                {
                    Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                    mAction.MoveToElement(mElement);
                    mAction.SendKeys(Keys.Enter);
                    mAction.Build().Perform();
                }
                else
                {
                    FocusUsingJavaScript();
                    mElement.SendKeys(Keys.Return);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Enter() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
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

                DlkImageButton button = new DlkImageButton("Button", mElement);

                while (++curRetry <= retryLimit && !bFound)
                {
                    if (button.Exists())
                    {
                        button.Click(5, 5);
                        bFound = true;

                        DlkLogger.LogInfo("Successfully executed ClickByRow()");
                    }
                }

                if (!bFound)
                {
                    throw new Exception("ClickByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                       "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickByRow() failed : " + e.Message, e);
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
