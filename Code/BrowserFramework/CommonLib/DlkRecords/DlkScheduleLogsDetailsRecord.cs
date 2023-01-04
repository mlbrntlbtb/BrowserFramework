using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkScheduleLogsDetailsRecord
    {
        public String mProduct { get; set; }
        public String mDateTime { get; set; }
        public String mTestID { get; set; }
        public String mTestEnvironment { get; set; }
        public String mTestName { get; set; }
        public String mTestStatus { get; set; }
        public String mAgentName { get; set; }
        public String mAgentStatus { get; set; }
        public String mMessage { get; set; }

        public DlkScheduleLogsDetailsRecord(String Product, String DateTime, String TestID, String TestEnvironment, String TestName, String TestStatus, String AgentName, String AgentStatus, String Message)
        {
            mProduct = Product;
            mDateTime = DateTime;
            mTestID = TestID;
            mTestEnvironment = TestEnvironment;
            mTestName = TestName;
            mTestStatus = TestStatus;
            mAgentName = AgentName;
            mAgentStatus = AgentStatus;
            mMessage = Message;
        }
    }
}
