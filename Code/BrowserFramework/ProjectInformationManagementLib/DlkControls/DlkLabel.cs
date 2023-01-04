using System;
using ProjectInformationManagementLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType;
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
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Contains("iframe"))
            {
                _iframeSearchType = true;
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
                //this.ScrollIntoViewUsingJavaScript();
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
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
                FindElement();
                Click(5);
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
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
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
           
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            try
            {
                Initialize();
                FindElement();

                String ActualResult = "";                
                ActualResult = mElement.Text.Trim();
                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "\n");
                }
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, ExpectedValue, ActualResult, true);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        #endregion

    }
}
