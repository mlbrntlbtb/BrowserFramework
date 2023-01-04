//#define ASYNC_LOAD
#define PARALLEL_LOAD

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TestRunner.Designer.View;
using TestRunner.Designer.Model;
using TestRunner.Common;
using CommonLib.DlkSystem;

namespace TestRunner.Designer.Presenter
{
    public class FinderPresenter
    {

        #region PUBLIC MEMBERS
        public List<DlkTest> loadedTests = new List<DlkTest>();
        #endregion
        #region PRIVATE MEMBERS
        private IFinderView mView = null;
        private BackgroundWorker[] mTestLoaderList = {new BackgroundWorker(), new BackgroundWorker(), new BackgroundWorker(),
                                                             new BackgroundWorker(), new BackgroundWorker()};
        private List<List<DlkTest>> mLoadedTests = new List<List<DlkTest>>();
        private TestLoadingStatus mLoadStatus = new TestLoadingStatus();
        private FileSystemWatcher mTestWatcher = new FileSystemWatcher();
        #endregion

        public List<DlkTest> LoadedTests 
        {
            get
            {
                return loadedTests;
            }
        }

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view"></param>
        public FinderPresenter(IFinderView view)
        {
            mView = view;
            DlkAssemblyKeywordHandler.Initialize(DlkEnvironment.mLibrary);
        }
        /// <summary>
        /// Loads all available screens
        /// </summary>
        /// <param name="objectStoreFiles"></param>
        public void LoadScreens(List<DlkObjectStoreFileRecord> objectStoreFiles=null)
        {
            //List<String> mScreens = new List<String>();
            //foreach (DlkObjectStoreFileRecord osfr in objectStoreFiles)
            //{
            //    mScreens.Add(osfr.mScreen);
            //}
            //mScreens.Sort();
            mView.Screens = DlkDynamicObjectStoreHandler.Instance.Screens;
        }

        /// <summary>
        /// Loads all controls available to given screen
        /// </summary>
        /// <param name="osfr"></param>
        public void LoadControls(string Screen)
        {
            List<String> mControls = new List<String>();
            if (Screen == "Function")
            {
                mControls.Add("Function");
            }
            else
            {
                foreach (DlkObjectStoreFileControlRecord ctrl in DlkDynamicObjectStoreHandler.Instance.GetControlRecords(Screen))
                {
                    mControls.Add(ctrl.mKey);
                }
            }
            mControls.Sort();
            mView.Controls = mControls;
        }

        /// <summary>
        /// Loads all keywords available to given control
        /// </summary>
        /// <param name="osfr"></param>
        public void LoadKeywords(String Screen, String Control)
        {
            String mType = "";
            List<String> kw = new List<String>();


            if (Screen == "Function" && Control == "Function")
            {
                mType = "Function";
            }
            else if (string.IsNullOrEmpty(Control))
            {
                mType = Screen;
            }
            else
            {
                mType = DlkDynamicObjectStoreHandler.Instance.GetControlType(Screen, Control);
            }
            kw = DlkAssemblyKeywordHandler.GetControlKeywords(mType);
            kw.Sort();
            mView.Keywords = kw;
        }
        /// <summary>
        /// Adds new step to finder test
        /// </summary>
        public void AddStep()
        {
            List<DlkTestStepRecord> tsrList = new List<DlkTestStepRecord>(mView.FinderTest);
            DlkTestStepRecord tsr = new DlkTestStepRecord();
            tsr.mParameters = new List<string>();
            tsr.mStepNumber = mView.FinderTest.Count + 1;
            tsr.mScreen = mView.CurrentScreen;
            tsr.mControl = mView.CurrentControl;
            tsr.mKeyword = mView.CurrentKeyword;
            tsr.mParameters.Add(mView.CurrentParameter);
            tsrList.Add(tsr);
            mView.FinderTest = tsrList;
        }

