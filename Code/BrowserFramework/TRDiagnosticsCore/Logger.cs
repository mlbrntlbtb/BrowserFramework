using System;
using System.Collections.Generic;
using System.Text;
using TRDiagnosticsCore.Utility;

namespace TRDiagnosticsCore
{
    public class Logger
    {
        public List<LogRecord> Logs = new List<LogRecord>();
        private bool _isConsole;

        public enum MessageType
        {
            INFO,
            ERROR,
            WARNING,
            SUCCESS,
            RECOMMENDATION,
            COUNTER,
            FILEPROGRESS,
            NULL
        }

        public Logger(Boolean IsConsole = false)
        {
            _isConsole = IsConsole;
        }

        public void LogResult(MessageType Type, String Message, bool PrintToConsole = true)
        {
            LogRecord log = new LogRecord(Type, Message);
            if (Type != MessageType.FILEPROGRESS && Type != MessageType.COUNTER)
            {
                Logs.Add(log);
            }

            if (PrintToConsole)
            {
                if (_isConsole && Type != MessageType.COUNTER && Type != MessageType.FILEPROGRESS)
                {
                    log.PrintToConsole();
                }
                else
                {
                    UILogger.LogResult(Type, Message);
                }
            }
        }
    }
}
