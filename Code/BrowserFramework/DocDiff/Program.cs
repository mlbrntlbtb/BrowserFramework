using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using CommonLib.DlkUtility;


namespace DocDiff
{
    public class DocDiffCompareInfoRecord
    {
        public String expectedfile { get; set; }
        public String actualfile { get; set; }
        public String configfile { get; set; }
        public String comparetype { get; set; }

        public DocDiffCompareInfoRecord(String ExpectedFile, String ActualFile, String ConfigFile, String CompareType)
        {
            expectedfile = ExpectedFile;
            actualfile = ActualFile;
            configfile = ConfigFile;
            comparetype = CompareType;
        }
        public DocDiffCompareInfoRecord()
        {
            expectedfile = "";
            actualfile = "";
            configfile = "";
            comparetype = "";
        }
    }
    public class DocDiffCompareDiffRecord
    {
        public int messagetype { get; set; }             // 0=info, 1=error
        public String testtype { get; set; }             // SheetNameValidation, SheetContentValidation
        public String testdetails { get; set; }          // a descriptive summary message    
        public String expectedfiledata { get; set; }       // data from the expected file
        public String actualfiledata { get; set; }      // data from the actual file

        public DocDiffCompareDiffRecord(int MessageType, String TestType, String TestDetails, String ExpectedFileData, String ActualFileData)
        {
            messagetype = MessageType;
            testtype = TestType;
            testdetails = TestDetails;
            expectedfiledata = ExpectedFileData;
            actualfiledata = ActualFileData;
        }
        public DocDiffCompareDiffRecord()
        {
            messagetype = -1;
            testtype = "";
            testdetails = "";
            expectedfiledata = "";
            actualfiledata = "";
        }
    }
    
    public static class Program
    {
        static String ExpectedFile = "";
        static String ActualFile = "";
        static String OutputFile = "";
        static String ConfigFile = "";
        static String CompareType = ""; // valid types excel, text, binary
        static String DocDiffExecutionLog = ""; // an execution detail log written to the same dir as the output file
        static Boolean ChangedExpectedFilePerms = false;
        static Boolean ChangedConfigFilePerms = false;

        const string TEST_TYPE_FILETYPECOMPARISON = "FileTypeComparison";
        const string ERR_FILETYPE_MISMATCH = "Expected and Actual file need to have the same file extension to be compared.";

        #region DOCUMENT TO DOCUMENT COMPARISON
        public static void Main(string[] args)
        {
            try
            {
                ExecuteDocDiff(DlkArgs.GetArg("-expected"), DlkArgs.GetArg("-actual"),
                        DlkArgs.GetArg("-output"), DlkArgs.GetArg("-config"));
            }
            catch (Exception e)
            {
                LogCriticalError(e);
            }
            finally
            {
                // reset the ExpectedFile perms if needed
                if (ChangedExpectedFilePerms)
                {
                    FileInfo mfInfo = new FileInfo(ExpectedFile);
                    mfInfo.IsReadOnly = true;
                }

                if(ChangedConfigFilePerms)
                {
                    FileInfo fileConfig = new FileInfo(ConfigFile);
                    fileConfig.IsReadOnly = true;
                }
            }
        }
        public static void Main(String expectedfile, String actualfile, String outputfile, String configfile)
        {
            try
            {
                ExecuteDocDiff(expectedfile, actualfile, outputfile, configfile);
            }
            catch (Exception e)
            {
                LogCriticalError(e);
            }
            finally
            {
                // reset the ExpectedFile perms if needed
                if (ChangedExpectedFilePerms)
                {
                    FileInfo mfInfo = new FileInfo(ExpectedFile);
                    mfInfo.IsReadOnly = true;
                }

                if (ChangedConfigFilePerms)
                {
                    FileInfo fileConfig = new FileInfo(ConfigFile);
                    fileConfig.IsReadOnly = true;
                }
            }
        }

