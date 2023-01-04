using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Drawer")]
    public class DlkDrawer : DlkBaseControl
    {
        #region Declarations
        string mainRowsXpathOld = ".//div[@class='panel-body']/div[contains(@class,'clearfix')]/div";
        string mainRowsXpath = ".//div[@class='container']//ancestor::div[@class='content']";
        string subRowsXpathOld = ".//div[@class='panel-body']/div[contains(@class,'clearfix')]/div/div[contains(@id,'content-drawer') and contains(@class,'active')]//div[contains(@class,'col-xs-12')]";
        string subRowsXpath = ".//div[@class='container']//ancestor::div[@class='content']//div[contains(@class,'active')]//div[contains(@class,'col-xs-12')]";
        string rowLinkXpathOld = "./div[@class='media-body']//h4//a[contains(@class,'matchedKey')]";
        string rowLinkXpath = ".//h4//a[contains(@class,'matchedKey')]";
        string mainRowToggleButton = ".//button[contains(@class,'main-drawer')]/span";
        string subRowToggleButton = ".//button[contains(@class,'content-drawer')]/span";
        string mainRowCogButton = ".//span[contains(@class,'glyphicon-cog')]/parent::a";
        string rowButtonsXpath = ".//*[contains(@class,'btn-default')]";
        string activePageXpath = "(//ul[contains(@class,'pagination-sm')]//li[@class='active']//span)[1]";
        string nextPageXpath = "(//a[@class='paginateNext'])[1]";
        string firstPageXpath = "(//ul[contains(@class,'pagination-sm')]//li//a[text()='1'])[1]";
        string loadIconXpath = "//div[contains(@class,'loader')]";
        bool isOld = false;
        #endregion

        #region Constructors

        public DlkDrawer(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {

        }

        public DlkDrawer(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {

        }

        #endregion

        #region Keywords
        [Keyword("ClickMainRow")]
        public void ClickMainRow(string Title)
        {
            try
            {
                initialize();
                IWebElement currentMainRowLink = getMainRowLinkByTitle(Title);
                DlkButton currentButton = new DlkButton("Main row", currentMainRowLink);
                currentButton.Click();

                DlkLogger.LogInfo("Successfully executed ClickMainRow(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickMainRow() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickSubRow")]
        public void ClickSubRow(string Title)
        {
            try
            {
                initialize();
                IWebElement currentSubRowLink = getSubRowLinkByTitle(Title);
                DlkButton currentButton = new DlkButton("Sub row", currentSubRowLink);
                currentButton.Click();

                DlkLogger.LogInfo("Successfully executed ClickSubRow(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickSubRow() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ToggleMainRow")]
        public void ToggleMainRow(string Title)
        {
            try
            {
                initialize();
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);
                IWebElement toggleButton = currentMainRowContainer.FindElement(By.XPath(mainRowToggleButton));
                DlkButton currentButton = new DlkButton("Toggle main button", toggleButton);
                currentButton.Click();

                DlkLogger.LogInfo("Successfully executed ToggleMainRow(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("ToggleMainRow() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ToggleSubRow")]
        public void ToggleSubRow(string Title)
        {
            try
            {
                initialize();
                IWebElement currentSubRowContainer = getSubRowContainerByTitle(Title);
                IWebElement toggleButton = currentSubRowContainer.FindElement(By.XPath(subRowToggleButton));
                DlkButton currentButton = new DlkButton("Toggle sub button", toggleButton);
                currentButton.Click();

                DlkLogger.LogInfo("Successfully executed ToggleSubRow(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("ToggleSubRow() execution failed. : " + ex.Message, ex);
            }
        }
        
        [Keyword("ClickMainRowIcon")]
        public void ClickMainRowIcon(string Title)
        {
            try
            {
                initialize();
                IWebElement currentIcon = getMainRowIcon(Title);
                currentIcon.Click();

                DlkLogger.LogInfo("Successfully executed ClickMainRowIcon(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickMainRowIcon() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickSubRowIcon")]
        public void ClickSubRowIcon(string Title)
        {
            try
            {
                initialize();
                IWebElement currentIcon = getSubRowIcon(Title);
                currentIcon.Click();

                DlkLogger.LogInfo("Successfully executed ClickSubRowIcon(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickSubRowIcon() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickMainRowButton")]
        public void ClickMainRowButton(string Title, string Index)
        {
            try
            {
                initialize();
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);
                List<IWebElement> buttons = currentMainRowContainer.FindElements(By.XPath(rowButtonsXpath)).ToList();
                IWebElement currentButton = buttons[Convert.ToInt32(Index) - 1];
                string currentButtonText = currentButton.Text;

                buttons[Convert.ToInt32(Index) - 1].Click();

                DlkLogger.LogInfo("Successfully executed ClickMainRowButton(): " + Index + "|" + currentButtonText);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickMainRowButton() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickSubRowButton")]
        public void ClickSubRowButton(string Title, string Index)
        {
            try
            {
                initialize();
                IWebElement currentSubRowContainer = getSubRowContainerByTitle(Title);
                List<IWebElement> buttons = currentSubRowContainer.FindElements(By.XPath(rowButtonsXpath)).ToList();
                IWebElement currentButton = buttons[Convert.ToInt32(Index) - 1];
                string currentButtonText = currentButton.Text;

                buttons[Convert.ToInt32(Index) - 1].Click();

                DlkLogger.LogInfo("Successfully executed ClickSubRowButton(): " + Index + "|" + currentButtonText);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickSubRowButton() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("PerformMainRowAction")]
        public void PerformMainRowAction(string Title, string Action)
        {
            try
            {
                initialize();
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);
                IWebElement cogButton = currentMainRowContainer.FindElement(By.XPath(mainRowCogButton));
                DlkBaseButton currentButton = new DlkBaseButton("Current button", cogButton);

                currentButton.ClickUsingJavaScript();
                Thread.Sleep(500);

                IWebElement actionLink = DlkEnvironment.AutoDriver.FindElement(
                    By.XPath("//div[contains(@class,'curriculum-actions') and contains(@class,'open')]//a[text()='" + Action +"']"));
                actionLink.Click();

                DlkLogger.LogInfo("Successfully executed PerformMainRowAction(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMainRowAction() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMainRowExists")]
        public void VerifyMainRowExists(string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool found = false;
                bool expected = Convert.ToBoolean(TrueOrFalse);

                // start at 1
                IWebElement activePage = DlkEnvironment.AutoDriver.FindElement(By.XPath(activePageXpath));

                if (activePage.Text != "1")
                {
                    IWebElement firstPage = DlkEnvironment.AutoDriver.FindElement(By.XPath(firstPageXpath));
                    firstPage.Click();
                    initialize();
                }

                List <IWebElement> paginateNext = DlkEnvironment.AutoDriver.FindElements(By.XPath(nextPageXpath)).ToList();

                do
                {
                    Thread.Sleep(1000);
                    waitForLoadIcon();
                    paginateNext = DlkEnvironment.AutoDriver.FindElements(By.XPath(nextPageXpath)).ToList();
                    initialize();
                    IWebElement currentMainRowLink = getMainRowLinkByTitle(Title);

                    if (currentMainRowLink != null)
                    {
                        found = true;
                        break;
                    }

                    if (paginateNext.Count != 0)
                    {
                        paginateNext[0].Click();
                        waitForLoadIcon();
                    }
                }
                while (paginateNext.Count != 0);

                DlkAssert.AssertEqual("VerifyMainRowExists() : ", expected, found);
                DlkLogger.LogInfo("Successfully executed VerifyMainRowExists(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMainRowExists() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySubRowExists")]
        public void VerifySubRowExists(string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expected = Convert.ToBoolean(TrueOrFalse);
                IWebElement currentSubRowLink = getSubRowLinkByTitle(Title);
                bool actual = currentSubRowLink != null ? true : false;

                DlkAssert.AssertEqual("VerifySubRowExists() : ", expected, actual);
                DlkLogger.LogInfo("Successfully executed VerifySubRowExists(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySubRowExists() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyClickableMainRowIcon")]
        public void VerifyClickableMainRowIcon(string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expected = Convert.ToBoolean(TrueOrFalse);
                IWebElement currentIcon = getMainRowIcon(Title);
                IWebElement parent = currentIcon.FindElement(By.XPath("./parent::a"));
                bool actual = isAttributePresent(parent, "onclick") && parent.GetAttribute("onclick").Contains("false") ? false : true;

                DlkAssert.AssertEqual("VerifyClickableMainRowIcon() : ", expected, actual);
                DlkLogger.LogInfo("Successfully executed VerifyClickableMainRowIcon(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyClickableMainRowIcon() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyClickableSubRowIcon")]
        public void VerifyClickableSubRowIcon(string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expected = Convert.ToBoolean(TrueOrFalse);
                IWebElement currentIcon = getSubRowIcon(Title);
                IWebElement parent = currentIcon.FindElement(By.XPath("./parent::a"));
                bool actual = isAttributePresent(parent, "onclick") && parent.GetAttribute("onclick").Contains("false") ? false : true;

                DlkAssert.AssertEqual("VerifyClickableSubRowIcon() : ", expected, actual);
                DlkLogger.LogInfo("Successfully executed VerifyClickableSubRowIcon(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyClickableSubRowIcon() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMainRowIconTooltip")]
        public void VerifyMainRowIconTooltip(string Title, string Tooltip)
        {
            try
            {
                initialize();
                IWebElement currentIcon = getMainRowIcon(Title);
                string actual = currentIcon.FindElement(By.XPath("./parent::a")).GetAttribute("data-original-title");

                DlkAssert.AssertEqual("VerifyMainRowIconTooltip() : ", Tooltip, actual);
                DlkLogger.LogInfo("Successfully executed VerifyMainRowIconTooltip(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMainRowIconTooltip() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySubRowIconTooltip")]
        public void VerifySubRowIconTooltip(string Title, string Tooltip)
        {
            try
            {
                initialize();
                IWebElement currentIcon = getSubRowIcon(Title);
                string actual = !string.IsNullOrEmpty(currentIcon.GetAttribute("title")) ? currentIcon.GetAttribute("title") :
                                !string.IsNullOrEmpty(currentIcon.GetAttribute("data-original-title")) ? currentIcon.GetAttribute("data-original-title") :
                                "";

                DlkAssert.AssertEqual("VerifySubRowIconTooltip() : ", Tooltip, actual);
                DlkLogger.LogInfo("Successfully executed VerifySubRowIconTooltip(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySubRowIconTooltip() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMainRowAction")]
        public void VerifyMainRowAction(string Title, string Action)
        {
            try
            {
                initialize();
                bool found = false;
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);
                IWebElement cogButton = currentMainRowContainer.FindElement(By.XPath(mainRowCogButton));

                cogButton.Click();
                Thread.Sleep(500);

                List<IWebElement> actionElements = currentMainRowContainer.FindElements(By.XPath(".//ul[contains(@class,'dropdown-menu')]//a")).ToList();

                foreach (var actionElement in actionElements)
                {
                    if (actionElement.Text == Action)
                    {
                        found = true;
                        break;
                    }
                }

                cogButton.Click();
                DlkAssert.AssertEqual("VerifyMainRowAction() : ", true, found);
                DlkLogger.LogInfo("Successfully executed VerifyMainRowAction(): " + Action);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMainRowAction() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMainRowDetails")]
        public void VerifyMainRowDetails(string Title, string Details)
        {
            try
            {
                initialize();
                string processedDetails = Details.Replace("\n", "~");
                string[] detailsToCheck = processedDetails.Split('~');
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);
                List<string> currentDetails = getMainRowDetails(currentMainRowContainer);

                foreach (string detail in detailsToCheck)
                {
                    bool found = currentDetails.Any(x => x == detail);
                    DlkAssert.AssertEqual("VerifyMainRowDetails() : ", true, found);
                    DlkLogger.LogInfo("Found: " + detail);
                }

                DlkLogger.LogInfo("Successfully executed VerifyMainRowDetails(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMainRowDetails() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySubRowDetails")]
        public void VerifySubRowDetails(string Title, string Details)
        {
            try
            {
                initialize();
                string processedDetails = Details.Replace("\n", "~");
                string[] detailsToCheck = processedDetails.Split('~');
                IWebElement currentSubRowContainer = getSubRowContainerByTitle(Title);
                List<string> currentDetails = getSubRowDetails(currentSubRowContainer);

                foreach (string detail in detailsToCheck)
                {
                    bool found = currentDetails.Any(x => x == detail);
                    DlkAssert.AssertEqual("VerifySubRowDetails() : ", true, found);
                    DlkLogger.LogInfo("Found: " + detail);
                }

                DlkLogger.LogInfo("Successfully executed VerifySubRowDetails(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySubRowDetails() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMainRowHasText")]
        public void VerifyMainRowHasText(string Title, string Text)
        {
            try
            {
                initialize();
                string[] textsToCheck = Text.Split('~');
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);

                if (currentMainRowContainer != null)
                {
                    foreach (string text in textsToCheck)
                    {
                        string actual = "";
                        IList<IWebElement> elementsWithText = DlkCommon.DlkCommonFunction.GetElementWithText(text, currentMainRowContainer, true);

                        if (elementsWithText.Count > 0)
                        {
                            foreach (var elementWithText in elementsWithText)
                            {
                                DlkBaseControl textControl = new DlkBaseControl("Text Control", elementWithText);
                                if (textControl.GetValue().Trim() == text)
                                {
                                    actual = textControl.GetValue().Trim();
                                    break;
                                }
                            }

                            DlkAssert.AssertEqual("VerifyMainRowHasText()", text, actual);
                        }
                        else
                        {
                            throw new Exception("No element with content : " + text);
                        }
                    }
                }

                DlkLogger.LogInfo("Successfully executed VerifyMainRowHasText(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMainRowHasText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySubRowHasText")]
        public void VerifySubRowHasText(string Title, string Text)
        {
            try
            {
                initialize();
                string[] textsToCheck = Text.Split('~');
                IWebElement currentSubRowContainer = getSubRowContainerByTitle(Title);

                if (currentSubRowContainer != null)
                {
                    foreach (string text in textsToCheck)
                    {
                        string actual = "";
                        IList<IWebElement> elementsWithText = DlkCommon.DlkCommonFunction.GetElementWithText(text, currentSubRowContainer, true);

                        if (elementsWithText.Count > 0)
                        {
                            foreach (var elementWithText in elementsWithText)
                            {
                                DlkBaseControl textControl = new DlkBaseControl("Text Control", elementWithText);
                                if (textControl.GetValue().Trim() == text)
                                {
                                    actual = textControl.GetValue().Trim();
                                    break;
                                }
                            }

                            DlkAssert.AssertEqual("VerifyMainRowHasText()", text, actual);
                        }
                        else
                        {
                            throw new Exception("No element with content : " + text);
                        }
                    }
                }

                DlkLogger.LogInfo("Successfully executed VerifySubRowHasText(): " + Title);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySubRowHasText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMainRowMarkedText")]
        public void VerifyMainRowMarkedText(string Title, string Text, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actual = false;
                bool expected = Convert.ToBoolean(TrueOrFalse);
                IWebElement currentMainRowContainer = getMainRowContainerByTitle(Title);

                if (currentMainRowContainer != null)
                {
                    IList<IWebElement> markedItem = DlkCommon.DlkCommonFunction.GetElementWithText(Text, currentMainRowContainer, elementTag: "mark");
                    if (markedItem.Count > 0)
                    {
                        actual = true;
                    }
                }

                DlkAssert.AssertEqual("VerifyMainRowMarkedText() : ", expected, actual);
                DlkLogger.LogInfo("Successfully executed VerifyMainRowMarkedText(): " + Text);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMainRowMarkedText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySubRowMarkedText")]
        public void VerifySubRowMarkedText(string Title, string Text, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actual = false;
                bool expected = Convert.ToBoolean(TrueOrFalse);
                IWebElement currentSubRowContainer = getSubRowContainerByTitle(Title);

                if (currentSubRowContainer != null)
                {
                    IList<IWebElement> markedItem = DlkCommon.DlkCommonFunction.GetElementWithText(Text, currentSubRowContainer, elementTag: "mark");
                    if (markedItem.Count > 0)
                    {
                        actual = true;
                    }
                }

                DlkAssert.AssertEqual("VerifySubRowMarkedText() : ", expected, actual);
                DlkLogger.LogInfo("Successfully executed VerifySubRowMarkedText(): " + Text);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySubRowMarkedText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifySubRowOrder", new String[] { "1|text|Subrow Title|Course 1", "2|text|Subrow Order|1"})]
        public void VerifySubRowOrder(string Title, string Order)
        {
            try
            {
                initialize();
                bool intResultTryParse = int.TryParse(Order, out int intOrder);
                if (!intResultTryParse)
                {
                    throw new Exception("Input for Order is not in number format.");
                }

                List<IWebElement> subRows = mElement.FindElements(By.XPath(subRowsXpath)).ToList();
                IWebElement subRowCurrent = subRows[intOrder - 1].FindElement(By.XPath(rowLinkXpath));

                DlkBaseControl subRow = new DlkBaseControl("CurrentSubRow", subRowCurrent);
                string actual = subRow.GetValue();

                DlkAssert.AssertEqual("VerifySubRowOrder() : ", Title, actual);
                DlkLogger.LogInfo("Successfully executed VerifySubRowOrder(): " + Title + ", " + Order);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySubRowOrder() execution failed. : " + ex.Message, ex);
            }
        }
        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();

            if (mElement.GetAttribute("class").Contains("panel-default"))
            {
                isOld = true;
            }
        }

        /// <summary>
        /// Returns link of the mainrow
        /// </summary>
        /// <param name="title"></param>
        private IWebElement getMainRowLinkByTitle(string title)
        {
            IWebElement mainRowLink = null;
            List<IWebElement> mainRows = isOld ? mElement.FindElements(By.XPath(mainRowsXpathOld)).ToList() 
                : mElement.FindElements(By.XPath(mainRowsXpath)).ToList();

            foreach (var mainRow in mainRows)
            {
                IWebElement mainRowCurrent = isOld ? mainRow.FindElement(By.XPath(rowLinkXpathOld)) : mainRow.FindElement(By.XPath(rowLinkXpath));

                if (mainRowCurrent.Text == title)
                {
                    mainRowLink = mainRowCurrent;
                    break;
                }
            }

            return mainRowLink;
        }

        /// <summary>
        /// Returns link of the subrow
        /// </summary>
        /// <param name="title"></param>
        private IWebElement getSubRowLinkByTitle(string title)
        {
            IWebElement subRowLink = null;
            List<IWebElement> subRows = isOld ? mElement.FindElements(By.XPath(subRowsXpathOld)).ToList() 
                : mElement.FindElements(By.XPath(subRowsXpath)).ToList();

            foreach (var subRow in subRows)
            {
                IWebElement subRowCurrent = isOld ? subRow.FindElement(By.XPath(rowLinkXpathOld)) : subRow.FindElement(By.XPath(rowLinkXpath));

                if (subRowCurrent.Text == title)
                {
                    subRowLink = subRowCurrent;
                    break;
                }
            }

            return subRowLink;
        }

        /// <summary>
        /// Returns container of the mainrow
        /// </summary>
        /// <param name="title"></param>
        private IWebElement getMainRowContainerByTitle(string title)
        {
            IWebElement mainRowContainer = null;
            List<IWebElement> mainRows = isOld ? mElement.FindElements(By.XPath(mainRowsXpathOld)).ToList() 
                : mElement.FindElements(By.XPath(mainRowsXpath)).ToList();

            foreach (var mainRow in mainRows)
            {
                IWebElement mainRowCurrent = isOld ? mainRow.FindElement(By.XPath(rowLinkXpathOld)) : mainRow.FindElement(By.XPath(rowLinkXpath));

                if (mainRowCurrent.Text == title)
                {
                    mainRowContainer = isOld ? mainRowCurrent.FindElement(By.XPath(".//ancestor::div[contains(@class,'col-xs-12')]")) 
                        : mainRowCurrent.FindElement(By.XPath(".//ancestor::div[@class='content']"));
                    break;
                }
            }

            if (mainRowContainer == null)
            {
                throw new Exception("Cannot find main row with title: " + title);
            }

            return mainRowContainer;
        }

        /// <summary>
        /// Returns container of the subrow
        /// </summary>
        /// <param name="title"></param>
        private IWebElement getSubRowContainerByTitle(string title)
        {
            IWebElement subRowContainer = null;
            List<IWebElement> subRows = isOld ? mElement.FindElements(By.XPath(subRowsXpathOld)).ToList() : 
                mElement.FindElements(By.XPath(subRowsXpath)).ToList();

            foreach (var subRow in subRows)
            {
                IWebElement subRowCurrent = isOld ? subRow.FindElement(By.XPath(rowLinkXpathOld)) : subRow.FindElement(By.XPath(rowLinkXpath));

                if (subRowCurrent.Text == title)
                {
                    subRowContainer = isOld ? subRowCurrent.FindElement(By.XPath(".//ancestor::div[@class='media-body']/parent::div")) 
                        : subRowCurrent.FindElement(By.XPath(".//ancestor::div[contains(@class,'col-xs-12')]"));
                    break;
                }
            }

            if (subRowContainer == null)
            {
                throw new Exception("Cannot find sub row with title: " + title);
            }

            return subRowContainer;
        }

        /// <summary>
        /// Returns icon of the mainrow
        /// </summary>
        /// <param name="title"></param>
        private IWebElement getMainRowIcon(string title)
        {
            IWebElement mainRowIcon = null;
            IWebElement mainRowLink = getMainRowLinkByTitle(title);
            mainRowIcon = mainRowLink.FindElement(By.XPath("./preceding-sibling::a/i"));

            return mainRowIcon;
        }

        /// <summary>
        /// Returns icon of the subrow
        /// </summary>
        /// <param name="title"></param>
        private IWebElement getSubRowIcon(string title)
        {
            IWebElement subRowIcon = null;
            IWebElement subRowLink = getSubRowLinkByTitle(title);
            subRowIcon = subRowLink.FindElement(By.XPath("./preceding-sibling::a/i"));

            return subRowIcon;
        }

        /// <summary>
        /// Checks if element has an attribute
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        private bool isAttributePresent(IWebElement element, string attribute)
        {
            bool result = false;
            string value = element.GetAttribute(attribute);

            if (value != null)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Returns details of the mainrow
        /// </summary>
        /// <param name="mainRowContainer"></param>
        private List<string> getMainRowDetails(IWebElement mainRowContainer)
        {
            List<string> finalDetails = new List<string>();
            string mainRowBadgeDetail = isOld ? mainRowContainer.FindElement(By.XPath("./div[@class='media-body']//span[contains(@class,'badge')]")).Text
                : mainRowContainer.FindElement(By.XPath(".//div[@class='text-muted text-sm divider-bulleted content-subheading']//span[contains(@class,'badge')]")).Text;
            List<IWebElement> mainRowDetails = isOld ? mainRowContainer.FindElements(By.XPath("./div[@class='media-body']//span[@class='divider-bullet']")).ToList()
                : mainRowContainer.FindElements(By.XPath(".//div[@class='text-muted text-sm divider-bulleted content-subheading']//span[@class='divider-bullet']")).ToList();
            string mainRowDescription = isOld ? mainRowContainer.FindElement(By.XPath("./div[@class='media-body']//div[contains(@class,'content-description')]")).Text
                : mainRowContainer.FindElement(By.XPath(".//div[@class='text-muted text-sm divider-bulleted content-subheading']/following-sibling::div[contains(@class,'content-description')]")).Text;

            // add badge detail
            finalDetails.Add(mainRowBadgeDetail);

            // add row details
            foreach (IWebElement mainRowDetail in mainRowDetails)
            {
                string text = mainRowDetail.Text;
                finalDetails.Add(text);
            }

            // add row description
            finalDetails.Add(mainRowDescription);

            return finalDetails;
        }

        /// <summary>
        /// Returns details of the subrow
        /// </summary>
        /// <param name="subRowContainer"></param>
        private List<string> getSubRowDetails(IWebElement subRowContainer)
        {
            List<string> finalDetails = new List<string>();
            string subRowBadgeDetail = isOld ? subRowContainer.FindElement(By.XPath("./div[@class='media-body']//span[contains(@class,'badge')]")).Text
                : subRowContainer.FindElement(By.XPath(".//div[@class='text-muted text-sm details divider-bulleted content-subheading']//span[contains(@class,'badge')]")).Text;
            List<IWebElement> subRowDetails = isOld ? subRowContainer.FindElements(By.XPath("./div[@class='media-body']//span[@class='divider-bullet']")).ToList()
                : subRowContainer.FindElements(By.XPath(".//div[@class='text-muted text-sm details divider-bulleted content-subheading']//span[@class='divider-bullet']")).ToList();
            string subRowDescription = isOld ? subRowContainer.FindElement(By.XPath("./div[@class='media-body']//div[contains(@class,'content-description')]")).Text
                : subRowContainer.FindElement(By.XPath(".//div[@class='text-muted text-sm details divider-bulleted content-subheading']/following-sibling::div[contains(@class,'content-description')]")).Text;

            // add badge detail
            finalDetails.Add(subRowBadgeDetail);

            // add row details
            foreach (IWebElement subRowDetail in subRowDetails)
            {
                string text = subRowDetail.Text;
                finalDetails.Add(text);
            }

            // add row description
            finalDetails.Add(subRowDescription);

            return finalDetails;
        }

        /// <summary>
        /// Wait for load icon to disappear
        /// </summary>
        private void waitForLoadIcon()
        {
            IList<IWebElement> loadIcons = DlkEnvironment.AutoDriver.FindElements(By.XPath(loadIconXpath)).Where(x => x.Displayed).ToList();

            while (loadIcons.Count != 0)
            {
                Thread.Sleep(500);
                loadIcons = DlkEnvironment.AutoDriver.FindElements(By.XPath(loadIconXpath)).Where(x => x.Displayed).ToList();
            }
        }

        #endregion

        #region Properties
        #endregion
    }
}
