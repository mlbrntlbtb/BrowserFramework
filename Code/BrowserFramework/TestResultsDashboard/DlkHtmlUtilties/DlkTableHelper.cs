using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TestResultsDashboard.DlkHtmlUtilities
{

    public static class DlkTableHelper
    {
        public static TableHeaderRow CreateHeaderRow(List<String> ColumnNames)
        {
            TableHeaderRow mHeaderRow = new TableHeaderRow();
            for (int i = 0; i < ColumnNames.Count; i++)
            {
                String mCol = ColumnNames[i];
                // check to see if we are passing column width ==> Width:125px~Test Start
                if (mCol.StartsWith("Width:"))
                {
                    String[] mData = mCol.Split('~');
                    mCol = mData[1];
                    String mWidth = mData[0].Replace("Width:", "").Replace("~", "").Trim();
                    mCol = "<div style=\"width:" + mWidth + "\">" + mCol + "</div>";
                }

                TableHeaderCell mHeaderCell = new TableHeaderCell();
                mHeaderCell.ID = "Header~" + i.ToString();
                mHeaderCell.Text = mCol;
                mHeaderRow.Cells.Add(mHeaderCell);
            }
            return mHeaderRow;
        }

        public static TableRow CreateRow(int iRowNumber, List<String> CellValues)
        {
            TableRow mTableRow = new TableRow();
            for (int i = 0; i < CellValues.Count; i++)
            {
                TableCell mCell = new TableCell();
                String mCellVal = CellValues[i];

                // check for style ==> format Style:min-width:150px;max-width:250px;~Cell Value
                if (mCellVal.StartsWith("Style:"))
                {
                    // detemine where Props end and assign to variable
                    int iEnd = mCellVal.IndexOf("~");
                    String mStyle = mCellVal.Substring(0, iEnd);
                    
                    // set cell value
                    mCellVal = mCellVal.Substring(iEnd + 1);

                    // add properties
                    mStyle= mStyle.Replace("Style:", "");
                    String[] mStylePairs = mStyle.Split(';');
                    foreach (String mPair in mStylePairs)
                    {
                        if (mPair != "")
                        {
                            String[] mData = mPair.Split(':');
                            mCell.Style.Add(mData[0], mData[1]);

                        }
                    }
                }

                mCell.ID = "Cell~" + iRowNumber.ToString() + "~" + i.ToString();
                mCell.Text = mCellVal;
                mTableRow.Cells.Add(mCell);
            }
            return mTableRow;
        }
    }
}