using System;
using ProjectInformationManagementLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;
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
                if (mControlName == "Login") //special case
                    try { Click(5); }
                    catch { Click(10,10); }
                else
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        ClickIfExistsAndEnabled(10,true);
                    }
                    else
                    {
                        ClickElementIfExistsAndEnabled(10, 10, true);
                    }
                }
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

        [Keyword("ClickThenHold")]
        public void ClickThenHold()
        {
            try
            {
                Initialize();
                ClickAndHold();
                
            }
            catch (Exception e)
            {
                throw new Exception("ClickThenHold() failed : " + e.Message, e);
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
                bool bExists = false;
                Initialize();
                FindElement();
                if (mElement.Displayed)
                {
                    bExists = true;
                }
                DlkAssert.AssertEqual("VerifyExists()", Convert.ToBoolean(IsTrueOrFalse), bExists);
                DlkLogger.LogInfo("VerifyExists() passed");
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                Initialize();
                FindElement();
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        #endregion

        #region METHODS
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
