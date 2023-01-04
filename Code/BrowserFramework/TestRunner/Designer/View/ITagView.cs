using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Designer.Model;
using TestRunner.Common;

namespace TestRunner.Designer.View
{
    public interface ITagView
    {
        List<DlkTag> AllTags { get; set; }
        List<DlkTag> SelectedTags { get; set; }
        List<DlkTag> AvailableTags { get; set; }
        List<DlkTag> TagsToAdd { get; set; }
        List<DlkTest> LoadedTests { get; set; }
        List<TLSuite> LoadedSuites { get; set; }

        int FilesUpdated { get; set; }
    }
}
