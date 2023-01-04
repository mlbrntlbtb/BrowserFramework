using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{

    [ControlType("ModalWindow")]
    public class DlkModalWindow : DlkBaseControl
    {
        private Boolean IsInit = false;
        public DlkModalWindow(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkModalWindow(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkModalWindow(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkModalWindow(String ControlName, IWebElement ExistingWebElement)
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

        #region Veriyf methods
        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyExists(Convert.ToBoolean(expectedValue));
                }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }
        #endregion


    }
}

