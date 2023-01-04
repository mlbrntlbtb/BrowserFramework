using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Designer.View;
using TestRunner.Designer.Model;
using TestRunner.Common;
using System.IO;
using TestRunner.Designer.Presenter;

namespace TestRunner.Designer.Presenter
{
    public class TestSuitesPresenter
    {
        #region PRIVATE MEMBERS
        private ITestSuitesView mView = null;
        private String mSearchSuitesPreviousFilter = "";
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view"></param>
        public TestSuitesPresenter(ITestSuitesView view)
        {
            mView = view;
        }

        /// <summary>
        /// Loads target suite and stores in memory
        /// </summary>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public void LoadTargetSuite(String targetPath, List<TLSuite> PoolOfSuites)
        {
            TLSuite suite = PoolOfSuites.FirstOrDefault(x => x.path == targetPath);
            if (suite != null)
            {
                suite.name = Path.GetFileName(targetPath);
                suite.path = targetPath;
                suite.ListIndex = string.Empty;
                mView.TargetSuite = suite;
            }
        }

        /// <summary>
        /// Loads test steps of selected suite
        /// </summary>
        /// <param name="testPath"></param>
        /// <returns></returns>
        public void LoadSelectedTestSteps(DlkExecutionQueueRecord TargetTest, bool ShowData=true)
        {
            List<DlkTestStepRecord> tsr = new List<DlkTestStepRecord>();
            if (File.Exists(TargetTest.fullpath))
            {
                DlkTest SelectedTest = new DlkTest(TargetTest.fullpath);
                if (ShowData)
                {
                    SelectedTest.mTestInstanceExecuted = Convert.ToInt32(TargetTest.instance);
                    DlkData.SubstituteDataVariables(SelectedTest);
                    DlkData.SubstituteExecuteDataVariables(SelectedTest);
                }
                foreach (DlkTestStepRecord test in SelectedTest.mTestSteps)
                {
                    test.mCurrentInstance = Convert.ToInt32(TargetTest.instance);
                    tsr.Add(test);
                }
            }
            mView.SelectedTestSteps = tsr;
        }

        /// <summary>
        /// Loads test steps of selected test
        /// </summary>
        /// <param name="targetTest"></param>
        /// <returns></returns>
        public void LoadSelectedTestSteps(DlkTest targetTest, bool ShowData = true)
        {
            List<DlkTestStepRecord> tsr = new List<DlkTestStepRecord>();
            DlkTest SelectedTest = new DlkTest(targetTest.mTestPath);
            if (ShowData)
            {
                SelectedTest.mTestInstanceExecuted = 1;
                DlkData.SubstituteDataVariables(SelectedTest);
                DlkData.SubstituteExecuteDataVariables(SelectedTest);
            }
            mView.SelectedTestSteps = SelectedTest.mTestSteps;

        }

        public void SearchSuites(string filterString)
        {
            filterString = filterString.ToLower();

            //if first time to filter (example: if first letter was typed) , filter the entire collection of xml files that 
            //satisfies filter
            if (mView.FilteredSuites.Count == 0 && !string.IsNullOrEmpty(filterString))
            {
                mView.FilteredSuites = SearchForSuites.GlobalCollectionOfSuites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
                mSearchSuitesPreviousFilter = filterString;
            }
            //if not first time to filter
            else if (mView.FilteredSuites.Count > 0 && !string.IsNullOrEmpty(filterString))
            {
                //handler for continued typing, query smaller list every succeeding letter
                if (filterString.Length > mSearchSuitesPreviousFilter.Length && filterString.Contains(mSearchSuitesPreviousFilter))
                {
                    mView.FilteredSuites = mView.FilteredSuites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
                }
                else
                {
                    mView.FilteredSuites = SearchForSuites.GlobalCollectionOfSuites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
                }
                mSearchSuitesPreviousFilter = filterString;
            }
            else if (string.IsNullOrEmpty(filterString))
            {
                //clear filtered test results here if no filter string.
                //this will return a list containing 0 items, making the treeView.datacontext use _testsFolderDirectory
                mView.FilteredSuites.Clear();
                mSearchSuitesPreviousFilter = "";
            }
        }
        #endregion

       
    }
}
