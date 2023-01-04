#define NATIVE_MAPPING

using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CPTouchLib.DlkUtility;

namespace CPTouchLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkMobileControl
    {
#if NATIVE_MAPPING
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
#endif
        private const int INT_INDEX_SEARCH_VAL = 2;

        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
#if NATIVE_MAPPING
        private bool mIsWebView = false;
#endif
        public void Initialize(bool findElement=true)
        {
#if NATIVE_MAPPING
            if (mSearchValues != null && mSearchValues.First().Contains(STR_WEBVIEW_INDICATOR))
            {
                mIsWebView = true;
                var sValue = mSearchValues.FirstOrDefault();
                var arrSearch = sValue.Split(CHAR_SEARCH_VAL_DELIMITER);
                mSearchValues[0] = arrSearch[INT_INDEX_SEARCH_VAL];
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
            }
#endif
            if (findElement)
            {
                FindElement();
            }
        }

        [Keyword("Tap")]
        public new void Tap()
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                if (mIsWebView)
                {
                    DlkCPTouchCommon.WaitForScreenToLoad();

                    DlkBaseControl ctlItem = new DlkBaseControl("Item", mElement);
                    DlkCPTouchCommon.WaitForElementToLoad(ctlItem);
                    ctlItem.ScrollIntoViewUsingJavaScript();
                    ctlItem.Click();
                }
                else
                {
                    base.Tap();
                }
#else
                base.Tap();
#endif
                DlkLogger.LogInfo("Successfully executed Tap().");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                
                if (mIsWebView)
                {
                    DlkCPTouchCommon.WaitForScreenToLoad();
                    DlkCPTouchCommon.WaitForElementToLoad(this);
                }

#if NATIVE_MAPPING
                if (!string.IsNullOrEmpty(mElement.Text))
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ios" && !mIsWebView)
                    {
                        //sets focus on element to be cleared
                        Tap();
                    }

                    mElement.Clear();
                }
#else
                mElement.Clear();
#endif

                mElement.SendKeys(TextToEnter);

                HideKeyboard();
                
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }

#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                var textArea = GetTextArea();
                var actualValue = textArea != null ? new DlkMobileControl(mControlName, textArea).GetValue().Trim() : GetValue().Trim();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }

#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyIfBlank", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyIfBlank(String TrueOrFalse)
        {
            try
            {
                Initialize();
                var textArea = GetTextArea();
                var actualValue = textArea != null ? new DlkMobileControl(mControlName, textArea).GetValue().Trim() : GetValue().Trim();
                DlkAssert.AssertEqual("VerifyIfBlank()", Convert.ToBoolean(TrueOrFalse), string.IsNullOrEmpty(actualValue));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfBlank() failed : " + e.Message, e);
            }

#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                var textArea = GetTextArea();
                var me = textArea != null? textArea : mElement;

                if (!mIsWebView)
                {
                    me = GetMeInOtherContext(false, false);
                    mIsWebView = true;
                }

                //Verify readonly attribute based on value picker type controls
                bool isReadOnly = false;
                var chevronTypes = mElement.FindElements(By.XPath("(.//ancestor::div[contains(@id, 'input')]//*[contains(@class, 'chevronfield')])")).FirstOrDefault(button => button.Displayed && button.Enabled);
                var textAreaTypes = mElement.FindElements(By.XPath("(.//ancestor::div[contains(@id, 'input')]//textarea[1])")).FirstOrDefault(button => button.Displayed && button.Enabled);
                var defaultTypes = mElement.FindElements(By.XPath("(.//ancestor::div[contains(@id, 'input')]//*[contains(@id, 'trigger')])")).FirstOrDefault(button => button.Displayed && button.Enabled); 
                
                var sReadOnly = me.GetAttribute("readonly");
                bool actual = false;

                if (defaultTypes != null && textAreaTypes == null)
                {
                    var parentElem = defaultTypes.FindElement(By.XPath(".//ancestor::div[contains(@id, 'input')]//input[1]"));
                    var attr = parentElem.GetAttribute("readonly");
                    bool.TryParse(attr, out isReadOnly);
                }
                else if (textAreaTypes != null && chevronTypes == null)
                {
                    var attr = textAreaTypes.GetAttribute("readonly");
                    bool.TryParse(attr, out isReadOnly);
                }
                else
                {
                    isReadOnly = chevronTypes != null ? false : true;
                }

                actual = !string.IsNullOrEmpty(sReadOnly)
                    && (bool.TryParse(sReadOnly, out actual) && isReadOnly)
                    && actual;

                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), actual);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }

#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize(false);
                if (!bool.TryParse(TrueOrFalse, out bool expected)) throw new Exception("Invalid input: '" + TrueOrFalse + "'. Keyword expecting TRUE or FALSE");

                VerifyExists(expected);
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }

#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("TapButtonIfExists", new String[] { "1|text|ButtonName|SampleValue" })]
        public void TapButtonIfExists(String ButtonName)
        {
            try
            {
                Initialize();
                string xpath = string.Empty;

                switch(ButtonName.ToLower())
                {
                    case "clear":
                        xpath = ".//ancestor::div[contains(@id, 'input')]//*[contains(@id, 'clear')]";
                        break;
                    default:
                        throw new Exception($"{ButtonName} not supported.");
                }

                var button = mElement.FindElements(By.XPath(xpath)).FirstOrDefault(x => x.Displayed && x.Enabled);

                if (button != null)
                {
                    var toClick = new DlkMobileControl(ButtonName, button);
                    toClick.ScrollIntoViewUsingJavaScript();
                    toClick.Click();
                }
                else
                    DlkLogger.LogInfo($"{ButtonName} does not exist. Skipping Tap...");

                DlkLogger.LogInfo("Successfully executed TapButtonIfExists()");
            }
            catch (Exception e)
            {
                throw new Exception("TapButtonIfExists() failed : " + e.Message, e);
            }

#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("AssignValueToVariable")]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                IWebElement textArea = GetTextArea();

                if (textArea != null)
                {
                    new DlkBaseControl(mControlName, textArea).AssignValueToVariable(VariableName);
                }
                else
                    base.AssignValueToVariable(VariableName);
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("AssignPartialValueToVariable")]
        public override void AssignPartialValueToVariable(string VariableName, string StartIndex, string Length)
        {
            try
            {
                Initialize();
                //Retrieve the textarea first.
                //Some textboxes have overlaying elements over the text area which prevents the script from retrieving the proper value.
                var textArea = GetTextArea();

                if (textArea != null)
                {
                    new DlkBaseControl(mControlName, textArea).AssignValueToVariable(VariableName);
                }
                else
                    base.AssignPartialValueToVariable(VariableName, StartIndex, Length);
            }
            catch (Exception e)
            {
                throw new Exception("AssignPartialValueToVariable() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

#region PRIVATE_METHODS
        private IWebElement GetTextArea()
        {
            //Retrieve the textarea first.
            //Some textboxes have overlaying elements over the text area which prevents the script from retrieving the proper value.
            return mElement.FindElements(By.XPath("./ancestor::*[contains(@id, 'field')][1]/descendant::*[@class='x-input-el']")).FirstOrDefault();
        }
#endregion
    }
}
