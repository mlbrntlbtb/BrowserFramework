using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("RichTextEditor")]
    public class DlkRichTextEditor : DlkBaseControl
    {
        #region Declarations

        private bool _iframeSearchType = false;

        #endregion

        #region Constructors

        public DlkRichTextEditor(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { initialize(SearchType); }
        public DlkRichTextEditor(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { initialize(SearchType); }
        public DlkRichTextEditor(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) 
        {
            DlkEnvironment.AutoDriver.SwitchTo().Frame(ExistingWebElement);
            DlkEnvironment.mSwitchediFrame = true;
            mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("html/body"));
        }

        #endregion

        #region Methods

        public void initialize(string searchType)
        {
            if (searchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                FindElement();
                DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']")));
                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("html/body"));
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
                DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']")));
                DlkEnvironment.mSwitchediFrame = true;
                FindElement();
            }
        }

        #endregion

        #region Keywords

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                //mElement.SendKeys(Keys.Return);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
            finally
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            //String ActValue = GetAttributeValue("value");
            try
            {
                String ActValue = new DlkBaseControl("Control", mElement).GetValue();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() failed : " + ex.Message, ex);
            }
            finally
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() failed : " + ex.Message, ex);
            }
            finally
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                if (_iframeSearchType)
                {
                    bool actualResult = mElement.Displayed;
                    DlkAssert.AssertEqual("VerifyExists() : " + mControlName, expectedResult, actualResult);
                }
                else
                {
                    base.VerifyExists(expectedResult);
                }

                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() failed : " + ex.Message, ex);
            }
            finally
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        #endregion
    }
}
