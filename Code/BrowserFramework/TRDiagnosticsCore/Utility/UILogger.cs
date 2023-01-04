using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TRDiagnosticsCore.Logger;

namespace TRDiagnosticsCore.Utility
{
    public static class UILogger
    {
        public const string HEADER = "==================================================";
        private static int _currentCount = 0;

        /// <summary>
        /// TR UI log event trigger
        /// </summary>
        public static event TRDiagnosticLog TRDiagnosticLogHandler;

        /// <summary>
        /// Event trigger method delegate
        /// </summary>
        /// <param name="type">Diagnostic log message type</param>
        /// <param name="message">Diagnostic log message</param>
        public delegate void TRDiagnosticLog(MessageType type, String message);   

        /// <summary>
        /// Dianostic test progress counter
        /// </summary>
        public static int CurrentTestCount { get { return _currentCount; } private set { } }

        /// <summary>
        /// Log result for diagnostic test
        /// </summary>
        /// <param name="Type">Message type (e.g. INFORMATION,WARNING,...)</param>
        /// <param name="Message">Log message</param>
        public static void LogResult(MessageType Type, String Message)
        {
            if (TRDiagnosticLogHandler != null)
            {
                if (Type == MessageType.COUNTER)
                    _currentCount++;

                TRDiagnosticLogHandler.Invoke(Type, Message);
            }
        }

        /// <summary>
        /// Log diagnostic-test Test Name
        /// </summary>
        /// <param name="TestName">Test name</param>
        public static void LogHeader(String TestName)
        {
            string message = $"{HEADER}\n{TestName}\n{HEADER}";
            TRDiagnosticLogHandler?.Invoke(MessageType.NULL, message);
        }

        /// <summary>
        /// Reset current test count after performing test
        /// </summary>
        public static void ResetTestCount()
        {
            _currentCount = 0;
        }
    }
}
