using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace CommonLib.DlkUtility
{
    public class DlkExcelHelper
    {
        #region DECLARATIONS

        private static bool isColumnHeaderStyled = false; //true - style would be applied to 1st row/column header 
        private static bool hasHeader = true; //true -  1st row is expected as column name 

        const string CSS_DARK = "table-dark";
        const string CSS_SUCCESS = "table-success";
        const string CSS_DANGER = "table-danger";
        const string CSS_WHITE = "table-white";
        const string CSS_GRAY = "table-active";
        const string FIRST_ROW_CELL = "A1";
        const string SECOND_ROW_CELL = "A2";

        #endregion

        #region PROPERTIES

        public static bool IsColumnHeaderStyled
        {
            get { return isColumnHeaderStyled; }
            set { isColumnHeaderStyled = value; }
        }

        public static bool HasHeader
        {
            get { return hasHeader; }
            set { hasHeader = value; }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Exports data from any data table to excel file
        /// Supports XLSX only
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ExportToExcel(DataTable dt, string fileName)
        {
            FileInfo file = new FileInfo(fileName);

            // Ensures we create a new workbook
            if (file.Exists)
            {
                file.Delete();
            }

            using (ExcelPackage pck = new ExcelPackage(file))
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                // Load data from DataTable to the worksheet
                ws.Cells["A1"].LoadFromDataTable((dt), true);
                ws.Cells.AutoFitColumns();

                if (isColumnHeaderStyled)
                {
                    // Add some styling
                    using (ExcelRange rng = ws.Cells[1, 1, 1, dt.Columns.Count])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }
                }

                // Save the new workbook
                pck.SaveAs(file);
            }
            return fileName;
        }

        /// <summary>
        /// Imports data from Excel into Data Table
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ImportFromExcel(string filePath)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = File.OpenRead(filePath))
                {
                    pck.Load(stream);
                }
                ExcelWorksheet ws = pck.Workbook.Worksheets.First();

                DataTable dt = new DataTable(ws.Name);
                int totalCols = ws.Dimension.End.Column;
                int totalRows = ws.Dimension.End.Row;
                int startRow = hasHeader ? 2 : 1;

                //Get Column Headers
                foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
                {
                    var columnName = hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column);
                    if (!dt.Columns.Contains(columnName))
                    {
                        dt.Columns.Add(columnName);
                    }
                    else
                    {
                        //check if there are duplicate headers
                        throw new DuplicateNameException();
                    }
                }

                //Get Column Rows
                for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
                {
                    Dictionary<int, string> cellList = new Dictionary<int, string>();
                    string value = String.Empty;
                    int valColNum;

                    for (int colNum = 1; colNum <= totalCols; colNum++)
                    {
                         value  = (ws.Cells[rowNum, colNum].Value ?? "").ToString();
                         valColNum = (ws.Cells[rowNum, colNum].Start.Column);
                         cellList.Add(valColNum,value);
                    }

                    if (cellList.Count() != dt.Columns.Count)
                    {
                        throw new ArgumentException();
                    }

                    var dr = dt.NewRow();
                    foreach (var cell in cellList)
                    {
                        dr[cell.Key - 1] = cell.Value ?? String.Empty;
                    }

                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        /// <summary>
        /// Check if Excel is empty
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CheckIfExcelIsEmpty(string fileName)
        {
            int rowCnt = 0;
            int colCnt = 0;

            FileInfo newFile = new FileInfo(fileName);
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.First();
                if (worksheet.Dimension != null) // check if worksheet is null first to prevent exception
                {
                    rowCnt = worksheet.Dimension.End.Row;
                    colCnt = worksheet.Dimension.End.Column;
                }
            }

            return rowCnt == 0 && colCnt == 0;
        }

        private static void CheckIfExcelIsInUse(string fileName)
        {
            bool fileInUse;
            using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                fileInUse = fs.CanRead;
                fs.Close();
            }

            if (fileInUse)
                throw new IOException();

        }

        /// <summary>
        /// Export and save data to an excel file. Supports *.xlsx only.
        /// </summary>
        /// <param name="ds">Data set to be exported</param>
        /// <param name="fileName">Filename of the excel file</param>
        /// <param name="sheetNames">List of worksheet names of the excel file</param>
        /// <param name="cellColors">(Currently for Test Library only) List of colors to be used in row headers</param>
        public static void ExportAndSaveToExcel(DataSet ds, string fileName, List<string> sheetNames, List<string> cellColors = null)
        {
            FileInfo file = new FileInfo(fileName);
            int idx = 0;

            // Ensure we create a new workbook
            if (file.Exists)
            {
                file.Delete();
            }

            using (ExcelPackage pck = new ExcelPackage(file))
            {
                // Add a new worksheet to the empty workbook
                // Load data from each DataTable to the worksheet
                foreach (DataTable dt in ds.Tables)
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetNames[idx]);
                    if (cellColors != null)
                    {
                        dt.Columns.RemoveAt(1);
                    }
                    ws.Cells["A1"].LoadFromDataTable((dt), true);
                    ws.Cells.AutoFitColumns();
                    idx = idx + 1;

                    if (isColumnHeaderStyled)
                    {
                        // Add some styling
                        using (ExcelRange rng = ws.Cells[1, 1, 1, dt.Columns.Count])
                        {
                            rng.Style.Font.Bold = true;
                            rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
                            rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        }
                    }
                    if (cellColors != null)
                    {
                        ws.Cells[1, 2].Value = "TOTAL";
                        for (int index = 0; index < cellColors.Count; index++)
                        {
                            using (ExcelRange rng = ws.Cells[index + 2, 1])
                            {
                                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                switch (cellColors[index]) 
                                {
                                    case "Gray":
                                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(245, 245, 245));
                                        break;
                                    case "Orange":
                                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 215, 0));
                                        break;
                                    case "Yellow":
                                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 250, 205));
                                        break;
                                }
                            }
                        }
                    }
                }
                                    
                // Save the new workbook
                pck.SaveAs(file);
            }
        }

        /// <summary>
        /// Exports and saves an HTML table  an Excel file. Supports *.xlsx only.
        /// </summary>
        /// <param name="htmlTable">HTML table to be exported. Should be in XElement format</param>
        /// <param name="fileName">Filename of the excel file</param>
        /// <param name="sheetName">Name of the worksheet</param>
        public static void ExportHTMLTableToExcel(XElement htmlTable, string fileName, string sheetName, bool hasSpecialHeader=false, bool hasCellStyling=false)
        {
            try
            {
                FileInfo file = new FileInfo(fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                //Convert HTML to DataTable for worksheet loading
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlTable.ToString());
                DataTable dt = ConvertHTMLToDataTable(htmlDoc);
                if(dt == null)
                {
                    throw new Exception("No HTML table found.");
                }

                using (ExcelPackage pck = new ExcelPackage(file))
                {
                    
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);
                    int index = 0;

                    // Load data from HTML Table to the worksheet
                    if (hasSpecialHeader)
                    {
                        ws.Cells[FIRST_ROW_CELL].Value = sheetName;
                        ws.Cells[SECOND_ROW_CELL].LoadFromDataTable((dt), true);
                        ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;
                        index = 2;
                    }
                    else
                    {
                        ws.Cells[FIRST_ROW_CELL].LoadFromDataTable((dt), true);
                        index = 1;
                    }

                    
                    ws.Cells.AutoFitColumns();
                    if (hasCellStyling)
                    {
                        if (hasSpecialHeader)
                        {
                            ws.Cells[FIRST_ROW_CELL].Style.Font.Bold = true;
                            ws.Cells[FIRST_ROW_CELL].Style.Font.Size = 14;
                        }
                        //Header Styling
                        ApplyRowStyling(ws, index,dt.Columns.Count,CSS_DARK,true);

                        //Row Styling
                        foreach(var row in htmlDoc.DocumentNode.SelectNodes("//tr[td]"))
                        {
                            // Look for class attribute. If not found, use table-white by default
                            index++;
                            string cssStyle = row.GetAttributeValue("class", CSS_WHITE);
                            ApplyRowStyling(ws, index, dt.Columns.Count, cssStyle, false, true);                            
                        }
                    }

                    pck.SaveAs(file);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Converts an HTML document into a DataTable
        /// </summary>
        /// <param name="htmlDoc">The HTML document to be converted</param>
        /// <returns></returns>
        private static DataTable ConvertHTMLToDataTable(HtmlDocument htmlDoc)
        {
            DataTable ret = new DataTable();
            var headers = htmlDoc.DocumentNode.SelectNodes("//tr/th");
            foreach (var header in htmlDoc.DocumentNode.SelectNodes("//tr/th"))
            {
                ret.Columns.Add(header.InnerText);
            }
            foreach (var row in htmlDoc.DocumentNode.SelectNodes("//tr[td]"))
            {
                ret.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
            }
            return ret;
        }

        /// <summary>
        /// Applies styling for each row
        /// </summary>
        /// <param name="workSheet"> The Excel worksheet where styling will be applied</param>
        /// <param name="rowIndex"> The row index where styling will be applied</param>
        /// <param name="columnCount">The number of columns in a given table</param>
        /// <param name="cssStyle">The css style from the HTML table row</param>
        /// <param name="isHeader">Boolean value that determines if the row is header or not</param>
        private static void ApplyRowStyling(ExcelWorksheet workSheet,int rowIndex, int columnCount, string cssStyle, bool isHeader = false, bool hasBorder= false)
        {
            using (ExcelRange rng = workSheet.Cells[rowIndex, 1, rowIndex, columnCount])
            {
                if (isHeader)
                {
                    rng.Style.Font.Bold = true;
                }

                switch (cssStyle)
                {
                    case CSS_DARK:
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Black);
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        break;
                    case CSS_DANGER:
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Salmon);
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        break;
                    case CSS_SUCCESS:
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        break;
                    case CSS_GRAY:
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        break;
                    case CSS_WHITE:
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        break;
                }
                                
                if (hasBorder)
                {
                    for(int cellIndex=1; cellIndex <= columnCount; cellIndex++)
                    {
                        workSheet.Cells[rowIndex, cellIndex].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }
            }
        }
        #endregion
    }
}
