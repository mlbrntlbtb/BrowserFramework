using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;
using TestRunner.Common;
using TestRunner.AdvancedScheduler.View;
using TestRunner.AdvancedScheduler.Model;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Media;

namespace TestRunner.AdvancedScheduler.Presenter
{
    public class ControllerPresenter
    {
        private IControllerView mView = null;
        private String mFavoritesDocumentPath = DlkEnvironment.mDirProductsRoot + @"\Common\Scheduler\Favorites.xml";

        public ControllerPresenter(IControllerView View)
        {
            mView = View;
        }

        /// <summary>
        /// Add a new testsuite to the test lineup
        /// </summary>
        /// <param name="ts"></param>
        public void AddToLineup(TestSuite ts, string library = null, string product = null, Agent agent = null)
        {
            if (library == null)
                library = DlkEnvironment.mLibrary;
            if (product == null)
                product = DlkTestRunnerSettingsHandler.ApplicationUnderTest.Name;

            string defaultBrowser = DlkEnvironment.GetDefaultBrowserNameOrIndex(true);

            TestLineupRecord suiteToAdd = new TestLineupRecord
            {
                Id = Guid.NewGuid().ToString(),
                Recurrence = Enumerations.RecurrenceType.Once,
                Schedule = DateTime.Today,
                RunningAgent = agent,
                Environment = TestLineupRecord.DEFAULT_ENVIRONMENT,
                Browser = new DlkBrowser(defaultBrowser == "" ? TestLineupRecord.DEFAULT_ENVIRONMENT : defaultBrowser),
                //give it 2 seconds delay to make sure it don't conflict with test queues
                StartTime = DateTime.Now.AddSeconds(-2), 
                Status = Enumerations.TestStatus.Ready,
                TestSuiteToRun = ts,
                DistributionList = new List<string>(),
                NumberOfRuns = 1,
                Enabled = true,
                Execute = true,
                Dependent = "false",
                ExecuteDependencyResult="",
                ExecuteDependencySuiteRow="",
                Library = library,
                Product = product,
                GroupID = string.Empty,
                GroupImage = string.Empty,
                RowColor = Color.FromArgb(255, 255, 255, 255),
                PostExecutionScripts = new List<ExternalScript>(),
                PreExecutionScripts = new List<ExternalScript>()
            };
            mView.TestLineup.Add(suiteToAdd);
            AddLastSuiteResultToTestLineupRecord(suiteToAdd);
        }

        /// <summary>
        /// Set the readonly property of schedules file
        /// </summary>
        /// <param name="isReadonly"></param>
        public void SetSchedulesFileReadOnly(bool isReadonly)
        {
            if (File.Exists(DlkAdvancedSchedulerFileHandler.SchedulesFilePath))
            {
                FileInfo fi = new FileInfo(DlkAdvancedSchedulerFileHandler.SchedulesFilePath);
                fi.IsReadOnly = isReadonly;
            }
        }

        /// <summary>
        /// Check if schedulesfilepath is readonly
        /// </summary>
        /// <returns></returns>
        public bool IsSchedulesFilePathReadOnly()
        {
            if (File.Exists(DlkAdvancedSchedulerFileHandler.SchedulesFilePath))
            {
                FileInfo fi = new FileInfo(DlkAdvancedSchedulerFileHandler.SchedulesFilePath);
                return fi.IsReadOnly;
            }

            return false;
        }

        /// <summary>
        /// Load the test lineup from xml file
        /// </summary>
        public void LoadLineup()
        {
            mView.TestLineup.Clear();
            mView.TestLineup = new ObservableCollection<TestLineupRecord>(DlkAdvancedSchedulerFileHandler.GetSchedulesLineup());
            RefreshLineUpImage();
            foreach (var item in mView.TestLineup)
            {
                AddLastSuiteResultToTestLineupRecord(item);
            }
        }