        /// <summary>
        /// This executes the DocDiff logic
        /// </summary>
        /// <param name="expectedfile"></param>
        /// <param name="actualfile"></param>
        /// <param name="outputfile"></param>
        /// <param name="configfile"></param>
        private static void ExecuteDocDiff(String expectedfile, String actualfile, String outputfile, String configfile)
        {
            try 
            { 
            Initialize(expectedfile, actualfile, outputfile, configfile);
            }
            catch (Exception e)
            {
                LogCriticalError(e);
            }

            Stopwatch mWatch = new Stopwatch();
            mWatch.Reset();
            mWatch.Start();

            DocDiffCompareInfoRecord CompareInfo = new DocDiffCompareInfoRecord(ExpectedFile, ActualFile, ConfigFile, CompareType);
            List<DocDiffCompareDiffRecord> ComparisonResults = new List<DocDiffCompareDiffRecord>();
            switch (CompareType)
            {
                case "excel":
                    XlsCompare xCompare = new XlsCompare(ExpectedFile, ActualFile, ConfigFile);
                    try
                    {
                        ComparisonResults = xCompare.CompareXls();
                        xCompare.Dispose();
                    }
                    catch
                    {
                        xCompare.Dispose();
                        throw;
                    }
                    break;
                case "csv":
                    CSVCompare csvCompare = new CSVCompare(ExpectedFile, ActualFile, ConfigFile);
                    ComparisonResults = csvCompare.CompareCSV();
                    break;
                case "binary":
                    ByteCompare bCompare = new ByteCompare(ExpectedFile, ActualFile);
                    ComparisonResults = bCompare.CompareFiles();
                    break;
                case "text":
                    TextCompare tCompare = new TextCompare(ExpectedFile, ActualFile);
                    ComparisonResults = tCompare.CompareFiles();
                    break;
                case "pdf":
                    PdfCompare pCompare = new PdfCompare(ExpectedFile, ActualFile);
                    ComparisonResults = pCompare.CompareFiles();
                    break;
                case "word":
                    WordCompare wCompare = new WordCompare(ExpectedFile, ActualFile, OutputFile);
                    ComparisonResults = wCompare.CompareFiles();
                    break;
                case "filetype":
                    string mExtExpected = new FileInfo(ExpectedFile).Extension.ToLower();
                    string mExtActual = new FileInfo(ActualFile).Extension.ToLower();
                    ComparisonResults.Add(new DocDiffCompareDiffRecord(1,
                        TEST_TYPE_FILETYPECOMPARISON,
                        ERR_FILETYPE_MISMATCH,
                        mExtExpected,
                        mExtActual
                        ));
                    break;
                default:
                    break;
            }
            mWatch.Stop();
            String mTotalCompareTime = "Total Comparison Time: " + GetExecutionTimeFormat(mWatch.Elapsed);
            System.Console.WriteLine(mTotalCompareTime);

            System.Console.WriteLine("Creating output file: " + OutputFile);
            Stopwatch mRWatch = new Stopwatch();
            mRWatch.Start();
            Reporter report = new Reporter(CompareInfo, ComparisonResults, mWatch.Elapsed);
            report.CreateReportHTML(OutputFile);
            mRWatch.Stop();
            System.Console.WriteLine("Output Creation Time: " + GetExecutionTimeFormat(mRWatch.Elapsed));

            // log run details
            String[] mLogDetails = new String[]
                {
                mTotalCompareTime,
                "Time Completed At: " + DateTime.Now.ToString(),
                "##############################################################################################################"
                };
            File.AppendAllLines(DocDiffExecutionLog, mLogDetails);
        }

