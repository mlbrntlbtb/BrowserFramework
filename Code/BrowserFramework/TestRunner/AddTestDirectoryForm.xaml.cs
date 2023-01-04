using System.Collections.Generic;
using System.Windows;
using TestRunner.Common;
using CommonLib.DlkSystem;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using CommonLib.DlkHandlers;
using MessageBox = System.Windows.MessageBox;
using System;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for FilterTestDirectoryForm.xaml
    /// </summary>
    public partial class AddTestDirectoryForm : Window
    {
        #region DECLARATIONS

        private string m_currentFolderPath;
        private string m_FolderName;
        private List<BFFolder> m_TestFolders = new List<BFFolder>();
        private DlkTestSuiteLoader m_Loader = new DlkTestSuiteLoader();
        private bool? mIsSaveAndCheckin;
        private string mSavedFolderPath;
        #endregion

        #region PROPERTIES

        public void CheckOutFromSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckOut(parameters.First().ToString(), parameters.Last().ToString());
        }

        public void AddToSource(object[] parameters)
        {
            DlkSourceControlHandler.Add(parameters.First().ToString(), parameters.Last().ToString());

        }

        public void CheckInToSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckIn(parameters.First().ToString(), parameters.Last().ToString());
        }

        public List<BFFolder> TestFolders
        {
            get
            {
                m_TestFolders.Clear();
                DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTests);
                BFFolder root = new BFFolder
                {
                    Name = di.Name,
                    Path = di.FullName,
                    DirItems = m_Loader.GetSuiteDirectories(DlkEnvironment.mDirTests),
                    IsSelected = false,
                    IsExpanded = true
                };
                m_TestFolders.Add(root);
                SetIsSelected(root);
                return m_TestFolders;
            }
        }

        private bool SetIsSelected(BFFolder Folder)
        {
            if (Path.Equals(Folder.Path, m_currentFolderPath))
            {
                Folder.IsSelected = true;
                return true;
            }
            else
            {
                foreach (BFFolder item in Folder.DirItems)
                {
                    if (Path.Equals(item.Path, m_currentFolderPath))
                    {
                        item.IsSelected = true;
                        return true;
                    }
                    else
                    {
                        if (SetIsSelected(item))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region CONSTRUCTOR

        public AddTestDirectoryForm(string currentFolderPath)
        {
            InitializeComponent();
            m_currentFolderPath = currentFolderPath;
            tvwFoldersExisting.ItemsSource = TestFolders;
        }

        #endregion

        #region EVENTS

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tvwFoldersExisting.Focus();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //create folder
                if (!Validate(out bool isDuplicate))
                {
                    DlkUserMessages.ShowError(isDuplicate ?
                         DlkUserMessages.ERR_FOLDER_NAME_ALREADY_EXISTS : DlkUserMessages.ERR_FOLDERNAME_INVALID);
                }
                else
                {
                    //check if source control is selected
                    SaveAndCheckInFolderToSource(out mIsSaveAndCheckin, out mSavedFolderPath);
                    if (mIsSaveAndCheckin != null)
                    {
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Show this dialog and set required parameters by caller
        /// </summary>
        /// <param name="IsSaveAndCheckin">>Out parameter to get save action</param>
        /// <param name="SaveFilePath">Out parameter to get save path</param>
        public void ShowDialog(out bool? IsSaveAndCheckin, out string SaveFolderPath)
        {
            this.ShowDialog();
            IsSaveAndCheckin = mIsSaveAndCheckin;
            SaveFolderPath = mSavedFolderPath;
        }

        public void DisplayFolderAddedInfo()
        {
            MessageBox.Show("Folder added: " + m_currentFolderPath + @"\" + m_FolderName, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tvwFoldersExisting_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                txtCurrentPath.Text = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                if (Validate(out bool isDuplicate))
                {
                    //if (((BFFolder)tvwFoldersExisting.SelectedItem).Path.)
                    m_currentFolderPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                    m_FolderName = txtFolderName.Text;
                    m_currentFolderPath = m_currentFolderPath.TrimEnd(Path.DirectorySeparatorChar);
                    txtCurrentPath.Text = m_currentFolderPath + @"\" + m_FolderName;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void TxtFolderName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                m_currentFolderPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                m_FolderName = txtFolderName.Text;
                m_currentFolderPath = m_currentFolderPath.TrimEnd(Path.DirectorySeparatorChar);
                txtCurrentPath.Text = m_currentFolderPath + @"\" + m_FolderName;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool Validate(out bool IsDuplicate)
        {
            IsDuplicate = false;

            if (string.IsNullOrEmpty(txtFolderName.Text) || string.IsNullOrWhiteSpace(txtFolderName.Text))
            {
                return false;
            }
            if (txtFolderName.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return false;
            }
            if (Directory.Exists(Path.Combine(m_currentFolderPath, m_FolderName.Trim())))
            {
                IsDuplicate = true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Save folder and check-in to server 
        /// </summary>
        /// <param name="IsSaveAndCheckIn">Output param to inform caller if user wants to checkin</param>
        /// <param name="SavedFolderPath">Output param to inform caller of save path</param>
        private void SaveAndCheckInFolderToSource(out bool? IsSaveAndCheckIn, out string SavedFolderPath)
        {
            IsSaveAndCheckIn = null;
            SavedFolderPath = string.Empty;
            string newFolder = m_currentFolderPath + @"\" + m_FolderName;

            /* Source control is NOT enabled */
            if (!DlkSourceControlHandler.SourceControlEnabled)
            {
                SavedFolderPath = newFolder;
                IsSaveAndCheckIn = false;

                if (!Directory.Exists(newFolder))
                {
                    DirectoryInfo di = new DirectoryInfo(m_currentFolderPath);
                    di.Attributes &= ~FileAttributes.ReadOnly;

                    SavedFolderPath = newFolder;
                    IsSaveAndCheckIn = false;
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_FOLDER_NAME_ALREADY_EXISTS);
                }
            }
            else /* Source control is enabled */
            {
                CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_CHECK_IN_SOURCE_CONTROL_FILE_AFTER_SAVING);

                /* The dialog is visible, this should be the parent form */
                ciscDialog.Owner = this.IsVisible ? this : this.Owner;
                ciscDialog.ShowDialog();

                if (!Directory.Exists(newFolder))
                {
                    DirectoryInfo di = new DirectoryInfo(m_currentFolderPath);
                    di.Attributes &= ~FileAttributes.ReadOnly;

                    SavedFolderPath = newFolder;
                    switch(ciscDialog.SaveCheckinChoice)
                    {
                        case CheckinSourceControlDialog.SaveCheckinOptions.SaveAndCheckin:
                            IsSaveAndCheckIn = true;
                            break;
                        case CheckinSourceControlDialog.SaveCheckinOptions.SaveOnly:
                            IsSaveAndCheckIn = false;
                            break;
                        default: /* default is abort */
                            break;
                    }
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_FOLDER_NAME_ALREADY_EXISTS);
                }
            }
        }
        #endregion
    }
}
