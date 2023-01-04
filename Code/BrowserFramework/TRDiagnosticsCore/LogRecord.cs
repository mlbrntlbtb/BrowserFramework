using System;

namespace TRDiagnosticsCore
{
    public class LogRecord
    {
        public Logger.MessageType MessageType { get; set; }

        public String MessageDetails { get; set; }

        public LogRecord(Logger.MessageType messageType, String messageDetails)
        {
            MessageType = messageType;
            MessageDetails = messageDetails;
        }

        public void PrintToConsole()
        {
            setColor();
            Console.Write($"[{MessageType.ToString()}] ");
            setDefault();
            Console.Write($"{MessageDetails.ToString()}");
            Console.WriteLine();
        }

        private void setColor()
        {
            switch (MessageType)
            {
                case Logger.MessageType.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Logger.MessageType.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Logger.MessageType.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Logger.MessageType.SUCCESS:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Logger.MessageType.RECOMMENDATION:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case Logger.MessageType.NULL:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        private void setDefault() => Console.ForegroundColor = ConsoleColor.White;


    }
}
