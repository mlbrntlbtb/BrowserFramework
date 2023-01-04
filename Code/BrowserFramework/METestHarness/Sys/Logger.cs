using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METestHarness.Sys
{
    public class Logger
    {
        public List<string> Logs = new List<string>();
        public enum MessageType
        {
            INF,
            ERR,
            WRN,
            NUL
        }
        private const string INF = "INF";
        private const string ERR = "ERR";
        private const string WRN = "WRN";

        public void WriteLine(string Message, MessageType MsgType = MessageType.NUL)
        {
            string newLine = GetLogHeader(MsgType) + Message;
            Console.WriteLine(newLine);
            Logs.Add(newLine);
        }

        private string GetLogHeader(MessageType MsgType)
        {
            if (MsgType == MessageType.NUL)
            {
                return string.Empty;
            }
            return "[" + GetMsgTypeToWrite(MsgType) + "] ";
        }

        private string GetMsgTypeToWrite(MessageType MsgType)
        {
            string ret = string.Empty;
            switch(MsgType)
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

        private string GetTimeStamp()
        {
            return string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
        }
    }
}