        /// <summary>
        /// Verifies the args are correct and sets globals... if not show usage
        /// Also used outside of the command line program
        /// </summary>
        private static void Initialize(String expectedfile, String actualfile, String outputfile, String configfile)
        {
            ExpectedFile = expectedfile;
            ActualFile = actualfile;
            OutputFile = outputfile;
            ConfigFile = configfile;

            // Create an empty log to append to
            String sDocDiffLogDir = new FileInfo(OutputFile).DirectoryName + @"\";
            String sFile = "DocDiffExecutionLog_" + DlkString.GetDateAsText("file") + ".txt";
            DocDiffExecutionLog = sDocDiffLogDir + sFile;
            File.WriteAllLines(DocDiffExecutionLog, new String[] 
            {
                "Expected: " + ExpectedFile,
                "Actual: " + ActualFile,
                "Output: " + OutputFile,
                "Config: " + ConfigFile,
                "---------------------------------------------"
            });

            if ((ExpectedFile == "") || (ActualFile == "") || (OutputFile == ""))
            {
                throw new Exception(@"Required input file path is empty. Please check expected, actual and output file paths");
            }

            if (!File.Exists(ExpectedFile))
            {
                throw new Exception(@"Supplied -expected <file path> does not exist. File: " + ExpectedFile);
            }

            if (!File.Exists(ActualFile))
            {
                throw new Exception("Supplied -actual <file path> does not exist. File: " + ActualFile);
            }

            String mExtExpected = new FileInfo(ExpectedFile).Extension.ToLower();
            String mExtActual = new FileInfo(ActualFile).Extension.ToLower();
            if (mExtExpected != mExtActual)
            {
                SetCompareType("filetype");
                throw new Exception(ERR_FILETYPE_MISMATCH);
            }
            else
            {
                SetCompareType(mExtExpected);
            }

            if (!File.Exists(ConfigFile))
            {
                if (CompareType == "excel")
                {
                    throw new Exception(@"Supplied -config <file path> does not exist. File: " + ConfigFile);
                }
            }

            //
            FileInfo fileConfig = new FileInfo(ConfigFile);
            if(fileConfig.IsReadOnly)
            {
                fileConfig.IsReadOnly = false;
                ChangedConfigFilePerms = true;
            }

            FileInfo fiExpected = new FileInfo(ExpectedFile);
            if (fiExpected.IsReadOnly)
            {
                fiExpected.IsReadOnly = false;
                ChangedExpectedFilePerms = true;
            }
        }

        /// <summary>
        /// Log critical error into execution logfile
        /// </summary>
        /// <param name="ex">The Exception thrown</param>
        private static void LogCriticalError(Exception ex)
        {
            String[] mAppend = new String[]
                {
                    "---------------------------------------------",
                    "Critical Error >> Please Review >>",
                    "---------------------------------------------",
                    ex.Message,
                    "---------------------------------------------",
                    ex.StackTrace,
                    "---------------------------------------------"
                };
            if (DocDiffExecutionLog != "")
            {
                File.AppendAllLines(DocDiffExecutionLog, mAppend);
                System.Console.Out.WriteLine("A critical error has occured. Please review log: " + DocDiffExecutionLog);
            }
            else
            {
                System.Console.Out.WriteLine("Critical Error >>");
                System.Console.Out.WriteLine("---------------------------------------------");
                System.Console.Out.WriteLine(ex.Message);
                System.Console.Out.WriteLine("---------------------------------------------");
                System.Console.Out.WriteLine(ex.StackTrace);
            }
        }
        #endregion

        #region STRING TO DOCUMENT COMPARISON
        /// <summary>
        /// This executes the DocDiff logic
        /// </summary>
        /// <param name="expectedfile"></param>
        /// <param name="actualfile"></param>
        /// <param name="outputfile"></param>
        /// <param name="configfile"></param>
        public static void ExecuteDocDiff(String expectedfile, List<String> actualStringList, String outputfile)
        {
            Initialize(expectedfile, actualStringList, outputfile);

            Stopwatch mWatch = new Stopwatch();
            mWatch.Reset();
            mWatch.Start();

            DocDiffCompareInfoRecord CompareInfo = new DocDiffCompareInfoRecord(ExpectedFile, ActualFile, ConfigFile, CompareType);
            List<DocDiffCompareDiffRecord> ComparisonResults = new List<DocDiffCompareDiffRecord>();
            switch (CompareType)
            {
                //case "pdf":
                //    PdfCompare pCompare = new PdfCompare(ExpectedFile, ActualFile);
                //    ComparisonResults = pCompare.CompareFiles();
                //    break;
                case "word":
                    WordCompare wCompare = new WordCompare(ExpectedFile, OutputFile);
                    ComparisonResults = wCompare.CompareFileToString(actualStringList);
                    break;
                default:
                    throw new Exception($"[{ CompareType }] type of document is currently not supported for this feature.");
            }
            mWatch.Stop();
            String mTotalCompareTime = "Total Comparison Time: " + GetExecutionTimeFormat(mWatch.Elapsed);
            Console.WriteLine(mTotalCompareTime);

            Console.WriteLine("Creating output file: " + OutputFile);
            Stopwatch mRWatch = new Stopwatch();
            mRWatch.Start();
            Reporter report = new Reporter(CompareInfo, ComparisonResults, mWatch.Elapsed);
            report.CreateReportHTML(OutputFile);
            mRWatch.Stop();
            Console.WriteLine("Output Creation Time: " + GetExecutionTimeFormat(mRWatch.Elapsed));

            // log run details
            String[] mLogDetails = new String[]
                {
                    mTotalCompareTime,
                    "Time Completed At: " + DateTime.Now.ToString(),
                    "##############################################################################################################"
                };
            File.AppendAllLines(DocDiffExecutionLog, mLogDetails);
        }

