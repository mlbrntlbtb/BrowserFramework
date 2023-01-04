using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SaveSuiteDialog.xaml
    /// </summary>
    public partial class SaveTestDialog : Window
    {
        private string m_FilePath = string.Empty;
        private DlkTestSuiteLoader m_Loader = new DlkTestSuiteLoader();
        private DlkTest m_TestToSave;
        private List<BFFolder> m_TestFolders = new List<BFFolder>();
        private bool? mIsSaveAndCheckin;
        private string mSavedFilePath;
        private const int MAX_PATH_LENGTH = 256;

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
                    DirItems = m_Loader.GetSuiteDirectories(DlkEnvironment.mDirTests, m_FilePath),
                    IsSelected = string.IsNullOrEmpty(m_FilePath) 
                        || (System.IO.Path.GetDirectoryName(m_FilePath)) == System.IO.Path.GetDirectoryName(di.FullName),
                    IsExpanded = true
                };
                m_TestFolders.Add(root);
                return m_TestFolders;
            }
        }

        public SaveTestDialog(DlkTest TestToSave, String FilteredPath = "", String Title = "")
        {
            InitializeComponent();

            this.Title = Title;
            m_TestToSave = TestToSave;
            m_FilePath = m_TestToSave.mTestPath;

            string fileName = "";

            if (IsValidFileName(m_TestToSave.mTestName))
            {
                fileName = m_TestToSave.mTestName;
            }
            else
            {
                fileName = m_TestToSave.mTestPath.Split('.').Last().ToLower() == "xml" ? m_TestToSave.mFileName : "NewTest";
            }

            if (FilteredPath != String.Empty)
            {
                m_FilePath = System.IO.Path.Combine(FilteredPath, fileName);
                txtNameExt.Text = fileName;
            }

            if (m_FilePath.Length > MAX_PATH_LENGTH) // handle filepath, if exceeds max, remove excess chars
            {
                m_FilePath = m_FilePath.Remove(256); // accounted for the file extension
            }

            // context menu
            ContextMenu ctx = ((ContextMenu)FindResource("ctxSaveDialogTreeView"));
            MenuItem mnu = new MenuItem();
            ctx.Items.Clear();
            mnu.Header = "Add new folder";
            ctx.Items.Add(mnu);
            mnu.Click += new RoutedEventHandler(AddNewFolderToTree);
            tvwFoldersExisting.ItemsSource = TestFolders;
        }

        private bool IsValidFileName(string Input)
        {
            bool ret = true;
            if (Input.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                ret = false;
            }
            return ret;
        }

        private void LoadTestList(string FolderPath)
        {
            List<DlkTest> lstTests = new List<DlkTest>();
            DirectoryInfo di = new DirectoryInfo(FolderPath);
            DlkTest selectedItem = null;

            foreach (FileInfo file in di.GetFiles("*.xml"))
            {
                DlkTest tst = new DlkTest(file.FullName);
                lstTests.Add(tst);
                if (m_FilePath == file.FullName)
                {
                    selectedItem = tst;
                }
            }

            DataContext = lstTests;

            lvwTests.SelectedItem = selectedItem;
            lvwTests.ScrollIntoView(selectedItem);
        }

        private void UpdateSelectedTestInfo()
        {
            if (lvwTests.SelectedItem != null)
            {
                DlkTest selection = (DlkTest)lvwTests.SelectedItem;
                txtNameExt.Text = System.IO.Path.GetFileNameWithoutExtension(selection.mTestPath);
                txtPathExt.Text = selection.mTestPath;
                txtPathExt.ToolTip = selection.mTestPath;
                m_FilePath = selection.mTestPath;
            }
        }

        /// <summary>
        /// Get input from user about parameters of Save
        /// including wether to checkin as well as save location
        /// </summary>
        /// <param name="IsSaveAndCheckin">Out parameter to get save action</param>
        /// TRUE -> Save and checkin
        /// FALSE -> Save local
        /// NULL -> User aborted save
        /// <param name="SavedFilePath">Out parameter to get save path</param>
        public void SaveTest(out bool? IsSaveAndCheckin, out string SavedFilePath)
        {
            IsSaveAndCheckin = null;
            SavedFilePath = string.Empty;

            /* Source control is NOT enabled */
            if (!DlkSourceControlHandler.SourceControlEnabled)
            {
                SavedFilePath = m_FilePath;
                IsSaveAndCheckin = false;

                /* Confirm if NOT willing overwrite readonly (Abort) */
                if (IsFileExistsAndReadOnly(m_FilePath) 
                    && DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_OVERWRITE_READ_ONLY) == MessageBoxResult.No)
                {
                    SavedFilePath = string.Empty; /* just flush */
                    IsSaveAndCheckin = null;
                }
            }
            else /* Source control is enabled */
            {
                CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_CHECK_IN_SOURCE_CONTROL_FILE_AFTER_SAVING);
                    
                /* The dialog is visible, this should be the parent form */
                /* Test Editor is the parent form, meaning 'Save' was clicked for an existing file */
                ciscDialog.Owner = this.IsVisible ? this : this.Owner;
                ciscDialog.ShowDialog(); 

                if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveAndCheckin) /* User saves AND checks-in */
                {
                    SavedFilePath = m_FilePath;
                    IsSaveAndCheckin = true;
                }
                else if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveOnly)
                {
                    SavedFilePath = m_FilePath;
                    IsSaveAndCheckin = false;
                }
                /* ELSE is abort */
            }
        }

        /// <summary>
        /// Show this dialog and set required parameters by caller
        /// </summary>
        /// <param name="IsSaveAndCheckin">>Out parameter to get save action</param>
        /// <param name="SaveFilePath">Out parameter to get save path</param>
        public void ShowDialog(out bool? IsSaveAndCheckin, out string SaveFilePath)
        {
            this.ShowDialog();
            IsSaveAndCheckin = mIsSaveAndCheckin;
            SaveFilePath = mSavedFilePath;
        }

        /// <summary>
        /// Checks if input file exists AND is readonly
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <returns>TRUE if file exists and readonly</returns>
        /// FALSE if file does not exist OR not readonly
        private bool IsFileExistsAndReadOnly(string path)
        {
            return File.Exists(path) && new FileInfo(path).IsReadOnly;
        }

        private void lvwSuites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateSelectedTestInfo();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void OnNameChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtNameExt.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    TextChange changed = e.Changes.First();
                    string invalidText = txtNameExt.Text.Substring(changed.Offset, changed.AddedLength);
                    txtNameExt.Text = txtNameExt.Text.Replace(invalidText, "");
                    txtNameExt.SelectionStart = txtNameExt.Text.Length;
                    popup.IsOpen = true;
                }

                if (tvwFoldersExisting.SelectedItem != null)
                {
                    string folder = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                    string path = Path.Combine(folder,txtNameExt.Text.Trim() + ".xml");

                    txtPathExt.Text = path;
                    txtPathExt.ToolTip = path;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void PreviewTextInput_Name(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 && e.Text != "\r" && e.Text != "\u001b")
            {
                e.Handled = true;
                popup.IsOpen = true;
            }
            else
            {
                if (popup.IsOpen)
                    popup.IsOpen = false;
            }
        }

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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Check for filename validity */
                if (string.IsNullOrEmpty(txtNameExt.Text.Trim()))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_FILENAME_INVALID);
                    return;
                }

                /* Check for filepath length */
                if (txtPathExt.Text.Length > MAX_PATH_LENGTH)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_FILE_NAME_EXCEEDS_MAX, "Error");
                    return;
                }

                if (File.Exists(txtPathExt.Text) && DlkUserMessages.ShowQuestionYesNoWarning(string.Format(DlkUserMessages.ASK_OVERWRITE_TEST_SCRIPT,txtNameExt.Text)) == MessageBoxResult.No)
                {
                    return;
                }

                m_FilePath = txtPathExt.Text;
                SaveTest(out mIsSaveAndCheckin, out mSavedFilePath);

                /* Close this window if user did not abort save --> NULL if user aborted */
                if (mIsSaveAndCheckin != null)
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvwFoldersExisting_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                OnNameChanged(null, null);
                if (tvwFoldersExisting.SelectedItem != null)
                {
                    LoadTestList(((BFFolder)tvwFoldersExisting.SelectedItem).Path);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ScrollSelectedTreeViewItem(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.BringIntoView();
                e.Handled = true;
            }
        }

        private void tvwFoldersNew_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                OnNameChanged(null, null);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void AddNewFolderToTree(object sender, RoutedEventArgs e)
        {
            try
            {
                // remember the old ones
                List<string> currItems = new List<string>();
                string prevSelected = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                foreach (BFFolder sf in ((BFFolder)tvwFoldersExisting.SelectedItem).DirItems)
                {
                    currItems.Add(sf.Name);
                }

                AddNewFolder dlg = new AddNewFolder(((BFFolder)tvwFoldersExisting.SelectedItem).Path);
                dlg.Owner = this;
                if ((bool)(dlg.ShowDialog()) == true)
                {
                    // refresh
                    tvwFoldersExisting.ItemsSource = TestFolders;
                    tvwFoldersExisting.Items.Refresh();

                    //select the new one
                    SelectNewlyAddedItem(prevSelected, currItems, ((BFFolder)tvwFoldersExisting.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void SelectNewlyAddedItem(string previouslySelected, List<string> OldItems, BFFolder Folder)
        {
            if (Folder.Path == previouslySelected)
            {
                foreach (BFFolder sf in Folder.DirItems)
                {

                    if (OldItems.Contains(sf.Name))
                    {
                        continue;
                    }
                    sf.IsSelected = true;
                    return;
                }
            }
            else
            {
                foreach (BFFolder sf in Folder.DirItems)
                {
                    SelectNewlyAddedItem(previouslySelected, OldItems, sf);
                }
            }

        }

        private class FileNameConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return System.IO.Path.GetFileNameWithoutExtension(value.ToString());
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    }

    [ValueConversion(typeof(String), typeof(String))]
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.IO.Path.GetFileNameWithoutExtension(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
