using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using BPMLib.DlkControls;
using BPMLib.DlkUtility;
using CommonLib.DlkUtility;
using BPMLib.DlkSystem;

namespace BPMLib.DlkControls
{
    [ControlType("Form")]
    public class DlkForm : DlkBaseControl
    {
        String mDlgTxtXPath = "//div[@id='dlg_txt_alertDlg']";

        public DlkForm(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkForm(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkForm(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
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

        [Keyword("IfExistsThen", new String[] { "1|text|Expected Value|TRUE",
                                                "2|text|Text to verify|No data",
                                                "3|text|GoToStep|1",
                                                "4|text|ElseGoToStep|2"})]
        public void IfExistsThen(String ExpectedValue, String TextToVerify, String GoToStep, String ElseGoToStep)
        {
            try
            {
                bool flag = false;
                int step = -1;

                try
                {
                    base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                    flag = true;
                }
                catch
                {
                    step = Convert.ToInt32(ElseGoToStep);
                    flag = false;
                }

                if (flag)
                {
                    IWebElement elem = mElement.FindElement(By.XPath(mDlgTxtXPath));
                    string actualResult = DlkString.ReplaceCarriageReturn(elem.Text.Trim(), "\n");
                    string textToVerify = DlkString.ReplaceCarriageReturn(TextToVerify, "\n");

                    DlkAssert.AssertEqual("IfExistsThen() : " + mControlName, textToVerify, actualResult, true);
                    step = Convert.ToInt32(GoToStep);
                }

                DlkBPMTestExecute.mGoToStep = (step - 1); // steps are zero based
            }
            catch (Exception e)
            {
                throw new Exception("IfExistsThen() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
    }
}
