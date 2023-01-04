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

using System.Runtime.InteropServices;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{

    [ControlType("ClassicTable")]
    public class DlkClassicTable : DlkTable
    {

        public DlkClassicTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkClassicTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkClassicTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }



        [Keyword("RefreshTableData")]
        public override void RefreshTableData()
        {
            mstrTableType = "gridtable";
            FindElement(); // find the table

            mlstRows = new List<DlkRow>();
            mlstHeaderGroups = new List<DlkHeaderGroup>();

            //Get Headers
            DlkHeaderGroup headerGroup = new DlkHeaderGroup(0);
            
            //.../tr[2] - headers
            IList<IWebElement> headers = mElement.FindElements(By.XPath("./tbody/tr[1]/td"));

            //check header group if it would match table form
            for (int i = 0; i < headers.Count; i++)
            {
                headerGroup.AddHeader(mstrTableType, headers[i], i);
            }
            mlstHeaderGroups.Add(headerGroup);

            //Contents start at tr[3] to tr[n-2]
            //.../tr[3] - contents
            //Get Table Content
            IList<IWebElement> rows = mElement.FindElements(By.XPath("./tbody/tr"));
            int totalRowCount = rows.Count;
            int iRow = 0;

            DlkLogger.LogInfo("Retrieving table rows, pls wait...");

            //for(int i=2; i < (totalRowCount -2)-1; i++)
            for(int i=2; i < totalRowCount + 1; i++)
            {
                //IWebElement row = rows[i];

                DlkRow currentRow = GetRow(iRow);
                //IList<IWebElement> cells = row.FindElements(By.XPath("./tbody/tr/td"));
                IList<IWebElement> cells = mElement.FindElements(By.XPath("./tbody/tr["+i+"]/td"));
                for (int iCell = 0; iCell < cells.Count; iCell++)
                {
                    DlkCell currentCell = new DlkCell(cells[iCell], iRow, mlstHeaderGroups[0].lstHeaders[iCell]);
                    currentRow.AddCell(currentCell);
                }
                iRow++;
            }
        }
    }
}

