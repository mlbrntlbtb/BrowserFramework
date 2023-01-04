using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;
using TestRunner.AdvancedScheduler.Model;

namespace TestRunner.AdvancedScheduler.View
{
    public interface IControllerView
    {
        ObservableCollection<TestLineupRecord> TestLineup { get; set; }
        bool IsSaveInProgress { get; set; }
        ObservableCollection<KwDirItem> Favorites { get; set; }
        ObservableCollection<ExecutionHistory> History { get; }
    }
}
