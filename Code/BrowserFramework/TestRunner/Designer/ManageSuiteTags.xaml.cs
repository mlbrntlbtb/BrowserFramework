using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManageSuiteTags.xaml
    /// </summary>
    public partial class ManageSuiteTags : Window
    {
        #region PRIVATE MEMBERS
        private DlkTestSuiteInfoRecord mSuite = null;
        private List<DlkExecutionQueueRecord> mTests = new List<DlkExecutionQueueRecord>();
        private List<DlkTag> mAllTags;
        private List<DlkTag> mCurrentTags;
        private List<DlkTag> mOriginalTags;
        private List<DlkTag> mConflictTags = new List<DlkTag>();
        private String mPath = string.Empty;
        private bool isAdmin;
        private bool mDirty = false;
        #endregion

        #region PUBLIC_MEMBERS
        /// <summary>
        /// Flag accessible to caller to know if user needs to checkin after save
        /// TRUE - Save and Checkin
        /// FALSE - Save Only
        /// NULL - User aborted save operation
        /// </summary>
        public bool? IsSaveAndCheckIn { get; set; }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AllTags"></param>
        /// <param name="CurrentTags"></param>
        /// <param name="SuitePath"></param>
        /// <param name="Suite"></param>
        /// <param name="Tests"></param>
        /// <param name="isAdmin"></param>
        public ManageSuiteTags(List<DlkTag> AllTags, List<DlkTag> CurrentTags, String SuitePath, DlkTestSuiteInfoRecord Suite, List<DlkExecutionQueueRecord> Tests, bool IsAdmin = false)
        {
            InitializeComponent();
            mSuite = Suite;
            mPath = SuitePath;
            mTests = Tests;
            mAllTags = AllTags;
            mOriginalTags = new List<DlkTag>(AllTags);
            mCurrentTags = CurrentTags;
            isAdmin = IsAdmin;
            Initialize();
        }

        public void Initialize()
        {
            mCurrentTags = mAllTags.FindAll(x => mCurrentTags.Any(y => y.Id == x.Id));

            //mAllTags = mAllTags.Except(mCurrentTags).ToList(); 
            // JPV: removed this, not sure if has other implication, but match by ID not by object reference
            // since assumption of Except here is that CurrentTags is just list of items from AllTags, what if they are 2 separate lists?
            mAllTags = mAllTags.FindAll(x => !mCurrentTags.Any(y => y.Id == x.Id));
            if (!isAdmin)
            {
                DlkTag tempTag = new DlkTag("[Add Contains Tag]", "");
                tempTag.Id = "0";
                mAllTags.Insert(0, tempTag);
            }
            lbxAvailableTags.ItemsSource = mAllTags;
            lbxCurrentTags.ItemsSource = mCurrentTags;
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

        #endregion

        #region EVENT HANDLERS
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
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
                if (mDirty)
                {
                    //Perform save of loaded suite
                    SaveSuiteDialogSC dlg = new SaveSuiteDialogSC(mPath, mSuite, mTests, false, mSuite.Description, mSuite.Owner);
                    bool? bSaveAndCheckIn;
                    string filePath;
                    dlg.Owner = this;
                    dlg.SaveSuite(out bSaveAndCheckIn, out filePath);
                    IsSaveAndCheckIn = bSaveAndCheckIn;
                    if (IsSaveAndCheckIn is bool)
                    {
                        mSuite.Tags = mCurrentTags;
                    }
                }
                DialogResult = true;
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
                if (lbxAvailableTags.SelectedItem != null && !mCurrentTags.Contains((DlkTag)lbxAvailableTags.SelectedItem))
                {
                    if (lbxAvailableTags.SelectedIndex == 0 && !isAdmin)
                    {
                        List<string> containsTags = new List<string>();
                        foreach (DlkTag tag in mCurrentTags)
                        {
                            if (tag.Id == "0")
                            {
                                containsTags.Add(tag.Name);
                            }
                        }
                        AddContainsTag ct = new AddContainsTag(containsTags);
                        ct.Owner = this;
                        if ((bool)ct.ShowDialog())
                        {
                            DlkTag containsTagToAdd = new DlkTag(ct.txtName.Text, "");
                            containsTagToAdd.Id = "0";
                            mCurrentTags.Add(containsTagToAdd);
                            lbxCurrentTags.Focus();
                            lbxCurrentTags.Items.Refresh();
                        }
                    }
                    else
                    {
                        DlkTag tagToAdd = (DlkTag)lbxAvailableTags.SelectedItem;
                        if (mCurrentTags.Count >=1 )
                        {
                            foreach (DlkTag tag in mOriginalTags)
                            {
                                if (BuildConflicts(tag.Conflicts, mOriginalTags).Contains(tagToAdd) && mCurrentTags.Contains(tag))
                                {
                                    mCurrentTags.Remove(tag);
                                    if (!mAllTags.Contains(tag))
                                    {
                                        mAllTags.Add(tag);
                                    }
                                }
                            }
                        }
                        mCurrentTags.Add(tagToAdd);
                        mAllTags.Remove(tagToAdd);
                        mCurrentTags.OrderBy(x => x.Name);
                        mAllTags.OrderBy(x => x.Name);
                        lbxCurrentTags.Focus();
                        lbxAvailableTags.Items.Refresh();
                        lbxCurrentTags.Items.Refresh();
                    }
                    mDirty = true;
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
                    if (tagToRemove.Id != "0")
                    {
                        if (!mAllTags.Contains(tagToRemove))
                        {
                            mAllTags.Add(tagToRemove);
                        }
                    }
                    mCurrentTags.Remove((DlkTag)lbxCurrentTags.SelectedItem);
                    mCurrentTags.OrderBy(x => x.Name);
                    mAllTags.OrderBy(x => x.Name);
                    lbxAvailableTags.Items.Refresh();
                    lbxCurrentTags.Items.Refresh();
                    mDirty = true;
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
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lbxAvailableTags_GotFocus(object sender, RoutedEventArgs e)
        {
            btnRemoveTag.IsEnabled = false;
            btnAddTag.IsEnabled = true && lbxAvailableTags.Items.Count > 0;
        }

        private void lbxCurrentTags_GotFocus(object sender, RoutedEventArgs e)
        {
            btnRemoveTag.IsEnabled = true && lbxCurrentTags.Items.Count > 0;
            btnAddTag.IsEnabled = false;
        }

        private void mnuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbxCurrentTags.SelectedIndex >= 0 && !isAdmin)
            {
                DlkTag selectedTag = mCurrentTags[lbxCurrentTags.SelectedIndex] as DlkTag;
                List<string> containsTags = new List<string>();
                foreach (DlkTag tag in mCurrentTags)
                {
                    if (tag.Id == "0" && tag.Name != selectedTag.Name)
                    {
                        containsTags.Add(tag.Name);
                    }
                }
                AddContainsTag ct = new AddContainsTag(containsTags, selectedTag.Name);
                ct.Owner = this;
                if ((bool)ct.ShowDialog())
                {
                    selectedTag.Name = ct.txtName.Text;
                    lbxCurrentTags.Items.Refresh();
                }
            }
        }
    }
}
