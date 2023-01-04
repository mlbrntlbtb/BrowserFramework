using System.Collections.ObjectModel;
using TestRunner.AdvancedScheduler.Model;

namespace TestRunner.AdvancedScheduler.View
{
    public interface IExportView
    {
        ObservableCollection<TestLineupRecord> TestLineup { get; }
        ObservableCollection<Agent> Agents { get; }
    }
}