        public void EditStep(DlkTestStepRecord SelectedStep)
        {
            SelectedStep.mScreen = mView.CurrentScreen;
            SelectedStep.mControl = mView.CurrentControl;
            SelectedStep.mKeyword = mView.CurrentKeyword;
            SelectedStep.mParameters = new List<string>();
            SelectedStep.mParameters.Add(mView.CurrentParameter);
            mView.FinderTest = mView.FinderTest;
        }

        /// <summary>
        /// Clears steps of finder test
        /// </summary>
        public void ClearSteps()
        {
            mView.FinderTest = new List<DlkTestStepRecord>();
        }

        /// <summary>
        /// Delete selected step/s from finder test
        /// </summary>
        /// <param name="selectedRecords"></param>
        public void DeleteStep(List<DlkTestStepRecord> selectedRecords)
        {
            int lastStep = selectedRecords.Max(x => x.mStepNumber);
            List<DlkTestStepRecord> tsr = mView.FinderTest.Except(selectedRecords).ToList();
            var currSelectedItem = tsr.Find(x => x.mStepNumber == lastStep + 1) ??
                                   tsr.Find(x => x.mStepNumber == lastStep - 1);

            // renumber all succeeding steps
            int currSelectedIndex = tsr.Count > 0 ? tsr.Count - 1 : 0;
            for (int idx = 0; idx < tsr.Count; idx++)
            {
                if (currSelectedItem != null && currSelectedItem.mStepNumber == tsr[idx].mStepNumber)
                {
                    currSelectedIndex = idx;
                }
                tsr[idx].mStepNumber = idx + 1;
            }
            mView.FinderTest = tsr;
        }

        /// <summary>
        /// Looks for matches of finder test across all existing tests
        /// </summary>
        public void FindTest()
        {
            List<Match> matches = new List<Match>();

            foreach (DlkTest test in FilteredTest())
            {
                Match tempMatch = new Match();
                tempMatch.TestName = test.mTestName;
                tempMatch.Path = test.mTestPath;
                tempMatch.StepCount = test.mTestSteps.Count;
                tempMatch.MatchedRows = GetMatches(mView.FinderTest, test, mView.IncludeParameters);
                tempMatch.MatchCount = tempMatch.MatchedRows.Count;
                tempMatch.IsExactMatch = (tempMatch.MatchCount == tempMatch.StepCount) && (tempMatch.StepCount == mView.FinderTest.Count()) ? true : false;
                tempMatch.IsExactMatchString = (tempMatch.MatchCount == tempMatch.StepCount) && (tempMatch.StepCount == mView.FinderTest.Count()) ? "Yes" : "No";
                tempMatch.StepCountDifference = GetStepCountDifference(test.mTestSteps, mView.FinderTest);
                if (tempMatch.MatchCount > 0)
                {
                    matches.Add(tempMatch);
                }
            }
            matches = matches.OrderByDescending(x => x.MatchCount).ThenBy(x => x.StepCountDifference).ToList();
            mView.FinderMatches = matches;
        }

        /// <summary>
        /// Loads all available tests
        /// </summary>
        public void LoadAllTests()
        {
            DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTests);
            FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);
            TestDescriptions.Items = new ConcurrentDictionary<string, TestDescriptionItem>();
#if ASYNC_LOAD
            /* Initialize load status semaphore */
            mLoadStatus = new TestLoadingStatus();

            int totalFile = mFiles.Count(); // total tests to load
            int numLoadersNeeded = mLoadStatus.TestLoaderCountNeeded(mFiles.Count()); // number of async loaders to use
            int filePerLoader = totalFile / numLoadersNeeded; // tests per async loader

