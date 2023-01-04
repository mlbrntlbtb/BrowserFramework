using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkUtility
{
    public static class DlkExcelApi
    {
        public static List<String> mColumnHeaders;
        public static List<String> mTestIds;
        private static Application ExcelApp;
        private static Workbooks mWorkbooks;
        private static Workbook mWorkbook;
        private static Worksheet mWorksheet;
        private static int iRowCount = -1;
        private static int iColCount = -1;
        private static String mDataDrivenFile = "";
        private static String mSelectedTestId = "";
        public static List<String> mSelectedRowData;

        //Added 3/22/2012 by Jerric
        private static Dictionary<String, List<String>> mWorkbookColumnHeaders;
        private static Dictionary<String, List<String>> mWorkbookSelectedRowData;
        private static Dictionary<String, int> mWorkbookColumnCount;
        private static Dictionary<String, int> mWorkbookRowCount;
        private static Dictionary<String, int> mWorkbookSelectedRow;
        private static int mCurrentRowIndex = -1;

        public static void Initialize()
        {
            ExcelApp = new Application();
            ExcelApp.Visible = false;
            ExcelApp.DisplayAlerts = false;
            mWorkbooks = ExcelApp.Workbooks;
            mSelectedRowData = new List<String>();

            //Aded 3/22/2012 by Jerric
            mWorkbookColumnHeaders = new Dictionary<String, List<String>>();
            mWorkbookSelectedRowData = new Dictionary<String, List<String>>();
            mWorkbookColumnCount = new Dictionary<String, int>();
            mWorkbookRowCount = new Dictionary<String, int>();
            mWorkbookSelectedRow = new Dictionary<String, int>();
        }
        public static void Dispose()
        {
            ExcelApp.Quit();
            //Marshal.FinalReleaseComObject(ExcelApp);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            ExcelApp = null;
            mDataDrivenFile = "";
            iRowCount = -1;
            iColCount = -1;
            mSelectedRowData = null;

            //Added 3/22/2012 by Jerric
            mWorkbookColumnHeaders = null;
            mWorkbookSelectedRowData = null;
            mWorkbookColumnCount = null;
            mWorkbookRowCount = null;
        }
        public static void SetDataDrivenFile(String DataDrivenFile)
        {
            OpenWorkBook(DataDrivenFile);

        }
        public static void SetRowData(String TestId)
        {
            mSelectedTestId = TestId;
            mSelectedRowData = GetRowData(TestId, true);

        }

        public static String GetCellData(String ColumnHeader)
        {
            String mCellVal = "";
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                if (mColumnHeaders[i] == ColumnHeader)
                {
                    mCellVal = mSelectedRowData[i];
                    break;
                }
            }
            return mCellVal;
        }

        //Added 3/22/2012 by Jerric
        public static void SetCurrentRow(String sSheetName, int iRow)
        {
            mWorkbookSelectedRow[sSheetName] = iRow;
            mWorkbookSelectedRowData[sSheetName] = GetRowData(sSheetName, iRow);
            mCurrentRowIndex = iRow;
        }

        public static int GetCurrentRowIndex()
        {
            return mCurrentRowIndex;
        }

        public static String GetCellData(String sSheetName, String sColumnHeader)
        {
            String mCellVal = "";
            for (int i = 0; i < mWorkbookColumnHeaders[sSheetName].Count; i++)
            {
                if (mWorkbookColumnHeaders[sSheetName][i] == sColumnHeader)
                {
                    mCellVal = mWorkbookSelectedRowData[sSheetName][i];
                    break;
                }
            }
            return mCellVal;
        }

        //Added 8/17/2012 by Jerric
        public static void SetCellData(String sSheetName, String sColumnHeader, String sValue)
        {
            for (int i = 0; i < mWorkbookColumnHeaders[sSheetName].Count; i++)
            {
                if (mWorkbookColumnHeaders[sSheetName][i] == sColumnHeader)
                {
                    Worksheet sheet = mWorkbook.Worksheets[sSheetName];
                    sheet.Cells[mWorkbookSelectedRow[sSheetName], i].Value = sValue;
                    break;
                }
            }
        }

        //Added 8/17/2012 by Jerric
        public static void SetCellData(String sSheetName, int iRow, int iCol, String sValue)
        {

            Worksheet sheet = mWorkbook.Worksheets[sSheetName];
            sheet.Cells[iRow, iCol].Value = sValue;

        }

        public static String GetColumnHeaders(String sSheetName)
        {
            String colHeaders = "";
            for (int i = 0; i < mWorkbookColumnHeaders[sSheetName].Count; i++)
            {
                if (i > 0)
                {
                    colHeaders = colHeaders + "~";
                }
                colHeaders = colHeaders + mWorkbookColumnHeaders[sSheetName][i];
            }

            return colHeaders;
        }

        public static int GetRowCount(String sSheetName)
        {
            return mWorkbookRowCount[sSheetName];
        }



        private static void SetTestIDs()
        {
            List<String> mIds = new List<String>();

            for (int i = 2; i <= iRowCount; i++)
            {
                // TestIDs are always in the 2nd column
                String mCellVal = GetCellValue("Tests", i, 2);
                mIds.Add(mCellVal);
            }
            mTestIds = mIds;
        }
        private static List<String> GetRowData(int iRow)
        {
            List<String> mData = new List<String>();
            for (int i = 1; i <= iColCount; i++)
            {
                String mCellVal = GetCellValue(iRow, i);
                mData.Add(mCellVal);
            }
            return mData;
        }
        private static List<String> GetRowData(String TestId, Boolean bLog)
        {
            List<String> mData = new List<String>();
            int i = 0;
            for (i = 0; i < mTestIds.Count; i++)
            {
                if (mTestIds[i] == TestId)
                {
                    //mData= GetRowData(i + 2);
                    mData = GetRowData("Tests", i + 2);
                    SetCurrentRow("Tests", i + 2);
                    break;
                }
            }
            if (bLog)
            {
                // DlkLogger.LogLineSeperator(1);
                DlkLogger.LogData("Row " + (i + 1).ToString(), mData);
                //DlkLogger.LogCurrentRowInfo(mColumnHeaders, mData, i + 1);
            }

            return mData;
        }
        private static void SetColumnNames()
        {
            //mColumnHeaders = GetRowData(1);
            mColumnHeaders = GetRowData("Tests", 1);
            foreach (Worksheet aSheet in mWorkbook.Sheets)
            {
                mWorkbookColumnHeaders.Add(aSheet.Name, GetRowData(aSheet.Name, 1));
            }
        }


        //Added 3/23/2012 by Jerric
        private static List<String> GetRowData(String sSheetName, int iRow)
        {
            List<String> mData = new List<String>();
            for (int i = 1; i <= mWorkbookColumnCount[sSheetName]; i++)
            {
                String mCellVal = GetCellValue(sSheetName, iRow, i);
                mData.Add(mCellVal);
            }
            return mData;
        }

        //Added 8/17/2012 by Jerric
        public static void ImportSheet(String FilePath, String OrigSheetName, String DestSheetName)
        {
            Workbook wbExternalWorkbook = mWorkbooks.Open(FilePath);
            Worksheet wsOrigSheet = wbExternalWorkbook.Worksheets[OrigSheetName];
            wsOrigSheet.Name = DestSheetName;
            wsOrigSheet.Copy(mWorkbook.Worksheets[1]);
            int iSheetRowCount = wsOrigSheet.Rows.Count;
            int iSheetColumnCount = wsOrigSheet.Columns.Count;

            mWorkbookColumnCount.Add(DestSheetName, TrimColumnCount(DestSheetName, iSheetColumnCount));
            mWorkbookRowCount.Add(DestSheetName, TrimRowCount(DestSheetName, iSheetRowCount));
            mWorkbookColumnHeaders.Add(DestSheetName, GetRowData(DestSheetName, 1));
            mWorkbookSelectedRow.Add(DestSheetName, 1);
        }

        public static Boolean SheetExists(String SheetName)
        {
            Boolean bFound = false;
            foreach (Worksheet sheet in mWorkbook.Worksheets)
            {
                if (sheet.Name == SheetName)
                {
                    bFound = true;
                    break;
                }
            }

            return bFound;
        }

        private static void OpenWorkBook(String FileToOpen)
        {
            Boolean bHadToOpen = false;
            int iSheetRowCount;
            int iSheetColumnCount;

            if (mDataDrivenFile == "")
            {
                mWorkbook = mWorkbooks.Open(FileToOpen);
                mDataDrivenFile = FileToOpen;
                bHadToOpen = true;
            }
            else
            {
                if (mDataDrivenFile == FileToOpen)
                {
                    // nothing to do; it's already open
                }
                else
                {
                    mWorkbook.Close();

                    mWorkbookColumnHeaders = new Dictionary<String, List<String>>();
                    mWorkbookSelectedRowData = new Dictionary<String, List<String>>();
                    mWorkbookColumnCount = new Dictionary<String, int>();
                    mWorkbookRowCount = new Dictionary<String, int>();
                    mWorkbookSelectedRow = new Dictionary<String, int>();

                    mWorkbook = mWorkbooks.Open(FileToOpen);
                    mDataDrivenFile = FileToOpen;
                    bHadToOpen = true;
                }
            }
            if (bHadToOpen)
            {
                //mWorksheet = mWorkbook.ActiveSheet;
                //iRowCount = mWorksheet.Rows.Count;
                //iColCount = mWorksheet.Columns.Count;
                //SetRowCount();
                //SetColumnCount();

                foreach (Worksheet aSheet in mWorkbook.Sheets)
                {
                    ((_Worksheet)aSheet).Activate();
                    iSheetRowCount = aSheet.Rows.Count;
                    iSheetColumnCount = aSheet.Columns.Count;
                    if (aSheet.Name == "Tests")
                    {
                        mWorksheet = aSheet;
                        iRowCount = aSheet.Rows.Count;
                        iColCount = aSheet.Columns.Count;
                        SetRowCount();
                        SetColumnCount();
                    }


                    mWorkbookColumnCount.Add(aSheet.Name, TrimColumnCount(aSheet.Name, iSheetColumnCount));
                    mWorkbookRowCount.Add(aSheet.Name, TrimRowCount(aSheet.Name, iSheetRowCount));
                    mWorkbookSelectedRow.Add(aSheet.Name, 1);
                }

                SetColumnNames();
                SetTestIDs();

            }
        }
        private static String GetCellValue(int iRow, int iCol)
        {
            Range mRangeCell = mWorksheet.Cells[iRow, iCol];
            String sCell = Convert.ToString(mRangeCell.Value);
            if (sCell == null)
            {
                sCell = "";
            }

            if (sCell.Contains("##########"))
            {
                sCell = Convert.ToString(mRangeCell.Value);
            }


            return sCell;
        }

        //Added 3/23/2012 by Jerric
        public static String GetCellValue(String sSheetName, int iRow, int iCol)
        {
            Worksheet currentSheet = mWorkbook.Sheets[sSheetName];
            Range mRangeCell = currentSheet.Cells[iRow, iCol];
            String sCell = Convert.ToString(mRangeCell.Value);
            if (sCell == null)
            {
                sCell = "";
            }
            return sCell;
        }


        private static int TrimColumnCount(String sSheetName, int iStartingCount)
        {
            int iFinalCount = 0;
            for (int i = 1; i <= iStartingCount; i++)
            {
                Worksheet aSheet = mWorkbook.Sheets[sSheetName];
                Range mRangeCell = aSheet.Cells[1, i];
                String mCell = Convert.ToString(mRangeCell.Value);
                if ((mCell == null) || (mCell == ""))
                {
                    //Added by Jerric
                    Range RangeNextCell = aSheet.Cells[1, i + 1];
                    String nextCell = Convert.ToString(RangeNextCell.Value);
                    if ((nextCell == null) || (nextCell == ""))
                    {
                        iFinalCount = i - 1;
                        break;
                    }
                }
            }
            return iFinalCount;
        }

        private static int TrimRowCount(String sSheetName, int iStartingCount)
        {
            int iFinalCount = 0;
            for (int i = 1; i <= iStartingCount; i++)
            {
                Worksheet aSheet = mWorkbook.Sheets[sSheetName];
                Range mRangeCell = aSheet.Cells[i, 1];
                String mCell = Convert.ToString(mRangeCell.Value);
                if ((mCell == null) || (mCell == ""))
                {
                    iFinalCount = i - 1;
                    break;
                }
            }
            return iFinalCount;
        }

        private static void SetColumnCount()
        {
            for (int i = 1; i <= iColCount; i++)
            {
                Range mRangeCell = mWorksheet.Cells[1, i];
                String mCell = Convert.ToString(mRangeCell.Value);
                if ((mCell == null) || (mCell == ""))
                {
                    iColCount = i - 1;
                    break;
                }
            }
        }
        private static void SetRowCount()
        {
            for (int i = 1; i <= iRowCount; i++)
            {
                Range mRangeCell = mWorksheet.Cells[i, 1];
                String mCell = Convert.ToString(mRangeCell.Value);
                if ((mCell == null) || (mCell == ""))
                {
                    iRowCount = i - 1;
                    break;
                }
            }
        }

        public static int CountValue(string ColumnHeader, string Value)
        {
            int columnIndex = -1;

            for (int i = 0; i < mWorkbookColumnHeaders["Script"].Count; i++)
            {
                if (mWorkbookColumnHeaders["Script"][i] == ColumnHeader)
                {
                    columnIndex = i + 1;
                    break;
                }
            }

            string columnLetter = ConvertExcelColumnFromNumberToLetter(columnIndex);
            string formula = "=SUMPRODUCT(--(" + columnLetter + ":" + columnLetter + "=" + "\"" + Value + "\"" + "))";

            int tempBufferInt;
            bool tempBuffeBool;
            bool isNum = int.TryParse(Value, out tempBufferInt);
            bool isBool = bool.TryParse(Value, out tempBuffeBool);

            if (isNum || isBool) // special handling for bool and int values
            {
                formula += " + SUMPRODUCT(--(" + columnLetter + ":" + columnLetter + "=" + Value + "))";
            }

            Worksheet refSheet = mWorkbook.Sheets["Script"];
            // pick out a random cell to attach a formula
            Range temp = refSheet.Cells[999, 999];
            temp.Formula = formula;

            return Convert.ToInt32(temp.Value);
        }

        private static string ConvertExcelColumnFromNumberToLetter(int column)
        {
            string columnString = "";
            decimal columnNumber = column;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }

        private static int ConvertExcelColumnFromLetterToNumber(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        /*
        public static string GetCustomProperty(string strProperty)
        {
            try
            {
                //Microsoft.Office.Core.DocumentProperties properties;
                //properties = (Microsoft.Office.Core.DocumentProperties)mWorkbook.CustomDocumentProperties;

                //foreach (Microsoft.Office.Core.DocumentProperty prop in properties)
                //{
                //    if (prop.Name == strProperty)
                //    {
                //        return Convert.ToString(prop.Value);
                //    }
                //}

                object returnVal = null;

                object oDocCustomProps = mWorkbook.CustomDocumentProperties;
                Type typeDocCustomProps = oDocCustomProps.GetType();


                object returned = typeDocCustomProps.InvokeMember("Item",
                                            System.Reflection.BindingFlags.Default |
                                           System.Reflection.BindingFlags.GetProperty, null,
                                           oDocCustomProps, new object[] { strProperty });

                Type typeDocAuthorProp = returned.GetType();
                returnVal = typeDocAuthorProp.InvokeMember("Value",
                                           System.Reflection.BindingFlags.Default |
                                           System.Reflection.BindingFlags.GetProperty,
                                           null, returned,
                                           new object[] { }).ToString();

                return returnVal.ToString();
            }
            catch
            {
                // empty
            }
            return string.Empty;
        }*/
    }
}
