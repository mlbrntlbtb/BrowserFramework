using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using OfficeOpenXml.Style.Dxf;
using OfficeOpenXml.Style;
using System.Globalization;

namespace DocDiff
{
    public struct ConfigFileRecord
    {
        public Boolean IgnoreDateErrors;
        public Boolean Value;
        public Boolean Formula;
        public Boolean Font_Bold;
        public Boolean Font_Bck;
        public Boolean Font_Color;
        public Boolean Font_Style;
        public Boolean Font_Italic;
        public Boolean Font_Name;
        public Boolean Font_Size;
        public Boolean Font_Underline;
        public Boolean Font_Strike;
        public Boolean Borders_Value;
        public Boolean Borders_Weight;
        public Boolean Borders_Color;
        public Boolean Borders_Count;
        public Boolean ColumnWidth;
        public Boolean Height;
        public Boolean Left;
        public Boolean NumberFormat;
        public Boolean Orientation;
        public Boolean RowHeight;
        public Boolean Style;
        public Boolean VerticalAlignment;
        public Boolean Width;
        public Boolean WrapText;
        public Boolean ConditionalFormatting;
        public Boolean CondFrmt_Formula1;
        public Boolean CondFrmt_NumberFormat;
        public Boolean CondFrmt_InteriorColor;
        public Boolean CondFrmt_InteriorColorIndex;
        public Boolean CondFrmt_InteriorPattern;
        public Boolean CondFrmt_InteriorPatternColor;
        public Boolean CondFrmt_InteriorPatternColorIndex;
        public Boolean CondFrmt_InteriorPatternThemeColor;
        public Boolean CondFrmt_InteriorPatternTintAndShade;
        public Boolean CondFrmt_InteriorThemeColor;
        public Boolean CondFrmt_InteriorTintAndShade;
        public Boolean SkipOtherSheetsOnFirstSheetPass;
        public Boolean IgnoreWorksheetName;
        public string IgnoreSheets;
        public int ErrorThreshold;
        public int RoundingPrecisionForFormulaValues;
        public Boolean TreatCellsWithOnlySpacesAsEmpty;
        public Dictionary<string, List<Tuple<int, int>>> IgnoreCells; //sheet name and list of tuple (item1 = col, item2 = row) to ignore
        public List<int> SheetIndexesToIgnore;
        public List<int> SheetsToIgnore;
        public List<string> InvalidSheets;
        public List<DocDiffCompareDiffRecord> configResults;
        private const int MAXIMUM_CHARACTERS_FOR_SHEETNAME = 31;

        /// <summary>
        /// Initialized comparison config
        /// </summary>
        /// <param name="sConfigFile">xml configuration full path</param>
        public void Initialize(String sConfigFile)
        {
            XDocument XDocConfig = new XDocument();
            configResults = new List<DocDiffCompareDiffRecord>();
            XDocConfig = XDocument.Load(sConfigFile);
            System.Console.WriteLine("Using config file: " + sConfigFile);

            SkipOtherSheetsOnFirstSheetPass = GetConfigValue(XDocConfig, "SkipOtherSheetsOnFirstSheetPass");

            IgnoreDateErrors = GetConfigValue(XDocConfig, "IgnoreDateErrors");
            Value = GetConfigValue(XDocConfig, "Value");
            Formula = GetConfigValue(XDocConfig, "Formula");
            Font_Bold = GetConfigValue(XDocConfig, "Font_Bold");
            Font_Bck = GetConfigValue(XDocConfig, "Font_Bck");
            Font_Color = GetConfigValue(XDocConfig, "Font_Color");
            Font_Style = GetConfigValue(XDocConfig, "Font_Style");
            Font_Italic = GetConfigValue(XDocConfig, "Font_Italic");
            Font_Name = GetConfigValue(XDocConfig, "Font_Name");
            Font_Size = GetConfigValue(XDocConfig, "Font_Size");
            Font_Underline = GetConfigValue(XDocConfig, "Font_Underline");
            Font_Strike = GetConfigValue(XDocConfig, "Font_Strike");
            Borders_Value = GetConfigValue(XDocConfig, "Borders_Value");
            Borders_Weight = GetConfigValue(XDocConfig, "Borders_Weight");
            Borders_Color = GetConfigValue(XDocConfig, "Borders_Color");
            Borders_Count = GetConfigValue(XDocConfig, "Borders_Count");
            ColumnWidth = GetConfigValue(XDocConfig, "ColumnWidth");
            Height = GetConfigValue(XDocConfig, "Height");
            Left = GetConfigValue(XDocConfig, "Left");
            NumberFormat = GetConfigValue(XDocConfig, "NumberFormat");
            Orientation = GetConfigValue(XDocConfig, "Orientation");
            RowHeight = GetConfigValue(XDocConfig, "RowHeight");
            Style = GetConfigValue(XDocConfig, "Style");
            VerticalAlignment = GetConfigValue(XDocConfig, "VerticalAlignment");
            Width = GetConfigValue(XDocConfig, "Width");
            WrapText = GetConfigValue(XDocConfig, "WrapText");
            ErrorThreshold = GetConfigValueInt(XDocConfig, "ErrorThreshold");
            RoundingPrecisionForFormulaValues = GetConfigValueInt(XDocConfig, "RoundingPrecisionForFormulaValues");
            TreatCellsWithOnlySpacesAsEmpty = GetConfigValue(XDocConfig, "TreatCellsWithOnlySpacesAsEmpty");

            CondFrmt_Formula1 = GetConfigValue(XDocConfig, "CondFrmt_Formula");
            CondFrmt_NumberFormat = GetConfigValue(XDocConfig, "CondFrmt_NumberFormat");
            CondFrmt_InteriorColor = GetConfigValue(XDocConfig, "CondFrmt_InteriorColor");
            CondFrmt_InteriorColorIndex = GetConfigValue(XDocConfig, "CondFrmt_InteriorColorIndex");
            CondFrmt_InteriorPattern = GetConfigValue(XDocConfig, "CondFrmt_InteriorPattern");
            CondFrmt_InteriorPatternColor = GetConfigValue(XDocConfig, "CondFrmt_InteriorPatternColor");
            CondFrmt_InteriorPatternColorIndex = GetConfigValue(XDocConfig, "CondFrmt_InteriorPatternColorIndex");
            CondFrmt_InteriorPatternThemeColor = GetConfigValue(XDocConfig, "CondFrmt_InteriorPatternThemeColor");
            CondFrmt_InteriorPatternTintAndShade = GetConfigValue(XDocConfig, "CondFrmt_InteriorPatternTintAndShade");
            CondFrmt_InteriorThemeColor = GetConfigValue(XDocConfig, "CondFrmt_InteriorThemeColor");
            CondFrmt_InteriorTintAndShade = GetConfigValue(XDocConfig, "CondFrmt_InteriorTintAndShade");

            if(!CheckConfigValue(XDocConfig, "IgnoreWorksheetName"))
            {
                AddConfigValue(XDocConfig, sConfigFile, "IgnoreWorksheetName", "true");
            }

            if (!CheckConfigValue(XDocConfig, "IgnoreSheets"))
            {
                AddConfigValue(XDocConfig, sConfigFile, "IgnoreSheets", "0");
            }

            IgnoreWorksheetName = GetConfigValue(XDocConfig, "IgnoreWorksheetName");
            IgnoreSheets = GetConfigValueString(XDocConfig, "IgnoreSheets");
            SheetsToIgnore = StringToIntArray(IgnoreSheets, ",", "IgnoreSheets");
            
            InvalidSheets = new List<string>();
            IgnoreCells = GetIgnoreCellsConfigValue(XDocConfig);
            SheetIndexesToIgnore = GetSheetIndexesToIgnore(IgnoreCells);

            if (
                (CondFrmt_Formula1) || (CondFrmt_NumberFormat) || (CondFrmt_InteriorColor) || (CondFrmt_InteriorColorIndex) ||
                (CondFrmt_InteriorPattern) || (CondFrmt_InteriorPatternColor) || (CondFrmt_InteriorPatternColorIndex) ||
                (CondFrmt_InteriorPatternThemeColor) || (CondFrmt_InteriorPatternTintAndShade) || (CondFrmt_InteriorThemeColor) ||
                (CondFrmt_InteriorTintAndShade)
                )
            {
                ConditionalFormatting = true;
            }
            else
            {
                ConditionalFormatting = false;
            }
        }

