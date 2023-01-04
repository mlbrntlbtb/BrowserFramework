using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TestRunner.Common;
using TestRunner.Designer.Model;
using TestRunner.Designer.View;

namespace TestRunner.Designer.Presenter
{
    public class TagPresenter
    {
        #region PRIVATE MEMBERS
        private ITagView mView;
        private List<DlkTest> TestsFromPath = new List<DlkTest>();
        private List<TLSuite> SuitesFromPath = new List<TLSuite>();
        private List<DlkTag> mInitialTags = new List<DlkTag>();
        private List<TLSuite> suitesFromDir = new List<TLSuite>();
        private List<DlkTest> testsFromDir = new List<DlkTest>();
        private static string mTagsFile = Path.Combine(DlkEnvironment.mDirFramework, @"Library\Tags\tags.xml");
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor Method
        /// </summary>
        public TagPresenter(ITagView view)
        {
            mView = view;
        }

        /// <summary>
        /// Loads all tags for selected test/folder.
        /// </summary>
        public void LoadCurrentTags(KwDirItem SelectedNode)
        {
            List<DlkTag> AllTags = mView.AllTags;
            List<DlkTag> CurrTags = new List<DlkTag>();
            //If given path is a test or a folder from tests
            if (Path.GetFullPath(SelectedNode.Path).StartsWith(DlkEnvironment.mDirTests))
            {
                if (DlkTest.IsValidTest(SelectedNode.Path))
                {
                    DlkTest mTest = mView.LoadedTests.First(test => test.mTestPath == SelectedNode.Path);
                    TestsFromPath.Add(mTest);
                    CurrTags = AllTags.Where(x => mTest.mTags.Any(y => x.Id == y.Id)).ToList();
                }

                else
                {
                    if (SelectedNode.GetType() == typeof(KwFolder)) // To include all subfolders in selected folder
                    {
                        KwFolder folder = (KwFolder)SelectedNode;
                        TestsFromPath = RecurseTestFolders(folder);
                    }
                    else
                    {
                        TestsFromPath = mView.LoadedTests.Where(test => Path.GetDirectoryName(test.mTestPath) == Path.GetFullPath(SelectedNode.Path)).ToList();
                    }
                    //TestsFromPath = mView.LoadedTests.Where(test => Path.GetFullPath(test.mTestPath).StartsWith(SelectedNode.Path)).ToList();
                    var tags = TestsFromPath.Select(x => x.mTags).ToList(); //Get all tags
                    // Get only the common tags in all tests
                    var common = tags.SelectMany(list => list.Distinct())
                                .GroupBy(item => item.Id)
                                .Select(group => new { Count = group.Count(), Item = group.Key })
                                .Where(item => item.Count == TestsFromPath.Count()).ToList();
                    CurrTags = AllTags.Where(x => common.Any(y => x.Id == y.Item)).ToList();
                }
            }

            CurrTags = CurrTags.OrderBy(x => x.Name).ToList();
            mInitialTags = CurrTags;
            mView.SelectedTags = CurrTags;
        }

        /// <summary>
        /// Loads all tags for selected suite/folder.
        /// </summary>
        public void LoadCurrentSuiteTags(KwDirItem SelectedNode)
        {
            List<DlkTag> AllTags = mView.AllTags;
            List<DlkTag> CurrTags = new List<DlkTag>();

            if (Path.GetFullPath(SelectedNode.Path).StartsWith(DlkEnvironment.mDirTestSuite))
            {
                if (DlkTestSuiteXmlHandler.IsValidFormat(SelectedNode.Path))
                {
                    TLSuite mSuite = mView.LoadedSuites.First(suite => suite.path == SelectedNode.Path);
                    SuitesFromPath.Add(mSuite);
                    CurrTags = AllTags.Where(x => mSuite.SuiteInfo.Tags.Any(y => x.Id == y.Id)).ToList();
                }
                else
                {
                    if (SelectedNode.GetType() == typeof(BFFolder)) // To include all subfolders in selected folder
                    {
                        BFFolder folder = (BFFolder)SelectedNode;
                        SuitesFromPath = RecurseFolders(folder);
                    }
                    else
                    {
                        SuitesFromPath = mView.LoadedSuites.Where(suite => Path.GetDirectoryName(suite.path) == Path.GetFullPath(SelectedNode.Path)).ToList();
                    }
                    var tags = SuitesFromPath.Select(x => x.SuiteInfo.Tags).ToList(); // Get all tags
                    // Get only the common tags in all suites
                    var common = tags.SelectMany(list => list.Distinct())
                                .GroupBy(item => item.Id)
                                .Select(group => new { Count = group.Count(), Item = group.Key })
                                .Where(item => item.Count == SuitesFromPath.Count()).ToList();
                    CurrTags = AllTags.Where(x => common.Any(y => x.Id == y.Item)).ToList();
                }
            }

            CurrTags = CurrTags.OrderBy(x => x.Name).ToList();
            mInitialTags = CurrTags;
            mView.SelectedTags = CurrTags;
        }

        /// <summary>
        /// Adds tag to test
        /// </summary>
        public void AddTag(List<DlkTag> CurrTags)
        {
            List<DlkTag> TempAvailable = mView.AvailableTags.Except(CurrTags).ToList();
            List<DlkTag> TempSelected = mView.SelectedTags;
            TempSelected.AddRange(CurrTags);

            /* Sort List */
            TempAvailable = TempAvailable.OrderBy(x => x.Name).ToList();
            TempSelected = TempSelected.OrderBy(x => x.Name).ToList();

            mView.SelectedTags = TempSelected;
            mView.AvailableTags = TempAvailable;
        }

