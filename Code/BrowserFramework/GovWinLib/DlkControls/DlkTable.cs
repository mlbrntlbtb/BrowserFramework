using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.Office.Interop.Excel;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using GovWinLib.DlkUtility;
using System.IO;
using System.Diagnostics;

namespace GovWinLib.DlkControls
{
    public class DlkRow
    {
        public int RowIndex;
        public List<DlkCell> lstCells;

        public DlkRow(int Row)
        {
            RowIndex = Row;
            lstCells = new List<DlkCell>();
        }

        public void AddCell(DlkCell cell)
        {
            lstCells.Add(cell);
        }

    }

    public class DlkHeaderGroup
    {
        public List<DlkHeader> lstHeaders;
        public int HeaderGroupIndex;

        public DlkHeaderGroup(int headerGroupIndex)
        {
            lstHeaders = new List<DlkHeader>();
            HeaderGroupIndex = headerGroupIndex;
        }

        public void AddHeader(String strTableType, IWebElement header, int headerIndex)
        {
            switch (strTableType)
            {
                case "formtable":
                case "datatable":
                case "gridstyleone":
                case "gridstyletwo":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    lstHeaders.Add(new DlkHeader(header, headerIndex));
                    break;

                default:
                    throw new Exception("AddHeader() failed. Table of '" + strTableType + "' type not supported.");
            }
        }
    }

    public class DlkHeader
    {
        public String HeaderText;
        public IWebElement element;
        public int HeaderIndex;

        public DlkHeader(IWebElement elm, int headerIndex)
        {
            if (elm != null)
            {
                DlkBaseControl headerControl = new DlkBaseControl("Header", elm);
                HeaderText = headerControl.GetValue().Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
                if (HeaderText != " ")
                {
                    HeaderText = HeaderText.Trim();
                }
            }
            else
            {
                //HeaderText = "";
                int strHeaderInt = headerIndex + 1;
                HeaderText = strHeaderInt.ToString();
            }
            element = elm;
            HeaderIndex = headerIndex;
        }

        public void Click()
        {
            DlkBaseControl headerControl = new DlkBaseControl("Header", element);
            headerControl.Click();
        }
    }

    public class DlkCell
    {
        public int row;
        public IWebElement element;
        public DlkHeader header;

        public DlkCell(IWebElement cellElement, int RowIndex, DlkHeader header)
        {
            row = RowIndex;
            element = cellElement;
            this.header = header;
        }

        public DlkCell(IWebElement cellElement, int RowIndex)
        {
            row = RowIndex;
            element = cellElement;
        }

