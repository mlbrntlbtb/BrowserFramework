using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using ProjectInformationManagementLib.DlkSystem;
using System;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTOR
        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
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

        #region METHODS
        #endregion
    }
}