        /// <summary>
        /// Removes tag from test
        /// </summary>
        public void RemoveTag(List<DlkTag> CurrTags)
        {
            List<DlkTag> TempAvailable = mView.AvailableTags;
            List<DlkTag> TempSelected = mView.SelectedTags.Except(CurrTags).ToList();
            TempAvailable.AddRange(CurrTags);

            /* Sort List */
            TempAvailable = TempAvailable.OrderBy(x => x.Name).ToList();
            TempSelected = TempSelected.OrderBy(x => x.Name).ToList();

            mView.SelectedTags = TempSelected;
            mView.AvailableTags = TempAvailable;
        }

        /// <summary>
        /// Updates test with selected tags
        /// </summary>
        public void UpdateTags()
        {
            List<DlkTag> TagsToUpdate = mView.SelectedTags;
            int count = 0;
            foreach (DlkTest test in TestsFromPath)
            {
                test.mTags.Clear();
                test.mTags.AddRange(TagsToUpdate);
                if (SaveTest(test))
                    count++;
            }
            mView.FilesUpdated = count;
        }

        /// <summary>
        /// Updates suites with selected tags
        /// </summary>
        public void UpdateSuiteTags()
        {
            List<DlkTag> TagsToUpdate = mView.SelectedTags;
            int count = 0;
            foreach (TLSuite suite in SuitesFromPath)
            {
                suite.SuiteInfo.Tags.Clear();
                suite.SuiteInfo.Tags.AddRange(TagsToUpdate);
                if (SaveSuite(suite))
                    count++;
            }
            mView.FilesUpdated = count;
        }

        /// <summary>
        ///Adds new tag to list of tags
        /// </summary>
        public void AddNewTags()
        {
            List<DlkTag> Available = mView.AvailableTags;
            mView.AllTags.AddRange(mView.TagsToAdd); // Add
            mView.TagsToAdd.Clear(); // Flush
            SaveTags(); // Write
        }
        #endregion

        #region PRIVATE METHODS
        private void SaveTags()
        {
            List<XElement> lstTagsNode = new List<XElement>();

            foreach (DlkTag tag in mView.AllTags)
            {
                XElement tagnode = new XElement("tag",
                    new XAttribute("id", tag.Id),
                    new XAttribute("name", tag.Name),
                    new XAttribute("conflicts", tag.Conflicts),
                    new XAttribute("description", tag.Description)
                    );
                lstTagsNode.Add(tagnode);
            }
            XElement mRoot = new XElement("tags", lstTagsNode);
            mRoot.Save(mTagsFile);
        }

        /// <summary>
        /// This will perform the actual saving of the Test file
        /// </summary>
        /// <param name="mTest">Test to be saved</param>
        /// <returns>True if the test was saved, False if not</returns>
        private bool SaveTest(DlkTest mTest)
        {
            FileInfo fi = new FileInfo(mTest.mTestPath);

            if (!fi.IsReadOnly)
            {
                /* Write to test file */
                mTest.WriteTest(mTest.mTestPath, true);
                return true;
            }
            else
            {
                /* Set Read Only to False */
                fi.IsReadOnly = false;
                /* Write to test file */
                mTest.WriteTest(mTest.mTestPath, true);
                return true;
            }
        }

        /// <summary>
        /// This will perform the actual saving of the Suite file
        /// </summary>
        /// <param name="mSuite">Suite file to be saved</param>
        /// <returns>True if the suite was saved, False if not</returns>
        private bool SaveSuite(TLSuite mSuite)
        {
            FileInfo fi = new FileInfo(mSuite.path);

            if (!fi.IsReadOnly)
            {
                /* Write to suite file */
                DlkTestSuiteXmlHandler.Save(mSuite.path, mSuite.SuiteInfo, mSuite.Tests);
                return true;
            }
            else
            {
                /* Set Read Only to False */
                fi.IsReadOnly = false;
                /* Write to suite file */
                DlkTestSuiteXmlHandler.Save(mSuite.path, mSuite.SuiteInfo, mSuite.Tests);
                return true;
            }
        }

        /// <summary>
        /// Retrieve all suites from selected folder recursively
        /// </summary>
        /// <param name="folderDirectory">folder directory</param>
        /// <returns>list of TLSuite objects from the selected folder directory</returns>
        private List<TLSuite> RecurseFolders(BFFolder folderDirectory)
        {
            foreach (KwDirItem itm in folderDirectory.DirItems)
            {
                if (itm.GetType() == typeof(BFFolder))
                {
                    RecurseFolders((BFFolder)itm);
                }
                else
                {
                    TLSuite mSuite = mView.LoadedSuites.First(suite => suite.path == itm.Path);
                    suitesFromDir.Add(mSuite);
                }
            }

            return suitesFromDir;
        }

        /// <summary>
        /// Retrieve all tests from selected folder recursively
        /// </summary>
        /// <param name="folderDirectory">folder directory</param>
        /// <returns>list of DlkTest objects from the selected folder directory</returns>
        private List<DlkTest> RecurseTestFolders(KwFolder folderDirectory)
        {
            foreach (KwDirItem itm in folderDirectory.DirItems)
            {
                if (itm.GetType() == typeof(KwFolder))
                {
                    RecurseTestFolders((KwFolder)itm);
                }
                else
                {
                    DlkTest mTest = mView.LoadedTests.First(test => test.mTestPath == itm.Path);
                    testsFromDir.Add(mTest);
                }
            }

            return testsFromDir;
        }
        #endregion
    }


}
