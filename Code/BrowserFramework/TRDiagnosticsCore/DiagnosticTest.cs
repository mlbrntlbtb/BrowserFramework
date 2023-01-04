using System;
using System.Collections.Generic;
using TRDiagnosticsCore.Utility;

namespace TRDiagnosticsCore
{
    public abstract class DiagnosticTest
    {
        #region PROPERTIES
        private int Steps_Warning = 0;
        private int Steps_Error = 0;
        private bool bConsole = false;
        private List<LogRecord> _warningLogs;
        private List<LogRecord> _errorLogs;
        private List<RecommendedItem> _recommendations;
        private bool isSummarized = false;

        public Logger DiagnosticLogger
        {
            get
            {
                return _diagnosticLogger;
            }
        }
        private Logger _diagnosticLogger;

        public String Error
        {
            get { return _Error; }
        }
        private string _Error = "";

        public bool TestCancelled { get; set; }
        
        public String TestName { get; set; }

        public List<RecommendedItem> Recommendations
        {
            get { return _recommendations; }
        }
        protected String TestRunnerPath { get; set; }

        public int TotalTestCount { get; set; }
        #endregion

        public void Run(string TestRunnerLocation, bool IsConsole = false, string DirRunArg = "")
        {

            SetDirTestArg(DirRunArg);
            TestRunnerPath = TestRunnerLocation;
            Setup(IsConsole);
            try
            {
                PerformCheck(out _Error);
                Summarize();
            }
            catch (Exception e)
            {
                if (TestCancelled)
                {
                    if (!isSummarized)
                    {
                        _recommendations = new List<RecommendedItem>();
                        _warningLogs = new List<LogRecord>();
                        _errorLogs = new List<LogRecord>();
                        Summarize();
                    }

                    _diagnosticLogger.LogResult(Logger.MessageType.INFO, "Unable to complete diagnostic check. Reason: User canceled diagnostic test.");
                }
                else
                {
                    _Error = e.Message;
                    _diagnosticLogger.LogResult(Logger.MessageType.ERROR, $"Unable to complete diagnostic check. Reason: {_Error}");
                }
                if (IsConsole) { ConsolePrinter.PrintHeader();}
            }
            finally
            {
                if(!IsConsole)
                {
                    isSummarized = true;
                    UILogger.ResetTestCount();
                }               
            }
        }

        /// <summary>
        /// Perform the diagnostics
        /// </summary>
        protected abstract void PerformCheck(out string ErrorMessage);

        /// <summary>
        /// Identifies recommendation based on the warnings and errors found
        /// </summary>
        protected abstract LogRecord IdentifyRecommendation(LogRecord logRecord);

        /// <summary>
        /// Define the header of the diagnostic test
        /// </summary>
        protected abstract void DefineTestName();

        #region PRIVATE METHODS

        /// <summary>
        /// Set the argument value to run optional directory test
        /// </summary>
        /// <param name="DirRunTestArg"></param>
        private void SetDirTestArg(string DirRunTestArg)
        {
            if (!string.IsNullOrEmpty(DirRunTestArg))
            {
                DirectoryArgument.RunTestScript = !DirRunTestArg.Contains("1");
                DirectoryArgument.RunTestScriptResults = !DirRunTestArg.Contains("2");
                DirectoryArgument.RunTestSuite = !DirRunTestArg.Contains("3");
                DirectoryArgument.RunTestSuiteResult = !DirRunTestArg.Contains("4");
            }
        }

        /// <summary>
        /// Perform setup operations
        /// </summary>
        private void Setup(bool IsConsole)
        {
            isSummarized = false;
            bConsole = IsConsole;
            _diagnosticLogger = new Logger(IsConsole);
            _warningLogs = new List<LogRecord>();
            _errorLogs = new List<LogRecord>();
            _recommendations = new List<RecommendedItem>();
            DefineTestName();

            if (bConsole)
            {
                ConsolePrinter.PrintTestHeader(TestName);
            }
            else
            {
                UILogger.LogHeader(TestName);
            }
        }

        /// <summary>
        /// Perform summary of logs
        /// </summary>
        private void Summarize()
        {
            foreach (LogRecord log in _diagnosticLogger.Logs)
            {
                if (log.MessageType == Logger.MessageType.WARNING)
                {
                    _warningLogs.Add(log);
                    LogRecord rec = IdentifyRecommendation(log);
                    _recommendations.Add(new RecommendedItem(log, rec));
                    Steps_Warning++;
                }
                if (log.MessageType == Logger.MessageType.ERROR)
                {
                    _errorLogs.Add(log);
                    LogRecord rec = IdentifyRecommendation(log);
                    _recommendations.Add(new RecommendedItem(log, rec));
                    Steps_Error++;
                }
            }
            if (bConsole)
            {
                if (Steps_Warning > 0) { ConsolePrinter.PrintSummary(Steps_Warning, _warningLogs, _recommendations, "Warning"); }
                if (Steps_Error > 0) { ConsolePrinter.PrintSummary(Steps_Error, _errorLogs, _recommendations, "Error"); }
                if (Steps_Error == 0 & Steps_Warning == 0) { ConsolePrinter.PrintHeader(); }
            }
            else
            {
                //TODO: Collate results for UI 
            }
        }
        #endregion
    }

    public static class DirectoryArgument
    {
        public static bool RunTestScript { get; set; }
        public static bool RunTestScriptResults { get; set; }
        public static bool RunTestSuite { get; set; }
        public static bool RunTestSuiteResult { get; set; }
    }

    public class RecommendedItem
    {
        public LogRecord Issue;
        public LogRecord Recommendation;

        public RecommendedItem(LogRecord DiagIssue, LogRecord DiagRecommendation)
        {
            Issue = DiagIssue;
            Recommendation = DiagRecommendation;
        }
    }

    public class TestRecord
    {
        public TestRecord() { }
        public TestRecord(string testName,List<LogRecord> logs, List<RecommendedItem> recommendations) 
        {
            TestName = testName;
            Logs = logs;
            Reco = recommendations;
        }

        public string TestName { get; set; }
        public List<LogRecord> Logs { get; set; }
        public List<RecommendedItem> Reco { get; set; }
    }
}