            for(int x = 0; x < numLoadersNeeded; x++)
            {
                /* If this is last loader, ensure it processes the 'excess' tests caused by int division */
                int limit = x == numLoadersNeeded - 1 ? filePerLoader + (totalFile - (numLoadersNeeded * filePerLoader)) : filePerLoader;

                /* get portion of tests for loading of async loader */
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
            Parallel.ForEach(mFiles, (test) =>
                {
                    if (DlkTest.IsValidTest(test.FullName))
                    {
                        DlkTest temp = new DlkTest(test.FullName);
                        loadedTests.Add(temp);
                        TestDescriptions.Items.TryAdd(test.FullName, new TestDescriptionItem(
                            temp.mTestDescription, new List<DlkExecutionQueueRecord>()));
                    }
                });
#else
            loadedTests = LoadPartialTests(mFiles);
#endif
#endif
            mTestWatcher.Path = DlkEnvironment.mDirTests;
            mTestWatcher.IncludeSubdirectories = true;
            mTestWatcher.NotifyFilter = NotifyFilters.LastWrite;
            mTestWatcher.Filter = "*.xml";
            mTestWatcher.EnableRaisingEvents = true;
            mTestWatcher.Changed += MTestWatcher_Changed;
        }
        
        /// <summary>
        /// Update view with latest load status
        /// </summary>
        public void UpdateLoadProgress()
        {
            mView.UpdateViewStatus(mLoadStatus.LoadProgress);
        }

        /// <summary>
        /// Go to folder of selected match
        /// </summary>
        /// <param name="TestPath">Path of file to open in WinExplorer</param>
        public void GoToFolder(string TestPath)
        {
            if (File.Exists(TestPath))
            {
                string args = "/select, \"" + TestPath + "\"";
                System.Diagnostics.Process.Start("explorer.exe", args);
            }
        }

