using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Designer.Model;
using TestRunner.Designer.View;

namespace TestRunner.Designer.Presenter
{
    public class DuplicatePresenter
    {

        #region PUBLIC MEMBERS
        public List<String> Screens = new List<String>();
        public List<DlkTest> loadedTests = new List<DlkTest>();
        #endregion

        #region PRIVATE MEMBERS
        private IDuplicateView mView;

        /// <summary>
        /// DuplicatePresenter Constructor
        /// </summary>
        /// <param name="view"></param>
        public DuplicatePresenter(IDuplicateView view)
        {
            mView = view;
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Finds matches from the given list of test steps and test data
        /// </summary>
        /// <param name="mFinder"></param>
        /// <param name="mTest"></param>
        /// <param name="CheckParameters"></param>
        private List<DlkTestStepRecord> GetMatches(List<DlkTestStepRecord> mFinder, List<DlkTestStepRecord> mCompare, bool CheckParameters)
        {
            List<DlkTestStepRecord> tsrMatch = new List<DlkTestStepRecord>();
            List<DlkTestStepRecord> tsrMatchA = new List<DlkTestStepRecord>();
            List<DlkTestStepRecord> tsrMatchB = new List<DlkTestStepRecord>();

            if (CheckParameters)
            {
                tsrMatchA = mCompare.Where(a => mFinder.Any(b => b.mKeyword == a.mKeyword && b.mControl == a.mControl && b.mScreen == a.mScreen && b.mParameters.Contains(a.mParameterOrigString))).Distinct().ToList();
                tsrMatchB = mFinder.Where(a => mCompare.Any(b => b.mKeyword == a.mKeyword && b.mControl == a.mControl && b.mScreen == a.mScreen && b.mParameters.Contains(a.mParameterOrigString))).Distinct().ToList();
                if (tsrMatchA.Count == tsrMatchB.Count)
                    tsrMatch = tsrMatchA;
            }
            else
            {
                tsrMatchA = mCompare.Where(a => mFinder.Any(b => a.mScreen == b.mScreen && a.mControl == b.mControl && a.mKeyword == b.mKeyword )).Distinct().ToList();
                tsrMatchB = mFinder.Where(a => mCompare.Any(b => a.mScreen == b.mScreen && a.mControl == b.mControl && a.mKeyword == b.mKeyword)).Distinct().ToList();
                if (tsrMatchA.Count == tsrMatchB.Count)
                    tsrMatch = tsrMatchA;          
            }
            return tsrMatch;
        }


        private void SubstituteTest(DlkTest mTest)
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

                    return;
                }
            }
        }

        /// <summary>
        /// Removes excluded test steps from a given list of test step record
        /// </summary>
        /// <param name="mTestToFind"></param>
        /// <param name="testList"></param>
        private List<DlkTestStepRecord> ExcludeSteps(List<DlkTestStepRecord> tsr)
        {
            List<DlkTestStepRecord> mScrubTest = new List<DlkTestStepRecord>();
            mScrubTest = tsr;
            foreach (ExcludedStepRecord exStep in mView.ExcludedSteps)
            {
                if (exStep.includeParameter)
                {
                    mScrubTest = mScrubTest.Except(mScrubTest.Where(x => x.mScreen == exStep.mScreen && x.mControl == exStep.mControl && x.mKeyword == exStep.mKeyword && x.mParameters.Contains(exStep.Parameters))).ToList();
                }
                else
                {
                    mScrubTest = mScrubTest.Except(mScrubTest.Where(x => x.mScreen == exStep.mScreen && x.mControl == exStep.mControl && x.mKeyword == exStep.mKeyword)).ToList();
                }
            }
            return mScrubTest;
        }

        /// <summary>
        /// Finds all duplicates of a given test in a given test list
        /// </summary>
        /// <param name="mTestToFind"></param>
        /// <param name="testList"></param>
        private List<Duplicate> CompareTest(DlkTest mTestToFind, List<DlkTest> testList)
        {
            List<Match> mMatchFound = new List<Match>();
            List<Duplicate> mDuplicatesFound = new List<Duplicate>();

            //Pre process current test
            SubstituteTest(mTestToFind);
            List<DlkTestStepRecord> mStepsToFind = mTestToFind.mTestSteps;
            mStepsToFind = ExcludeSteps(mStepsToFind);
            foreach (DlkTest test in testList)
            {
                SubstituteTest(test);
                List<DlkTestStepRecord> mStepsCompare = test.mTestSteps;
                mStepsCompare = ExcludeSteps(mStepsCompare);
                Match tempMatch = new Match();
                tempMatch.TestName = test.mTestName;
                tempMatch.Path = test.mTestPath;
                tempMatch.StepCount = test.mTestSteps.Count;
                tempMatch.MatchedRows = GetMatches(mStepsToFind, mStepsCompare, mView.IncludeDupParameters);
                tempMatch.MatchCount = tempMatch.MatchedRows.Count;
                tempMatch.IsExactMatch = (tempMatch.MatchCount == mStepsCompare.Count()) && (mStepsCompare.Count() == mStepsToFind.Count()) ? true : false;
                tempMatch.IsExactMatchString = (tempMatch.MatchCount == tempMatch.StepCount) && (tempMatch.StepCount == mView.FinderTest.Count()) ? "Yes" : "No";
                if (tempMatch.IsExactMatch)
                {
                    mMatchFound.Add(tempMatch);
                }
            }

            //Process all current duplicates
            foreach (Match dup in mMatchFound)
            {
                foreach (Match item in mMatchFound.Where(x => x != dup))
                {
                    Duplicate dupFound = new Duplicate();
                    dupFound.TestToFind = dup;
                    dupFound.TestDuplicate = item;
                    mDuplicatesFound.Add(dupFound);
                }
            }
            return mDuplicatesFound;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Go to folder of selected duplicate
        /// </summary>
        /// <param name="TestPath"></param>
        public void GoToFolder(string TestPath)
        {
            System.Diagnostics.Process.Start(Path.GetDirectoryName(TestPath));
        }

        /// <summary>
        /// Adds a new excluded step record
        /// </summary>
        public void AddExcludedStep()
        {
            ExcludedStepRecord exStep = new ExcludedStepRecord(mView.Screens);
            mView.ExcludedSteps.Add(exStep);

            //exStep.step.mScreen = mView.Screens;
            //mView.ExcludedSteps.Add(exStep);
        }

        /// <summary>
        /// Delete selected step record/s from excluded steps
        /// </summary>
        /// <param name="selectedRecords"></param>
        public void DeleteExcludedSteps(List<ExcludedStepRecord> selectedExSteps)
        {
            List<ExcludedStepRecord> esr = mView.ExcludedSteps.Except(selectedExSteps).ToList();
            mView.ExcludedSteps = new ObservableCollection<ExcludedStepRecord>(esr);
        }

        /// <summary>
        /// Clears all the steps of excluded steps
        /// </summary>
        public void ClearExcludedSteps()
        { 
            mView.ExcludedSteps = new ObservableCollection<ExcludedStepRecord>();
        }

        /// <summary>
        /// Finds all duplicates in a given path
        /// </summary>
        /// <param name="TestPaths">Collection of Test Paths to scan</param>
        public void FindDuplicates(String[] TestPaths)
        {
            List<DlkTest> TestsToScan = new List<DlkTest>();
            List<Duplicate> DuplicatesFound = new List<Duplicate>();
            bool isMultipleItems = TestPaths.Count() > 1;

            if (!isMultipleItems && File.Exists(TestPaths.First())) // special case: single selection and file (not folder)
            {
                try
                {
                    TestsToScan = mView.LoadedTests.Where(test => test.mTestPath.Contains(Path.GetDirectoryName(TestPaths.First()))).ToList();
                    DuplicatesFound = CompareTest(mView.LoadedTests.Find(test => test.mTestPath == TestPaths.First()), TestsToScan);
                }
                catch
                {
                    // ignore, return empty duplicates found
                }
            }
            else // multiple selection (covers mix of file and folder)  OR single selection folder
            {
                /* collate tests to compare */
                foreach (string path in TestPaths)
                {
                    if (File.Exists(path)) // File
                    {
                        try
                        {
                            TestsToScan.Add(mView.LoadedTests.Find(test => test.mTestPath == path));
                        }
                        catch
                        {
                            continue; // ignore if not found in all tests, not possible IF all tests are loaded
                        }
                    }
                    else // Directory
                    {
                        TestsToScan.AddRange(mView.LoadedTests.Where(test => Path.GetFullPath(test.mTestPath).StartsWith(path)).ToList());
                    }
                }
                /* remove duplicates, ie: selection includes test and its own folder */
                TestsToScan = TestsToScan.Distinct(new DlkTestCompare()).ToList();

                /* compare each test against the rest of tests in collection */
                foreach (DlkTest currTest in TestsToScan)
                {
                    if (!DuplicatesFound.Exists(rec => rec.TestToFind.Path == currTest.mTestPath))
                    {
                        DuplicatesFound.AddRange(CompareTest(currTest, TestsToScan));
                    }
                }
            }
            mView.DuplicateTests = DuplicatesFound;
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
        #endregion
    }
}