        /// <summary>
        /// Verifies the args are correct and sets globals... if not show usage
        /// Also used outside of the command line program
        /// Used for comparing string to document content
        /// </summary>
        private static void Initialize(String expectedfile, List<String> actualstring, String outputfile)
        {
            ExpectedFile = expectedfile;
            OutputFile = outputfile;

            // Create an empty log to append to
            String sDocDiffLogDir = new FileInfo(OutputFile).DirectoryName + @"\";
            String sFile = "DocDiffExecutionLog_" + DlkString.GetDateAsText("file") + ".txt";
            DocDiffExecutionLog = sDocDiffLogDir + sFile;
            File.WriteAllLines(DocDiffExecutionLog, new String[]
            {
                "Expected: " + ExpectedFile,
                "Output: " + OutputFile,
                "---------------------------------------------"
            });

            if ((ExpectedFile == "") || (OutputFile == ""))
            {
                throw new Exception(@"Required input file path is empty. Please check expected, ppetand output file paths");
            }

            if (!File.Exists(ExpectedFile))
            {
                throw new Exception(@"Supplied -expected <file path> does not exist. File: " + ExpectedFile);
            }

            String mExtExpected = new FileInfo(ExpectedFile).Extension.ToLower();
            SetCompareType(mExtExpected);

            if (!File.Exists(ConfigFile))
            {
                if (CompareType == "excel")
                {
                    throw new Exception(@"Supplied -config <file path> does not exist. File: " + ConfigFile);
                }
            }

            FileInfo fiExpected = new FileInfo(ExpectedFile);
            if (fiExpected.IsReadOnly)
            {
                fiExpected.IsReadOnly = false;
                ChangedExpectedFilePerms = true;
            }
        }
        #endregion 

        private static void SetCompareType(string ExtExpected)
        {
            switch (ExtExpected)
            {
                case ".xlsx":
                case ".xls":
                    CompareType = "excel";
                    break;
                case ".txt":
                case ".log":
                case ".xml":
                case ".ets":
                case ".exp":
                case ".adv":
                    CompareType = "text";
                    break;
                case ".doc":
                case ".docx":
                    CompareType = "word";
                    break;
                case ".pdf":
                    CompareType = "pdf";
                    break;
                case ".htm":
                case ".html":
                    CompareType = "binary";
                    break;
                case ".csv":
                    CompareType = "csv";
                    break;
                case "filetype":
                    CompareType = "filetype";
                    break;
                default:
                    throw new Exception(@"Cannot compare file with extension: " + ExtExpected);
            }
        }

        /// <summary>
        /// Format execution time to 00:00:00.00, the same with the output file
        /// </summary>
        /// <param name="executionTime">elapsed time</param>
        /// <returns>00:00:00.00 time format</returns>
        private static string GetExecutionTimeFormat(TimeSpan executionTime)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    executionTime.Hours, executionTime.Minutes, executionTime.Seconds, executionTime.Milliseconds / 10);
        }
    }
}
