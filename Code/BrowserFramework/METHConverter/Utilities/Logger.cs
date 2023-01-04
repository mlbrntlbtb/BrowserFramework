using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Utilities
{
    public class Logger
    {
        public List<string> Logs = new List<string>();
        private static string LOG_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs.txt");

        private const string INF = "INF";
        private const string ERR = "ERR";
        private const string WRN = "WRN";

        public enum MessageType
        {
            INF,
            ERR,
            WRN,
            NUL
        }

        public void WriteLine(string Message, MessageType MsgType = MessageType.NUL)
        {
            string newEntry = "[" + GetMsgTypeToWrite(MsgType) + "] " + "[" + GetTimeStamp() + "] |" + Message;
            Console.WriteLine(newEntry);
            Logs.Add(newEntry);
        }

        public void WriteToFile()
        {
            foreach (string log in Logs)
            {
                File.AppendAllText(LOG_PATH, log + Environment.NewLine);
            }
        }

        private string GetTimeStamp()
        {
            return string.Format("{0:dd/mm/yyyy hh:mm}", DateTime.Now);
        }

        private string GetMsgTypeToWrite(MessageType MsgType)
        {
            string ret = string.Empty;
            switch (MsgType)
            {
                case MessageType.INF:
                    ret = INF;
                    break;
                case MessageType.ERR:
                    ret = ERR;
                    break;
                case MessageType.WRN:
                    ret = WRN;
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}
