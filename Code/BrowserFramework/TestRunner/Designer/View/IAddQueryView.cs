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
    public interface IAddQueryView
    {
        string Name { get; set; }
        Enumerations.AddQueryMode Mode { get; set; }
        List<string> Operators { get; set; }
        List<string> IsTaggedAsList { get; set; }
        Enumerations.QueryOperator SubQueryAOperator { get; set; }
        Enumerations.QueryOperator SubQueryBOperator { get; set; }
        List<QueryTag> SubQueryA { get; set; }
        List<QueryTag> SubQueryB { get; set; }
        List<DlkTag> Tags { get; set; }
        QueryRow Row { get; set; }
        QueryCol Col { get; set; }
    }
}
