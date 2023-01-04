using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        { 
            initialize();
        }


        #endregion

        #region Keywords

        [Keyword("Select")]
        public void Select(string TabCaption)
        {
            try
            {
                IWebElement tabElement = mElement.FindElement(By.XPath(@"./li[@role='presentation']/a[contains(text(),'" + TabCaption + "')]"));
                DlkBaseControl tabElementControl = new DlkBaseControl("Tab : " + TabCaption, tabElement);
                tabElementControl.Click();
                DlkLogger.LogInfo("Select( ) executed successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception("Select( ) execution failed. : " + ex.Message, ex);
            }
        }

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

        #region Methods

        private void initialize()
        {
            FindElement();
        }


        #endregion

        #region Properties

        #endregion
    }
}
