using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System.Xml.Linq;
using System.IO;
using OpenQA.Selenium;
using Microsoft.VisualBasic.Devices;

namespace CommonLib.DlkSystem
{
    /// <summary>
    /// This class allows us to store test step execution details into memory
    /// </summary>
    public static class DlkLogger
    {
        #region PRIVATE MEMBERS
        private static List<DlkLoggerRecord> _LogRecs;
        private static List<DlkLoggerSessionObject> _sessionLogs = new List<DlkLoggerSessionObject>();
        private static bool m_IsInitialized = false;
        private static string m_SessionLogFile = string.Empty;
        #endregion

        #region PUBLIC MEMBERS
        public const int INT_HEADER_LOG_MAX_LENGTH = 140;
        public const string STR_TYPE_OUTPUT_ONLY_LOG = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string STR_HEADER_CHAR = "-";
        public const string STR_CANCELLATION_MESSAGE = "Test execution was interrupted";
        public static string m_LogFolderPath = System.IO.Path.Combine(Directory.GetParent(DlkEnvironment.mDirTools).Parent.FullName, "System", "errorlogs");
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// clears out any records in the logger
        /// </summary>
        public static void ClearLogger()
        {
            _LogRecs = new List<DlkLoggerRecord>();
        }

        /// <summary>
        /// Start a new session logger object to track logs specific to this session
        /// </summary>
        /// <param name="SessionId">ID of new session. Format is in 32-bit GUID</param>
        public static void StartNewSessionLogs(string SessionId)
        {
            /* Assemble sessionobjectid ==> concat of sessionid : threadid */
            DlkLoggerSessionObject logSession = new DlkLoggerSessionObject(SessionId, Thread.CurrentThread.ManagedThreadId.ToString());
            logSession.AddRecord(new DlkLoggerRecord(DlkLogger.STR_TYPE_OUTPUT_ONLY_LOG, "Initializing new session...\n"));

            /* add to session objects list */
            _sessionLogs.Add(logSession);
        }

        /// <summary>
        /// Remove session logger object
        /// </summary>
        /// <param name="SessionId">ID of target session to remove. Format is in 32-bit GUID</param>
        public static void RemoveSessionLogs(string SessionId)
        {
            if (_sessionLogs.Any(x => x.SessionId == SessionId))
            {
                _sessionLogs.Remove(_sessionLogs.FirstOrDefault(x => x.SessionId == SessionId));
            }
        }

        /// <summary>
        /// Format input string to log header format
        /// </summary>
        /// <param name="Caption">Input string</param>
        /// <returns>Formatted string</returns>
        public static string ConvertToHeader(string Caption)
        {
            /* replace spaces */
            Caption = Caption.Replace(" ", STR_HEADER_CHAR);
            int halfCount = (INT_HEADER_LOG_MAX_LENGTH - Caption.Length) / 2;

            /* Add leading/trailing header char */
            while (halfCount-- > 0)
            {
                Caption = STR_HEADER_CHAR + Caption + STR_HEADER_CHAR;
            }

            return Caption;
        }

        /// <summary>
        /// these are the log records that accumulate when we call the logger
        /// </summary>
        public static List<DlkLoggerRecord> mLogRecs
        {
            get
            {
                if (_LogRecs == null)
                {
                    _LogRecs = new List<DlkLoggerRecord>();
                }
                return _LogRecs;
            }
        }

        /// <summary>
        /// Log data about the system we are executing on
        /// </summary>
        public static void LogSystemInfo(String Browser, DlkLoginConfigRecord logInfo)
        {
            _LogSystemInfo("Machine", Environment.MachineName);
            _LogSystemInfo("OS Version", new ComputerInfo().OSFullName);
            _LogSystemInfo("Processor Count", Environment.ProcessorCount.ToString());
            _LogSystemInfo("System Page Size", Environment.SystemPageSize.ToString());
            _LogSystemInfo("Domain Name", Environment.UserDomainName);
            _LogSystemInfo("User Name", Environment.UserName);
            _LogSystemInfo("64 Bit?", Environment.Is64BitOperatingSystem.ToString());
            _LogSystemInfo("Browser", Browser);
            if (logInfo != null)
            {
                _LogSystemInfo("Login ID", logInfo.mID);
                _LogSystemInfo("Login URL", logInfo.mUrl);
                _LogSystemInfo("Login User", logInfo.mUser);
                _LogSystemInfo("Login Password", logInfo.mPassword);
                _LogSystemInfo("Login Database", logInfo.mDatabase);
            }
            else
            {
                _LogSystemInfo("Login Info", "Invalid");
            }

        }

