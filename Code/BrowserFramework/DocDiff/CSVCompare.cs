using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DocDiff
{
    public class CSVCompare
    {
        const string NULL = "'null'";
        const string TEST_TYPE_ROW_COUNT_COMPARISON = "RowCountComparison";
        const string TEST_TYPE_COLUMN_COUNT_COMPARISON = "ColumnCountComparison";
        const string TEST_TYPE_DELIMITER_COMPARISON = "DelimiterComparison";
        const string TEST_TYPE_CELL_DATA_COMPARISON = "CellDataComparison";
        const string TEST_TYPE_SHEET_NAME_COMPARISON = "SheetNameComparison";

        const string ROW_COUNT_MATCH = "Row count matches. Sheet: {0}, Row Count: {1}";
        const string ROW_COUNT_NOT_MATCH = "Row count does not match. Sheet: {0}, Row Count Expected: {1}, Row Count Actual: {2}";

        const string DELIMITER_NOT_MATCH = "Separator does not match. Sheet: {0}, Separator Expected: {1}, Separator Actual: {2}";
        const string DELIMITER_MATCH = "Separator matches. Sheet: {0}, Separator: {1}";

        const string COLUMN_COUNT_MATCH = "Column count matches. Sheet: {0}, Column Count: {1}";
        const string COLUMN_COUNT_NOT_MATCH = "Column count does not match. Sheet: {0}, Column Count Expected: {1}, Column Count Actual: {2}";

        const string CELL_DIFFERENCE = "Cell difference found. Sheet: {0}, Row: {1}, Column: {2}, Key: Value";
        const string CELL_MATCHES = "Cell Matches. Sheet: {0}, Row: {1}, Column: {2}, Key: Value";
    
        /// <summary>
        /// CSV file separators
        /// </summary>
        private readonly char[] mDelimiters = new char[] { ',', '|', '\t', ';' }; //you can add supported delimiters here
        
        /// <summary>
        /// The delimiter used in actual csv file
        /// </summary>
        private string mActualDelimiter;

        /// <summary>
        /// The delimiter used in expected csv file
        /// </summary>
        private string mExpectedDelimiter;

        /// <summary>
        /// Config for csv comparison
        /// </summary>
        private ConfigFileRecord mConfigFileRecord;

        /// <summary>
        /// Lines of expected csv file
        /// </summary>
        private readonly List<string> mExpectedData = new List<string>();

        /// <summary>
        /// Lines of actual csv file
        /// </summary>
        private readonly List<string> mActualData = new List<string>();

        private long totalExpectedRows = 0;
        private int totalExpectedColumns = 0;
        private readonly string mExpectedSheetName;
        private readonly string mActualSheetName;
        private readonly bool mErrorsOnly = true; // we used to allow this to be toggled... but too many results (remove readonly if you want to enable this)
        private readonly Stopwatch mWatch;

        /// <summary>
        /// Constructor for CSV Comparison
        /// </summary>
        /// <param name="ExpectedFile">expected csv file full path</param>
        /// <param name="ActualFile">actual csv file full path</param>
        /// <param name="ConfigFile">xml configuration file full path</param>
        public CSVCompare(string ExpectedFile, string ActualFile, string ConfigFile)
        {
            mExpectedSheetName = Path.GetFileNameWithoutExtension(ExpectedFile);
            mActualSheetName = Path.GetFileNameWithoutExtension(ActualFile);
            mExpectedData = File.ReadAllLines(ExpectedFile).ToList();
            mActualData = File.ReadAllLines(ActualFile).ToList();

            mActualDelimiter = GetUsedDelimiter(mActualData.Take(2).ToList());
            mExpectedDelimiter = GetUsedDelimiter(mExpectedData.Take(2).ToList());

            mWatch = new Stopwatch();
            mConfigFileRecord = new ConfigFileRecord();
            mConfigFileRecord.Initialize(ConfigFile);
        }

        /// <summary>
        /// Main CSV files comparison
        /// </summary>
        /// <returns>list of comparison results</returns>
        public List<DocDiffCompareDiffRecord> CompareCSV()
        {
            List<DocDiffCompareDiffRecord> results = new List<DocDiffCompareDiffRecord>();

            void resetTimer()
            {
                mWatch.Reset();
                mWatch.Start();
            }

            try
            {              
                /* ------------ RESERVE CODE FOR CSV SEPARATOR COMPARISON --------------------------
                        Remove line 58 and 59 if you want use this type of compasion

                Console.WriteLine("    Starting Separator Comparison...");
                resetTimer();
                CompareDelimiter(ref results);
                mWatch.Stop();
                Console.WriteLine("    Separator Comparison Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());
                */


                Console.WriteLine("    Starting Row Count Comparison...");
                resetTimer();
                CompareRowCount(ref results);
                mWatch.Stop();
                Console.WriteLine("    Row Count Comparison Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());

                Console.WriteLine("    Starting Column Count Comparison...");
                resetTimer();
                CompareColumnCount(ref results);
                mWatch.Stop();
                Console.WriteLine("    Column Count Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());

                Console.WriteLine("    Compare Cell Data Comparison...");
                resetTimer();
                CompareCellData(ref results);
                mWatch.Stop();
                Console.WriteLine("    Column Count Complete. Time: " + new TimeSpan(0, 0, 0, 0, (int)mWatch.ElapsedMilliseconds).ToString());
            }
            catch
            {
                //do nothing
            }
            return results;
        }

        /// <summary>
        /// Compares the actual and expected cell value
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareCellData(ref List<DocDiffCompareDiffRecord> results)
        {
            int errorCount = 0;
            long totalCells = totalExpectedRows * totalExpectedColumns;
            long iCurrent = 0;
            long iStatus = 0;
            Stopwatch mCellWatch = new Stopwatch();
            mCellWatch.Start();
            
            Regex exRegex = new Regex(mExpectedDelimiter + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", RegexOptions.Compiled);
            Regex acRegex = new Regex(mActualDelimiter + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", RegexOptions.Compiled);

            for (int rowIndex = 0; rowIndex < GetMaxRowCount(); rowIndex++)
            {
                int columnCount = GetMaxColumnCount(rowIndex, out string[] actualColumns, out string[] expectedColumns, acRegex, exRegex);

                for (int iCol = 0; iCol < columnCount; iCol++)
                {
                    string expectedValue = (expectedColumns.Length >= iCol +1 ? expectedColumns[iCol] : null)?.Replace("\"", "");
                    string actualValue = (actualColumns.Length >= iCol +1 ? actualColumns[iCol] : null)?.Replace("\"", "");
                    string sExpectedValOrig = "";
                    string sActualValOrig = "";
                    bool expectedProcessed = false;
                    bool actualProcessed = false;

                    iStatus++;
                    if (iStatus == 1000)
                    {
                        mCellWatch.Stop();
                        Console.WriteLine("      1000 Cells Examined in: " + new TimeSpan(0, 0, 0, 0, (int)mCellWatch.ElapsedMilliseconds).ToString());
                        iCurrent = 1000 + iCurrent;
                        double iPrct = (double)iCurrent / (double)totalCells;
                        iPrct = Math.Round((iPrct * 100), 1);
                        Console.WriteLine("      Status [" + iPrct.ToString() + "%]: Cells Examined: " + iCurrent.ToString() + @" / " + totalCells);
                        iStatus = 0;
                        mCellWatch.Reset();
                        mCellWatch.Start();
                    }

                    if (!(mConfigFileRecord.IgnoreDateErrors && DateTime.TryParse(expectedValue, out _)))
                    {
                        if (mConfigFileRecord.IgnoreDateErrors)
                        {
                            ProcessDateTimeText(ref sExpectedValOrig, ref expectedValue, ref expectedProcessed);
                            ProcessDateTimeText(ref sActualValOrig, ref actualValue, ref actualProcessed);
                        }

                        if (expectedValue == actualValue)
                        {
                            if (!mErrorsOnly)
                            {
                                DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info,
                                    TEST_TYPE_CELL_DATA_COMPARISON,
                                    string.Format(CELL_MATCHES, mExpectedSheetName, rowIndex + 1, iCol + 1),
                                    expectedValue?? NULL.ToString(),
                                    actualValue ?? NULL.ToString());
                                results.Add(record);
                            }
                        }
                        else
                        {
                            if (expectedProcessed)
                            {
                                expectedValue = sExpectedValOrig;
                            }
                            if (actualProcessed)
                            {
                                actualValue = sActualValOrig;
                            }
                            DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Error,
                                TEST_TYPE_CELL_DATA_COMPARISON,
                                string.Format(CELL_DIFFERENCE, mExpectedSheetName, rowIndex + 1, iCol + 1),
                                expectedValue?? NULL.ToString(),
                                actualValue?? NULL.ToString());
                            results.Add(record);

                            errorCount++;
                        }
                    }

                    if (mConfigFileRecord.ErrorThreshold > 0 && errorCount >= mConfigFileRecord.ErrorThreshold)
                    {
                        Console.WriteLine("      Error Threshold Reached: " + errorCount.ToString());
                        mCellWatch.Stop();
                        Console.WriteLine("      Final [" + (totalCells - iCurrent).ToString() + "]Cells Examined in: " + new TimeSpan(0, 0, 0, 0, (int)mCellWatch.ElapsedMilliseconds).ToString());
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Get the maximum column count
        /// </summary>
        /// <param name="rowIndex">Current row index in comparison</param>
        /// <param name="actualColumns">Actual file column values within specific row</param>
        /// <param name="expectedColumns">Expected file column values within specific row</param>
        /// <param name="acRegex">Regex for actual data</param>
        /// <param name="exRegex">Regex for expected data</param>
        /// <returns>integer value</returns>
        private int GetMaxColumnCount(int rowIndex, out string[] actualColumns, out string[] expectedColumns, Regex acRegex, Regex exRegex)
        {
            actualColumns = mActualData.Count - 1 >= rowIndex ? acRegex.Split(mActualData[rowIndex]) : new string[] { };
            expectedColumns = mExpectedData.Count - 1 >= rowIndex ? exRegex.Split(mExpectedData[rowIndex]) : new string[] { };

            return expectedColumns.Length >= actualColumns.Length ? expectedColumns.Length : actualColumns.Length;
        }

        /// <summary>
        /// Get the maximum row count
        /// </summary>
        /// <returns>integer value</returns>
        private int GetMaxRowCount()
        {
            int actual = mActualData.Count;
            int expected = mExpectedData.Count;
            return expected >= actual ? expected : actual;
        }

        /// <summary>
        /// Reserve code for CSV separator comparison
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareDelimiter(ref List<DocDiffCompareDiffRecord> results)
        {
            mActualDelimiter = GetUsedDelimiter(mActualData.Take(2).ToList());
            mExpectedDelimiter = GetUsedDelimiter(mExpectedData.Take(2).ToList());

            if (mActualDelimiter != mExpectedDelimiter)
            {
                DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Error,
                        TEST_TYPE_DELIMITER_COMPARISON,
                        string.Format(DELIMITER_NOT_MATCH, mExpectedSheetName, $"'{mExpectedDelimiter}'", $"'{mActualDelimiter}'"),
                        $"'{mExpectedDelimiter}'",
                        $"'{mActualDelimiter}'");
                results.Add(record);
            }
            else if (!mErrorsOnly)
            {
                DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info,
                        TEST_TYPE_DELIMITER_COMPARISON,
                        string.Format(DELIMITER_MATCH, mExpectedSheetName, $"'{mExpectedDelimiter}'"),
                        $"'{mExpectedDelimiter}'",
                        $"'{mActualDelimiter}'");
                results.Add(record);
            }
        }

        /// <summary>
        /// Compares the column count of the actual and expected csv files
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareColumnCount(ref List<DocDiffCompareDiffRecord> results)
        {
            int actualColumnCount = mActualData.FirstOrDefault().Split(char.Parse(mActualDelimiter)).Length;
            totalExpectedColumns = mExpectedData.FirstOrDefault().Split(char.Parse(mExpectedDelimiter)).Length;

            if (actualColumnCount != totalExpectedColumns)
            {
                DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Error,
                    TEST_TYPE_COLUMN_COUNT_COMPARISON,
                    string.Format(COLUMN_COUNT_NOT_MATCH, mExpectedSheetName, totalExpectedColumns, actualColumnCount),
                    totalExpectedColumns.ToString(),
                    actualColumnCount.ToString());
                            results.Add(record);
            }
            else if(!mErrorsOnly)
            {
                DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info,
                    TEST_TYPE_COLUMN_COUNT_COMPARISON,
                    string.Format(COLUMN_COUNT_MATCH, mExpectedSheetName, totalExpectedColumns),
                    totalExpectedColumns.ToString(),
                    actualColumnCount.ToString());
                            results.Add(record);
            }
        }

        /// <summary>
        /// Compares the row count of the actual and expected csv files
        /// </summary>
        /// <param name="results">list of comparison results</param>
        private void CompareRowCount(ref List<DocDiffCompareDiffRecord> results)
        {
            totalExpectedRows = mExpectedData.Count;
            long actualRows = mActualData.Count;
            if (actualRows == totalExpectedRows)
            {
                if (!mErrorsOnly)
                {
                    DocDiffCompareDiffRecord record = new DocDiffCompareDiffRecord((int)MessageType.Info,
                        TEST_TYPE_ROW_COUNT_COMPARISON,
                        string.Format(ROW_COUNT_MATCH, mExpectedSheetName, totalExpectedRows),
                        totalExpectedRows.ToString(),
                        actualRows.ToString());
                    results.Add(record);
                }
            }
            else
            {
                DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord((int)MessageType.Error,
                        TEST_TYPE_ROW_COUNT_COMPARISON,
                        string.Format(ROW_COUNT_NOT_MATCH, mExpectedSheetName, totalExpectedRows, actualRows),
                        totalExpectedRows.ToString(),
                        actualRows.ToString());
                results.Add(cdr);
            }
        }

        /// <summary>
        /// Returns the delimiter used on the given list of lines
        /// </summary>
        /// <param name="lines">csv lines</param>
        /// <returns></returns>
        private string GetUsedDelimiter(List<string> lines)
        {
            //default delimiter ','
            string result = mDelimiters[0].ToString();
            if (lines.Count == 1)
            {
                foreach (char delimiter in mDelimiters)
                {
                    int delimiterCount = lines[0].ToCharArray().Count(c => c == delimiter);

                    if (delimiterCount > 0)
                    {
                        result = delimiter.ToString();
                        break;
                    }
                }
            }
            else if (lines.Count == 2)
            {
                foreach (char delimiter in mDelimiters)
                {
                    int delimiterLine1Count = lines[0].ToCharArray().Count(c => c == delimiter);
                    int delimiterLine2Count = lines[1].ToCharArray().Count(c => c == delimiter);

                    if (delimiterLine1Count == delimiterLine2Count && delimiterLine1Count > 0)
                    {
                        result = delimiter.ToString();
                        break;
                    }
                }
            }

            return result;
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
    }
}
