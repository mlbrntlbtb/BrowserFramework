using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("SelectAllRows")]
    public class DlkSelectAllRows : DlkBaseControl
    {
        public DlkSelectAllRows(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSelectAllRows(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSelectAllRows(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {

            FindElement();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                Click(1.5);
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

    }
}
