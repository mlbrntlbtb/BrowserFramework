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
    /// Interaction logic for TagBasket.xaml
    /// </summary>
    public partial class ManageTags : Window
    {
        #region PUBLIC METHODS
        #endregion

        #region PRIVATE MEMBERS
        private DlkTest mTest = null;
        private List<DlkTag> mAllTags;
        private List<DlkTag> mCurrentTags;
        private List<DlkTag> mOriginalTags;
        private List<DlkTag> mConflictTags = new List<DlkTag>();
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

        #region ITagView
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="Tags"></param>
        /// <param name="Tests"></param>
        public ManageTags(List<DlkTag> AllTags, List<DlkTag> CurrentTags, bool IsAdmin = false, DlkTest Test = null)
        {
            InitializeComponent();
            mTest = Test;
            mAllTags = AllTags;
            mOriginalTags = new List<DlkTag>(AllTags);
            mCurrentTags = CurrentTags;
            isAdmin = IsAdmin;
            Initialize();
        }

        public void Initialize()
        {
            if (mTest != null) // Check for TL as this has a side effect in the query rows
            {
                mCurrentTags = mAllTags.FindAll(x => mCurrentTags.Any(y => y.Id == x.Id));
            }

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
                if (mDirty && mTest != null)
                {
                    //Perform save of loaded test
                    SaveTestDialog dlg = new SaveTestDialog(mTest);
                    bool? bSaveAndCheckin;
                    string filePath;
                    dlg.Owner = this;
                    dlg.SaveTest(out bSaveAndCheckin, out filePath);
                    IsSaveAndCheckIn = bSaveAndCheckin;
                    if (IsSaveAndCheckIn is bool) /* should be NULL if user aborted */
                    {
                        mTest.mTags = mCurrentTags;
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
                        if (mCurrentTags.Count >= 1)
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

    public class ListBoxContainsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value.ToString() != "0" ? Visibility.Hidden : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ListBoxItemContainsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string tagid = values[0].ToString();
            string tagcontains = values[1].ToString();
            if (tagid == "0")
            {
                return "[Contains] " + tagcontains;
            }
            else
            {
                return tagcontains;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
 