using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;
using TestRunner.AdvancedScheduler.Model;

namespace TestRunner.AdvancedScheduler.View
{
    public interface IScheduleRunnerView
    {
        bool AllowExecute { get; set; }
        void UpdateScheduleRecord(TestLineupRecord record);
        Enumerations.TestStatus GetScheduleStatus(TestLineupRecord record);
        IEnumerable<Agent> GetAvailableAgents();
        IEnumerable<Agent> GetAvailableAgentsInGroup(string groupName);
        void RemoveAgent(Agent agent);
    }
}
