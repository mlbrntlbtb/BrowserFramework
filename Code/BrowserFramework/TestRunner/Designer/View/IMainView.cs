using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;
using CommonLib.DlkHandlers;
using TestRunner.Designer.Model;

namespace TestRunner.Designer.View
{
    public interface IMainView
    {
        List<KwDirItem> Suites { get; set; }
        List<KwDirItem> Tests { get; set; }
        List<DlkTest> AllTests { get; }
        List<TLSuite> AllSuites { get; }
        List<DlkObjectStoreFileRecord> ObjectStoreFiles { get; set; }
        void UpdateViewStatus(object Status);
        string CurrentQueryPath { get; set; }
        Query CurrentQuery { get; set; }
        DataView CurrentQueryDataSource { get; set; }
        Enumerations.QueryType CurrentQueryType { get; set; }
        Dictionary<int, Enumerations.QueryRowColor> QueryColorMap { get; set; }
        List<QueryRow> QueryRows { get; set; }
        List<QueryCol> QueryCols { get; set; }
        List<DlkTag> TagsToAdd { get; set; }
        List<DlkTag> Tags { get; set; }
        List<string> QueryTypes { get; set; }
        List<string> QueryList { get; set; }
        List<DlkTest> QueryResultsTest { get; set; }
        DlkTest SelectedQueryResultTest { get; set; }
        bool ShowQueryResult { get; set; }
        Dictionary<string, List<DlkTest>> AllQueryResultTest { get; set; }
        string QueryResultsTitle { get; set; }
    }
}