        /// <summary>
        /// should only be used in TestExecute to log the raised error
        /// </summary>
        /// <param name="ex">exception object</param>
        public static void LogError(Exception ex)
        {
            /* User initiated test cancellation */
            if (DlkTestRunnerApi.mCancellationPending)
            {
                LogMsg("ExceptionMsg", STR_CANCELLATION_MESSAGE, false);
                return; /* Do not take screenshot */
            }
            /* Dynamic invocation exception, get inner exception */
            else if (ex.Message.Contains("Exception has been thrown by the target of an invocation"))
            {
                LogMsg("ExceptionMsg", ex.InnerException.Message);
                LogMsg("ExceptionStack", ex.InnerException.StackTrace);
            }
            /* All other error types */
            else
            {
                /* output file paths */
                if (ex.Message.Contains("See outputfile:"))
                {
                    LogMsg("ExceptionMsg", ex.Message.Substring(0, ex.Message.LastIndexOf("outputfile:") + 10));
                    LogMsg("OutputFile", ex.Message.Substring(ex.Message.LastIndexOf("outputfile:") + 12));
                }
                /* default */
                else
                {
                    LogMsg("ExceptionMsg", ex.Message);
                }
                LogMsg("ExceptionStack", ex.StackTrace);
            }
            /* Screen capture if possible */
            LogScreenCapture("ExceptionImg");
        }

        /// <summary>
        /// used to log info during test execution
        /// </summary>
        /// <param name="msg">Message to log</param>
        public static void LogInfo(String msg)
        {
            LogMsg("info", msg);
        }

        /// <summary>
        /// used to log info during test execution
        /// </summary>
        /// <param name="mActionOrMethod">Action to log</param>
        /// <param name="mControl">Control to log</param>
        public static void LogInfo(String mActionOrMethod, String mControl)
        {
            LogMsg("info", "Successfully executed: " + mActionOrMethod + ", Control: " + mControl);
        }

        /// <summary>
        /// used to log info during test execution
        /// </summary>
        /// <param name="mActionOrMethod">Action to log</param>
        /// <param name="mControl">Control to log</param>
        /// <param name="Details">Additional details to log</param>
        public static void LogInfo(String mActionOrMethod, String mControl, String Details)
        {
            LogMsg("info", "Successfully executed: " + mActionOrMethod + ", Control: " + mControl + ", " + Details);
        }

        /// <summary>
        /// used to log warnings during test execution
        /// </summary>
        /// <param name="msg">Warning message</param>
        public static void LogWarning(String msg)
        {
            LogMsg("warning", msg);
        }

        /// <summary>
        /// used to capture the screen at a state
        /// </summary>
        public static void LogScreenCapture(String sMsgType, String OutputFile = "", bool IgnoreError = true)
        {
            string sImgFile = string.Empty;
            try
            {
                sImgFile = string.IsNullOrEmpty(OutputFile) ? DlkEnvironment.mDirTestResultsCurrent + sMsgType.ToUpper() + "_" +
                    string.Format("{0:yyMMddhhmmss}", DateTime.Now) + ".png" : OutputFile;
                Screenshot ss = ((ITakesScreenshot)DlkEnvironment.AutoDriver).GetScreenshot();
                ss.SaveAsFile(sImgFile, ScreenshotImageFormat.Png);
                LogMsg(sMsgType, "Image: " + sImgFile);

            }
            catch
            {
                try
                {
                    using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                            Screen.PrimaryScreen.Bounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                        {
                            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                             Screen.PrimaryScreen.Bounds.Y,
                                             0, 0,
                                             bmpScreenCapture.Size,
                                             CopyPixelOperation.SourceCopy);
                        }
                        bmpScreenCapture.Save(sImgFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    LogMsg(sMsgType, "Image: " + sImgFile);
                }
                catch
                {
                    if (!IgnoreError)
                    {
                        throw;
                    }
                    DlkLogger.LogWarning("LogScreenCapture(): Could not create Screenshot.");
                }
            }
        }

