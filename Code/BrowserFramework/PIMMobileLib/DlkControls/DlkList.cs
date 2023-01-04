using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;

namespace PIMMobileLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        #region DECLARATIONS
        #endregion

        #region CONSTRUCTOR

        public DlkList(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement){}

        public DlkList(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue){}

        public DlkList(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues){}

        public DlkList(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector){}

        public DlkList(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue){}


        public void Initialize()
        {
            FindElement();
        }
        #endregion

        #region KEYWORDS
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

    }
}
