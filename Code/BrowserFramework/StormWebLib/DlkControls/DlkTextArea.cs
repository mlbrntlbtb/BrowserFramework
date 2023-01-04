using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;


namespace StormWebLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {
        private String mEditorFrameClass = "cke_wysiwyg_frame";
        private String mPasteFrameClass = "cke_pasteframe";
        private IWebElement mEditorElement;


        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextArea(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        //public DlkTextArea(String ControlName, String FrameName, String SearchType, String SearchValue)
        //    : base(ControlName, FrameName, SearchType, SearchValue) { }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            IWebElement mFrameElement = this.mElement.FindElement(By.XPath(".//iframe[contains(@class, '" + mEditorFrameClass + "') or contains(@class, '" + mPasteFrameClass + "')]"));
            DlkEnvironment.AutoDriver.SwitchTo().Frame(mFrameElement);
            mEditorElement = DlkEnvironment.AutoDriver.FindElement(By.TagName("body"));


        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (this.mElement.GetAttribute("class").ToLower().Contains("cke_editor"))
            {
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'cke_editor')]"));
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }

            }
        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("TextArea", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                IWebElement parentTextArea = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'cke_editor')]"));
                mCorrectControl = new DlkBaseControl("CorrectControl", parentTextArea);
                mAutoCorrect = true;

                if (mAutoCorrect)
                {
                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClassName = mCorrectControl.GetAttributeValue("class");
                    if (mId != null && mId != "")
                    {
                        SearchType = "ID";
                        SearchValue = mId;
                    }
                    else if (mName != null && mName != "")
                    {
                        SearchType = "NAME";
                        SearchValue = mName;
                    }
                    else if (mClassName != null && mClassName != "")
                    {
                        SearchType = "CLASSNAME";
                        SearchValue = mClassName.Split(' ').First();
                    }
                    else
                    {
                        SearchType = "XPATH";
                        SearchValue = mCorrectControl.FindXPath();
                    }
                }
            }
            catch
            {

            }
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                mEditorElement.Clear();
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].focus();", mEditorElement);
                }
                else
                {
                    mEditorElement.SendKeys(Keys.Shift + Keys.Tab);
                }
                mEditorElement.SendKeys(Value);
                mEditorElement.SendKeys(Keys.Tab);
            DlkLogger.LogInfo("Successfully executed Set() : " + mControlName + ": " + Value);
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
        }

        [Keyword("SetTextOnly", new String[] { "1|text|Value|SampleValue" })]
        public void SetTextOnly(String Value)
        {
            try
            {
                Initialize();
                mEditorElement.SendKeys(Value);
                mEditorElement.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("Successfully executed SetTextOnly() : " + mControlName + ": " + Value);
            }
            catch (Exception e)
            {
                throw new Exception("SetTextOnly() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
        }

        [Keyword("Clear", new String[] { "1|text" })]
        public void Clear()
        {
            try
            {
                Initialize();              
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].focus();", mEditorElement);              
                mEditorElement.Clear();
                mEditorElement.SendKeys(Keys.Shift + Keys.Tab);
                mEditorElement.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("Successfully executed Clear()");
            }
            catch (Exception e)
            {
                throw new Exception("Clear() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
               DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, new DlkBaseControl("EditorElement", mEditorElement).GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool actualValue;
                if (bool.TryParse(mEditorElement.GetAttribute("contenteditable"), out actualValue))
            //    if (bool.TryParse(new DlkBaseControl("EditorElement", mEditorElement).GetAttributeValue("contenteditable"), out actualValue))
                {
                    DlkAssert.AssertEqual("VerifyReadOnly()", bool.Parse(TrueOrFalse), !actualValue);
                    DlkLogger.LogInfo("VerifyReadOnly() passed");
                }
                else
                {
                    throw new Exception(@"Attribute 'isContentEditable' value cannot be determined.");
                }
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
        }

        [Keyword("AppendText", new String[] {"1|text|Cursor Position|Start", 
                                                "2|text|Text to Append|Additional text" })]
        public void AppendText(String StartOrEnd, String Value)
        {
            try
            {
                Initialize();
                string strCurrentNotes = new DlkBaseControl("EditorElement", mEditorElement).GetValue();
                mEditorElement.Clear();
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].focus();", mEditorElement);

                if (StartOrEnd.ToLower() == "start")
                {
                    mEditorElement.SendKeys(Value + strCurrentNotes);
                    mEditorElement.SendKeys(Keys.Shift + Keys.Tab);
                    mEditorElement.SendKeys(Keys.Tab);
                    DlkLogger.LogInfo("Successfully executed AppendText() : " + Value + " added at the start of the text.");
                }
                else if (StartOrEnd.ToLower() == "end")
                {
                    mEditorElement.SendKeys(strCurrentNotes + Value);
                    mEditorElement.SendKeys(Keys.Shift + Keys.Tab);
                    mEditorElement.SendKeys(Keys.Tab);
                    DlkLogger.LogInfo("Successfully executed AppendText() : " + Value + " added at the end of the text.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("AppendText() failed " + e.Message, e);
            }
            finally
            {
              //  mEditorElement.SendKeys(Keys.Shift + Keys.Tab);
                mEditorElement.SendKeys(Keys.Tab);
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                Thread.Sleep(500);
            }
        }
    }
}
