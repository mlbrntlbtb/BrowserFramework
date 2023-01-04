using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using CommonLib.DlkSystem;
using TestRunner.Common;
using CommonLib.DlkHandlers;
using System.Text.RegularExpressions;

namespace TestRunner.AdvancedScheduler.Model
{
    public class TestLineupStatus
    {
        private Action<StatusLogContext> mContext { get; set; }
        public TestLineupStatus(Action<StatusLogContext> context) 
        {
            mContext = context;
        }

        public void Log(TestLineupRecord record, Agent agent, string message)
        {
            mContext(new StatusLogContext
            {
                Color =  GetColor(record.Status),
                Title = $"{FormatTextToDisplay(record.TestSuiteToRun.Name)} | {CurrentAgentName(record)}",
                Message = FormatMessageWithOffset(message, record.TestSuiteToRun.Name.Count())
            });
            SaveStatusLog(record, agent, message);
        }

        public void Clear()
        {
            mContext(new StatusLogContext
            {
                Color = new StatusColor { Background = Brushes.White },
                Title = string.Empty,
                Message = string.Empty
            });
        }

        public void SaveStatusLog(TestLineupRecord record, Agent agent, string message)
        {
            try
            {
                var path = GetLogsLocation();
                var newLog = new XElement("statuslog",
                                new XAttribute("product", HandleProdName(record.Product)),
                                new XElement("datetime", DateTime.Now.ToString()),
                                new XElement("testid", record.Id),
                                new XElement("testenvironment", record.Environment),
                                new XElement("testname", record.TestSuiteToRun.Name),
                                new XElement("teststatus", record.Status.ToString()),
                                new XElement("agentname", CurrentAgentName(record)),
                                new XElement("agentstatus", agent == null ? Enumerations.AgentStatus.Null : agent.Status),
                                new XElement("message", Regex.Replace(message, @"\s*[\n\r]\s*", "\n")));

                if (File.Exists(path))
                {
                    if (ShouldSaveLog(record, agent))
                    {
                        var file = XDocument.Load(path);
                        var schedulerstatuslogs = file.Descendants("schedulerstatuslogs").FirstOrDefault();
                        PurgeOldStatus(schedulerstatuslogs, path);
                        if (schedulerstatuslogs != null)
                        {
                            schedulerstatuslogs.Add(newLog);
                            schedulerstatuslogs.Save(path);
                        }
                    }
                }
                else new XDocument(new XElement("schedulerstatuslogs", newLog)).Save(path);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Will determin whether to save logs or not
        /// </summary>
        /// <param name="record"></param>
        /// <param name="agent"></param>
        /// <returns>Boolean</returns>
        private bool ShouldSaveLog(TestLineupRecord record, Agent agent)
        {
            var result = 
                record.Status == Enumerations.TestStatus.Pending && 
                agent.Status == Enumerations.AgentStatus.Warning;
            return !result;
        }

        private string HandleProdName(string prodname)
        {
            if (int.TryParse(prodname, out int pname))
            {
                var foundProdname = DlkTestRunnerSettingsHandler.ApplicationList.FirstOrDefault(a => a.ID == prodname);
                if (foundProdname == null) return "Undefined";
                return foundProdname.DisplayName;
            }
            else return prodname;
                
        }

        private string FormatMessageWithOffset(string messageToDisplay, int textToOffsetCount)
        {
            const int MAX_COUNT = 100;
            const int MESSAGE_MAX_COUNT = 70;
            int maxCharCountWithOffset;

            if (textToOffsetCount > MAX_COUNT) maxCharCountWithOffset = MESSAGE_MAX_COUNT;
            else maxCharCountWithOffset = (MAX_COUNT + MESSAGE_MAX_COUNT) - textToOffsetCount;

            return FormatTextToDisplay(messageToDisplay, maxCharCountWithOffset);
        }

        private string FormatTextToDisplay(string textToDisplay, int maxCharCount = 100)
        {

            if (textToDisplay.Count() > maxCharCount)
                return textToDisplay
                    .Replace("\n", string.Empty).Replace("\r", string.Empty)
                    .Substring(0, maxCharCount) + "...";

            return textToDisplay.Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        private void PurgeOldStatus(XElement parentElement, string path)
        {
            const int MAX_DAYS = 7;
            var statuslogs = parentElement.Descendants("statuslog").ToList();
            foreach (var sl in statuslogs)
            {
                var datetime = sl.Descendants("datetime").FirstOrDefault().Value;
                var datetimeStamp = Convert.ToDateTime(datetime);
                var days = (DateTime.Now - datetimeStamp).TotalDays;
                if (days >= MAX_DAYS)
                {
                    sl.Remove();
                    sl.Save(path);
                }
            }
        }

        public string GetLogsLocation()
        {
            try
            {
                return Path.Combine(DlkEnvironment.mDirProductsRoot, @"Common\Scheduler\schedulerstatuslogs.log");
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                return null;
            }
        }

        private string CurrentAgentName(TestLineupRecord record)
        {
            return 
                record.AssignedAgentName == Agent.ANY_AGENT_NAME &&
                record.Status != Enumerations.TestStatus.Pending
                ? record.PrevAgentName
                : record.AssignedAgentName;
        }

        private StatusColor GetColor(Enumerations.TestStatus testStatus)
        {
            switch (testStatus)
            {
                case Enumerations.TestStatus.Running:
                    return new StatusColor { Background = Brushes.DodgerBlue };
                case Enumerations.TestStatus.Passed:
                    return new StatusColor { Background = Brushes.LimeGreen };
                case Enumerations.TestStatus.Cancelling:
                case Enumerations.TestStatus.Pending:
                    return new StatusColor { Background = Brushes.LemonChiffon };
                case Enumerations.TestStatus.Error:
                case Enumerations.TestStatus.Failed:
                case Enumerations.TestStatus.Warning:
                    return new StatusColor { Background = Brushes.SandyBrown };
                case Enumerations.TestStatus.Cancelled:
                case Enumerations.TestStatus.Disconnected:
                    return new StatusColor { Background = Brushes.LightGray };
                default:
                    return new StatusColor { Background = Brushes.White };
            }
        }
    }
    public class StatusLogContext
    {
        public StatusColor Color { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class StatusColor
    {
        public Brush Background { get; set; }
        public Brush Foreground { get; set; } = Brushes.Black;
    }
}
