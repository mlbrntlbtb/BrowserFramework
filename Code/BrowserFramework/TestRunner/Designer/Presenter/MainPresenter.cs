using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using TestRunner.Common;
using TestRunner.Designer.Model;
using TestRunner.Designer.View;

namespace TestRunner.Designer.Presenter
{
    public class MainPresenter
    {
        #region PRIVATE MEMBERS
        private const int INT_MIN_DELAY = 1000;
        private const int INT_NORMAL_DELAY = 2000;
        private const string STR_XML_TAG_QUERY = "query";
        private const string STR_XML_TAG_QUERY_ROW = "qrow";
        private const string STR_XML_TAG_QUERY_COLUMN = "qcol";
        private const string STR_XML_TAG_QUERY_COLUMNS = "qcols";
        private const string STR_XML_TAG_QUERY_TAG = "qtag";
        private const string STR_XML_TAG = "tag";


        private IMainView mView = null;

        private static string mTagsFile = Path.Combine(DlkEnvironment.mDirFramework, @"Library\Tags\tags.xml");
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view"></param>
        public MainPresenter(IMainView view)
        {
            mView = view;
        }

        /// <summary>
        /// Loads all Object Store files and stores to memory
        /// </summary>
        public void LoadObjectStore()
        {

            DlkDynamicObjectStoreHandler objHandler = DlkDynamicObjectStoreHandler.Instance;
            objHandler.Initialize();
            Thread.Sleep(INT_NORMAL_DELAY);
            while (objHandler.StillLoading)
            {
                Thread.Sleep(INT_MIN_DELAY);
            }

            if (objHandler.OutDatedOS && DlkEnvironment.IsShowAppNameProduct)
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_OUTDATED_TEST_LIBRARY_OBJECTSTORE);
                Environment.Exit(0);
            }
            //List<DlkObjectStoreFileRecord> osfr = new List<DlkObjectStoreFileRecord>();
            //DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirObjectStore);
            //FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);
            //foreach (FileInfo mFile in mFiles)
            //{
            //    DlkObjectStoreFileRecord mOsfr = LoadObjectStoreRecord(mFile.FullName);
            //    osfr.Add(mOsfr);
            //}
            //mView.ObjectStoreFiles = osfr;
        }

        /// Loads all suites and stores to memory
        /// </summary>
        public void LoadSuites(string suiteDirectory)
        {
            DlkTestSuiteLoader SuiteLoader = new DlkTestSuiteLoader();
            mView.Suites = SuiteLoader.GetSuiteDirectories2(suiteDirectory);
        }

        /// <summary>
        /// Loads all tests and stores to memory
        /// </summary>
        public void LoadTests()
        {
            DlkKeywordTestsLoader TestsLoader = new DlkKeywordTestsLoader();
            mView.Tests = TestsLoader.GetKeywordDirectories(DlkEnvironment.mDirTests);
        }

        public void LoadQueryOfSelectedType()
        {
            List<string> output = new List<string>();
            switch (mView.CurrentQueryType)
            {
                case Enumerations.QueryType.Suite:
                    output = mQueries.FindAll(x => GetQueryType(x) == Enumerations.QueryType.Suite);
                    break;
                case Enumerations.QueryType.Test:
                    output = mQueries.FindAll(x => GetQueryType(x) == Enumerations.QueryType.Test);
                    break;
                default:
                    break;
            }
            mView.QueryList = output.ConvertAll(x => Path.GetFileName(x));
        }

        private Enumerations.QueryType GetQueryType(string filePath)
        {
            Enumerations.QueryType ret = Enumerations.QueryType.Invalid;
            try
            {
                var mXml = XDocument.Load(filePath);
                var qries = from doc in mXml.Descendants("query")
                            select new
                            {
                                type = doc.Attribute("type").Value.ToString(),
                            };


                var qry = qries.FirstOrDefault();
                if (qry.type == "Test")
                {
                    ret = Enumerations.QueryType.Test;
                }
                else if (qry.type == "Suite")
                {
                    ret = Enumerations.QueryType.Suite;
                }
            }
            catch
            {
                ret = Enumerations.QueryType.Invalid;
            }
            return ret;
        }


        private List<string> mQueries = new List<string>();
        public void LoadAllQueries()
        {
            DirectoryInfo di = Directory.CreateDirectory(Path.Combine(DlkEnvironment.mDirFramework, "Library", "Queries"));
            mQueries = new List<string>(Directory.GetFiles(di.FullName, "*.xml"));
            mQueries = mQueries.FindAll(x => IsValidQuery(Path.Combine(DlkEnvironment.mDirFramework, "Library", "Queries", x)));
        }

        private bool IsValidQuery(string path)
        {
            try
            {
                Query q = new Query(path, new List<DlkTest>());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void LoadQuery()
        {
            Query myQuery = null;

            if (mView.CurrentQueryType == Enumerations.QueryType.Test)
            {
                myQuery = new Query(mView.CurrentQueryPath, mView.AllTests);
                Dictionary<QueryRow, List<DlkTest>> outputList = new Dictionary<QueryRow, List<DlkTest>>();
            }
            else if (mView.CurrentQueryType == Enumerations.QueryType.Suite)
            {
                myQuery = new Query(mView.CurrentQueryPath, mView.AllSuites);
                Dictionary<QueryRow, List<TLSuite>> outputList = new Dictionary<QueryRow, List<TLSuite>>();
            }

            this.mView.QueryRows = myQuery.Rows;
            this.mView.QueryCols = myQuery.Columns;

            /* Construct data table --> source of datagrid */
            DataTable newdt = mView.CurrentQueryDataSource.Table;

            /* Add columns to dataview */
            for (int i = 0; i < myQuery.Columns.Count; i++)
            {
                newdt.Columns.Add(myQuery.Columns[i].Name, typeof(string)); // check if I can set as DataTemplateColumn
            }

            /* cache TOTAL column outputlist */
            foreach (QueryRow qryRow in myQuery.Rows)
            {
                string keyTotal = qryRow.Name + Query.STR_QUERYNAME_COLUMNINDEX_DELIMITER + "TOTAL";
                if (mView.AllQueryResultTest.ContainsKey(keyTotal))
                {
                    mView.AllQueryResultTest[keyTotal] = qryRow.OutputList;
                }
                else
                {
                    mView.AllQueryResultTest.Add(keyTotal, qryRow.OutputList);
                }
            }

            /* Set table values */
            Dictionary<int, Enumerations.QueryRowColor> clrMap = new Dictionary<int, Enumerations.QueryRowColor>();
            int cnt = 0;

            foreach (DataRow dr in newdt.Rows)
            {
                for (int i = 0; i < myQuery.Columns.Count; i++)
                {
                    if (dr[i + 3].ToString() == String.Empty)
                    {
                        List<DlkTest> dummy = new List<DlkTest>();
                        List<TLSuite> dummy2 = new List<TLSuite>();
                        if (myQuery.Columns[i].Type == Enumerations.QueryTagType.Column)
                        {
                            double rawValue = ComputeColumnValue(myQuery.Columns, i, myQuery.Rows[cnt].OutputList, dr);
                            if (myQuery.Columns[i].IsPercentage)
                            {
                                rawValue *= 100;
                                dr[i + 3] = rawValue.ToString("N" + myQuery.Columns[i].DecimalPlaces) + "%";
                            }
                            else
                            {
                                rawValue = Math.Round(rawValue, myQuery.Columns[i].DecimalPlaces);
                                dr[i + 3] = rawValue.ToString("N" + myQuery.Columns[i].DecimalPlaces);
                            }
                        }
                        else
                        {
                            if (mView.CurrentQueryType == Enumerations.QueryType.Test)
                            {
                                dr[i + 3] = myQuery.Columns[i].GetQueryColumnValue(myQuery.Rows[cnt].OutputList, out dummy);
                                string keyCol = myQuery.Rows[cnt].Name + Query.STR_QUERYNAME_COLUMNINDEX_DELIMITER + myQuery.Columns[i].Name;
                                if (mView.AllQueryResultTest.ContainsKey(keyCol))
                                {
                                    mView.AllQueryResultTest[keyCol] = dummy;
                                }
                                else
                                {
                                    mView.AllQueryResultTest.Add(keyCol, dummy);
                                }
                            }
                            else if (mView.CurrentQueryType == Enumerations.QueryType.Suite)
                            {
                                dr[i + 3] = myQuery.Columns[i].GetSuiteQueryColumnValue(myQuery.Rows[cnt].OutputSuiteList, out dummy2);
                            }
                        }
                    }
                }
                cnt++;
            }

            /* set dataview */
            mView.CurrentQuery = myQuery;
            mView.CurrentQueryDataSource = newdt.AsDataView();
            //mView.QueryColorMap = clrMap;
        }

        private double ComputeColumnValue(List<QueryCol> QueryColumns, int ColumnIndex, List<DlkTest> Tests, DataRow dr)
        {
            double colValue1, colValue2, finalValue = 0;
            List<DlkTest> dummy = new List<DlkTest>();
            string colId1 = QueryColumns[ColumnIndex].QCols[0].Id;
            string colId2 = QueryColumns[ColumnIndex].QCols[1].Id;
            QueryCol column1 = QueryColumns.Find(x => x.Id == colId1);
            QueryCol column2 = QueryColumns.Find(x => x.Id == colId2);
            if (colId1 == "-1") // operand is Total
            {
                double.TryParse(dr[2].ToString(), out colValue1);
            }
            else
            {
                if (dr[column1.Index + 3].ToString() == String.Empty) // operand column needs computation in advance
                {
                    if (column1.Type == Enumerations.QueryTagType.Column)
                    {
                        colValue1 = ComputeColumnValue(QueryColumns, column1.Index, Tests, dr);
                    }
                    else
                    {
                        double.TryParse(column1.GetQueryColumnValue(Tests, out dummy), out colValue1);
                    }
                }
                else
                {
                    double.TryParse(dr[column1.Index + 3].ToString().TrimEnd('%'), out colValue1); // just use existing column value
                    if (dr[column1.Index + 3].ToString().Contains('%'))
                    {
                        colValue1 /= 100;
                    }
                }
            }
            if (colId2 == "-1") // operand is Total
            {
                double.TryParse(dr[2].ToString(), out colValue2);
            }
            else
            {
                if (dr[column2.Index + 3].ToString() == String.Empty) // operand column needs computation in advance
                {
                    if (column2.Type == Enumerations.QueryTagType.Column)
                    {
                        colValue2 = ComputeColumnValue(QueryColumns, column2.Index, Tests, dr);
                    }
                    else
                    {
                        double.TryParse(column2.GetQueryColumnValue(Tests, out dummy), out colValue2);
                    }
                }
                else
                {
                    double.TryParse(dr[column2.Index + 3].ToString().TrimEnd('%'), out colValue2); // just use existing column value
                    if (dr[column2.Index + 3].ToString().Contains('%'))
                    {
                        colValue2 /= 100;
                    }
                }
            }
            switch (QueryColumns[ColumnIndex].Operator)
            {
                case Enumerations.QueryOperator.Sum:
                    finalValue = colValue1 + colValue2;
                    break;
                case Enumerations.QueryOperator.Difference:
                    finalValue = colValue1 - colValue2;
                    break;
                case Enumerations.QueryOperator.Product:
                    finalValue = colValue1 * colValue2;
                    break;
                case Enumerations.QueryOperator.Quotient:
                    if (colValue2 == 0)
                    {
                        return 0;
                    }
                    finalValue = colValue1 / colValue2;
                    break;
            }
            return finalValue;
        }

        public void NewQuery(string QueryPath)
        {
            if (!File.Exists(QueryPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(QueryPath));
                File.WriteAllText(QueryPath, "<query type=\""
                    + Enumerations.ConvertToString(mView.CurrentQueryType) + "\" />");

                LoadAllQueries();
                LoadQueryOfSelectedType();
                mView.CurrentQueryPath = QueryPath;
                LoadQuery();
            }
        }

        public void SaveAsQuery(string QueryPath)
        {
            if (!File.Exists(QueryPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(QueryPath));

                File.Copy(mView.CurrentQueryPath, QueryPath);
                LoadAllQueries();
                LoadQueryOfSelectedType();
                mView.CurrentQueryPath = QueryPath;
                LoadQuery();
            }
        }

        public void AddEditQueryRow(QueryRow queryRow, QueryCol queryCol, Enumerations.AddQueryMode queryUpdateMode)
        {
            XDocument doc = XDocument.Load(mView.CurrentQueryPath);

            Enumerations.QueryType queryType = (Enumerations.QueryType)Enum.Parse(typeof(Enumerations.QueryType),
                doc.Root.Attribute("type").Value.ToString());

            switch (queryUpdateMode)
            {
                case Enumerations.AddQueryMode.AddNewQuery:

                    break;
            }
        }

        public void DeleteQuery()
        {
            File.Delete(mView.CurrentQueryPath);
            LoadAllQueries();
            LoadQueryOfSelectedType();
        }

        public void SaveQuery()
        {
            if (mView.CurrentQuery != null)
            {
                mView.CurrentQuery.Save();
            }
        }

        public void DeleteRow(int index)
        {
            mView.QueryRows.RemoveAt(index);
        }

        public void DeleteColumn(int index)
        {
            mView.QueryCols.RemoveAt(index);
        }

        public void AddNewTags()
        {
            mView.Tags.AddRange(mView.TagsToAdd); // Add
            mView.TagsToAdd.Clear(); // Flush
            SaveTags(); // Write
        }

        public void SaveTags()
        {
            DlkTag.SaveTags(mView.Tags);
        }

        public void LoadTags()
        {
            mView.Tags = DlkTag.LoadAllTags();
        }

        /// <summary>
        /// Retrieves the selected directory filter
        /// </summary>
        /// <returns>Directory filter from the xml file</returns>
        public string RetrieveDirectoryFilter()
        {
            string filter = string.Empty;
            try
            {
                /* Check if directory file exists */
                if (File.Exists(DlkEnvironment.mDirFramework + "Library\\Directory\\Directory.xml"))
                {
                    XDocument doc = XDocument.Load(DlkEnvironment.mDirFramework + "Library\\Directory\\Directory.xml");
                    var query = doc.Descendants("directory").Select(s => new
                    {
                        PATH = s.Element("path").Value
                    }).FirstOrDefault();
                    filter = query.PATH;
                }
                else
                {
                    filter = DlkEnvironment.mDirTestSuite;
                }
            }
            catch
            {
                // let error and ignore
            }
            return filter;
        }

        /// <summary>
        /// Retrieves the selected directory filter
        /// </summary>
        /// <returns>Directory filter from the xml file</returns>
        public string RetrieveDirectoryFilter(string xmlDirectoryFile)
        {
            string filter = string.Empty;
            try
            {
                /* Check if directory file exists */
                if (File.Exists(xmlDirectoryFile))
                {
                    XDocument doc = XDocument.Load(xmlDirectoryFile);
                    var query = doc.Descendants("directory").Select(s => new
                    {
                        PATH = s.Element("path").Value
                    }).FirstOrDefault();
                    filter = query.PATH;
                }
                else
                {
                    /*Creates the directory and xml file if file and folder does not exist */
                    string xmlNewPath = xmlDirectoryFile.Remove(xmlDirectoryFile.IndexOf("\\Framework\\Library\\Directory\\Directory.xml")) + "\\Suites";

                    SaveDirectory(xmlNewPath, xmlDirectoryFile);

                    filter = RetrieveDirectoryFilter(xmlDirectoryFile);
                }
            }
            catch
            {
                // let error and ignore
            }
            return filter;
        }

        /// <summary>
        /// Save directory to xml file as a reference when TL is loaded again
        /// </summary>
        /// <param name="DirectoryFilter">Selected suite directory filter</param>
        /// <param name="XmlFilePath">file path where the selected suite directory will be stored</param>
        public void SaveDirectory(string DirectoryFilter, string XmlFilePath)
        {
            /* Ensure directory file exists */
            if (!File.Exists(XmlFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(XmlFilePath));
                File.WriteAllText(XmlFilePath, "<directory />");
            }

            XElement mRoot = new XElement("directory",
                new XElement("path", DirectoryFilter));

            File.SetAttributes(XmlFilePath, FileAttributes.Normal);
            FileIOPermission fPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, XmlFilePath);

            mRoot.Save(XmlFilePath);
        }

        /// <summary>
        /// Update view of query results list
        /// </summary>
        /// <param name="Key">Key of selected query row/column combination saved in cache</param>
        public void UpdateSelectedQueryResult(string Key)
        {
            if (mView.CurrentQueryType == Enumerations.QueryType.Test)
            {
                bool bExist = mView.AllQueryResultTest.ContainsKey(Key);
                mView.QueryResultsTest = bExist ? mView.AllQueryResultTest[Key] : null;
                mView.QueryResultsTitle = bExist ? Key + " (" + mView.AllQueryResultTest[Key].Count + ")" : string.Empty;
            }
            else if (mView.CurrentQueryType == Enumerations.QueryType.Suite)
            {
                // STUB for Suites
            }
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Reads the object store file data into a record
        /// </summary>
        /// <param name="mFilePath"></param>
        /// <returns></returns>
        private DlkObjectStoreFileRecord LoadObjectStoreRecord(String mFilePath)
        {
            FileInfo mFile = new FileInfo(mFilePath);
            DlkObjectStoreFileRecord mOsfr = new DlkObjectStoreFileRecord();
            mOsfr.mFile = mFile.FullName;
            XDocument mXmlDoc = XDocument.Load(mFile.FullName);
            //Get Screen
            var data = from doc in mXmlDoc.Descendants("objectstore")
                       select new
                       {
                           screen = doc.Attribute("screen").Value
                       };
            foreach (var val in data)
            {
                mOsfr.mScreen = val.screen;
            }
            //Get Controls
            List<DlkObjectStoreFileControlRecord> mCntrls = new List<DlkObjectStoreFileControlRecord>();
            var dataCtrls = from doc in mXmlDoc.Descendants("control")
                            select new
                            {
                                key = doc.Attribute("key").Value,
                                type = doc.Element("controltype").Value,
                                searchmethod = doc.Element("searchmethod").Value,
                                searchparams = doc.Element("searchparameters").Value
                            };
            foreach (var val in dataCtrls)
            {
                DlkObjectStoreFileControlRecord cr = new DlkObjectStoreFileControlRecord();
                cr.mKey = val.key;
                cr.mControlType = val.type;
                cr.mSearchMethod = val.searchmethod;
                cr.mSearchParameters = val.searchparams;
                mCntrls.Add(cr);
            }
            mOsfr.mControls = mCntrls;
            return mOsfr;
        }

        //private string GetQueryColumnValue(List<DlkTest> InitialTestList, QueryCol column, out List<DlkTest> Output)
        //{
        //    string ret = string.Empty;
        //    List<DlkTest> me = InitialTestList;
        //    Output = new List<DlkTest>();

        //    switch(column.Operator)
        //    {
        //        case Enumerations.QueryOperator.And:
        //            foreach (QueryTag qt in column.QTags)
        //            {
        //                if (qt.Is)
        //                {
        //                    me = me.FindAll(x => x.mTags.Any(y => y.Id == qt.TagId));
        //                }
        //                else
        //                {
        //                    me = me.FindAll(x => !x.mTags.Any(y => y.Id == qt.TagId));
        //                }
        //            }
        //            Output = me;
        //            ret = me.Count.ToString();
        //            break;
        //        case Enumerations.QueryOperator.Or:
        //            List<DlkTest> now = me;
        //            foreach (QueryTag qt in column.QTags)
        //            {
        //                if (qt.Is)
        //                {
        //                    now = now.Union(me.FindAll(x => x.mTags.Any(y => y.Id == qt.TagId))).ToList();
        //                }
        //                else
        //                {
        //                    now = now.Union(me.FindAll(x => !x.mTags.Any(y => y.Id == qt.TagId))).ToList();
        //                }
        //            }
        //            Output = me;
        //            ret = now.Count.ToString();
        //            break;
        //    }
        //    return ret;
        //}

        #endregion
    }


}
