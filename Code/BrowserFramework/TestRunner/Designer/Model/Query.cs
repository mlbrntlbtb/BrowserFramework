using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestRunner.Designer.Model
{
    public class Query
    {
        private const string STR_XML_TAG_QUERY = "query";
        private const string STR_XML_TAG_QUERY_ROW = "qrow";
        private const string STR_XML_TAG_QUERY_COLUMN = "qcol";
        private const string STR_XML_TAG_QUERY_COLUMNS = "qcols";
        private const string STR_XML_TAG_QUERY_TAG = "qtag";
        private const string STR_XML_TAG_QUERY_SUBCOLUMN = "subqcol";
        private const string STR_XML_TAG = "tag";

        public const string STR_QUERYNAME_COLUMNINDEX_DELIMITER = "|";

        public Query(string QueryPath, List<DlkTest> TestList)
        {
            Path = QueryPath;

            XDocument doc = XDocument.Load(Path);

            Dictionary<QueryRow, List<DlkTest>> outputList = new Dictionary<QueryRow, List<DlkTest>>();

            Type = (Enumerations.QueryType)Enum.Parse(typeof(Enumerations.QueryType),
                doc.Root.Attribute("type").Value.ToString());

            var rows = from row in doc.Descendants(STR_XML_TAG_QUERY_ROW)
                       select new
                       {
                           index = int.Parse(row.Attribute("index").Value),
                           name = row.Attribute("name").Value.ToString(),
                           color = row.Attribute("color").Value.ToString(),
                           totalcol = row.Element(STR_XML_TAG_QUERY_COLUMN)
                       };

            /* Get Rows */
            List<QueryRow> qryRows = new List<QueryRow>();

            foreach (var itm in rows)
            {
                Enumerations.QueryRowColor clr = (Enumerations.QueryRowColor)Enum.Parse(typeof(Enumerations.QueryRowColor), itm.color);
                QueryRow qry = new QueryRow(itm.index, itm.name, clr);

                /* TOTAL column */
                string totalColName = itm.totalcol.Attribute("name").Value.ToString();
                int totalColIndex = int.Parse(itm.totalcol.Attribute("index").Value);
                Enumerations.QueryOperator totalColOperator = (Enumerations.QueryOperator)Enum.Parse(typeof(Enumerations.QueryOperator),
                    itm.totalcol.Attribute("operator").Value);

                var totaltags = from col in itm.totalcol.Descendants(STR_XML_TAG_QUERY_TAG)
                                select new
                                {
                                    tagid = col.Attribute("id").Value.ToString(),
                                    tagis = col.Attribute("is").Value.ToString(),
                                    tagindex = int.Parse(col.Attribute("colindex").Value),
                                    tagcontains = col.Attribute("contains").Value.ToString()
                                };


                List<QueryTag> lstTotalQTags = new List<QueryTag>();
                foreach (var subitm in totaltags)
                {
                    QueryTag totalqtag;
                    if (subitm.tagis == String.Empty)
                    {
                        totalqtag = new QueryTag(subitm.tagid, null, subitm.tagindex, subitm.tagcontains);
                    }
                    else
                    {
                        totalqtag = new QueryTag(subitm.tagid, Convert.ToBoolean(subitm.tagis), subitm.tagindex, null);
                    }
                    lstTotalQTags.Add(totalqtag);
                    totalqtag.ListPosition = lstTotalQTags.IndexOf(totalqtag);
                }

                var totalsubcols = from col in itm.totalcol.Descendants(STR_XML_TAG_QUERY_SUBCOLUMN)
                                select new
                                {
                                    scolid = col.Attribute("id").Value.ToString(),
                                    scolname = col.Attribute("name").Value.ToString()
                                };


                List<QueryCol> lstTotalSubQCols = new List<QueryCol>();
                foreach (var subitm in totalsubcols)
                {
                    QueryCol totalsubqcol = new QueryCol(subitm.scolid, subitm.scolname);
                    lstTotalSubQCols.Add(totalsubqcol);
                }

                QueryCol totalCol = new QueryCol("-1", totalColName, totalColIndex, lstTotalQTags, lstTotalSubQCols, totalColOperator, 
                    Enumerations.QueryTagType.SubQuery, false, 0);
                List<DlkTest> myOutput = new List<DlkTest>();

                qry.TotalColumn = totalCol;
                qry.TotalValue = totalCol.GetQueryColumnValue(TestList, out myOutput);
                qry.OutputList = myOutput;
                //outputList.Add(qry, myOutput);
                qryRows.Add(qry);
            }

            Rows = qryRows;

            /* Get Columns */
            var cols = from col in doc.Descendants(STR_XML_TAG_QUERY_COLUMNS).Descendants(STR_XML_TAG_QUERY_COLUMN)
                       select new
                       {
                           index = int.Parse(col.Attribute("index").Value),
                           id = col.Attribute("id").Value.ToString(),
                           name = col.Attribute("name").Value.ToString(),
                           optr = col.Attribute("operator").Value.ToString(),
                           type = col.Attribute("type").Value.ToString(),
                           percentage = bool.Parse(col.Attribute("percentage").Value.ToString()),
                           decimalplaces = int.Parse(col.Attribute("decimalplaces").Value.ToString()),
                           qsubcols = col.Elements(STR_XML_TAG_QUERY_SUBCOLUMN),
                           qtags = col.Elements(STR_XML_TAG_QUERY_TAG)
                       };


            /* Add Query Name column */
            List<QueryCol> qryCols = new List<QueryCol>();
            foreach (var itm in cols)
            {
                List<QueryCol> lstSubQCols = new List<QueryCol>();
                List<QueryTag> lstQTags = new List<QueryTag>();
                if (itm.type == "SubQuery")
                {
                    foreach (XElement xe in itm.qtags)
                    {
                        QueryTag qt;
                        if (xe.Attribute("is").Value.ToString() == String.Empty)
                        {
                            qt = new QueryTag(xe.Attribute("id").Value.ToString(),
                            null,
                            int.Parse(xe.Attribute("colindex").Value),
                            xe.Attribute("contains").Value.ToString());
                        }
                        else
                        {
                            qt = new QueryTag(xe.Attribute("id").Value.ToString(),
                            Convert.ToBoolean(xe.Attribute("is").Value),
                            int.Parse(xe.Attribute("colindex").Value),
                            xe.Attribute("contains").Value.ToString());
                        }
                        lstQTags.Add(qt);
                        qt.ListPosition = lstQTags.IndexOf(qt);
                    }
                }
                else if (itm.type == "Column")
                {

                    foreach (XElement xe in itm.qsubcols)
                    {
                        QueryCol sqc = new QueryCol(xe.Attribute("id").Value.ToString(),
                            xe.Attribute("name").Value.ToString());
                        lstSubQCols.Add(sqc);
                    }
                }
                Enumerations.QueryOperator colOperator = (Enumerations.QueryOperator)Enum.Parse(typeof(Enumerations.QueryOperator), itm.optr);
                Enumerations.QueryTagType colType = (Enumerations.QueryTagType)Enum.Parse(typeof(Enumerations.QueryTagType), itm.type);

                qryCols.Add(new QueryCol(itm.id, itm.name, itm.index, lstQTags, lstSubQCols, colOperator, colType, itm.percentage, itm.decimalplaces));
            }
            Columns = qryCols;
        }

        public Query(string QueryPath, List<TLSuite> SuiteList)
        {
            Path = QueryPath;

            XDocument doc = XDocument.Load(Path);

            Dictionary<QueryRow, List<DlkTestSuiteInfoRecord>> outputList = new Dictionary<QueryRow, List<DlkTestSuiteInfoRecord>>();

            Type = (Enumerations.QueryType)Enum.Parse(typeof(Enumerations.QueryType),
                doc.Root.Attribute("type").Value.ToString());

            var rows = from row in doc.Descendants(STR_XML_TAG_QUERY_ROW)
                       select new
                       {
                           index = int.Parse(row.Attribute("index").Value),
                           name = row.Attribute("name").Value.ToString(),
                           color = row.Attribute("color").Value.ToString(),
                           totalcol = row.Element(STR_XML_TAG_QUERY_COLUMN)
                       };

            /* Get Rows */
            List<QueryRow> qryRows = new List<QueryRow>();

            foreach (var itm in rows)
            {
                Enumerations.QueryRowColor clr = (Enumerations.QueryRowColor)Enum.Parse(typeof(Enumerations.QueryRowColor), itm.color);
                QueryRow qry = new QueryRow(itm.index, itm.name, clr);

                /* TOTAL column */
                string totalColName = itm.totalcol.Attribute("name").Value.ToString();
                int totalColIndex = int.Parse(itm.totalcol.Attribute("index").Value);
                Enumerations.QueryOperator totalColOperator = (Enumerations.QueryOperator)Enum.Parse(typeof(Enumerations.QueryOperator),
                    itm.totalcol.Attribute("operator").Value);

                var totaltags = from col in itm.totalcol.Descendants(STR_XML_TAG_QUERY_TAG)
                                select new
                                {
                                    tagid = col.Attribute("id").Value.ToString(),
                                    tagis = col.Attribute("is").Value.ToString(),
                                    tagindex = int.Parse(col.Attribute("colindex").Value),
                                    tagcontains = col.Attribute("contains").Value.ToString()
                                };


                List<QueryTag> lstTotalQTags = new List<QueryTag>();
                foreach (var subitm in totaltags)
                {
                    QueryTag totalqtag;
                    if (subitm.tagis == String.Empty)
                    {
                        totalqtag = new QueryTag(subitm.tagid, null, subitm.tagindex, subitm.tagcontains);
                    }
                    else
                    {
                        totalqtag = new QueryTag(subitm.tagid, Convert.ToBoolean(subitm.tagis), subitm.tagindex, null);
                    }
                    lstTotalQTags.Add(totalqtag);
                    totalqtag.ListPosition = lstTotalQTags.IndexOf(totalqtag);
                }

                var totalsubcols = from col in itm.totalcol.Descendants(STR_XML_TAG_QUERY_SUBCOLUMN)
                                   select new
                                   {
                                       scolid = col.Attribute("id").Value.ToString(),
                                       scolname = col.Attribute("name").Value.ToString()
                                   };


                List<QueryCol> lstTotalSubQCols = new List<QueryCol>();
                foreach (var subitm in totalsubcols)
                {
                    QueryCol totalsubqcol = new QueryCol(subitm.scolid, subitm.scolname);
                    lstTotalSubQCols.Add(totalsubqcol);
                }

                QueryCol totalCol = new QueryCol("-1", totalColName, totalColIndex, lstTotalQTags, lstTotalSubQCols, totalColOperator,
                    Enumerations.QueryTagType.SubQuery, false, 0);
                List<TLSuite> myOutput = new List<TLSuite>();

                qry.TotalColumn = totalCol;
                qry.TotalValue = totalCol.GetSuiteQueryColumnValue(SuiteList, out myOutput);
                qry.OutputSuiteList = myOutput;
                qryRows.Add(qry);
            }

            Rows = qryRows;

            /* Get Columns */
            var cols = from col in doc.Descendants(STR_XML_TAG_QUERY_COLUMNS).Descendants(STR_XML_TAG_QUERY_COLUMN)
                       select new
                       {
                           index = int.Parse(col.Attribute("index").Value),
                           id = col.Attribute("id").Value.ToString(),
                           name = col.Attribute("name").Value.ToString(),
                           optr = col.Attribute("operator").Value.ToString(),
                           type = col.Attribute("type").Value.ToString(),
                           percentage = bool.Parse(col.Attribute("percentage").Value.ToString()),
                           decimalplaces = int.Parse(col.Attribute("decimalplaces").Value.ToString()),
                           qsubcols = col.Elements(STR_XML_TAG_QUERY_SUBCOLUMN),
                           qtags = col.Elements(STR_XML_TAG_QUERY_TAG)
                       };


            /* Add Query Name column */
            List<QueryCol> qryCols = new List<QueryCol>();
            foreach (var itm in cols)
            {
                List<QueryCol> lstSubQCols = new List<QueryCol>();
                List<QueryTag> lstQTags = new List<QueryTag>();
                if (itm.type == "SubQuery")
                {
                    foreach (XElement xe in itm.qtags)
                    {
                        QueryTag qt;
                        if (xe.Attribute("is").Value.ToString() == String.Empty)
                        {
                            qt = new QueryTag(xe.Attribute("id").Value.ToString(),
                            null,
                            int.Parse(xe.Attribute("colindex").Value),
                            xe.Attribute("contains").Value.ToString());
                        }
                        else
                        {
                            qt = new QueryTag(xe.Attribute("id").Value.ToString(),
                            Convert.ToBoolean(xe.Attribute("is").Value),
                            int.Parse(xe.Attribute("colindex").Value),
                            xe.Attribute("contains").Value.ToString());
                        }
                        lstQTags.Add(qt);
                        qt.ListPosition = lstQTags.IndexOf(qt);
                    }
                }
                else if (itm.type == "Column")
                {

                    foreach (XElement xe in itm.qsubcols)
                    {
                        QueryCol sqc = new QueryCol(xe.Attribute("id").Value.ToString(),
                            xe.Attribute("name").Value.ToString());
                        lstSubQCols.Add(sqc);
                    }
                }
                Enumerations.QueryOperator colOperator = (Enumerations.QueryOperator)Enum.Parse(typeof(Enumerations.QueryOperator), itm.optr);
                Enumerations.QueryTagType colType = (Enumerations.QueryTagType)Enum.Parse(typeof(Enumerations.QueryTagType), itm.type);

                qryCols.Add(new QueryCol(itm.id, itm.name, itm.index, lstQTags, lstSubQCols, colOperator, colType, itm.percentage, itm.decimalplaces));
            }
            Columns = qryCols;
        }

        public void Save()
        {
            List<XElement> lstQRows = new List<XElement>();
            foreach (QueryRow qr in Rows)
            {
                List<XElement> lstTotalQTags = new List<XElement>();
                foreach (QueryTag qt in qr.TotalColumn.QTags)
                {
                    XElement totalQtag = new XElement("qtag", 
                        new XAttribute("id", qt.TagId),
                        new XAttribute("is", qt.Is == null ? String.Empty : qt.Is.ToString()), 
                        new XAttribute("colindex", qt.TargetColumnIndex.ToString()),
                        new XAttribute("contains", qt.Contains == null ? String.Empty : qt.Contains.ToString())
                        );
                    lstTotalQTags.Add(totalQtag);

                }
                XElement totalColumn = new XElement("qcol",
                    new XAttribute("index", qr.TotalColumn.Index.ToString()),
                    new XAttribute("id", "-1"),
                    new XAttribute("name", qr.TotalColumn.Name),
                    new XAttribute("operator", Enumerations.ConvertToString(qr.TotalColumn.Operator)),
                    new XAttribute("type", Enumerations.ConvertToString(qr.TotalColumn.Type)),
                    new XAttribute("percentage", bool.FalseString),
                    new XAttribute("decimalplaces", "0"),


                    lstTotalQTags
                    );

                XElement qrow = new XElement("qrow",
                    new XAttribute("index", qr.Index.ToString()),
                    new XAttribute("name", qr.Name),
                    new XAttribute("color", Enumerations.ConvertToString(qr.Color)),
                    totalColumn
                    );
                lstQRows.Add(qrow);
            }

            XElement qrows = new XElement("qrows", lstQRows);

            List<XElement> lstQCols = new List<XElement>();
            foreach (QueryCol qc in Columns)
            {
                List<XElement> qsubelems = new List<XElement>();
                if (qc.Type == Enumerations.QueryTagType.SubQuery)
                {
                    

                    foreach (QueryTag qt in qc.QTags)
                    {
                        XElement qtag = new XElement("qtag",
                            new XAttribute("id", qt.TagId),
                            new XAttribute("is", qt.Is == null ? String.Empty : qt.Is.ToString()),
                            new XAttribute("colindex", qt.TargetColumnIndex.ToString()),
                            new XAttribute("contains", qt.Contains == null ? String.Empty : qt.Contains.ToString())
                            );
                        qsubelems.Add(qtag);
                    }
                }

                else if (qc.Type == Enumerations.QueryTagType.Column)
                {
                    foreach (QueryCol sqc in qc.QCols)
                    {
                        XElement subqcol = new XElement("subqcol",
                            new XAttribute("id", sqc.Id),
                            new XAttribute("name", sqc.Name),
                            new XAttribute("index", sqc.Index.ToString())
                            );
                        qsubelems.Add(subqcol);
                    }
                }

                XElement qcol = new XElement("qcol",
                    new XAttribute("index", qc.Index.ToString()),
                    new XAttribute("id", qc.Id),
                    new XAttribute("name", qc.Name),
                    new XAttribute("operator", Enumerations.ConvertToString(qc.Operator)),
                    new XAttribute("type", Enumerations.ConvertToString(qc.Type)),
                    new XAttribute("percentage", qc.IsPercentage.ToString()),
                    new XAttribute("decimalplaces", qc.DecimalPlaces.ToString()),
                    qsubelems
                    );
                lstQCols.Add(qcol);
            }
            XElement qcols = new XElement("qcols", lstQCols);

            XElement root = new XElement("query",
                new XAttribute("type", Enumerations.ConvertToString(Type)),
                qrows,
                qcols
                );

            XDocument xdoc = new XDocument(root);
            xdoc.Save(Path);
        }

        public string Path { get; set; }
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(Path);
            }
        }

        public List<QueryRow> Rows { get; set; }
        public List<QueryCol> Columns { get; set; }
        public Enumerations.QueryType Type { get; set; }
    }
}