        /// <summary>
        /// Take screen capture
        /// </summary>
        public static void LogScreenCapture()
        {
            LogScreenCapture("info");
        }

        /// <summary>
        /// used to log assertions
        /// </summary>
        /// <param name="TestDesc">Test description</param>
        /// <param name="ExpectedResult">Expected result</param>
        /// <param name="ActualResult">Actual result</param>
        public static void LogAssertion(String TestDesc, String ExpectedResult, String ActualResult)
        {
            LogAssertion(TestDesc, ExpectedResult, ActualResult, true);
        }

        /// <summary>
        /// used to log assertions
        /// </summary>
        /// <param name="TestDesc">Test description</param>
        /// <param name="ExpectedResult">Expected result</param>
        /// <param name="ActualResult">Actual Result</param>
        /// <param name="IsEqual">Expected Assertion match condition</param>
        public static void LogAssertion(String TestDesc, String ExpectedResult, String ActualResult, Boolean IsEqual)
        {
            String msg = TestDesc + " : Expected Result: [" + ExpectedResult + "], Actual Result: [" + ActualResult + "]";
            if (IsEqual)
            {
                LogMsg("assert|isequal", msg);
            }
            else
            {
                LogMsg("assert|isnotequal", msg);
            }
        }

        /// <summary>
        /// log assertion
        /// </summary>
        /// <param name="TestDesc">test description</param>
        /// <param name="ExpectedResult">Expected result to assert match</param>
        /// <param name="ActualResult">Actual result to assert match</param>
        public static void LogAssertionMatch(String TestDesc, String ExpectedResult, String ActualResult)
        {
            String msg = TestDesc + " : Expected Result: " + ExpectedResult + ", Actual Result: " + ActualResult;
            LogMsg("assert|partialmatch", msg);
        }

        /// <summary>
        /// used to log generic data
        /// </summary>
        /// <param name="Data">Data to log</param>
        public static void LogData(String Data)
        {
            // always log
            LogMsg("data", Data);
        }

        /// <summary>
        /// used to log data
        /// </summary>
        /// <param name="Msg">Message to log</param>
        /// <param name="Column1">Data columns</param>
        public static void LogData(String Msg, List<String> Column1)
        {
            // Always print regardless of log level
            System.Console.WriteLine("Data|" + DlkString.GetDateAsText("long") + ": " + Msg + ": ");
            for (int i = 0; i < Column1.Count; i++)
            {
                System.Console.WriteLine("     [" + Column1[i] + "]");
            }
        }