        /// <summary>
        /// Parse string with numeric value to array of integer
        /// </summary>
        /// <param name="Value">string value</param>
        /// <param name="Separator">string separator</param>
        /// <returns>returns parsed string to array of integers</returns>
        private List<int> StringToIntArray(string Value, string Separator, string Config)
        {
            List<int> parseString;
            try
            {
                parseString = new List<int>();
                if (Value.Contains(Separator))
                {
                    parseString = Value.Split(char.Parse(Separator)).Select(Int32.Parse).ToList();
                }
                else
                {
                    parseString.Add(Convert.ToInt32(Value));
                }
            }
            catch
            {
                //NOTE: In case non-numeric value has been parse, default value should be 0
                Console.WriteLine("  StringToIntArray() failed: Encountered error in parsing config value of " + Config);
                AddDocDiffCompareRecod(Config, $"Numeric string with '{Separator}' separator", Value);
                parseString = new List<int>{0};
            }

            return parseString;
        }

        /// <summary>
        /// Checks configuration file if config exist or not
        /// </summary>
        /// <param name="XDocConfig">Config content</param>
        /// <param name="Name">Name of config to check</param>
        /// <returns></returns>
        private Boolean CheckConfigValue(XDocument XDocConfig, String Name)
        {
            Boolean bResult = false;
            var data = from doc in XDocConfig.Descendants("excel").Elements("type")
                       select new
                       {
                           evalName = doc.Attribute("name").Value,
                           evalAttrib = doc.Value
                       };
            string configName = (string)data.Where(x => x.evalName.Equals(Name)).Select(x => x.evalName).SingleOrDefault();
            if(!string.IsNullOrEmpty(configName))
            {
                bResult = true;
            }
            return bResult;
        }

        /// <summary>
        /// Adds new config element is it does not exist
        /// </summary>
        /// <param name="XDocConfig">Config content</param>
        /// <param name="FilePath">Full path of config file</param>
        /// <param name="Name">Config name to add</param>
        private void AddConfigValue(XDocument XDocConfig, String FilePath, String Name, String Value)
        {
            XElement excel = XDocConfig.Descendants("excel").FirstOrDefault();
            XElement type = new XElement("type");

            type.SetAttributeValue("name", Name);
            type.Value = Value;
            excel.Add(type);
            XDocConfig.Save(FilePath);
        }

        private Boolean GetConfigValue(XDocument XDocConfig, String Name)
        {
            Boolean bResult = false;
            var data = from doc in XDocConfig.Descendants("excel").Elements("type")
                       select new
                       {
                           evalName = doc.Attribute("name").Value,
                           evalAttrib = doc.Value
                       };
            foreach (var val in data)
            {
                if (val.evalName.ToLower() == Name.ToLower())
                {
                    if (Boolean.TryParse(val.evalAttrib, out _))
                    {
                        bResult = Convert.ToBoolean(val.evalAttrib);
                    }
                    else
                    {
                        AddDocDiffCompareRecod(val.evalName, "True/False", val.evalAttrib);
                    }
                    break;
                }
            }
            return bResult;
        }

        private int GetConfigValueInt(XDocument XDocConfig, String Name)
        {
            int iResult = -1;
            var data = from doc in XDocConfig.Descendants("excel").Elements("type")
                       select new
                       {
                           evalName = doc.Attribute("name").Value,
                           evalAttrib = doc.Value
                       };
            foreach (var val in data)
            {
                if (val.evalName.ToLower() == Name.ToLower())
                {
                    if (int.TryParse(val.evalAttrib, out _))
                    {
                        iResult = Convert.ToInt32(val.evalAttrib);
                    }
                    else
                    {
                        AddDocDiffCompareRecod(val.evalName, "Integer", val.evalAttrib);
                    }
                    break;
                }           
            }
            return iResult;
        }

        private string GetConfigValueString(XDocument XDocConfig, String Name)
        {
            string sResult = "";
            var data = from doc in XDocConfig.Descendants("excel").Elements("type")
                       select new
                       {
                           evalName = doc.Attribute("name").Value,
                           evalAttrib = doc.Value
                       };
            foreach (var val in data)
            {
                if (val.evalName.ToLower() == Name.ToLower())
                {
                    sResult = val.evalAttrib;
                    break;
                }
            }
            return sResult;
        }

