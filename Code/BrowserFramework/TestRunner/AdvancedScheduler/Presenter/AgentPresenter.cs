using CommonLib.DlkHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.AdvancedScheduler.View;
using CommonLib.DlkSystem;

namespace TestRunner.AdvancedScheduler.Presenter
{
    public class AgentPresenter
    {
        private IAgentsView mView = null;
        private string mAgentsFile = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "schedulingagents" + ".agent");

        private const int RUNTIME_IDX = 1;
        private const int OPERATING_SYSTEM_IDX = 2;
        private const int AVAILABLE_SPACE_IDX = 3;
        private const int SYSTEM_MEMORY_IDX = 4;
        private const int PEAK_MEMORY_IDX = 5;

        public AgentPresenter(IAgentsView View)
        {
            mView = View;
        }

        public void LoadAgents()
        {
            mView.AgentsPool.Clear();
            if (File.Exists(mAgentsFile))
            {
                XDocument xAgents = XDocument.Load(mAgentsFile);

                //load agents
                var agentData = from doc in xAgents.Descendants("agent")
                                select new Agent(doc.Attribute("name").Value, Agent.AGENT_TYPE_NETWORK)
                                {
                                    Id = doc.Attribute("id").Value,
                                    Status = Enumerations.AgentStatus.Offline,
                                    Disabled = doc.Attribute("disabled") != null ? Boolean.Parse(doc.Attribute("disabled").Value) : false
                                };
                mView.AgentsPool = new ObservableCollection<Agent>(agentData);

                //load groups
                var groupData = from doc in xAgents.Descendants("agentgroup")
                                select new AgentGroup(doc.Attribute("name").Value)
                                {
                                    Members = doc.Descendants("member").Select(x => x.Attribute("name").Value).ToList()
                                };
                mView.AgentsGroupPool = new ObservableCollection<AgentGroup>(groupData);
                mView.AgentsGroupPool.Insert(0, new AgentGroup(AgentGroup.ALL_GROUP_NAME));                
            }
            else
            {
                mView.AgentsGroupPool = new ObservableCollection<AgentGroup>();
                mView.AgentsGroupPool.Insert(0, new AgentGroup(AgentGroup.ALL_GROUP_NAME));     
            }
        }

        public void SaveAgents()
        {
            //agents list
            List<XElement> agentList = new List<XElement>();
            foreach (Agent agent in mView.AgentsPool)
            {
                XElement elem = new XElement("agent",
                    new XAttribute("name", agent.Name),
                    new XAttribute("id", agent.Id),
                    new XAttribute("disabled", agent.Disabled)
                    );
                agentList.Add(elem);
            }

            //agent groups
            List<XElement> agentGroups = new List<XElement>();
            foreach (AgentGroup group in mView.AgentsGroupPool.Where(x=>x.Name != AgentGroup.ALL_GROUP_NAME))
            {
                XElement groupElement = new XElement( "agentgroup",
                                                        new XAttribute("name", group.Name),
                                                        group.Members.Select(x => new XElement("member", new XAttribute("name", x)))
                                                    );
                agentGroups.Add(groupElement);
            }

            //combine to schedulingagent element
            XElement schedulingagent = new XElement("schedulingagent", agentList, agentGroups);

            XDocument xDoc = new XDocument(schedulingagent);
            XDocument refDoc = File.Exists(mAgentsFile) ? XDocument.Load(mAgentsFile) : null;
            xDoc.Save(mAgentsFile);
        }

        public void UpdateStatus(string agentName)
        {
            string description = string.Empty;
            string status = "ready";
            var agentStatus = Enumerations.AgentStatus.Ready;

            status = AgentUtility.SendCommandToServer(agentName, "status");

            Agent agent = mView.AgentsPool.FirstOrDefault(x => x.Name == agentName);

            //Exit if disabled
            if (agent.Disabled)
                return;

            if (status == null)
            {
                agentStatus = Enumerations.AgentStatus.Offline;

                //if getting latest, offline means updating
                if (agent.GettingLatest)
                    agentStatus = Enumerations.AgentStatus.Updating;
            }
            else if (status.ToLower().StartsWith("ready"))
            {
                agentStatus = !agent.IsReserved ? Enumerations.AgentStatus.Ready : Enumerations.AgentStatus.Reserved; //do not reset reserved agents

                //reset getting latest flag once agent is online
                if (agent.GettingLatest)
                    agent.GettingLatest = false;
            }
            else if (status.ToLower().StartsWith("error"))
            {
                description = status.Substring(6, status.Length - 6);
                agentStatus = Enumerations.AgentStatus.Error;
            }
            else if (status.ToLower().StartsWith("warning"))
            {
                description = status.Substring(8, status.Length - 8);
                agentStatus = Enumerations.AgentStatus.Warning;
            }
            else
            {
                string[] arrStatus = status.Split('|');
                description = string.Format("{0} : {1}", arrStatus[arrStatus.Count() - 1], mView.GetSuiteName(status.Substring(0, status.IndexOf('|'))));
                agentStatus = Enumerations.AgentStatus.Busy;
            }

            if (agent != null)
            {
                agent.Status = agentStatus;
                agent.Description = description.Trim();
            }

            //update the agent stats
            if (status != null)
            {
                string[] overallStatus = status.Split('|');
                if (overallStatus.Count() > 2)
                {
                    agent.AgentStats = new AgentSystemStatistics();
                    agent.Runtime = overallStatus[RUNTIME_IDX];
                    agent.AgentStats.OperatingSystem = overallStatus[OPERATING_SYSTEM_IDX];
                    double diskSpace = 0;
                    double.TryParse(overallStatus[AVAILABLE_SPACE_IDX], out diskSpace);
                    agent.AgentStats.AvailableDiskSpace = diskSpace;
                    agent.AgentStats.SystemMemory = overallStatus[SYSTEM_MEMORY_IDX];
                    agent.AgentStats.PeakMemoryUsage = overallStatus[PEAK_MEMORY_IDX];
                }
            }

            //getlatest command is placed here to avoid conflict with agent status engine
            //once status check is called, set the gettinglatest flag to true so we know it's updating until it is back online
            if (!string.IsNullOrEmpty(agent.GetLatestFolderName))
            {
                AgentUtility.SendCommandToServer(agent.Name, "getlatest|" + agent.GetLatestFolderName);
                agent.GetLatestFolderName = string.Empty;
                agent.GettingLatest = true;
                agent.Status = Enumerations.AgentStatus.Updating;
            }
        }

        /// <summary>
        /// Delete selected item from agent history grid
        /// </summary>
        /// <param name="historyList">List of Agent history</param>
        public void DeleteHistoryGridItem(List<ExecutionHistory> historyList)
        {
            var suiteResultsDirectory = DlkEnvironment.mDirSuiteResults;

            foreach (ExecutionHistory history in historyList)
            {
                var mSuiteResultsFolder = suiteResultsDirectory + Path.GetFileNameWithoutExtension(history.SuiteFullPath);
                if (Directory.Exists(mSuiteResultsFolder))
                {
                    var directoryList = new DirectoryInfo(mSuiteResultsFolder).GetDirectories();
                    string folderName = string.Format("{0}{1}", Convert.ToDateTime(history.ExecutionDate).ToString("yyyyMMdd"), Convert.ToDateTime(history.EndTime).ToString("HHmmss"));
                    if (directoryList.Any(x => x.Name.Equals(folderName)))
                    {
                        string path = directoryList.FirstOrDefault(x => x.Name.Equals(folderName)).FullName;
                        Directory.Delete(path, true);
                    }
                }
            }
        }
    }
}
