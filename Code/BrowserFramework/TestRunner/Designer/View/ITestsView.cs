using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;
using TestRunner.Designer.Model;

namespace TestRunner.Designer.View
{
    public interface ITestsView
    {
        List<KwDirItem> TestRoot { get; set; }
        List<KwDirItem> FilteredTests { get; set; }
        List<TLSuite> ContainingSuites { get; set; }
        DlkTest TargetTest { get; set; }
        List<DlkExecutionQueueRecord> SelectedSuiteTests { get; set; }
        List<DlkTestStepRecord> SelectedSuiteTestSteps { get; set; }
        void UpdateViewStatus(object Status);
    }
}
