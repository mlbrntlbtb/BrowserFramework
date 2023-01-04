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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.Common;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using TestRunner.Designer.View;

namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for TagBasket.xaml
    /// </summary>
    public partial class TagBasket : Window, ITagView
    {
        #region PUBLIC METHODS
        public TagPresenter mTagPresenter { get; set; }
        #endregion

        #region PRIVATE MEMBERS
        private List<DlkTag> mAllTags;
        private List<DlkTag> mSelectedTags;
        private List<DlkTag> mAvailableTags;
        private List<DlkTag> mOriginalTags;
        private List<DlkTest> mLoadedTests;

        private List<TLSuite> mLoadedSuites;
        private KwDirItem SelectedNode;
        private int mFilesUpdated;
        private bool TagChanged;
        private bool TagAdded = false;
        private List<DlkTag> mTagsToAdd = new List<DlkTag>();
        private List<DlkTag> mConflictTags = new List<DlkTag>();
        private List<DlkTest> mSelectedTests = new List<DlkTest>();
        private List<DlkTest> mTestsFromDir = new List<DlkTest>();
        private bool mOverwriteReadOnly = true;
        #endregion

        #region ITagView
        public List<DlkTag> AllTags
        {
            get
            {
                return mAllTags;

            }
            set
            {
                mAllTags = value;
            }
        }
        public List<DlkTag> AvailableTags
        {
            get
            {
                return mAvailableTags;
            }
            set
            {
                mAvailableTags = value;
                lbxAvailableTags.ItemsSource = mAvailableTags;
                lbxAvailableTags.Items.Refresh();
            }
        }
        public List<DlkTag> SelectedTags
        {
            get
            {
                return mSelectedTags;

            }
            set
            {
                mSelectedTags = value;
                lbxCurrentTags.ItemsSource = mSelectedTags;
                lbxCurrentTags.Items.Refresh();
            }
        }
        public List<DlkTag> TagsToAdd
        {
            get
            {
                return mTagsToAdd;

            }
            set
            {
                mTagsToAdd = value;
            }
        }
        public List<DlkTest> LoadedTests
        {
            get
            {
                return mLoadedTests;

            }
            set
            {
                mLoadedTests = value;
            }
        }
        public int FilesUpdated
        {
            get
            {
                return mFilesUpdated;

            }
            set
            {
                mFilesUpdated = value;
            }
        }
        public List<TLSuite> LoadedSuites
        {
            get
            {
                return mLoadedSuites;

            }
            set
            {
                mLoadedSuites = value;
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="Tags"></param>
        /// <param name="Tests"></param>
        public TagBasket(KwDirItem Node, List<DlkTag> Tags, List<DlkTest> Tests)
        {
            SelectedNode = Node;
            AllTags = Tags;
            LoadedTests = Tests;
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            mOriginalTags = new List<DlkTag>(mAllTags);
            mTagPresenter = new TagPresenter(this);
            tbxTagPath.Text = SelectedNode.Path;
            /* Load Current Tags */
            mTagPresenter.LoadCurrentTags(SelectedNode);
            AvailableTags = mAllTags.Except(SelectedTags).ToList();
        }
        #endregion

        #region PRIVATE METHODS

        private List<DlkTag> BuildConflicts(string conflictIDs, List<DlkTag> currentTags)
        {
            List<DlkTag> tagList = new List<DlkTag>();
            if (conflictIDs != String.Empty)
            {
                List<string> IDs = conflictIDs.Trim(';').Split(';').ToList();
                foreach (string ID in IDs)
                {
                    tagList.Add(currentTags.Find(x => x.Id == ID));
                }
            }
            return tagList;
        }

        /// <summary>
        /// This will prompt the user with a notification if the file is read only.
        /// </summary>
        /// <param name="SelectedTest">Test file or folder/directory</param>
        /// <returns>True if the file will be overwritten, False if not</returns>
        private bool TrySaveTest(KwDirItem SelectedTest)
        {
            // Retrieve suites list
            GetTests(SelectedTest);

            foreach (DlkTest test in mSelectedTests)
            {
                FileInfo fi = new FileInfo(test.mTestPath);

                if (fi.IsReadOnly)
                {
                    if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_OVERWRITE_READ_ONLY_FILES) == MessageBoxResult.No)
                    {
                        mOverwriteReadOnly = false;
                        break;
                    }
                    mOverwriteReadOnly = true;
                    break;
                }
            }
            return mOverwriteReadOnly;
        }

        /// <summary>
        /// Retrieves all tests of selected folder/directory.
        /// </summary>
        private void GetTests(KwDirItem SelectedNode)
        {
            if (System.IO.Path.GetFullPath(SelectedNode.Path).StartsWith(DlkEnvironment.mDirTests))
            {
                if (DlkTest.IsValidTest(SelectedNode.Path))
                {
                    DlkTest mTest = LoadedTests.First(test => test.mTestPath == SelectedNode.Path);
                    mSelectedTests.Add(mTest);
                }
                else
                {
                    if (SelectedNode.GetType() == typeof(KwFolder)) // To include all subfolders in selected folder
                    {
                        KwFolder folder = (KwFolder)SelectedNode;
                        mSelectedTests = RecurseTestFolders(folder);
                    }
                    else
                    {
                        mSelectedTests = LoadedTests.Where(test => System.IO.Path.GetDirectoryName(test.mTestPath) == System.IO.Path.GetFullPath(SelectedNode.Path)).ToList();
                    }
                }
            }
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
                    DlkTest mTest = LoadedTests.First(test => test.mTestPath == itm.Path);
                    mTestsFromDir.Add(mTest);
                }
            }

            return mTestsFromDir;
        }

        #endregion

        #region EVENT HANDLERS
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TagChanged)
                {
                    if (DlkUserMessages.ShowQuestionYesNoWarning("All changes will be lost. Proceed?")
                        == MessageBoxResult.Yes)
                        if (TagAdded)
                        {
                            this.DialogResult = true;
                        }
                        this.Close();
                }
                if (TagAdded)
                {
                    this.DialogResult = true;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TagChanged)
                {
                    if (TrySaveTest(SelectedNode))
                    {
                        mTagPresenter.UpdateTags();
                        DlkUserMessages.ShowInfo(FilesUpdated + DlkUserMessages.INF_FILES_UPDATED);
                    }
                }
                if (TagAdded)
                {
                    this.DialogResult = true;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
        #endregion

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbxAvailableTags.SelectedItem != null && !SelectedTags.Contains((DlkTag)lbxAvailableTags.SelectedItem))
                {
                    DlkTag tagToAdd = (DlkTag)lbxAvailableTags.SelectedItem;
                    foreach (DlkTag tag in mOriginalTags)
                    {
                        if (BuildConflicts(tag.Conflicts, mOriginalTags).Contains(tagToAdd) && SelectedTags.Contains(tag))
                        {
                            SelectedTags.Remove(tag);
                            if (!AvailableTags.Contains(tag))
                            {
                                AvailableTags.Add(tag);
                            }
                        }
                    }
                    mTagPresenter.AddTag(lbxAvailableTags.SelectedItems.Cast<DlkTag>().ToList());
                    TagChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbxCurrentTags.SelectedItem != null) 
                {
                    DlkTag tagToRemove = (DlkTag)lbxCurrentTags.SelectedItem;
                    mTagPresenter.RemoveTag(lbxCurrentTags.SelectedItems.Cast<DlkTag>().ToList());
                    TagChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddNewTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTag newTag = new DlkTag(string.Empty, string.Empty, string.Empty);
                AddEditTag dlg = new AddEditTag(newTag, mAllTags);
                dlg.Owner = this;
                if ((bool)dlg.ShowDialog())
                {
                    TagsToAdd.Add(newTag);
                    mTagPresenter.AddNewTags();
                    if ((bool)dlg.DialogResult)
                    {
                        AvailableTags.Add(newTag);
                        lbxAvailableTags.Items.Refresh();
                        TagAdded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TagAdded)
            {
                this.DialogResult = true;
            }
        }

    }
}
