using HTMLBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TRDiagnosticsCore;
using TRDiagnosticsCore.Utility;

namespace TRDiagnosticsCore.Utility
{
    public class SaveLogsHandler
    {
        private const string MESSAGE_HEADER = "==================================================";
        private const string MESSAGE_TITLE = "Test Runner Diagnostics Tool";
        private const string MESSAGE_AUTHOR = "Author: MakatiAutomation";

        private static string htmlPath = null;

        /// <summary>
        /// Check directories for diagnotics log path and generates new log path
        /// </summary>
        public static string GetLogFilePath(string TRPath, bool isHtml = false)
        {
            if (TRPath.EndsWith(".log")) return TRPath.Replace(".log", ".html");
            //Get root path from test runner directory
            string mToolsPath = String.Equals(new DirectoryInfo(TRPath).Name, "Tools") ? TRPath : Directory.GetParent(TRPath).FullName;
            string mBrowserFrameworkPath = String.Equals(new DirectoryInfo(mToolsPath).Name, "BrowserFramework") ? mToolsPath : Directory.GetParent(mToolsPath).FullName;

            //Check system directory if existing
            string mDirSystemRoot = Path.Combine(mBrowserFrameworkPath, "System") + @"\";
            if (!Directory.Exists(mDirSystemRoot))
            {
                Directory.CreateDirectory(mDirSystemRoot);
            }

            //Create diagnostics directory
            string mDirDiagnosticsRoot = Path.Combine(mDirSystemRoot, "Diagnostics") + @"\";
            if (!Directory.Exists(mDirDiagnosticsRoot))
            {
                Directory.CreateDirectory(mDirDiagnosticsRoot);
            }

            //Generate new diagnostic log file path
            string ext = isHtml ? ".html" : ".log";
            string filePath = Path.Combine(mDirDiagnosticsRoot, "diag_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext);
            return filePath;
        }

        /// <summary>
        /// Check directories for error log path and generates new log path
        /// </summary>
        private static string GetErrorLogFilePath(string TRPath)
        {
            //Get root path from browser framework directory
            string mToolsPath = Directory.GetParent(TRPath).FullName;
            string mBrowserFrameworkPath = Directory.GetParent(mToolsPath).FullName;

            //Check system directory if existing
            string mDirSystemRoot = Path.Combine(mBrowserFrameworkPath, "System") + @"\";
            if (!Directory.Exists(mDirSystemRoot))
            {
                Directory.CreateDirectory(mDirSystemRoot);
            }

            //Create diagnostics directory
            string mDirDiagnosticsRoot = Path.Combine(mDirSystemRoot, "Diagnostics") + @"\";
            if (!Directory.Exists(mDirDiagnosticsRoot))
            {
                Directory.CreateDirectory(mDirDiagnosticsRoot);
            }

            //Create error logs directory
            string mDirErrorLogsRoot = Path.Combine(mDirDiagnosticsRoot, "ErrorLogs") + @"\";
            if (!Directory.Exists(mDirErrorLogsRoot))
            {
                Directory.CreateDirectory(mDirErrorLogsRoot);
            }

            //Generate new error log file path
            string filePath = Path.Combine(mDirErrorLogsRoot, "error_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log");
            return filePath;
        }

        /// <summary>
        /// Save logs after the diagnostics check
        /// </summary>
        /// <returns></returns>
        public static void SaveLogs(string testRunnerPath, List<LogRecord> logs, List<RecommendedItem> recommendations, string testName, bool appendLogs, out string saveLogsPath, out bool isLogsAppended)
        {
            //Generate new log file path
            saveLogsPath = GetLogFilePath(testRunnerPath);

            //Initialize log string builder to append values needed
            StringBuilder logValues = new StringBuilder();

            //Check if diagnostic logs from test instance has value
            if (logs.Count > 0)
            {
                //Append header value
                if (!appendLogs)
                {
                    logValues.AppendLine(MESSAGE_HEADER);
                    logValues.AppendLine(MESSAGE_TITLE);
                    logValues.AppendLine(MESSAGE_AUTHOR);
                    logValues.AppendLine(MESSAGE_HEADER);
                }

                //Append selected test runner directory
                logValues.AppendLine("Directory selected: <" + testRunnerPath + ">");
                logValues.AppendLine(MESSAGE_HEADER);

                //Append test name value
                logValues.AppendLine(testName);
                logValues.AppendLine(MESSAGE_HEADER);

                //Append diagnostic logs of test instance
                foreach (LogRecord log in logs)
                {
                    logValues.AppendLine($"[{log.MessageType.ToString()}] {log.MessageDetails.ToString()}");
                }
                logValues.AppendLine(MESSAGE_HEADER);

                StringBuilder warningLogs = new StringBuilder();
                StringBuilder errorLogs = new StringBuilder();
                int warningCount = 0;
                int errorCount = 0;

                //Append diagnostic recommendations of test instance
                foreach (RecommendedItem rec in recommendations)
                {
                    if (rec.Issue.MessageType == Logger.MessageType.WARNING)
                    {
                        warningLogs.AppendLine($"[{rec.Issue.MessageType.ToString()}] {rec.Issue.MessageDetails.ToString()}");
                        warningLogs.AppendLine($"[{rec.Recommendation.MessageType.ToString()}] {rec.Recommendation.MessageDetails.ToString()}");
                        warningLogs.AppendLine(MESSAGE_HEADER);
                        warningCount++;
                    }
                    if (rec.Issue.MessageType == Logger.MessageType.ERROR)
                    {
                        errorLogs.AppendLine($"[{rec.Issue.MessageType.ToString()}] {rec.Issue.MessageDetails.ToString()}");
                        errorLogs.AppendLine($"[{rec.Recommendation.MessageType.ToString()}] {rec.Recommendation.MessageDetails.ToString()}");
                        errorLogs.AppendLine(MESSAGE_HEADER);
                        errorCount++;
                    }
                }

                //Check if diagnostic recommendations from test instance has value
                if (recommendations != null)
                {
                    //Append recommendations from warning logs
                    if (warningCount > 0)
                    {
                        logValues.AppendLine($"Warnings found: {warningCount}");
                        logValues.AppendLine(warningLogs.ToString().Trim());
                    }
                    //Append recommendations from error logs
                    if (errorCount > 0)
                    {
                        logValues.AppendLine($"Errors found: {errorCount}");
                        logValues.AppendLine(errorLogs.ToString().Trim());
                    }
                }
            }
            else
            {
                //No diagnostic logs found in test instance to append
                if (appendLogs)
                    logValues.AppendLine(testName);

                logValues.AppendLine(MESSAGE_HEADER);
                logValues.AppendLine("No diagnostic logs to display.");
                logValues.AppendLine(MESSAGE_HEADER);
            }

            //Check if test instance is independent run or used in full diagnostic test run
            if (!appendLogs)
            {
                //Create new diagnostic log file
                using (StreamWriter writer = File.CreateText(saveLogsPath))
                {
                    writer.WriteLine(logValues.ToString().Trim());
                    writer.Close();
                }
                isLogsAppended = false;
            }
            else
            {
                //Get latest diagnostic log file created and append current test diagnostic logs
                string getDiagnosticsPath = Directory.GetParent(saveLogsPath).FullName;
                var getPrevLogsFile = new DirectoryInfo(getDiagnosticsPath).GetFiles().OrderByDescending(f => f.LastWriteTime).First().FullName;
                using (StreamWriter writer = File.AppendText(getPrevLogsFile))
                {
                    writer.WriteLine(logValues.ToString().Trim());
                    writer.Close();
                }
                isLogsAppended = true;
                saveLogsPath = getPrevLogsFile;
            }
        }

        /// <summary>
        /// Save error logs under diagnostic error logs directory
        /// </summary>
        public static bool SaveErrorLogs(string testRunnerPath, string error, string message, out string errorLogsPath)
        {
            bool isSuccess = false;
            try
            {
                //Generate new error log file path
                errorLogsPath = GetErrorLogFilePath(testRunnerPath);

                //Creates new error log file
                using (StreamWriter writer = File.CreateText(errorLogsPath))
                {
                    writer.WriteLine(MESSAGE_HEADER);
                    writer.WriteLine("Diagnostic test error: " + error);
                    writer.WriteLine(MESSAGE_HEADER);
                    writer.Close();
                }
                return isSuccess = true;
            }
            catch
            {
                errorLogsPath = null;
                return isSuccess;
            }
        }

        public static bool OpenHTMLFile()
        {
            if (htmlPath == null) return false;

            Process.Start(htmlPath);
            return true;
        }

        public static string GenerateHTMLReport(string FileOutput, List<TestRecord> testRecords)
        {
            List<XElement> modals = new List<XElement>();
            List<XElement> errorRows = new List<XElement>();
            List<XElement> warningRows = new List<XElement>();
            List<XElement> successRows = new List<XElement>();

            int errors = 0;
            int warnings = 0;
            int success = 0;


            foreach(var testRecord in testRecords)
            {
                foreach (var reco in testRecord.Reco)
                {
                    var id = "tr" + Guid.NewGuid().ToString();

                    if (reco.Issue.MessageType == Logger.MessageType.ERROR)
                        errorRows.Add(Row(
                            id,
                            ++errors,
                            testRecord.TestName,
                            reco.Issue.MessageType,
                            reco.Issue.MessageDetails,
                            reco.Recommendation.MessageDetails
                        ));

                    if (reco.Issue.MessageType == Logger.MessageType.WARNING)
                        warningRows.Add(Row(
                            id,
                            ++warnings,
                            testRecord.TestName,
                            reco.Issue.MessageType,
                            reco.Issue.MessageDetails,
                            reco.Recommendation.MessageDetails
                        ));
                }
                foreach(var log in testRecord.Logs)
                {
                    if (log.MessageType == Logger.MessageType.SUCCESS)
                        successRows.Add(Row(
                            null,
                            ++success,
                            testRecord.TestName,
                            log.MessageType,
                            log.MessageDetails
                        ));
                }
            }


            
            var cardReport =
                HTML.Container(
                     HTML.CustomCard("Test Runner Diagnostic Report",
                        HTML.H4(HTML.Class(UC.MB_0), "Summary"),
                        HTML.P(HTML.Class(UC.TEXT_SECONDARY), "Click each summary to update the report details table"),
                        HTML.TabLilnkContainer(
                            HTML.TabLink("error", true, HTML.Class(UC.MB_5),
                                HTML.InfoCard($"Error{plural(errorRows.Count)} found", errorRows.Count.ToString(), UC.BG_DANGER)
                            ),
                            HTML.TabLink("warning", false, HTML.Class(UC.MB_5, UC.MX_MD_4),
                                HTML.InfoCard($"Warning{plural(warningRows.Count)} found", warningRows.Count.ToString(), UC.BG_WARNING)
                            ),
                            HTML.TabLink("success", false, HTML.Class(UC.MB_5),
                                HTML.InfoCard("Success recorded", successRows.Count.ToString(), UC.BG_SUCCESS)
                            )
                        ),
                        HTML.TabContent(
                            HTML.Class(UC.MB_4),
                            HTML.Tab("error", true,
                                HTML.Class("table-responsive"),
                                HTML.H4(HTML.Class(UC.TEXT_DANGER), "Errors"),
                                HTML.Table(HTML.Class("table-hover align-middle table-bordered"),
                                    HTML.THead(
                                        HTML.Class("table-danger"),
                                        HTML.TR(
                                            HTML.TH(HTML.Class(UC.TEXT_CENTER), "#"),
                                            HTML.TH("Test Type"),
                                            HTML.TH("Log Message"),
                                            HTML.TH("Recommendation")
                                        )
                                    ),
                                    errorRows.Count > 0 
                                    ? HTML.TBody(errorRows) 
                                    : HTML.TBody(
                                            HTML.TR(
                                                HTML.TD(
                                                    HTML.Class(UC.TEXT_CENTER),
                                                    HTML.Attr("colspan", "4"),
                                                    HTML.H3("No records to display")
                                                )
                                            )
                                      )
                                )
                            ),
                            HTML.Tab("warning", false,
                                HTML.Class("table-responsive"),
                                HTML.H4(HTML.Class(UC.TEXT_WARNING), "Warnings"),
                                HTML.Table(HTML.Class("table-hover align-middle table-bordered"),
                                    HTML.THead(
                                        HTML.Class("table-warning"),
                                        HTML.TR(
                                            HTML.TH(HTML.Class(UC.TEXT_CENTER), "#"),
                                            HTML.TH("Test Type"),
                                            HTML.TH("Log Message"),
                                            HTML.TH("Recommendation")
                                        )
                                    ),
                                    warningRows.Count > 0
                                    ? HTML.TBody(warningRows)
                                    : HTML.TBody(
                                            HTML.TR(
                                                HTML.TD(
                                                    HTML.Class(UC.TEXT_CENTER),
                                                    HTML.Attr("colspan", "4"),
                                                    HTML.H3("No records to display")
                                                )
                                            )
                                      )
                                )
                            ),
                            HTML.Tab("success", false,
                                HTML.Class("table-responsive"),
                                HTML.H4(HTML.Class(UC.TEXT_SUCCESS), "Success"),
                                HTML.Table(HTML.Class("table-hover align-middle table-bordered"),
                                    HTML.THead(
                                        HTML.Class("table-success"),
                                        HTML.TR(
                                            HTML.TH(HTML.Class(UC.TEXT_CENTER), "#"),
                                            HTML.TH("Test Type"),
                                            HTML.TH("Log Message")
                                        )
                                    ),
                                    successRows.Count > 0
                                    ? HTML.TBody(successRows)
                                    : HTML.TBody(
                                            HTML.TR(
                                                HTML.TD(
                                                    HTML.Class(UC.TEXT_CENTER),
                                                    HTML.Attr("colspan", "4"),
                                                    HTML.H3("No records to display")
                                                )
                                            )
                                    )
                                )
                            )
                        ),
                        HTML.Span(HTML.Class(UC.TEXT_SECONDARY, UC.D_BLOCK), "Full Logs Location"),
                        HTML.Div(HTML.Class(UC.D_FLEX),
                            HTML.Div(HTML.Class(UC.M_0),
                                HTML.Btn(
                                    HTML.Class("copy-btn", UC.BTN_PRIMARY, UC.MR_2),
                                    HTML.Attr("target", "logpath"),
                                    HTML.Style("margin-bottom: 8px"),
                                    HTML.TooltipAttr("Copy to clipboard", false),
                                    HTML.Tag("i", HTML.Class("far fa-copy"), HTML.Tag("span"))
                                )
                            ),
                            HTML.Div(HTML.Class(UC.M_0), HTML.Style("overflow-x: scroll"),
                                HTML.H4(
                                    HTML.TooltipAttr("Copy to clipboard"),
                                    HTML.ID("logpath"),
                                    HTML.Class("copy", UC.D_INLINE_BLOCK), 
                                    FileOutput.Replace("html", "log")
                                )
                            )
                        ),                        
                        HTML.P(
                            HTML.Class(UC.TEXT_CENTER, UC.MB_0, UC.MT_5),
                            HTML.Style("color: #8F8F8F"),
                            "* Full details of the report are not available for Internet Explorer. It is recommended to view this report in the latest version of Google Chrome, Mozilla Firefox or Microsoft Edge (Chromium) *"
                        )
                    )
                );

            htmlPath = FileOutput;
            var html = new HtmlBuilder();
            html.Body(cardReport, modals);
            html.Save(FileOutput);

            XElement Row(string id, int rowCount, string testName, Logger.MessageType logType, string logMessage, string reco = null)
            {
                if(reco != null)
                    modals.Add(HTML.Modal(
                        testName,
                        rowCount.ToString(),
                        LogStyle(logType),
                        id,
                        HTML.H5("Issue"),
                        HTML.P(FormatIfContainsList(logMessage)),
                        HTML.H5("Recommendation"),
                        HTML.P(reco)
                    ));

                return HTML.TR(
                       HTML.Style("cursor: pointer;"),
                       reco == null ? null : HTML.ModalAttr(id),
                       HTML.TH(HTML.Class(UC.TEXT_CENTER), rowCount),
                       HTML.TD(testName),
                       HTML.TD(HTML.Style("max-width: 25rem !important;"), FormatIfContainsList(logMessage)),
                       reco == null ? null : HTML.TD(reco));

            }

            string plural(int count) => count == 1 ? "" : "s";


            string LogStyle(Logger.MessageType logType)
            {
                switch (logType)
                {
                    case Logger.MessageType.ERROR: return UC.TEXT_DANGER;
                    case Logger.MessageType.WARNING: return UC.TEXT_WARNING;
                    case Logger.MessageType.INFO: return UC.TEXT_INFO;
                    case Logger.MessageType.SUCCESS: return UC.TEXT_SUCCESS;
                    default: return UC.TEXT_DARK;
                }
            }

            object FormatIfContainsList(string message)
            {
                if(message.Contains("\n") || message.Contains("\t"))
                {
                    List<object> newWithList = new List<object>();
                        
                    foreach(var m  in message.Split('\n'))
                    {
                        if (m.Contains("\t") || m.Contains("\u2022"))
                            newWithList.Add(
                                HTML.Ul(
                                    HTML.Li(m.Replace("\t", "").Replace("\u2022", "").Trim())
                                )
                            );
                        else newWithList.Add(m);
                    }
                    return newWithList;
                }

                return message;
            }

            return FileOutput;
        }
    }
}
