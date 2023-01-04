using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
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
            //Do Nothing.
        }


        #endregion

        #region Keywords

        [Keyword("Select")]
        public void Select(string TabCaption)
        {
            try
            {
                initialize();

                IList<IWebElement> results = HRSmartLib.DlkCommon.DlkCommonFunction.GetElementWithText(TabCaption, 
                                                                                                       mElement,
                                                                                                       false,
                                                                                                       "a",
                                                                                                       true,
                                                                                                       true,
                                                                                                       true);
                if (results.Count > 0)
                {
                    IWebElement tabElement = results[0];
                    IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    javascript.ExecuteScript("arguments[0].scrollIntoView(true);", tabElement);
                    javascript.ExecuteScript("arguments[0].scrollIntoView(false);", tabElement);

                    DlkButton tabElementControl = new DlkButton("Tab : " + TabCaption, tabElement);
                    tabElementControl.Click();
                    DlkLogger.LogInfo("Select( ) executed successfully.");
                }
                else
                {
                    throw new Exception($"Missing Tab Item : {TabCaption}");
                }
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
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTabCaption")]
        public void VerifyTabCaption(string TabCaption, string TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(TabCaption, mElement, true, "a");
                if (elements.Count > 0)
                {
                    actualResult = true;
                }
                DlkAssert.AssertEqual("Tab Caption : " + TabCaption, expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyTabCaption() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyTabCaption() execution failed. : " + ex.Message, ex);
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