        /// <summary>
        /// Import test file
        /// </summary>
        public void ImportTest()
        {
            var importDlg = new System.Windows.Forms.OpenFileDialog();
            importDlg.InitialDirectory = DlkEnvironment.mDirTests;
            importDlg.Filter = "XML files (*.xml)|*.xml";
            importDlg.Title = "Import Test";

            if (importDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string importFile = importDlg.FileName;
                    //Check if valid test file
                    if (DlkTest.IsValidTest(importFile))
                    {
                        DlkTest mTest = null;
                        try
                        {
                            mTest = new DlkTest(importFile);
                        }
                        catch (System.Xml.XmlException) //We only want to catch the error in opening an invalid trd file
                        {
                            DlkUserMessages.ShowError(DlkUserMessages.ERR_DATA_XML_INVALID);
                            return;
                        }

                        if (mTest.Data.Records.Count != 0)
                        {
                            mTest.mTestInstanceExecuted = 1; //Only loading the first instance
                            DlkData.SubstituteDataVariables(mTest);
                            DlkData.SubstituteExecuteDataVariables(mTest);
                        }
                        ClearSteps();
                        mView.FinderTest = mTest.mTestSteps;
                    }
                    else
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Update Test when adding tags through Test Editor
        /// </summary>
        /// <param name="TestsToUpdate"></param>
        public void UpdateLoadedTests(FileInfo[] TestsToUpdate)
        {
            foreach (FileInfo test in TestsToUpdate)
            {
                int i = loadedTests.IndexOf(loadedTests.Find(x => x.mTestPath == test.FullName));
                loadedTests[i] = new DlkTest(test.FullName);
                /* update static Test Description List */
                if (TestDescriptions.Items.ContainsKey(test.FullName))
                {
                    TestDescriptions.Items[test.FullName].description = loadedTests[i].mTestDescription;
                    foreach(var eqr in TestDescriptions.Items[test.FullName].testAsRecordsOnSuites)
                    {
                        eqr.description = loadedTests[i].mTestDescription;
                    }
                }
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Finds matches from the given list of test steps and test data
        /// </summary>
        /// <param name="mFinder"></param>
        /// <param name="mTest"></param>
        /// <param name="CheckParameters"></param>
        public List<DlkTestStepRecord> GetMatches(List<DlkTestStepRecord> mFinder, DlkTest mTest, bool CheckParameters)
        {
            List<DlkTestStepRecord> tsrMatch = new List<DlkTestStepRecord>();
            if (CheckParameters)
            {
                for (int x = 1; x <= mTest.mInstanceCount; x++)
                {
                    //Substituting data variables may raise exceptions that should be ignored
                    try
                    {
                        //Substitute data first
                        mTest.mTestInstanceExecuted = x;
                        DlkData.SubstituteDataVariables(mTest);
                        DlkData.SubstituteExecuteDataVariables(mTest);
                    }
                    catch
                    {
                        //Do nothing
                    }

                    //Match data
                    if (mFinder.Any(a => a.mParameterOrigString.Contains("^")))
                    {
                        foreach (DlkTestStepRecord item in mFinder)
                        {
                            List<DlkTestStepRecord> recToAdd;
                            CompareParameter(item, mTest.mTestSteps, out recToAdd);
                            if (recToAdd.Count > 0)
                            {
                                foreach (DlkTestStepRecord rec in recToAdd)
                                {
                                    tsrMatch.Add(rec);
                                }
                            }
                        }
                    }
                    else
                    {
                        if(mFinder.Count == 0) // When Finder is empty
                        {
                            tsrMatch = mTest.mTestSteps;
                        }
                        else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mControl) && String.IsNullOrEmpty(b.mKeyword))) && mFinder.Count == 1) // Only Screen is specified
                        {
                            tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mScreen == a.mScreen && b.mParameterOrigString == a.mParameterOrigString)).Distinct().ToList();
                        }
                        else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mKeyword)) && mFinder.Count == 1)) // Screen & Control is specified
                        {
                            tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mControl == a.mControl && b.mScreen == a.mScreen && b.mParameterOrigString == a.mParameterOrigString)).Distinct().ToList();
                        }
                        else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mControl)) && mFinder.Count == 1))  // Screen & Keyword is specified
                        {
                            tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mKeyword == a.mKeyword && b.mScreen == a.mScreen && b.mParameterOrigString == a.mParameterOrigString)).Distinct().ToList();
                        }
                        else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mControl) || String.IsNullOrEmpty(b.mKeyword)))) // When more than 1 row & at least 1 row has incomplete input
                        {
                            tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mScreen == a.mScreen && b.mParameterOrigString == a.mParameterOrigString ||
                                                                 b.mScreen == a.mScreen && b.mControl == a.mControl && b.mParameterOrigString == a.mParameterOrigString ||
                                                                 b.mScreen == a.mScreen && b.mKeyword == a.mKeyword && b.mParameterOrigString == a.mParameterOrigString ||
                                                                 b.mKeyword == a.mKeyword && b.mControl == a.mControl && b.mScreen == a.mScreen && b.mParameterOrigString == a.mParameterOrigString)).Distinct().ToList();
                        }
                        else // Search for all specific matches for complete input
                        {
                            tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mKeyword == a.mKeyword && b.mControl == a.mControl && b.mScreen == a.mScreen && b.mParameterOrigString == a.mParameterOrigString)).Distinct().ToList();
                        }


                    if (tsrMatch.Count > 0)
                        break;
                }
            }
            }
            else
            {
                if (mFinder.Count == 0)  // When Finder is empty
                {
                    tsrMatch = mTest.mTestSteps;
                }
                else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mControl) && String.IsNullOrEmpty(b.mKeyword))) && mFinder.Count == 1) // Only Screen is specified
                {
                    tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mScreen == a.mScreen)).Distinct().ToList();
                }
                else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mKeyword)) && mFinder.Count == 1)) // Screen & Control is specified
                {
                    tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mControl == a.mControl && b.mScreen == a.mScreen)).Distinct().ToList();
                }
                else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mControl)) && mFinder.Count == 1))  // Screen & Keyword is specified
                {
                    tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mKeyword == a.mKeyword && b.mScreen == a.mScreen)).Distinct().ToList();
                }
                else if ((mFinder.Any(b => String.IsNullOrEmpty(b.mControl) || String.IsNullOrEmpty(b.mKeyword)))) // When more than 1 row & at least 1 row has incomplete input
                {
                    tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mScreen == a.mScreen ||
                                                         b.mScreen == a.mScreen && b.mControl == a.mControl ||
                                                         b.mScreen == a.mScreen && b.mKeyword == a.mKeyword ||
                                                         b.mKeyword == a.mKeyword && b.mControl == a.mControl && b.mScreen == a.mScreen)).Distinct().ToList();
                }
                else // Search for all specific matches for complete input
                {
                    tsrMatch = mTest.mTestSteps.Where(a => mFinder.Any(b => b.mKeyword == a.mKeyword && b.mControl == a.mControl && b.mScreen == a.mScreen)).Distinct().ToList();
                }
            }

            

            return tsrMatch;
        }

        private List<DlkTest> FilteredTest()
        {
            if (!string.IsNullOrEmpty(mView.Filter.Description.Trim()) ||
                !string.IsNullOrEmpty(mView.Filter.Tags.Trim()) ||
                !string.IsNullOrEmpty(mView.Filter.Links.Trim()))
            {
                return loadedTests.Where(test => TestFilter(test)).ToList();
            }
            return loadedTests;
        }

        private bool TestFilter(DlkTest test)
        {
            var filter = mView.Filter;
            bool Description = true,
                         Tags = true,
                         Links = true;

            //DESCRIPTION
            if (!string.IsNullOrEmpty(filter.Description))
            {
                var desc = filter.Description.Trim();
                if (filter.DescExactMatch)
                    Description = test.mTestDescription == desc;
                else
                    Description = test.mTestDescription.ToLower().Contains(desc.ToLower());
            }

            //TAGS
            if (!string.IsNullOrEmpty(filter.Tags))
            {
                List<bool> temp = new List<bool>();
                var tags = filter.Tags.Split(',').Select(d => d.Trim()).ToList();
                
                if (tags != null && tags.Count() > 0)
                {
                    //EXACT MATCH CHECK
                    if (filter.TagsExactMatch)
                        foreach (var tag in tags)
                            temp.Add(test.mTags.Where(t => t.Name.Trim() == tag.Trim()).Count() > 0);
                    else
                        foreach (var tag in tags)
                            temp.Add(test.mTags.Where(t => t.Name.Contains(tag.Trim())).Count() > 0);

                    //AND OR CHECK
                    if (filter.TagsAnd && tags.Distinct().Count() == tags.Count())
                        Tags = temp.All(t => t == true); // all should be true
                    else if (filter.TagsOr)
                        Tags = temp.Contains(true); // atleast one is true
                    else
                        Tags = false;
                }
            }

            //LINKS
            if (!string.IsNullOrEmpty(filter.Links))
            {
                List<bool> temp = new List<bool>();
                var links = filter.Links.Split(',').Select(d => d.Trim()).ToList();
                
                if (links != null && links.Count() > 0)
                {   
                    //EXACT MATCH CHECK
                    if ((filter.LinksExactMatch))
                        foreach (var link in links)
                            temp.Add(test.mLinks.Where(l => l.DisplayName.Trim() == link.Trim() || l.LinkPath.Trim() == link.Trim()).Count() > 0);
                    else
                        foreach (var link in links)
                            temp.Add(test.mLinks.Where(l => l.DisplayName.Contains(link.Trim()) || l.LinkPath.Trim().Contains(link.Trim())).Count() > 0);

                    //AND OR CHECK
                    if (filter.LinksAnd && links.Distinct().Count() == links.Count())
                        Links = temp.All(t => t == true);  // all should be true
                    else if (filter.LinksOr)
                        Links = temp.Contains(true); // atleast one is true
                    else
                        Links = false;
                }
            }

            return Description && Tags && Links;
        }

        private int GetStepCountDifference(List<DlkTestStepRecord> ListA, List<DlkTestStepRecord> ListB)
        {
            return Math.Abs(ListA.Count - ListB.Count);
        }

        /// <summary>
        /// Load portion of tests
        /// </summary>
        /// <param name="PartialTests"></param>
        /// <returns></returns>
        private List<DlkTest> LoadPartialTests(FileInfo[] PartialTests)
        {
            List<DlkTest> ret = new List<DlkTest>();
            foreach (FileInfo test in PartialTests)
            {
                if (DlkTest.IsValidTest(test.FullName))
                {
                    DlkTest temp = new DlkTest(test.FullName);
                    ret.Add(temp);
                }
            }
            return ret;
        }

        private void CompareParameter(DlkTestStepRecord finder, List<DlkTestStepRecord> tests, out List<DlkTestStepRecord> recToAdd)
        {
            bool flag = false;
            recToAdd = new List<DlkTestStepRecord>();

            foreach (DlkTestStepRecord test in tests)
            {
                string[] finderParm = finder.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                string[] testParm = test.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                if (finderParm.Count() == testParm.Count())
                {
                    if ((test.mScreen == finder.mScreen) &&
                        (test.mControl == finder.mControl) &&
                        (test.mKeyword == finder.mKeyword))
                    {
                        for (int i = 0; i < finderParm.Count(); i++)
                        {
                            if (finderParm[i].Contains("^"))
                            {
                                string tempParm = finderParm[i].Replace("^", "");
                                if (testParm[i].Contains(tempParm))
                                {
                                    flag = true;
                                    continue;
                                }
                                else
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (testParm[i].Equals(finderParm[i]))
                                {
                                    flag = true;
                                    continue;

                                }
                                else
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        if(flag)
                        {
                            recToAdd.Add(test);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region EVENTHANDLERS
        private void TestLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mLoadStatus.LoaderFinished();
            loadedTests.AddRange((List<DlkTest>)e.Result);
        }

        private void TestLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo[] arg = (FileInfo[])e.Argument;
            e.Result = LoadPartialTests(arg);
        }

        /// <summary>
        /// Handler for File System watcher changed event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void MTestWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                var target = loadedTests.FindAll(x => x.mTestPath == e.FullPath).FirstOrDefault();
                if (target != null)
                {
                    UpdateLoadedTests(new FileInfo[] { new FileInfo(e.FullPath) });
                    mView.UpdateViewStatus(Enumerations.FinderViewStatus.SelectedTestEdited); /* temp */
                }
                else // Is it ok to assume that it is a new file?
                {
                    /* TO DO: For now, do not support this */
                    //loadedTests.Add(new DlkTest(e.FullPath));
                    mView.UpdateViewStatus(Enumerations.FinderViewStatus.NewTestAdded);
                }
            }
            catch
            {
                /* Do nothing. Ignore failure to track changes to dir */
            }
        }
        #endregion
    }

    public class TestLoadingStatus
    {
        public static int WAIT = 1000;
        private int mNumLoadersNeeded = 0;
        private int mNumLoadersDone = 0;
        private const string READY = "Ready";
        private const string NOT_READY = "Page resources are not fully loaded. To ensure accurate results, wait for status to turn Ready.";

        public bool IsLoadingDone()
        {
            return mNumLoadersDone == mNumLoadersNeeded;
        }

        public void LoaderFinished()
        {
            mNumLoadersDone++;
        }

        public string LoadProgress
        {
            get
            {
                return IsLoadingDone() ? READY : NOT_READY;
            }
        }

        public int TestLoaderCountNeeded(int numFiles)
        {
            int ret = 1;
            if (numFiles <= 500)
            {
                ret = 1;
            }
            else if (numFiles <= 1000)
            {
                ret = 2;
            }
            else if (numFiles <= 3000)
            {
                ret = 3;
            }
            else if (numFiles <= 5000)
            {
                ret = 4;
            }
            else
            {
                ret = 5;
            }
            mNumLoadersNeeded = ret;
            return ret;
        }
    }
}