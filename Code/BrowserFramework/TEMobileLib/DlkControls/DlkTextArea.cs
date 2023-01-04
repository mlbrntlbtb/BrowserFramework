using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using TEMobileLib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {
        private static String ID_STR_PopupContainer = "expandoDiv";
        private static String ID_NUM_PopupContainer = "numInputDiv";
        private static String ID_CAL_PopupContainer = "calDiv";
        private static String XPath_STR_PopupTextArea = "//input[@id='expandoEditInput'] | //*[@id='expandoEdit']";
        private static String XPath_NUM_PopupTextArea = "//input[@id='numInputMobileInput']";
        private static String XPath_CAL_PopupTextArea = "//input[@id='popupCalMobileInput']";
        private static String XPath_STR_PopupTextAreaOK = "//input[@id='expandoOK']";
        private static String XPath_NUM_PopupTextAreaOK = "//div[@id='niO']";
        private static String XPath_CAL_PopupTextAreaOK = "//*[@id='calOkBtn']";
        private static String XPath_MOBILE_INPUT = "//form//input[contains(@class, 'MobileInput')]";
        private static String XPath_PopupTextArea_Container = $"//*[@id='{ID_STR_PopupContainer}' or @id='{ID_NUM_PopupContainer}' or @id='{ID_CAL_PopupContainer}']";
        private static String XPath_PopupTextArea_TAField = $"{XPath_STR_PopupTextArea} | {XPath_NUM_PopupTextArea} | {XPath_CAL_PopupTextArea}";
        private static String XPath_MOBILE_OK = "//form//*[@id='mbOk']";
        private static String XPath_MOBILE_INPUT_NEW = "//form//div[contains(@class,'lkp') and contains(@style,'block')]//input[contains(@class, 'MobileInput')]";
        private static String Xpath_MOBILE_INPUT_FIND = "//form//div[contains(@class,'lkpSearch')]//div[contains(@class, 'rsTbBtn')]";
        private static String Xpath_MOBILE_INPUT_EDIT = "//form//div[contains(@class,'lkpEdit')]//div[contains(@class, 'rsTbBtn')]";
        private static String Xpath_MOBILE_INPUT_SELECT = "//div[contains(@id,'LKP') or contains(@id,'LOOKUP') or contains(@id,'FILE_TYPES')]/ancestor::form[1]/following::*[@class='bOk' and @value='Select']";

        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                DlkTEMobileCommon.WaitForElementToLoad(new DlkBaseControl("TextArea", mElement));

                ScrollIntoViewUsingJavaScript();
                mElement.Click();
                DlkTEMobileCommon.WaitLoadingFinished(DlkTEMobileCommon.IsCurrentComponentModal());

                var PopupTextAreaCon = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_PopupTextArea_Container)).FirstOrDefault(item => item.Displayed);

                //Hotfix to re-click and re-find the popup textbox. 
                //Some popup textboxes read as inactive on safari. Focus methods do not work atm, revisit after appium updates.
                if (DlkEnvironment.mBrowser.ToLower() == "safari" && !mElement.Equals(DlkEnvironment.AutoDriver.SwitchTo().ActiveElement()))
                {
                    //re-click for popup textboxes only
                    if (PopupTextAreaCon == null && !DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_INPUT)).Any(e => e.Displayed))
                    {
                        mElement.Click();
                        PopupTextAreaCon = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_PopupTextArea_Container)).FirstOrDefault(item => item.Displayed);
                    }
                }

                if (PopupTextAreaCon != null)
                {
                    Thread.Sleep(500);//pause to emsure that the popup textbox is enabled before interacting

                    IWebElement PopupTextArea = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_PopupTextArea_TAField)).FirstOrDefault(item => item.Displayed);

                    if (PopupTextArea != null)
                    {
                        var textAreaType = PopupTextArea.GetAttribute("id").ToLower();

                        if (textAreaType.Contains("expando") || textAreaType.Contains("cal"))
                        {
                            PopupTextArea.Clear();
                            PopupTextArea.SendKeys(TextToEnter);
                        }
                        else if (textAreaType.Contains("num"))
                        {
                            char[] stringToNumPattern = TextToEnter.ToCharArray();
                            foreach (char num in stringToNumPattern)
                            {
                                switch (num)
                                {
                                    case '-':
                                        {
                                            IWebElement btnNum = PopupTextAreaCon.FindElement(By.XPath("//div[@id='niM']"));
                                            btnNum.Click();
                                        }
                                        break;
                                    case '.':
                                        {
                                            IWebElement btnNum = PopupTextAreaCon.FindElement(By.XPath("//div[@id='niD']"));
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
                                            IWebElement btnNum = PopupTextAreaCon.FindElement(By.XPath("//div[@id='ni" + num.ToString() + "']"));
                                            btnNum.Click();
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("TextArea control cannot be determined.");
                        }

                        IWebElement PopupOK =
                        DlkEnvironment.AutoDriver.FindElements(By.XPath($"{XPath_STR_PopupTextAreaOK} | {XPath_NUM_PopupTextAreaOK} | {XPath_CAL_PopupTextAreaOK}")).FirstOrDefault(item => item.Displayed);

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
