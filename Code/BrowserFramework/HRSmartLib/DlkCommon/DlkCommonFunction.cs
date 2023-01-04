using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using HRSmartLib.DlkSystem;
using HRSmartLib.LatestVersion.DlkControls;
using HRSmartLib.System;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Automation;

namespace HRSmartLib.DlkCommon
{
    public class DlkCommonFunction
    {
        #region Declarations
        public const string SKIP_TEXTBOX_SET = "@escape";
        public const string MISSING_DIALOG_ERR = "Please check the data manually as the alert message is missing.";
        public const string PROCESS_ERROR = "Process Error";
        public const string ACCESS_ERROR = "Access Error";
        public const string PERMISSION_ERROR = "Permission Error";
        public const string OTHER_ERROR = "Other Error";
        public const string ACCESSIBLE = "Accessible";
        #endregion

        #region Constructors
        #endregion

        #region Methods

        /// <summary>
        /// Find Element with matching text.
        /// </summary>
        /// <param name="textToSearch"></param>
        /// <param name="parentElement"></param>
        /// <param name="partialMatch"></param>
        /// <returns></returns>
        public static IList<IWebElement> GetElementWithText(string textToSearch, 
                                                            IWebElement parentElement = null,
                                                            bool partialMatch = false,
                                                            string elementTag = "*",
                                                            bool stopRecursion = false,
                                                            bool ignoreCasing = false,
                                                            bool returnDisplayedElements = false)
        {
            try
            {
                string browserTextContent = "normalize-space()";
                if (ignoreCasing)
                {
                    browserTextContent = "translate(normalize-space(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')";
                    textToSearch = textToSearch.ToLower();
                }

                string groupKey = "\"";
                if (textToSearch.Contains("\""))
                {
                    groupKey = "'";
                }


                string searchCriteria = $"//{elementTag}[text()[{browserTextContent}={groupKey}{textToSearch}{groupKey}] and not(.//{elementTag}[text()[{browserTextContent}={groupKey}{textToSearch}{groupKey}]])]";
                
                IList<IWebElement> matchingElements = null;

                if (partialMatch)
                {
                    searchCriteria = $"//{elementTag}[contains({browserTextContent},{groupKey}{textToSearch}{groupKey}) and not(.//{elementTag}[contains({browserTextContent},{groupKey}{textToSearch}{groupKey})])]";
                }

                if (parentElement == null)
                {
                    matchingElements = DlkEnvironment.AutoDriver.FindElements(By.XPath(searchCriteria));
                }
                else
                {
                    matchingElements = parentElement.FindElements(By.XPath(string.Concat(".", searchCriteria)));
                }

                //Change due to UIUX underlying text is not all caps but displaying all caps.
                if (matchingElements.Count == 0 && !stopRecursion)
                {
                    textToSearch = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(textToSearch.ToLower());
                    matchingElements = GetElementWithText(textToSearch, parentElement, partialMatch, elementTag, true);
                }

                if (returnDisplayedElements)
                {
                    return matchingElements.Where(item => item.Displayed).ToList();
                }

                return matchingElements;

            }
            catch(Exception ex)
            {
                throw new Exception("GetElementWithText() element with text : " + textToSearch + " doesnt exist. " + ex.Message, ex);
            }
        }

