using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("ContainerGroup")]
    public class DlkContainerGroup : DlkBaseControl
    {
        #region Declarations

        private const string TOGGLE_HEADER = @".//div[@role='tab' and @class='panel-heading']";

        #endregion

        #region Constructors

        public DlkContainerGroup(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {

        }

        public DlkContainerGroup(String ControlName, IWebElement ExistingElement)
            : base(ControlName, ExistingElement)
        {

        }

        #endregion

        #region Properties
        #endregion

        #region Keywords

        [Keyword("SetRowScore")]
        public void SetRowScore(string Row, string Value)
        {
            try
            {
                if (!Value.Equals(string.Empty))
                {
                    initialize();
                    int row = Int32.MinValue;
                    if (Int32.TryParse(Row, out row))
                    {
                        DlkContainerItem containerItem = getRow(row - 1);
                        containerItem.SetScore(Value);
                        DlkLogger.LogInfo("SetRowScore() successfully executed.");
                    }
                    else
                    {
                        throw new Exception("Row : " + Row + " is not an integer.");
                    }
                }
                else
                {
                    DlkLogger.LogInfo("Skipping set since the data parameter is blank.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetRowScore() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("SelectRowCommentPermission")]
        public void SelectRowCommentPermission(string Row, string Value)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.SelectCommentPermission(Value);
                    DlkLogger.LogInfo("SelectRowCommentPermission() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectRowCommentPermission() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("SetRowComment")]
        public void SetRowComment(string Row, string Value)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.SetComment(Value);
                    DlkLogger.LogInfo("SetRowComment() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetRowComment() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickRowControlByIndex")]
        public void ClickRowControlByIndex(string Row, string Index)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                int index = Int32.Parse(Index) - 1;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.ClickControlByIndex(index);
                    DlkLogger.LogInfo("ClickRowControlByIndex() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ClickRowControlByIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickRowControlByTitle")]
        public void ClickRowControlByTitle(string Row, string Title)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.ClickControlByTitleAndIndex(Title, 0);
                    DlkLogger.LogInfo("ClickRowControlByTitle() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("ClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickRowControlByTitleAndIndex")]
        public void ClickRowControlByTitleAndIndex(string Row, string Title, string Index)
        {
            try
            {
                initialize();
                int index = Int32.Parse(Index) - 1;
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.ClickControlByTitleAndIndex(Title, index);
                    DlkLogger.LogInfo("ClickRowControlByTitleAndIndex() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ClickRowControlByTitleAndIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("RowMenuButtonClickByTitle")]
        public void RowMenuButtonClickByTitle(string Row, string Title)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.MenuButtonSelectByTitle(Title);
                    DlkLogger.LogInfo("RowMenuButtonClickByTitle() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("RowMenuButtonClickByIndex")]
        public void RowMenuButtonClickByIndex(string Row, string Index)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.MenuButtonSelectByIndex(Index);
                    DlkLogger.LogInfo("RowMenuButtonClickByIndex() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowMenuButtonClickByIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("RowClickAddJournalEntry")]
        public void RowClickAddJournalEntry(string Row)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.ClickAddJournalEntry();
                    DlkLogger.LogInfo("RowClickAddJournalEntry() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowClickAddJournalEntry() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("RowClickDownloadIcon")]
        public void RowClickDownloadIcon(string Row)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.ClickDownloadIcon();
                    DlkLogger.LogInfo("RowClickDownloadIcon() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowClickDownloadIcon() execution failed. " + ex.Message, ex);
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
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount")]
        public void VerifyRowCount(string RowCount)
        {
            try
            {
                initialize();
                int expectedResult = Convert.ToInt32(RowCount);
                int actualResult = getRows().Count;
                DlkAssert.AssertEqual("VerifyRowCount : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() execution failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCommentState")]
        public void VerifyRowCommentState(string Row, string TrueOrFalse)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.VerifyCommentState(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyRowCommentState() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyRowCommentState() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyRowCommentPermissionState")]
        public void VerifyRowCommentPermissionState(string Row, string TrueOrFalse)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.VerifyCommentPermissionState(TrueOrFalse);
                    DlkLogger.LogInfo("VerifyRowCommentPermissionState() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRowCommentPermissionState() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyRowControlByTitleExists")]
        public void VerifyRowControlByTitleExists(string Row, string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.VerifyControlByTitleExists(Title, Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyRowControlByTitleExists() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyRowControlByTitleExists() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("PerformTableRowActionColumn")]
        public void PerformTableRowActionByColumn(string Row, string TableRow, string Column, string MenuAction)
        {
            try
            {
                initialize();
                int row = Int32.MinValue;
                if (Int32.TryParse(Row, out row))
                {
                    DlkContainerItem containerItem = getRow(row - 1);
                    containerItem.PerformTableRowActionByColumn(TableRow, Column, MenuAction);
                    DlkLogger.LogInfo("PerformTableRowActionByColumn() successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PerformTableRowActionByColumn() execution failed. " + ex.Message, ex);
            }
        }

        #endregion

        #region Keywords Workflow Review

        [Keyword("HasTextContent")]
        public void HasTextContent(string Text)
        {
            try
            {
                initialize();
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResult in expectedResults)
                {
                    string expected = expectedResult;
                    string actualResult = string.Empty;
                    IWebElement element = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, mElement, false)[0];
                    DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                    if (textControl.mElement.TagName == "th")
                    {
                        expected = expectedResult.ToUpper();
                    }
                    actualResult = textControl.GetValue().Trim();

                    DlkAssert.AssertEqual("HasTextContent : ", expected, actualResult);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("HasTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("HasPartialTextContent")]
        public void HasPartialTextContent(string Text)
        {
            try
            {
                initialize();
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResult in expectedResults)
                {
                    string expected = expectedResult;
                    IWebElement element = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, mElement, true)[0];
                    DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                    if (textControl.mElement.TagName == "th")
                    {
                        expected = expectedResult.ToUpper();
                    }
                    string actualResult = textControl.GetValue().Trim();
                    DlkAssert.AssertEqual("HasPartialTextContent : ", expected, actualResult, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("HasPartialTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("ToggleHeader")]
        public void ToggleHeader()
        {
            try
            {
                initialize();
                IWebElement headerElement = mElement.FindElement(By.XPath(TOGGLE_HEADER));
                DlkBaseControl headerControl = new DlkBaseControl("Header", headerElement);
                headerControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ToggleHeader() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ToggleHeader() execution failed. " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private DlkContainerItem getRow(int row)
        {
            DlkContainerItem containerItem = null;
            IList<IWebElement> elements = getRows();
            if (row <= elements.Count)
            {
                containerItem = new DlkContainerItem("Container_Item", elements[row]);
            }
            else
            {
                throw new Exception("Row : " + row + " not found.");
            }

            return containerItem;
        }

        private IList<IWebElement> getRows()
        {
            IList<IWebElement> rows = null;
            List<string> rowLocators = new List<string>()
            {
                @"./div[@id and not(contains(@id,'instructionContainer'))]",
                @"./div[@class='panel-body']/div[@class='row']",
                @"./div[@class='panel panel-default']/div[@id]",
                @"./div[@class='panel-body']/div[@class='row-table']/div[@class='row']",
                @"./fieldset/div[@class='row']",
                @"./div[@class='panel-body']/div[@class='table-responsive']/table/tbody/tr",
                @"./div/div[contains(@class,'row')]",
                @"./fieldset/div[contains(@class,'score')]",
                @".//fieldset//div[@class='rating_row']"
            };
            for (int i = 0; i < rowLocators.Count; i++)
            {
                rows = mElement.FindElements(By.XPath(rowLocators[i]));
                if (rows.Count > 0)
                    break;
            }
            return rows;
        }

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }

    public class DlkContainerItem : DlkBaseControl
    {
        #region Declarations

        //Control for scoring appraisal.
        private const string COMMENT_PERMISSION = @".//select[contains(@name,'rating_comment_permission')]";
        private const string COMMENT = @".//textarea[contains(@name,'rating_comment') and not(contains(@style,'hidden'))]";
        private const string SLIDER_RATER = @".//div[@class='slider slider-horizontal']";
        private const string NUMERIC_RATER = @".//input[contains(@id,'field_goal_numeric')]";
        private const string ALL_OR_NOTHING_RATER = @".//input[contains(@id,'goal_allornothing')]";
        //Control for My Appraisals.
        private const string MENU_BUTTON = @".//div[contains(@id,'ribbon_action_menu')]/a | .//div[contains(@class,'dropdown')]/a | .//a[@class='btn-panel dropdown-toggle']";
        private const string ADD_JOURNAL_ENTRY = @".//a[@class='btn btn-link'][1]";
        private const string DOWNLOAD_ICON = @".//a/img";
        //Control for CDSP My Employee Development.
        private const string TABLE = @"./following-sibling::div//table";

        #endregion

        #region Constructors

        public DlkContainerItem(String ControlName, IWebElement ExistingElement)
            : base(ControlName, ExistingElement) { }

        #endregion

        #region Properties
        #endregion

        #region Keywords

        public void ClickControlByTitleAndIndex(string title, int index)
        {
            IList<IWebElement> elements = getControlWithText(title);
            try
            {
                if (index < elements.Count)
                {
                    DlkButton buttonControl = new DlkButton("Add Journal Entry", elements[index]);
                    buttonControl.Click();
                    //elements[index].SendKeys(Keys.Enter);
                }
                else
                {
                    throw new Exception("Index : " + index + " out of bounds.");
                }
            } 
            catch(Exception ex)
            {
                if (ex.Message.Contains("unknown error: cannot focus element") ||
                    ex.Message.Contains("Element is not clickable"))
                {
                    DlkBaseControl control = new DlkBaseControl("Clickable Control", elements[index]);
                    control.Click();
                }
                else
                {
                    throw ex;
                }
            }
        }

        public void ClickControlByIndex(int index)
        {
            int indexCounter = -1;
            IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//*"));
            try
            {
                for (int i = 0; i <= elements.Count; i++)
                {
                    IWebElement element = elements[i];
                    if (element.TagName.Equals("a") ||
                        element.TagName.Equals("button") ||
                        element.TagName.Equals("img"))
                    {
                        indexCounter++;
                        if (indexCounter == index)
                        {
                            //for handling error Element is npt clickable.
                            indexCounter = i;
                            DlkBaseControl control = new DlkBaseControl("Clickable Control", element);
                            control.Click();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unknown error: cannot focus element") ||
                    ex.Message.Contains("Element is not clickable"))
                {
                    DlkBaseControl control = new DlkBaseControl("Clickable Control", elements[indexCounter]);
                    control.Click();
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Keywords Scoring Appraisals

        //scoring appraisal
        public void SetScore(string value)
        {
            IWebElement element = getScoringElement(value); 
            if (element != null)
            {
                switch(element.TagName.Trim())
                {
                    case "input" :
                        {
                            string elementAttr = element.GetAttribute("type");

                            if (elementAttr != null)
                            {
                                switch (elementAttr)
                                {
                                    case "radio" :
                                    {
                                        DlkRadioButton radioButton = new DlkRadioButton(value, element);
                                        radioButton.ClickUsingJavaScript();
                                        break;
                                    }
                                    case "checkbox" :
                                    {
                                        DlkCheckBox checkBoxControl = new DlkCheckBox("All_Or_Nothing", element);
                                        if (checkBoxControl.mElement.Text == string.Empty)
                                        {
                                            element.Click();
                                        }
                                        else
                                        {
                                            checkBoxControl.Set(value);
                                        }
                                        break;
                                    }
                                    case "text" :
                                    {
                                        DlkTextBox textBoxControl = new DlkTextBox("Goal_Numeric", element);
                                        textBoxControl.Set(value);
                                        break;
                                    }
                                }
                            }

                            break;
                        }
                    case "div" :
                        {
                            if (element.GetAttribute("class") != null &&
                                element.GetAttribute("class").Equals("slider slider-horizontal"))
                            {
                                DlkSlider sliderControl = new DlkSlider(value, element);
                                sliderControl.Set(value);
                            }
                            break;
                        }
                    default :
                        break;
                }
            }
            else
            {
                throw new Exception("Element with text : " + value + " not found.");
            }
        }

        private IWebElement getScoringElement(string value)
        {
            //check first for radio type scoring element.
            IWebElement element = getScoreControlWithText(value);
            List<string> ratersMapping = new List<string>() 
            {
                SLIDER_RATER,
                ALL_OR_NOTHING_RATER,
                NUMERIC_RATER
            };

            if (element == null)
            {
                IList<IWebElement> controls = null;
                for (int i = 0; i < ratersMapping.Count; i++ )
                {
                    controls = mElement.FindElements(By.XPath(ratersMapping[i]));
                    if (controls.Count > 0)
                    {
                        element = controls[0];
                        break;
                    }
                }
            }

            return element;
        }

        public void SelectCommentPermission(string value)
        { 
            IWebElement commentPermissionElement = mElement.FindElement(By.XPath(COMMENT_PERMISSION));
            DlkComboBox commentPermissionControl = new DlkComboBox("Comment_Permission", commentPermissionElement);
            commentPermissionControl.Select(value);
        }

        public void SetComment(string value)
        {
            IList<IWebElement> commentElement = mElement.FindElements(By.XPath(COMMENT));
            if (commentElement.Count > 0)
            {
                DlkTextBox commentControl = new DlkTextBox("Comment_Permission", commentElement[0]);
                commentControl.Set(value);
            }
            else
            {
                commentElement = mElement.FindElements(By.XPath(".//iframe[@class='cke_wysiwyg_frame cke_reset']"));
                DlkRichTextEditor editorControl = new DlkRichTextEditor("Comment_Permission", commentElement[0]);
                editorControl.Set(value);
            }
        }

        public void VerifyCommentState(bool trueOrFalse)
        {
            bool actualResult = false;
            string disabled = string.Empty;
            IList<IWebElement> commentElement = mElement.FindElements(By.XPath(COMMENT));
            if (commentElement.Count > 0)
            {
                DlkTextBox commentControl = new DlkTextBox("Comment_Permission", commentElement[0]);
                disabled = commentControl.GetAttributeValue("disable");
            }
            else
            {
                commentElement = mElement.FindElements(By.XPath(".//iframe[@class='cke_wysiwyg_frame cke_reset']"));
                DlkRichTextEditor editorControl = new DlkRichTextEditor("Comment_Permission", commentElement[0]);
                disabled = editorControl.GetAttributeValue("disable");
            }


            if (disabled != null &&
                disabled.Trim() == "true")
            {
                actualResult = false;
            }
            else
            {
                actualResult = true;
            }

            DlkAssert.AssertEqual("VerifyCommentState : ", trueOrFalse, actualResult);
        }

        public void VerifyCommentPermissionState(string trueOrFalse)
        {
            IWebElement commentPermissionElement = mElement.FindElement(By.XPath(COMMENT_PERMISSION));
            DlkComboBox commentPermissionControl = new DlkComboBox("Comment_Permission", commentPermissionElement);
            commentPermissionControl.VerifyState(trueOrFalse);
        }

        public void VerifyControlByTitleExists(string title, bool expectedResult)
        {
            bool actualResult = false;
            IList<IWebElement> elements = getControlWithText(title);
            if (elements.Count > 0)
            {
                actualResult = true;
            }
            DlkAssert.AssertEqual("VerifyControlByTitleExists : ", expectedResult, actualResult);
        }
        #endregion

        #region Keywords My Appraisals

        public void MenuButtonSelectByTitle(string title)
        {
            IWebElement menuButtonElement = mElement.FindElement(By.XPath(MENU_BUTTON));
            DlkButtonGroup buttonGroupControl = new DlkButtonGroup("Menu_Button_Group", menuButtonElement);
            buttonGroupControl.SelectByTitle(title);
        }

        public void MenuButtonSelectByIndex(string index)
        {
            IWebElement menuButtonElement = mElement.FindElement(By.XPath(MENU_BUTTON));
            DlkButtonGroup buttonGroupControl = new DlkButtonGroup("Menu_Button_Group", menuButtonElement);
            buttonGroupControl.SelectByIndex(index);
        }

        public void ClickAddJournalEntry()
        {
            IWebElement addJournalEntry = mElement.FindElement(By.XPath(ADD_JOURNAL_ENTRY));
            DlkButton buttonControl = new DlkButton("Add Journal Entry", addJournalEntry);
            buttonControl.Click();
            //addJournalEntry.SendKeys(Keys.Enter);
        }

        public void ClickDownloadIcon()
        {
            IWebElement downloadIcon = mElement.FindElement(By.XPath(DOWNLOAD_ICON));
            DlkBaseControl downloadControl = new DlkBaseControl("Download_Control", downloadIcon);
            downloadControl.ScrollIntoViewUsingJavaScript();
            downloadControl.ClickUsingJavaScript();
        }

        public void PerformTableRowActionByColumn(string tableRow, string column, string menuAction)
        {
            IWebElement tableElement = mElement.FindElement(By.XPath(TABLE));
            DlkTable tableControl = new DlkTable("Table", tableElement);
            tableControl.PerformRowActionByColumn(tableRow, column, menuAction);
        }

        #endregion

        #region Methods

        //Score Appraisals
        private IWebElement getScoreControlWithText(string title)
        {
            IList<IWebElement> elements = mElement.FindElements(By.XPath(".//*[contains(text(),'" + title + "')]/*"));

            if (elements.Count > 0)
            {
                return elements[0];
            }
            else
            {
                elements = DlkCommon.DlkCommonFunction.GetElementWithText(title, mElement, false, "label");
                if (elements.Count > 0)
                {
                    elements = elements[0].FindElements(By.XPath(@"./..//input"));
                    if (elements.Count > 0)
                    {
                        return elements[0];
                    }
                }

            }

            return null;
        }

        private IList<IWebElement> getControlWithText(string title)
        {
            IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(title, mElement, true);
            return elements;
        }

        #endregion
    }
}
