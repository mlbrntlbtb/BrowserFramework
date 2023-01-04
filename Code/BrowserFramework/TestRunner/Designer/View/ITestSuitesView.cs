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
    public interface ITestSuitesView
    {
        List<KwDirItem> TestSuiteRoot { get; set; }
        List<KwDirItem> FilteredSuites { get; set; }
        TLSuite TargetSuite { get; set; }
        DlkExecutionQueueRecord TargetSuiteTests {get; set;}
        List <DlkTestStepRecord> SelectedTestSteps { get; set; }
        void UpdateViewStatus(object Status);
    }
}
