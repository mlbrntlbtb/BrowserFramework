using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;
using TestRunner.Designer.Model;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;

namespace TestRunner.Designer.View
{
    public interface IBatchReplaceView
    {
        DlkTest TargetTest { get; set; }
        List<KwDirItem> Tests { get; set; }
        List<DlkExecutionQueueRecord> TestsInSuite { get; set; }
        bool IsSuite { get; }
        bool IsExactMatch { get; }
        List<DlkTestStepRecord> SelectedTestSteps { get; set; }
        List<DlkTest> TestsForSaving { get; set; }
        List<string> UpdatedTests { get; set; }
        string NewText { get; }
        string TextToSearch { get; }
        string SelectedTestDirPath { get; set; }
        int FilesUpdated { get; set; }
        Enumerations.BatchReplaceType ReplaceType { get; }
    }

}