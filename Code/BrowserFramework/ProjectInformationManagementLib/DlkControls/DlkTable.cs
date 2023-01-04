using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using ProjectInformationManagementLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;

        private String mstrBodyXPath = "//tbody/tr";
        private List<IWebElement> mlstHeaders;
        private List<IWebElement> mlstBody;
        int colIndex;
        #endregion

        #region CONSTRUCTOR
        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
        }

        #endregion

        #region KEYWORDS
        [Keyword("ClickCellControl", new[] { "1|text|Conrol|button",
                                             "2|text|ControlColumn|1",
                                             "3|text|ColumnHeader|Name",
                                             "4|text|ColumnValue|Sample"})]
        public virtual void ClickCellControl(String Control, String ControlColumn, String ColumnHeader, String ColumnValue)
        {
            try
            {
                string _control = string.Empty;

                FindElement();
                Initialize();

                if (Control.ToLower().Equals("button")) _control = "button";
                if (Control.ToLower().Equals("checkbox")) _control = "checkbox";

                colIndex = GetColumnIndexByHeader(ColumnHeader);
                int rowIndex = GetRowIndexByColumnValue(ColumnValue) + 1;

                IWebElement control = mElement.FindElement(By.XPath("//tbody/tr[" + rowIndex + "]/td[" + Convert.ToInt32(ControlColumn) + "]//" + _control));
                if(control != null)
                {
                    control.Click();
                    DlkLogger.LogInfo("ClickCellControl(): click successful");
                }
                else
                {
                    DlkLogger.LogInfo("ClickCellControl(): control to click can't be found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellControl() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellButton", new[] { "1|text|RowValue|Client",
                                            "2|text|Column|1" })]
        public virtual void ClickCellButton(String RowValue, String Column)
        {
            try
            {
                FindElement();
                Initialize();

                //IWebElement button = mElement.FindElement(By.XPath("//tr[" + Row + "]/td[" + Column + "]//button"));
                IWebElement button = mElement.FindElement(By.XPath("//td[@title='" + RowValue + "']/preceding-sibling::td//button"));
                if (button != null)
                {
                    button.Click();
                    DlkLogger.LogInfo("ClickCellButton(): click successful");
                }
                else
                {
                    DlkLogger.LogInfo("ClickCellButton(): button to click can't be found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new[] { "1|text|Column Header|Line*" })]
        public virtual void VerifyCellValue(String Row, String Column, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindElement();
                DlkAssert.AssertEqual("VerifyCellValue() : " + mControlName, ExpectedValue, GetCellValue(Row, Column));

            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        #endregion

        #region METHODS
        public string GetCellValue(string row, string column)
        {
            int rowNumber = Convert.ToInt32(row);
            int colNumber = Convert.ToInt32(column);
            int colNumber_adjust = colNumber;
            IWebElement cellInLocation = null;
            int colspan = 0;

            List<IWebElement> cells = mElement.FindElements(By.XPath(".//tbody/tr[not(contains(@style,'none'))][" + rowNumber + "]/td[not(contains(@style,'none'))]")).ToList();

            for (int i = 0; i < cells.Count; i++)
            {
                colNumber_adjust -= colspan;
                if (colNumber_adjust == (i + 1))
                {
                    cellInLocation = cells[i];
                    break;
                }

                colspan = cells[i].GetAttribute("colspan") != null ?
                    (Convert.ToInt32(cells[i].GetAttribute("colspan")) - 1) :
                    0;

            }
            return DlkString.ReplaceCarriageReturn(cellInLocation.Text.Trim(), "");
        }

        private int GetRowIndexByColumnValue(String ColumnValue)
        {
            int index = -1;
            Boolean bCont = true;

            RefreshTableBody();
            while (bCont && index == -1)
            {
                for (int i = 0; i < mlstBody.Count; i++)
                {
                    String tBodyText = mlstBody[i].Text.Replace("\r\n", "|");
                    String[] arrBodyText = tBodyText.Split('|');
                    if (ColumnValue.Trim() == arrBodyText[colIndex - 1])
                    {
                        index = i;
                        break;
                    }
                }

                if (index > -1)
                {
                    bCont = false;
                }
            }

            return index;
        }

        private int GetColumnIndexByHeader(String columnHeader)
        {
            int index = -1;
            Boolean bCont = true;

            RefreshHeaders();
            while (bCont && index == -1)
            {
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    if (columnHeader.ToLower().Trim() == mlstHeaders[i].Text.ToLower().Trim())
                    {
                        index = 1;
                        break;
                    }
                }

                if (index > -1)
                {
                    bCont = false;
                }
            }
            return index;
        }

        private void RefreshHeaders()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            FindElement();

            mlstHeaders = new List<IWebElement>();

            //IList<IWebElement> headers = mElement.FindElements(By.ClassName(mstrHeaderClass));
            IList<IWebElement> headers = mElement.FindElements(By.XPath("//table/thead/tr/th"));
            foreach (IWebElement columnHeader in headers)
            {
                if (columnHeader.GetCssValue("display") != "none")
                {
                    mlstHeaders.Add(columnHeader);
                }
            }
        }
        private void RefreshTableBody()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            FindElement();

            mlstBody = new List<IWebElement>();
            IList<IWebElement> body = mElement.FindElements(By.XPath(mstrBodyXPath));
            foreach (IWebElement rowBody in body)
            {
                if (rowBody.GetCssValue("display") != "none")
                {
                    mlstBody.Add(rowBody);
                }
            }

        }
        #endregion


    }
}
