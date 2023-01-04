using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("MessageArea")]
    public class DlkMessageArea : DlkBaseControl
    {
        private String mstrMessagesXPath1 = "//span[contains(@class,'msgTextHdr')]/following-sibling::span[not(contains(@class,'msgTextHdr'))]";
        private String mstrMessagesXPath2 = "[{0}]/a[contains(@id, 'Link')]";
        private String mstrMessagesCSS2 = "a.eLnk";
        private List<String> mlstMessages;
        private String mstrCloseMessageAreaCSS = "*[id=closeM]";
        private String mstrCloseMessageArea2CSS = "*[id=closeM2]";
        private String mstrClickOkButtonCSS = "*[id=wok]";
        private String mstrClickCancelButtonCSS = "*[id=woc]";
        private String mstrClickCloseButtonCSS = "*[id=closeM]";

        public DlkMessageArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMessageArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMessageArea(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }


        private void GetMessages()
        {
            mlstMessages = new List<String>();
            IList<IWebElement> msgElements;
            IList<IWebElement> lnkMsgElements;

            msgElements = mElement.FindElements(By.XPath(mstrMessagesXPath1));

            foreach (IWebElement msgElement in msgElements)
            {
                DlkBaseControl msgControl = new DlkBaseControl("Message", msgElement);
                DlkLogger.LogInfo(msgControl.GetValue().Trim());
                mlstMessages.Add(msgControl.GetValue().Trim());
            }
              
            lnkMsgElements = mElement.FindElements(By.CssSelector(mstrMessagesCSS2));
            
            foreach (IWebElement lnkMsgElement in lnkMsgElements)
            {
                DlkBaseControl msgControl = new DlkBaseControl("Message", lnkMsgElement);
                DlkLogger.LogInfo(msgControl.GetValue().Trim());
                if (!mlstMessages.Contains(msgControl.GetValue().Trim())) // To prevent duplicates in the list
                {
                    mlstMessages.Add(msgControl.GetValue().Trim());
                }
            }
        }

        [Keyword("WaitExists", new String[] { "1|text|Secs To Wait|20" })]
        public void WaitExists(String SecToWait)
        {
            Initialize();
            int i;
            int iSecWait = Convert.ToInt32(SecToWait);
            for (i = 0; i < iSecWait; i++)
            {
                if (Exists())
                {
                    break;
                }
            }

            if (i >= iSecWait)
            {
                throw new Exception("WaitExists() failed : Keyword has timed out.");
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|MessageExists|True or False" })]
        public void VerifyExists(String MessageExists)
        {
            Boolean bExists = Exists(10);

            if (bExists == Convert.ToBoolean(MessageExists))
            {
                DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + MessageExists);
            }
            else
            {
                throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + MessageExists);
            }
        }

        [Keyword("VerifyMessageExists", new String[] { "1|text|Actual Message|Message Text", "2|text|Expected Msg Exists|True or False" })]
        public void VerifyMessageExists(String MessageText, String ExpectedExists)
        {
            Initialize();
            GetMessages();

            Boolean bContains = mlstMessages.Contains(MessageText);

            if (bContains == Convert.ToBoolean(ExpectedExists))
            {
                DlkLogger.LogInfo("VerifyMessageExists() passed : Actual = " + Convert.ToString(bContains) + " : Expected = " + ExpectedExists);
            }
            else
            {
                throw new Exception("VerifyMessageExists() failed : Actual = " + Convert.ToString(bContains) + " : Expected = " + ExpectedExists);
            }       
        }

        [Keyword("VerifyMessageContains", new String[] { "1|text|Part of Message Text|Sample Message Text", "2|text|Expected Part of Msg Exists|True or False" })]
        public void VerifyMessageContains(String PartOfMessageText, String ExpectedExists)
        {
            Initialize();
            GetMessages();

            Boolean bContains = false;

            foreach (string msg in mlstMessages)
            {
                if (msg.Contains(PartOfMessageText))
                {
                    bContains = true;
                    break;
                }
            }

            if (bContains == Convert.ToBoolean(ExpectedExists))
            {
                DlkLogger.LogInfo("VerifyMessageContains() passed : Actual = " + Convert.ToString(bContains) + " : Expected = " + ExpectedExists);
            }
            else
            {
                throw new Exception("VerifyMessageContains() failed : Actual = " + Convert.ToString(bContains) + " : Expected = " + ExpectedExists);
            }
        }

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();
                string sCloseButtonCSSPath = mElement.FindElements(By.CssSelector(mstrCloseMessageAreaCSS)).Where(x => x.Displayed).Any() ? mstrCloseMessageAreaCSS : mstrCloseMessageArea2CSS;
                DlkBaseControl btnControl = new DlkBaseControl("Close Button", mElement.FindElement(By.CssSelector(sCloseButtonCSSPath)));
                btnControl.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickMessagesAreaButton", new String[] { "1|text|ButtonCaption|OK or Cancel" })]
        public void ClickMessagesAreaButton(String ButtonCaption)
        {
            try
            {
                Initialize();
                IWebElement btnControl = null;

                switch (ButtonCaption.ToLower())
                {
                    case "ok":
                        btnControl = mElement.FindElement(By.CssSelector(mstrClickOkButtonCSS), 1);
                        break;
                    case "cancel":
                        btnControl = mElement.FindElement(By.CssSelector(mstrClickCancelButtonCSS), 1);
                        break;
                    case "close":
                        string sCloseButtonCSSPath = mElement.FindElements(By.CssSelector(mstrCloseMessageAreaCSS)).Where(x => x.Displayed).Any() ? mstrCloseMessageAreaCSS : mstrCloseMessageArea2CSS;
                        btnControl = mElement.FindElement(By.CssSelector(sCloseButtonCSSPath), 1);
                        break;
                    default:
                        break;
                }

                if (btnControl == null)
                {
                    throw new Exception("Button control: " + ButtonCaption + " not found");
                }
                else
                {
                    btnControl.Click();
                    DlkLogger.LogInfo("ClickMessagesAreaButton() successfully executed.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickMessagesAreaButton() failed : " + e.Message, e);
            }
        }

        [Keyword("GetMessage", new String[] { "1|text|VariableName|MyVar"})]
        public void GetMessage(string sVariableName)
        {
            Initialize();
            GetMessages();

            String sMessage = "";
            foreach (String sText in mlstMessages)
            {
                sMessage = sMessage + sText + " ";
            }
            sMessage = sMessage.Trim();

            DlkVariable.SetVariable(sVariableName, sMessage);
            DlkLogger.LogInfo("Successfully executed GetMessage().");
        }

        [Keyword("AssignPartialValueToVariable", new String[] { "1|text|VariableName|MyVar" })]
        public override void AssignPartialValueToVariable(string VariableName, string StartIndex, string Length)
        {
            try
            {
                Initialize();
                GetMessages();

                String sMessage = "";
                foreach (String sText in mlstMessages)
                {
                    sMessage = sMessage + sText + " ";
                }
                sMessage = sMessage.Trim();

                if (string.IsNullOrEmpty(sMessage))
                {
                    DlkVariable.SetVariable(VariableName, string.Empty);                    
                }
                else
                {
                    DlkVariable.SetVariable(VariableName, sMessage.Substring(int.Parse(StartIndex) - 1, int.Parse(Length)));
                }
                DlkLogger.LogInfo("Successfully executed AssignPartialValueToVariable().");
            }
            catch (Exception e)
            {
                throw new Exception("AssignPartialValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMessagesCount", new String[] { "1|text|Expected Messages Count|2" })]
        public void VerifyMessagesCount(String ExpectedMessageCount)
        {
            try
            {
                Initialize();
                GetMessages();

                int expMsgCount = Convert.ToInt32(ExpectedMessageCount);
                int totalCount = mlstMessages.Count;

                DlkAssert.AssertEqual("VerifyMessagesCount()", expMsgCount, totalCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMessagesCount() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickMessagesAreaButtonIfExists", new String[] { "1|text|ButtonCaption|OK or Cancel" })]
        public void ClickMessagesAreaButtonIfExists(String ButtonCaption)
        {
            try
            {
                IWebElement btnControl = null;

                if (Exists())
                {
                    switch (ButtonCaption.ToLower())
                    {
                        case "ok":
                            btnControl = mElement.FindElement(By.CssSelector(mstrClickOkButtonCSS), 1);
                            break;
                        case "cancel":
                            btnControl = mElement.FindElement(By.CssSelector(mstrClickCancelButtonCSS), 1);
                            break;
                        case "close":
                            string sCloseButtonCSSPath = mElement.FindElements(By.CssSelector(mstrCloseMessageAreaCSS)).Where(x => x.Displayed).Any() ? mstrCloseMessageAreaCSS : mstrCloseMessageArea2CSS;
                            btnControl = mElement.FindElement(By.CssSelector(sCloseButtonCSSPath), 1);
                            break;
                        default:
                            break;
                    }
                }

                if (btnControl == null)
                {
                    DlkLogger.LogInfo("Button control: " + ButtonCaption + " not found");
                }
                else
                {
                    btnControl.Click();
                    DlkLogger.LogInfo("ClickMessagesAreaButtonIfExists() successfully executed.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickMessagesAreaButtonIfExists() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickMessagesAreaLink", new String[] { "1|text|Index|1" })]
        public void ClickMessagesAreaLink(String Index)
        {
            try
            {
                Initialize();

                String linkMsg = String.Format(mstrMessagesXPath2, Index);
                String link = mstrMessagesXPath1 + linkMsg;

                DlkBaseControl btnControl = new DlkBaseControl("Link Message", mElement.FindElement(By.XPath(link)));
                btnControl.Click();

                DlkLogger.LogInfo("ClickMessagesAreaLink() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickMessagesAreaLink() failed : " + e.Message, e);
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
