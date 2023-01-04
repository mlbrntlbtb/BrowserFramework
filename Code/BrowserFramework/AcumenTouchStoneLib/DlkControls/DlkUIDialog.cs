using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;


namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("UIDialog")]
    public class DlkUIDialog : DlkBaseControl
    {
        private String mTitleXPath = "./..//span[@class='ui-dialog-title']";
        private String mContentXPath = "..//p[@class='mb-message']";
        private String mCloseButtonXPath = "../preceding-sibling::div[contains(@class, 'ui-dialog-titlebar')]/button[@title='Close']";

        public DlkUIDialog(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkUIDialog(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkUIDialog(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

            FindElement();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (this.mElement.GetAttribute("class").ToLower().Contains("ui-dialog-content"))
            {
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'ui-dialog-content')]"));
                    return true;
                }
                catch
                {

                }
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'ui-dialog')]"));
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
                DlkBaseControl mCorrectControl = new DlkBaseControl("UIDialog", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                IWebElement parentUIDialog = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'ui-dialog-content')]"));
                mCorrectControl = new DlkBaseControl("CorrectControl", parentUIDialog);
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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Boolean bFound = false;
                if (this.Exists(1))
                {
                    if (mElement.GetCssValue("display") != "none")
                    {
                        bFound = true;
                    }
                }

                DlkAssert.AssertEqual("VerifyExists", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Title|Sample form title" })]
        public void VerifyTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement titleElement = mElement.FindElement(By.XPath(mTitleXPath));
                DlkLabel lblTitle = new DlkLabel("Title", titleElement);
                lblTitle.VerifyText(ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMessage", new String[] { "1|text|Expected Message|Sample form message" })]
        public void VerifyMessage(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement messageElement = mElement.FindElement(By.XPath(mContentXPath));
                DlkLabel lblTitle = new DlkLabel("MessageBody", messageElement);
                lblTitle.VerifyText(ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();
                IWebElement buttonElement = mElement.FindElement(By.XPath(mCloseButtonXPath));
                DlkButton btnClose = new DlkButton("Close", buttonElement);
                btnClose.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }
    }
}