        public static void ClickElementByTitleAndIndex(IWebElement element, string title, int index)
        {
            try
            {
                IList<IWebElement> elements = GetElementWithText(title, element);
                ClickElementByIndex(elements, index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ClickElementByIndex(IList<IWebElement> clickableElements, int index, bool doubleClick = false)
        {
            try
            {
                int indexCounter = -1;
                bool hasExpectedElement = false;

                for (int i = 0; i < clickableElements.Count; i++)
                {
                    IWebElement clickableElement = clickableElements[i];
                    switch (clickableElement.TagName.Trim())
                    {
                        case "button":
                        case "a":
                            {
                                if (clickableElement.Displayed)
                                {
                                    indexCounter++;
                                }
                                if (index == indexCounter)
                                {
                                    hasExpectedElement = true;
                                    DlkButton buttonControl = new DlkButton("Button_Element", clickableElement);
                                    if (doubleClick)
                                    {
                                        buttonControl.DoubleClick();
                                    }
                                    else
                                    {
                                        buttonControl.Click();
                                    }
                                }
                                break;
                            }
                        case "input":
                            {
                                if (clickableElement.GetAttribute("type") != null &&
                                    clickableElement.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    if (clickableElement.Displayed)
                                    {
                                        indexCounter++;
                                    }
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox_Element : " + clickableElement.Text, clickableElement);
                                        if (doubleClick)
                                        {
                                            checkBoxControl.DoubleClick();
                                        }
                                        else
                                        {
                                            checkBoxControl.Click();
                                        }
                                    }
                                }
                                else if (clickableElement.GetAttribute("type") != null &&
                                            clickableElement.GetAttribute("type").Equals("radio"))
                                {
                                    if (clickableElement.Displayed)
                                    {
                                        indexCounter++;
                                    }
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkRadioButton radioButtonControl = new DlkRadioButton("RadioButton_Element : " + clickableElement.Text, clickableElement);
                                        if (doubleClick)
                                        {
                                            radioButtonControl.DoubleClick();
                                        }
                                        else
                                        {
                                            radioButtonControl.Click();
                                        }
                                    }
                                }
                                break;
                            }
                    }

                    if (hasExpectedElement)
                    {
                        break;
                    }
                }

                if (!hasExpectedElement)
                {
                    throw new Exception("No clickable element found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetElementByIndex(IWebElement element, string Value, int index)
        {
            try
            {
                int indexCounter = -1;
                bool hasExpectedElement = false;
                IList<IWebElement> elements = element.FindElements(By.XPath(".//input | .//select | .//iframe | .//textarea"));
                for (int i = 0; i < elements.Count; i++)
                {
                    IWebElement targetElement = elements[i];
                    if (!targetElement.Displayed)
                    {
                        continue;
                    }
                    switch (targetElement.TagName)
                    {
                        case "textarea" :
                        {
                                indexCounter++;
                                if (index == indexCounter)
                                {
                                    hasExpectedElement = true;
                                    SelectAllClear(targetElement);
                                    targetElement.SendKeys(Value);
                                }
                                break;
                        }
                        case "input" :
                            {
                                if (targetElement.GetAttribute("type") != null &&
                                    targetElement.GetAttribute("type").Trim().Equals("text"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        SelectAllClear(targetElement);
                                        while (targetElement.Text.Length > 0)
                                        {
                                            targetElement.SendKeys(Keys.Backspace);
                                        }
                                        targetElement.SendKeys(Value);
                                    }
                                }
                                else if (targetElement.GetAttribute("type") != null &&
                                         targetElement.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox", targetElement);
                                        checkBoxControl.Set(Value);
                                    }
                                }
                                else if (targetElement.GetAttribute("type") != null &&
                                         targetElement.GetAttribute("type").Equals("radio"))
                                {

                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        //value should be row or column number.
                                        hasExpectedElement = true;
                                        DlkRadioButton radioButton = new DlkRadioButton("Radio Button", targetElement);
                                        radioButton.ClickUsingJavaScript();
                                    }
                                }
                                break;
                            }
                        case "select":
                            {
                                if (targetElement.GetAttribute("multiple") == null)
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        DlkComboBox comboBoxControl = new DlkComboBox("ComboBox", targetElement);
                                        comboBoxControl.Select(Value);
                                        hasExpectedElement = true;
                                    }
                                }
                                else
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        DlkMultiSelect multiSelectControl = new DlkMultiSelect("MultiSelect", targetElement);
                                        multiSelectControl.Select(Value);
                                        hasExpectedElement = true;
                                    }
                                }
                                break;
                            }
                        case "iframe":
                            {
                                if (targetElement.GetAttribute("class") != null &&
                                    targetElement.GetAttribute("class").Trim().Equals("cke_wysiwyg_frame cke_reset"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkRichTextEditor richTextEditorControl = new DlkRichTextEditor("RichTextEditor_", targetElement);
                                        richTextEditorControl.Set(Value);
                                    }
                                }
                                break;
                            }
                    }

                    if (hasExpectedElement)
                    {
                        break;
                    }
                }

                if (!hasExpectedElement)
                {
                    throw new Exception("No element found to set.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool VerifyElementByIndexExists(IWebElement element, int index)
        {
            try
            {
                int indexCounter = -1;
                bool hasExpectedElement = false;
                IList<IWebElement> elements = element.FindElements(By.XPath(".//input | .//select | .//iframe | .//textarea"));
                for (int i = 0; i < elements.Count; i++)
                {
                    IWebElement targetElement = elements[i];
                    if (!targetElement.Displayed)
                    {
                        continue;
                    }
                    switch (targetElement.TagName)
                    {
                        case "textarea":
                            {
                                indexCounter++;
                                if (index == indexCounter)
                                {
                                    hasExpectedElement = true;
                                }
                                break;
                            }
                        case "input":
                            {
                                if (targetElement.GetAttribute("type") != null &&
                                    targetElement.GetAttribute("type").Trim().Equals("text"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                    }
                                }
                                else if (targetElement.GetAttribute("type") != null &&
                                         targetElement.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                    }
                                }
                                else if (targetElement.GetAttribute("type") != null &&
                                         targetElement.GetAttribute("type").Equals("radio"))
                                {

                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                    }
                                }
                                break;
                            }
                        case "select":
                            {
                                if (targetElement.GetAttribute("multiple") == null)
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                    }
                                }
                                else
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                    }
                                }
                                break;
                            }
                        case "iframe":
                            {
                                if (targetElement.GetAttribute("class") != null &&
                                    targetElement.GetAttribute("class").Trim().Equals("cke_wysiwyg_frame cke_reset"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                    }
                                }
                                break;
                            }
                    }

