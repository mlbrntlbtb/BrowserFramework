using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using HRSmartLib.PreviousVersion.DlkControls;

namespace HRSmartLib.PreviousVersion.DlkFunctions
{
    [Component("Browser")]
    public class DlkBrowser
    {
        #region Keywords

        [Keyword("GoToUrl", new String[] { "1|text|Url|http://www.google.com" })]
        public static void GoToUrl(String Url)
        {
            try
            {
                DlkEnvironment.AutoDriver.Url = Url;
                DlkEnvironment.AutoDriver.Navigate();
                DlkLogger.LogInfo("GoToURL( ) passed");
            }
            catch (Exception ex)
            {
                throw new Exception("GoToURL() failed : " + ex.Message, ex);
            }
        }

        [Keyword("ScrollToElement")]
        public static void ScrollToElement(string XPath)
        {
            try
            {
                IWebElement element = DlkEnvironment.AutoDriver.FindElement(By.XPath(XPath));
                if (element != null)
                {
                    IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    javascript.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                }

                DlkLogger.LogInfo("ScrollToElement( ) passed");
            }
            catch(Exception ex)
            {
                throw new Exception("ScrollToElement( ) failed" + ex.Message, ex);
            }
        }

        [Keyword("DragAndDrop")]
        public static void DragAndDrop(string FromElement, string ToElement)
        {
            try
            {
                IWebElement mSource = DlkEnvironment.AutoDriver.FindElement(By.XPath(FromElement));
                IWebElement mTarget = DlkEnvironment.AutoDriver.FindElement(By.XPath(ToElement));

                if (mSource != null && 
                    mTarget != null)
                {
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.DragAndDrop(mSource, mTarget).Perform();
                }

                DlkLogger.LogInfo("DragAndDrop( ) passed");
            }
            catch (Exception ex)
            {
                throw new Exception("DragAndDrop( ) failed..." + ex.Message, ex);
            }
        }

        [Keyword("PerformActionWithAlertMessage")]
        public static void PerformActionWithAlertMessage(string AcceptOrDismiss, string Message)
        {
            try
            {
                bool accept = false;
                if (!Boolean.TryParse(AcceptOrDismiss, out accept))
                {

                    if (AcceptOrDismiss.ToLower().Equals("accept"))
                    {
                        accept = true;
                    }
                    else if (AcceptOrDismiss.ToLower().Equals("dismiss"))
                    {
                        accept = false;
                    }
                    else
                    {
                        throw new Exception("Incorrect parameter for AcceptOrDismiss.");
                    }
                }

                if (accept)
                {
                    DlkAlert.ClickOkDialogWithMessage(Message);
                }
                else
                {
                    DlkAlert.ClickCancelDialogWithMessage(Message);
                }

                DlkLogger.LogInfo("PerformActionWithAlertMessage( ) successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("PerformActionWithAlertMessage( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyActionResult")]
        public static void VerifyActionResult(string SuccessOrFail)
        {
            try
            {
                bool expectedResult;
                IWebElement divElement = DlkEnvironment.AutoDriver.FindElement(By.CssSelector("div#successOrFail > div"));
                string actualResult = divElement.GetAttribute("id").ToLower();

                if (!Boolean.TryParse(SuccessOrFail, out expectedResult))
                {

                    if (SuccessOrFail.ToLower().Equals("success"))
                    {
                        expectedResult = true;
                    }
                    else if (SuccessOrFail.ToLower().Equals("fail"))
                    {
                        expectedResult = false;
                    }
                    else
                    {
                        throw new Exception("Incorrect parameter for SuccessOrFail.");
                    }
                }

                bool result = false;
                if (actualResult.Equals("success"))
                {
                    result = true;
                }
                else if (actualResult.Equals("failed"))
                {
                    result = false; 
                }
                else
                {
                    throw new Exception("Missing Dialog.");
                }

                DlkAssert.AssertEqual("Success Or Failed.", expectedResult, result);

            }
            catch (Exception ex)
            {
                throw new Exception("VerifyActionResult( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyActionMessage")]
        public static void VerifyActionMessage(string ExpectedResult)
        {
            try
            {
                IWebElement divElement = DlkEnvironment.AutoDriver.FindElement(By.CssSelector("div#successOrFail > div > div"));
                DlkBaseControl textAreaControl = new DlkBaseControl("Action Message Control", divElement);
                string actualResult = textAreaControl.GetValue().Replace("Error:", string.Empty).Replace("Success:", string.Empty).Replace("Warning:", string.Empty).Replace("×\r\n ",string.Empty).Trim();
                //actualResult = actualResult.Replace("×\r\n ", string.Empty);
                DlkAssert.AssertEqual("Verify Action Message", ExpectedResult, actualResult);
                DlkLogger.LogInfo("VerifyActionMessage( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyActionMessage( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("CloseAlertMessage")]
        public static void CloseAlertMessage()
        {
            try
            {
                IWebElement closeButtonElement = DlkEnvironment.AutoDriver.FindElement(By.CssSelector("div#successOrFail > div.alert > button.close"));
                DlkButton closeButtonControl = new DlkButton("Close_Alert_Message : ", closeButtonElement);
                closeButtonControl.Click();
            }
            catch(Exception ex)
            {
                throw new Exception("CloseAlertMessage() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("FocusBrowserWithTitle", new String[] { "1|text|Title|GovWinIQ -",
                                                         "2|text|Timeout|60" })]
        public static void FocusBrowserWithTitle(String sTitle, String sTimeout)
        {
            DlkEnvironment.SetBrowserFocus(sTitle, Convert.ToInt32(sTimeout));
        }

        [Keyword("FocusBrowserWithUrl", new String[] { "1|text|Url|www.google.com",
                                                       "2|text|Timeout|60"})]
        public static void FocusBrowserWithUrl(String sUrl, String sTimeout)
        {
            DlkEnvironment.SetBrowserFocusByUrl(sUrl, Convert.ToInt32(sTimeout));
        }

        [Keyword("HasPartialTextContent")]
        public static void HasPartialTextContent(string Text)
        {
            try
            {
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResult in expectedResults)
                {
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, null, true);
                    if (elements.Count > 0)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", elements[0]);
                        string actualResult = textControl.GetValue().Trim();
                        DlkAssert.AssertEqual("HasPartialTextContent : ", expectedResult, actualResult, true);
                    }
                    else
                    {
                        throw new Exception("Text : " + expectedResult + " not Found.");
                    }
                }

                DlkLogger.LogInfo("HasPartialTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HasPartialTextContent() execution failed", ex);
            }
        }

        [Keyword("HasTextContent")]
        public static void HasTextContent(string Text)
        {
            try
            {
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResult in expectedResults)
                {
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult);
                    if (elements.Count > 0)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", elements[0]);
                        string actualResult = textControl.GetValue().Trim();
                        DlkAssert.AssertEqual("HasTextContent : ", expectedResult, actualResult, true);
                    }
                    else
                    {
                        throw new Exception("Text : " + expectedResult + " not Found.");
                    }
                }

                DlkLogger.LogInfo("HasTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HasTextContent() execution failed.", ex);
            }
        }

        [Keyword("Close")]
        public static void Close()
        {
            try
            {
                DlkEnvironment.AutoDriver.Close();
                DlkLogger.LogInfo("Close() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Close() execution failed.", ex);
            }
        }
        #endregion
    }
}
