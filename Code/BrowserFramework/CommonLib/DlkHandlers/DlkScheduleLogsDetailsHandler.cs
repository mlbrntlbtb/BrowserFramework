using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    public class DlkScheduleLogsDetailsHandler
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
        public ObservableCollection<DlkScheduleLogsDetailsRecord> mScheduleLogsRecords { get; set; }
        private XDocument schedulerLogs { get; set; }
        public DlkScheduleLogsDetailsHandler(String ScheduleLogsDetailsPath)
        {
            mScheduleLogsRecords = new ObservableCollection<DlkScheduleLogsDetailsRecord>();
            schedulerLogs = XDocument.Load(ScheduleLogsDetailsPath);
            foreach (XElement node in schedulerLogs.Descendants("statuslog"))
            {
                mProduct = (String)node.Attribute("product").Value;
                mDateTime = string.IsNullOrEmpty((String)node.Element("datetime")) ? string.Empty : (String)node.Element("datetime");
                mTestID = string.IsNullOrEmpty((String)node.Element("testid")) ? string.Empty : (String)node.Element("testid");
                mTestEnvironment = string.IsNullOrEmpty((String)node.Element("testenvironment")) ? string.Empty : node.Element("testenvironment").ToString().ToLower().Contains("use default") ? "DEFAULT" : (String)node.Element("testenvironment");
                mTestName = string.IsNullOrEmpty((String)node.Element("testname")) ? string.Empty : (String)node.Element("testname");
                mTestStatus = string.IsNullOrEmpty((String)node.Element("teststatus")) ? string.Empty : (String)node.Element("teststatus");
                mAgentName = string.IsNullOrEmpty((String)node.Element("agentname")) ? string.Empty : (String)node.Element("agentname");
                mAgentStatus = string.IsNullOrEmpty((String)node.Element("agentstatus")) ? string.Empty : (String)node.Element("agentstatus");
                mMessage = string.IsNullOrEmpty((String)node.Element("message")) ? string.Empty : (String)node.Element("message");

                DlkScheduleLogsDetailsRecord schedLogs = new DlkScheduleLogsDetailsRecord(mProduct, mDateTime, mTestID, mTestEnvironment, mTestName, mTestStatus, mAgentName, mAgentStatus, mMessage);
                mScheduleLogsRecords.Add(schedLogs);
            }
        }
    }
}
