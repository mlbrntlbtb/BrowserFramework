using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;

//using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{

    [ControlType("NotesTable")]
    public class DlkNotesTable : DlkTable
    {

        private DlkTextBox mFilterlabel;// = new DlkTextBox("filter", "XPATH", "//div[@id='myNotes_filter']//input");

        public DlkNotesTable(String ControlName, String SearchType, String SearchValue, DlkTextBox filterLabel)
            : base(ControlName, SearchType, SearchValue) 
        { 
            mFilterlabel = filterLabel;
        }
        public DlkNotesTable(String ControlName, String SearchType, String[] SearchValues, DlkTextBox filterLabel)
            : base(ControlName, SearchType, SearchValues) 
        { 
            mFilterlabel = filterLabel;
        }
        public DlkNotesTable(String ControlName, IWebElement ExistingWebElement, DlkTextBox filterLabel)
            : base(ControlName, ExistingWebElement) 
        { 
            mFilterlabel = filterLabel;
        }

        private new void Initialize()
        {
            Thread.Sleep(1000);
            mFilterlabel.FindElement();
            base.Initialize();
        }

        public List<DlkCell> GetRowValuesOfColumn(String sColumnHeader)
        {            
            List<DlkCell> values = new List<DlkCell>();

            if (HeaderExists(sColumnHeader))
            {
                for (int i = 0; i < mlstRows.Count; i++)
                {

                    foreach (DlkCell cell in mlstRows[i].lstCells)
                    {
                        if (cell.header.HeaderText.ToLower() == sColumnHeader.ToLower())
                        {
                            try
                            {
                                values.Add(cell);             
                            }
                            catch (Exception e)
                            {
                                throw new Exception("Exception encountered in GetRowValuesOfColumn", e);
                            }
                        }
                    }                    
                }
            }

            return values;
        }

        #region Verify methods
        [RetryKeyword("VerifyFilterColumnValue", new string[]{ "1|text|Expected Result|TRUE"})]
        public void VerifyFilterContainsValue(String TrueOrFalse)
        {
            String expectedResult = TrueOrFalse;

            this.PerformAction(() =>
                {
                    try
                    {
                        Initialize();

                        string filterValue = mFilterlabel.GetValue();

                        DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
                        //IList<IWebElement> headers = mElement.FindElements(By.CssSelector("tbody>tr>th>a"));

                        //for (int i = 0; i < headers.Count; i++)
                        //{
                        //    headerGroup.AddHeader(mstrTableType, headers[i], i);
                        //}

                        bool actualResult = true;
                        bool rowCheck = false;
                        //Update Table Content
                        //IList<IWebElement> rows = mElement.FindElements(By.CssSelector("tbody>tr"));
                        foreach (DlkRow row in mlstRows.ToList())
                        {

                            foreach (DlkCell cell in row.lstCells)
                            {
                                if (cell.header.HeaderText != "")
                                {
                                    string cellValue = cell.GetValue(mstrTableType);
                                    if (!cellValue.Contains(filterValue))
                                    {
                                        rowCheck = true;
                                        break;
                                    }
                                }
                            }

                            if (rowCheck == false)
                            {
                                //did not find filter word in a row, this is false verification
                                actualResult = false;
                                break;
                            }
                            else
                            {
                                //prepare checking for next row
                                rowCheck = false;
                            }
                        }


                        DlkAssert.AssertEqual("VerifyFilterColumnValue()", Convert.ToBoolean(expectedResult), actualResult);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }, new String[]{"retry"});
        }
        #endregion
    }

}

