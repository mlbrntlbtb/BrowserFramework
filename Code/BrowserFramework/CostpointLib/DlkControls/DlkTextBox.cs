using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CostpointLib.DlkUtility;
using CostpointLib.DlkControls;


namespace CostpointLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        private bool mSetRetry = false;
        private string mstrPopupCommentOkButtonXPATH = "//input[@id='expandoOK']";
        private string mstrPopupCommentCloseButtonXPATH = "//div[@id='expandoClose']";

        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        
        public new bool VerifyControlType()
        {
            FindElement();
            if (GetAttributeValue("type") == "text")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {
            FindElement();

        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            Set(TextToEnter, true);
        }

        /// <summary>
        /// Set Text box value
        /// </summary>
        /// <param name="TextToEnter">Text to set</param>
        /// <param name="UseShiftTabOut">True to shift+tab after setting. Shift+tab may cause unexpected behavior in some controls, set to false in that case.</param>
        public void Set(String TextToEnter, bool UseShiftTabOut)
        {
            try
            {
                Initialize();
                if (mControlName == "EditYear" && !DlkEnvironment.mIsMobileBrowser)
                {
                    DlkBaseControl yearEdit = new DlkBaseControl("year", mElement);
                    yearEdit.FindElement();
                    yearEdit.DoubleClick();
                }
                else
                {
                    mElement.Clear();
                    mElement.Click();
                }

                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    mElement.SendKeys(TextToEnter);
                    //Thread.Sleep(DlkEnvironment.MediumWaitMs);
                    DlkCostpointCommon.WaitLoadingFinished(DlkCostpointCommon.IsCurrentComponentModal());
                    //if the Selenium SendKeys failed, try to use the Browser's native send keys method
                    // if failed, simply retry. This happens because of slow performace of AutoComplete
                    bool isEqualCaseInsensitive = mElement.GetAttribute("value").ToLower() == TextToEnter.ToLower();
                    if (!isEqualCaseInsensitive || mElement.GetAttribute("value") != TextToEnter)
                    {
                        mElement.Clear();
                        mElement.Click();
                        mElement.SendKeys(TextToEnter);
                        //Thread.Sleep(DlkEnvironment.MediumWaitMs);
                    }
                }

                //mElement.SendKeys(Keys.Shift + Keys.Tab);
                if (UseShiftTabOut)
                {
                    ShiftTab();
                }
                else
                {
                    mElement.SendKeys(Keys.Tab);
                }

                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (ElementClickInterceptedException) when (!mSetRetry)
            {
                mSetRetry = true;
                ScrollIntoViewUsingJavaScript();
                Set(TextToEnter, UseShiftTabOut);
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
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

        [Keyword("VerifyIfBlank", new String[] { "1|text|Expected Value|SampleValue" })]
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


        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String TextToVerify)
        {
            try
            {
                String ActValue = null;
                if (GetAttributeValue("class").ToLower() == "totval") // bottom multipart table
                {
                    ActValue = mElement.Text;
                }
                else
                {
                    ActValue = GetAttributeValue("value");
                }

                if(IsPasswordControl())
                    DlkAssert.AssertEqualPassword("VerifyText()", TextToVerify, ActValue);
                else
                    DlkAssert.AssertEqual("VerifyText()", TextToVerify, ActValue);

                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTextBoxButton")]
        public void ClickTextBoxButton()
        {
            try
            {
                //Click first on the text box to bring up the textbox button
                Click();
                DlkButton btnTextBoxButton = new DlkButton("TextBoxButton", "XPATH", mSearchValues.First() + "//following-sibling::*[contains(@class,'lookupIcon')]");
                if (btnTextBoxButton.Exists())
                {
                    btnTextBoxButton.Click();
                    Thread.Sleep(DlkEnvironment.mMediumWaitMs);
                    if (!DlkAlert.DoesAlertExist() && mElement.FindElements(By.XPath("//div[@id='subtaskBtn' and contains(@style,'visible')]/input[@id='bOk']/preceding::form[1]")).Count == 0)
                    {
                        btnTextBoxButton.Click(2);
                    }
                }
                else
                {
                    throw new Exception("Button not found within the textbox");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextBoxButton() failed : " + e.Message, e);
            }
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
        }

        [Keyword("ShowAutoComplete", new String[] { "1|text|LookupString|AAA" })]
        public void ShowAutoComplete(String LookupString)
        {
            try
            {
                Initialize();
                if (string.IsNullOrEmpty(LookupString))
                {
                    LookupString = Keys.Backspace;
                }
                mElement.Clear();
                mElement.SendKeys(LookupString);
            }
            catch (Exception e)
            {
                throw new Exception("ShowAutoComplete() failed : " + e.Message, e);
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

        [Keyword("GetAutoCompleteValue", new String[] { "1|text|Row|O{Row}","2|text|VariableName|sampleVar" })]
        public void GetAutoCompleteValue(string Row, string VariableName)
        {
            try
            {
                Initialize();
                List<IWebElement> items = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='fldAutoCompleteDiv']/*[contains(@style,'display: block')]")).ToList();

                if (int.TryParse(Row, out int row))
                {
                    if (items.Count >= row)
                    {
                        DlkBaseControl control = new DlkBaseControl("autoCompleteItem", items[row - 1]);
                        string actualValue = control.GetValue();
                        DlkVariable.SetVariable(VariableName, actualValue);
                    }
                    else
                    {
                        throw new Exception($"GetAutoCompleteValue() failed : Row {Row} cannot be found.");
                    }
                }
                else
                    throw new Exception("VerifyAutoCompleteValue() failed : Incorrect format for Row parameter.");
            }
            catch (Exception e)
            {
                throw new Exception("GetAutoCompleteValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAutoCompleteValue", new String[] { "1|text|Row|O{Row}", "2|text|Expected Value|Sample Value" })]
        public void VerifyAutoCompleteValue(string Row, string ExpectedValue)
        {
            try
            {
                Initialize();
                List<IWebElement> items = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='fldAutoCompleteDiv']/*[contains(@style,'display: block')]")).ToList();

                if (int.TryParse(Row, out int row))
                {
                    if (items.Count >= row)
                    {
                        DlkBaseControl control = new DlkBaseControl("autoCompleteItem", items[row-1]);
                        string actualValue = control.GetValue();
                        DlkAssert.AssertEqual("VerifyAutoCompleteValue()", ExpectedValue, actualValue);
                    }
                    else
                    {
                        throw new Exception($"VerifyAutoCompleteValue() failed : Row {Row} cannot be found.");
                    }
                }
                else
                    throw new Exception("VerifyAutoCompleteValue() failed : Incorrect format for Row parameter.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAutoCompleteValue() failed : " + e.Message, e);
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

        [Keyword("ClickTextBoxPopupButton")]
        public void ClickTextBoxPopupButton()
        {
            try
            {
                //Click first on the text box to show the text entry button
                Click();
                DlkButton btnTextEntryButton = new DlkButton("TextBoxButton", "XPATH", mSearchValues.First() + "//following-sibling::*[contains(@class,'fExpandoBtn')]");
                if (btnTextEntryButton.Exists())
                {
                    btnTextEntryButton.Click();
                }
                else
                {
                    throw new Exception("Button not found within the textbox");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextBoxPopupButton() failed : " + e.Message, e);
            }
        }
 

        [Keyword("ClickTextEntryPopupButton", new String[] { "3|text|Button Name|Ok or Close or Cancel" })]
        public void ClickTextEntryPopupButton(String sButtonCaption)
        {
            try
            {
                Initialize();
                DlkBaseControl btnControl = null;
                if (sButtonCaption.ToLower() == "ok")
                {
                    btnControl = new DlkBaseControl("OK Button", mElement.FindElement(By.XPath(mstrPopupCommentOkButtonXPATH)));
                }
                else if (sButtonCaption.ToLower() == "cancel")
                {
                    btnControl = new DlkBaseControl("Cancel Button", mElement.FindElement(By.XPath(mstrPopupCommentCloseButtonXPATH)));
                }
                else if (sButtonCaption.ToLower() == "close")
                {
                    btnControl = new DlkBaseControl("Close Button", mElement.FindElement(By.XPath(mstrPopupCommentCloseButtonXPATH)));
                }

                if (!btnControl.Exists() || btnControl == null)
                {
                    throw new Exception("Button control: " + sButtonCaption + " not found");
                }
                else
                {
                    btnControl.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextEntryPopupButton() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTextEntryPopupValue", new String[] { "1|text|VariableName|SampleVar" })]
        public void GetTextEntryPopupValue(String sVariableName)
        {
            try
            {
                DlkTextBox txtEntry = new DlkTextBox ("TextBoxEntry", "ID", "expandoEdit");
                string commentValue = txtEntry.GetValue();
                DlkVariable.SetVariable(sVariableName, commentValue);
                DlkLogger.LogInfo("Successfully executed GetTextEntryPopupValue()");
            }
            catch (Exception e)
            {
                throw new Exception("GetTextEntryPopupValue() failed " + e.Message, e);
            }
        }

        [Keyword("SetTextEntryPopupValue", new String[] { "1|text|Field|Sample text" })]
        public void SetTextEntryPopupValue(String TextToEnter)
        {
            try
            {
                DlkTextArea txtEntryPopup = new DlkTextArea("TextEntryPopup", "ID", "expandoEdit");
                txtEntryPopup.Set(TextToEnter);
            }
            catch (Exception e)
            {
                throw new Exception("SetTextEntryPopupValue() failed " + e.Message, e);
            }
        }

        /// <summary>
        /// Checks mControlName is a password control
        /// </summary>
        /// <returns></returns>
        private bool IsPasswordControl()
        {
            var controls = CommonLib.DlkRecords.DlkPasswordMaskedRecord.GetPasswordControls();
            return controls.FirstOrDefault(s => s.Control == mControlName)!=null;
        }
    }
}
