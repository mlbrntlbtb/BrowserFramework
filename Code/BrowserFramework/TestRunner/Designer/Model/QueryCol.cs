using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkHandlers;

namespace TestRunner.Designer.Model
{
    public class QueryCol
    {
        public QueryCol(string ColId, string ColName, int ColIndex, List<QueryTag> QTagList, List<QueryCol> QColList, Enumerations.QueryOperator ColOperator, 
            Enumerations.QueryTagType ColType, bool ColPercentage, int ColDecimalPlaces)
        {
            Id = ColId;
            Name = ColName;
            Index = ColIndex;
            QTags = QTagList;
            QCols = QColList;
            Operator = ColOperator;
            Type = ColType;
            IsPercentage = ColPercentage;
            DecimalPlaces = ColDecimalPlaces;
        }

        public QueryCol(string ColId, string ColName)
        {
            Id = ColId;
            Name = ColName;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public List<QueryTag> QTags { get; set; }
        public List<QueryCol> QCols { get; set; }
        public Enumerations.QueryOperator Operator { get; set; }
        public Enumerations.QueryTagType Type { get; set; }
        public bool IsPercentage { get; set; }
        public int DecimalPlaces { get; set; }

        public string GetQueryColumnValue(List<DlkTest> InitialList, out List<DlkTest> Output)
        {
            string ret = string.Empty;
            List<DlkTest> me = InitialList;
            Output = new List<DlkTest>();

            switch (this.Operator)
            {
                case Enumerations.QueryOperator.And:
                    foreach (QueryTag qt in this.QTags)
                    {
                        if (qt.Is == null)
                        {
                            me = me.FindAll(x => x.mTags.Any(y => y.Name.Contains(qt.Contains)));
                        }
                        else if ((bool)qt.Is)
                        {
                            me = me.FindAll(x => x.mTags.Any(y => y.Id == qt.TagId));
                        }
                        else
                        {
                            me = me.FindAll(x => !x.mTags.Any(y => y.Id == qt.TagId));
                        }
                    }
                    Output = me;
                    ret = me.Count.ToString();
                    break;
                case Enumerations.QueryOperator.Or:
                    List<DlkTest> now = new List<DlkTest>();
                    foreach (QueryTag qt in this.QTags)
                    {
                        //if (this.QTags.IndexOf(qt) == 0)
                        //{
                        //    now = qt.Is ? me.FindAll(x => x.mTags.Any(y => y.Id == qt.TagId)) : me.FindAll(x => !x.mTags.Any(y => y.Id == qt.TagId));
                        //    continue;
                        //}
                        if (qt.Is == null)
                        {
                            now = now.Union(me.FindAll(x => x.mTags.Any(y => y.Name.Contains(qt.Contains)))).ToList();
                        }
                        else if ((bool)qt.Is)
                        {
                            now = now.Union(me.FindAll(x => x.mTags.Any(y => y.Id == qt.TagId))).ToList();
                        }
                        else
                        {
                            now = now.Union(me.FindAll(x => !x.mTags.Any(y => y.Id == qt.TagId))).ToList();
                        }
                    }
                    Output = now;
                    ret = now.Count.ToString();
                    break;
                case Enumerations.QueryOperator.Sum:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;
                case Enumerations.QueryOperator.Difference:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;
                case Enumerations.QueryOperator.Product:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;

                case Enumerations.QueryOperator.Quotient:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;
            }
            return ret;
        }

        public string GetSuiteQueryColumnValue(List<TLSuite> InitialList, out List<TLSuite> Output)
        {
            string ret = string.Empty;
            List<TLSuite> me = InitialList;
            Output = new List<TLSuite>();

            switch (this.Operator)
            {
                case Enumerations.QueryOperator.And:
                    foreach (QueryTag qt in this.QTags)
                    {
                        if (qt.Is == null)
                        {
                            me = me.FindAll(x => x.SuiteInfo.Tags.Any(y => y.Name.Contains(qt.Contains)));
                        }
                        else if ((bool)qt.Is)
                        {
                            me = me.FindAll(x => x.SuiteInfo.Tags.Any(y => y.Id == qt.TagId));
                        }
                        else
                        {
                            me = me.FindAll(x => !x.SuiteInfo.Tags.Any(y => y.Id == qt.TagId));
                        }
                    }
                    Output = me;
                    ret = me.Count.ToString();
                    break;
                case Enumerations.QueryOperator.Or:
                    List<TLSuite> now = new List<TLSuite>();
                    foreach (QueryTag qt in this.QTags)
                    {
                        if (qt.Is == null)
                        {
                            now = now.Union(me.FindAll(x => x.SuiteInfo.Tags.Any(y => y.Name.Contains(qt.Contains)))).ToList();
                        }
                        else if ((bool)qt.Is)
                        {
                            now = now.Union(me.FindAll(x => x.SuiteInfo.Tags.Any(y => y.Id == qt.TagId))).ToList();
                        }
                        else
                        {
                            now = now.Union(me.FindAll(x => !x.SuiteInfo.Tags.Any(y => y.Id == qt.TagId))).ToList();
                        }
                    }
                    Output = now;
                    ret = now.Count.ToString();
                    break;
                case Enumerations.QueryOperator.Sum:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;
                case Enumerations.QueryOperator.Difference:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;
                case Enumerations.QueryOperator.Product:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;

                case Enumerations.QueryOperator.Quotient:
                    if (this.QTags.Count != 2)
                    {

                    }
                    break;
            }
            return ret;
        }
    }
}
