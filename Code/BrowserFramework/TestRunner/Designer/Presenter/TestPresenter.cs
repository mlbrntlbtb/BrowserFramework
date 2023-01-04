//#define ASYNC_LOAD
#define PARALLEL_LOAD

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestRunner.Common;
using TestRunner.Designer.Model;
using TestRunner.Designer.View;

namespace TestRunner.Designer.Presenter
{
    public class TestPresenter
    {
        #region PRIVATE MEMBERS
        private ITestsView mView = null;
        private List<TLSuite> allSuiteTests = new List<TLSuite>();
        private BackgroundWorker[] mTestLoaderList = {new BackgroundWorker(), new BackgroundWorker(), new BackgroundWorker(), 
                                                             new BackgroundWorker(), new BackgroundWorker()};
        private String mSearchSuitesPreviousFilter = "";
        private List<List<TLSuite>> mLoadedSuiteTests = new List<List<TLSuite>>();
        public TestLoadingStatus mLoadStatus = new TestLoadingStatus();
        public List<TLSuite> AllSuites = new List<TLSuite>();
        private FileSystemWatcher mSuitesWatcher = new FileSystemWatcher();
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view"></param>
        public TestPresenter(ITestsView view)
        {
            mView = view;
        }

        /// <summary>
        /// Loads test steps of selected test in suite containing the target test
        /// </summary>
        /// <returns></returns>
        public void LoadSelectedContainingSuiteTestSteps(String testPath)
        {
            List<DlkTestStepRecord> testSteps = new List<DlkTestStepRecord>();
            if (File.Exists(testPath))
            {
                DlkTest SelectedTest = new DlkTest(testPath);
                foreach (DlkTestStepRecord tsr in SelectedTest.mTestSteps)
                {
                    testSteps.Add(tsr);
                }
            }
            mView.SelectedSuiteTestSteps = testSteps;
        }

        /// <summary>
        /// Loads selected test description and containing suites
        /// </summary>
        /// <returns></returns>
        public void LoadTargetTest(String testPath, List<DlkTest> PoolOfTests)
        {
            String testFile = Path.GetFileName(testPath);
            String testFolder = Path.GetDirectoryName(testPath).Replace(DlkEnvironment.mDirTests.Trim("\\"
                .ToCharArray()), string.Empty);
            List<TLSuite> suiteList = new List<TLSuite>();
            if (File.Exists(testPath))
            {
                DlkTest targetTest = PoolOfTests.FirstOrDefault(x => x.mTestPath == testPath);
                if (targetTest != null)
                {
                    mView.TargetTest = new DlkTest(testPath);
                    foreach (TLSuite suite in allSuiteTests)
                    {
                        if (suite.Tests.Exists(s => s.file == testFile && s.folder == testFolder))
                        {
                            suite.ListIndex = (suiteList.Count + 1).ToString();
                            suiteList.Add(suite);
                        }
                    }
                }
            }
            mView.ContainingSuites = suiteList;
        }

        /// <summary>
        /// Loads suite info and tests of each suite
        /// </summary>
        /// <returns></returns>
        public void LoadAllSuiteTests(string suiteDirectory)
        {
            DirectoryInfo di = new DirectoryInfo(suiteDirectory);
            FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);

#if ASYNC_LOAD
            /* Initialize load status semaphore */
            mLoadStatus = new TestLoadingStatus();

            int totalFile = mFiles.Count(); // total suites to load
            int numLoadersNeeded = mLoadStatus.TestLoaderCountNeeded(mFiles.Count()); // number of async loaders to use
            int filePerLoader = totalFile / numLoadersNeeded; // suites per async loader

            for (int x = 0; x < numLoadersNeeded; x++)
            {
                /* If this is last loader, ensure it processes the 'excess' suites caused by int division */
                int limit = x == numLoadersNeeded - 1 ? filePerLoader + (totalFile - (numLoadersNeeded * filePerLoader)) : filePerLoader;

                /* get portion of suites for loading of async loader */
                FileInfo[] fiArray = new FileInfo[limit];
                int startCopyAt = x * filePerLoader;
                Array.Copy(mFiles, startCopyAt, fiArray, 0, limit);

                /* Load the portion of files */
                mTestLoaderList[x].DoWork += TestLoader_DoWork;
                mTestLoaderList[x].RunWorkerCompleted += TestLoader_RunWorkerCompleted;
                mTestLoaderList[x].RunWorkerAsync(fiArray);
            }
#else
#if PARALLEL_LOAD
            Parallel.ForEach(mFiles, (suite) =>
                {
                    try
                    {
                        Tuple<DlkTestSuiteInfoRecord, List<DlkExecutionQueueRecord>> currSuite 
                            = DlkTestSuiteXmlHandler.LoadSuiteInfoAndRecords(suite.FullName, "0/0");

                        TLSuite temp = new TLSuite()
                        {
                            SuiteInfo = currSuite.Item1,
                            Tests = currSuite.Item2,
                            name = suite.Name,
                            path = suite.FullName
                        };

                        foreach (var tst in temp.Tests)
                        {
                            if (TestDescriptions.Items.ContainsKey(tst.fullpath))
                            {
                                tst.description = TestDescriptions.Items[tst.fullpath].description;
                                TestDescriptions.Items[tst.fullpath].testAsRecordsOnSuites.Add(tst);
                            }
                        }
                        allSuiteTests.Add(temp);
                    }
                    catch
                    {
                        // let error and ignore rather than check for validity, which is expensive and not thread-safe
                    }
                    AllSuites = allSuiteTests;
                });
#else
            allSuiteTests = LoadPartialSuiteTests(mFiles);
#endif
#endif
            /* Initialize directory watcher to notify on changes */
            mSuitesWatcher.Path = suiteDirectory;
            mSuitesWatcher.IncludeSubdirectories = true;
            mSuitesWatcher.NotifyFilter = NotifyFilters.LastWrite;
            mSuitesWatcher.Filter = "*.*";
            mSuitesWatcher.EnableRaisingEvents = true;
            mSuitesWatcher.Changed += MSuitesWatcher_Changed;
        }