        /// <summary>
        /// Save the test lineup to xml file
        /// </summary>
        public void SaveLineup()
        {
            try
            {
                string schedulesFilePath = DlkAdvancedSchedulerFileHandler.SchedulesFilePath;
                XDocument xDoc = DlkAdvancedSchedulerFileHandler.GetSchedulerFileDocument(mView.TestLineup);
                XDocument refDoc = File.Exists(schedulesFilePath) ? XDocument.Load(schedulesFilePath) : null;

                //if current lineup is different from saved file: save
                if (IsSchedulerChanged(refDoc == null ? string.Empty : refDoc.ToString(), xDoc.ToString()))
                {
                    mView.IsSaveInProgress = true;
                    xDoc.Save(schedulesFilePath);
                    File.Copy(schedulesFilePath, DlkAdvancedSchedulerFileHandler.SchedulesFileGhost, true);
                }
                mView.IsSaveInProgress = false;
            }
            catch
            {
                /* ensure flag is set to false, even if IO exception caught in Save/Copy */
                mView.IsSaveInProgress = false;

            }
        }

        /// <summary>
        /// Updates group image in collection
        /// </summary>
        public void RefreshLineUpImage()
        {
            foreach (var lineUp in mView.TestLineup)
            {
                if (lineUp.GroupID == string.Empty)
                {
                    lineUp.GroupImage = string.Empty;
                }
                else
                {
                    var firstInGroup = mView.TestLineup.FirstOrDefault(x => x.GroupID == lineUp.GroupID);
                    var lastInGroup = mView.TestLineup.LastOrDefault(x => x.GroupID == lineUp.GroupID);

                    if (firstInGroup != null && lastInGroup != null && firstInGroup != lastInGroup)
                    {
                        if(lineUp == firstInGroup)
                            lineUp.GroupImage = "pack://siteoforigin:,,,/Resources/group_top.png";
                        else if (lineUp == lastInGroup)
                            lineUp.GroupImage = "pack://siteoforigin:,,,/Resources/group_bottom.png";
                        else
                            lineUp.GroupImage = "pack://siteoforigin:,,,/Resources/group_left.png";
                    }
                }
            }
        }

        /// <summary>
        /// This method will be invoked by the one implementing the view.
        /// Creates the XML file from the TreeView
        /// </summary>
        public void SaveFavoritesTreeToFile()
        {
            // Create the file if it does not exist to avoid unexpected error, ex: if they delete the file during runtime
            // under normal circumstances, this if statement will not be true because we generate the xml file in LoadFavoritesTreeIfRecordsExist()
            if (!File.Exists(mFavoritesDocumentPath))
            {
                var doc = new XDocument();
                doc.Add(new XElement("root"));
                doc.Save(mFavoritesDocumentPath);
            }
            var document = XDocument.Load(mFavoritesDocumentPath);

            // if there is no favorites node for our current product, create it during runtime.
            XElement productSpecificFavorites = null;
            if (document.Root.Elements("favorites").Where(node => node.Attribute("product").Value.Equals(DlkEnvironment.mProductFolder)).Count() == 1)
            {
                productSpecificFavorites = document.Root.Elements("favorites").Where(node => node.Attribute("product").Value.Equals(DlkEnvironment.mProductFolder)).FirstOrDefault();
                productSpecificFavorites.RemoveNodes(); // remove all descendants
                SearchForSuites.GlobalCollectionOfFavorites.Clear();
                foreach (var kwDirItem in mView.Favorites)
                {
                    if (kwDirItem is BFFolder)
                    {
                        productSpecificFavorites.Add(ConstructSchemaFromTreeView(kwDirItem));
                    }
                    else
                    {
                        productSpecificFavorites.Add(new XElement("file", new XAttribute("path", kwDirItem.Path), new XAttribute("name", kwDirItem.Name)));
                        SearchForSuites.GlobalCollectionOfFavorites.Add(kwDirItem);
                    }
                }
                document.Save(mFavoritesDocumentPath);
            }
        }

        /// <summary>
        /// Change the agent of all lineup records with the given agent.
        /// </summary>
        /// <param name="newAgent">Replace all records with this agent</param>
        public void ChangeAgentsInAllLineup(IAgentList newAgent)
        {
            List<TestLineupRecord> affectedRecords = mView.TestLineup.Where(x => x.RunningAgent.Name != newAgent.Name).ToList();
            foreach(TestLineupRecord tl in affectedRecords)
            {
                tl.RunningAgent = newAgent;
            }
            SaveLineup();
        }

