using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SBCLib.DlkControls
{
    [ControlType("DivisionAccordion")]
    public class DlkDivisionAccordion : DlkBaseControl
    {
        #region Constructors
        public DlkDivisionAccordion(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkDivisionAccordion(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDivisionAccordion(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region DECLARATIONS
        private const string strHeadersXPath = "//div[contains(@class,'primaryWroflow-Container')]//div[contains(@class,'containerHeading')][not(contains(@class,'hide'))]";
        private const string strDivisionXpath = ".//div[@class='division']";
        private const string strSectionXpath = ".//div[@id='childContainerSectionColumn']";
        private IList<IWebElement> lstColHeaders;
        private IList<IWebElement> lstDivisions;
        private IList<IWebElement> lstSections;
        private string mCellType;
        #endregion

        public void Initialize()
        {
            FindElement();
            InitializeColumnHeaders();
            InitializeDivisions();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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

        /// <summary>
        /// Expands a division item
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("ExpandDivision", new String[] { "1|text|Expected Value|TRUE" })]
        public void ExpandDivision(String Item)
        {
            try
            {
                Initialize();
                IWebElement divItem = GetDivItem(Item);
                Boolean bCurrentValue = GetExpandedState(divItem);
                if (!bCurrentValue)
                {
                    divItem.FindElement(By.XPath(".//span[contains(@class,'divIcon')]")).Click();
                }
                else
                {
                    DlkLogger.LogInfo("Item is already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("ExpandDivision() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandDivision() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Collapses a division item
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("CollapseDivision", new String[] { "1|text|Expected Value|TRUE" })]
        public void CollapseDivision(String Item)
        {
            try
            {
                Initialize();
                IWebElement divItem = GetDivItem(Item);
                Boolean bCurrentValue = GetExpandedState(divItem);
                if (bCurrentValue)
                {
                    divItem.FindElement(By.XPath(".//span[contains(@class,'divIcon')]")).Click();
                }
                else
                {
                    DlkLogger.LogInfo("Item is already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("CollapseDivision() passed");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseDivision() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks an item given the row and column name
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="Row"></param>
        [Keyword("ClickItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickItem(String ColumnName, String Row)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Row, out int row)) throw new Exception($"Row: [{Row}] is not a valid integer input.");
                
                int fromCol = GetColIndexfromHeader(ColumnName);
                IWebElement childItem = GetColumnChildItem(fromCol, row - 1);
                new DlkBaseControl("Item", childItem).ScrollIntoViewUsingJavaScript();
                childItem.Click();
                DlkLogger.LogInfo("ClickItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItem() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickBySectionName", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickBySectionName(String ColumnName, String SectionName)
        {
            try
            {
                Initialize();

                var sections = mElement.FindElements(By.XPath(".//*[contains(@class, 'sectionColumn')]")).ToList();
                if (sections.Count == 0) throw new Exception("Sections not found.");

                var section = sections.Where(s => s.Text.Replace(System.Environment.NewLine, " ").Trim() == SectionName.Trim()).FirstOrDefault();
                if (section == null) throw new Exception($"Cannot find Section: {SectionName}.");

                var itemClass = ColumnName.Equals("Editing") ? "edit" : ColumnName.ToLower();

                var item = section.FindElements(By.XPath($"./following-sibling::*[contains(@class, '{itemClass}')]//a")).FirstOrDefault();
                if (item == null) throw new Exception($"Cannot find item in column: {ColumnName}.");

                item.Click();

                DlkLogger.LogInfo("ClickBySectionName() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickBySectionName() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemSelected")]
        public void VerifyItemSelected(String SectionNameOrIndex, String ColNameOrIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();

                var item = GetColumnChildItem(SectionNameOrIndex, ColNameOrIndex);

                bool isItemSelected = item.GetAttribute("class").Contains("selectedChildContainer");

                DlkAssert.AssertEqual("VerifyItemSelected(): ", Convert.ToBoolean(TrueOrFalse), isItemSelected);
                DlkLogger.LogInfo("VerifyItemSelected() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemSelected() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifySectionSelected")]
        public void VerifySectionSelected(String SectionNameOrIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();

                var item = GetSection(SectionNameOrIndex);

                bool isSectionSelected = item.GetAttribute("class").Contains("selectedChildContainer");

                DlkAssert.AssertEqual("VerifySectionSelected(): ", Convert.ToBoolean(TrueOrFalse), isSectionSelected);
                DlkLogger.LogInfo("VerifySectionSelected() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySectionSelected() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyItemText")]
        public void VerifyItemText(String SectionNameOrIndex, String ColNameOrIndex, String ExpectedText)
        {
            try
            {
                Initialize();

                var item = GetColumnChildItem(SectionNameOrIndex, ColNameOrIndex);

                var itemTextContainer = item.FindElements(By.XPath(".//a")).FirstOrDefault();
                if (itemTextContainer == null) throw new Exception("Cannot find text container.");

                var itemText = itemTextContainer.Text.Trim();

                DlkAssert.AssertEqual("VerifyItemText(): ", ExpectedText, itemText);
                DlkLogger.LogInfo("VerifyItemText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemText() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifySectionText")]
        public void VerifySectionText(String SectionNameOrIndex, String ExpectedText)
        {
            try
            {
                Initialize();

                var section = GetSection(SectionNameOrIndex);

                var sectionTextContainer = section.FindElements(By.XPath(".//*[contains(@class, 'sectionName')]")).FirstOrDefault();
                if (sectionTextContainer == null) throw new Exception("Cannot find text container.");

                var sectionText = sectionTextContainer.Text.Replace(System.Environment.NewLine, " ").Trim();

                DlkAssert.AssertEqual("VerifySectionText(): ", ExpectedText.Trim(), sectionText);
                DlkLogger.LogInfo("VerifySectionText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySectiontext() failed : " + e.Message, e);
            }

        }
        
        [Keyword("VerifyHeaders")]
        public void VerifyHeaders(String ExpectedHeaders)
        {
            try
            {
                Initialize();

                if (lstColHeaders.Count < 1) throw new Exception("No Header found.");

                var headerList = lstColHeaders.ToList();
                headerList.RemoveAt(0);
                
                var headerTextList = headerList.Select(header => header.Text.Replace(System.Environment.NewLine, " ").Trim()).ToList();
                var ActualHeaders = string.Join("~", headerTextList);

                DlkAssert.AssertEqual("VerifyHeaders(): ", ExpectedHeaders.Trim(), ActualHeaders.Trim());
                DlkLogger.LogInfo("VerifyHeaders() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaders() failed : " + e.Message, e);
            }

        }


        /// <summary>
        /// Clicks a link inside the item given the row and column name
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="Row"></param>
        [Keyword("ClickItemLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickItemLink(String ColumnName, String Row)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Row, out int row)) throw new Exception($"Row: [{Row}] is not a valid integer input.");

                int fromCol = GetColIndexfromHeader(ColumnName);
                IWebElement childItem = GetColumnChildItem(fromCol, row - 1);
                new DlkBaseControl("Item", childItem).ScrollIntoViewUsingJavaScript();
                new DlkLink("Link", childItem.FindElement(By.TagName("a"))).Click();             
                DlkLogger.LogInfo("ClickItemLink() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemLink() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks a section with a given text
        /// </summary>
        /// <param name="Text"></param>
        [Keyword("ClickSectionButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickSectionButton(String Text)
        {
            try
            {
                Initialize();
                InitializeSections();
                IWebElement sectionItem = GetSectionItem(Text);
                new DlkBaseControl("Item", sectionItem).ScrollIntoViewUsingJavaScript();
                sectionItem.FindElement(By.XPath(".//div[@class='after']")).Click();
                DlkLogger.LogInfo("ClickSectionButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickSectionButton() failed : " + e.Message, e);
            }
        }

        //COMMENTED: NOT WORKING ONGOING INVESTIGATION
        /// <summary>
        /// Collapses an accordion item
        /// </summary>
        /// <param name="Item"></param>
        //[Keyword("DragAndDropItem", new String[] { "1|text|Expected Value|TRUE" })]
        //public void DragAndDropItem(String ColumnName, String Row, String DropColumn)
        //{
        //    try
        //    {
        //        Initialize();
        //        if (!Int32.TryParse(Row, out int row)) throw new Exception($"Row: [{Row}] is not a valid integer input.");

        //        int fromCol = GetColIndexfromHeader(ColumnName);
        //        int targetCol = GetColIndexfromHeader(DropColumn);

        //        IWebElement childItem = GetColumnChildItem(fromCol + 1, row - 1);
        //        IWebElement targetItem = GetColumnChildItem(targetCol + 1, row - 1, true);
        //        if (mCellType != "draggable") throw new Exception($"Target item is not draggable.");
        //        new DlkBaseControl("TargetItem", childItem).ScrollIntoViewUsingJavaScript();
        //        //TODO: Try other ways to drag and drop
        //        //Perform drag and drop
        //        Actions mAction = new Actions(DlkEnvironment.AutoDriver);
        //        //mAction.DragAndDrop(childItem, ta).Build().Perform();
        //        //mAction.DragAndDropToOffset(childItem, targetItem.Location.X + (targetItem.Size.Width/2), targetItem.Location.Y);
        //        //mAction.ClickAndHold(childItem).MoveToElement(targetItem).Release().Build().Perform();
        //        IAction dragDrop = mAction.ClickAndHold(childItem)
        //               .MoveByOffset(-1, -1)
        //               .MoveByOffset(targetItem.Location.X +1 , targetItem.Location.Y + 1)
        //               .Release(targetItem)
        //               .Build();
        //        dragDrop.Perform();
        //        DlkLogger.LogInfo("DragAndDropItem() passed");

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("DragAndDropItem() failed : " + e.Message, e);
        //    }
        //}

        #endregion

        #region Private Methods
        private void InitializeColumnHeaders()
        {
            lstColHeaders = mElement.FindElements(By.XPath(strHeadersXPath));

        }
        private void InitializeDivisions()
        {
            lstDivisions = mElement.FindElements(By.XPath(strDivisionXpath));
        }
        private void InitializeSections()
        {
            lstSections = mElement.FindElements(By.XPath(strSectionXpath));
        }
        private int GetColIndexfromHeader(String Header)
        {
            int index = 0;
            if (lstColHeaders.Count <= 0) throw new Exception("No column headers found...");
            index = lstColHeaders.ToList().FindIndex(x => DlkString.RemoveCarriageReturn(x.Text).Trim(' ').Equals(Header));
            if (index < 0) throw new Exception($"Header [{Header}] not found...");
            return index;
        }

        private IWebElement GetDivItem(String Text)
        {
            if (lstDivisions == null) throw new Exception("No division items found...");
            IWebElement element = lstDivisions.Where(x => DlkString.RemoveCarriageReturn(x.Text).Trim(' ').Equals(Text)).FirstOrDefault();
            if (element == null) throw new Exception($"[{Text}] not found...");
            return element;
        }

        private IWebElement GetSectionItem(String Text)
        {
            if (lstSections == null) throw new Exception("No items found...");
            IWebElement element = lstSections.Where(x => DlkString.RemoveCarriageReturn(x.Text).Trim(' ').Equals(Text)).FirstOrDefault();
            if (element == null) throw new Exception($"[{Text}] not found...");
            return element;
        }

        private IWebElement GetColumnChildItem(int ColumnIndex, int Row)
        {
            IList<IWebElement> lstColumnChildren = mElement.FindElements(By.XPath($".//*[contains(@class, ' sectionColumn') and not(contains(@class, ' sectionColumnHeading'))]/following-sibling::*[{ColumnIndex}]")).ToList();
            if (lstColumnChildren.Count < 1 && lstColumnChildren.Count < Row) throw new Exception($"Cannot find item at column [{ColumnIndex}] row [{Row}].");

            IWebElement element = lstColumnChildren.ElementAt(Row);
            if (element == null) throw new Exception($"Item not found...");
            mCellType = element.GetAttribute("draggable").ToLower().Equals("true") ? "draggable" : "regular";
            return element;
        }

        /// <summary>
        /// Returns true if the accordion is in expanded state.
        /// </summary>
        private Boolean GetExpandedState(IWebElement Item)
        {
            Initialize();
            string classValue = Item.FindElement(By.XPath(".//p[contains(@class,'header')]")).GetAttribute("class");       
            Boolean bCurrentVal = classValue.ToLower().Contains("headeractive") ? true : false;
            string bState = bCurrentVal ? "expanded" : "collapsed";
            DlkLogger.LogInfo($"Division is in [{ bState }] state");
            return bCurrentVal;
        }

        private IWebElement GetColumnChildItem(String Section, String Column)
        {
            
            IWebElement item;

            bool isColumnIndex = Int32.TryParse(Column, out int columnIndex);

            var section = GetSection(Section);

            var items = section.FindElements(By.XPath("./following-sibling::*")).ToList();
            if (items.Count < 1) throw new Exception("Cannot find section items.");

            if (isColumnIndex)
            {
                if (columnIndex < 1) throw new Exception("columnIndex should be equal or greater than 1.");

                item = items.ElementAt(columnIndex - 1);
            }
            else
                item = items.ElementAt(GetColIndexfromHeader(Column) -1);

            if (item == null) throw new Exception("Cannot find SectionItem");

            return item;
        }

        private IWebElement GetSection(String Section)
        {
            IWebElement section;
            
            bool isSectionIndex = Int32.TryParse(Section, out int sectionIndex);
            if (isSectionIndex)
            {
                if (sectionIndex < 1) throw new Exception("sectionIndex should be equal or greater than 1.");

                var sections = GetSections();

                section = sections.ElementAt(sectionIndex - 1);
            }
            else
            {
                var sections = GetSections(true);

                var foundSelection = sections.Where(s => Regex.Replace(s.Text.Trim(), @"\s+", " ") == Section.Trim()).FirstOrDefault();
                if (foundSelection == null) throw new Exception($"Cannot find section with name [{Section}].");

                section = foundSelection.FindElements(By.XPath(".//ancestor::*[@id='childContainerSectionColumn']")).FirstOrDefault();
            }

            if (section == null) throw new Exception("Cannot find Section");
            return section;
        }

        private List<IWebElement> GetSections(bool isTextContainer = false)
        {
            List<IWebElement> sections;
            if (isTextContainer)
                sections = mElement.FindElements(By.XPath(".//*[contains(@class, 'sectionName')]")).ToList();
            else
                sections = mElement.FindElements(By.XPath(strSectionXpath)).ToList();

            if (sections.Count < 1) throw new Exception("No selections found.");
            return sections;
        }
        #endregion
    }
 }

