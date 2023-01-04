using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using DeltekProjectsToolLib.DlkSystem;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace DeltekProjectsToolLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
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
            DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                mElement.Click();
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }
    }
}
