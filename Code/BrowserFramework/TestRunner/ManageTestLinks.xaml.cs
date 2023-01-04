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
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ManageTestLinks.xaml
    /// </summary>
    public partial class ManageTestLinks : Window
    {
        #region PRIVATE_MEMBERS
        private DlkTest mTest = null;
        private List<DlkTestLinkRecord> mLstLinks = null;
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

        #region PUBLIC_FUNCTIONS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="Test"></param>
        public ManageTestLinks(DlkTest Test)
        {
            InitializeComponent();
            mTest = Test;
            mLstLinks = Test.mLinks.Select(x => x.Clone()).ToList();
            Initialize();
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        /// <summary>
        /// Load initial values/resources
        /// </summary>
        private void Initialize()
        {
            /* Load bindings */
            dgLinks.ItemsSource = mLstLinks;
        }

        /// <summary>
        /// Refresh UI, usually after every edit
        /// </summary>
        private void Refresh(int SelectedIndex = -1)
        {
            dgLinks.Items.Refresh();
            dgLinks.SelectedIndex = dgLinks.Items.Count > 0 ? SelectedIndex : -1;
            UpdateButtonStates();
        }

        /// <summary>
        /// Update toolbar button enabled state
        /// </summary>
        private void UpdateButtonStates()
        {
            /* UI Enabled State changes */
            btnEditRow.IsEnabled = dgLinks.SelectedIndex >= 0;
            btnDeleteRow.IsEnabled = btnEditRow.IsEnabled;
            btnMoveUp.IsEnabled = dgLinks.SelectedIndex > 0;
            btnMoveDown.IsEnabled = dgLinks.SelectedIndex < dgLinks.Items.Count - 1;
        }

        /// <summary>
        /// Add/Edit selected link
        /// </summary>
        /// <param name="Record">Selected link record</param>
        /// <param name="IsAdd">TRUE if for Add, FALSE for edit</param>
        private void AddEditLink(DlkTestLinkRecord Record, bool IsAdd)
        {
            AddEditTestLink dlg = new AddEditTestLink(Record, mLstLinks);
            dlg.Owner = this;

            if ((bool)dlg.ShowDialog())
            {
                if (IsAdd)
                {
                    mLstLinks.Add(Record);
                    Refresh(mLstLinks.Count - 1);
                }
                else
                {
                    Refresh(dgLinks.Items.IndexOf(Record));
                }

                mDirty = true;
            }
        }

        /// <summary>
        /// Check URL format if isValid
        /// </summary>
        /// <param name="URL">URL to check</param>
        /// <returns></returns>
        private bool ValidateURL(String URL)
        {
            Uri uriResult;
            bool isURLValid = Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
            && Uri.IsWellFormedUriString(URL, UriKind.Absolute);
            return isURLValid;
        }

        /// <summary>
        /// Validate link input if valid local/network path or valid HTTP uri string
        /// </summary>
        /// <param name="input">Input link string</param>
        /// <returns></returns>
        private bool ValidateLink(string input)
        {
            if (System.IO.Path.IsPathRooted(input)) // Valid local/network path
            {
                return true;
            }
            if (!input.StartsWith("http")) // Append HTTP as default protocol if string is not valid abolsute URI
            {
                input = "http://" + input;
            }
            return DlkString.IsValidUriString(input);
        }
        #endregion

        #region EVENT_HANDLERS
        /// <summary>
        /// Handler for Add click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTestLinkRecord rec = new DlkTestLinkRecord(string.Empty, string.Empty);
                AddEditLink(rec, true);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Edit click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnEditRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTestLinkRecord rec = dgLinks.SelectedItem as DlkTestLinkRecord;
                AddEditLink(rec, false);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Move up click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgLinks.SelectedIndex <= 0)
                {
                    return;
                }
                DlkTestLinkRecord src = dgLinks.SelectedItem as DlkTestLinkRecord;
                int srcIdx = dgLinks.SelectedIndex;
                int destIdx = dgLinks.SelectedIndex - 1; // new index is 1 place UP
                mLstLinks.RemoveAt(srcIdx);
                mLstLinks.Insert(destIdx, src);
                Refresh(destIdx); // Current selection is new index
                mDirty = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Move down click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgLinks.SelectedIndex >= dgLinks.Items.Count)
                {
                    return;
                }
                DlkTestLinkRecord src = dgLinks.SelectedItem as DlkTestLinkRecord;
                int srcIdx = dgLinks.SelectedIndex;
                int destIdx = dgLinks.SelectedIndex + 1; // new index is 1 place DOWN
                mLstLinks.RemoveAt(srcIdx);
                mLstLinks.Insert(destIdx, src);
                Refresh(destIdx); // Current selection is new index
                mDirty = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Delete click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_TEST_LINK) == MessageBoxResult.Yes)
                {
                    int idx = dgLinks.SelectedIndex;
                    mLstLinks.RemoveAt(idx);
                    // Current selection is 1 place UP of deleted, unless deleted is first row, in which case selection will be first row
                    Refresh(dgLinks.Items.Count > 0 ? idx - (idx == 0 ? 0 : 1) : -1);
                    mDirty = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for OK click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mDirty)
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
                        mTest.mLinks = mLstLinks;
                    }
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Cancel click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Handler for grid selection changed
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void dgLinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for window loaded
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Load defaults */
                if (mLstLinks.Any())
                {
                    dgLinks.SelectedIndex = 0; // select first row
                }
                Refresh(dgLinks.SelectedIndex);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for grid drop
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void dgLinks_Drop(object sender, DragEventArgs e)
        {
            try
            {
                DlkTestLinkRecord rec = null;
                /* Check if drop is file drop */
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null)
                {
                    rec = new DlkTestLinkRecord(string.Empty, files.First());
                }
                /* If drop is string, check if valid local/network path or valid URL */
                else if (e.Data.GetData(DataFormats.Text) != null && ValidateLink(e.Data.GetData(DataFormats.Text).ToString()))
                {
                    rec = new DlkTestLinkRecord(string.Empty, e.Data.GetData(DataFormats.Text).ToString());
                }
                else
                {
                    return; // Don't drop anything
                }
                AddEditLink(rec, true);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for grid drag enter
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void dgLinks_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                /* UI effects if valid drop */
                if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
                {
                    e.Effects = DragDropEffects.Copy;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion



    }
}
