using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls.Controls.WebControls
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

    public class DlkCell
    {
        public IWebElement element;


        public DlkCell(IWebElement cellElement)
        {
            element = cellElement;
        }


        public void ClickButton(String strTableType)
        {
            IList<IWebElement> buttons;

            buttons = element.FindElements(By.XPath("//a"));
            if (buttons.Count > 0)
            {
                DlkLink cellButton = new DlkLink("Cell Button", buttons.First());
                cellButton.Click();
            }
            else
            {
                throw new Exception("ClickButton() failed. Cell does not contain a button.");
            }

        }
    }

    [ControlType("GritterTable")]
    public class DlkGritterTable
    {
        //protected virtual void Initialize()
        //{
        //    FindElement();
        //}

        protected virtual void RefreshGritterTable()
        {
            //IWebElement tableContent = mElement.FindElement(By.XPath(".//div[@id='CompareContent']"));

            //rows = tableContent.FindElements(By.XPath("/div"));

            //int iRow = 0;
            //foreach (IWebElement row in rows)
            //{
                //IWebElement currentRow = row;
                //IList<DlkCell> cells = row.FindElements(By.XPath("./div"));
                //for (int iCell = 0; iCell < cells.Count; iCell++)
                //{
                    //DlkCell currentCell = new DlkCell(cells[iCell], iRow);
                    //currentRow.AddCell(currentCell);
                //}
                //iRow++;
            //}

        }
    }
}