        /// <summary>
        /// Returns the dictionary of cells to ignore. Sheet name is the key which retrieves the list of tuple
        /// tuple item 1 is col. item 2 is row
        /// </summary>
        /// <param name="XDocConfig"></param>
        /// <returns></returns>
        private Dictionary<string, List<Tuple<int, int>>> GetIgnoreCellsConfigValue(XDocument XDocConfig)
        {
            var dictionaryResult = new Dictionary<string, List<Tuple<int, int>>>();
            //get ignore cells data
            var data = from doc in XDocConfig.Descendants("excel").Elements("type").Where(x => x.Attribute("name").Value.ToLower() == "ignorecells")
                       select new
                       {
                           evalName = doc.Attribute("name").Value,
                           evalSheetName = doc.Attribute("sheetname") != null ? doc.Attribute("sheetname").Value : string.Empty,
                           evalValue = doc.Value
                       };
            //segregate into dictionary - sheetname as key and list of col/row
            var regex = new Regex(@"(?<col>([A-Z]|[a-z])+)(?<row>(\d)+)");
            foreach (var val in data)
            {
                string[] inputs = val.evalValue.Split(';');
                string sheet = "";
                string inputCell = "";

                foreach (string input in inputs)
                {
                    string value = input.Trim();

                    if (value.Contains('!') && (!value.Contains('[') && !value.Contains(']'))) // with sheetname
                    {
                        sheet = value.Substring(0, value.LastIndexOf('!'));
                        inputCell = value.Substring(value.LastIndexOf('!') + 1);

                        if (IsInvalidSheetName(sheet))
                        {
                            InvalidSheets.Add(sheet);
                            sheet = "";
                            inputCell = null;
                        }
                    }
                    else if (value.Contains('!') && (value.Contains('[') && value.Contains(']'))) // with sheetindex
                    {
                        sheet = value.Substring(value.IndexOf("[") + 1);
                        sheet = sheet.Substring(0, value.IndexOf("]") - 1);
                        sheet = "[" + sheet + "]";
                        inputCell = value.Substring(value.IndexOf('!') + 1);
                    }
                    else // cell only
                    {
                        inputCell = value;
                    }

                    if (inputCell != null)
                    {
                        if (inputCell.Contains(':')) // range
                        {
                            if (!string.IsNullOrEmpty(sheet))
                            {
                                inputCell = inputCell.Replace(sheet, "");
                            }

                            List<string> cellsToAdd = new List<string>();
                            string[] bounds = inputCell.Split(':');
                            var matchStart = regex.Match(bounds[0].Trim());
                            var matchEnd = regex.Match(bounds[1].Trim());

                            if (matchStart != null && matchEnd != null)
                            {
                                int rowNumberStart = int.Parse(matchStart.Groups["row"].Value);
                                string rowNumberEnd = matchEnd.Groups["row"].Value;
                                string columnLetterStart = matchStart.Groups["col"].Value;
                                string columnLetterEnd = matchEnd.Groups["col"].Value;

                                int columnLetterStartToNumber = GetColumnNumber(columnLetterStart);
                                int columnLetterEndToNumber = GetColumnNumber(columnLetterEnd);
                                int rowCount = (int.Parse(rowNumberEnd) - rowNumberStart + 1);
                                int columnCount = ((columnLetterEndToNumber - columnLetterStartToNumber) + 1);
                                int rowCounter = rowNumberStart;

                                for (int i = 1; i <= rowCount; i++) // rows
                                {
                                    for (int j = columnLetterStartToNumber; j <= (columnLetterStartToNumber + columnCount) - 1; j++) // columns
                                    {
                                        string column = GetExcelColumnName(j);
                                        string row = rowCounter.ToString();

                                        string cell = column + row;
                                        cellsToAdd.Add(cell);
                                    }
                                    rowCounter++;
                                }

                                foreach (string cellToAdd in cellsToAdd)
                                {
                                    var match = regex.Match(cellToAdd);

                                    if (match != null)
                                    {
                                        var col = GetColumnNumber(match.Groups["col"].Value);
                                        var row = int.Parse(match.Groups["row"].Value);
                                        var cell = new Tuple<int, int>(col, row);

                                        if (!dictionaryResult.ContainsKey(sheet))
                                            dictionaryResult.Add(sheet, new List<Tuple<int, int>> { cell });
                                        else
                                            dictionaryResult[sheet].Add(cell);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var match = regex.Match(inputCell);

                            if (match != null)
                            {
                                var col = GetColumnNumber(match.Groups["col"].Value);
                                var row = int.Parse(match.Groups["row"].Value);
                                var cell = new Tuple<int, int>(col, row);

                                if (!dictionaryResult.ContainsKey(sheet))
                                    dictionaryResult.Add(sheet, new List<Tuple<int, int>> { cell });
                                else
                                    dictionaryResult[sheet].Add(cell);
                            }
                        }
                    }
                }
            }
            return dictionaryResult;
        }

        /// <summary>
        /// Get column number based on the letter structure of excel
        /// </summary>
        /// <param name="column">the column letter to convert to number</param>
        /// <returns></returns>
        private int GetColumnNumber(string column)
        {
            int colNum = 0;
            int pow = 1;
            for (int i = column.Length - 1; i >= 0; i--)
            {
                colNum += (column[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return colNum;
        }

        /// <summary>
        /// Get column letter based on the number
        /// </summary>
        /// <param name="columnNumber">Column number to convert to letter</param>
        /// <returns></returns>
        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }

            return columnName;
        }

        /// <summary>
        /// Returns a list of sheet indexes from the dictionary that contains cells to be ignored
        /// </summary>
        /// <param name="ignoreCells">Dictionary containing the cells to be ignored</param>
        /// <returns>List of sheet indexes</returns>
        private List<int> GetSheetIndexesToIgnore(Dictionary<string, List<Tuple<int, int>>> ignoreCells)
        {
            List<int> indexes = new List<int>();

            foreach (var entry in ignoreCells)
            {
                string keyToAdd = "";

                if (entry.Key.Contains('[') && entry.Key.Contains(']'))
                {
                    keyToAdd = entry.Key.Substring(entry.Key.IndexOf("[") + 1);
                    keyToAdd = keyToAdd.Substring(0, entry.Key.IndexOf("]") - 1);

                    if (int.TryParse(keyToAdd, out int keyNumber))
                    {
                        indexes.Add(keyNumber);
                    }
                }
            }

            return indexes;
        }

        /// <summary>
        /// Checks if string is a valid excel sheet name
        /// </summary>
        /// <param name="sheetname">String to be checked</param>
        /// <returns>True if string is an invalid sheetname; otherwise false</returns>
        private bool IsInvalidSheetName(string sheetname)
        {
            string invalidChars = ":\\/?*[]";
            if (sheetname.Length > MAXIMUM_CHARACTERS_FOR_SHEETNAME)
            {
                return true;
            }

            if (sheetname.IndexOfAny(invalidChars.ToCharArray()) != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a new record in the DocDiff comparison results to display in the html output.
        /// </summary>
        /// <param name="configName">Configuration name</param>
        /// <param name="expectedValue">Expected config value</param>
        /// <param name="actualValue">Actual config value</param>
        private void AddDocDiffCompareRecod(string configName,  string expectedValue, string actualValue)
        {
            DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord(0, "Configuration",
                       "Invalid configuration value. Configuration: " + configName,
                       "Expected: " + expectedValue,
                       "Actual: " + actualValue);
            configResults.Add(record);
        }
    }

    /// <summary>
    /// Class for excel comparison
    /// </summary>
    public class XlsCompare
    {        
        const string TEST_TYPE_SHEET_COUNT = "SheetCount";
        const string TEST_TYPE_SHEETNAME_COMPARISON = "SheetNameComparison";
        const string TEST_TYPE_COLUMN_COUNT_COMPARISON = "ColumnCountComparison";
        const string TEST_TYPE_ROW_COUNT_COMPARISON = "RowCountComparison";        

        const string WORKSHEET_NAMES_MATCH = "Worksheet names match.";
        const string WORKSHEET_NAMES_DIFFERENT = "Worksheet names are different.";
        const string WORKSHEET_OUT_OF_RANGE = "Sheet {0} for actual worksheet is out of range";

        const string ROW_COUNT_MATCH = "Row count matches. Sheet: {0}, Row Count: {1}";
        const string ROW_COUNT_NOT_MATCH = "Row count does not match. Sheet: {0}, Row Count Expected: {1}, Row Count Actual: {2}";

        const string COLUMN_COUNT_MATCH = "Column count matches. Sheet: {0}, Column Count: {1}";
        const string COLUMN_COUNT_NOT_MATCH = "Column count does not match. Sheet: {0}, Column Count Expected: {1}, Column Count Actual: {2}";

        const string NULL = "'null'";

        private bool mErrorsOnly = true; // we used to allow this to be toggled... but too many results
        private Stopwatch mWatch;
        private ConfigFileRecord mConfigFileRecord;
        private bool bErrorThresholdReached = false;

        /// <summary>
        /// Actual excel file EPPLUS excel package
        /// </summary>
        private ExcelEPPData mEPPActualPackage;

        /// <summary>
        /// Expected excel file EPPLUS excel package
        /// </summary>
        private ExcelEPPData mEPPExpectedPackage;

        /// <summary>
        /// Constructor for Excel files comparison
        /// </summary>
        /// <param name="ExpectedFile">expected excel file full path</param>
        /// <param name="ActualFile">actual excel file full path</param>
        /// <param name="ConfigFile">specified configuration xml file</param>
        public XlsCompare(string ExpectedFile, string ActualFile, string ConfigFile)
        {
            mEPPActualPackage = new ExcelEPPData(ActualFile);
            mEPPExpectedPackage = new ExcelEPPData(ExpectedFile);

            mWatch = new Stopwatch();
            mConfigFileRecord = new ConfigFileRecord();
            mConfigFileRecord.Initialize(ConfigFile);
        }

        /// <summary>
        /// Main xls comparison method
        /// </summary>
        /// <returns></returns>
        public List<DocDiffCompareDiffRecord> CompareXls()
        {
            List<DocDiffCompareDiffRecord> results = new List<DocDiffCompareDiffRecord>();
            results = mConfigFileRecord.configResults;

            void resetTimer()
            {
                mWatch.Reset();
                mWatch.Start();
            }

            try
            {
                if (!mConfigFileRecord.IgnoreWorksheetName)
                {
                    CompareEPPSheetNames(ref results);
                }

                // check for incorrect sheet names
                if (mConfigFileRecord.InvalidSheets.Any())
                {
                    foreach (string invalidSheet in mConfigFileRecord.InvalidSheets)
                    {
                        DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(0, "InvalidIgnoreCellParameter", "Invalid sheet name: " + invalidSheet, "", "");
                        results.Add(cdr);
                    }
                }

                for (int i = 1; i <= mEPPExpectedPackage.SheetsCount; i++)
                {
                    //check if actual worksheet contains sheet[index].
                    if (mEPPActualPackage.SheetsCount < i)
                    {
                        results.Add(new DocDiffCompareDiffRecord((int)MessageType.Error,
                                TEST_TYPE_SHEET_COUNT, 
                                string.Format(WORKSHEET_OUT_OF_RANGE, i),
                                mEPPExpectedPackage.SheetsCount.ToString(), 
                                mEPPActualPackage.SheetsCount.ToString()));

                        continue;
                    }

                    if(!mConfigFileRecord.SkipOtherSheetsOnFirstSheetPass
                        && !mConfigFileRecord.SheetsToIgnore.Contains(0))
                    {
                        if(mConfigFileRecord.SheetsToIgnore.Contains(i))
                        {
                            Console.WriteLine("  Skipping worksheet #" + i.ToString());
                            continue;
                        }
                    }

                    mEPPExpectedPackage.SetWorkSheet(i);
                    mEPPActualPackage.SetWorkSheet(i);

                    Console.WriteLine("  Comparing worksheet: " + mEPPExpectedPackage.SelectedSheet.Name);

                    //compare row counts
                    Console.WriteLine("    Starting Row Count Comparison...");

                    resetTimer();
                    CompareEPPRowCount(ref results);
                    mWatch.Stop();
                    Console.WriteLine("    Row Count Comparison Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());

                    //compare column counts
                    Console.WriteLine("    Starting Column Count Comparison...");

                    resetTimer();
                    CompareEPPColumnCount(ref results);
                    mWatch.Stop();
                    Console.WriteLine("    Column Count Comparison Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());

                    // compare cell data
                    Console.WriteLine("    Starting Cell Data Comparison...");

                    resetTimer();
                    CompareEPPCellData(ref results);
                    mWatch.Stop();

                    Console.WriteLine("    Cell Data Comparison Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());
                    if (bErrorThresholdReached)
                    {
                        return results;
                    }

                    if ((i == 1) && (mConfigFileRecord.SkipOtherSheetsOnFirstSheetPass))
                    {
                        bool bErrorFound = false;
                        for (int arcount = 0; arcount < results.Count; arcount++)
                        {
                            if (results[arcount].messagetype == 1)
                            {
                                bErrorFound = true;
                                break;
                            }
                        }
                        if (!bErrorFound)
                        {
                            Console.WriteLine("    >> [SkipOtherSheetsOnFirstSheetPass] is set to true in config file.");
                            Console.WriteLine("    >> No errors were found. Done comparing!");
                            break;
                        }
                    }
                }
            }
            catch
            {
                //do nothing
            }
            return results;
        }

        /// <summary>
        /// Iterates excel files cell by row and column 
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareEPPCellData(ref List<DocDiffCompareDiffRecord> results)
        {
            int isStatus = 0;
            int iCurrent = 0;
            int iErrorCount = 0;
            double iPrct = 0;
            int iTotal = mEPPExpectedPackage.TotalCellsCount;
            Stopwatch mCellWatch = new Stopwatch();
            mCellWatch.Start();

            for (int rowIndex = 1; rowIndex <= GetMaxRowCount(); rowIndex++)
            {
                for (int columnIndex = 1; columnIndex <= GetMaxColumnCount(); columnIndex++)
                {
                    isStatus++;
                    if (isStatus == 1000)
                    {
                        mCellWatch.Stop();
                        Console.WriteLine("      1000 Cells Examined in: " + new TimeSpan(0, 0, 0, 0, (int)mCellWatch.ElapsedMilliseconds).ToString());
                        iCurrent = 1000 + iCurrent;
                        iPrct = (double)iCurrent / (double)iTotal;
                        iPrct = Math.Round((iPrct * 100), 1);
                        Console.WriteLine("      Status [" + iPrct.ToString() + "%]: Cells Examined: " + iCurrent.ToString() + @" / " + iTotal);
                        isStatus = 0;
                        mCellWatch.Reset();
                        mCellWatch.Start();
                    }

                    //check if we want to ignore cell based on config
                    bool ignoreCell = false;
                    if (mConfigFileRecord.IgnoreCells.ContainsKey(mEPPExpectedPackage.SelectedSheet.Name) || mConfigFileRecord.SheetIndexesToIgnore.Any(x => x == mEPPExpectedPackage.SelectedSheet.Index))
                    {
                        if (mConfigFileRecord.IgnoreCells.ContainsKey(mEPPExpectedPackage.SelectedSheet.Name))
                        {
                            ignoreCell = mConfigFileRecord.IgnoreCells[mEPPExpectedPackage.SelectedSheet.Name].Any(x => x.Item1 == columnIndex && x.Item2 == rowIndex);
                        }
                        if (mConfigFileRecord.SheetIndexesToIgnore.Any(x => x == mEPPExpectedPackage.SelectedSheet.Index) && !ignoreCell)
                        {
                            ignoreCell = mConfigFileRecord.IgnoreCells["[" + mEPPExpectedPackage.SelectedSheet.Index.ToString() + "]"].Any(x => x.Item1 == columnIndex && x.Item2 == rowIndex);
                        }
                    }
                    if (!ignoreCell && mConfigFileRecord.IgnoreCells.ContainsKey(string.Empty))
                        ignoreCell = mConfigFileRecord.IgnoreCells[string.Empty].Any(x => x.Item1 == columnIndex && x.Item2 == rowIndex);


                    List<DocDiffCompareDiffRecord> cellresults = ignoreCell ? new List<DocDiffCompareDiffRecord>() : CompareCells(rowIndex, columnIndex);
                    if (cellresults.Count > 0)
                    {
                        foreach (DocDiffCompareDiffRecord cdr in cellresults)
                        {
                            results.Add(cdr);

                            if (mConfigFileRecord.ErrorThreshold > 0)
                            {
                                if (cdr.messagetype > 0)
                                {
                                    iErrorCount++;
                                }
                            }
                        }
                    }
                    if (mConfigFileRecord.ErrorThreshold > 0)
                    {
                        if (iErrorCount >= mConfigFileRecord.ErrorThreshold)
                        {
                            bErrorThresholdReached = true;
                            Console.WriteLine("      Error Threshold Reached: " + iErrorCount.ToString());
                            mCellWatch.Stop();
                            Console.WriteLine("      Final [" + (iTotal - iCurrent).ToString() + "]Cells Examined in: " + new TimeSpan(0, 0, 0, 0, (int)mCellWatch.ElapsedMilliseconds).ToString());
                            return;
                        }
                    }
                }
            }
            mCellWatch.Stop();
            Console.WriteLine("      Final Cells Examined in: " + new TimeSpan(0, 0, 0, 0, (int)mCellWatch.ElapsedMilliseconds).ToString());
        }

        /// <summary>
        /// Compares cell data, formula, conditional formatting, and styles
        /// </summary>
        /// <param name="row">specified row</param>
        /// <param name="col">specified column</param>
        /// <returns></returns>
        private List<DocDiffCompareDiffRecord> CompareCells(int row, int col)
        {
            bool bFormulaConversionError = false;
            List<DocDiffCompareDiffRecord> results = new List<DocDiffCompareDiffRecord>();
            ExcelRange mExpectedCell = mEPPExpectedPackage.SetWorkingCell(row, col);
            ExcelRange mActualCell = mEPPActualPackage.SetWorkingCell(row, col);
            string sExpectedVal = null;
            string sActualVal = null;
            string sExpectedValOrig = "";
            string sActualValOrig = "";
            bool expectedProcessed = false;
            bool actualProcessed = false;

            if (mExpectedCell?.Text != null)
            {
                sExpectedVal = mExpectedCell.Text.ToString();
            }
            
            if(mActualCell?.Text != null)
            {
                sActualVal = mActualCell.Text.ToString();
            }

            if (sActualVal == "" && sExpectedVal == "")
            {
                //if both empty don't proceed
                return results;
            }
            else
            {
                if (mConfigFileRecord.TreatCellsWithOnlySpacesAsEmpty)
                {
                    if (sExpectedVal?.Trim() == "")
                    {
                        sExpectedVal = sExpectedVal.Trim();
                    }

                    if (sActualVal?.Trim() == "")
                    {
                        sActualVal = sActualVal.Trim();
                    }

                    if (((sExpectedVal == "") && (sActualVal == "")) || ((sExpectedVal == null) && (sActualVal == null)))
                    {
                        return results;
                    }
                }

                if (!(mConfigFileRecord.IgnoreDateErrors && (DateTime.TryParse(sExpectedVal, out _) && DateTime.TryParse(sActualVal, out _))))
                {
                    if (mConfigFileRecord.IgnoreDateErrors)
                    {
                        ProcessDateTimeText(ref sExpectedValOrig, ref sExpectedVal, ref expectedProcessed);
                        ProcessDateTimeText(ref sActualValOrig, ref sActualVal, ref actualProcessed);
                    }
                    Hashtable mExpectedFormatData = new Hashtable();
                    Hashtable mActualFormatData = new Hashtable();

                    if (mConfigFileRecord.ConditionalFormatting)
                    {
                        mExpectedFormatData.Add("ConditionalFormattingCount", mEPPExpectedPackage.ConditionalFormats.Count.ToString());
                        mActualFormatData.Add("ConditionalFormattingCount", mEPPActualPackage.ConditionalFormats.Count.ToString());

                        for (int i = 1; i <= GetMaxConditionalFormattingCount(); i++)
                        {
                            int currentCellIndex = i - 1;

                            if (mConfigFileRecord.CondFrmt_Formula1)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]Formula1", mEPPExpectedPackage.GetCondFrmtFormula(currentCellIndex));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]Formula1", mEPPActualPackage.GetCondFrmtFormula(currentCellIndex));
                            }
       
                            if (mConfigFileRecord.CondFrmt_InteriorColorIndex)
                            {                                
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorColorIndex", mEPPExpectedPackage.GetCondFrmtFill(currentCellIndex, "index"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorColorIndex", mEPPActualPackage.GetCondFrmtFill(currentCellIndex, "index"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorColor)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorColor", mEPPExpectedPackage.GetCondFrmtFill(currentCellIndex, "color"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorColor", mEPPActualPackage.GetCondFrmtFill(currentCellIndex, "color"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorPattern)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorPattern", mEPPExpectedPackage.GetCndFrmtPatternType(currentCellIndex));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorPattern", mEPPActualPackage.GetCndFrmtPatternType(currentCellIndex));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorPatternColor)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternColor", mEPPExpectedPackage.GetCondFrmtPattern(currentCellIndex, "color"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternColor", mEPPActualPackage.GetCondFrmtPattern(currentCellIndex, "color"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorPatternColorIndex)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternColorIndex", mEPPExpectedPackage.GetCondFrmtPattern(currentCellIndex, "index"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternColorIndex", mEPPActualPackage.GetCondFrmtPattern(currentCellIndex, "index"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorPatternThemeColor)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternThemeColor", mEPPExpectedPackage.GetCondFrmtPattern(currentCellIndex, "theme"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternThemeColor", mEPPActualPackage.GetCondFrmtPattern(currentCellIndex, "theme"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorPatternTintAndShade)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternTintAndShade", mEPPExpectedPackage.GetCondFrmtPattern(currentCellIndex, "tint"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorPatternTintAndShade", mEPPActualPackage.GetCondFrmtPattern(currentCellIndex, "tint"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorThemeColor)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorThemeColor", mEPPExpectedPackage.GetCondFrmtFill(currentCellIndex, "theme"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorThemeColor", mEPPActualPackage.GetCondFrmtFill(currentCellIndex, "theme"));
                            }

                            if (mConfigFileRecord.CondFrmt_InteriorTintAndShade)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]InteriorTintAndShade", mEPPExpectedPackage.GetCondFrmtFill(currentCellIndex, "tint"));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]InteriorTintAndShade", mEPPActualPackage.GetCondFrmtFill(currentCellIndex, "tint"));
                            }

                            if (mConfigFileRecord.CondFrmt_NumberFormat)
                            {
                                mExpectedFormatData.Add("ConditionalFormatting[" + i + "]NumberFormat", mEPPExpectedPackage.GetCondFrmtNumberFormat(currentCellIndex));
                                mActualFormatData.Add("ConditionalFormatting[" + i + "]NumberFormat", mEPPActualPackage.GetCondFrmtNumberFormat(currentCellIndex));
                            }
                        }
                    }

                    if (mConfigFileRecord.Value)
                    {
                        // if the formula is the value, then we only have a value
                        String sExpectedFormula = "";
                        try
                        {
                            sExpectedFormula = Convert.ToString(mExpectedCell?.Value);
                        }
                        catch
                        {
                            bFormulaConversionError = true;
                        }
                        if (sExpectedVal == sExpectedFormula)
                        {
                            mExpectedFormatData.Add("Value", sExpectedVal);
                            mActualFormatData.Add("Value", sActualVal);
                        }
                        else
                        {
                            bool bDblConverted = false;
                            if (mConfigFileRecord.RoundingPrecisionForFormulaValues < 0)
                            {
                                mExpectedFormatData.Add("Value", sExpectedVal);
                                mActualFormatData.Add("Value", sActualVal);
                            }
                            else
                            {
                                if (double.TryParse(sExpectedVal, out double dValExpected))
                                {
                                    if (double.TryParse(Convert.ToString(mActualCell.Text), out double dValActual))
                                    {
                                        dValExpected = Math.Round(dValExpected, mConfigFileRecord.RoundingPrecisionForFormulaValues);
                                        dValActual = Math.Round(dValActual, mConfigFileRecord.RoundingPrecisionForFormulaValues);
                                        mExpectedFormatData.Add("Value", dValExpected);
                                        mActualFormatData.Add("Value", dValActual);
                                        bDblConverted = true;
                                    }
                                }
                                if (!bDblConverted)
                                {
                                    mExpectedFormatData.Add("Value", sExpectedVal);
                                    mActualFormatData.Add("Value", sActualVal);
                                }
                            }
                        }

                    }

                    if (mConfigFileRecord.Formula)
                    {
                        if (!bFormulaConversionError)
                        {
                            mExpectedFormatData.Add("Formula", mExpectedCell?.Formula);
                            mActualFormatData.Add("Formula", mActualCell?.Formula);
                        }
                    }

                    if (mConfigFileRecord.Font_Bold)
                    {
                        mExpectedFormatData.Add("Font_Bold", mExpectedCell?.Style.Font.Bold);
                        mActualFormatData.Add("Font_Bold", mActualCell?.Style.Font.Bold);

                    }

                    if (mConfigFileRecord.Font_Bck)
                    {
                        mExpectedFormatData.Add("Font_Bck", mEPPExpectedPackage.GetFontBackGround());
                        mActualFormatData.Add("Font_Bck", mEPPActualPackage.GetFontBackGround());
                    }

                    if (mConfigFileRecord.Font_Color)
                    {
                        mExpectedFormatData.Add("Font_Color", mEPPExpectedPackage.GetFontColor());
                        mActualFormatData.Add("Font_Color", mEPPActualPackage.GetFontColor());
                    }

                    if (mConfigFileRecord.Font_Style)
                    {
                        string getFontStyle(ExcelFont font)
                        {                            
                            if (font != null)
                            {
                                string result = string.Empty;
                                if (font.Bold)
                                    result = "Bold";
                                if (font.Italic)
                                    result += (result == "" ? "" : " ") + "Italic";

                                result = result == "" ? "Regular" : result;
                                return result;
                            }
                            else
                            {
                                return null;
                            }
                        }

                        mExpectedFormatData.Add("Font_Style", getFontStyle(mExpectedCell?.Style.Font));
                        mActualFormatData.Add("Font_Style", getFontStyle(mActualCell?.Style.Font));
                    }

                    if (mConfigFileRecord.Font_Italic)
                    {
                        mExpectedFormatData.Add("Font_Italic", mExpectedCell?.Style.Font.Italic);
                        mActualFormatData.Add("Font_Italic", mActualCell?.Style.Font.Italic);
                    }

                    if (mConfigFileRecord.Font_Name)
                    {
                        mExpectedFormatData.Add("Font_Name", mExpectedCell?.Style.Font.Name);
                        mActualFormatData.Add("Font_Name", mActualCell?.Style.Font.Name);
                    }

                    if (mConfigFileRecord.Font_Size)
                    {
                        mExpectedFormatData.Add("Font_Size", mExpectedCell?.Style.Font.Size);
                        mActualFormatData.Add("Font_Size", mActualCell?.Style.Font.Size);
                    }

                    if (mConfigFileRecord.Font_Underline)
                    {
                        mExpectedFormatData.Add("Font_Underline", mExpectedCell?.Style.Font.UnderLine);
                        mActualFormatData.Add("Font_Underline", mActualCell?.Style.Font.UnderLine);
                    }

                    if (mConfigFileRecord.Font_Strike)
                    {
                        mExpectedFormatData.Add("Font_Strike", mExpectedCell?.Style.Font.Strike);
                        mActualFormatData.Add("Font_Strike", mActualCell?.Style.Font.Strike);
                    }

                    if (mConfigFileRecord.Borders_Value)
                    {
                        mExpectedFormatData.Add("Borders_Value", mEPPExpectedPackage.GetBorderStyle());
                        mActualFormatData.Add("Borders_Value", mEPPActualPackage.GetBorderStyle());
                    }

                    if (mConfigFileRecord.Borders_Weight)
                    {
                        mExpectedFormatData.Add("Borders_Weight", mEPPExpectedPackage.GetBorderWeight());
                        mActualFormatData.Add("Borders_Weight", mEPPActualPackage.GetBorderWeight());
                    }

                    if (mConfigFileRecord.Borders_Color)
                    {
                        mExpectedFormatData.Add("Borders_Color", mEPPExpectedPackage.GetBorderColor());
                        mActualFormatData.Add("Borders_Color", mEPPActualPackage.GetBorderColor());
                    }

                    if (mConfigFileRecord.Borders_Count)
                    {
                        mExpectedFormatData.Add("Borders_Count", mEPPExpectedPackage.GetBorderCount());
                        mActualFormatData.Add("Borders_Count", mEPPActualPackage.GetBorderCount());
                    }

                    if (mConfigFileRecord.ColumnWidth)
                    {
                        mExpectedFormatData.Add("ColumnWidth", mEPPExpectedPackage.GetColumnWidth(col));
                        mActualFormatData.Add("ColumnWidth", mEPPActualPackage.GetColumnWidth(col));
                    }

                    if (mConfigFileRecord.Height)
                    {
                        mExpectedFormatData.Add("Height", string.Format("{0:0.0}", Math.Truncate(mEPPExpectedPackage.SelectedSheet.Row(row).Height * 10) / 10)); 
                        mActualFormatData.Add("Height", string.Format("{0:0.0}", Math.Truncate(mEPPActualPackage.SelectedSheet.Row(row).Height * 10) / 10));
                    }

                    if (mConfigFileRecord.Left)
                    {
                        mExpectedFormatData.Add("Left", mEPPExpectedPackage.CurrentCell?.Style.Indent);
                        mActualFormatData.Add("Left", mEPPActualPackage.CurrentCell?.Style.Indent);
                    }

                    if (mConfigFileRecord.NumberFormat)
                    {
                        mExpectedFormatData.Add("NumberFormat", mEPPExpectedPackage.CurrentCell?.Style.Numberformat.Format);
                        mActualFormatData.Add("NumberFormat", mEPPActualPackage.CurrentCell?.Style.Numberformat.Format);
                    }

                    if (mConfigFileRecord.Orientation)
                    {
                        mExpectedFormatData.Add("Orientation", mEPPExpectedPackage.CurrentCell?.Style.TextRotation);
                        mActualFormatData.Add("Orientation", mEPPActualPackage.CurrentCell?.Style.TextRotation);
                    }

                    if (mConfigFileRecord.RowHeight)
                    {
                        mExpectedFormatData.Add("RowHeight", string.Format("{0:0.0}", Math.Truncate(mEPPExpectedPackage.SelectedSheet.Row(row).Height * 10) / 10));
                        mActualFormatData.Add("RowHeight", string.Format("{0:0.0}", Math.Truncate(mEPPActualPackage.SelectedSheet.Row(row).Height * 10) / 10));
                    }

                    if (mConfigFileRecord.Style)
                    {
                        string convertOldStyle(ExcelRange _style)
                        {
                            if (_style != null)
                            {
                                return string.IsNullOrEmpty(_style.StyleName) ? "Normal" : _style.StyleName;
                            }
                            return null;
                        }

                        mExpectedFormatData.Add("Style", convertOldStyle(mEPPExpectedPackage.CurrentCell));
                        mActualFormatData.Add("Style", convertOldStyle(mEPPActualPackage.CurrentCell));
                    }

                    if (mConfigFileRecord.VerticalAlignment)
                    {
                        mExpectedFormatData.Add("VerticalAlignment", mEPPExpectedPackage.CurrentCell?.Style.VerticalAlignment);
                        mActualFormatData.Add("VerticalAlignment", mEPPActualPackage.CurrentCell?.Style.VerticalAlignment);
                    }

                    if (mConfigFileRecord.Width)
                    {
                        mExpectedFormatData.Add("Width", mEPPExpectedPackage.GetColumnWidth(col));
                        mActualFormatData.Add("Width", mEPPActualPackage.GetColumnWidth(col));
                    }

                    if (mConfigFileRecord.WrapText)
                    {
                        mExpectedFormatData.Add("WrapText", mEPPExpectedPackage.CurrentCell?.Style.WrapText);
                        mActualFormatData.Add("WrapText", mEPPActualPackage.CurrentCell?.Style.WrapText);
                    }

                    if ((mConfigFileRecord.Formula) && (bFormulaConversionError))
                    {
                        string sResults = "Formula conversion error. Sheet: " + mEPPExpectedPackage.SelectedSheet.Name + ", Row: " + row.ToString() + ", Column: " + col.ToString();
                        DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(1, "CellDataComparison", sResults, "Unknown", "Unknown");
                        results.Add(cdr);
                    }

                    foreach (DictionaryEntry de in mExpectedFormatData)
                    {
                        int iType = 1;
                        try
                        {
                            string sKey = "";
                            string sExpected = null;
                            string sActual = null;

                            if (de.Key != null)
                            {
                                sKey = de.Key.ToString();
                            }

                            if (mExpectedFormatData[de.Key] != null)
                            {
                                sExpected = mExpectedFormatData[de.Key].ToString();
                            }

                            if (mActualFormatData[de.Key] != null)
                            {
                                sActual = mActualFormatData[de.Key].ToString();
                            }

                            String sResults = "Cell difference found. Sheet: " + mEPPExpectedPackage.SelectedSheet.Name + ", Row: " + row.ToString() + ", Column: " + col.ToString()
                                + ", Key; " + sKey;
                            if (sExpected == sActual)
                            {
                                if (!mErrorsOnly)
                                {
                                    iType = 0;
                                    sResults = "Cell matches. Sheet: " + mEPPExpectedPackage.SelectedSheet.Name + ", Row: " + row.ToString() + ", Column: " + col.ToString()
                                        + ", Key; " + sKey;
                                    DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, "CellDataComparison", sResults, sExpected, sActual);
                                    results.Add(cdr);
                                }
                            }
                            else
                            {
                                if (expectedProcessed)
                                {
                                    sExpected = sExpectedValOrig;
                                }
                                if (actualProcessed)
                                {
                                    sActual = sActualValOrig;
                                }
                                DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, "CellDataComparison", sResults, sExpected??NULL, sActual??NULL);
                                results.Add(cdr);
                            }
                        }
                        catch
                        {
                            string sResults = "Cell difference found. Unknown issue. Sheet: " + mEPPExpectedPackage.SelectedSheet.Name + ", Row: " + row.ToString() + ", Column: " + col.ToString();
                            DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, "CellDataComparison", sResults, "Unknown", "Unknown");
                            results.Add(cdr);
                        }
                    }

                }
            }
            return results;
        }

        /// <summary>
        /// Get maximum conditional formatting count
        /// </summary>
        /// <returns></returns>
        private int GetMaxConditionalFormattingCount()
        {
            int actual = mEPPActualPackage.ConditionalFormats.Count;
            int expected = mEPPExpectedPackage.ConditionalFormats.Count;

            return expected >= actual ? expected : actual;
        }

        /// <summary>
        /// Get the maximum row connt
        /// </summary>
        /// <returns>integer value</returns>
        private int GetMaxRowCount()
        {
            int actual = mEPPActualPackage.RowCount;
            int expected = mEPPExpectedPackage.RowCount;
            return expected >= actual ? expected : actual;
        }

        /// <summary>
        /// Get the maximum column count
        /// </summary>
        /// <returns>integer value</returns>
        private int GetMaxColumnCount()
        {
            int actual = mEPPActualPackage.ColumnCount;
            int expected = mEPPExpectedPackage.ColumnCount;
            return expected >= actual ? expected : actual;
        }

        /// <summary>
        /// Compares the actual and expected files column count
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareEPPColumnCount(ref List<DocDiffCompareDiffRecord> results)
        {
            if (mEPPExpectedPackage.ColumnCount == mEPPActualPackage.ColumnCount)
            {
                if (!mErrorsOnly)
                {
                    DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info, 
                        TEST_TYPE_COLUMN_COUNT_COMPARISON,
                        string.Format(COLUMN_COUNT_MATCH, mEPPExpectedPackage.SelectedSheet.Name, mEPPExpectedPackage.ColumnCount), 
                        mEPPExpectedPackage.ColumnCount.ToString(), 
                        mEPPActualPackage.ColumnCount.ToString());
                    results.Add(record);
                }
            }
            else
            {
                DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord((int)MessageType.Error, TEST_TYPE_COLUMN_COUNT_COMPARISON,
                        string.Format(COLUMN_COUNT_NOT_MATCH, mEPPExpectedPackage.SelectedSheet.Name, mEPPExpectedPackage.ColumnCount, mEPPActualPackage.ColumnCount),
                        mEPPExpectedPackage.ColumnCount.ToString(), 
                        mEPPActualPackage.ColumnCount.ToString());
                results.Add(cdr);
            }
        }

        /// <summary>
        /// Compares the actual and expected files row count
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareEPPRowCount(ref List<DocDiffCompareDiffRecord> results)
        {
            if (mEPPExpectedPackage.RowCount == mEPPActualPackage.RowCount)
            {
                if (!mErrorsOnly)
                {
                    DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info, 
                        TEST_TYPE_ROW_COUNT_COMPARISON,
                        string.Format(ROW_COUNT_MATCH, mEPPExpectedPackage.SelectedSheet.Name, mEPPExpectedPackage.RowCount),
                        mEPPExpectedPackage.RowCount.ToString(), 
                        mEPPActualPackage.RowCount.ToString());
                    results.Add(record);
                }
            }
            else
            {
                DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord((int)MessageType.Error, 
                        TEST_TYPE_ROW_COUNT_COMPARISON,
                        string.Format(ROW_COUNT_NOT_MATCH, mEPPExpectedPackage.SelectedSheet.Name, mEPPExpectedPackage.RowCount, mEPPActualPackage.RowCount),
                        mEPPExpectedPackage.RowCount.ToString(), 
                        mEPPActualPackage.RowCount.ToString());
                results.Add(cdr);
            }
        }

        /// <summary>
        /// Compares the actual and expected files sheet name
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareEPPSheetNames(ref List<DocDiffCompareDiffRecord> results)
        {
            for (int i = 1; i <= mEPPExpectedPackage.SheetsCount; i++)
            {
                mEPPExpectedPackage.SetWorkSheet(i);
                mEPPActualPackage.SetWorkSheet(i);

                if (mEPPExpectedPackage.SelectedSheet.Name == mEPPActualPackage.SelectedSheet?.Name)
                {
                    if (!mErrorsOnly)
                    {
                        DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info, 
                            TEST_TYPE_SHEETNAME_COMPARISON,
                            WORKSHEET_NAMES_MATCH, 
                            mEPPExpectedPackage.SelectedSheet.Name, 
                            mEPPActualPackage.SelectedSheet.Name);
                        results.Add(record);
                    }
                }
                else
                {
                    DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Error, 
                            TEST_TYPE_SHEETNAME_COMPARISON, 
                            WORKSHEET_NAMES_DIFFERENT, 
                            mEPPExpectedPackage.SelectedSheet.Name, 
                            mEPPActualPackage.SelectedSheet?.Name);
                    results.Add(record);
                }
            }
        }

        /// <summary>
        /// Checks whether text contains datetime and process text if there is any
        /// </summary>
        /// <param name="original">Original text</param>
        /// <param name="processed">Text after being processed</param>
        /// <param name="status">Flag whether text has been processed</param>
        private void ProcessDateTimeText(ref string original, ref string processed, ref bool status)
        {
            if (processed != null && DlkDateTimeHelper.HasDateAndTime(processed, out List<string> textsToBeRemoved))
            {
                original = processed;
                status = true;
                foreach (string text in textsToBeRemoved)
                {
                    processed = processed.Replace(text, "");
                }
            }
        }

        /// <summary>
        /// Dispose EPPLUS excel package and set to null objects
        /// </summary>
        public void Dispose()
        {
            mEPPExpectedPackage.Package.Dispose();
            mEPPActualPackage.Package.Dispose();
            mEPPExpectedPackage = null;
            mEPPActualPackage = null;
        }
    }

    /// <summary>
    /// Class containing excel package
    /// </summary>
    public class ExcelEPPData: ConditionalFormatting
    {
        /// <summary>
        /// Loads excel file to EPPLUS excel package
        /// </summary>
        /// <param name="filePath">excel file full path</param>
        public ExcelEPPData(string filePath)
        {
            FileInfo fileInfo;
            fileInfo = new FileInfo(filePath);

            this.Package = new ExcelPackage(fileInfo);
            SheetsCount = Worksheets.Count;
        }

        /// <summary>
        /// Current excel package
        /// </summary>
        public ExcelPackage Package { get; set; }

        /// <summary>
        /// Available workbook of the excel package
        /// </summary>
        public ExcelWorkbook Workbook => Package.Workbook;

        /// <summary>
        /// Available worksheets of the loaded excel package
        /// </summary>
        public ExcelWorksheets Worksheets => Workbook.Worksheets;

        public int SheetsCount { get; private set; }

        /// <summary>
        /// Current sheet column count
        /// </summary>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Total used cell of the current sheet
        /// </summary>
        public int TotalCellsCount { get; private set; }

        /// <summary>
        /// Current sheet row count
        /// </summary>
        public int RowCount { get; private set; }

        public ExcelWorksheet SelectedSheet { get; private set; }

        /// <summary>
        /// Contains the properties and methods of the current cell
        /// </summary>
        public ExcelRange CurrentCell { get; private set; }

        /// <summary>
        /// Sets the current working cell
        /// </summary>
        /// <param name="row">sheet row</param>
        /// <param name="col">sheet column</param>
        /// <returns>Cell properties and methods</returns>
        public ExcelRange SetWorkingCell(int row, int col)
        {
            if (RowCount >= row && ColumnCount >= col)
            {
                CurrentCell = SelectedSheet.Cells[row, col];
            }
            else
            {
                CurrentCell = null;
            }

            var currentCellConditionalFormats = new List<IExcelConditionalFormattingRule>();
            foreach (var frmt in SelectedSheet.ConditionalFormatting)
            {
                if (IsInRange(row, frmt.Address.Start.Row, frmt.Address.Rows) && IsInRange(col, frmt.Address.Start.Column, frmt.Address.Columns))
                {
                    currentCellConditionalFormats.Add(frmt);                    
                }
            }
            SetCellBorderStyle(CurrentCell?.Style.Border);
            ConditionalFormats = currentCellConditionalFormats;
            return CurrentCell;
        }

        private bool IsInRange(int val, int from, int to)
        {
            to = from + to;
            return val >= from && val <= to;
        }

        /// <summary>
        /// Returns the column width of the specified column
        /// </summary>
        /// <param name="col">index of the working column</param>
        /// <returns></returns>
        public string GetColumnWidth(int col)
        {
            return Math.Round(SelectedSheet.Column(col).Width - (5 / 7.86),1).ToString();
        }

        /// <summary>
        /// Set the current working sheet of the excel package
        /// </summary>
        /// <param name="index">excel sheet index</param>
        public void SetWorkSheet(int index)
        {
            if (this.Worksheets.Count >= index)
            {
                SelectedSheet = this.Worksheets[index];
                SetSheetColumnRowCount();
            }
            else
            {
                SelectedSheet = null;
            }
        }

        /// <summary>
        /// Returns fill color of the current cell
        /// </summary>
        /// <returns>RGB for standard color or TINT and Theme for office theme colors</returns>
        public string GetFontBackGround()
        {
            var cellBackground = CurrentCell?.Style.Fill.BackgroundColor;

            if (cellBackground != null)
            {
                if (string.IsNullOrEmpty(cellBackground.Rgb))
                {
                    return $"Theme = {cellBackground.Theme};Tint = {cellBackground.Tint}";
                }
                else
                {
                    //support older version of office
                    string rgb = cellBackground.Rgb.Length >= 2 && !cellBackground.Rgb.StartsWith("FF") ? $"FF{cellBackground.Rgb}" : cellBackground.Rgb;
                    return $"RGB = {rgb}";
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns font color of the current cell
        /// </summary>
        /// <returns>RGB for standard color or TINT and Theme for office theme colors</returns>
        public string GetFontColor()
        {
            var fontColor = CurrentCell?.Style.Font.Color;

            if (fontColor != null)
            {
                if (string.IsNullOrEmpty(fontColor.Rgb))
                {
                    return $"Theme = {fontColor.Theme};Tint = {fontColor.Tint}";
                }
                else
                {
                    //support older version of office
                    string rgb = fontColor.Rgb.Length >= 2 && !fontColor.Rgb.StartsWith("FF") ? $"FF{fontColor.Rgb}" : fontColor.Rgb;
                    return $"RGB = {rgb}";
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Calculates the current sheet column count, row count, and total cell count
        /// </summary>
        private void SetSheetColumnRowCount()
        {
            ColumnCount = SelectedSheet.Dimension != null ? SelectedSheet.Dimension.Columns : 0;
            RowCount = SelectedSheet.Dimension != null ? SelectedSheet.Dimension.Rows : 0;
            TotalCellsCount = ColumnCount * RowCount;
        }
    }

    /// <summary>
    /// Class for cell conditional formats
    /// </summary>
    public class ConditionalFormatting: CellBorder
    {
        /// <summary>
        /// List of conditional formats of the current working cell
        /// </summary>
        public List<IExcelConditionalFormattingRule> ConditionalFormats { get; set; }

        /// <summary>
        /// Returns the conditional formatting formula of the specified formatting index
        /// </summary>
        /// <param name="formatIndex">Conditional formatting index (Note: cell can have multiple conditional format)</param>
        /// <returns></returns>
        public string GetCondFrmtFormula(int formatIndex)
        {
            if (ConditionalFormats.Count < formatIndex + 1)
                return null;

            var format = ConditionalFormats[formatIndex];
            return format.Type.ToString() + " " + GetPropertyValue("Formula", format);
        }

        /// <summary>
        /// Returns the conditional formatting number format of the specified formatting index
        /// </summary>
        /// <param name="formatIndex">Conditional formatting index (Note: cell can have multiple conditional format)</param>
        /// <returns></returns>
        public string GetCondFrmtNumberFormat(int formatIndex)
        {
            if (ConditionalFormats.Count < formatIndex + 1)
                return null;

            var format = ConditionalFormats[formatIndex].Style.NumberFormat.Format;
            return format;
        }

        /// <summary>
        /// Returns the conditional formatting Fill Color of the specified formatting index
        /// </summary>
        /// <param name="formatIndex">Conditional formatting index (Note: cell can have multiple conditional format)</param>
        /// <param name="type">type of fill (ex. Color, Index, Tint)</param>
        /// <returns></returns>
        public string GetCondFrmtFill(int formatIndex, string type)
        {
            if (ConditionalFormats.Count < formatIndex + 1)
                return null;

            ExcelDxfColor backgroundColor = ConditionalFormats[formatIndex].Style.Fill.BackgroundColor;
            string result;
            switch (type)
            {
                case "color":
                    result = backgroundColor.Color?.ToString();
                    break;
                case "index":
                    result = backgroundColor.Color?.Name.ToString();
                    break;
                case "tint":
                    result = backgroundColor.Tint?.ToString();
                    break;
                case "theme":
                    result = backgroundColor.Theme?.ToString();
                    break;
                default:
                    throw new Exception($"GetCondFrmtFillColorOrIndex() : Unsupported Fill Type '{type}'");
            }
            return result;
        }

        /// <summary>
        /// Returns the conditional formatting pattern of the specified format index
        /// </summary>
        /// <param name="formatIndex">Conditional formatting index (Note: cell can have multiple conditional format)</param>
        /// <param name="type">type of pattern (ex. Color, Index, Tint)</param>
        /// <returns></returns>
        public string GetCondFrmtPattern(int formatIndex, string type)
        {
            if (ConditionalFormats.Count < formatIndex + 1)
                return null;

            ExcelDxfColor patternColor = ConditionalFormats[formatIndex].Style.Fill.PatternColor;

            string result;
            switch (type)
            {
                case "color":
                    result = patternColor.Color?.ToString();
                    break;
                case "index":
                    result = patternColor.Color?.Name.ToString();
                    break;
                case "tint":
                    result = patternColor.Tint?.ToString();
                    break;
                case "theme":
                    result = patternColor.Theme?.ToString();
                    break;
                default:
                    throw new Exception($"GetCondFrmtPattern() : Unsupported Pattern Type '{type}'");
            }
            return result;
        }

        /// <summary>
        /// Returns the conditional formatting pattern type of the specified format index
        /// </summary>
        /// <param name="formatIndex">Conditional formatting index (Note: cell can have multiple conditional format)</param>
        /// <returns></returns>
        public string GetCndFrmtPatternType(int formatIndex)
        {
            if (ConditionalFormats.Count < formatIndex + 1)
                return null;

            ExcelDxfFill pattern = ConditionalFormats[formatIndex].Style.Fill;
            return pattern.PatternType.ToString();
        }

        private string GetPropertyValue(string propertyName, object obj)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj).ToString();
        }
    }

    /// <summary>
    /// Class for border style
    /// </summary>
    public class CellBorder
    {
        private Border cellBorder;
        private ExcelBorderItem TopBorder;
        private ExcelBorderItem BottomBorder;
        private ExcelBorderItem LeftBorder;
        private ExcelBorderItem RightBorder;
        private ExcelBorderItem DiagonalBorder;

        /// <summary>
        /// Checks if working cell contains top border
        /// </summary>
        private bool HasTopBorder => TopBorder != null && TopBorder.Style != ExcelBorderStyle.None;

        /// <summary>
        /// Checks if working cell contains bottom border
        /// </summary>
        private bool HasBottomBorder => BottomBorder != null && BottomBorder.Style != ExcelBorderStyle.None;

        /// <summary>
        /// Checks if working cell contains right border
        /// </summary>
        private bool HasRightBorder => RightBorder != null && RightBorder.Style != ExcelBorderStyle.None;

        /// <summary>
        /// Checks if working cell contains left border
        /// </summary>
        private bool HasLeftBorder => LeftBorder != null && LeftBorder.Style != ExcelBorderStyle.None;

        /// <summary>
        /// Sets the border style of the working cell
        /// </summary>
        /// <param name="CellBorderStyle">Current cell border style</param>
        public void SetCellBorderStyle(Border CellBorderStyle)
        {
            cellBorder = CellBorderStyle;
            TopBorder = cellBorder?.Top;
            BottomBorder = cellBorder?.Bottom;
            LeftBorder = cellBorder?.Left;
            RightBorder = cellBorder?.Right;
            DiagonalBorder = cellBorder?.Diagonal;
        }

        /// <summary>
        /// Checks if the working cell have diagonal borders
        /// </summary>
        /// <param name="up">diagonal up</param>
        /// <param name="down">diagonal down</param>
        /// <returns>true/false</returns>
        private bool HasDiagonalBorder(out bool up, out bool down)
        {
            up = cellBorder?.DiagonalUp??false;
            down = cellBorder?.DiagonalDown??false;
            return DiagonalBorder != null && DiagonalBorder.Style != ExcelBorderStyle.None;
        }

        /// <summary>
        /// Returns the available border count of the working cell
        /// </summary>
        /// <returns>border count</returns>
        public string GetBorderCount()
        {
            int borderCount = 0;
            if (HasBottomBorder)
                borderCount++;

            if (HasTopBorder)
                borderCount++;

            if (HasLeftBorder)
                borderCount++;

            if (HasRightBorder)
                borderCount++;

            if (HasDiagonalBorder(out bool up, out bool down))
            {
                if (up)
                    borderCount++;
                if (down)
                    borderCount++;
            }

            return borderCount.ToString();
        }

        /// <summary>
        /// Returns the available border styles of the working cell
        /// </summary>
        /// <returns>border styles of working cell</returns>
        public string GetBorderStyle()
        {
            string result = "";

            if (HasTopBorder)
                result = $"Top={TopBorder.Style};";
            if (HasBottomBorder)
                result += $"Bottom={BottomBorder.Style};";
            if (HasLeftBorder)
                result += $"Left={LeftBorder.Style};";
            if (HasRightBorder)
                result += $"Right={RightBorder.Style};";
            if (HasDiagonalBorder(out bool up, out bool down))
            { 
                if(up)
                    result += $"DiagonalUp={DiagonalBorder.Style};";
                if(down)
                    result += $"DiagonalDown={DiagonalBorder.Style};";
            }
            return result;
        }

        /// <summary>
        /// Returns the available border colors of the working cell
        /// </summary>
        /// <returns>border colors of the working cell</returns>
        public string GetBorderColor()
        {
            string result = "";
            string getColor(ExcelColor color) => color.Rgb != "" ? color.Rgb : $"Theme {color.Theme};Tint {color.Tint}";

            if (HasTopBorder)
            {
                result = $"Top={getColor(TopBorder.Color)};";
            }
            if (HasBottomBorder)
                result += $"Bottom={getColor(BottomBorder.Color)};";
            if (HasLeftBorder)
                result += $"Left={getColor(LeftBorder.Color)};";
            if (HasRightBorder)
                result += $"Right={getColor(RightBorder.Color)};";
            if (HasDiagonalBorder(out bool up, out bool down))
            {
                if (up)
                    result += $"DiagonalUp={getColor(DiagonalBorder.Color)};";
                if (down)
                    result += $"DiagonalDown={getColor(DiagonalBorder.Color)};";
            }
            return result;
        }

        /// <summary>
        /// Returns the weight of available borders
        /// </summary>
        /// <returns>Border style weight</returns>
        public string GetBorderWeight()
        {
            string result = "";

            if (HasTopBorder)
                result = $"Top={(int)TopBorder.Style};";
            if (HasBottomBorder)
                result += $"Bottom={(int)BottomBorder.Style};";
            if (HasLeftBorder)
                result += $"Left={(int)LeftBorder.Style};";
            if (HasRightBorder)
                result += $"Right={(int)RightBorder.Style};";
            if (HasDiagonalBorder(out bool up, out bool down))
            {
                if (up)
                    result += $"DiagonalUp={(int)DiagonalBorder.Style};";
                if (down)
                    result += $"DiagonalDown={(int)DiagonalBorder.Style};";
            }
            return result;
        }
    }

    /// <summary>
    /// Use this ENUM in comparison result message type
    /// </summary>
    enum MessageType
    { 
        Info = 0,
        Error = 1
    }
}