        /// <summary>
        /// Update view with latest status
        /// </summary>
        public void UpdateLoadProgress()
        {
            mView.UpdateViewStatus(mLoadStatus.LoadProgress);
        }

        /// <summary>
        /// Loads tests in the selected suite containing the target test
        /// </summary>
        /// <returns></returns>
        public void LoadSelectedContainingSuite(String targetPath, List<TLSuite> PoolofSuites)
        {
            mView.SelectedSuiteTests = new List<DlkExecutionQueueRecord>();
            TLSuite targetSuite = PoolofSuites.FirstOrDefault(x => x.path == targetPath);
            if (targetSuite != null)
            {
                mView.SelectedSuiteTests = targetSuite.Tests;
            }
        }

        public void SearchTests(string filterString)
        {
            filterString = filterString.ToLower();

            //if first time to filter (example: if first letter was typed) , filter the entire collection of xml files that 
            //satisfies filter
            if (mView.FilteredTests.Count == 0 && !string.IsNullOrEmpty(filterString))
            {
                mView.FilteredTests = SearchForTests._globalCollectionOfTests.FindAll(s => s.Name.ToLower().Contains(filterString) && s is KwFile).AsParallel().ToList();
                mSearchSuitesPreviousFilter = filterString;
            }
            //if not first time to filter
            else if (mView.FilteredTests.Count > 0 && !string.IsNullOrEmpty(filterString))
            {
                //handler for continued typing, query smaller list every succeeding letter
                if (filterString.Length > mSearchSuitesPreviousFilter.Length && filterString.Contains(mSearchSuitesPreviousFilter))
                {
                    mView.FilteredTests = mView.FilteredTests.FindAll(s => s.Name.ToLower().Contains(filterString) && s is KwFile).AsParallel().ToList();
                }
                else
                {
                    mView.FilteredTests = SearchForTests._globalCollectionOfTests.FindAll(s => s.Name.ToLower().Contains(filterString) && s is KwFile).AsParallel().ToList();
                }
                mSearchSuitesPreviousFilter = filterString;
            }
            else if (string.IsNullOrEmpty(filterString))
            {
                //clear filtered test results here if no filter string.
                //this will return a list containing 0 items, making the treeView.datacontext use _testsFolderDirectory
                mView.FilteredTests.Clear();
                mSearchSuitesPreviousFilter = "";
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Load a portion of suites tests
        /// </summary>
        /// <param name="PartialSuites"></param>
        /// <returns></returns>
        private List<TLSuite> LoadPartialSuiteTests(FileInfo[] PartialSuites)
        {
            List<TLSuite> ret = new List<TLSuite>();
            foreach (FileInfo suite in PartialSuites)
            {
                try
                {
                    Tuple<DlkTestSuiteInfoRecord, List<DlkExecutionQueueRecord>> currSuite
                        = DlkTestSuiteXmlHandler.LoadSuiteInfoAndRecords(suite.FullName, "0/0");

                    TLSuite temp = new TLSuite()
                    {
                        SuiteInfo = currSuite.Item1,
                        Tests = currSuite.Item2,
                        name = suite.Name,
                        path = suite.FullName
                    };
                    ret.Add(temp);
                }
                catch
                {
                    // let error and ignore rather than check for validity, which is expensive and not thread-safe
                }
            }
            return ret;
        }  
        #endregion

        #region EVENTHANDLERS
        private void TestLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mLoadStatus.LoaderFinished();
            allSuiteTests.AddRange((List<TLSuite>)e.Result);
        }

        private void TestLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo[] arg = (FileInfo[])e.Argument;
            e.Result = LoadPartialSuiteTests(arg);
        }

        /// <summary>
        /// Handler for File System watcher changed event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void MSuitesWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                var target = AllSuites.FindAll(x => x.path == e.FullPath).FirstOrDefault();
                if (target != null)
                {

                    var updated = LoadPartialSuiteTests(new FileInfo[] { new FileInfo(e.FullPath) }).FirstOrDefault() ?? target;
                    target.SuiteInfo.Update(updated.SuiteInfo.Browser, updated.SuiteInfo.EnvID, updated.SuiteInfo.Language, updated.SuiteInfo.Tags,
                        updated.SuiteInfo.Email, updated.SuiteInfo.Description, updated.SuiteInfo.mLinks, updated.SuiteInfo.Owner, updated.SuiteInfo.GlobalVar);
                    target.Tests = updated.Tests; // JPV: is this enough?
                    mView.UpdateViewStatus(Enumerations.TestViewStatus.SelectedSuiteEdited); /* temp */
                }
                else // Is it ok to assume that a new file was added?
                {
                    /* To Do: For now, do not support this */
                    mView.UpdateViewStatus(Enumerations.TestViewStatus.NewSuiteAdded);
                }
            }
            catch
            {
                /* Do nothing. Ignore failure to track changes to dir */
            }
        }
        #endregion
    }
}