        /// <summary>
        /// Log generic system error to file under System directory
        /// </summary>
        /// <param name="Message">Error message</param>
        /// <param name="ExceptionObject">Optional Exception object for stack trace purposes</param>
        public static void LogToFile(String Message, Exception ExceptionObject = null, bool ShowErrorMessage = false)
        {
            try
            {
                InitializeLogger();
                if (ExceptionObject != null)
                {
                    File.AppendAllLines(m_SessionLogFile, new string[] { DlkString.GetDateAsText("long") + "| " + Message,
                    DlkString.GetDateAsText("long") + "| " + ExceptionObject.Source, 
                    DlkString.GetDateAsText("long") + "| " + ExceptionObject.Message, 
                    DlkString.GetDateAsText("long") + "| " + ExceptionObject.StackTrace });
                }
                else
                {
                    File.AppendAllLines(m_SessionLogFile, new string[] { DlkString.GetDateAsText("long") + "| " + Message });
                }

                if (ShowErrorMessage)
                {
                    MessageBox.Show(Message + " " + ExceptionObject.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                // do nothing
            }

        }

        /// <summary>
        /// Log to log listener (display) only
        /// </summary>
        /// <param name="Message">String to log</param>
        /// <param name="SessionId">Target session. OPTIONAL, if not provided, added to no particular session object</param>
        public static void LogToOutputDisplay(string Message, string SessionId = "")
        {
            if (!string.IsNullOrEmpty(SessionId))
            {
                if (_sessionLogs.Any(x => x.SessionId == SessionId))
                {
                    _sessionLogs.FirstOrDefault(x => x.SessionId == SessionId).AddRecord(
                        new DlkLoggerRecord(STR_TYPE_OUTPUT_ONLY_LOG, Message));
                }
            }
            else
            {
                LogMsg(STR_TYPE_OUTPUT_ONLY_LOG, Message);
            }
        }

        /// <summary>
        /// Get target session object with input ID
        /// </summary>
        /// <param name="SessionId">ID of target session. Format is in 32-bit GUID</param>
        /// <returns>Session object</returns>
        public static DlkLoggerSessionObject GetSessionObject(string SessionId)
        {
            return _sessionLogs.FirstOrDefault(x => x.SessionId == SessionId);
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Log system info
        /// </summary>
        /// <param name="SettingName">System attribute</param>
        /// <param name="SettingValue">Setting value</param>
        private static void _LogSystemInfo(String SettingName, String SettingValue)
        {
            LogMsg("SystemInfo", SettingName + ": " + SettingValue);
        }

        /// <summary>
        /// used internally... almost all log messages end up here... this manages the log records
        /// </summary>
        /// <param name="msgType">Message type</param>
        /// <param name="Msg">Message</param>
        /// <param name="DetectCancellation">Optional param: Flag to indicate if LogMsg will check if user cancelled</param>
        private static void LogMsg(String msgType, String Msg, bool DetectCancellation = true)
        {
            DlkLoggerRecord mRec = new DlkLoggerRecord(msgType.Trim().ToUpper(), Msg);
            AddLog(mRec);
            /* This is the most common func call used in framework. Detect user call to cancel here */
            if (DetectCancellation && DlkTestRunnerApi.mCancellationPending)
            {
                throw new Exception(STR_CANCELLATION_MESSAGE);   
            }
        }

        /// <summary>
        /// Initialize generic system error logger
        /// </summary>
        private static void InitializeLogger()
        {
            try
            {
                if (!m_IsInitialized)
                {
                    if (!Directory.Exists(m_LogFolderPath))
                    {
                        Directory.CreateDirectory(m_LogFolderPath);
                    }
                    m_SessionLogFile = System.IO.Path.Combine(m_LogFolderPath, DlkString.GetDateAsText("file") + ".log");
                }
                m_IsInitialized = true;
            }
            catch
            {
                // do nothing
            }
        }

        /// <summary>
        /// Add log to log list and to any session object with same thread ID
        /// </summary>
        /// <param name="Record">Record to add to log list</param>
        private static void AddLog(DlkLoggerRecord Record)
        {
            if (Record.mMessageType != DlkLogger.STR_TYPE_OUTPUT_ONLY_LOG)
            {
                mLogRecs.Add(Record);
            }

            /* NOTE: Thread ID was used since this is UNIQUE for threads running in the same process */
            /* WARNING: However, Thread ID is recycled everytime a thread terminates. There are possible effects on parallel execution */
            if (_sessionLogs.Any(x => x.ThreadId == Thread.CurrentThread.ManagedThreadId.ToString()))
            {
                _sessionLogs.FirstOrDefault(x => x.ThreadId == Thread.CurrentThread.ManagedThreadId.ToString()).AddRecord(Record);
            }
        }
        #endregion
    }

    /// <summary>
    /// Class to store logs of a particular session, identified by a unique Session ID (GUID) and unique Thread ID (int)
    /// </summary>
    public class DlkLoggerSessionObject : INotifyPropertyChanged
    {
        #region DECLARATIONS
        private List<DlkLoggerRecord> mRecords = new List<DlkLoggerRecord>();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PUBLIC
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="sessionId">Input session ID</param>
        /// <param name="threadId">Input Thread ID</param>
        public DlkLoggerSessionObject(string sessionId, string threadId)
        {
            SessionId = sessionId;
            ThreadId = threadId;
        }

        /// <summary>
        /// Session ID in 32-bit GUID format
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Thread ID in uint format
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// Logs formatted to newline-delimted string
        /// </summary>
        public string Output
        {
            get
            {
                try
                {
                    return string.Join("\r\n", mRecords.Select(x => x.MessageLine));
                }
                catch
                {
                    /* Swallow. Let next call to Output update the display. */
                    /* Sometimes mRecords changes too fast before update is propagated, throwing invalidoperation exception */
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Add log record
        /// </summary>
        /// <param name="record">Record to add</param>
        public void AddRecord(DlkLoggerRecord record)
        {
            mRecords.Add(record);
            NotifyPropertyChanged("Output");
        }
        #endregion

        #region PRIVATE
        /// <summary>
        /// Property changed event notifier
        /// </summary>
        /// <param name="propertyName">Name of property changed</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
