using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using CommonLib.DlkHandlers;

namespace TestRunner.Designer.Model
{
    public class QueryRow
    {
        private const string STR_XML_TAG_QUERY = "query";
        private const string STR_XML_TAG_QUERY_ROW = "qrow";
        private const string STR_XML_TAG_QUERY_COLUMN = "qcol";
        private const string STR_XML_TAG_QUERY_COLUMNS = "qcols";
        private const string STR_XML_TAG_QUERY_TAG = "qtag";
        private const string STR_XML_TAG = "tag";


        public static readonly string COL_NAME_DEFAULT = "Total";
        public static readonly int COL_IDX_DEFAULT = 0;


        public QueryRow(int RowIndex, string RowName, Enumerations.QueryRowColor RowColor)
        {
            Index = RowIndex;
            Name = RowName;
            Color = RowColor;
            TotalColumn = new QueryCol("-1", "TOTAL", -1, new List<QueryTag>(), new List<QueryCol>(), Enumerations.QueryOperator.And, 
                Enumerations.QueryTagType.SubQuery, false, 0);
            OutputList = new List<DlkTest>();
            OutputSuiteList = new List<TLSuite>();
            TotalValue = "-1";
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public List<QueryCol> QColumns { get; set; }
        public Enumerations.QueryRowColor Color { get; set; }
        public string TotalValue { get; set; }
        public QueryCol TotalColumn { get; set; }
        public List<DlkTest> OutputList { get; set; }
        public List<TLSuite> OutputSuiteList { get; set; }
    }

    public class QueryRowDisplay
    {
        public string Name { get; set; }

    }
}
