using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Image")]
    public class DlkImage : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                base.Click();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("Click() execution failed. " + ex.Message, ex);
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

        [Keyword("VerifyImageSourceExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyImageSourceExists(String TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = this.mElement.GetAttribute("src").Contains("no_employee_photo") ? false : true;
                DlkAssert.AssertEqual("Image", Convert.ToBoolean(TrueOrFalse), actualResult);
                DlkLogger.LogInfo("VerifyImageSourceExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyImageSourceExists() failed : " + e.Message, e);
            }
        }

        [Keyword("Hover")]
        public void Hover()
        {
            try
            {
                initialize();
                base.ScrollIntoViewUsingJavaScript();
                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(mElement).Build();
                mAction.Perform();

                DlkLogger.LogInfo("Hover() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("Hover() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("HoverAndVerifyCaption")]
        public void HoverAndVerifyCaption(string Caption)
        {
            try
            {
                initialize();
                base.ScrollIntoViewUsingJavaScript();
                string actualResult = mElement.GetAttribute("data-original-title") == string.Empty ? mElement.GetAttribute("data-content") : mElement.GetAttribute("data-original-title");
                string expectedResult = Caption.Trim();
                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(mElement).Build(); 
                mAction.Perform();
                DlkAssert.AssertEqual("HoverAndVerifyCaption : ", expectedResult, actualResult);

                DlkLogger.LogInfo("Hover() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Hover() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
