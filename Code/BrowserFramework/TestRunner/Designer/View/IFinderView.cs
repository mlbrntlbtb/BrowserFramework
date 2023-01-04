using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;

namespace TestRunner.Designer.Presenter
{
    public interface IFinderView
    {
        List<String> Screens { get; set; }
        List<String> Controls { get; set; }
        List<String> Keywords { get; set; }
        List<Match> FinderMatches { get; set; }
        String CurrentControl { get; set; }
        String CurrentKeyword { get; set; }
        String CurrentParameter { get; set; }
        String CurrentScreen { get; set; }
        List<DlkTestStepRecord> FinderTest { get; set; }
        bool IncludeParameters { get; set; }
        void UpdateViewStatus(object Status);
        Filter Filter { get; set; }
    }

}

public interface IDuplicateView : IFinderView
{
    ObservableCollection<ExcludedStepRecord> ExcludedSteps { get; set; }
    List<Duplicate> DuplicateTests { get; set; }
    bool IncludeDupParameters { get; set; }
    List<DlkTest> LoadedTests { get; set; }
}
