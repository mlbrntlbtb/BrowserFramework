using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CostpointLib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace CostpointLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {

        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();

                if (!mElement.Displayed)
                    ScrollIntoViewUsingJavaScript(false);
                mElement.Click();
                DlkLogger.LogInfo("Successfully executed Click()");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                mElement.Clear();
                mElement.Click();
                mElement.SendKeys(TextToEnter);
                DlkCostpointCommon.WaitLoadingFinished(DlkCostpointCommon.IsCurrentComponentModal());
                //if the Selenium SendKeys failed, try to use the Browser's native send keys method
                // if failed, simply retry. This happens because of slow performace of AutoComplete
                bool isEqualCaseInsensitive = mElement.GetAttribute("value").ToLower() == TextToEnter.ToLower();
                if (!isEqualCaseInsensitive || mElement.GetAttribute("value") != TextToEnter)
                {
                    mElement.Clear();
                    mElement.Click();
                    mElement.SendKeys(TextToEnter);
                }
                DlkLogger.LogInfo("Successfully executed Set() : " + mControlName + ": " + TextToEnter);
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String TextToVerify)
        {
            try
            {
                String actualValue = DlkString.ReplaceCarriageReturn(GetValue(), "\n");
                String expectedValue = DlkString.ReplaceCarriageReturn(TextToVerify, "\n");

                DlkAssert.AssertEqual("VerifyText()", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed " + e.Message, e);
            }
        }

        [Keyword("AppendText", new String[] {"1|text|Cursor Position|Start", 
                                                "2|text|Text to Append|Additional text" })]
        public void AppendText(String CursorPosition, String TextToAppend)
        {
            try
            {
                Initialize();
                string strCurrentNotes = mElement.GetAttribute("value");

                if (CursorPosition.ToLower() == "start")
                {
                    mElement.Clear();
                    mElement.SendKeys(TextToAppend + strCurrentNotes);
                    DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the start of the text.");
                }
                else if (CursorPosition.ToLower() == "end")
                {
                    mElement.Clear();
                    mElement.SendKeys(strCurrentNotes + TextToAppend);
                    DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the end of the text.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("AppendText() failed " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|VariableName|MyVar" })]
        public void GetValue(string sVariableName)
        {
            try
            {
                Initialize();
                String txtValue = GetAttributeValue("value");
                DlkVariable.SetVariable(sVariableName, txtValue);
                DlkLogger.LogInfo("Successfully executed GetValue().");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyIfBlank", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIfBlank(String TrueOrFalse)
        {
            try
            {
                String ActValue = GetAttributeValue("value") ?? string.Empty;
                DlkLogger.LogInfo("Actual Value : '" + ActValue + "'");
                DlkAssert.AssertEqual("VerifyIfBlank()", Convert.ToBoolean(TrueOrFalse), string.IsNullOrEmpty(ActValue));
                DlkLogger.LogInfo("VerifyIfBlank() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfBlank() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTextAreaButton")]
        public void ClickTextAreaButton()
        {
            try
            {
                Initialize();                
                MouseOver();
                IWebElement lookup = mElement.FindElement(By.XPath("./following-sibling::div[contains(@class,'lookupIcon3')]"));
                DlkBaseControl mLookup = new DlkBaseControl("lookup", lookup);

                if (new string[] { "ie", "safari" }.Contains(DlkEnvironment.mBrowser.ToLower()))
                {
                    mLookup.ClickUsingJavaScript(false);
                }
                else
                {
                    mLookup.Click();
                }
                DlkLogger.LogInfo("ClickTextAreaButton() Passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextAreaButton() failed : " + e.Message, e);
            }

        }

        [Keyword("SelectAutoCompleteValue", new String[] { "1|text|Value|Sample Value" })]
        public void SelectAutoCompleteValue(string Value)
        {
            try
            {
                Initialize();
                bool found = false;
                List<IWebElement> items = new List<IWebElement>();
                void getItems() => items = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='fldAutoCompleteDiv' and contains(@style,'display: block')]/*[contains(@style,'display: block')]")).ToList();

                getItems();
                if (items.Count == 0)
                {
                    mElement.Click();
                    Thread.Sleep(300);
                    getItems();
                }

                if (items.Count > 0)
                {
                    foreach (IWebElement item in items)
                    {
                        if (item.Text == Value)
                        {
                            item.Click();
                            found = true;
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Autocomplete values not found");
                }

                if (!found)
                    throw new Exception($"Autocomplete value: '{Value}' not found.");
                else
                    DlkLogger.LogInfo("SelectAutoCompleteValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectAutoCompleteValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextColor", new String[] { "1|text|ExpectedColor|Orange" })]
        public void VerifyTextColor(String ExpectedColor)
        {
            try
            {
                Initialize();
                string style = mElement.GetAttribute("style");
                string actualColor = "Default";

                if (style.Contains("color: rgb(242, 119, 42)"))
                    actualColor = "Orange";

                DlkAssert.AssertEqual("VerifyTextColor()", ExpectedColor.ToLower(), actualColor.ToLower());
                DlkLogger.LogInfo("VerifyTextColor() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextColor() failed : " + e.Message, e);
            }
        }
    }
}
