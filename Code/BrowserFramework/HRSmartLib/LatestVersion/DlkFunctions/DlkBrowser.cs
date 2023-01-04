using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using HRSmartLib.LatestVersion.DlkControls;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace HRSmartLib.LatestVersion.DlkFunctions
{
    [Component("Browser")]
    public class DlkBrowser
    {
        #region Keywords

        [Keyword("Back")]
        public static void Back()
        {
            DlkEnvironment.AutoDriver.Navigate().Back();
        }

        [Keyword("Refresh")]
        public static void Refresh()
        {
            DlkEnvironment.AutoDriver.Navigate().Refresh();
        }


        [Keyword("Tab")]
        public static void Tab()
        {
            IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='dtm-main-content ']//input[@type='text']"));
            DlkBaseControl currentControl = new DlkBaseControl("Current control", header);

            if (currentControl.IsReadOnly() == "true")
            {
                IWebElement focusedElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                focusedElement.SendKeys(Keys.Tab);
            }
            else
            {
                header.SendKeys(Keys.Tab);
            }
        }

        [Keyword("GoToUrl", new String[] { "1|text|Url|http://www.google.com" })]
        public static void GoToUrl(String Url)
        {
            try
            {
                string fullURL = Url.Contains(".com") ? Url : string.Concat(DlkEnvironment.AutoDriver.Url, Url); 
                DlkEnvironment.AutoDriver.Url = fullURL;
                DlkEnvironment.AutoDriver.Navigate();
                DlkLogger.LogInfo("GoToURL( ) passed");
            }
            catch (Exception ex)
            {
                throw new Exception("GoToURL() failed : " + ex.Message, ex);
            }
        }

        [Keyword("ReplaceURL")]
        public static void ReplaceURL(String OldText, string NewText)
        {
            try
            {
                string fullURL = DlkEnvironment.AutoDriver.Url.Replace(OldText, NewText);
                DlkEnvironment.AutoDriver.Url = fullURL;
                DlkEnvironment.AutoDriver.Navigate();
                DlkLogger.LogInfo("ReplaceURL( ) passed");
            }
            catch (Exception ex)
            {
                throw new Exception("ReplaceURL() failed : " + ex.Message, ex);
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
                IWebElement from = DlkEnvironment.AutoDriver.FindElement(By.XPath(FromElement));
                IWebElement to = DlkEnvironment.AutoDriver.FindElement(By.XPath(ToElement));
                HRSmartLib.DlkCommon.DlkCommonFunction.ScrollIntoView(to);

                if (from != null && 
                    to != null)
                {
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.ClickAndHold(from).Build().Perform();
                    mAction.MoveToElement(to).Build().Perform();
                    mAction.Release().Build().Perform();
                    Thread.Sleep(5000);
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

        [Keyword("PerformActionWithPopUpMessage")]
        public static void PerformActionWithPopUpMessage(string AcceptOrDismiss, string Message)
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

                IList<IWebElement> popupMsg = DlkEnvironment.AutoDriver.FindElements(By.Id("popup_content"));

                if (popupMsg.Count > 0)
                {
                    IWebElement clickableElement;
                    if (accept)
                    {
                        clickableElement = DlkEnvironment.AutoDriver.FindElement(By.Id("popup_ok"));
                    }
                    else
                    {
                        clickableElement = DlkEnvironment.AutoDriver.FindElement(By.Id("popup_cancel"));
                    }

                    clickableElement.Click();
                }
                else
                {
                    throw new Exception("Incorrect pop up message.");
                }

                DlkLogger.LogInfo("PerformActionWithPopUpMessage( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformActionWithPopUpMessage( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyActionResult")]
        public static void VerifyActionResult(string SuccessOrFail)
        {
            try
            {
                verifyActionResult(SuccessOrFail);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(DlkCommon.DlkCommonFunction.MISSING_DIALOG_ERR))
                {
                    DlkLogger.LogError(ex);
                }
                else
                {
                    throw new Exception("VerifyActionResult( ) execution failed. " + ex.Message, ex);
                }
            }
        }

        [Keyword("VerifyActionResultModal")]
        public static void VerifyActionResultModal(string SuccessOrFail)
        {
            try
            {

                IList<IWebElement> frames = DlkEnvironment.AutoDriver.FindElements(By.XPath("//iframe")).Where(x => x.Displayed).ToList();
                Thread.Sleep(1000);
                DlkEnvironment.AutoDriver.SwitchTo().Frame(frames[0]);
                verifyActionResult(SuccessOrFail);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(DlkCommon.DlkCommonFunction.MISSING_DIALOG_ERR))
                {
                    DlkLogger.LogError(ex);
                }
                else
                {
                    throw new Exception("VerifyActionResultModal( ) execution failed. " + ex.Message, ex);
                }
            }
        }

        private static void verifyActionResult(string successOrFail)
        {
            bool expectedResult;
            int incrementLimit = 20;
            IList<IWebElement> divElement = DlkEnvironment.AutoDriver.FindElements(By.CssSelector("div#successOrFail > div > div.alert, div#dlz_alert > div"));

            while (divElement.Count <= 0 &&
                   incrementLimit != 0)
            {
                Thread.Sleep(100);
                incrementLimit--;
                DlkLogger.LogInfo("Retrying...");
                divElement = DlkEnvironment.AutoDriver.FindElements(By.CssSelector("div#successOrFail > div"));
            }

            string actualResult = string.Empty;
            if (divElement.Count > 0)
            {
                actualResult = divElement[0].GetAttribute("class").ToLower();
            }

            if (!Boolean.TryParse(successOrFail, out expectedResult))
            {

                if (successOrFail.ToLower().Equals("success"))
                {
                    expectedResult = true;
                }
                else if (successOrFail.ToLower().Equals("fail") || successOrFail.ToLower().Equals("failedContainer"))
                {
                    expectedResult = false;
                }
                else
                {
                    throw new Exception("Incorrect parameter for SuccessOrFail.");
                }
            }

            bool result = false;
            if (actualResult.Equals("success") || actualResult.Contains("success"))
            {
                result = true;
            }
            else if (actualResult.Equals("failed") || actualResult.Contains("failed") || actualResult.Contains("danger"))
            {
                result = false;
            }
            else
            {
                throw new Exception(DlkCommon.DlkCommonFunction.MISSING_DIALOG_ERR);
            }

            DlkAssert.AssertEqual("Success Or Failed.", expectedResult, result);
        }

        [Keyword("VerifyActionMessage")]
        public static void VerifyActionMessage(string ExpectedResult)
        {
            try
            {
                IList<IWebElement> divElement = DlkEnvironment.AutoDriver.FindElements(By.CssSelector("div#successOrFail > div > div, div#dlz_alert > div"));
                if (divElement.Count == 0)
                {
                    throw new Exception(DlkCommon.DlkCommonFunction.MISSING_DIALOG_ERR);
                }
                else
                {
                    DlkBaseControl textAreaControl = new DlkBaseControl("Action Message Control", divElement[0]);
                    string actualResult = textAreaControl.GetValue().Replace("Error:", string.Empty).Replace("Success:", string.Empty).Replace("Warning:", string.Empty).Replace("×\r\n ", string.Empty).Replace("\r\n×", string.Empty).Replace("\r\n", "\n").Trim();
                    //actualResult = actualResult.Replace("×\r\n ", string.Empty);
                    DlkAssert.AssertEqual("Verify Action Message", ExpectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyActionMessage( ) successfully executed.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(DlkCommon.DlkCommonFunction.MISSING_DIALOG_ERR))
                {
                    DlkLogger.LogError(ex);
                }
                else
                {
                    throw new Exception("VerifyActionMessage( ) execution failed. " + ex.Message, ex);
                }
            }
        }

        [Keyword("CloseAlertMessage")]
        public static void CloseAlertMessage()
        {
            try
            {
                bool insideIframe = false;
                bool alertExists = false;
                IWebElement closeButtonElement = null;
                IList<IWebElement> elements = DlkEnvironment.AutoDriver.FindElements(By.XPath(@"//div[@id='successOrFail']//button[@class='close']"));
                if (elements.Count > 0)
                {
                    closeButtonElement = elements[0];
                    alertExists = true;
                }
                else
                {
                    IList<IWebElement> frames;
                    frames = DlkEnvironment.AutoDriver.FindElements(By.XPath("//iframe"));
                    if (frames.Count > 0)
                    {
                        DlkEnvironment.AutoDriver.SwitchTo().Frame(frames[0]);
                        insideIframe = true;
                        elements = DlkEnvironment.AutoDriver.FindElements(By.XPath(@"//div[@id='successOrFail']//button[@class='close']"));
                        if (elements.Count > 0)
                        {
                            closeButtonElement = elements[0];
                            alertExists = true;
                        }
                    }
                }
                //DlkButton closeButtonControl = new DlkButton("Close_Alert_Message : ", closeButtonElement);
                //closeButtonControl.Click();
                if (alertExists)
                {
                    closeButtonElement.Click();
                }
                else
                {
                    DlkLogger.LogInfo("Alert message missing.");
                }
                if (insideIframe)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("CloseAlertMessage() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyURL")]
        public static void VerifyURL(String URL)
        {
            string actualResult = DlkEnvironment.AutoDriver.Url;
            if (!URL.Contains(".com"))
            {
                actualResult = actualResult.Substring(actualResult.IndexOf(".com") + 4);
            }
            DlkAssert.AssertEqual("Window URL Check", URL, actualResult);
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

        [Keyword("FocusBrowserToMainWindow")]
        public static void FocusBrowserToMainWindow()
        {
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[0]);
        }

        [Keyword("HasPartialTextContent")]
        public static void HasPartialTextContent(string Text)
        {
            try
            {
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResult in expectedResults)
                {
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, null, true, ignoreCasing:true,returnDisplayedElements:true);
                    if (elements.Count > 0)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", elements[0]);
                        string textClass = textControl.mElement.GetAttribute("class");
                        string actualResult = textControl.GetValue().Trim();
                        DlkAssert.AssertEqual("HasPartialTextContent : ", expectedResult.ToUpper(), actualResult.ToUpper(), true);
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

        [Keyword("VerifyPartialTextContent")]
        public static void VerifyPartialTextContent(string Text, string TrueOrFalse)
        {
            try
            {
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResultText in expectedResults)
                { 
                    bool actualResult;
                    bool expectedResult = Convert.ToBoolean(TrueOrFalse);

                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResultText, null, true, ignoreCasing:true, returnDisplayedElements:true);
                    if (elements.Count > 0)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", elements[0]);
                        actualResult = true;
                    }
                    else
                    {
                        actualResult = false;
                    }

                    DlkAssert.AssertEqual("VerifyPartialTextContent", expectedResult, actualResult);
                }
                
                DlkLogger.LogInfo("VerifyPartialTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyPartialTextContent() execution failed", ex);
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
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, ignoreCasing:true, returnDisplayedElements:true);
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

        [Keyword("StopMasquerading")]
        public static void StopMasquerading()
        {
            try
            {
                IWebElement masqueradeButton = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='masqueradeParent']/a[1]"));
                DlkBaseControl masqueradeControl = new DlkBaseControl("Masquerade_Button", masqueradeButton);
                masqueradeControl.Click();
                DlkLogger.LogInfo("StopMasquerading() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("StopMasquerading() execution failed. : " + ex.Message);
            }
        }

        [Keyword("VerifyPageTitleBar")]
        public static void VerifyPageTitleBar(string TitleBar)
        {
            try
            {
                string actualResult = DlkEnvironment.AutoDriver.Title;
                DlkAssert.AssertEqual("Title Bar", TitleBar, actualResult);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyPageTitleBar() execution failed. : " + ex.Message);
            }
        }

        [Keyword("Hint", new String[] { @"1|text|Hint|This is a hint" })]
        public static void Hint(String HintToLog)
        {
            try
            {
                DlkLogger.LogInfo("Hint: " + HintToLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Hint() failed: " + ex.Message);
            }
        }

        [Keyword("Note", new String[] { @"1|text|Note|This is a note" })]
        public static void Note(String NoteToLog)
        {
            try
            {
                DlkLogger.LogInfo("Note: " + NoteToLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Note() failed: " + ex.Message);
            }
        }

        [Keyword("ExpectedResult", new String[] { @"1|text|Expected Result|This is an expected result." })]
        public static void ExpectedResult(String ExpectedResultToLog)
        {
            try
            {
                DlkLogger.LogInfo("ExpectedResult: " + ExpectedResultToLog);
            }
            catch (Exception ex)
            {
                throw new Exception("ExpectedResult() failed: " + ex.Message);
            }
        }

        [Keyword("Step", new String[] { @"1|text|Step|This is a step." })]
        public static void Step(String StepToLog)
        {
            try
            {
                DlkLogger.LogInfo("Step: " + StepToLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Step() failed: " + ex.Message);
            }
        }

        [Keyword("Warning", new String[] { @"1|text|Warning|This is a Warning." })]
        public static void Warning(String WarningToLog)
        {
            try
            {
                DlkLogger.LogInfo("Warning: " + WarningToLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Warning() failed: " + ex.Message);
            }
        }

        [Keyword("VerifyCaptionToolTip")]
        public static void VerifyCaptionToolTip(string TextCaption, string ToolTipContent)
        {
            try
            {
                string[] expectedResults = ToolTipContent.Split('~');
                IWebElement captionElement = DlkCommon.DlkCommonFunction.GetElementWithText(TextCaption)[0];
                IList<IWebElement> tooltipElement = captionElement.FindElements(By.XPath(@"./parent::*[@class='popover-toggle' and @data-content]"));
                if (tooltipElement.Count == 0)
                {
                    throw new Exception("Could not find tooltip element.");
                }
                string actualResult = DlkCommon.DlkCommonFunction.StripHTMLTags(tooltipElement[0].GetAttribute("data-content"), true);

                foreach (string expectedResult in expectedResults)
                {
                    DlkAssert.AssertEqual("VerifyCaptionToolTip : ", expectedResult, actualResult, true);
                }

                DlkLogger.LogInfo("VerifyCaptionToolTip() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCaptionToolTip() execution failed.", ex);
            }
        }



        [Keyword("VerifyPageOptionCaptionExists")]
        public static void VerifyPageOptionCaptionExists(string OptionCaption, string TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                string expectedCaption = OptionCaption;
                IList<IWebElement> pageOptions = DlkEnvironment.AutoDriver.FindElements(By.XPath(@"//div[contains(@class,'page-options')]/a"));
                foreach (IWebElement option in pageOptions)
                {
                    string actualCaption = option.GetAttribute("data-original-title") ?? string.Empty;
                    if (actualCaption == expectedCaption)
                    {
                        actualResult = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyPageOptionCaptionExists", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyPageOptionCaptionExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyPageOptionCaptionExists() execution failed.", ex);
            }
        }

        [Keyword("ExecuteKeyword")]
        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String Parameters)
        {
            try
            {
                string[] parameters = Parameters.Split(new[] { "|" }, StringSplitOptions.None); 
                HRSmartLib.LatestVersion.System.DlkHRSmartKeywordHandler handler = new System.DlkHRSmartKeywordHandler();
                handler.ExecuteKeyword(Screen, ControlName, Keyword, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifyScriptContent")]
        public static void VerifyScriptContent(string Content, string TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                IList<IWebElement> scriptContentList = DlkCommon.DlkCommonFunction.GetElementWithText(Content, elementTag: "script", partialMatch: true);
                if (scriptContentList.Count > 0)
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("Script Content : ", expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifyHighlightedTextInPage")]
        public static void VerifyHighlightedTextInPage(string Text, string TrueOrFalse)
        {
            try
            {
                if (!Boolean.TryParse(TrueOrFalse, out bool expectedResult))
                {
                    throw new Exception("TrueOrFalse must be a Boolean value.");
                }

                bool actualResult = false;
                List<IWebElement> highlightedElements = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@style='background-color: #FFCC00']")).ToList();

                if (highlightedElements.Any(x => x.Text == Text))
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("Highlighted", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyHighlightedTextInPage() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyHighlightedTextInPage() execution failed.", ex);
            }
        }

        [Keyword("Delay")]
        public static void Delay(String Seconds)
        {
            try
            {
                decimal seconds = Convert.ToDecimal(Seconds);
                int miliseconds = Convert.ToInt32(seconds * 1000);
                Thread.Sleep(miliseconds);
                DlkLogger.LogInfo("Sleeping for : " + Seconds + "Second/s");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifyURLAccessibility")]
        public static void VerifyURLAccessibility(string URLFile, string Accessibility)
        {
            StringBuilder results = new StringBuilder();
            string actual = string.Empty;
           

            try
            {
                StringBuilder permissionErrorURLs = new StringBuilder();
                string fixedURL = DlkEnvironment.AutoDriver.Url.Substring(0, DlkEnvironment.AutoDriver.Url.IndexOf(".com") + 4);
                string inputFilePath = string.Concat(DlkEnvironment.mDirData, URLFile);
                List<string> urls = getURLs(inputFilePath);
                Dictionary<string, Int16> ErrorList = new Dictionary<string, Int16>();
                ErrorList.Add(DlkCommon.DlkCommonFunction.ACCESS_ERROR, 0);
                ErrorList.Add(DlkCommon.DlkCommonFunction.PERMISSION_ERROR, 0);
                ErrorList.Add(DlkCommon.DlkCommonFunction.OTHER_ERROR, 0);
                ErrorList.Add(DlkCommon.DlkCommonFunction.PROCESS_ERROR, 0);
                ErrorList.Add(DlkCommon.DlkCommonFunction.ACCESSIBLE, 0);

                foreach (string url in urls)
                {
                    string newLine = string.Empty;
                    string fullURL = string.Concat(fixedURL, url);
                    DlkEnvironment.AutoDriver.Url = fullURL;
                    DlkEnvironment.AutoDriver.Navigate();
                                      

                    IList<IWebElement> error = DlkCommon.DlkCommonFunction.GetElementWithText("We're sorry, but the application could not complete your request at this time", partialMatch: true);
                    if (error != null && error.Count > 0)
                    {
                        DlkLogger.LogInfo("Adding to process request error file : " + url);
                        newLine = string.Format("{0},{1}", url, "Process Request Error");
                        results.AppendLine(newLine);
                        ErrorList[DlkCommon.DlkCommonFunction.PROCESS_ERROR] += 1;
                       
                        continue;
                    }

                    error = DlkCommon.DlkCommonFunction.GetElementWithText("An error has occurred while trying to access this page.", partialMatch: true);
                    if (error != null && error.Count > 0)
                    {
                        DlkLogger.LogInfo("Adding to access error file : " + url);
                        newLine = string.Format("{0},{1}", url, "Access Request Error");
                        results.AppendLine(newLine);
                        ErrorList[DlkCommon.DlkCommonFunction.ACCESS_ERROR] += 1;
                        continue;
                    }

                    error = DlkCommon.DlkCommonFunction.GetElementWithText("You do not have permission to view this page.", partialMatch: true);
                    if (error != null && error.Count > 0)
                    {
                        DlkLogger.LogInfo("Adding to permission error file : " + url);
                        newLine = string.Format("{0},{1}", url, "Permission Request Error");
                        results.AppendLine(newLine);
                        ErrorList[DlkCommon.DlkCommonFunction.PERMISSION_ERROR] += 1;
                        continue;
                    }

                    error = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='failedContainer']//*[contains(normalize-space(),'Error:')]"));
                    if (error != null && error.Count > 0)
                    {
                        DlkLogger.LogInfo("Adding to other error file : " + url);
                        newLine = string.Format("{0},{1}", url, "Other Error");
                        ErrorList[DlkCommon.DlkCommonFunction.OTHER_ERROR] += 1;
                        results.AppendLine(newLine);
                       
                        continue;
                    }
                    else
                    {
                        IList<IWebElement> dtmMainForm = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='app_main_id']"));
                        if (dtmMainForm.Count > 0)
                        {
                            DlkLogger.LogInfo("Adding to accessible URL file : " + url);
                            newLine = string.Format("{0},{1}", url, "URL is accessible");
                            ErrorList[DlkCommon.DlkCommonFunction.ACCESSIBLE] += 1;
                            results.AppendLine(newLine);
                           
                        }
                        //else
                        //{
                        //    DlkLogger.LogInfo("Adding to accessible URL but not in DTM main form file : " + url);
                        //    newLine = string.Format("{0},{1}", url, "URL is accessible but not in DTM main Form");
                        //    results.AppendLine(newLine);
                        //}
                    }

                }

                if(ErrorList[Accessibility] == urls.Count)
                {
                    actual = Accessibility;
                } else
                {
                    actual = "Please refer to result file.";
                }

                string fileName = string.Concat(Path.GetFileNameWithoutExtension(URLFile),"Results.csv");
                string outputFilePath = string.Concat(DlkEnvironment.mDirData, fileName);
                File.WriteAllText(outputFilePath, results.ToString());
                DlkAssert.AssertEqual("VerifyURLAccessibility", Accessibility, actual);

            }
            catch (Exception ex)
            {
                string fileName = string.Concat(Path.GetFileNameWithoutExtension(URLFile), "Results.csv");
                string filePath = string.Concat(DlkEnvironment.mDirData, fileName);
                File.WriteAllText(filePath, results.ToString());
                throw ex;
            }
        
        }


        private static List<string> getURLs(string Path)
        {
            List<string> urlList = new List<string>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(Path);
            Excel._Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range = worksheet.UsedRange;

            DlkLogger.LogInfo("Getting URLs from : " + Path);
            int rowCount = range.Rows.Count;
            //int colCount = range.Columns.Count;

            for (int i = 1; i <= rowCount; i++)
            {
                //for (int j = 2; j <= colCount; j++)
                //{
                //    //new line
                //    if (j == 1)
                //        Console.Write("\r\n");

                //write the value to the console
                if (range.Cells[i, 1] != null && range.Cells[i, 1].Value2 != null)
                {
                    urlList.Add(range.Cells[i, 1].Value2.ToString());
                    //Console.Write(range.Cells[i, 2].Value2.ToString() + "\t");
                }
                //}
            }

            DlkLogger.LogInfo("Done getting URLs.");
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(worksheet);

            //close and release
            workbook.Close();
            Marshal.ReleaseComObject(workbook);

            //quit and release
            app.Quit();
            Marshal.ReleaseComObject(app);

            return urlList;
        }

        [Keyword("CheckJavascriptError")]
        public static void CheckJavascriptError()
        {
            try
            {
                var errorStrings = new List<string>
                {
                    "SyntaxError",
                    "EvalError",
                    "ReferenceError",
                    "RangeError",
                    "TypeError",
                    "URIError"
                };
                var logs = DlkEnvironment.AutoDriver.Manage().Logs.GetLog(LogType.Browser);

                var jsErrors = DlkEnvironment.AutoDriver.Manage().Logs.GetLog(LogType.Browser).Where(x => errorStrings.Any(e => x.Message.Contains(e)));

                if (jsErrors.Any())
                {
                    throw new Exception("JavaScript error(s):" + Environment.NewLine + jsErrors.Aggregate("", (s, entry) => s + entry.Message + Environment.NewLine));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifyJavascriptError")]
        public static void VerifyJavascriptError()
        {
            if (!isAlertPresent())
            {
                if (!OnExternalSite())
                {
                    Thread.Sleep(300);
                    bool hasError = false;
                    string jSErrorIconXpath = "//img[contains(@src,'chrome-extension')]";
                    List<IWebElement> errorIcon = DlkEnvironment.AutoDriver.FindElements(By.XPath(jSErrorIconXpath)).ToList();

                    try
                    {
                        if (errorIcon.Count > 0)
                        {
                            hasError = true;
                        }

                        DlkAssert.AssertEqual("VerifyJavascriptError()", false, hasError);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (hasError)
                        {
                            DismissJavascriptError(errorIcon[0]);
                        }
                    }
                }
            }
            else
            {
                DlkLogger.LogInfo("Skipping VerifyJavascriptError(), alert message is found.");
                return;
            }
        }

        private static void DismissJavascriptError(IWebElement errorIcon)
        {
            bool switchedFrame = false;
            try
            {
                string closeJSErrorIconXpath = "//a[@id='clear']";
                string jsErrorIFrameXpath = "//iframe[contains(@src,'chrome-extension')]";
                DlkButton errorButton = new DlkButton("Error button", errorIcon);

                errorButton.Click();
                // get screenshot of error
                Thread.Sleep(1000);
                DlkLogger.LogScreenCapture("ExceptionImg");

                DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath(jsErrorIFrameXpath)));
                switchedFrame = true;
                List<IWebElement> closeIcon = DlkEnvironment.AutoDriver.FindElements(By.XPath(closeJSErrorIconXpath)).ToList();

                if (closeIcon.Count > 0)
                {
                    closeIcon[0].Click();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (switchedFrame)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                }
            }
        }

        [Keyword("GenerateTestCase")]
        public static void GenerateTestCase(string TestFile, string OutputDir)
        {
            try
            {
                List<string> csvLines = LinesToWrite(GetDataFromTest(TestFile));
                string csvFile = Path.GetFileNameWithoutExtension(TestFile) + ".csv";
                var csv = new StringBuilder();

                foreach (string line in csvLines)
                {
                    csv.AppendLine(line);
                }

                File.WriteAllText(Path.Combine(OutputDir, csvFile), csv.ToString());

                DlkLogger.LogInfo("GenerateTestCase() successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static Stack<XElement> GetDataFromTest(String TestFilePath)
        {
            Stack<XElement> stackData = new Stack<XElement>();
            XDocument mXml = XDocument.Load(TestFilePath);

            List<XElement> steps = mXml.Descendants("step").ToList();
            List<XElement> stepsToInclude = new List<XElement>();

            // filter steps to include
            foreach (var step in steps)
            {
                if (step.Element("screen").Value == "Browser" &&
                    (step.Element("keyword").Value == "Step" || step.Element("keyword").Value == "ExpectedResult" ||
                     step.Element("keyword").Value == "Note" || step.Element("keyword").Value == "Hint"))
                {
                    stepsToInclude.Add(step);
                }
            }

            for (int i = stepsToInclude.Count - 1; i >= 0; i--)
            {
                stackData.Push(stepsToInclude[i]);
            }

            return stackData;
        }

        private static List<string> LinesToWrite(Stack<XElement> xStepElements)
        {
            List<string> stringsToWrite = new List<string>();

            // set the columns
            var columnHeaders = "ID,Work Item Type,Title,Test Step,Step Action,Step Expected,Revision,Area Path,Assigned To,State";
            stringsToWrite.Add(columnHeaders);

            try
            {
                var stepBuilder = new StringBuilder();
                int stepCount = 0;
                string prevStepHolder = "";
                string currentStepHolder = "";
                int indexOfCurrentStep = 0;
                bool expectedResultHasCorrespondingStep = false;
                string StepValue = "";

                while (xStepElements.Count != 0)
                {
                    List<XElement> checker = xStepElements.ToList();
                    if (xStepElements.Peek().Element("keyword").Value == "Step")
                    {
                        StepValue = xStepElements.Peek().Element("parameters").Element("parameter").Value;
                        StepValue = StepValue.Contains(",") ? "\"" + StepValue + "\"" : StepValue;
                        StepValue = StepValue.Replace("\n", " ");
                        currentStepHolder = StepValue;

                        if (stepCount == 0)
                        {
                            stepCount++;

                            //remove first the current step before checking
                            checker.RemoveAt(0);

                            // check if Step or ExpectedResults appear first
                            foreach (var element in checker)
                            {
                                if (element.Element("keyword").Value == "Step")
                                {
                                    stepBuilder.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                        "", "", "", stepCount, StepValue, "", "", "", "", ""));

                                    stringsToWrite.Add(stepBuilder.ToString());
                                    stepBuilder.Clear();
                                    prevStepHolder = StepValue;
                                    break;
                                }
                                if (element.Element("keyword").Value == "ExpectedResult")
                                {
                                    stringsToWrite.Add("");
                                    stepBuilder.Append(string.Format("{0},{1},{2},{3},{4}",
                                         "", "", "", stepCount, StepValue));

                                    prevStepHolder = StepValue;
                                    expectedResultHasCorrespondingStep = true;
                                    break;
                                }
                            }
                        }

                        if (prevStepHolder != currentStepHolder)
                        {
                            stepCount++;
                            stepBuilder.Clear();

                            // check if there is a preceding expected result to be attached
                            if (xStepElements.Where(x => x.Element("keyword").Value == "ExpectedResult").Any())
                            {
                                //remove first the current step before checking
                                checker.RemoveAt(0);

                                // check if Step or ExpectedResults appear first
                                foreach (var element in checker)
                                {
                                    if (element.Element("keyword").Value == "Step")
                                    {
                                        stepBuilder.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                             "", "", "", stepCount, StepValue, "", "", "", "", ""));

                                        stringsToWrite.Add(stepBuilder.ToString());
                                        stepBuilder.Clear();
                                        prevStepHolder = StepValue;
                                        break;
                                    }
                                    if (element.Element("keyword").Value == "ExpectedResult")
                                    {
                                        stringsToWrite.Add("");
                                        stepBuilder.Append(string.Format("{0},{1},{2},{3},{4}",
                                             "", "", "", stepCount, StepValue));

                                        expectedResultHasCorrespondingStep = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                stepBuilder.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                     "", "", "", stepCount, StepValue, "", "", "", "", ""));
                                stringsToWrite.Add(stepBuilder.ToString());
                            }
                        }

                        prevStepHolder = currentStepHolder;
                        indexOfCurrentStep = stringsToWrite.Count - 1;
                        xStepElements.Pop();
                    }

                    if (xStepElements.Count != 0)
                    {
                        if (xStepElements.Peek().Element("keyword").Value == "ExpectedResult")
                        {
                            string ExpectedResultValue = xStepElements.Peek().Element("parameters").Element("parameter").Value;
                            ExpectedResultValue = ExpectedResultValue.Contains(",") ? "\"" + ExpectedResultValue + "\"" : ExpectedResultValue;
                            ExpectedResultValue = ExpectedResultValue.Replace("\n", " ");

                            if (expectedResultHasCorrespondingStep)
                            {
                                stepBuilder.Append(string.Format(",{0},{1},{2},{3},{4}",
                                     ExpectedResultValue, "", "", "", ""));
                                stringsToWrite[indexOfCurrentStep] = stepBuilder.ToString();
                            }
                            else
                            {
                                string newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                "", "", "", "", "", ExpectedResultValue, "", "", "", "");
                                stringsToWrite.Add(newLine);
                            }

                            expectedResultHasCorrespondingStep = false;
                            xStepElements.Pop();
                        }
                    }

                    if (xStepElements.Count != 0)
                    {
                        if (xStepElements.Peek().Element("keyword").Value == "Note")
                        {
                            string NoteValue = xStepElements.Peek().Element("parameters").Element("parameter").Value;
                            NoteValue = NoteValue.Contains(",") ? "\"" + NoteValue + "\"" : NoteValue;
                            NoteValue = NoteValue.Replace("\n", " ");

                            string newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                 "", "", "", "", "Note", NoteValue, "", "", "", "");
                            stringsToWrite.Add(newLine);

                            xStepElements.Pop();
                        }
                    }

                    if (xStepElements.Count != 0)
                    {
                        if (xStepElements.Peek().Element("keyword").Value == "Hint")
                        {
                            string HintValue = xStepElements.Peek().Element("parameters").Element("parameter").Value;
                            HintValue = HintValue.Contains(",") ? "\"" + HintValue + "\"" : HintValue;
                            HintValue = HintValue.Replace("\n", " ");

                            string newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                 "", "", "", "", "Hint", HintValue, "", "", "", "");
                            stringsToWrite.Add(newLine);

                            xStepElements.Pop();
                        }
                    }
                }

                return stringsToWrite;
            }
            catch (Exception)
            {
                return stringsToWrite;
            }
        }

        private static bool isAlertPresent()
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException ex)
            {
                return false;
            }
        }

        private static bool OnExternalSite()
        {
            List<string> ExternalSites = new List<string>(new string[] { "https://www.deltek.com" });
            bool onExternalSite = false;

            string url = DlkEnvironment.AutoDriver.Url;

            foreach (string externalSites in ExternalSites)
            {
                if (url.StartsWith(externalSites))
                {
                    return true;
                }
            }

            return onExternalSite;
        }
        #endregion

        #region Dev Keywords

        [Keyword("GenerateScript")]
        public static void GenerateScript(string ScreenName, string OutputDir)
        {
            string resultPath = string.Concat(DlkEnvironment.mDirTests, OutputDir, @"\result.xml");
            try
            {
                string[] files = Directory.GetFiles(DlkEnvironment.mDirObjectStore, string.Concat("OS_",ScreenName,".xml"), SearchOption.AllDirectories);
                string filePath = files[0];
                string xsltPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\GenerateStandardScript.xslt"));
                string resultDir = string.Concat(DlkEnvironment.mDirTests, OutputDir);
                XslCompiledTransform xslTranslator = new XslCompiledTransform();
                xslTranslator.Load(xsltPath);
                xslTranslator.Transform(filePath, resultPath);
                XmlDocument doc = new XmlDocument();
                doc.Load(resultPath);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("//test|//data");
                foreach (XmlNode node in nodes)
                {
                    if (node.Name == "test")
                    {
                        using (StreamWriter file = new StreamWriter(string.Concat(resultDir, @"\", node.SelectSingleNode("name").InnerText, ".xml"), false))
                        {
                            file.WriteLine(XDocument.Parse(node.OuterXml));
                        }
                    }
                    else
                    {
                        using (StreamWriter file = new StreamWriter(string.Concat(resultDir, @"\", node.Attributes[0].Value, ".trd"), false))
                        {
                            file.WriteLine(XDocument.Parse(node.OuterXml));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                File.Delete(resultPath);
            }
        }

        [Keyword("ControlsCount")]
        public static void ControlsCount(string Path)
        {
            try
            {
                List<string> tests = new List<string>();
                string[] myFiles = Directory.GetFiles(Path);
                foreach (var myFile in myFiles)
                {
                    FileInfo fi = new FileInfo(myFile);
                    if (fi.Extension.Equals(".xml"))
                    {
                        XDocument currentDoc = XDocument.Load(myFile);
                        if (currentDoc != null)
                        {
                            var elements = (from e in currentDoc.Element("suite").Elements("test")
                                           group e by new { Folder = e.Attribute("folder").Value, File = e.Attribute("file").Value }).ToList();
                            foreach (var item in elements)
                            {
                                string testFilePath = string.Concat(item.Key.Folder, @"\", item.Key.File);
                                if (!tests.Contains(testFilePath))
                                {
                                    tests.Add(testFilePath);
                                }
                            }
                        }
                    }
                }

                Dictionary<string, List<string>> screenControls = new Dictionary<string, List<string>>();
                foreach (string test in tests)
                {
                    string testFilePath = string.Concat(DlkEnvironment.mDirTests, test);
                    FileInfo fi = new FileInfo(testFilePath);
                    if (fi.Exists &&
                        fi.Extension.Equals(".xml"))
                    {
                        XDocument currentDoc = XDocument.Load(testFilePath);
                        var testItems = (from e in currentDoc.Element("test").Element("steps").Elements("step")
                                        group e by new { Screen = e.Element("screen").Value, Control = e.Element("control").Value }).ToList();
                        foreach (var item in testItems)
                        {
                            if (screenControls.ContainsKey(item.Key.Screen))
                            {
                                if (!screenControls[item.Key.Screen].Contains(item.Key.Control))
                                {
                                    screenControls[item.Key.Screen].Add(item.Key.Control);
                                }
                            }
                            else
                            {
                                List<string> controls = new List<string> { item.Key.Control };
                                screenControls.Add(item.Key.Screen, controls);
                            }
                        }
                    }
                }

                int totalControls = 0;
                foreach (var item in screenControls)
                {
                    DlkLogger.LogInfo(item.Key + " : " + item.Value.Count);
                    totalControls += item.Value.Count;
                }
                DlkLogger.LogInfo("Total Screens : " + screenControls.Count);

                DlkLogger.LogInfo("Total Controls : " + totalControls);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