        /// <summary>
        /// Change all lineup records with a certain agent to the given agent.
        /// </summary>
        /// <param name="agentName">Name of agent to replace in all records</param>
        /// <param name="newAgent">New agent we want to replace with</param>
        public void ChangeAgentsInLineup(string agentName, IAgentList newAgent)
        {
            List<TestLineupRecord> affectedRecords = mView.TestLineup.Where(x => x.RunningAgent.Name == agentName).ToList();
            foreach (TestLineupRecord tl in affectedRecords)
            {
                tl.RunningAgent = newAgent;
            }
            SaveLineup();
        }

        /// <summary>
        /// Adds the items in the Favorites XML file to the IObservable ItemsSource of the treeview 
        /// </summary>
        public void LoadFavoritesTree()
        {
            if (!File.Exists(mFavoritesDocumentPath))
            {
                var doc = new XDocument();
                doc.Add(new XElement("root"));
                doc.Save(mFavoritesDocumentPath);
            }
            SearchForSuites.GlobalCollectionOfFavorites.Clear();
            var document = XDocument.Load(mFavoritesDocumentPath);
            // grab hold of the favorites node for the current product (if it exists).
            var productSpecificFavorites = document.Root.Elements("favorites").Where(node => node.Attribute("product").Value.Equals(DlkEnvironment.mProductFolder)).ToList();
            // check if there are saved favorites for a product within the root. if there are, load the tests in the node into the tree
            if (productSpecificFavorites.Count() == 1) 
            {
                mView.Favorites.Clear();
                foreach (var element in productSpecificFavorites.Elements().ToList())
                {
                    // construct treeview from xelement until all xelements are read
                    mView.Favorites.Add(ConstructTreeViewFromSchema(element));
                }
            }
            else // if there is no favorite node for the specific product, create it here already so we will have no problems when saving.
            {
                document.Root.Add(new XElement("favorites", new XAttribute("product", DlkEnvironment.mProductFolder)));
                document.Save(mFavoritesDocumentPath);
            }
        }

