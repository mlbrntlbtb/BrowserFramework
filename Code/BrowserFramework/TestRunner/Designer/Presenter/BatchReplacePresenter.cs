using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using TestRunner.Designer.Model;
using TestRunner.Designer.View;

namespace TestRunner.Designer.Presenter
{
    public class BatchReplacePresenter
    {
        #region PRIVATE MEMBERS
        private IBatchReplaceView mView = null;
        #endregion
           
        #region PUBLIC MEMBERS
        public List<DlkTest> LoadedTests { get; set; }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Save test
        /// </summary>
        /// <param name="mTest">Test to save</param>
        /// <returns></returns>
        private bool SaveTest(DlkTest mTest)
        {
            FileInfo fi = new FileInfo(mTest.mTestPath);

            if (!fi.IsReadOnly)
            {
                /* Write to test file */
                mTest.WriteTest(mTest.mTestPath, true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TargetTest">Test to replace param values from data</param>
        /// <param name="ValueToFind">Text to find in params</param>
        /// <param name="NewValue">New value to replace</param>
        /// <param name="IsPartialMatch">TRUE if partial mtch search only, FALSE if full match</param>
        /// <param name="ColumnHeader">Column header in TRD file to look value from</param>
        /// <param name="Instance">-1 if all instances, otherwise target instance to look values from</param>
        private void ReplaceDataValue(DlkTest TargetTest, string ValueToFind, string NewValue, bool IsPartialMatch, string ColumnHeader, int Instance = -1)
        {
            var targetData = TargetTest.Data;
            if (targetData != null)
            {
                var dr = targetData.Records.FirstOrDefault(x => x.Name == ColumnHeader);
                if (dr == null)
                {
                    return;
                }
                if (Instance == -1) // ALL
                {
                    dr.Values = dr.Values.Select(x => ReplaceAt(TargetTest, x, ValueToFind, NewValue, !IsPartialMatch, dr.Values.IndexOf(x) + 1)).ToList();
                }
                else if (Instance > 0 && Instance <= dr.Values.Count) // Just 1
                {
                    dr.Values[Instance - 1] = ReplaceAt(TargetTest, dr.Values[Instance - 1], ValueToFind, NewValue, !IsPartialMatch, Instance);
                }
            }
            /* else No data file, nothing to replace */
        }

        /// <summary>
        /// Performs the actual replacing of text from the test steps
        /// </summary>
        /// <param name="Source">Source value - whether Parameter or Control</param>
        /// <param name="FindText">Text to search for</param>
        /// <param name="ReplaceText">Text to replace the searched text</param>
        /// <param name="ExactMatch">True or False - if Exact Match setting is checked</param>
        /// <param name="Instance">-1 if all instances should be cached to UpdatedTests, otherwise actual instance</param>
        /// <returns>The resulting string after replace or the same string if no replace was performed</returns>
        private string ReplaceAt(DlkTest TargetTest, string Source, string FindText, string ReplaceText, bool ExactMatch, int Instance)
        {
            string result = Source;
            var replace = (!ExactMatch && Source.Contains(FindText)) || (ExactMatch && Source == FindText);
            if (replace)
            {
                int Place = Source.IndexOf(FindText);
                result = Source.Remove(Place, FindText.Length).Insert(Place, ReplaceText);

                /* Cache tests for saving */
                if (!mView.TestsForSaving.Any(x => x.mTestPath == TargetTest.mTestPath))
                {
                    mView.TestsForSaving.Add(TargetTest);
                }

                /* Cache to list of testpath:instance modified */
                /* Ensure UpdatedTests include all instances of Test if Instance is -1 */
                if (Instance == -1)
                {
                    for (int i = 1; i <= TargetTest.mInstanceCount; i++)
                    {
                        var updateKey = TargetTest.mTestPath + ":" + i;
                        if (!mView.UpdatedTests.Contains(updateKey))
                        {
                            mView.UpdatedTests.Add(updateKey);
                        }
                    }
                }
                else
                {
                    var updateKey = TargetTest.mTestPath + ":" + Instance;
                    if (!mView.UpdatedTests.Contains(updateKey))
                    {
                        mView.UpdatedTests.Add(updateKey);
                    }
                }
            }
            return result;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">View</param>
        public BatchReplacePresenter(IBatchReplaceView view)
        {
            this.mView = view;
        }

        /// <summary>
        /// Loads all available tests
        /// </summary>
        public void LoadAllTests()
        {
            LoadedTests = new List<DlkTest>();
            if (mView.IsSuite)
            {
                foreach (DlkExecutionQueueRecord eqr in mView.TestsInSuite)
                {
                    if (DlkTest.IsValidTest(eqr.fullpath))
                    {
                        var newTest = new DlkTest(eqr.fullpath);
                        int inst = 1;
                        if (!int.TryParse(eqr.instance, out inst))
                        {
                            inst = 1;
                        }
                        newTest.mTestInstanceExecuted = inst;
                        /* Same tests should hold only 1 reference to TRD file, to avoid overwriting of previous replace */
                        DlkTest sameTestInList = LoadedTests.FirstOrDefault(x => x.mTestPath == newTest.mTestPath);
                        if (sameTestInList != null)
                        {
                            newTest.Data = sameTestInList.Data;
                        }
                        LoadedTests.Add(newTest);
                    }
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(mView.SelectedTestDirPath);
                FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);

                Parallel.ForEach(mFiles, (test) =>
                {
                    if (DlkTest.IsValidTest(test.FullName))
                    {
                        DlkTest newTest = new DlkTest(test.FullName);
                        newTest.mTestInstanceExecuted = 1;
                        /* Same tests should hold only 1 reference to TRD file, to avoid overwriting of previous replace */
                        DlkTest sameTestInList = LoadedTests.FirstOrDefault(x => x.mTestPath == newTest.mTestPath);
                        if (sameTestInList != null)
                        {
                            newTest.Data = sameTestInList.Data;
                        }
                        LoadedTests.Add(newTest);
                    }
                });
            }
        }

        /// <summary>
        /// Loads test steps of selected test
        /// </summary>
        /// <param name="targetTest">target test</param>
        /// <param name="ShowData">Display parameters instead of data driven values. Always set to True for now</param>
        public void LoadSelectedTestSteps(DlkTest targetTest, bool ShowData = true)
        {
            if (ShowData)
            {
                foreach (DlkTestStepRecord step in targetTest.mTestSteps) // set steps to use updated instance
                {
                    step.mCurrentInstance = targetTest.mTestInstanceExecuted;
                }
            }
            mView.SelectedTestSteps = targetTest.mTestSteps;
        }

        /// <summary>
        /// Loads selected test description and containing suites
        /// </summary>
        /// <param name="testPath">Full path of target test</param>
        /// <param name="Instance">Instance of test to load</param
        public void LoadTargetTest(String testPath, int Instance)
        {
            mView.TargetTest = LoadedTests.FirstOrDefault(x => x.mTestPath == testPath && x.mTestInstanceExecuted == Instance);
        }

        /// <summary>
        /// Updates tests from selected directory
        /// </summary>
        public void UpdateTests()
        {
            /* Clear list of updated test. This list is per replace basis, not cumulative */ 
            mView.UpdatedTests.Clear();
            foreach (DlkTest test in LoadedTests)
            {
                List<DlkTestStepRecord> testSteps = test.mTestSteps;
                switch (mView.ReplaceType)
                {
                    case Enumerations.BatchReplaceType.Control:
                        foreach (DlkTestStepRecord rec in testSteps)
                        {
                            rec.mControl = ReplaceAt(test, rec.mControl, mView.TextToSearch, mView.NewText, mView.IsExactMatch, -1);
                        }
                        break;
                    case Enumerations.BatchReplaceType.Parameter:
                        /* get instance of the test used if this is a list of tests from a suite */
                        var instance = mView.IsSuite ? test.mTestInstanceExecuted : -1;

                        /* Do replacement per step */
                        foreach (DlkTestStepRecord rec in testSteps)
                        {
                            string[] paramFields = rec.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                            string temp = string.Empty;

                            /* Do replacement per parameter in parameterstring 'X1|x2|x3' */
                            foreach (string param in paramFields)
                            {
                                if (!DlkData.IsDataDrivenParam(param))
                                {
                                    temp += ReplaceAt(test, param, mView.TextToSearch, mView.NewText, mView.IsExactMatch, -1) + DlkTestStepRecord.globalParamDelimiter;
                                }
                                else
                                {
                                    var paramName = DlkData.GetDataParamName(param);
                                    ReplaceDataValue(test, mView.TextToSearch, mView.NewText, !mView.IsExactMatch, paramName, instance);
                                    temp += param + DlkTestStepRecord.globalParamDelimiter;
                                }
                            }
                            /* replace all params written in DlkTest.mTestSteps, not just the first. these are used on other places instead of TRD values */
                            rec.mParameters = rec.mParameters.Select(x => temp.Substring(0, temp.LastIndexOf(DlkTestStepRecord.globalParamDelimiter))).ToList();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Saves the updated test
        /// </summary>
        /// <returns>True if save is successful else False</returns>
        public bool SaveTests()
        {
            bool ret = false;
            int count = 0;
            try
            {
                foreach (DlkTest test in mView.TestsForSaving)
                {
                    if (SaveTest(test))
                        count++;
                }
                ret = true;
            }
            catch
            {
                /* Do nothing */
            }
            mView.FilesUpdated = count;
            return ret;
        }
        #endregion

    }
}
