using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using TEMobileLib.DlkUtility;
using TEMobileLib.DlkControls;


namespace TEMobileLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        //private String mTextBoxButtonDesc = "lookup_icon";
        private static String ID_STR_PopupContainer = "expandoDiv";
        private static String XPath_STR_PopupTextBox = "//input[@id='expandoEditInput'] | //*[@id='expandoEdit']";
        private static String XPath_STR_PopupTextBoxOK = "//input[@id='expandoOK']";
        private static String ID_NUM_PopupContainer = "numInputDiv";
        private static String XPath_NUM_PopupTextBox = "//input[@id='numInputMobileInput']";
        private static String XPath_NUM_PopupTextBoxOK = "//div[@id='niO']";
        private static String XPath_NUM_PopupTextBoxOK81 = "//div[@id='numInputDone']";
        private static String ID_CAL_PopupContainer = "calDiv";
        private static String XPath_CAL_PopupTextBox = "//input[@id='popupCalMobileInput']";
        private static String XPath_CAL_PopupTextBoxOK = "//*[@id='calOkBtn']";
        private static String XPath_MOBILE_INPUT = "//form//input[contains(@class, 'MobileInput')]";
        private static String XPath_MOBILE_INPUT_NEW = "//form//div[contains(@class,'lkp') and contains(@style,'block')]//input[contains(@class, 'MobileInput')]";
        private static String Xpath_MOBILE_INPUT_FIND = "//form//div[contains(@class,'lkpSearch')]//div[contains(@class, 'rsTbBtn')]";
        private static String Xpath_MOBILE_INPUT_EDIT = "//form//div[contains(@class,'lkpEdit')]//div[contains(@class, 'rsTbBtn')]";
        private static String Xpath_MOBILE_INPUT_SELECT = "//div[contains(@id,'LKP') or contains(@id,'LOOKUP') or contains(@id,'FILE_TYPES')]/ancestor::form[1]/following::*[@class='bOk' and @value='Select']";
        private static String XPath_MOBILE_OK = "//form//*[@id='mbOk']";
        private static String XPath_PopupTextBox_Container = $"//*[@id='{ID_STR_PopupContainer}' or @id='{ID_NUM_PopupContainer}' or @id='{ID_CAL_PopupContainer}']";
        private static String XPath_PopupTextBox_TBField = $"{XPath_STR_PopupTextBox} | {XPath_NUM_PopupTextBox} | {XPath_CAL_PopupTextBox}";

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

        /// <summary>
        /// Set Text box value
        /// </summary>
        /// <param name="TextToEnter">Text to set</param>
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                DlkTEMobileCommon.WaitForElementToLoad(new DlkBaseControl("Textbox", mElement));

                ScrollIntoViewUsingJavaScript();
                mElement.Click();
                DlkTEMobileCommon.WaitLoadingFinished(DlkTEMobileCommon.IsCurrentComponentModal());

                var PopupTextBoxCon = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_PopupTextBox_Container)).FirstOrDefault(item => item.Displayed);

                //Hotfix to re-click and re-find the popup textbox. 
                //Some popup textboxes read as inactive on safari. Focus methods do not work atm, revisit after appium updates.
                if (DlkEnvironment.mBrowser.ToLower() == "safari" && !mElement.Equals(DlkEnvironment.AutoDriver.SwitchTo().ActiveElement()))
                {
                    //re-click for popup textboxes only
                    if (PopupTextBoxCon == null && !DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT)).Any(e => e.Displayed))
                    {
                        mElement.Click();
                        PopupTextBoxCon = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_PopupTextBox_Container)).FirstOrDefault(item => item.Displayed);
                    }
                }

                if (PopupTextBoxCon != null)
                {
                    Thread.Sleep(500);//pause to emsure that the popup textbox is enabled before interacting

                    IWebElement PopupTextBox = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_PopupTextBox_TBField)).FirstOrDefault(item => item.Displayed);

                    if (PopupTextBox != null)
                    {
                        var textBoxType = PopupTextBox.GetAttribute("id").ToLower();

                        if (textBoxType.Contains("expando") || textBoxType.Contains("cal"))
                        {
                            PopupTextBox.Clear();
                            PopupTextBox.SendKeys(TextToEnter);
                        }
                        else if (textBoxType.Contains("num"))
                        {
                            char[] stringToNumPattern = TextToEnter.ToCharArray();
                            foreach (char num in stringToNumPattern)
                            {
                                switch (num)
                                {
                                    case '-':
                                        {
                                            IWebElement btnNum = PopupTextBoxCon.FindElement(By.XPath("//div[@id='niM']"));
                                            btnNum.Click();
                                        }
                                        break;
                                    case '.':
                                        {
                                            IWebElement btnNum = PopupTextBoxCon.FindElement(By.XPath("//div[@id='niD']"));
                                            btnNum.Click();
                                        }
                                        break;
                                    case ',':
                                    case '%':
                                        {
                                            //DO NOTHING
                                        }
                                        break;
                                    default:
                                        {
                                            IWebElement btnNum = PopupTextBoxCon.FindElement(By.XPath("//div[@id='ni" + num.ToString() + "']"));
                                            btnNum.Click();
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("TextBox control cannot be determined.");
                        }

                        IWebElement PopupOK =
                        DlkEnvironment.AutoDriver.FindElements(By.XPath($"{XPath_STR_PopupTextBoxOK} | {XPath_NUM_PopupTextBoxOK} | {XPath_CAL_PopupTextBoxOK}")).FirstOrDefault(item => item.Displayed);
                        if (PopupOK.Text == "Cancel")
                        {
                            PopupOK = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_NUM_PopupTextBoxOK81)).FirstOrDefault(item => item.Displayed);
                        }
                        DlkBaseControl mPopupOK = new DlkBaseControl("OKButton", PopupOK);

                        if (mPopupOK.Exists())
                            mPopupOK.Click();
                    }

                }
                else
                {
                    var mobileInput = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT)).FirstOrDefault();

                    if (mobileInput != null)
                    {
                        mobileInput = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT)).FirstOrDefault().Displayed ? DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT)).FirstOrDefault() : DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT_NEW)).FirstOrDefault();

                        mobileInput.Clear();
                        mobileInput.SendKeys(TextToEnter);
                        Thread.Sleep(1000);
                        var mobileInputValue = mobileInput.GetAttribute("value");
                        // Retry if it doesn't get right on first try
                        if (!mobileInputValue.Equals(TextToEnter))
                        {
                            mobileInput.Clear();
                            mobileInput.SendKeys(TextToEnter);
                        }
                        //Clicking lookup OK button should be optional, adding exception for lookup
                        if (!mControlName.ToLower().Contains("lookup"))
                        {
                            var mobileOK = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_OK)).FirstOrDefault();
                            Thread.Sleep(500);

                            if (mobileOK.Displayed)
                            {
                                mobileOK.Click();
                            }
                            else
                            {
                                var mobileFind = DlkEnvironment.AutoDriver.FindElements(By.XPath(Xpath_MOBILE_INPUT_FIND)).FirstOrDefault(element => element.Displayed);
                                mobileFind = mobileFind != null ? mobileFind : DlkEnvironment.AutoDriver.FindElements(By.XPath(Xpath_MOBILE_INPUT_EDIT)).FirstOrDefault();
                                Thread.Sleep(500);
                                mobileFind.Click();
                                Thread.Sleep(500);
                                var mobileSelect = DlkEnvironment.AutoDriver.FindElements(By.XPath(Xpath_MOBILE_INPUT_SELECT)).FirstOrDefault(element => element.Displayed);
                                if (mobileSelect != null)
                                {
                                    Thread.Sleep(500);
                                    mobileSelect.Click();
                                }
                            }
                        }
                    }
                    else
                    {
                        //Old set function retain for login and application search
                        mElement.Clear();
                        mElement.Click();
                        mElement.SendKeys(TextToEnter);
                    }
                }

                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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
                    ActValue = mElement.Text != "" ? mElement.Text : GetAttributeValue("value");
                }
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

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                ScrollIntoViewUsingJavaScript();
                mElement.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTextBoxButton", new String[] { "1|text|Value|lookup" })]
        public void ClickTextBoxButton(String ButtonName)
        {
            try
            {
                //Click first on the text box to bring up the textbox button
                Click();
                DlkButton btnTextBoxButton = new DlkButton("", "", "");
                switch (ButtonName.ToLower())
                {
                    case "lookup":
                        btnTextBoxButton = new DlkButton("TextBoxButton", "class_display", "lookupIcon");
                        break;
                    case "clear":
                        btnTextBoxButton = new DlkButton("TextBoxButton", "class_display", "clearText");
                        break;
                    default:
                        throw new Exception("Button not supported");
                }
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

                if (string.IsNullOrEmpty(LookupString)) LookupString = Keys.Backspace;
                
                mElement.Click();
                DlkTEMobileCommon.WaitLoadingFinished(DlkTEMobileCommon.IsCurrentComponentModal());

                var mobileInput = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT)).FirstOrDefault();

                if (mobileInput != null)
                {
                    mobileInput.Clear();
                    mobileInput.SendKeys(LookupString);
                }
            }
            catch (Exception e)
            {
                throw new Exception("ShowAutoComplete() failed : " + e.Message, e);
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

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }
    }
}
