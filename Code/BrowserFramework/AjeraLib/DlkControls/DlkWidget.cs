using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using AjeraLib.DlkSystem;

namespace AjeraLib.DlkControls
{
    [ControlType("Widget")]
    public class DlkWidget : DlkAjeraBaseControl
    {

        #region DECLARATIONS
        private Boolean IsInit;
        private List<DlkBaseControl> mlstWidget;
        private int retryLimit = 3;
        #endregion

        #region CONSTRUCTORS 
        public DlkWidget(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkWidget(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkWidget(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkWidget(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkWidget(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector){}
        

        public void Initialize(string WidgetCaption ="")
        {
            if (!IsInit)
            {
                mlstWidget = new List<DlkBaseControl>();
                FindElement();
                FindWidget(WidgetCaption);
                IsInit = true;
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("VerifyExists", new String[] { "Unapproved Time | True"})]
        public void VerifyExists(String WidgetCaption, String IsTrueOrFalse)
        {

            bool bFound = false;
            String strActualWidgets = "";

            try
            {
                Initialize(WidgetCaption);
                foreach (DlkBaseControl widget in mlstWidget)
                {
                    DlkLogger.LogInfo(widget.GetValue());
                    strActualWidgets = strActualWidgets + widget.GetValue() + " ";
                    if (widget.GetValue().ToLower() == WidgetCaption.ToLower())
                    {
                        if (widget.Exists())
                        {
                            bFound = true;
                            break;
                        }
                    }
                }


                DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(IsTrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyWidgetSubtitle", new String[] { "Unapproved Time | Sample Subtitle" })]
        public void VerifyWidgetSubtitle(String WidgetCaption, String ExpectedSubtitle)
        {

            bool bFound = false;
            String strActualWidgets = "";

            try
            {
                Initialize(WidgetCaption);
                foreach (DlkBaseControl widget in mlstWidget)
                {
                    DlkLogger.LogInfo(widget.GetValue());
                    strActualWidgets = strActualWidgets + widget.GetValue() + " ";
                    if (widget.GetValue().ToLower() == WidgetCaption.ToLower())
                    {
                        if (widget.Exists())
                        {
                            bFound = true;
                            break;
                        }
                    }
                }
                if (bFound)
                {
                    //verify subtitle
                    DlkLabel subtitle = new DlkLabel("Subtitle", "XPath", mSearchValues[0] + "//div[@class='ax-widget-chrome-title' and contains(text(),'" + WidgetCaption + "')]/following-sibling::div[@class='ax-widget-chrome-subtext']");
                    var ActualResult = subtitle.GetValue();

                    if (ActualResult.Contains("\r\n"))
                    {
                        ActualResult = ActualResult.Replace("\r\n", "<br>");
                    }
                    DlkAssert.AssertEqual("VerifyWidgetSubtitle() : " + mControlName, ExpectedSubtitle, ActualResult);
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidgetSubtitle() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemHeight", new String[] { "Unapproved Time" })]
        public void SelectItemHeight(String WidgetCaption)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //verify subtitle
                    DlkLink height = new DlkLink("Height", "XPath", mSearchValues[0] + "//div[text()='"+ WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]/span[@id='height']");
                    if (height.Exists())
                    {
                        height.Click();
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Height link not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemHeight() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemHeight", new String[] { "Unapproved Time | 2" })]
        public void VerifyItemHeight(String WidgetCaption, String ExpectedHeight)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //verify subtitle
                    DlkLink height = new DlkLink("Height", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]");
                    var ActualResult = height.GetValue();

                    DlkAssert.AssertEqual("VerifyItemHeight() : " + mControlName, "Height: " + ExpectedHeight, ActualResult);
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemHeight() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectWidgetTableColumnHeader", new String[] { "Unapproved Time | Supervisor" })]
        public virtual void SelectWidgetTableColumnHeader(String WidgetCaption, String ColumnHeader)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        bool bFound;
                        IWebElement header;
                        table.FindHeader(ColumnHeader, out bFound, out header);
                        if (bFound == false)
                        {
                            throw new Exception("Column header " + ColumnHeader + " not found");
                        }
                        else
                        {
                            header.Click();
                            DlkLogger.LogInfo("Successfully executed SelectColumnHeader(): " + ColumnHeader);
                        }
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectWidgetTableColumnHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyIfColumnSelected", new String[] {"WidgetCaption|ColumnHeader|IsTrueOrFalse"})]
        public virtual void VerifyIfColumnSelected(String WidgetCaption, String ColumnHeader, String IsTrueOrFalse)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        bool bFound, headerIsSelected;
                        IWebElement header;
                        table.FindHeader(ColumnHeader, out bFound, out header);
                        if (bFound)
                        {
                            headerIsSelected = header.GetAttribute("class").ToLower().Contains("ax-axgrid-headercell-highlight");
                            DlkAssert.AssertEqual("VerifyIfColumnSelected() : " + mControlName, Convert.ToBoolean(IsTrueOrFalse), headerIsSelected);
                        }
                        else
                        {
                            throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                        }
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfColumnSelected() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyExistWidgetTableColumnHeader", new String[] { "Unapproved Time | Supervisor | True" })]
        public virtual void VerifyExistWidgetTableColumnHeader(String WidgetCaption, String ColumnHeader, String IsTrueOrFalse)
        {
            try
            {
               if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        bool bFound;
                        IWebElement header;
                        table.FindHeader(ColumnHeader, out bFound, out header);
                        DlkAssert.AssertEqual("VerifyExistsColumnHeader() : " + mControlName, Convert.ToBoolean(IsTrueOrFalse), bFound);
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistWidgetTableColumnHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyWidgetTableColumnCount", new String[] { "Unapproved Time | 10" })]
        public virtual void VerifyWidgetTableColumnCount(String WidgetCaption, String ExpectedColCount)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        Initialize();
                        DlkAssert.AssertEqual("VerifyWidgetTableColumnCount() : " + mControlName, Convert.ToInt32(ExpectedColCount), table.CountColumn());
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidgetTableColumnCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyWidgetTableRowCount", new String[] { "Unapproved Time | 10" })]
        public virtual void VerifyWidgetTableRowCount(String WidgetCaption, String ExpectedRowCount)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        Initialize();
                        DlkAssert.AssertEqual("VerifyWidgetTableRowCount() : " + mControlName, Convert.ToInt32(ExpectedRowCount), table.CountRow());
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidgetTableRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyWidgetTableCellValue", new String[] { "Unapproved Time | 10" })]
        public virtual void VerifyWidgetTableCellValue(String WidgetCaption, String Row, String Column, String ExpectedValue)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        Initialize();
                        DlkAssert.AssertEqual("VerifyWidgetTableCellValue() : " + mControlName, ExpectedValue, table.GetCellValue(Row, Column));
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidgetTableCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickWidgetTableCell", new String[] { "1|text|Column Header|Line*" })]
        public virtual void ClickWidgetTableCell(String WidgetCaption, String Row, String Column)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath_Display", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");                    

                    if (table.Exists())
                    {
                        IWebElement cell;
                        table.GetCell(out cell, Row, Column);
                        if (cell != null)
                        {
                            TryClick(cell);
                        }
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickWidgetTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickWidgetTableCellButton", new String[] { "1|text|Column Header|Line*" })]
        public virtual void ClickWidgetTableCellButton(String WidgetCaption, String Row, String Column)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath_Display", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");

                    if (table.Exists())
                    {
                        IWebElement cell;
                        table.GetCell(out cell, Row, Column);
                        if (cell != null)
                        {
                            IWebElement cellButton;

                            //checking if the cell is not yet clicked - the button is not yet displayed
                            if (cell.TagName.ToLower() != "img")
                            {
                                TryClick(cell);
                                Thread.Sleep(1000);
                                cellButton = cell.FindElement(By.XPath("./ancestor::*[contains(@class,'edit')]//span[img[contains(@src,'pencil.svg')]]"));
                            }
                            else
                            {
                                cellButton = cell;
                            }
                            TryClick(cellButton);
                        }
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickWidgetTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SetWidgetTableCell", new String[] { "1|text|Column Header|Line*" })]
        public virtual void SetWidgetTableCell(String WidgetCaption, String Row, String Column, String Value)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        IWebElement cell;
                        table.GetCell(out cell, Row, Column);

                        cell.Clear();
                        if (!string.IsNullOrEmpty(Value))
                        {
                            cell.SendKeys(Value);

                            if (cell.GetAttribute("value").ToLower() != Value.ToLower())
                            {
                                cell.Clear();
                                cell.SendKeys(Value);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetWidgetTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemFromWidgetTableCell", new String[] { "1|text|Column Header|Line*" })]
        public virtual void SelectItemFromWidgetTableCell(String WidgetCaption, String Row, String Column, String Value)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        IWebElement cell;
                        table.GetCell(out cell, Row, Column);
                        cell.Click();

                        IWebElement itemMenu;
                        IList<IWebElement> menuItems = null;
                        int currRetry = 0;
                        bool bFound = false ;
                        string actualItems = string.Empty;

                        //find popUp
                        itemMenu = mElement.FindElement(By.XPath("//*[contains(@class,'popupmenu-main')]"));
                        
                        //Look for items in the popupmenu/dropdown
                        menuItems = itemMenu.FindElements(By.CssSelector("div"));
                        while (++currRetry <= retryLimit && !bFound)
                        {
                            foreach (IWebElement aListItem in menuItems)
                            {
                                var dlkTreeItem = new DlkBaseControl("PopUp Item", aListItem);
                                if (currRetry <= 1)
                                {
                                    actualItems = actualItems + dlkTreeItem.GetValue() + " ";
                                }
                                if (dlkTreeItem.GetValue().ToLower() == Value.ToLower())
                                {
                                    dlkTreeItem.MouseOver();
                                    dlkTreeItem.Click();
                                    Thread.Sleep(1000);

                                    bFound = true;
                                    break;
                                }
                            }
                        }

                        if (!bFound)
                        {
                            throw new Exception(Value + " not found in list. : Actual List = " + actualItems);
                        }
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemFromWidgetTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignValueToVariable(String WidgetCaption, String Row, String Column, String VariableName)
        {
           try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find table
                    DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                    if (table.Exists())
                    {
                        IWebElement cell;
                        table.GetCell(out cell, Row, Column);
                        if (cell != null)
                        {
                            DlkFunctionHandler.AssignToVariable(VariableName, new DlkBaseControl("TableCell", cell).GetValue());
                            DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
                        };
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - Widget Table not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }

        }

        [Keyword("ClickWidgetToolbarButton", new String[] { "Manage Purchase Order|New" })]
        public void ClickWidgetToolbarButton(String WidgetCaption, String ButtonName)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    //find new in button set
                    //DlkButton button = new DlkButton("Button", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]/div[contains(@class,'toolbar')]//label[contains(.,'New')]");
                    DlkButton button = new DlkButton("Button", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]/div[contains(@class,'toolbar')]//label[contains(.,'"+ ButtonName +"')]");
                    
                    if (button.Exists())
                    {
                        button.Click();
                    }
                    else
                    {
                        throw new Exception(mControlName + " = '" + WidgetCaption + "' - " + ButtonName + " Button not found");
                    }
                }
                else
                {
                    throw new Exception(mControlName + " = '" + WidgetCaption + "' widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickWidgetToolbarButton() failed : " + e.Message, e);
            }

        }
        
        [Keyword("VerifyWidgetTableColumnWidth", new String[] { "WidgetCaption|Column|Width" })]
        public void VerifyWidgetTableColumnWidth(String WidgetCaption, String Column, String Width)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    IWebElement columnHeader = mElement.FindElement(By.XPath("./descendant::div[.='"+WidgetCaption+"']/../following-sibling::div/descendant::th[" + Column + "]"));
                    //string columnWidth = columnHeader.GetCssValue("width");       This returns width value in px, even if the width is not in px format (conversion takes place)
                    string columnStyleList = columnHeader.GetAttribute("style");
                    string[] columnStyles = columnStyleList.Split(new char[] { ';', ':' },StringSplitOptions.RemoveEmptyEntries);
                    var testGetIndex = columnStyles.Select((x, i) => new { x, i }).Where(x => x.x.ToLower().Trim() == "width").Select(x => x.i);
                    int widthIndex = Convert.ToInt32(testGetIndex.First()) + 1;
                    String ColumnWidth = Regex.Replace(columnStyles[widthIndex], "[^0-9]*$", "");

                    DlkAssert.AssertEqual("VerifyWidgetTableColumnWidth()", Width.Trim(), ColumnWidth.Trim());
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidgetTableColumnWidth() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword Clicks the search icon/ImageButton in a widget in the JEManage screen.
        /// </summary>
        /// <param name="WidgetCaption"></param>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        [Keyword("SearchFromWidgetTableCell", new String[] { "Non-zero Row Index | Non-zero Column Index | Expected Value" })]
        public void SearchFromWidgetTableCell(String WidgetCaption, String Row, String Column)
        {

            try
            {
                if (WidgetExists(WidgetCaption) && mElement != null)
                {
                    Thread.Sleep(2500);
                    /*
                     this is the xpath to find the search button (for future references):
                     ./div/div[descendant::div[text()='Journal Entry Detail']]/descendant::tr[contains(@class,'rowindex')][5]/td[2]/descendant::span[contains(@class,'imagebutton') and descendant::img[contains(@src,'search')]]
                     
                    */
                    var specificCell = mElement.FindElement(By.XPath(string.Format("./div/div[descendant::div[text()='{0}']]/descendant::tr[contains(@class,'rowindex')][{1}]/td[{2}]", WidgetCaption, Row,Column)));
                    specificCell.Click();
                    DlkLogger.LogInfo(string.Format("You clicked the cell at row {0} of tag <{1}>",Row,specificCell.TagName));
                    Thread.Sleep(2500);
                   
                    //this is because clicking the cell generates a loading screen,
                    //and we don't want to click the loading screen
                    //so we sleep and wait until the loading finishes
                   
                    if (!specificCell.FindElement(By.XPath("./descendant::span[contains(@class,'imagebutton') and descendant::img[contains(@src,'search')]]")).Displayed)
                    {
                        specificCell.Click();
                    }
                    var searchImageButton = specificCell.FindElement(By.XPath("./descendant::span[contains(@class,'imagebutton') and descendant::img[contains(@src,'search')]]"));

                    searchImageButton.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidgetTableColumnWidth() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellColor", new String[] { "WidgetCaption|Row|Column|Color" })]
        public void VerifyCellColor(String WidgetCaption, String Row, String Column, String CellColor)
        {
            try
            {
                if (WidgetExists(WidgetCaption))
                {
                    IWebElement columnHeader = mElement.FindElement(By.XPath("./descendant::div[.='" + WidgetCaption + "']/../following-sibling::div/descendant::table//tr[" + Row + "]/td[" + Column + "]"));

                    string actualCellColorRGB = columnHeader.GetCssValue("background-color");

                    int currRetry = 0;
                    int retryLimit = 10; //first try for td, second try for tr
                    string defaultcolor = "rgba(0, 0, 0, 0)";

                    while (++currRetry <= retryLimit && ((actualCellColorRGB == defaultcolor) || actualCellColorRGB.ToLower() == "transparent"))
                    {   
                        //Check if the cell has specific style. If none, get the row style.
                        //style is undefined for Chrome/FF but empty for IE
                        if (DlkEnvironment.mBrowser.ToLower() == "ie")
                        {
                            string style = columnHeader.FindElement(By.XPath(".//div[1]")).GetAttribute("style");
                            columnHeader = !String.IsNullOrWhiteSpace(style) ? columnHeader.FindElement(By.XPath(".//div[1]")) : columnHeader.FindElement(By.XPath("./.."));
                        }
                        else
                        {
                            columnHeader = columnHeader.FindElements(By.XPath(".//div[not(contains(@style,'undefined'))]")).Count > 0 ? columnHeader.FindElement(By.XPath(".//div[1]")) : columnHeader.FindElement(By.XPath("./.."));
                        }
                        actualCellColorRGB = columnHeader.GetCssValue("background-color");
                    }
                    //the format of color returned by the previous line(RGB format) is not the same as the format entered by the user while editing (HTML Format)
                    //because of this, there is a need to convert the color to HTML Format
                    String[] rgbValues = actualCellColorRGB.Replace("rgba(", "").Replace(")", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    int r = Int32.Parse(rgbValues[0].Trim());
                    int g = Int32.Parse(rgbValues[1].Trim());
                    int b = Int32.Parse(rgbValues[2].Trim());
                    String actualCellColor = ConvertColor_RGBtoHTML(r, g, b);

                    DlkAssert.AssertEqual("VerifyCellColor()", CellColor.ToLower(), actualCellColor.ToLower());
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellColor() failed : " + e.Message, e);
            }

        }
        #endregion

        #region METHODS
        public void FindWidget(string WidgetCaption)
        {
            string widgetContainer = string.Empty;
            IList<IWebElement> lstWidgetElements;
            if (mSearchValues[0].ToLower().Contains("blank-widget"))
            {
                widgetContainer = ".//div[contains(@class,'chrome')]/div[contains(text(),'" + WidgetCaption + "')]";
            }
            else
            {
                widgetContainer = "//div[contains(@class,'ax-widget-chrome-title')]";
            }

            lstWidgetElements = mElement.FindElements(By.XPath(widgetContainer));
            foreach (IWebElement widgetElement in lstWidgetElements.Where(item => item.Text != ""))
            {
                mlstWidget.Add(new DlkBaseControl("Widget", widgetElement));
            }
        }

        public bool WidgetExists(string WidgetCaption)
        {
            String strActualWidgets = "";
            Initialize(WidgetCaption);
            foreach (DlkBaseControl widget in mlstWidget)
            {
                DlkLogger.LogInfo(widget.GetValue());
                strActualWidgets = strActualWidgets + widget.GetValue() + " ";
                if (widget.GetValue().ToLower() == WidgetCaption.ToLower().Trim())
                {
                    if (widget.Exists())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void TryClick(IWebElement element)
        {
            bool clicked = false;
            int currRetry = 0;
            int retryLimit = 3;

            while (++currRetry <= retryLimit && !clicked)
            try
            {
                var m = new DlkBaseControl("TableCell", element);
                m.ScrollIntoViewUsingJavaScript();
                m.Click();
          
                clicked = true;
            }
            catch (WebDriverException)
            {
                ScrollToPositionUsingJavaScript(element, 0, -350);
                if(currRetry >= retryLimit)
                    throw new Exception("TryClick() failed. ");
            }
            catch (Exception)
            {
                throw new Exception("TryClick() failed. ");
            }
        }

        #endregion
    }
}
