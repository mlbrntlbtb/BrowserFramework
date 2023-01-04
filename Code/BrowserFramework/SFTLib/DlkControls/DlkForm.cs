using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using SFTLib.DlkControls;
using SFTLib.DlkUtility;
using System.Threading;

namespace SFTLib.DlkControls
{
    [ControlType("Form")]
    public class DlkForm : DlkBaseControl
    {
        #region Constructors
        public DlkForm(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkForm(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkForm(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                if(DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    Initialize();
                    ScrollIntoViewUsingJavaScript();
                    Thread.Sleep(1000);
                    DlkAssert.AssertEqual("Form", Convert.ToBoolean(strExpectedValue), mElement.Displayed);
                }
                else
                {
                    base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                }
                
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
    }
}
