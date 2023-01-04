using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.DlkUtility;
using CommonLib.DlkSystem;

namespace CommonLib.DlkRecords
{
    [Serializable]
    public class DlkLoggerRecord
    {
        private string _mMessageDetails;
        public DateTime mMessageDateTime { get; set; }
        public String mMessageType { get; set; }
        public String mMessageDetails
        {
            get 
            {
                return _mMessageDetails;
            }
            set
            {
                if (value != null)
                {
                    _mMessageDetails = DlkString.ReplaceUnicode(value, "");
                }
                else
                {
                    _mMessageDetails = "";
                }
            }
        }

        public DlkLoggerRecord()
        {
            mMessageDateTime = DateTime.Now;
            mMessageType = "";
            mMessageDetails = "";
        }
        public DlkLoggerRecord(DateTime MessageDateTime, String MessageType, String MessageDetails)
        {
            mMessageDateTime = MessageDateTime;
            mMessageType = MessageType;
            mMessageDetails = MessageDetails;
        }
        public DlkLoggerRecord(String MessageType, String MessageDetails)
        {
            mMessageDateTime = DateTime.Now;
            mMessageType = MessageType;
            mMessageDetails = MessageDetails;
        }

        public string MessageLine
        {
            get
            {
                return mMessageType == DlkLogger.STR_TYPE_OUTPUT_ONLY_LOG ? mMessageDetails 
                    : "[" + mMessageType + "] " + DlkString.GetDateAsText(mMessageDateTime, "long") + "| " + mMessageDetails;
            }
        }
    }
}