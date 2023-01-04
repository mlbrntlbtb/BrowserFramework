using System.Collections.ObjectModel;
using CommonLib.DlkRecords;

namespace Recorder.View
{
    public interface IEditStepView
    {
        DlkTestStepRecord TargetStep { get; set; }
        ObservableCollection<DlkKeywordParameterRecord> Parameters { get; set; }
    }
}
