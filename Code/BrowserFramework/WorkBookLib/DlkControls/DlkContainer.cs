using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;

namespace WorkBookLib.DlkControls
{
    [ControlType("Container")]
    public class DlkContainer : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkContainer(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkContainer(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkContainer(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkContainer(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
        }
        #endregion

        #region KEYWORDS
        [Keyword("SetAttributeStyle")]
        public void SetAttributeStyle(String Value)
        {
            try
            {
                Initialize();
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("arguments[0].setAttribute('style','" + Value + "')", mElement);
                DlkLogger.LogInfo("SetAttributeStyle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetAttributeStyle() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}