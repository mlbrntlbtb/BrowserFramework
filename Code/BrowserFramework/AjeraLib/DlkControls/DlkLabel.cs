using System;
using AjeraLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace AjeraLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkAjeraBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit = false;
        #endregion

        #region CONSTRUCTORS
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
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
        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            try
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
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualResult);
            }
            catch(Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
           
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            try
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
            catch (Exception e)
            {

                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
            
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST

        [Keyword("VerifyTextByRow", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextByRow(String RowNumber, String ExpectedValue)
        {
            try
            {
                InitializeRow(RowNumber);
                if (mElement.Displayed)
                {
                    String ActualResult = "";
                    ActualResult = GetValue();
                    if (ActualResult.Contains("\r\n"))
                    {
                        ActualResult = ActualResult.Replace("\r\n", "<br>");
                    }
                    DlkAssert.AssertEqual("VerifyTextByRow() : " + mControlName, ExpectedValue, ActualResult);
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

        #endregion

        #region METHODS
        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "label")
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
