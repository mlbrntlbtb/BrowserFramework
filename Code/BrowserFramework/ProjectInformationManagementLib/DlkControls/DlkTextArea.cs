using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using ProjectInformationManagementLib.DlkSystem;
using System;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTOR
        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextArea(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, IWebElement ExistingWebElement)
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

            if (mSearchType.ToLower().Equals("iframe_xpath"))
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
        #endregion

        #region METHODS
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                FindElement();
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
        #endregion
    }
}