        /// <summary>
        /// Update history grid with all the history of suite
        /// </summary>
        /// <param name="suite">Target schedule record</param>
        public void UpdateHistoryGrid(TestLineupRecord suite)
        {
            try
            {
                mView.History.Clear();
                var suiteNameWithoutExtension = Path.GetFileNameWithoutExtension(suite.TestSuiteToRun.Name);
                var productFolderName = DlkTestRunnerCmdLib.GetProductFolder(suite.TestSuiteToRun.Path);
                var suiteResultsDirectory = suite.TestSuiteToRun.Path.Substring(0, suite.TestSuiteToRun.Path.IndexOf(productFolderName) + productFolderName.Length) + "\\Framework\\SuiteResults\\";
                var mSuiteResultsFolder = suiteResultsDirectory + suiteNameWithoutExtension;
                if (Directory.Exists(mSuiteResultsFolder))
                {
                    var instances = new DirectoryInfo(mSuiteResultsFolder).GetDirectories();
                    for (int i = 0; i < instances.Count(); i++)
                    {
                        var instance = instances[i];
                        var suiteResult = GenerateExecutionHistoryFromSuiteInstance(suite.TestSuiteToRun.Path, mSuiteResultsFolder, instance.Name, suite);
                        // ignore files with no summary.dat
                        if (suiteResult != null)
                        {
                            mView.History.Add(suiteResult); // auto update UI because of ObservableCollection type
                        }
                    }
                }
                mView.History.Sort(observableCollection => observableCollection.OrderByDescending(item => DateTime.ParseExact(item.ExecutionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                                                                .ThenByDescending(item => DateTime.ParseExact(item.StartTime, "hh:mm:ss.ff tt", CultureInfo.InvariantCulture)));

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Delete selected item from controller history grid
        /// </summary>
        /// <param name="suite">Selected suite from test lineup</param>
        /// <param name="history">Selected item from controller history grid</param>
        public void DeleteHistoryGridItem(TestLineupRecord suite, ExecutionHistory history)
        {
            var suiteNameWithoutExtension = Path.GetFileNameWithoutExtension(suite.TestSuiteToRun.Name);
            var suiteResultsDirectory = DlkEnvironment.mDirSuiteResults; 
            var mSuiteResultsFolder = suiteResultsDirectory + suiteNameWithoutExtension;
            if (Directory.Exists(mSuiteResultsFolder))
            {
                var directoryList = new DirectoryInfo(mSuiteResultsFolder).GetDirectories();
                string folderName = string.Format("{0}{1}", Convert.ToDateTime(history.ExecutionDate).ToString("yyyyMMdd"), Convert.ToDateTime(history.EndTime).ToString("HHmmss"));
                if (directoryList.Any(x => x.Name.Equals(folderName)))
                {
                    string path = directoryList.FirstOrDefault(x => x.Name.Equals(folderName)).FullName;
                    Directory.Delete(path, true);
                    mView.History.Remove(history);
                }
            }
        }

        /// <summary>
        /// Filter the current list on history grid from the selected dates
        /// </summary>
        /// <param name="historyList">Current list in the history grid</param>
        /// <param name="from">Filter date from</param>
        /// <param name="to">Filter date to</param>
        public bool FilterControllerHistory(string from, string to)
        {
            DateTime dateFrom = DateTime.Parse(from);
            DateTime dateTo = DateTime.Parse(to);

            int result = DateTime.Compare(dateFrom, dateTo);
            if (result <= 0)
            {
                List<ExecutionHistory> historyList = mView.History.ToList();
                mView.History.Clear();
                foreach (var item in historyList)
                {
                    DateTime infoDate = DateTime.Parse(item.ExecutionDate);
                    if (infoDate >= dateFrom && infoDate <= dateTo)
                    {
                        mView.History.Add(item);
                    }
                }

                mView.History.Sort(observableCollection => observableCollection.OrderByDescending(item => DateTime.ParseExact(item.ExecutionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                                                                .ThenByDescending(item => DateTime.ParseExact(item.StartTime, "hh:mm:ss.ff tt", CultureInfo.InvariantCulture)));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the Favorites XML from the TreeView
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private XElement ConstructSchemaFromTreeView(KwDirItem item)
        {
            XElement returnValue = null;

            if (item is BFFolder)  // if item is folder, perform recursion to create the xml tree
            {
                returnValue = new XElement("folder", new XAttribute("path", item.Path), new XAttribute("name", item.Name), new XAttribute("expanded", item.IsExpanded.ToString()));
                foreach (var folderItem in (item as BFFolder).DirItems)
                {
                    returnValue.Add(ConstructSchemaFromTreeView(folderItem));
                }
            }
            else // if item is not a folder, just add the file
            {
                returnValue = new XElement("file", new XAttribute("path", item.Path), new XAttribute("name", item.Name));
                SearchForSuites.GlobalCollectionOfFavorites.Add(item);
            }
            return returnValue;
        }

        /// <summary>
        /// Creates the TreeView from the Favorites XML file
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private KwDirItem ConstructTreeViewFromSchema(XElement element)
        {
            dynamic returnValue = null;
            if (element.Name.ToString().Equals("folder"))
            {
                var temp = new BFFolder();
                temp.IsExpanded = bool.Parse(element.Attribute("expanded").Value.ToString());
                temp.Name = element.Attribute("name").Value.ToString();
                temp.Path = element.Attribute("path").Value.ToString();
                foreach (var folderChild in element.Elements())
                {
                    temp.DirItems.Add(ConstructTreeViewFromSchema(folderChild));
                }
                returnValue = temp;
            }
            else
            {
                returnValue = new BFFile()
                {
                    Name = element.Attribute("name").Value.ToString(),
                    Path = element.Attribute("path").Value.ToString()
                };
                SearchForSuites.GlobalCollectionOfFavorites.Add(returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Add last suite result to the results view
        /// </summary>
        /// <param name="suite"></param>
        public void AddLastSuiteResultToTestLineupRecord(TestLineupRecord suite)
        {
            try
            {
                var suiteNameWithoutExtension = Path.GetFileNameWithoutExtension(suite.TestSuiteToRun.Name);
                var productFolderName = DlkTestRunnerCmdLib.GetProductFolder(suite.TestSuiteToRun.Path);
                var suiteResultsDirectory = suite.TestSuiteToRun.Path.Substring(0, suite.TestSuiteToRun.Path.IndexOf(productFolderName) + productFolderName.Length) + "\\Framework\\SuiteResults\\";
                var mSuiteResultsFolder = suiteResultsDirectory + suiteNameWithoutExtension;
                if (Directory.Exists(mSuiteResultsFolder))
                {
                    var instances = new DirectoryInfo(mSuiteResultsFolder).GetDirectories().OrderByDescending(folder => folder.Name);
                    // get last successful suite result. loop all suite results
                    foreach (var instance in instances)
                    {
                        var suiteResult = GenerateExecutionHistoryFromSuiteInstance(suite.TestSuiteToRun.Path, mSuiteResultsFolder, instance.Name, suite);
                        if (suiteResult != null)
                        {
                            suite.LastRunResult = suiteResult;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            
        }

        /// <summary>
        /// Generates a suite result/ExecutionHistory
        /// </summary>
        /// <param name="mSuiteName">Name of suite</param>
        /// <param name="mSuiteResultsFolder">Full path of all results folder</param>
        /// <param name="instance">Target results folder</param>
        /// <param name="Owner">Owner object of history</param>
        /// <returns></returns>
        private ExecutionHistory GenerateExecutionHistoryFromSuiteInstance(String suitePath, String mSuiteResultsFolder, String instance, object Owner)
        {
            ExecutionHistory historyEntry = null;
            var resultsPath = mSuiteResultsFolder + @"\" + instance;
            var summaryPath = resultsPath + @"\summary.dat";
            if (File.Exists(summaryPath))
            {
                Dictionary<string, string> summaryDict = DlkTestSuiteResultsFileHandler.GetSummaryAttributeValues(summaryPath);
                var executionDate = DateTime.ParseExact(instance.Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var duration = TimeSpan.Parse(summaryDict[DlkTestSuiteInfoAttributes.ELAPSED]);

                historyEntry = new ExecutionHistory()
                {
                    SuiteFullPath = suitePath,
                    ResultsFolderFullPath = resultsPath,
                    ExecutionDate = executionDate.Date.ToString("MM/dd/yyyy"),
                    Duration = duration.ToString("hh\\:mm\\:ss\\.ff"),
                    Browser = summaryDict[DlkTestSuiteInfoAttributes.BROWSER],
                    Environment = summaryDict[DlkTestSuiteInfoAttributes.ENVIRONMENT],
                    StartTime = executionDate.Subtract(duration).ToString("hh:mm:ss.ff tt"), // JP: Is this start time reliable?
                    EndTime = executionDate.ToString("hh:mm:ss.ff tt"),
                    TotalTests = summaryDict[DlkTestSuiteInfoAttributes.TOTAL],
                    PassedTests = summaryDict[DlkTestSuiteInfoAttributes.PASSED],
                    FailedTests = summaryDict[DlkTestSuiteInfoAttributes.FAILED],
                    NotRunTests = summaryDict[DlkTestSuiteInfoAttributes.NOTRUN],
                    Id = Guid.NewGuid().ToString(), // update id, random guid for now
                    RunningAgent = summaryDict[DlkTestSuiteInfoAttributes.MACHINENAME],
                    OperatingSystem = summaryDict[DlkTestSuiteInfoAttributes.OPERATINGSYSTEM],
                    UserName = summaryDict[DlkTestSuiteInfoAttributes.USERNAME],
                    RunNumber = summaryDict[DlkTestSuiteInfoAttributes.RUNNUMBER],
                    Parent = Owner
                };
            }
           
            return historyEntry;
        }

        /// <summary>
        /// Compare the 2 scheduler file if they have the same content
        /// </summary>
        /// <param name="currentScheduleXDocStr"></param>
        /// <param name="modifiedScheduleXDocStr"></param>
        /// <returns></returns>
        private bool IsSchedulerChanged(string currentScheduleXDocStr, string modifiedScheduleXDocStr)
        {
            return string.IsNullOrEmpty(currentScheduleXDocStr) ? true : currentScheduleXDocStr != modifiedScheduleXDocStr;
        }
    }
}
