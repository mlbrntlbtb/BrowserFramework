using System.Collections.ObjectModel;
using TestRunner.AdvancedScheduler.Model;

namespace TestRunner.AdvancedScheduler.View
{
    public interface IAgentsView
    {
        ObservableCollection<Agent> AgentsPool { get; set; }
        ObservableCollection<AgentGroup> AgentsGroupPool { get; set; }
        ObservableCollection<ExecutionHistory> AgentHistory { get; set; }
        string GetSuiteName(string SuiteId);
    }
}
