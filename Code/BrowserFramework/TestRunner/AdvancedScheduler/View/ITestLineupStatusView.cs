using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.AdvancedScheduler.Model;

namespace TestRunner.AdvancedScheduler.View
{
    public interface ITestLineupStatusView
    {
        ObservableCollection<Agent> AgentsPool { get; set; }
        bool CheckIfLocalMachineBusy();
        TestLineupStatus TestLineupStatus { get; set; }
    }
}