                    if (hasExpectedElement)
                    {
                        break;
                    }
                }

                return hasExpectedElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Will get the Wizard Step or Header of page.
        /// </summary>
        /// <returns>Wizard Step or Header respectively.</returns>
        public static string GetWizardStepOrHeader()
        {
            string actualResult = string.Empty;
            IList<IWebElement> wizardStep = DlkEnvironment.AutoDriver.FindElements(By.XPath(@"//li[contains(@class,'active')]/a/p"));
            if (wizardStep.Count > 0)
            {
                DlkBaseControl wizardStepControl = new DlkBaseControl("Wizard_Step : " + wizardStep[0].Text, wizardStep[0]);
                return wizardStepControl.GetValue().Trim();
            }
            else
            {
                IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("(//h1)[last()]"));
                DlkBaseControl headerControl = new DlkBaseControl("Header", header);
                return headerControl.GetValue().Trim().Replace("\r\n","\n");
            }
        }

        public static IFunctionHandler GetFunctionHandler()
        {
            IFunctionHandler handler = null;

            handler = new LatestVersion.System.DlkHRSmartFunctionHandler();
            /*if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("DTM_A"))
            {
                //for now we will be using the latest version and will change accordinglly if necessary.
                //handler = new PreviousVersion.System.DlkHRSmartFunctionHandler();
                handler = new LatestVersion.System.DlkHRSmartFunctionHandler();
            }
            else if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("DTM_B"))
            {
                handler = new LatestVersion.System.DlkHRSmartFunctionHandler();
            }
            else if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("New"))
            {
                handler = new LatestVersion.System.DlkHRSmartFunctionHandler();
            }*/
            return handler;
        }

        public static IKeywordHandler GetKeywordHandler()
        {
            IKeywordHandler handler = null;

            handler = new LatestVersion.System.DlkHRSmartKeywordHandler();
            /*if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("DTM_A"))
            {
                //for now we will be using the latest version and will change accordinglly if necessary.
                //handler = new PreviousVersion.System.DlkHRSmartKeywordHandler();
                handler = new LatestVersion.System.DlkHRSmartKeywordHandler();
            }
            else if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("DTM_B"))
            {
                handler = new LatestVersion.System.DlkHRSmartKeywordHandler();
            }
            else if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("New"))
            {
                handler = new LatestVersion.System.DlkHRSmartKeywordHandler();
            }*/

            return handler;
        }

        /// <summary>
        /// Will check for Message Dialog Control Availability.
        /// </summary>
        /// <returns>True if there is a Message or Upload Dialog on Screen otherwise False.</returns>
        public static bool HasOpenMessageDialog()
        {
            bool hasOpenDialog = DlkAlert.DoesAlertExist();
            if (!hasOpenDialog)
            {
                IList<IWebElement> messageDialogElement = DlkEnvironment.AutoDriver.FindElements(By.CssSelector("div#successOrFail > div"));
                if (messageDialogElement.Count == 0)
                {
                    hasOpenDialog = checkFileUploadDialog();
                }
                else
                {
                        
                    hasOpenDialog = true;
                }
            }

            return hasOpenDialog;
        }

        public static bool VerifyElementExists(IWebElement element)
        {
            bool isExists = false;

            try
            {
                if (element != null &&
                    element.Displayed)
                {
                    isExists = true;
                }
            }
            catch
            {
                isExists = false;
            }

            return isExists;
        }

        public static void ScrollIntoElement(IWebElement element)
        {
            if (DlkEnvironment.mBrowser.ToLower() == "chrome")
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(element, 5, 5).Perform();
            }
            else
            {
                IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                javascript.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                javascript.ExecuteScript("arguments[0].scrollIntoView(false);", element);
            }
        }

        private static bool checkFileUploadDialog()
        {
            bool hasOpenDialog = false;
            AutomationElement fileUploadDialog = null;
            AutomationElement aeDesktop = AutomationElement.RootElement;
            Condition controlCondition = Automation.ControlViewCondition;
            TreeWalker controlWalker = new TreeWalker(controlCondition);
            List<string> dialogNames = new List<string>() { "Upload", "Open", "Choose File to Upload" };

            AutomationElement browserWindow = CommonLib.DlkUtility.DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, DlkEnvironment.mPreviousTitle);

            for (int i = 0; i < dialogNames.Count; i++)
            {
                fileUploadDialog = CommonLib.DlkUtility.DlkMSUIAutomationHelper.FindWindow(browserWindow, controlWalker, dialogNames[i]);
                if (fileUploadDialog != null)
                {
                    hasOpenDialog = true;
                    break;
                }
            }

            return hasOpenDialog;
        }

        public static void ScrollIntoView(IWebElement element)
        {
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            javascript.ExecuteScript("scroll(20000,0)");
            if (CommonLib.DlkHandlers.DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("Previous"))
            {
                javascript.ExecuteScript("arguments[0].scrollIntoView(false);", element);
            }
            else
            {
                javascript.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }

            //IList<IWebElement> floatingHeader = element.FindElements(By.XPath(".//ancestor::table//thead[contains(@class,'fixed')]"));
            //if (floatingHeader.Count > 0)
            //{
                javascript.ExecuteScript("window.scrollBy(" + 0 + "," + -75 + ");");
            //}

            
        }

        public static bool CheckFatalError()
        {
            try
            {
                bool hasFatalError = false;
                List<string> errorMsgs = new List<string>()
                {
                    "We're sorry, but the application could not complete your request at this time",
                    "You do not have permission to view this page.",
                    "An error has occurred while trying to access this page."
                };

                foreach (string msg in errorMsgs)
                { 
                    IList<IWebElement> fatalError = GetElementWithText(msg);

                    if (fatalError != null && fatalError.Count > 0)
                    {
                        hasFatalError = true;
                        break;
                    }
                }


                return hasFatalError;
            }
            catch
            {
                //dont process the exception as it will cause the Test Suite to stop / run in idle.
                return false;
            }
        }

        public static void DragAndDrop(IWebElement fromElement, IWebElement toElement)
        {
            try
            {
                HRSmartLib.DlkCommon.DlkCommonFunction.ScrollIntoView(toElement);

                // if browser is firefox make sure 'from' element is also seen
                if (DlkEnvironment.mBrowser.ToLower() == "firefox")
                {
                    HRSmartLib.DlkCommon.DlkCommonFunction.ScrollIntoView(fromElement);
                }

                if (fromElement != null &&
                    toElement != null)
                {
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.ClickAndHold(fromElement).Build().Perform();
                    mAction.MoveToElement(toElement).Build().Perform();
                    mAction.Release().Build().Perform();
                    Thread.Sleep(1500);
                }
                else
                {
                    throw new Exception("Please check parameters.");
                }

                DlkLogger.LogInfo("DragAndDrop( ) passed");
            }
            catch (Exception ex)
            {
                throw new Exception("DragAndDrop( ) failed..." + ex.Message, ex);
            }
        }

        public static void DragAndDrop(IWebElement fromElement, IWebElement toElement, int offsetX, int offsetY)
        {
            try
            {
                HRSmartLib.DlkCommon.DlkCommonFunction.ScrollIntoView(toElement);

                // if browser is firefox make sure 'from' element is also seen
                if (DlkEnvironment.mBrowser.ToLower() == "firefox")
                {
                    HRSmartLib.DlkCommon.DlkCommonFunction.ScrollIntoView(fromElement);
                }

                if (fromElement != null &&
                    toElement != null)
                {
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.ClickAndHold(fromElement).Build().Perform();
                    mAction.MoveToElement(toElement).Build().Perform();
                    mAction.MoveByOffset(offsetX, offsetY);
                    mAction.Release().Build().Perform();
                    Thread.Sleep(1500);
                }
                else
                {
                    throw new Exception("Please check parameters.");
                }

                DlkLogger.LogInfo("DragAndDrop( ) passed");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("move target out of bounds"))
                {
                    // scroll down again for retry
                    Thread.Sleep(2000);
                    DlkFunctionHandler.ScrollDown();

                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.ClickAndHold(fromElement).Build().Perform();
                    mAction.MoveToElement(toElement).Build().Perform();
                    mAction.MoveByOffset(offsetX, offsetY);
                    mAction.Release().Build().Perform();
                    Thread.Sleep(1500);
                }
                else
                {
                    throw new Exception("DragAndDrop( ) failed..." + ex.Message, ex);
                }
            }
        }

        public static string StripHTMLTags(string source, bool replaceParagraphTagToEnter = false)
        {
            if (replaceParagraphTagToEnter)
            {
                source = source.Replace("<br></p>", "\n").Replace("</p>", "\n").Replace("<br>", "\n").Replace("\n ", "\n").Replace("\t", "").Replace("\r\n", "\n");
            }
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true; continue;
                }
                if (let == '>')
                {
                    inside = false; continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let; arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex).TrimEnd(Environment.NewLine.ToCharArray());
        }

        public static void SelectAllClear(IWebElement targetElement)
        {
            targetElement.SendKeys(Keys.Control + "a");
            targetElement.SendKeys(Keys.Delete);
        }

        #endregion
    }
}
