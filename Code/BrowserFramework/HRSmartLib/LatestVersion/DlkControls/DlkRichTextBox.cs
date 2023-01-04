using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("RichTextEditor")]
    public class DlkRichTextEditor : DlkBaseControl
    {
        #region Declarations

        private bool _iframeSearchType = false;
        private IWebElement toolBar = null;
        private string linkButtonXpath = "//a[contains(@class,'cke_button__link')]";
        private string linkWindowXpath = "//table[@class='cke_dialog_contents']";
        IWebElement textEditorFrame = null;

        #endregion

        #region Constructors

        public DlkRichTextEditor(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { initialize(SearchType, SearchValues[0]); }
        public DlkRichTextEditor(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) 
        {
            DlkEnvironment.AutoDriver.SwitchTo().Frame(ExistingWebElement);
            DlkEnvironment.mSwitchediFrame = true;
            mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("html/body"));
        }

        #endregion

        #region Methods

        public void initialize(string searchType, string searchValue)
        {
            DlkLogger.LogInfo("Initializing");
            Thread.Sleep(1000);
            string richTextEditorIndex = getIndex(searchValue);

            if (searchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                FindElement();
                DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("(//iframe[@class='cke_wysiwyg_frame cke_reset' or @id='translation-wysiwyg-iframe'])[" + richTextEditorIndex + "]")));
                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("html/body"));
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;

                IWebElement frame = DlkEnvironment.AutoDriver.FindElement(By.XPath("(//iframe[@class='cke_wysiwyg_frame cke_reset' or @id='translation-wysiwyg-iframe'])[" + richTextEditorIndex + "]"));
                textEditorFrame = frame;

                // set toolbar before switching to frame
                toolBar = frame.FindElements(By.XPath(".//parent::div/preceding-sibling::span")).Count > 0 ?
                    frame.FindElement(By.XPath(".//parent::div/preceding-sibling::span")) : null;

                DlkEnvironment.AutoDriver.SwitchTo().Frame(frame);
                DlkEnvironment.mSwitchediFrame = true;

                FindElement();
            }

            DlkLogger.LogInfo("Done Initializing");
        }

        private string getIndex(string searchValue)
        {
            string sIndex = searchValue.Substring(searchValue.LastIndexOf('_') + 1, searchValue.Length - searchValue.LastIndexOf('_') - 1);
            int iIndex = int.MinValue;

            if (int.TryParse(sIndex, out iIndex))
            {
                base.mSearchValues[0] = searchValue.Remove(searchValue.LastIndexOf('_'));
                return sIndex;
            }

            return "1";
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
                if (ActValue == "\r\n")
                {
                    ActValue = "";
                }
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

        [Keyword("VerifyTextContains")]
        public void VerifyTextContains(String ExpectedValue)
        {
            //String ActValue = GetAttributeValue("value");
            try
            {
                String ActValue = new DlkBaseControl("Control", mElement).GetValue();
                DlkAssert.AssertEqual("VerifyTextContains()", ExpectedValue, ActValue, true);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTextContains() failed : " + ex.Message, ex);
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
                if (!TrueOrFalse.Equals(string.Empty))
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
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped.");
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

        [Keyword("AddHyperLink", new String[] { "3|text|Value|ValueToSet" })]
        public void AddHyperLink(String DisplayText, String Protocol, String URL)
        {
            try
            {
                // add space to text before adding hyperlink
                mElement.Click();
                mElement.SendKeys(Keys.End);
                mElement.SendKeys(" ");

                if (DlkEnvironment.mSwitchediFrame)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    DlkEnvironment.mSwitchediFrame = false;
                }

                IWebElement linkButton = toolBar.FindElement(By.XPath(linkButtonXpath));
                linkButton.Click();
                Thread.Sleep(3000);

                // initialize web elements
                IWebElement linkWindow = DlkEnvironment.AutoDriver.FindElement(By.XPath(linkWindowXpath));
                IWebElement displayTextEl = linkWindow.FindElement(By.XPath("(.//table[@role='presentation'])[1]/tbody/tr[1]//input"));
                IWebElement urlEl = linkWindow.FindElement(By.XPath("(.//table[@role='presentation'])[2]/tbody/tr[1]//input"));
                IWebElement protocolEl = linkWindow.FindElement(By.XPath("(.//table[@role='presentation'])[2]/tbody/tr[1]//select"));
                IWebElement okEl = linkWindow.FindElement(By.XPath("./tbody/tr[2]//tbody//td[1]/a"));
                SelectElement comboBoxElement = new SelectElement(protocolEl);

                // set the parameters
                if (DisplayText != " ")
                {
                    displayTextEl.SendKeys(DisplayText);
                }
                comboBoxElement.SelectByValue(Protocol);
                urlEl.SendKeys(URL);

                // click OK
                okEl.Click();

                DlkEnvironment.AutoDriver.SwitchTo().Frame(textEditorFrame);
                DlkEnvironment.mSwitchediFrame = true;
                mElement.Click();

                DlkLogger.LogInfo("Successfully executed AddHyperLink()");
            }
            catch (Exception e)
            {
                throw new Exception("AddHyperLink() failed : " + e.Message, e);
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
