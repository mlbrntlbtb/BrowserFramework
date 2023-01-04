using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("ExpansionPanel")]
    public class DlkExpansionPanel : DlkBaseControl
    {
        #region Constructors
        public DlkExpansionPanel(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkExpansionPanel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkExpansionPanel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Clicks specified ExpansionPanel
        /// </summary>
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                if (mElement.TagName.Equals("span"))
                {
                    ClickUsingJavaScript();
                }
                else
                {
                    Click(4.5);
                }
                DlkLogger.LogInfo("Click() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

       
        /// <summary>
        /// Verifies if ExpansionPanel exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
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