        public void ClickRadioButton(String strTableType)
        {
            IList<IWebElement> buttons;
            switch (strTableType)
            {
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "panelBody flush subrowstable ui-sortable":
                    buttons = element.FindElements(By.CssSelector("input[type='radio']"));
                    if (buttons.Count > 0)
                    {
                        DlkRadioButton cellButton = new DlkRadioButton("Cell Radio Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickRadioButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a radio button.");
                    }

                    break;
                case "gritter-item":
                case "gritter-without-image":
                    buttons = element.FindElements(By.XPath("//a"));
                    if (buttons.Count > 0)
                    {
                        DlkLink cellButton = new DlkLink("Cell Radio Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickRadioButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a radio button.");
                    }
                    break;

                case "formtable":
                    IList<IWebElement> actionCell = element.FindElements(By.XPath("./following-sibling::td[@class='actions']"));
                    if (actionCell.Count > 0)
                    {
                        buttons = actionCell.First().FindElements(By.CssSelector("input[type='radio']"));
                    }
                    else
                    {
                        buttons = element.FindElements(By.CssSelector("input[type='radio']"));
                    }
                    if (buttons.Count > 0)
                    {
                        DlkButton cellButton = new DlkButton("Cell Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickRadioButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a radio button.");
                    }
                    break;
                default:
                    throw new Exception("ClickRadioButton() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void ClickButton(String strTableType)
        {
            IList<IWebElement> buttons;
            switch (strTableType)
            {
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "panelBody flush subrowstable ui-sortable":
                    buttons = element.FindElements(By.CssSelector("input[type='button']"));
                    if (buttons.Count > 0)
                    {
                        DlkButton cellButton = new DlkButton("Cell Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }

                    break;
                case "gritter-item":
                case "gritter-without-image":
                    buttons = element.FindElements(By.XPath("//a"));
                    if (buttons.Count > 0)
                    {
                        DlkLink cellButton = new DlkLink("Cell Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }
                    break;

                case "formtable":
                    IList<IWebElement> actionCell = element.FindElements(By.XPath("./following-sibling::td[@class='actions']"));
                    if (actionCell.Count > 0)
                    {
                        buttons = actionCell.First().FindElements(By.CssSelector("input[type='button']"));
                    }
                    else
                    {
                        buttons = element.FindElements(By.CssSelector("input[type='button']"));
                    }
                    if (buttons.Count > 0)
                    {
                        DlkButton cellButton = new DlkButton("Cell Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }
                    break;
                default:
                    throw new Exception("ClickButton() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void SelectMark(String strTableType, string expectedValue)
        {
            IList<IWebElement> marks;
            string selectorValue = string.Format("./div[contains(@class,'markWidget')]|.//div[@class='markingWidgetContainer']//span[contains(@class,'markingWidgetLevelSelect')]");
            switch (strTableType)
            {
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    marks = element.FindElements(By.XPath(selectorValue));
                    if (marks.Count > 0)
                    {
                        DlkMark cellMark = new DlkMark("Cell Mark", marks.First());
                        cellMark.Select(expectedValue);
                    }
                    else
                    {
                        throw new Exception("SelectMark() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a mark.");
                    }

                    break;
                case "formtable":
                    IList<IWebElement> actionCell = element.FindElements(By.XPath("./div[contains(@class,'markWidget')]"));
                    if (actionCell.Count > 0)
                    {
                        marks = actionCell.First().FindElements(By.XPath(selectorValue));
                    }
                    else
                    {
                        marks = element.FindElements(By.XPath(selectorValue));
                    }
                    if (marks.Count > 0)
                    {
                        DlkButton cellButton = new DlkButton("Cell Mark", marks.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("SelectMark() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a mark.");
                    }
                    break;
                default:
                    throw new Exception("SelectMark() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void ClickButton(String strTableType, string expectedType)
        {
            IList<IWebElement> buttons;
            string selectorValue = string.Format("input[type='{0}']", expectedType);
            switch (strTableType)
            {
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    buttons = element.FindElements(By.CssSelector(selectorValue));
                    if (buttons.Count > 0)
                    {
                        DlkButton cellButton = new DlkButton("Cell Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }

                    break;
                case "formtable":
                    IList<IWebElement> actionCell = element.FindElements(By.XPath("./following-sibling::td[@class='actions']"));
                    if (actionCell.Count > 0)
                    {
                        buttons = actionCell.First().FindElements(By.CssSelector(selectorValue));
                    }
                    else
                    {
                        buttons = element.FindElements(By.CssSelector(selectorValue));
                    }
                    if (buttons.Count > 0)
                    {
                        DlkButton cellButton = new DlkButton("Cell Button", buttons.First());
                        cellButton.Click();
                    }
                    else
                    {
                        throw new Exception("ClickButton() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }
                    break;
                default:
                    throw new Exception("ClickButton() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void ClickButtonWithText(String strTableType, String sText)
        {
            IList<IWebElement> buttons;
            switch (strTableType)
            {
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    buttons = element.FindElements(By.CssSelector("input[type='button']"));
                    if (buttons.Count > 0)
                    {
                        Boolean bFound = false;
                        foreach (IWebElement buttonElement in buttons)
                        {
                            DlkButton cellButton = new DlkButton("Cell Button", buttonElement);
                            if (cellButton.GetAttributeValue("value") == sText)
                            {
                                bFound = true;
                                cellButton.Click();
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("ClickButtonWithText() failed. Button with text '" + sText + "' not found.");
                        }
                    }
                    else
                    {
                        throw new Exception("ClickButtonWithText() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }

                    break;
                case "formtable":
                    IList<IWebElement> actionCell = element.FindElements(By.XPath("./following-sibling::td[@class='actions']"));
                    if (actionCell.Count > 0)
                    {
                        buttons = actionCell.First().FindElements(By.CssSelector("input[type='button']"));
                    }
                    else
                    {
                        buttons = element.FindElements(By.CssSelector("input[type='button']"));
                    }
                    if (buttons.Count > 0)
                    {
                        Boolean bFound = false;
                        foreach (IWebElement buttonElement in buttons)
                        {
                            DlkButton cellButton = new DlkButton("Cell Button", buttonElement);
                            if (cellButton.GetAttributeValue("value") == sText)
                            {
                                bFound = true;
                                cellButton.Click();
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("ClickButtonWithText() failed. Button with text '" + sText + "' not found.");
                        }
                    }
                    else
                    {
                        throw new Exception("ClickButtonWithText() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a button.");
                    }
                    break;
                default:
                    throw new Exception("ClickButtonWithText() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public bool VerifyButtonWithText(String strTableType, String sText)
        {
            bool bExist = false;

            IList<IWebElement> buttons;
            switch (strTableType)
            {
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    buttons = element.FindElements(By.CssSelector("input[type='button']"));
                    if (buttons.Count > 0)
                    {
                        foreach (IWebElement buttonElement in buttons)
                        {
                            DlkButton cellButton = new DlkButton("Cell Button", buttonElement);
                            if (cellButton.GetAttributeValue("value") == sText)
                            {
                                bExist = true;
                            }
                        }

                    }

                    break;
                case "formtable":
                    IList<IWebElement> actionCell = element.FindElements(By.XPath("./following-sibling::td[@class='actions']"));
                    if (actionCell.Count > 0)
                    {
                        buttons = actionCell.First().FindElements(By.CssSelector("input[type='button']"));
                    }
                    else
                    {
                        buttons = element.FindElements(By.CssSelector("input[type='button']"));
                    }
                    if (buttons.Count > 0)
                    {
                        bExist = false;
                        foreach (IWebElement buttonElement in buttons)
                        {
                            DlkButton cellButton = new DlkButton("Cell Button", buttonElement);
                            if (cellButton.GetAttributeValue("value") == sText)
                            {
                                bExist = true;
                                break;
                            }
                        }
                    }

                    break;
                default:
                    break;
            }

            return bExist;
        }

        public void ClickLink(String strTableType)
        {
            switch (strTableType)
            {
                case "formtable":
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    DlkLogger.LogInfo("cell link = '" + GetValue(strTableType) + "'");
                    IList<IWebElement> links = element.FindElements(By.CssSelector("a"));
                    if (links.Count > 0)
                    {
                        DlkLink cellLink;
                        if(links.First().Text.Trim().Equals(""))
                            cellLink = new DlkLink("Cell Link", links[1]);
                        else
                            cellLink = new DlkLink("Cell Link", links.First());
                        cellLink.Click();
                    }
                    else
                    {
                        throw new Exception("ClickLink() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a link.");
                    }

                    break;
                default:
                    throw new Exception("ClickLink() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void ClickLink(String strTableType, String strTitle)
        {
            switch (strTableType)
            {
                case "formtable":
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    Boolean bFound = false;
                    IList<IWebElement> links = element.FindElements(By.CssSelector("a"));
                    if (links.Count > 0)
                    {
                        for (int i = 0; i < links.Count; i++)
                        {
                            DlkLogger.LogInfo("Title=" + links[i].GetAttribute("title").ToLower() + " sTitle=" + strTitle);
                            if (links[i].GetAttribute("title").ToLower().Contains(strTitle.ToLower()))
                            {
                                DlkLink cellLink = new DlkLink("Cell Link", links[i]);
                                cellLink.Click();
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("ClickLink() failed. Link with title '" + strTitle + "' not found in Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "'.");
                        }
                    }
                    else
                    {
                        throw new Exception("ClickLink() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a link.");
                    }

                    break;
                default:
                    throw new Exception("ClickLink() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void ClickLinkWithText(String strTableType, String strText)
        {
            switch (strTableType)
            {
                case "formtable":
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    Boolean bFound = false;
                    IList<IWebElement> links = element.FindElements(By.CssSelector("a"));
                    if (links.Count > 0)
                    {
                        for (int i = 0; i < links.Count; i++)
                        {
                            DlkLink cellLink = new DlkLink("Cell Link", links[i]);
                            if (cellLink.GetValue().ToLower().Contains(strText.ToLower()))
                            {
                                cellLink.Click();
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("ClickLink() failed. Link with text '" + strText + "' not found in cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "'.");
                        }
                    }
                    else
                    {
                        throw new Exception("ClickLink() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a link.");
                    }

                    break;
                default:
                    throw new Exception("ClickLink() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public void ClickComboBoxWithText(String strTableType, String strText)
        {
            switch (strTableType)
            {
                case "formtable":
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    Boolean bFound = false;
                    IList<IWebElement> cmb = element.FindElements(By.CssSelector("select"));
                    if (cmb.Count > 0)
                    {
                        for (int i = 0; i < cmb.Count; i++)
                        {
                            DlkComboBox cellCmb = new DlkComboBox("Cell ComboBox", cmb[i]);
                            if (cellCmb.ifListContains(strText.ToLower(), "TRUE"))
                            {
                                cellCmb.Select(strText);
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("ClickComboBoxWithText() failed. Link with text '" + strText + "' not found in cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "'.");
                        }
                    }
                    else
                    {
                        throw new Exception("ClickComboBoxWithText() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a link.");
                    }

                    break;
                default:
                    throw new Exception("ClickComboBoxWithText() failed. Table of '" + strTableType + "' type not supported.");
            }
        }

        public String GetValue(String strTableType)
        {
            String value = "";
            DlkBaseControl cell;
            switch (strTableType)
            {
                case "formtable":
                    cell = new DlkBaseControl("Cell", element);
                    value = TrimValue(cell.GetValue());
                    break;
                case "gridtable":
                case "gritter-item":
                case "gritter-without-image":
                case "panelBody flush subrowstable ui-sortable":
                    if (element != null)
                    {
                        cell = new DlkBaseControl("Cell", element);
                        value = TrimValue(cell.GetValue());

                    }
                    else
                    {
                        value = "";
                    }
                    break;
                case "datatable":
                case "gridstyletwo":
                case "gridstyleone":
                    //check if cell contains checkbox
                    IList<IWebElement> lstCheckBox = element.FindElements(By.CssSelector("input[type='checkbox']"));
                    IList<IWebElement> lstComboBox = element.FindElements(By.CssSelector("select"));
                    if (lstCheckBox.Count > 0)
                    {
                        DlkCheckBox chkCellCheckBox = new DlkCheckBox("Cell CheckBox", lstCheckBox.First());
                        value = Convert.ToString(chkCellCheckBox.GetCheckedState());
                    }
                    if (lstComboBox.Count > 0)
                    {
                        SelectElement comboBoxElement = new SelectElement(lstComboBox.First());
                        DlkBaseControl selectedOption = new DlkBaseControl("Selected Option", comboBoxElement.SelectedOption);
                        value = selectedOption.GetValue();
                    }
                    else //cell does not contain any checkboxes
                    {
                        cell = new DlkBaseControl("Cell", element);
                        value = TrimValue(cell.GetValue());
                        if (value == " ")
                        {
                            value = "";
                        }
                    }
                    break;

                default:
                    throw new Exception("GetValue() failed. Table of '" + strTableType + "' type not supported.");
            }

            return value;
        }

        public void SelectComboBoxValue(String sValue)
        {
            IList<IWebElement> selectElement = element.FindElements(By.XPath(".//select"));
            if (selectElement.Count > 0)
            {
                DlkBaseControl selectControl = new DlkBaseControl("Select", selectElement.First());
                String selectType = selectControl.GetAttributeValue("type");
                switch (selectType.ToLower())
                {
                    case "select-one":
                        DlkComboBox cboSelect = new DlkComboBox("ComboBox", selectControl.mElement);
                        cboSelect.Select(sValue);
                        break;
                    case "select-multiple":
                        DlkList lstSelect = new DlkList("ListBox", selectControl.mElement);
                        lstSelect.SelectMultiple(sValue);
                        break;
                    default:
                        throw new Exception("SetValue() failed. Unsupported select type '" + selectType + "'");
                }
            }
        }

        public void VerifyComboBoxListContains(String sValue)
        {
            IList<IWebElement> selectElement = element.FindElements(By.XPath(".//select"));
            if (selectElement.Count > 0)
            {
                DlkComboBox selectControl = new DlkComboBox("Select", selectElement.First());
                selectControl.VerifyListContains(sValue, true.ToString());
            }
        }

        public void SetValue(String strTableType, String sValue)
        {
            switch (strTableType)
            {
                case "datatable":
                    IList<IWebElement> inputs = element.FindElements(By.XPath(".//input[not(@type='hidden')]"));
                    if (inputs.Count > 0)
                    {
                        DlkBaseControl inputControl = new DlkBaseControl("Input", inputs.First());
                        String inputType = inputControl.GetAttributeValue("type");
                        switch (inputType)
                        {
                            case "checkbox":
                                DlkCheckBox chkInput = new DlkCheckBox("Input Checkbox", inputControl.mElement);
                                chkInput.Set(sValue);
                                break;
                            case "text":
                                DlkTextBox txtInput = new DlkTextBox("Input Textbox", inputControl.mElement);
                                txtInput.Set(sValue);
                                break;
                            default:
                                throw new Exception("SetValue() failed. Input control of type '" + inputType + "' is not supported.");
                        }
                    }
                    break;
                case "formtable":
                    IList<IWebElement> selectElement = element.FindElements(By.XPath(".//select"));
                    if (selectElement.Count > 0)
                    {
                        DlkBaseControl selectControl = new DlkBaseControl("Select", selectElement.First());
                        String selectType = selectControl.GetAttributeValue("type");
                        switch (selectType.ToLower())
                        {
                            case "select-one":
                                DlkComboBox cboSelect = new DlkComboBox("ComboBox", selectControl.mElement);
                                cboSelect.Select(sValue);
                                break;
                            case "select-multiple":
                                DlkList lstSelect = new DlkList("ListBox", selectControl.mElement);
                                lstSelect.SelectMultiple(sValue);
                                break;
                            default:
                                throw new Exception("SetValue() failed. Unsupported select type '" + selectType + "'");
                        }
                    }
                    else
                    {
                        IList<IWebElement> inputElements = element.FindElements(By.XPath(".//input[not(@type='hidden')][not(@type='button')]"));
                        if (inputElements.Count > 0)
                        {
                            DlkBaseControl inputControl = new DlkBaseControl("Input", inputElements.First());
                            String inputType = inputControl.GetAttributeValue("type");
                            switch (inputType)
                            {
                                case "checkbox":
                                    DlkCheckBox chkInput = new DlkCheckBox("Input Checkbox", inputControl.mElement);
                                    chkInput.Set(sValue);
                                    break;
                                case "text":
                                    DlkTextBox txtInput = new DlkTextBox("Input Textbox", inputControl.mElement);
                                    txtInput.Set(sValue);
                                    break;
                                default:
                                    throw new Exception("SetValue() failed. Input control of type '" + inputType + "' is not supported.");
                            }
                        }
                        else
                        {
                            IList<IWebElement> textareaElements = element.FindElements(By.XPath(".//textarea"));
                            if (textareaElements.Count > 0)
                            {
                                DlkTextArea textareaControl = new DlkTextArea("TextArea", textareaElements.First());
                                textareaControl.Set(sValue);
                            }
                            else
                            {
                                throw new Exception("SetValue() failed. No input controls found.");
                            }

                        }
                    }
                    break;
                default:
                    throw new Exception("SetValue() failed. Table of '" + strTableType + "' type not supported.");
            }

        }

        private String TrimValue(String sValue)
        {
            sValue = sValue.Replace("\n", " ");
            sValue = sValue.Replace("\r", " ");
            while (sValue.Contains("  "))
            {
                sValue = sValue.Replace("  ", " ");
            }
            return sValue.Trim();
        }
    }

    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        protected const string SCRIPT_SHEET_NAME = "Script";
        protected const string SCRIPT_OUTPUT_COLUMN_NAME = "Output";

        protected Boolean IsInitialized = false;

        protected String mstrTableType = "";
        protected List<DlkRow> mlstRows;
        protected Dictionary<int, List<DlkRow>> mDicSubRows;
        protected Dictionary<int, List<DlkRow>> mDicSubRowsExpandable;
        protected List<DlkHeaderGroup> mlstHeaderGroups;
        protected DlkProcessing mProcessing = null;

        public List<DlkHeaderGroup> HeaderGroups
        {
            get { return this.mlstHeaderGroups; }
        }

        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String SearchValue, DlkProcessing processing)
            : base(ControlName, SearchType, SearchValue) { mProcessing = processing; }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }


        protected virtual void Initialize()
        {
            mlstRows = new List<DlkRow>();
            mlstHeaderGroups = new List<DlkHeaderGroup>();
            if (mProcessing != null)
                mProcessing.WaitProcessing();

            RefreshTableData();
        }


        public virtual bool checkContentIsEmpty(int iRow, String sColumnHeader)
        {
            bool bResult = false;

            Initialize();

            DlkCell currentCell = null;
            foreach (DlkCell cell in mlstRows[iRow].lstCells)
            {
                if (cell.header.HeaderText.ToLower() == sColumnHeader.ToLower())
                {
                    currentCell = cell;
                    if (currentCell.GetValue(mstrTableType).Equals(""))
                    {
                        return true;
                    }

                    break;
                }
            }

            return bResult;
        }


        [Keyword("CompareTableToCSV", new String[] { "1|text|outputFile" })]
        public void CompareTableToCSV(string outputFile)
        {
            FindElement();

            string tableContentsXpath = "//*[@id='gridViewTable_wrapper']//tbody/tr/td",
                   nextPaginationXpath = "//div[@class='leftActions']//*[@class='next paginate_button']",
                   searchResultsTotalXpath = "//*[@id='searchResultsTotalCount']";

            var totalSearchResultElement = mElement.FindElement(By.XPath(searchResultsTotalXpath)).Text;

            int maxDisplayedNumberOfRecordsPerPage = 50,
                currentlyDisplayedNumberOfRecords = mElement.FindElements(By.XPath(tableContentsXpath)).Count(),
                headerCount = GetAllHeaders().Split(',').Length, // Identifies how many columns per row
                totalNumberOfDisplayedRows = (currentlyDisplayedNumberOfRecords / headerCount);

            var parsedCsv = File.ReadLines(Path.Combine(DlkEnvironment.mDirData + outputFile))
                                .Select((Text, Index) => new { Text = Text.Trim(), Index }).ToList();

            // If searchResultElement has int value
            if (int.TryParse(totalSearchResultElement, out int totalSearchResults))
            {
                // Divide total search result to 50 (since it displays 50 records per page) to identify how many times "next" paginate button will be clicked
                decimal numberOfPages = GetNumberOfPages(totalSearchResults, maxDisplayedNumberOfRecordsPerPage);
                // Check if totalSearchResults inside app is equal to number of results in csv; Subtracting 1 to csv count due to csvHeaders are being counted as additional line
                if(totalSearchResults == parsedCsv.Count() - 1)
                {
                    // If total number of pages is greater than one, then start for loop
                    for (int i = 1; i <= numberOfPages; i++) 
                    {
                        if (i == numberOfPages) // if last iteration (last page) - re-assign variable values to get latest displayedRows count since it may no longer maximize 50 records per page
                        {
                            currentlyDisplayedNumberOfRecords = mElement.FindElements(By.XPath(tableContentsXpath)).Count();
                            totalNumberOfDisplayedRows = (currentlyDisplayedNumberOfRecords / headerCount);
                        }

                        for (int rowNum = 0; rowNum <= totalNumberOfDisplayedRows; rowNum++)
                        {
                            string rowContents = null,
                                   csvContents = null,
                                   tdContents = null;

                            int indexBasedOnPageNumber = 0,
                                 recordNumber = ((50 * (i - 1)) + rowNum);

                            if (i > 1 && rowNum == 0) // if iteration is in next page and first iteration
                            {
                                indexBasedOnPageNumber = (recordNumber + 1);
                                rowNum = rowNum + 1;
                            }
                            else if (i > 1) // 2nd page but not first iteration
                            {
                                indexBasedOnPageNumber = recordNumber;
                                rowNum = rowNum++;
                            }
                            else
                                indexBasedOnPageNumber = rowNum;

                            tdContents = "(//*[@id='gridViewTable_wrapper']//tbody/tr)[" + rowNum + "]//td";
                            csvContents = parsedCsv.Where(x => x.Index == indexBasedOnPageNumber).Select(x => x.Text.Trim()).FirstOrDefault();


                            // Not on second page - must not parse headers
                            if (rowNum != 0)
                            {
                                foreach (var rowItem in mElement.FindElements(By.XPath(tdContents)).ToList())
                                {
                                    rowContents += '"' + rowItem.Text.Trim() + '"' + ",";
                                }
                            }
                            else // If iteration is in first page, then parse header
                                rowContents = GetAllHeaders();

                            AssertTableAndCSVPerRow(rowContents.TrimEnd(','), csvContents, rowNum, i);
                        }

                        if (numberOfPages >= i + 1) // Page 2 and so on indicator
                        {
                            IWebElement nextPaginateButton = mElement.FindElements(By.XPath(nextPaginationXpath)).FirstOrDefault();
                            int pageNumber = i + 1;

                            DlkLogger.LogInfo("Clicking next button..");
                            new DlkBaseControl("Button", nextPaginateButton).ClickUsingJavaScript();
                            DlkLogger.LogInfo("Getting records on page: " + pageNumber);

                            Thread.Sleep(1500);
                        }
                    }
                }
                else
                {
                    throw new Exception("Number of search results must be equal to number of query results");
                }

               
            }
        }


        [Keyword("ClickColumnHeader", new String[] { "1|text|Column Header|Line*" })]
        public virtual void ClickColumnHeader(String ColumnHeader)
        {
            Initialize();
            GetHeader(ColumnHeader).Click();
        }

        [Keyword("ClickColumnHeaderCheckbox", new String[] { "1|text|Column Header|Line*" })]
        public virtual void ClickColumnHeaderCheckbox(String Column)
        {
            Initialize();
            mElement.FindElement(By.XPath(".//thead//th[" + Column + "]/input")).Click();
        }

        [Keyword("SortColumn_Asc")]
        public virtual void SortColumn_Asc(String ColumnHeader)
        {
            Initialize();

            bool isGridStyleOne = false;
            if (mElement.GetAttribute("class").ToLower().Contains("gridstyleone"))
                isGridStyleOne = true;
            Stopwatch s = new Stopwatch();
            int timeLimitMinutes = 5;
            s.Start();
            var columnHeaderAsc = GetHeader(ColumnHeader);
            while (!columnHeaderAsc.element.GetAttribute("class").ToLower().Contains("_asc") || s.Elapsed > TimeSpan.FromMinutes(timeLimitMinutes))
            {
               
                if (s.Elapsed > TimeSpan.FromMinutes(timeLimitMinutes))
                    throw new Exception("SortColumn_Asc failed. Keyword has exceeded 5-minute time limit.");

                columnHeaderAsc.Click();
                Thread.Sleep(4000);

                if (isGridStyleOne)
                {
                    FindElement();
                    RefreshGridStyleOneTable();
                    columnHeaderAsc = GetHeader(ColumnHeader);
                    if (columnHeaderAsc.element.FindElement(By.XPath(".//img")).GetAttribute("src").ToLower().Contains("up"))
                        break;
                }
            }
            s.Stop();
        }

        [Keyword("SortColumn_Desc")]
        public virtual void SortColumn_Desc(String ColumnHeader)
        {
            Initialize();
            bool isGridStyleOne = false;
            if (mElement.GetAttribute("class").ToLower().Contains("gridstyleone"))
                isGridStyleOne = true;

            Stopwatch s = new Stopwatch();
            int timeLimitMinutes = 5;
            s.Start();
            var columnHeaderAsc = GetHeader(ColumnHeader);
            while (!columnHeaderAsc.element.GetAttribute("class").ToLower().Contains("_desc") || s.Elapsed > TimeSpan.FromMinutes(timeLimitMinutes))
            {
                if (s.Elapsed > TimeSpan.FromMinutes(timeLimitMinutes))
                    throw new Exception("SortColumn_Desc failed. Keyword has exceeded 5-minute time limit.");


                columnHeaderAsc.Click();
                Thread.Sleep(4000);

                if (isGridStyleOne)
                {
                    FindElement();
                    RefreshGridStyleOneTable();
                    columnHeaderAsc = GetHeader(ColumnHeader);
                    if (columnHeaderAsc.element.FindElement(By.XPath(".//img")).GetAttribute("src").ToLower().Contains("down"))
                        break;
                }
            }
            s.Stop();
        }

        [Keyword("VerifyIfColumnIsAscending")]
        public virtual void VerifyIfColumnIsAscending(String ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            bool ActualValue = false, isGridStyleOne = false;
            if (mElement.GetAttribute("class").ToLower().Contains("gridstyleone"))
                isGridStyleOne = true;

            var columnHeaderAsc = GetHeader(ColumnHeader);
            if (columnHeaderAsc.element.GetAttribute("class").ToLower().Contains("_asc"))
                ActualValue = true;
            if (isGridStyleOne)
            {
                columnHeaderAsc = GetHeader(ColumnHeader);
                if (columnHeaderAsc.element.FindElement(By.XPath(".//img")).GetAttribute("src").ToLower().Contains("up"))
                    ActualValue = true;
            }
            DlkAssert.AssertEqual("VerifyIfColumnIsAscending()", Convert.ToBoolean(TrueOrFalse), ActualValue);
        }

        [Keyword("VerifyIfColumnIsDescending")]
        public virtual void VerifyIfColumnIsDescending(String ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            bool ActualValue = false, isGridStyleOne = false;
            if (mElement.GetAttribute("class").ToLower().Contains("gridstyleone"))
                isGridStyleOne = true;

            var columnHeaderAsc = GetHeader(ColumnHeader);
            if (columnHeaderAsc.element.GetAttribute("class").ToLower().Contains("_desc"))
                ActualValue = true;
            if (isGridStyleOne)
            {
                columnHeaderAsc = GetHeader(ColumnHeader);
                if (columnHeaderAsc.element.FindElement(By.XPath(".//img")).GetAttribute("src").ToLower().Contains("down"))
                    ActualValue = true;
            }
            DlkAssert.AssertEqual("VerifyIfColumnIsAscending()", Convert.ToBoolean(TrueOrFalse), ActualValue);
        }

        [Keyword("ClickColumnHeaderWithText", new String[] { "1|text|Column Header|Line*" })]
        public virtual void ClickColumnHeaderWithText(String ColumnHeader)
        {
            Initialize();
            GetHeaderContainsText(ColumnHeader).Click();
        }

        [Keyword("ClickTableCellButton", new String[] {"1|text|Row|O{Row}",
                                                       "2|text|Column Header|Line*"})]
        public virtual void ClickTableCellButton(String Row, String ColumnHeader)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickButton(mstrTableType);
            }
            else
            {
                throw new Exception("ClickTableCellButton() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("ClickTableCellRadioButton", new String[] {"1|text|Row|O{Row}",
                                                       "2|text|Column Header|Line*"})]
        public virtual void ClickTableCellRadioButton(String Row, String ColumnHeader)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickRadioButton(mstrTableType);
            }
            else
            {
                throw new Exception("ClickTableCellRadioButton() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("SelectTableCellMark", new String[] {"1|text|Row|O{Row}",
                                                       "2|text|Column Header|Line*", "2|text|Column Header|1*"})]
        public virtual void SelectTableCellMark(String Row, String ColumnHeader, String MarkValue)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.SelectMark(mstrTableType,MarkValue);
            }
            else
            {
                throw new Exception("SelectTableCellMark() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("ClickTableCellButtonWithNoHeader", new String[] {"1|text|Row|O{Row}",
                                                       "2|text|Column|O{Col}"})]
        public virtual void ClickTableCellButtonWithNoHeader(String Row, String Column)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithCoordinates(iRow, -1, Convert.ToInt32(Column) - 1);

            if (cell != null)
            {
                cell.ClickButton(mstrTableType);
            }
            else
            {
                throw new Exception("ClickTableCellButton() failed. Unable to get cell with Row=" + Row + " and Column=" + Column);
            }

        }

        [Keyword("ClickTableCellCheckBox", new String[] {"1|text|Row|O{Row}",
                                                       "2|text|Column Header|Line*"})]
        public virtual void ClickTableCellCheckBox(String Row, String ColumnHeader)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickButton(mstrTableType, "checkbox");
            }
            else
            {
                throw new Exception("ClickTableCellCheckBox() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("ClickTableCellCheckBoxWithText", new String[] {"1|text|Row|O{Row}",
                                                       "2|text|Column Header|Line*"})]
        public virtual void ClickTableCellCheckBoxWithText(String CheckboxText)
        {
            Initialize();

            DlkCell cell = null;

            foreach (DlkRow row in mlstRows)
            {
                DlkCell tempCell = null;
                foreach (DlkCell curCell in row.lstCells)
                {
                    if (curCell.GetValue(mstrTableType).Trim().ToLower().Equals(CheckboxText.ToLower().Trim()))
                        cell = tempCell;
                    tempCell = curCell;
                }
            }

            if (cell != null)
            {
                cell.ClickButton(mstrTableType, "checkbox");
            }
            else
            {
                throw new Exception("ClickTableCellCheckBoxWithText() failed. Unable to get cell with CheckboxText=" + CheckboxText);
            }

        }

        [Keyword("ClickTableCellButtonWithText", new String[] {"1|text|Row|O{Row}",
                                                               "2|text|Column Header|Line*",
                                                               "3|text|Button Text"})]
        public virtual void ClickTableCellButtonWithText(String Row, String ColumnHeader, String ButtonText)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickButtonWithText(mstrTableType, ButtonText);
            }
            else
            {
                throw new Exception("ClickTableCellButtonWithText() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [RetryKeyword("VerifyTableCellButtonWithText", new String[] {  "1|text|Row|O{Row}",
                                                                        "2|text|Column Header|Line*",
                                                                        "3|text|Button Text",
                                                                        "4|text|Expected TRUE or FALSE|TRUE"})]
        public virtual void VerifyTableCellButtonWithText(String Row, String ColumnHeader, String ButtonText, String TrueOrFalse)
        {
            String row = Row;
            String columnHeader = ColumnHeader;
            String buttonText = ButtonText;
            String expectedValue = TrueOrFalse;

             String[] inputs = new String[]{row,columnHeader,buttonText,expectedValue};
            this.PerformAction(() =>
                {
                    bool bActualValue = false;
                    Initialize();

                    //converting sRow from 1 based to zero based index
                    int iRow = Convert.ToInt32(row) - 1;

                    DlkCell cell = GetCellWithHeaderText(iRow, columnHeader);

                    if (cell != null)
                    {
                        bActualValue = cell.VerifyButtonWithText(mstrTableType, buttonText);
                    }
                    else
                    {
                        throw new Exception("VerifyTableCellButtonWithText() failed. Unable to get cell with Row=" + row + " and Column=" + columnHeader);
                    }

                    DlkAssert.AssertEqual("VerifyTableCellButtonWithText()", Convert.ToBoolean(expectedValue), bActualValue);

                }, new String[] { "retry" });
        }

        [Keyword("ClickTableCellLink", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Column Header|Line*"})]
        public virtual void ClickTableCellLink(String Row, String ColumnHeader)
        {
            int intcol = 0;
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            //if no header
            if (int.TryParse(ColumnHeader, out intcol))
            {
                intcol = intcol - 1;
                ColumnHeader = intcol.ToString();
            }
            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickLink(mstrTableType);
            }
            else
            {
                throw new Exception("ClickTableCellLink() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("VerifyTableCellLinkIsReadOnly", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Column Header|Line*"})]
        public virtual void VerifyTableCellLinkIsReadOnly(String Row, String ColumnHeader)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                //IList<IWebElement> links = element.FindElements(By.CssSelector("a"));
                //if (links.Count > 0)
                //{
                //    DlkLink cellLink = new DlkLink("Cell Link", links.First());
                //    cellLink.Click();
                //}
                //else
                //{
                //    DlkLogger.LogException("ClickLink() failed. Cell at row '" + this.row.ToString() + "' column '" + this.header.HeaderText + "' does not have a link.");
                //}

            }
            //else
            //{
            //    DlkLogger.LogException("ClickTableCellLink() failed. Unable to get cell with Row=" + sRow + " and Column=" + sColumnHeader);
            //}

        }

        [Keyword("ClickSubRowLinkWithNoHeader", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Sub Row|O{SubRow}",
                                                    "3|text|Cell Column|O{Col}*"})]
        public virtual void ClickSubRowLinkWithNoHeader(String Row, String SubRow, String CellColumn)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;
            int iSubRow = Convert.ToInt32(SubRow) - 1;
            int iCol = Convert.ToInt32(CellColumn) - 1;

            DlkCell cell = GetCellWithCoordinates(iRow, iSubRow, iCol);

            if (cell != null)
            {
                cell.ClickLink(mstrTableType);
            }
            else
            {
                throw new Exception("ClickSubRowLinkWithNoHeader() failed. Unable to get cell with Row=" + Row + " and Column=" + CellColumn);
            }
        }

        [Keyword("ClickRowLinkWithNoHeader", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Cell Column|O{Col}*"})]
        public virtual void ClickRowLinkWithNoHeader(String Row, String CellColumn)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;
            int iCol = Convert.ToInt32(CellColumn) - 1;

            DlkCell cell = GetCellWithCoordinates(iRow, -1, iCol);

            if (cell != null)
            {
                cell.ClickLink(mstrTableType);
            }
            else
            {
                throw new Exception("ClickRowLinkWithNoHeader() failed. Unable to get cell with Row=" + Row + " and Column=" + CellColumn);
            }
        }

        [Keyword("ClickSubRowLink", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Sub Row|O{SubRow}",
                                                    "3|text|Column Header|Line*"})]
        public virtual void ClickSubRowLink(String Row, String SubRow, String ColumnHeader)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;
            int iSubRow = Convert.ToInt32(SubRow) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, iSubRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickLink(mstrTableType);
            }
            else
            {
                throw new Exception("ClickSubRowLink() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }
        }

        [Keyword("ClickTableCellLinkWithTitle", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Column Header|Line*",
                                                    "3|text|Link Title|Sample Title"})]
        public virtual void ClickTableCellLinkWithTitle(String Row, String ColumnHeader, String Title)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickLink(mstrTableType, Title);
            }
            else
            {
                throw new Exception("ClickTableCellLink() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("ClickTableCellLinkWithText", new String[] {"1|text|Row|O{Row}",
                                                            "2|text|Column Header|Line*",
                                                            "3|text|Link Text|Sample Text"})]
        public virtual void ClickTableCellLinkWithText(String Row, String ColumnHeader, String Text)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickLinkWithText(mstrTableType, Text);
            }
            else
            {
                throw new Exception("ClickTableCellLinkWithText() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("ClickTableCellComboBox", new String[] {"1|text|Row|O{Row}",
                                                            "2|text|Column Header|Line*",
                                                            "3|text|Option Text|Sample Text"})]
        public virtual void ClickTableCellComboBox(String Row, String ColumnHeader, String Text)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.ClickComboBoxWithText(mstrTableType, Text);
            }
            else
            {
                throw new Exception("ClickTableCellComboBox() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("SetTableCellValue", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Column Header|Line*",
                                                    "3|text|Value|Sample Value"})]
        public virtual void SetTableCellValue(String Row, String ColumnHeader, String Value)
        {
            Initialize();

            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.SetValue(mstrTableType, Value);
            }
            else
            {
                throw new Exception("SetTableCellValue() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }

        }

        [Keyword("SetTableCellComboBoxValue", new String[] {    "1|text|Row Index|O{Row}",
                                                                            "2|text|Column Index|Line*",
                                                                            "3|text|Expected Value|Sample Value"})]
        public virtual void SetTableCellComboBoxValue(String Row, String ColumnHeader, String Value)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.SelectComboBoxValue(Value);
            }
            else
            {
                throw new Exception("SetTableCellComboBoxValue() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }
        }

        [Keyword("GetTableCellValue", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Column Header|Line*",
                                                    "3|text|VariableName|MyValue"})]
        public virtual void GetTableCellValue(String Row, String ColumnHeader, String VariableName)
        {
            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            Initialize();

            if (iRow < mlstRows.Count)
            {
                DlkCell currentCell = null;
                foreach (DlkCell cell in mlstRows[iRow].lstCells)
                {
                    if (cell.header.HeaderText.ToLower() == ColumnHeader.ToLower())
                    {
                        currentCell = cell;
                        break;
                    }
                }

                DlkVariable.SetVariable(VariableName, currentCell.GetValue(mstrTableType));
            }
            else
            {
                throw new Exception("GetTableCellValue() failed. Row '" + Row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
            }
        }

        [Keyword("GetTableCellValueNoHeader", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Column|O{Col}",
                                                    "3|text|VariableName|MyValue"})]
        public virtual void GetTableCellValueNoHeader(String Row, String Column, String VariableName)
        {
            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;


            Initialize();

            if (iRow < mlstRows.Count)
            {
                DlkCell currentCell = null;

                DlkRow row = mlstRows[iRow];
                currentCell = row.lstCells[Convert.ToInt32(Column)];

                DlkVariable.SetVariable(VariableName, currentCell.GetValue(mstrTableType));

            }
            else
            {
                throw new Exception("GetTableCellValueNoHeader() failed. Row '" + Row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
            }
        }

        [Keyword("GetTableSubRowWithRowColumnValue", new String[] {"1|text|Row|O{Row}",
                                                            "2|text|Column Header|Line*",
                                                            "3|text|Value|SubTab1",
                                                            "4|text|VariableName|MySubRow"})]
        public virtual void GetTableSubRowWithRowColumnValue(String Row, String ColumnHeader, String Value, String VariableName)
        {
            bool blnFound = false;
            int iRow = Convert.ToInt32(Row) -1;

            Initialize();

            if (HeaderExists(ColumnHeader))
            {
                for (int i = 0; i < mDicSubRows.ElementAt(iRow).Value.Count; i++)
                {

                    foreach (DlkCell cell in mDicSubRows.ElementAt(iRow).Value[i].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == ColumnHeader.ToLower())
                        {
                            try
                            {
                                if (cell.GetValue(mstrTableType) == Value)
                                {
                                    DlkVariable.SetVariable(VariableName,Convert.ToString(i + 1));
                                    blnFound = true;
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }

                    }
                    if (blnFound)
                    {
                        break;
                    }

                }


                if (blnFound)
                {
                    DlkLogger.LogInfo("Successfully executed GetTableSubRowWithRowColumnValue().");
                }
                else
                {
                    throw new Exception("GetTableSubRowWithRowColumnValue() failed. Value '" + Value + "' under Column '" + ColumnHeader + "' not found in table.");
                }
            }
            else
            {
                throw new Exception("GetTableSubRowWithRowColumnValue() failed. Column '" + ColumnHeader + "' not found.");
            }


        }

        public virtual void GetTableRowWithDetailHeader(String ColHeaderOrNumber, String VariableName)
        {
            bool blnFound = false;

            Initialize();

            if (HeaderExists(ColHeaderOrNumber))
            {
                for (int i = 0; i < mlstRows.Count; i++)
                {
                    int curCol = 1;
                    foreach (DlkCell cell in mlstRows[i].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower().Contains(ColHeaderOrNumber.ToLower()) || int.TryParse(ColHeaderOrNumber, out curCol))
                        {
                            try
                            {
                                DlkVariable.SetVariable(VariableName, Convert.ToString(i + 1));

                                blnFound = true;
                                break;
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }

                        }


                    }
                    if (blnFound)
                    {
                        break;
                    }

                }


                if (blnFound)
                {
                    DlkLogger.LogInfo("Successfully executed GetTableRowWithDetailHeader().");
                }
                else
                {
                    throw new Exception("GetTableRowWithDetailHeader() failed. Column '" + ColHeaderOrNumber + "' not found in table.");
                }
            }
            else
            {
                throw new Exception("GetTableRowWithColumnValue() failed. Column '" + ColHeaderOrNumber + "' not found.");
            }


        }



        [Keyword("GetTableRowWithColumnValue", new String[] {"1|text|Column Header|Line*",
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public virtual void GetTableRowWithColumnValue(String ColHeaderOrNumber, String Value, String VariableName)
        {
            bool blnFound = false;

            Initialize();

            if (HeaderExists(ColHeaderOrNumber))
            {
                for (int i = 0; i < mlstRows.Count; i++)
                {
                    int curCol = 1;
                    foreach (DlkCell cell in mlstRows[i].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == ColHeaderOrNumber.ToLower() || int.TryParse(ColHeaderOrNumber,out curCol))
                        {
                            try
                            {
                                if (cell.GetValue(mstrTableType).Trim().Contains(Value))
                                {
                                    DlkVariable.SetVariable(VariableName, Convert.ToString(i + 1));

                                    blnFound = true;
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                throw new Exception("Header: " + ColHeaderOrNumber +" does not contain the value: " + Value +".");
                            }
                            curCol++;
                        }


                    }
                    if (blnFound)
                    {
                        break;
                    }

                }


                if (blnFound)
                {
                    DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue().");
                }
                else
                {
                    throw new Exception("GetTableRowWithColumnValue() failed. Value '" + Value + "' under Column '" + ColHeaderOrNumber + "' not found in table.");
                }
            }
            else
            {
                throw new Exception("GetTableRowWithColumnValue() failed. Column '" + ColHeaderOrNumber + "' not found.");
            }


        }

        [Keyword("VerifyTableRowExistWithColumnValue", new String[] {"1|text|Column Header|Line*",
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public virtual void VerifyTableRowExistWithColumnValue(String ColHeaderOrNumber, String Value, String Expected)
        {
            bool blnFound = false;
            bool expectedVal = Convert.ToBoolean(Expected);

            Initialize();

            if (HeaderExists(ColHeaderOrNumber))
            {
                for (int i = 0; i < mlstRows.Count; i++)
                {
                    int curCol = 1;
                    foreach (DlkCell cell in mlstRows[i].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == ColHeaderOrNumber.ToLower() || int.TryParse(ColHeaderOrNumber, out curCol))
                        {
                            try
                            {
                                if (cell.GetValue(mstrTableType).Trim() == Value)
                                {

                                    blnFound = true;
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                            curCol++;
                        }


                    }
                    DlkAssert.AssertEqual("VerifyTableRowExistWithColumnValue():", expectedVal, blnFound);

                }


                if (blnFound)
                {
                    DlkLogger.LogInfo("Successfully executed VerifyTableRowExistWithColumnValue().");
                }
                else
                {
                    throw new Exception("VerifyTableRowExistWithColumnValue() failed. Value '" + Value + "' under Column '" + ColHeaderOrNumber + "' not found in table.");
                }
            }
            else
            {
                throw new Exception("VerifyTableRowExistWithColumnValue() failed. Column '" + ColHeaderOrNumber + "' not found.");
            }


        }

        [Keyword("GetTableHeaders", new String[] {"1|text|VariableName|MyHeaders"})]
        public virtual void GetTableHeaders(String VariableName)
        {
            String strActualHeaders = "";
            Boolean bFirst = true;

            Initialize();
            foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGroup.lstHeaders)
                {
                    if (!bFirst)
                    {
                        strActualHeaders = strActualHeaders + "~";
                    }
                    strActualHeaders = strActualHeaders + header.HeaderText;
                    bFirst = false;
                }

                DlkVariable.SetVariable(VariableName, strActualHeaders);
            }
        }

        [Keyword("GetTableRowWithPartialColumnValue", new String[] {"1|text|Column Header|Line*",
                                                                    "2|text|Partial Value|Partial Text",
                                                                    "3|text|VariableName|MyRow"})]
        public virtual void GetTableRowWithPartialColumnValue(String ColHeaderOrNumber, String Value, String VariableName)
        {
            bool blnFound = false;

            Initialize();

            if (HeaderExists(ColHeaderOrNumber))
            {
                for (int i = 0; i < mlstRows.Count; i++)
                {
                    int curCol = 1;
                    foreach (DlkCell cell in mlstRows[i].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == ColHeaderOrNumber.ToLower() || int.TryParse(ColHeaderOrNumber, out curCol))
                        {
                            try
                            {
                                if (cell.GetValue(mstrTableType).Contains(Value))
                                {
                                    DlkVariable.SetVariable(VariableName, Convert.ToString(i + 1));

                                    blnFound = true;
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }

                    }
                    if (blnFound)
                    {
                        break;
                    }

                }


                if (blnFound)
                {
                    DlkLogger.LogInfo("Successfully executed GetTableRowWithPartialColumnValue().");
                }
                else
                {
                    throw new Exception("GetTableRowWithPartialColumnValue() failed. Partial value '" + Value + "' under Column '" + ColHeaderOrNumber + "' not found in table.");
                }
            }
            else
            {
                throw new Exception("GetTableRowWithPartialColumnValue() failed. Column '" + ColHeaderOrNumber + "' not found.");
            }


        }

        public virtual String GetTableColumnHeaders()
        {
            Boolean bFirst = true;
            String sActual = "";
            Initialize();
            foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGroup.lstHeaders)
                {
                    if (!bFirst)
                    {
                        sActual = sActual + "~";
                    }
                    sActual = sActual + header.HeaderText;
                    bFirst = false;
                }
            }

            return sActual;
        }

        [Keyword("ExportTableToExcel", new String[] { "1|text|Excel Filename|Export.xlsx" })]
        public virtual void ExportTableToExcel(String ExcelFilePath)
        {
            Initialize();

            Application ExcelApp = new Application();
            ExcelApp.Workbooks.Add(Type.Missing);
            Worksheet activeSheet = ExcelApp.ActiveWorkbook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            activeSheet.Name = mControlName;
            int colIndex = 1;
            foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGroup.lstHeaders)
                {
                    activeSheet.Cells[1, colIndex].Value = header.HeaderText;
                    colIndex++;
                }
            }
            int rowIndex = 2;
            DlkLogger.LogInfo("mstrTableType = " + mstrTableType);
            foreach (DlkRow row in mlstRows)
            {
                for (int iCol = 1; iCol <= row.lstCells.Count; iCol++)
                {
                    activeSheet.Cells[rowIndex, iCol].NumberFormat = "@";
                    activeSheet.Cells[rowIndex, iCol].Value = row.lstCells[iCol - 1].GetValue(mstrTableType);
                }

                //foreach (DlkCell cell in row.lstCells)
                //{
                //    DlkLogger.LogInfo("header index=" + cell.header.HeaderIndex.ToString());
                //    DlkLogger.LogInfo((cell.element == null).ToString());
                //    activeSheet.Cells[rowIndex, cell.header.HeaderIndex + 1].NumberFormat = "@";
                //    activeSheet.Cells[rowIndex, cell.header.HeaderIndex + 1].Value = cell.GetValue(mstrTableType);
                //}
                rowIndex++;
            }
            ExcelApp.ActiveWorkbook.SaveAs(DlkEnvironment.mDirTools + ExcelFilePath, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange,
                        XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ExcelApp.ActiveWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ExcelApp.Quit();
        }

        [Keyword("RefreshTableData")]
        public virtual void RefreshTableData()
        {
            Thread.Sleep(1000);
            FindElement(); // find the table
            DlkLogger.LogInfo("RefreshTableData: FindElement() succeeded.");

            mlstRows = new List<DlkRow>();
            mlstHeaderGroups = new List<DlkHeaderGroup>();

            string classVal = mElement.GetAttribute("class").ToLower().Length > 0 ?
                                mElement.GetAttribute("class").ToLower() :
                                DlkTableTypeStructureParser.ParseFormTableStructure(mElement);
            if (classVal.Contains("datatable"))
                classVal = "datatable";
            if (classVal.Contains("formtable"))
                classVal = "formtable";
            if (classVal.Contains("gridtable"))
                classVal = "gridtable";
            if (classVal.Contains("gridstyleone"))
                classVal = "gridstyleone";
            if (classVal.Contains("gridstyletwo"))
                classVal = "gridstyletwo";
            if (classVal.Contains("gritter-item"))
                classVal = "gritter-item";
            if (classVal.Contains("subrowstable"))
                classVal = "subrowstable";
            if (classVal.Equals("smallfont"))
                classVal = "formtable";
            if (classVal.Contains("resultstable"))
                classVal = "gridtable";

            switch (classVal)
            {
                case "datatable":
                    mstrTableType = "datatable";
                    DlkLogger.LogInfo("RefreshTableData: preparing for RefreshDataTable().");
                    if (!mControlName.Contains("SubTab"))
                        RefreshDataTable();
                    else
                        RefreshDataSubTable();
                    break;
                case "charttable":
                case "formtable":
                    mstrTableType = "formtable";
                    DlkLogger.LogInfo("RefreshTableData: preparing for RefreshFormTable().");
                    RefreshFormTable();
                    break;
                case "gridstyleone":
                    mstrTableType = "gridstyleone";
                    DlkLogger.LogInfo("RefreshTableData: preparing for RefreshGridStyleOneTable().");
                    RefreshGridStyleOneTable();
                    break;
                case "gridstyletwo fivecolumncompare":
                    mstrTableType = "gridstyletwo";
                    DlkLogger.LogInfo("RefreshTableData: preparing for RefreshGridStyleTwoTable().");
                    RefreshGridStyleTwoTable();
                    break;
                case "gridtable":
                case "zipcontents":
                case "statustable":
                    mstrTableType = "gridtable";
                    DlkLogger.LogInfo("RefreshTableData: preparing for RefreshGridTable().");
                    RefreshGridTable();
                    break;
                case "gritter-item":
                    mstrTableType = "gritter-item";
                    RefreshGritterTable();
                    break;
                case "subrowstable":
                    mstrTableType = "panelBody flush subrowstable ui-sortable";
                    RefreshExpandableTable();
                    break;
                default:
                    throw new Exception("RefreshTableData() failed. Table type '" + mElement.GetAttribute("class") + "' not supported.");
            }

            DlkLogger.LogInfo("RefreshTableData: Refresh table succeeded.");
            IsInitialized = true;
        }

        protected virtual void RefreshExpandableTable()
        {
            IList<IWebElement> rows;
            IList<IWebElement> subrows;
            List<DlkRow> currentSubRows;
            mDicSubRowsExpandable = new Dictionary<int, List<DlkRow>>();

            //add placeholder header
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);

            IList<IWebElement> colHeaders = mElement.FindElements(By.XPath("./div/table/colgroup/col"));

            //Get Headers
            for (int i = 0; i < colHeaders.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, null, i);
            }
            mlstHeaderGroups.Add(headerGroup);

            IWebElement tableContent = mElement;

            rows = tableContent.FindElements(By.XPath("./div"));

            int iRow = 0;
            foreach (IWebElement row in rows)
            {
                DlkRow currentRow = GetRow(iRow);
                IList<IWebElement> cells = row.FindElements(By.XPath("./table//td"));
                for (int iCell = 0; iCell < cells.Count; iCell++)
                {
                    DlkCell currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                    currentRow.AddCell(currentCell);
                }

                //Get Sub Contents per Row                
                currentSubRows = new List<DlkRow>();
                subrows = row.FindElements(By.XPath("./div/table//tr"));
                if (subrows.Count > 0)
                {
                    //subrows = DlkEnvironment.AutoDriver.FindElements(By.XPath("./div[@id[contains(.,'row1DataContainer')]]/table//tr"));

                    int iSubRow = 0;

                    foreach (IWebElement subrow in subrows)
                    {

                        DlkRow currentSubRow = new DlkRow(iSubRow);

                        IList<IWebElement> subCells = subrow.FindElements(By.XPath("./td"));
                        for (int iCell = 0; iCell < subCells.Count; iCell++)
                        {
                            DlkCell currentCell = new DlkCell(subCells[iCell], iSubRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                            currentSubRow.AddCell(currentCell);
                        }
                        currentSubRows.Add(currentSubRow);

                        iSubRow++;
                    }
                }
                mDicSubRowsExpandable.Add(iRow, currentSubRows);
                mDicSubRows = mDicSubRowsExpandable;


                iRow++;
            }

        }

        protected virtual void RefreshGritterTable()
        {
            //add placeholder header
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            IList<IWebElement> colHeaders = mElement.FindElements(By.XPath(".//div[@id='CompareContent']/div[@id[contains(.,'lineItem')]]/div"));

            //Get Headers
            for (int i = 0; i < colHeaders.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, null, i);
            }
            mlstHeaderGroups.Add(headerGroup);

            IList<IWebElement> rows;

            IWebElement tableContent = mElement.FindElement(By.XPath(".//div[@id='CompareContent']"));

            rows = tableContent.FindElements(By.XPath("./div"));

            int iRow = 0;
            foreach (IWebElement row in rows)
            {
                DlkRow currentRow = GetRow(iRow);
                IList<IWebElement> cells = row.FindElements(By.XPath("./div"));
                for (int iCell = 0; iCell < cells.Count; iCell++)
                {
                    DlkCell currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                    currentRow.AddCell(currentCell);
                }
                iRow++;
            }

        }

        public Boolean ColumnHeaderExists(String sColumnHeader)
        {
            Initialize();
            foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGroup.lstHeaders)
                {
                    if (header.HeaderText == sColumnHeader)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Boolean ColumnHeaderExistsNoWhiteSpace(String sColumnHeader)
        {
            Initialize();
            foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGroup.lstHeaders)
                {
                    if (header.HeaderText.Replace(" ", "") == sColumnHeader)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Boolean ContainsColumnHeaders()
        {
            bool containsColHeaders = false;
            Initialize();

            foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGroup.lstHeaders)
                {
                    containsColHeaders = true;
                    break;
                }
            }

            return containsColHeaders;
        }

        protected virtual void RefreshDataSubTable()
        {
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            IList<IWebElement> headers;
            IList<IWebElement> rows;
            List<IWebElement> subrows;
            List<DlkRow> currentSubRows;
            mDicSubRows = new Dictionary<int, List<DlkRow>>();


            //Populate Header and Row
            headers = mElement.FindElements(By.CssSelector("thead>tr>th"));
            rows = mElement.FindElements(By.XPath("./tbody/tr[@class='odd']"));

            //Get Headers
            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }
            mlstHeaderGroups.Add(headerGroup);

            //Get Table Content
            int iRow = 0;

            foreach (IWebElement row in rows)
            {
                if (row.GetAttribute("class") != "GridHeader" && row.GetAttribute("display") != "none")
                {
                    DlkRow currentRow = GetRow(iRow);

                    IList<IWebElement> cells = row.FindElements(By.XPath("./td"));
                    for (int iCell = 0; iCell < cells.Count; iCell++)
                    {
                        DlkCell currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                        currentRow.AddCell(currentCell);
                    }


                    //Get Sub Contents per Row
                    bool isAcquired = false;
                    subrows = new List<IWebElement>();
                    currentSubRows = new List<DlkRow>();
                    IWebElement sRow = null;
                    if (row.FindElements(By.XPath("following-sibling::tr")).Count > 0)
                    {
                        sRow = row.FindElement(By.XPath("following-sibling::tr"));

                        while (!isAcquired)
                        {
                            if (sRow.GetAttribute("class").Equals("even"))
                                subrows.Add(sRow);
                            else
                                isAcquired = true;
                            if (sRow.FindElements(By.XPath("following-sibling::tr")).Count > 0)
                                sRow = sRow.FindElement(By.XPath("following-sibling::tr"));
                            else
                                break;
                        }

                        int iSubRow = 0;

                        foreach (IWebElement subrow in subrows)
                        {

                            DlkRow currentSubRow = new DlkRow(iSubRow);

                            IList<IWebElement> subCells = subrow.FindElements(By.XPath("./td"));
                            for (int iCell = 0; iCell < subCells.Count; iCell++)
                            {
                                DlkCell currentCell = new DlkCell(subCells[iCell], iSubRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                                currentSubRow.AddCell(currentCell);
                            }
                            currentSubRows.Add(currentSubRow);

                            iSubRow++;
                        }
                    }
                    mDicSubRows.Add(iRow, currentSubRows);
                    iRow++;

                }
            }

        }

        protected virtual void RefreshDataTable()
        {

            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            IList<IWebElement> headers;
            IList<IWebElement> rows;

            if (mElement.GetAttribute("class").Equals("dataTables_wrapper") || mElement.GetAttribute("class").Equals("dataTables_scroll"))
            {
                headers = mElement.FindElements(By.XPath(".//div[@class='dataTables_scrollHead']/div/table//th"));
                rows = mElement.FindElements(By.XPath(".//div[@class='dataTables_scrollBody']//tbody/tr"));
            }
            else
            {
                headers = mElement.FindElements(By.CssSelector("thead>tr>th"));
                rows = mElement.FindElements(By.XPath("./tbody/tr"));
            }

            //Get Headers
            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }
            mlstHeaderGroups.Add(headerGroup);

            //Get Table Content            
            int iRow = 0;
            foreach (IWebElement row in rows)
            {
                if (row.GetAttribute("class") != "GridHeader" && row.GetAttribute("display") != "none")
                {
                    DlkRow currentRow = GetRow(iRow);
                    IList<IWebElement> cells = row.FindElements(By.XPath("./td"));
                    for (int iCell = 0; iCell < cells.Count; iCell++)
                    {
                        DlkCell currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                        currentRow.AddCell(currentCell);
                    }
                    iRow++;
                }
            }


        }

        private void RefreshFormTable()
        {
            bool isHorizontalTable = false; //headers on side
            bool tdHeader = false;
            bool logolistTable = false;

            //Get Headers
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            IList<IWebElement> headers;
            //IList<IWebElement> tempHeaders = null;

            //check Count of tds per tr
            IList<IWebElement> TRcount = mElement.FindElements(By.TagName("tr"));
            IList<IWebElement> TDcount;
            int TDsize = 0;
            foreach(IWebElement e in TRcount)
            {
                TDcount = e.FindElements(By.TagName("td"));
                if(TDcount.Count > 0)
                    TDsize = TDcount.Count;
            }

            headers = mElement.FindElements(By.XPath(".//tbody/tr/th"));

            if (headers.Count == 0)
                headers = mElement.FindElements(By.XPath(".//thead/tr/th"));

            if (mElement.GetAttribute("class").Equals("formTable twoColumn vTop logoList"))
            {
                //headers = mElement.FindElements(By.XPath(".//tbody/tr/td//h4"));
                //tdHeader = true;
                headers = mElement.FindElements(By.XPath(".//tbody/h4"));
                logolistTable = true;
            }

            if (mElement.GetAttribute("class").Contains("formTable twoColumn") && TDsize.Equals(2) && headers.Count.Equals(0) && !logolistTable)
            {
                headers = mElement.FindElements(By.XPath(".//tbody/tr/td[1]|.//tbody/tr/th[1]"));
                if (mElement.FindElements(By.XPath(".//tbody/tr/td[1]")).Count == headers.Count)
                    tdHeader = true;
            }
            if (mElement.GetAttribute("class").Trim().Equals("formTable threeColumn inlineEdit") && TDsize.Equals(2))
            {
                headers = mElement.FindElements(By.XPath(".//tbody/tr/th[1]"));
            }





            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }

            if (headers.Count == 0)
            {
                //add placeholder header
                String searchValHeader = ".//tbody/tr/td";
                if (mElement.GetAttribute("class").Contains("formTable twoColumn"))
                    searchValHeader = ".//thead/tr/th";
                if (mElement.GetAttribute("class").Contains("formTable fourColumn"))
                    searchValHeader = ".//tbody/tr/th";
                if (!(mElement.GetAttribute("class").Equals("smallFont")))
                    headers = mElement.FindElements(By.XPath(searchValHeader));

                if (headers.Count != 0)
                {
                    //Get Headers
                    for (int i = 0; i < headers.Count; i++)
                    {
                        headerGroup.AddHeader(mstrTableType, headers[i], i);
                    }
                }
            }
            if(headers.Count == 0)
            {
                //add placeholder header
                headerGroup = new DlkHeaderGroup(0);
                //headerGroup.AddHeader(mstrTableType, null, 0);

                int headerCnt = mElement.FindElement(By.XPath(".//tbody/tr")).FindElements(By.XPath(".//td")).Count;

                for(int i = 0; i < headerCnt; i++)
                {
                    headerGroup.AddHeader(mstrTableType, null, i);
                }
                if (mElement.GetAttribute("class").ToLower().Equals("smallfont"))
                    isHorizontalTable = true;
            }

            mlstHeaderGroups.Add(headerGroup);
            //Get Table Content
            IList<IWebElement> raws = mElement.FindElements(By.XPath(".//tbody/tr"));

            if(tdHeader)
            {
                raws = mElement.FindElements(By.XPath(".//tbody/td[last()]"));
            }

            IList<IWebElement> rows = raws.GroupBy(p => p.Text).Select(g => g.First()).ToList();
            int iRow = 0;
            int iHeader = 0;
            foreach (IWebElement row in rows)
            {
                if (row.Text.Trim().Equals(""))
                    continue;
                DlkRow currentRow = GetRow(iRow);
                IList<IWebElement> cells = row.FindElements(By.XPath(".//td"));
                if (cells.Count > 1)
                    isHorizontalTable = true; //test for vertical header form table -renpol
                int iCellCounter = 0;
                for (int iCell = 0; iCell < cells.Count; iCell++)
                {
                    //if (cells[iCell].GetAttribute("class") != "actions" && cells[iCell].GetAttribute("class") != "inlineEditCell")
                    //{
                    DlkCell currentCell;
                    if (mElement.GetAttribute("class").ToLower().Contains("vtop") && !(mElement.GetAttribute("class").ToLower().Trim().Equals("formtable twocolumn vtop")) && !(mElement.GetAttribute("class").ToLower().Trim().Equals("formtable threecolumn vtop")) && !(mElement.GetAttribute("class").ToLower().Trim().Equals("formtable fourcolumn vtop")))
                    {
                        if(!logolistTable)
                            iRow = 0;
                        iHeader = 0;
                    }
                    if ((mElement.GetAttribute("class").ToLower().Contains("vtop") && (mElement.GetAttribute("class").ToLower().Trim().Contains("column")) || (mElement.GetAttribute("class").ToLower().Trim().Contains("formtable threecolumn inlineedit"))) && !(mElement.GetAttribute("class").ToLower().Trim().Equals("formtable fourcolumn vtop")))
                        isHorizontalTable = false;
                    if (mElement.GetAttribute("class").Equals("formTable fourColumn") || (mElement.GetAttribute("class").Equals("formTable threeColumn")))
                        if (iCellCounter == 1)
                            iHeader += 1;
                    if (isHorizontalTable)
                        currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell+(2*iRow)]);
                    else
                        currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iHeader]);
                    currentRow.AddCell(currentCell);
                    iCellCounter++;
                    //}
                }
                iRow++;
                iHeader++;

            }
        }


        private void RefreshFormTableTest()
        {

            //Get Headers
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            String tHead = ".//thead/tr/th";
            String headerBody = ".//tbody/tr/th";
            IList<IWebElement> headers = mElement.FindElements(By.XPath(tHead));
            bool isDetails = false;
            if ((headers.Count == 0) || (headers == null))
            {
                headers = mElement.FindElements(By.XPath(headerBody));
                isDetails = true;
            }


            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }

            if (headers.Count == 0)
            {
                //add placeholder header
                String searchValHeader = ".//tbody/tr/td";
                if (mElement.GetAttribute("class").Contains("formTable twoColumn"))
                    searchValHeader = ".//thead/tr/th";
                if (mElement.GetAttribute("class").Contains("formTable fourColumn"))
                    searchValHeader = ".//tbody/tr/th";
                if (!(mElement.GetAttribute("class").Equals("smallFont")))
                    headers = mElement.FindElements(By.XPath(searchValHeader));

                if (headers.Count != 0)
                {
                    //Get Headers
                    for (int i = 0; i < headers.Count; i++)
                    {
                        headerGroup.AddHeader(mstrTableType, headers[i], i);
                    }
                }
            }
            if (headers.Count == 0)
            {
                //add placeholder header
                headerGroup = new DlkHeaderGroup(0);
                //headerGroup.AddHeader(mstrTableType, null, 0);

                int headerCnt = mElement.FindElement(By.XPath(".//tbody/tr")).FindElements(By.XPath(".//td")).Count;

                for (int i = 0; i < headerCnt; i++)
                {
                    headerGroup.AddHeader(mstrTableType, null, i);
                }

            }

            mlstHeaderGroups.Add(headerGroup);
            //Get Table Content
            IList<IWebElement> rows = mElement.FindElements(By.XPath(".//tbody/tr"));
            int iRow = 0;

            foreach (IWebElement row in rows)
            {
                if (iRow == mlstHeaderGroups[0].lstHeaders.Count && (isDetails))
                    continue;
                if (row.Text.Trim().Equals(""))
                {
                    iRow++;
                    continue;
                }

                DlkRow currentRow = GetRow(iRow);
                IList<IWebElement> cells = row.FindElements(By.XPath(".//td"));
                if(cells.Count==0)
                    cells = row.FindElements(By.XPath(".//th"));

                int iCellCounter = 0;
                for (int iCell = 0; iCell < cells.Count; iCell++)
                {
                    DlkCell currentCell;

                    if (row.FindElements(By.XPath("//th")) != null && row.FindElements(By.XPath("//td")) != null && (isDetails))
                    {
                        cells = row.FindElements(By.XPath(".//th"));
                        currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iRow]);
                    }
                    else
                    {
                        currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                    }


                    currentRow.AddCell(currentCell);
                    iCellCounter++;
                }

                iRow++;

            }
        }


        DlkHeaderGroup headerGroupGridStyleOne;

        private void RefreshGridStyleOneTable()
        {
            //Get Headers
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            FindElement();
            IList<IWebElement> headers = mElement.FindElements(By.CssSelector("tbody>tr>th>:not(div)"));
            if (headers.Count == 0)
                headers = mElement.FindElements(By.XPath(".//thead/tr/th"));

            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }
            mlstHeaderGroups.Add(headerGroup);

            //Get Table Content
            IList<IWebElement> rows = mElement.FindElements(By.CssSelector("tbody>tr"));
            int iRow = 0;
            foreach (IWebElement row in rows)
            {
                if (row.GetAttribute("id") != "")
                {
                    DlkRow currentRow = GetRow(iRow);
                    IList<IWebElement> cells = row.FindElements(By.CssSelector("td"));
                    for (int iCell = 0; iCell < cells.Count; iCell++)
                    {
                        DlkCell currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                        currentRow.AddCell(currentCell);
                    }
                    iRow++;
                }
            }

            headerGroupGridStyleOne = headerGroup;
        }

        private void RefreshGridStyleTwoTable()
        {
            //Get Headers
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            IList<IWebElement> headers = mElement.FindElements(By.CssSelector("tbody>tr>th:not([class*='contractName'])"));

            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }
            mlstHeaderGroups.Add(headerGroup);

            //Get Table Content
            IList<IWebElement> cellCounter = mElement.FindElements(By.CssSelector("tbody>tr:nth-child("+1+")>th[class*='contractName']"));
            int rowCount = cellCounter.Count;

            IList<IWebElement> cells;
            for (int iRow = 0; iRow < rowCount; iRow++)
            {
                DlkRow currentRow = GetRow(iRow);
                for (int iCell = 0; iCell < headers.Count; iCell++)
                {
                    cells = mElement.FindElements(By.CssSelector("tbody>tr:nth-child(" + (iCell + 1) + ")>:nth-child(" + (iRow + 2) + ")"));

                    DlkCell currentCell = new DlkCell(cells[0], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                    currentRow.AddCell(currentCell);
                }
            }
        }

        private void RefreshGridTable()
        {
            IList<IWebElement> headers = mElement.FindElements(By.XPath(".//thead/tr/th"));

            //headers could be in the first row
            if (headers.Count == 0)
            {
                headers = mElement.FindElements(By.XPath(".//tbody/tr/th"));
            }

            if (headers.Count > 0)
            {
                RefreshGridTableWithHeaders(headers);
            }
            else
            {
                RefreshGridTableWithNoHeader();
            }



        }

        private void RefreshGridTableWithHeaders(IList<IWebElement> headers)
        {
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            int iHeaderIndex = 0;
            foreach (IWebElement header in headers)
            {
                headerGroup.AddHeader(mstrTableType, header, iHeaderIndex);
                iHeaderIndex++;
            }
            mlstHeaderGroups.Add(headerGroup);

            //Get table content
            IList<IWebElement> rows = mElement.FindElements(By.XPath(".//tbody/tr"));
            int iRow = 0;
            foreach (IWebElement row in rows)
            {
                DlkRow currentRow = GetRow(iRow);
                IList<IWebElement> cells = row.FindElements(By.XPath("th|td"));
                for (int iCellIndex = 0; iCellIndex < cells.Count; iCellIndex++)
                {
                    DlkCell cell = new DlkCell(cells[iCellIndex], iRow, mlstHeaderGroups[0].lstHeaders[iCellIndex]);
                    currentRow.AddCell(cell);
                }

                iRow++;

            }

        }

        private void RefreshGridTableWithNoHeader()
        {
            //add placeholder header
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            headerGroup.AddHeader(mstrTableType, null, 0);
            mlstHeaderGroups.Add(headerGroup);

            //Get table content
            IList<IWebElement> rows = mElement.FindElements(By.XPath(".//tbody/tr"));
            int iRow = 0;
            foreach (IWebElement row in rows)
            {
                DlkRow currentRow = GetRow(iRow);
                IWebElement cellElm = row.FindElement(By.XPath(".//td"));
                DlkCell currentCell = new DlkCell(cellElm, iRow, mlstHeaderGroups[0].lstHeaders[0]);
                currentRow.AddCell(currentCell);

                iRow++;

            }
        }

        protected DlkRow GetRow(int rowIndex)
        {
            if (rowIndex < mlstRows.Count)
            {
                return mlstRows[rowIndex];
            }
            else
            {
                mlstRows.Add(new DlkRow(rowIndex));
                return mlstRows[rowIndex];
            }
        }

        protected Boolean HeaderExists(String sHeaderText)
        {
            Boolean bFound = false;
            foreach (DlkHeaderGroup headerGrp in mlstHeaderGroups)
            {
                foreach (DlkHeader header in headerGrp.lstHeaders)
                {
                    if (header.HeaderText.ToLower() == sHeaderText.ToLower())
                    {
                        bFound = true;
                        break;
                    }
                }
                if (bFound)
                {
                    break;
                }
            }

            return bFound;
        }

        private IWebElement GetHeader(int index)
        {
            IWebElement header = null;
            Boolean bFound = false;
            foreach (DlkHeaderGroup headerGrp in mlstHeaderGroups)
            {
                header = (IWebElement) headerGrp.lstHeaders.ElementAt(index);
                if (header != null) bFound = true;

                if (bFound)
                {
                    break;
                }
            }

            return header;
        }

        private DlkHeader GetHeader(String sHeaderText)
        {
            DlkHeader header = null;
            Boolean bFound = false;

            if (headerGroupGridStyleOne != null)
            {

                DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
                FindElement();
                Thread.Sleep(5000);
                IList<IWebElement> headers = mElement.FindElements(By.CssSelector("tbody>tr>th>:not(div)"));
                if (headers.Count == 0)
                    headers = mElement.FindElements(By.XPath(".//thead/tr/th"));

                for (int i = 0; i < headers.Count; i++)
                {
                    headerGroup.AddHeader(mstrTableType, headers[i], i);
                }

                foreach (DlkHeader aHeader in headerGroup.lstHeaders)
                {
                    if (aHeader.HeaderText.ToLower() == sHeaderText.ToLower())
                    {
                        header = aHeader;
                        bFound = true;
                        break;
                    }
                }

            }
            else
            {
                foreach (DlkHeaderGroup headerGrp in mlstHeaderGroups)
                {
                    foreach (DlkHeader aHeader in headerGrp.lstHeaders)
                    {
                        if (aHeader.HeaderText.ToLower() == sHeaderText.ToLower())
                        {
                            header = aHeader;
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)
                    {
                        break;
                    }
                }
            }


            return header;
        }

        private DlkHeader GetHeaderContainsText(String sHeaderText)
        {
            DlkHeader header = null;
            Boolean bFound = false;
            foreach (DlkHeaderGroup headerGrp in mlstHeaderGroups)
            {
                foreach (DlkHeader aHeader in headerGrp.lstHeaders)
                {
                    if (aHeader.HeaderText.ToLower().Contains(sHeaderText.ToLower()))
                    {
                        header = aHeader;
                        bFound = true;
                        break;
                    }
                }
                if (bFound)
                {
                    break;
                }
            }

            return header;
        }

        private DlkCell GetCellWithHeaderText(int iRow, String sHeaderText)
        {
            if (iRow < mlstRows.Count)
            {
                int curCol;
                DlkRow row = mlstRows[iRow];
                if (int.TryParse(sHeaderText, out curCol))
                {
                    return row.lstCells[Convert.ToInt32(curCol)];
                }

                foreach (DlkCell cell in row.lstCells)
                {
                    if (cell.header.HeaderText.ToLower().Contains("checkbox") && sHeaderText.ToLower().Contains("checkbox"))
                    {
                        cell.header.HeaderText = "checkbox";
                        return cell;
                    }
                    if (cell.header.HeaderText.ToLower() == sHeaderText.ToLower())
                    {
                        return cell;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        private DlkCell GetCellWithCoordinates(int iRow, int iSubRow, int iCol)
        {
            DlkCell currentCell = null;
            if (iRow < mlstRows.Count)
            {
                if (iSubRow == -1)
                {
                    DlkRow row = mlstRows[iRow];
                    currentCell = row.lstCells[iCol];

                }
                else
                {
                    List<DlkRow> tempList = mDicSubRows.ElementAt(iRow).Value;
                    DlkRow row = tempList[iSubRow];
                    currentCell = row.lstCells[iCol];
                }

                return currentCell;
            }
            else
            {
                return null;
            }
        }

        private DlkCell GetCellWithHeaderText(int iRow, int iSubRow, String sHeaderText)
        {
            if (iRow < mlstRows.Count)
            {
                List<DlkRow> tempList = mDicSubRows.ElementAt(iRow).Value;
                DlkRow row = tempList[iSubRow];
                foreach (DlkCell cell in row.lstCells)
                {
                    if (cell.header.HeaderText.ToLower() == sHeaderText.ToLower())
                    {
                        return cell;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        private string GetAllHeaders()
        {
            string headersXpath = "//*[@id='gridViewTable_wrapper']//tr[@role='row']/th[string-length(text()) > 0]",
                   h = null;

            var headers = mElement.FindElements(By.XPath(headersXpath));

            for (int i = 0; i < headers.Count; i++)
            {
                h += '"' + headers[i].Text + '"' + ",";
            }

            return h.TrimEnd(',');
        }

        private void AssertTableAndCSVPerRow(string tableContents, string csvContent, int rowNum, int pageNum)
        {
            int incrementedRowNumber = (rowNum + (50 * (pageNum - 1)));

            if (rowNum == 0)
                DlkAssert.AssertEqual("Header comparison", tableContents, csvContent);
            else if (pageNum == 1)
                DlkAssert.AssertEqual("Row [" + rowNum + "]: CompareTableVsCSV()", tableContents, csvContent);
            else
                DlkAssert.AssertEqual("Row: [" + incrementedRowNumber + "]: CompareTableVsCSV()", tableContents, csvContent);
        }
        private decimal GetNumberOfPages(int totalSearchResults, int displayedNumberOfRecordsPerPage)
        {
            decimal decimalResults = decimal.Divide(totalSearchResults, displayedNumberOfRecordsPerPage);
            int resultsWithoutDecimalPlace = Convert.ToInt32(decimalResults),
                totalPages = 0;

            // Identify if has decimal value
            if (decimalResults > resultsWithoutDecimalPlace)
                totalPages = resultsWithoutDecimalPlace + 1;
            else
                totalPages = resultsWithoutDecimalPlace;

            return totalPages;
        }


        #region Verify methods
        [RetryKeyword("VerifyTableRowWithColumnValueExists", new String[] {    "1|text|Column Header|Task Order",
                                                                                "2|text|Value|Securty Services - Network Rebuild",
                                                                                "3|text|Expected Result|TRUE"})]
        public virtual void VerifyTableRowWithColumnValueExists(String ColumnHeader, String Value, String TrueOrFalse)
        {
            String columnHeader = ColumnHeader;
            String value = Value;
            String expectedResult = TrueOrFalse;

            String[] inputs = new String[] { columnHeader, value, expectedResult };

            this.PerformAction(() =>
            {
                bool blnFound = false;

                Initialize();

                if (HeaderExists(columnHeader))
                {
                    for (int i = 0; i < mlstRows.Count; i++)
                    {

                        foreach (DlkCell cell in mlstRows[i].lstCells)
                        {
                            if (cell.header.HeaderText.ToLower() == columnHeader.ToLower())
                            {
                                if (cell.GetValue(mstrTableType) == value)
                                {
                                    blnFound = true;
                                    break;
                                }
                            }

                        }
                        if (blnFound)
                        {
                            break;
                        }

                    }

                    DlkAssert.AssertEqual("VerifyTableRowWithColumnValueExists()", Convert.ToBoolean(expectedResult), blnFound);
                }
                else
                {
                    throw new Exception("VerifyTableRowWithColumnValueExists() failed. Column '" + columnHeader + "' not found.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTableCellValue", new String[] {   "1|text|Row|O{Row}",
                                                                "2|text|Column Header|Line*",
                                                                "3|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellValue(String Row, String ColumnHeader, String ExpectedValue)
        {
            String row = Row;
            String columnHeader = ColumnHeader;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { row, columnHeader, expectedValue};

            this.PerformAction(() =>
            {
                //converting sRow from 1 based to zero based index
                int iRow = Convert.ToInt32(row) - 1;

                Initialize();

                if (iRow < mlstRows.Count)
                {
                    Boolean bFound = false;
                    DlkCell currentCell = null;
                    foreach (DlkCell cell in mlstRows[iRow].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == columnHeader.ToLower())
                        {
                            bFound = true;
                            currentCell = cell;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        throw new Exception("VerifyTableCellValue() failed. Column '" + columnHeader + "' not found in table.");
                    }
                    else
                    {
                        DlkAssert.AssertEqual("VerifyTableCellValue()", expectedValue, currentCell.GetValue(mstrTableType));
                    }
                }
                else
                {
                    throw new Exception("VerifyTableCellValue() failed. Row '" + row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTableCellNumberFormat", new String[] {   "1|text|Row|O{Row}",
                                                                "2|text|Column Header|Line*",
                                                                "3|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellNumberFormat(String Row, String ColumnHeader, String ExpectedValue)
        {
            String row = Row;
            String columnHeader = ColumnHeader;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { row, columnHeader, expectedValue };

            this.PerformAction(() =>
            {
                //converting sRow from 1 based to zero based index
                int iRow = Convert.ToInt32(row) - 1;

                Initialize();

                if (iRow < mlstRows.Count)
                {
                    Boolean bFound = false;
                    Boolean bCorrect = true;
                    DlkCell currentCell = null;

                    foreach (DlkCell cell in mlstRows[iRow].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == columnHeader.ToLower())
                        {
                            Char[] value = cell.GetValue("gridtable").ToArray();

                            Boolean beginCounter = true;

                            int counter = 0;
                            for (int i = 0; i < value.Length; i++)
                            {
                                switch (value[i])
                                {
                                    case ',':
                                    case '.':
                                        if (beginCounter)
                                            beginCounter = false;
                                        else
                                        {
                                            if (counter != 3)
                                            {
                                                i = value.Length;
                                                throw new Exception("VerifyTableCellNumberFormat() has an invalid format");
                                            }
                                            else counter = 0;
                                        }

                                        break;

                                    case '1':
                                    case '2':
                                    case '3':
                                    case '4':
                                    case '5':
                                    case '6':
                                    case '7':
                                    case '8':
                                    case '9':
                                    case '0': if (!beginCounter)
                                            counter++;
                                        break;
                                    default:
                                        throw new Exception("VerifyTableCellNumberFormat() has an invalid format");

                                }
                            }

                            bFound = true;
                            currentCell = cell;
                        }
                    }

                    if (!bFound)
                    {
                        throw new Exception("VerifyTableCellNumberFormat() failed. Column '" + columnHeader + "' not found in table.");
                    }
                    else
                    {
                        if (bCorrect)
                            throw new Exception("Number: " + currentCell.GetValue(mstrTableType) + " is in the correct format.");
                    }
                }
                else
                {
                    throw new Exception("VerifyTableCellNumberFormat() failed. Row '" + row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTableCellValueContains", new String[] {"1|text|Row|O{Row}",
                                                                "2|text|Column Header|Line*",
                                                                "3|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellValueContains(String Row, String ColumnHeader, String ExpectedValue)
        {
            String row = Row;
            String columnHeader = ColumnHeader;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { row, columnHeader, expectedValue };

            this.PerformAction(() =>
            {
                //converting sRow from 1 based to zero based index
                int iRow = Convert.ToInt32(row) - 1;

                Initialize();

                if (iRow < mlstRows.Count)
                {
                    Boolean bFound = false;
                    DlkCell currentCell = null;
                    foreach (DlkCell cell in mlstRows[iRow].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == columnHeader.ToLower())
                        {
                            bFound = true;
                            currentCell = cell;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        throw new Exception("VerifyTableCellValueContains() failed. Column '" + columnHeader + "' not found in table.");
                    }
                    else
                    {
                        bool bContains = false;

                        if (currentCell.GetValue(mstrTableType).Contains(expectedValue))
                            bContains = true;

                        DlkAssert.AssertEqual("VerifyTableCellValueContains()", true, bContains);
                    }
                }
                else
                {
                    throw new Exception("VerifyTableCellValueContains() failed. Row '" + row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTableCellValueBySubRowColumn", new String[] {  "1|text|Row Index|O{Row}",
                                                                            "2|text|Sub Row Index|O{SubRow}",
                                                                            "3|text|Column Index|Line*",
                                                                            "4|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellValueBySubRowColumn(String Row, String SubRow, String Column, String ExpectedValue)
        {
            String row = Row;
            String subRow = SubRow;
            String columnIndex = Column;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { row, subRow, columnIndex, expectedValue };

            this.PerformAction(() =>
            {
                //converting sRow from 1 based to zero based index
                int iRow = Convert.ToInt32(row) - 1;
                int iSubRow = Convert.ToInt32(subRow) - 1;
                int iCol = Convert.ToInt32(columnIndex) - 1;

                Initialize();

                if (iRow < mlstRows.Count)
                {
                    if (iSubRow < mDicSubRows.ElementAt(iRow).Value.Count)
                    {
                        if (iCol < mlstHeaderGroups[0].lstHeaders.Count)
                        {
                            DlkCell currentCell = mDicSubRows.ElementAt(iRow).Value[iRow].lstCells[iCol];
                            DlkAssert.AssertEqual("VerifyTableCellValueBySubRowColumn()", expectedValue, currentCell.GetValue(mstrTableType));
                        }
                        else
                        {
                            throw new Exception("VerifyTableCellValueByRowColumn() failed. Column '" + columnIndex + "' is not valid. The table has " + Convert.ToString(mlstHeaderGroups[0].lstHeaders.Count) + " columns.");
                        }
                    }
                    else
                    {
                        throw new Exception("VerifyTableCellValueByRowColumn() failed. Sub Row '" + subRow + "' is not valid. The table has " + Convert.ToString(mDicSubRows.ElementAt(iRow).Value.Count) + " sub rows.");
                    }

                }
                else
                {
                    throw new Exception("VerifyTableCellValueByRowColumn() failed. Row '" + row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
                }
            }, new String[] { "retry" });

        }

        [RetryKeyword("VerifyTableCellValueByRowColumn", new String[] {    "1|text|Row Index|O{Row}",
                                                                            "2|text|Column Index|Line*",
                                                                            "3|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellValueByRowColumn(String Row, String Column, String ExpectedValue)
        {
            String row = Row;
            String columnIndex = Column;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { row, columnIndex, expectedValue };

            this.PerformAction(() =>
            {
                //converting sRow from 1 based to zero based index
                int iRow = Convert.ToInt32(row) - 1;
                int iCol = Convert.ToInt32(columnIndex) - 1;

                Initialize();

                if (iRow < mlstRows.Count)
                {
                    if (iCol < mlstHeaderGroups[0].lstHeaders.Count)
                    {
                        DlkCell currentCell = mlstRows[iRow].lstCells[iCol];
                        DlkAssert.AssertEqual("VerifyTableCellValueByRowColumn()", expectedValue, currentCell.GetValue(mstrTableType));
                    }
                    else
                    {
                        throw new Exception("VerifyTableCellValueByRowColumn() failed. Column '" + columnIndex + "' is not valid. The table has " + Convert.ToString(mlstHeaderGroups[0].lstHeaders.Count) + " columns.");
                    }

                }
                else
                {
                    throw new Exception("VerifyTableCellValueByRowColumn() failed. Row '" + row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTableCellComboBoxListContains", new String[] {    "1|text|Row Index|O{Row}",
                                                                            "2|text|Column Index|Line*",
                                                                            "3|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellComboBoxListContains(String Row, String ColumnHeader, String Value)
        {
            Initialize();

            int iRow = Convert.ToInt32(Row) - 1;

            DlkCell cell = GetCellWithHeaderText(iRow, ColumnHeader);

            if (cell != null)
            {
                cell.VerifyComboBoxListContains(Value);
            }
            else
            {
                throw new Exception("VerifyTableCellComboBoxListContains() failed. Unable to get cell with Row=" + Row + " and Column=" + ColumnHeader);
            }


        }

        [RetryKeyword("VerifyTableCellValueNoHeader", new String[] {    "1|text|Row Index|O{Row}",
                                                                            "2|text|Column Index|Line*",
                                                                            "3|text|Expected Value|Sample Value"})]
        public virtual void VerifyTableCellValueNoHeader(String Row, String Column, String ExpectedResult)
        {
            //converting sRow from 1 based to zero based index
            int iRow = Convert.ToInt32(Row) - 1;

            bool actual = false;

            Initialize();

            if (iRow < mlstRows.Count)
            {
                DlkCell currentCell = null;

                DlkRow row = mlstRows[iRow];
                currentCell = row.lstCells[Convert.ToInt32(Column)-1];

                if (currentCell.element.Text.ToLower().Contains(ExpectedResult.ToLower()))
                    actual = true;

                DlkAssert.AssertEqual("VerifyTableCellValueNoHeader(): Actual = " + currentCell.element.Text + "; Expected = " + ExpectedResult, true, actual);

            }
        }


        [RetryKeyword("VerifyElementExists", new String[] {"1|text|Row Index|O{Row}",
                                                            "2|text|Column Header|Line*",
                                                            "3|text|XPATH|Sample Value ",
                                                            "4|text|Expected (TRUE or FALSE)|TRUE"})]
        public virtual void VerifyElementExists(String Row, String ColumnHeader, String XPATH, String TrueOrFalse)
        {
            String row = Row;
            String columnHeader = ColumnHeader;
            String xpath = XPATH;
            String expectedValue = TrueOrFalse;

            String[] inputs = new String[] { row, columnHeader, xpath, expectedValue };

            this.PerformAction(() =>
            {
                int iRow = Convert.ToInt32(row) - 1;

                Initialize();

                if (iRow < mlstRows.Count)
                {

                    Boolean bFound = false;
                    DlkCell currentCell = null;
                    foreach (DlkCell cell in mlstRows[iRow].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == columnHeader.ToLower())
                        {
                            bFound = true;
                            currentCell = cell;
                            break;
                        }
                    }

                    if (bFound)
                    {
                        try
                        {
                            //IWebElement searchedElement = currentCell.element.FindElement(By.XPath(sXpath));
                            //bool elementFound = searchedElement == null ? false : true;
                            //DlkAssert.AreEqual("VerifyElementExists()", Convert.ToBoolean(sExpectedValue), elementFound);

                            DlkBaseControl elmControl = new DlkBaseControl("Element", new DlkBaseControl("Cell", currentCell.element), "XPATH", xpath);
                            elmControl.FindElement();
                            elmControl.VerifyExists(Convert.ToBoolean(expectedValue));

                        }
                        catch (Exception e)
                        {
                            throw new Exception("VerifyElementExists() failed.", e);
                        }
                    }
                    else
                    {
                        throw new Exception("VerifyElementExists() failed. Column '" + columnHeader + "' not found in table.");
                    }
                }
                else
                {
                    throw new Exception("VerifyElementExists() failed. Row '" + row + "' is not valid. The table has " + Convert.ToString(mlstRows.Count) + " rows.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTableColumnHeaders", new String[] { "1|text|Expected header texts|Header1~Header2~Header3" })]
        public virtual void VerifyTableColumnHeaders(String ExpectedHeaderTexts)
        {
            String expectedHeaders = ExpectedHeaderTexts;

            this.PerformAction(() =>
            {
                String strActualHeaders = "";
                Boolean bFirst = true;

                Initialize();
                foreach (DlkHeaderGroup headerGroup in mlstHeaderGroups)
                {
                    foreach (DlkHeader header in headerGroup.lstHeaders)
                    {
                        if (!bFirst)
                        {
                            strActualHeaders = strActualHeaders + "~";
                        }
                        strActualHeaders = strActualHeaders + header.HeaderText;
                        bFirst = false;
                    }
                }
                DlkAssert.AssertEqual("VerifyTableColumnHeaders()", expectedHeaders.ToLower(), strActualHeaders.ToLower());
            }, expectedHeaders);
        }

        [RetryKeyword("VerifyTableRowCount", new String[] { "1|text|Expected Row Count|5" })]
        public virtual void VerifyTableRowCount(String ExpectedRowCount)
        {
            String expectedRowCount = ExpectedRowCount;

            this.PerformAction(() =>
            {
                int iExpectedRowCount = Convert.ToInt32(expectedRowCount);

                Initialize();

                DlkAssert.AssertEqual("VerifyTableRowCount()", iExpectedRowCount, mlstRows.Count);
            }, ExpectedRowCount);

        }

        [Keyword("VerifyTableRowDifference", new String[] { "1|text|Row Count|O{Count}", "2|text|Rows Deleted|1"})]
        public virtual void VerifyTableRowDifference(String RowCount, String RowsDeleted)
        {
            int rowCount = Convert.ToInt32(RowCount);
            int rowsDeleted = Convert.ToInt32(RowsDeleted);
            int rowCountDiff = rowCount - rowsDeleted;
            bool actualVal = false;

            Initialize();

            if (rowCountDiff == mlstRows.Count)
            {
                actualVal = true;
            }

            DlkAssert.AssertEqual("VerifyTableRowDifference()",true, actualVal);
        }

        [Keyword("GetTableRowCount", new String[] {"1|text|VariableName|MyRowCount"})]
        public virtual void GetTableRowCount(String VariableName)
        {
            bool blnFound = false;

            Initialize();

            try
            {
                DlkVariable.SetVariable(VariableName,Convert.ToString(mlstRows.Count));
                blnFound = true;

            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("Exception:" + e.Message);
                throw e;
            }

            if (blnFound)
            {
                DlkLogger.LogInfo("Successfully executed GetTableRowCount().");
            }
            else
            {
                throw new Exception("GetTableRowCount() failed.");
            }


        }

        [RetryKeyword("VerifyTableBySheet", new String[] { "1|text|Sheet Name|ExpectedSheet1" })]
        public virtual void VerifyTableBySheet(String SheetName)
        {
            String expectedSheet = SheetName;

            this.PerformAction(() =>
            {
                String colHeaders = DlkExcelApi.GetColumnHeaders(expectedSheet);
                String[] cols = colHeaders.Split('~');
                VerifyTableColumnHeaders(colHeaders);
                for (int iRow = 2; iRow <= DlkExcelApi.GetRowCount(expectedSheet); iRow++)
                {
                    DlkExcelApi.SetCurrentRow(expectedSheet, iRow);
                    foreach (String col in cols)
                    {
                        VerifyTableCellValue(Convert.ToString(iRow - 1), col, DlkExcelApi.GetCellData(expectedSheet, col));
                    }
                }
            }, SheetName);
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Initialize();
                    VerifyExists(Convert.ToBoolean(expectedValue));
                }, expectedValue);
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyIfContentExist", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyIfContentExist(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Initialize();
                    RefreshDataTable();

                    bool bContentExist = (this.mlstRows.Count > 0) ? true : false;
                    DlkAssert.AssertEqual("VerifyIfContentExist()", Convert.ToBoolean(expectedValue), bContentExist);
                }, TrueOrFalse);
        }
        #endregion

        [Keyword("SetReorderTab", new String[] {"1|text|Old ReOrder Number / Row| 1",
                                                "2|text|New ReOrder Number|2"})]
        public void SetReorderTab(String OldReOrderNumber, String NewReOrderNumber)
        {
            try
            {
                int oldNum = Convert.ToInt32(OldReOrderNumber) - 1;
                String newNum = NewReOrderNumber;
                String actualResult = "";

                while (!this.Exists())
                {
                    Thread.Sleep(500);
                }

                mapTextBox();

                if (oldNum >= txtElems.Count)
                {
                    throw new Exception("SetReorderTab() failed. Index is greater than the number of result items in the result list.");
                }
                else
                {
                    DlkTextBox txtSelected = txtElems.ElementAt(oldNum).Value;
                    txtSelected.Click();
                    txtSelected.Set(newNum);
                    actualResult = txtSelected.GetValue().ToString();
                    DlkLogger.LogInfo("Successfully executed SetReorderTab().");
                }

                DlkAssert.AssertEqual("SetReorderTab()", newNum, actualResult);

            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }

        }

        Dictionary<String, DlkTextBox> txtElems;
        private void mapTextBox()
        {
            int elemInd = 1;
            IList<IWebElement> lstElem = DlkEnvironment.AutoDriver.FindElements(By.TagName("input"));
            txtElems = new Dictionary<string, DlkTextBox>();

            int lstElemCount = lstElem.Count;
            int indx = 0;
            foreach (IWebElement elem in lstElem)
            {


                var inputType = elem.GetAttribute("type");

                if (inputType.Equals("text"))
                {
                    txtElems.Add(elemInd.ToString(), new DlkTextBox("txt" + elemInd, elem));
                    elemInd++;
                }

                indx++;

            }

        }
    }
}

