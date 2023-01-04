using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestRunner.Common;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SaveSuiteDialog.xaml
    /// </summary>
    public partial class SaveSuiteDialogSC : Window
    {
        #region DECLARATIONS
        public event PropertyChangedEventHandler PropertyChanged;
        private const string MENU_TEXT_SUITESONLY = "Search for suites only";
        private const string MENU_TEXT_TAGSONLY = "Search for tags only";
        private const string MENU_TEXT_OWNERSONLY = "Search for owners only";
        private const string MENU_TEXT_SUITESANDTAGS = "Include suites and tags in search";
        private const string STATUS_TEXT_DELETING = "Deleting...";
        private const string STATUS_TEXT_READY = "Ready";
        private const int PROCESS_ACTION_ARG_IDX_ACTION = 0;
        private const int PROCESS_ACTION_ARG_IDX_PATH = 1;
        private const int PROCESS_ACTION_ARG_IDX_COMMENTS = 2;
        private const int PROCESS_ACTION_ARG_IDX_SOURCE = 3;
        private const int PROCESS_ACTION_ARG_EXACT_COUNT = 4;
        private const int PROCESS_ACTION_RES_IDX_ACTION = 0;
        private const int PROCESS_ACTION_RES_IDX_RET = 1;
        private const int PROCESS_ACTION_REX_IDX_ISFILE = 3;
        private const int PROCESS_ACTION_RES_EXACT_COUNT = 4;
        private const int MAX_PATH_LENGTH = 256;
        private KwDirItem.SearchType mSearchType;
        private DlkTestSuiteInfoRecord m_TestSuite = null;
        private bool m_IsNew = true;
        private string m_FilePath = string.Empty;
        private string mStatusText = string.Empty;
        private bool m_IsOpen = false;
        private BackgroundWorker mProcessWorker = null;
        private List<DlkExecutionQueueRecord> m_Tests = null;
        private DlkTestSuiteLoader m_Loader = new DlkTestSuiteLoader();
        private List<BFFolder> m_SuiteFolders = new List<BFFolder>();
        private bool? mIsSaveAndCheckin;
        private bool mIsProcessInProgress = false;
        private string mSavedFilePath;
        private string mImportLocation;
        private List<FileInfo> mSuiteFiles;
        private List<DlkTag> mAllTags = new List<DlkTag>();
        private List<SuiteFile> mSuiteTags = new List<SuiteFile>();
        private List<SuiteFile> mSuiteOwners = new List<SuiteFile>();
        private readonly MenuItem delMenu = new MenuItem();

        private enum ProcessWorkerAction /* Add here if additional action in the future */
        {
            SaveSuiteLocal,
            SaveSuiteLocalSilent,
            SaveAndCheckInSuite,
            SaveAndCheckInSuiteSilent,
            AddTestFolderLocal,
            AddAndCheckInTestFolder,
            MoveAndCheckInTest,
            DeleteTestSuiteLocal,
            DeleteAndCheckInSuiteItem
        }

        /// <summary>
        /// Status text when Main Window is busy
        /// </summary>
        public string StatusText
        {
            get
            {
                return mStatusText;
            }
            set
            {
                mStatusText = value;
                OnPropertyChanged("StatusText");
            }
        }
        #endregion

        #region PROPERTIES
        public List<BFFolder> SuiteFolders
        {
            get
            {
                m_SuiteFolders.Clear();
                DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTestSuite);
                BFFolder root = new BFFolder
                {
                    Name = di.Name,
                    Path = di.FullName,
                    DirItems = m_Loader.GetSuiteDirectories(DlkEnvironment.mDirTestSuite, m_FilePath),
                    IsSelected = m_IsOpen || string.IsNullOrEmpty(m_FilePath)
                        || (System.IO.Path.GetDirectoryName(m_FilePath)) == System.IO.Path.GetDirectoryName(di.FullName),
                    IsExpanded = true
                };
                m_SuiteFolders.Add(root);
                return m_SuiteFolders;
            }
        }

        /// <summary>
        /// Destination of files or folders to be imported
        /// </summary>
        public String ImportLocation
        {
            get
            {
                if (mImportLocation == null)
                {
                    mImportLocation = DlkEnvironment.mDirTestSuite;
                }
                return mImportLocation;
            }
            set
            {
                mImportLocation = value;
            }
        }

        /// <summary>
        /// Flag that indicates whether a process is in progress in the background
        /// </summary>
        public bool IsBackgroundProcessInProgress
        {
            get
            {
                return mIsProcessInProgress;
            }
            set
            {
                mIsProcessInProgress = value;
                OnPropertyChanged("IsBackgroundProcessInProgress");
            }
        }
        #endregion

        #region CONSTRUCTOR
        public SaveSuiteDialogSC(string FilePath, DlkTestSuiteInfoRecord TestSuite, List<DlkExecutionQueueRecord> Tests, bool IsOpen = false, string SuiteDescription = "", string SuiteOwner = "")
        {
            InitializeComponent();
            InitializeProcessWorker();
            m_IsNew = string.IsNullOrEmpty(FilePath);
            m_FilePath = FilePath;
            m_TestSuite = TestSuite;
            m_Tests = Tests;
            InitializeSuitesAndTags();
            if (IsOpen)
            {                
                m_IsOpen = IsOpen;
                this.Title = "Open";
                btnOK.Content = "Open";
                txtNameExt.IsEnabled = false;
                txtNameExt.IsReadOnly = true;
            }
            else
            {
                m_TestSuite.Description = SuiteDescription;
                m_TestSuite.Owner = SuiteOwner;
            }

            // context menu
            ContextMenu ctx = ((ContextMenu)FindResource("ctxSaveDialogTreeView"));
            ctx.Items.Clear();
            MenuItem addMenu = new MenuItem();
            addMenu.Header = "Add new folder";
            ctx.Items.Add(addMenu);            
            delMenu.Header = "Delete folder";
            ctx.Items.Add(delMenu);
            addMenu.Click += new RoutedEventHandler(AddNewFolderToTree);
            delMenu.Click += new RoutedEventHandler(DeleteFolder);
            tvwFoldersExisting.ItemsSource = SuiteFolders;

            // search type
            string[] searchType = { MENU_TEXT_SUITESONLY, MENU_TEXT_OWNERSONLY, MENU_TEXT_TAGSONLY, MENU_TEXT_SUITESANDTAGS };
            for (int countType = 0; countType < searchType.Length; countType++)
            {
                MenuItem searchItem = new MenuItem();
                searchItem.Header = searchType[countType];
                searchItem.IsCheckable = true;
                if (countType == 0)
                {
                    searchItem.IsChecked = true;
                }
                searchItem.Click += SearchTypeMenuItem_Click;
                cmnuSearchTypes.Items.Add(searchItem);
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Initialize Process background worker
        /// </summary>
        private void InitializeProcessWorker()
        {
            if (mProcessWorker == null)
            {
                mProcessWorker = new BackgroundWorker();
                mProcessWorker.DoWork += mProcessWorker_DoWork;
                mProcessWorker.RunWorkerCompleted += mProcessWorker_RunWorkerCompleted;
            }
        }

        /// <summary>
        /// Cache suite files and tags
        /// </summary>
        private void InitializeSuitesAndTags(string path = "")
        {
            try
            {
                List<FileInfo> tempFileInfo = new List<FileInfo>();
                bool hasImported = !string.IsNullOrEmpty(path);
                path = hasImported ? path : DlkEnvironment.mDirTestSuite;
                DirectoryInfo di = new DirectoryInfo(path);

                if (!hasImported)
                {
                    mSuiteFiles = di.GetFiles("*.xml", SearchOption.AllDirectories).ToList();
                }
                else
                {
                    mSuiteTags.RemoveAll(dir => dir.DirectoryName == path);
                    mSuiteOwners.RemoveAll(dir => dir == null || dir.DirectoryName == path);
                    mSuiteFiles.RemoveAll(dir => dir == null || dir.DirectoryName == path);
                    tempFileInfo = di.GetFiles("*.xml", SearchOption.TopDirectoryOnly).ToList();
                    mSuiteFiles.AddRange(tempFileInfo);
                }
                mAllTags = DlkTag.LoadAllTags();

                Task.Factory.StartNew(() =>
                {
                    Parallel.ForEach(hasImported ? tempFileInfo : mSuiteFiles, file =>
                    {
                        SuiteFile suite = new SuiteFile();
                        lock (suite)
                        {
                            try
                            {
                                suite.FullName = file.FullName;
                                suite.DirectoryName = file.DirectoryName;
                                suite.Name = file.Name;

                                using (XmlReader reader = XmlReader.Create(file.FullName))
                                {
                                    reader.ReadToFollowing("suite");
                                    string owner = reader.GetAttribute("owner");

                                    if (!string.IsNullOrEmpty(owner))
                                    {
                                        suite.Owner = owner;
                                        mSuiteOwners.Add(suite);
                                    }

                                    while (reader.ReadToFollowing("tag"))
                                    {
                                        string name = reader.GetAttribute("name");
                                        string id = reader.GetAttribute("id");
                                        suite.Tags.Add(new DlkTag(id, name, "", ""));
                                    }
                                    if(suite.Tags.Count > 0)
                                        mSuiteTags.Add(suite);
                                }
                            }
                            catch
                            {
                                //skip malformed xml file
                            }                            
                        }
                    });
                });
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, e);
            }
        }

        /// <summary>
        /// Displays the suites that will satisfy the searchValue and searchOption to the ListView
        /// </summary>
        private void LoadSuiteList(string FolderPath, string searchValue = "", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            List<SuiteFile> lstSuites = new List<SuiteFile>();
            List<FileInfo> files = mSuiteFiles;
            DirectoryInfo di = new DirectoryInfo(FolderPath);
            SuiteFile selectedItem = null;
            string fileNameWithoutExtension = "";
            searchValue = searchValue.ToLower();

            if (mSearchType == KwDirItem.SearchType.TestsSuitesOnly || searchValue == "")
            {
                if (searchValue == "")
                    files = mSuiteFiles.FindAll(f => f.DirectoryName == FolderPath.TrimEnd('\\'));
                else
                    files = mSuiteFiles.FindAll(f => f.Name.ToLower().Contains(searchValue));
            }
            else if (mSearchType == KwDirItem.SearchType.TagsOnly)
            {
                files = mSuiteFiles.Join(mSuiteTags, suiteFiles => suiteFiles.FullName, suiteTags => suiteTags.FullName, (suiteFiles, suiteTags) => suiteFiles).ToList();
            }
            else if (mSearchType == KwDirItem.SearchType.Owner)
            {
                files = mSuiteFiles.Join(mSuiteOwners, suiteFiles => suiteFiles.FullName, suiteOwner => suiteOwner.FullName, (suiteFiles, suiteOwner) => suiteFiles).ToList();
            }

            foreach (var file in files)
            {
                bool addSuiteToTree = true;
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                if (searchValue != "")
                {
                    switch (mSearchType)
                    {

                        case KwDirItem.SearchType.TestsSuitesOnly:
                            if (!fileNameWithoutExtension.ToLower().Contains(searchValue))
                            {
                                addSuiteToTree = false;
                            }
                            break;
                        case KwDirItem.SearchType.TagsOnly:
                            List<DlkTag> tags = mSuiteTags.Find(f => f.FullName == file.FullName).Tags;
                            if (!tags.Any(x => x.Name.ToLower().Contains(searchValue) && mAllTags.Any(y => y.Name == x.Name)))
                            {
                                addSuiteToTree = false;
                            }
                            break;
                        case KwDirItem.SearchType.Owner:
                            string suiteOwner = mSuiteOwners.Find(f => f.FullName == file.FullName).Owner;
                            if (!suiteOwner.ToLower().Contains(searchValue))
                            {
                                addSuiteToTree = false;
                            }
                            break;
                        case KwDirItem.SearchType.TestsSuitesAndTags:
                            tags = mSuiteTags.Find(f => f.FullName == file.FullName)?.Tags;
                            if (!(fileNameWithoutExtension.ToLower().Contains(searchValue) || (tags != null && tags.Any(x => x.Name.ToLower().Contains(searchValue) && mAllTags.Any(y => y.Name == x.Name)))))
                            {
                                addSuiteToTree = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (addSuiteToTree)
                {
                    SuiteFile suiteToAdd = new SuiteFile { Name = fileNameWithoutExtension, FullName = file.FullName };
                    lstSuites.Add(suiteToAdd);
                    if (m_FilePath == file.FullName)
                    {
                        selectedItem = suiteToAdd;
                    }
                }
            }

            DataContext = lstSuites;

            if (!m_IsOpen)
            {
                lvwSuites.SelectedItem = selectedItem;
                lvwSuites.ScrollIntoView(selectedItem);
            }
        }

        private void UpdateSelectedSuiteInfo()
        {
            if (lvwSuites.SelectedItem != null)
            {
                SuiteFile selection = (SuiteFile)lvwSuites.SelectedItem;

                btnDeleteSuite.IsEnabled = selection.FullName != DlkEnvironment.mDirTestSuite;
                delMenu.IsEnabled = selection.FullName != DlkEnvironment.mDirTestSuite;
                txtNameExt.Text = selection.Name;
                txtPathExt.Text = selection.FullName;
                txtPathExt.ToolTip = selection.FullName;
                m_FilePath = selection.FullName;
            }
        }

        private void btnDeleteSuite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvwSuites.SelectedItem != null || tvwFoldersExisting.SelectedItem != null)
                {
                    DeleteItem();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name"></param>
        private void OnPropertyChanged(string Name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(Name));
            }
        }

        private void UpdateStatusBar(string Status = "")
        {
            if (string.IsNullOrEmpty(Status))
            {
                StatusText = STATUS_TEXT_READY;
            }
            else
            {
                StatusText = Status;
            }
        }

        /// <summary>
        /// Run process via Backgroundworker
        /// </summary>
        /// <param name="actionToPerform">Process to run</param>
        /// <param name="path">Relevant file Path</param>
        private void RunProcessInBackground(ProcessWorkerAction actionToPerform, string path, string source = "")
        {
            string sComments = string.Empty;
            IsBackgroundProcessInProgress = true;

            /* set status text */
            string statusTxt = string.Empty;
            switch (actionToPerform)
            {
                case ProcessWorkerAction.DeleteTestSuiteLocal:
                case ProcessWorkerAction.DeleteAndCheckInSuiteItem:
                    statusTxt = STATUS_TEXT_DELETING;
                    break;
                default:
                    break;
            }

            UpdateStatusBar(statusTxt);
            mProcessWorker.RunWorkerAsync(new object[] { actionToPerform, path, sComments, source });
        }

        /// <summary>
        /// Check whether file is readonly or directory contains any readonly file
        /// </summary>
        /// <param name="path">Path of either file or directory</param>
        /// <returns>TRUE if has readonly; FALSE otherwise</returns>
        private bool HasReadOnlyFile(string path)
        {
            bool ret = false;
            if (Directory.Exists(path))
            {
                string[] readOnlyFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                ret = readOnlyFiles.Select(x => new FileInfo(x)).Any(y => y.IsReadOnly == true);
            }
            else if (File.Exists(path))
            {
                ret = new FileInfo(path).IsReadOnly;
            }
            return ret;
        }

        /// <summary>
        /// Delete single item from Test Explorer
        /// </summary>
        private void DeleteFolder(object sender, RoutedEventArgs e)
        {
            DeleteItem();
        }

        /// <summary>
        /// Delete single item from Test Explorer
        /// </summary>
        private void DeleteItem()
        {
            try
            {
                string pathToDelete = "";
                if (lvwSuites.SelectedItem != null)
                {
                    pathToDelete = ((SuiteFile)lvwSuites.SelectedItem).FullName;
                }
                else if (tvwFoldersExisting.SelectedItem != null)
                {
                    pathToDelete = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                }

                bool bIsFile = File.Exists(pathToDelete);
                bool bIsFolder = Directory.Exists(pathToDelete);
                if (bIsFile || bIsFolder)
                {
                    if (DlkSourceControlHandler.SourceControlEnabled)
                    {
                        CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_DELETE_SOURCE_CONTROL);
                        ciscDialog.rbSaveAndCheckin.Content = "Yes, delete both local and source control file/s.";
                        ciscDialog.rbSaveOnly.Content = "No, delete local file/s only.";
                        ciscDialog.Owner = IsVisible ? this : Owner;
                        ciscDialog.ShowDialog();
                        if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveAndCheckin)
                        {
                            RunProcessInBackground(ProcessWorkerAction.DeleteAndCheckInSuiteItem, pathToDelete);
                        }
                        else if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveOnly)
                        {
                            RunProcessInBackground(ProcessWorkerAction.DeleteTestSuiteLocal, pathToDelete);
                        }
                    }
                    else
                    {
                        string msg = bIsFile ? DlkUserMessages.ASK_SUITE_DELETE + pathToDelete + "?" : DlkUserMessages.ASK_FOLDER_DELETE + pathToDelete + DlkUserMessages.ASK_FOLDER_ALLFILES;
                        if (HasReadOnlyFile(pathToDelete)) /* Change message if there 'readonly' conideration */
                        {
                            msg = bIsFile ? DlkUserMessages.ASK_SUITE_DELETE + pathToDelete + "?\n\n" + DlkUserMessages.ASK_FILE_READ_ONLY
                                : DlkUserMessages.ASK_FOLDER_DELETE + pathToDelete + DlkUserMessages.ASK_FOLDER_ALLFILES + "\n\n" + DlkUserMessages.ASK_FOLDER_READ_ONLY;
                        }

                        if (DlkUserMessages.ShowQuestionYesNo(msg, "Delete") == MessageBoxResult.Yes)
                        {
                            RunProcessInBackground(ProcessWorkerAction.DeleteTestSuiteLocal, pathToDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Delete folder from local and server
        /// </summary>
        /// <param name="path">Folder path to delete</param>
        private void DeleteSourceControlDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return;
                }
                DlkSourceControlHandler.Delete(path, "");
                DlkSourceControlHandler.CheckIn(path, "/comment:TestRunner");
                if (Directory.Exists(path))
                {
                    DeleteLocalDirectory(path);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete folder from local
        /// </summary>
        /// <param name="path">Folder path to delete</param>
        private void DeleteLocalDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return;
                }
                string[] readOnlyFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                /* remove lock of files inside */
                foreach (string file in readOnlyFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.IsReadOnly = false;
                }
                Directory.Delete(path, true);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete test from local
        /// </summary>
        /// <param name="path">Path of suite to delete</param>
        private void DeleteLocalSuite(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                FileInfo fileInfo = new FileInfo(path);
                fileInfo.IsReadOnly = false;
                File.Delete(path);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    ClearSelectedSuiteInfo();
                }));
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Delete test from local and server
        /// </summary>
        /// <param name="path">Path of suite to delete</param>
        private void DeleteSourceControlSuite(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                DlkSourceControlHandler.Delete(path, "");
                DlkSourceControlHandler.CheckIn(path, "/comment:TestRunner");
                if (File.Exists(path))
                {
                    DeleteLocalSuite(path);
                }
            }
            catch
            {
                throw;
            }

        }

        private void ClearSelectedSuiteInfo()
        {
            txtNameExt.Text = "";
            txtPathExt.Text = "";
            txtPathExt.ToolTip = "";
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

        /// <summary>
        /// Get input from user about parameters of Save
        /// including wether to checkin as well as save location
        /// </summary>
        /// <param name="IsSaveAndCheckin">Out parameter to get save action</param>
        /// TRUE -> Save and checkin
        /// FALSE -> Save local
        /// NULL -> User aborted save
        /// <param name="SavedFilePath">Out parameter to get save path</param>
        public void SaveSuite(out bool? IsSaveAndCheckin, out string SavedFilePath)
        {
            IsSaveAndCheckin = null;
            SavedFilePath = string.Empty;

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
            else
            {
                CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_CHECK_IN_SOURCE_CONTROL_FILE_AFTER_SAVING);

                /* The dialog is visible, this should be the parent form */
                /* Main Window is the parent form, meaning 'Save' was clicked for an existing file */
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

        public void AddToSource(object[] parameters)
        {
            DlkSourceControlHandler.Add(parameters.First().ToString(), parameters.Last().ToString());

        }

        public void CheckInToSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckIn(parameters.First().ToString(), parameters.Last().ToString());
        }

        public void CheckOutFromSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckOut(parameters.First().ToString(), parameters.Last().ToString());
        }

        private bool LoadSuite()
        {
            if (string.IsNullOrEmpty(m_FilePath))
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_FILE_TO_OPEN);
                return false;
            }
            ((MainWindow)this.Owner).LoadedSuite = m_FilePath;
            return true;
        }

        private bool DeleteItemLocal(string pathToDelete)
        {
            bool ret = true;
            try
            {
                bool bIsFile = File.Exists(pathToDelete);
                bool bIsFolder = Directory.Exists(pathToDelete);
                if (!bIsFile && !bIsFolder) /* Nothing to delete */
                {
                    return false;
                }
                if (bIsFolder)
                {
                    DeleteLocalDirectory(pathToDelete);
                }
                else
                {
                    DeleteLocalSuite(pathToDelete);
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Delete single item in Test Explorer and checkin
        /// </summary>
        /// <param name="pathToDelete">Path of item selected</param>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private bool DeleteItemAndCheckIn(string pathToDelete)
        {
            bool ret = true;
            try
            {
                bool bIsFile = File.Exists(pathToDelete);
                bool bIsFolder = Directory.Exists(pathToDelete);
                if (!bIsFile && !bIsFolder) /* Nothing to delete */
                {
                    return false;
                }

                if (bIsFolder)
                {
                    DeleteSourceControlDirectory(pathToDelete);
                }
                else
                {
                    DeleteSourceControlSuite(pathToDelete);
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Expand/Collapse the tree view recursively
        /// </summary>
        private void ExpandCollapseTreeView()
        {
            ItemContainerGenerator gen = this.tvwFoldersExisting.ItemContainerGenerator;

            foreach (var itm in tvwFoldersExisting.Items)
            {
                TreeViewItem item = gen.ContainerFromItem(itm) as TreeViewItem;
                if (!item.IsExpanded)
                {
                    item.ExpandSubtree();
                }
                else
                {
                    item.IsExpanded = false;
                }
            }
        }

        /// <summary>
        /// This method is used in Import when the user selects to copy a Folder/Directory.
        /// </summary>
        /// <param name="sourcePath">Full path of the folder/directory to copy</param>
        /// <param name="targetPath">Full path of the destination directory</param>
        private void CopyDirectory(string sourcePath, string targetPath, Boolean bOverwrite)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            string[] files = Directory.GetFiles(sourcePath, "*.xml");
            foreach (var file in files)
            {
                string destFile = Path.Combine(targetPath, Path.GetFileNameWithoutExtension(file) + ".xml");

                try
                {
                    if (DlkTestSuiteXmlHandler.IsValidFormat(file))
                    {
                        if (bOverwrite)
                        {
                            File.Copy(file, destFile, true);
                            File.SetAttributes(destFile, FileAttributes.Normal);
                        }
                        else if (!File.Exists(destFile))
                        {
                            File.Copy(file, destFile, false);
                            File.SetAttributes(destFile, FileAttributes.Normal);
                        }
                    }
                    else
                    {
                        // Skip invalid tests
                        continue;
                    }
                }
                catch (Exception)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                    return;
                }
            }

            string[] directories = Directory.GetDirectories(sourcePath);
            foreach (var directory in directories)
            {
                string name = Path.GetFileName(directory);
                string dest = Path.Combine(targetPath, name);
                CopyDirectory(directory, dest, bOverwrite);
            }
        }

        /// <summary>
        /// Checks if directory is the root Tests/Suites directory or if it contains or is under the root Tests/Suites directory
        /// </summary>
        /// <param name="importFolderDirectory">Directory to be checked</param>
        /// <param name="error">Error encountered</param>
        private bool CheckFolderForRootTestsOrSuites(string importFolderDirectory, out String error)
        {
            error = "";
            bool contains = false;
            string rootTest = DlkEnvironment.mDirTests.Remove(DlkEnvironment.mDirTests.Length - 1);
            string rootSuite = DlkEnvironment.mDirTestSuite.Remove(DlkEnvironment.mDirTestSuite.Length - 1);

            // check if folder is under root Tests/Suites folder or if directory is the root
            if (importFolderDirectory.Contains(rootTest))
            {
                error = "RootTest";
                return true;
            }
            else if (importFolderDirectory.Contains(rootSuite))
            {
                error = "RootSuite";
                return true;
            }
            else
            {
                List<string> directories = Directory.GetDirectories(importFolderDirectory, "*", SearchOption.AllDirectories).ToList();

                foreach (string dir in directories)
                {
                    if (dir.Equals(rootTest))
                    {
                        error = "RootSuiteAndTest";
                        return true;
                    }
                    if (dir.Equals(rootSuite))
                    {
                        error = "RootSuiteAndTest";
                        return true;
                    }
                }
            }

            return contains;
        }

        /// <summary>
        /// Checks if directory has more than 5 levels deep of nested folders
        /// </summary>
        /// <param name="importFolderDirectory">Directory to be checked</param>
        private bool CheckFolderForMultipleNestedFolders(string importFolderDirectory)
        {
            bool contains = false;
            string parentDirectory = importFolderDirectory.Substring(0, importFolderDirectory.LastIndexOf("\\"));
            List<string> directories = Directory.GetDirectories(importFolderDirectory, "*", SearchOption.AllDirectories).ToList();

            foreach (string dir in directories)
            {
                string directory = dir.Substring(parentDirectory.Length + 1);
                int count = directory.Count(x => (x == '\\'));
                if (count > 5)
                {
                    return true;
                }
            }

            return contains;
        }

        #endregion 

        #region EVENTS
        private void lvwSuites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateSelectedSuiteInfo();
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
                    string path = System.IO.Path.Combine(folder, txtNameExt.Text.Trim() + ".xml");
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

        private class SuiteFile
        {
            /// <summary>
            /// Suite file name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Suite full path
            /// </summary>
            public string FullName { get; set; }

            /// <summary>
            /// Suite file directory name
            /// </summary>
            public string DirectoryName { get; set; }

            /// <summary>
            /// Suite owner
            /// </summary>
            public string Owner { get; set; }

            /// <summary>
            /// Suite tags
            /// </summary>
            public List<DlkTag> Tags { get; set; } = new List<DlkTag>();
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
                if (m_IsOpen)
                {
                    if (LoadSuite())
                    {
                        this.DialogResult = true;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNameExt.Text.Trim()))
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_FILENAME_INVALID);
                        return;
                    }

                    if (txtPathExt.Text.Length > MAX_PATH_LENGTH)
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_FILE_NAME_EXCEEDS_MAX, "Error");
                        return;
                    }

                    m_FilePath = txtPathExt.Text;
                    SaveSuite(out mIsSaveAndCheckin, out mSavedFilePath);
                    this.DialogResult = true;
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
                ClearSelectedSuiteInfo();
                if (tvwFoldersExisting.SelectedItem != null)
                {
                    string selectedPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                    btnDeleteSuite.IsEnabled = selectedPath != DlkEnvironment.mDirTestSuite;
                    delMenu.IsEnabled = selectedPath != DlkEnvironment.mDirTestSuite;
                    LoadSuiteList(selectedPath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
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
                    tvwFoldersExisting.ItemsSource = SuiteFolders;
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

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImportTestOptionDialog importDlg = new ImportTestOptionDialog("suite");
                bool hasImported = false;
                importDlg.Owner = this;
                importDlg.ShowDialog();

                ImportDestination importDest = new ImportDestination(DlkEnvironment.mDirTestSuite);
                importDest.Owner = this;

                if (importDlg.ImportOption == ImportTestOptionDialog.ImportTestOptions.Folder)
                {
                    var oFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
                    oFolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    oFolderDialog.SelectedPath = DlkEnvironment.mDirTestSuite;
                    oFolderDialog.Description = "Import Suite Folder";

                    if (oFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (CheckFolderForRootTestsOrSuites(oFolderDialog.SelectedPath, out String error))
                        {
                            switch (error)
                            {
                                case "RootTest":
                                    DlkUserMessages.ShowError(DlkUserMessages.ERR_IMPORT_ROOTTEST);
                                    break;
                                case "RootSuite":
                                    DlkUserMessages.ShowError(DlkUserMessages.ERR_IMPORT_ROOTSUITE);
                                    break;
                                case "RootSuiteAndTest":
                                    DlkUserMessages.ShowError("The folder you wish to import contains the Tests/Suites directory of " + DlkEnvironment.mProductFolder
                                    + " inside. Please choose a different folder."); ;
                                    break;
                            }
                        }
                        else
                        {
                            if (CheckFolderForMultipleNestedFolders(oFolderDialog.SelectedPath))
                            {
                                MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_IMPORT_NESTEDFOLDERS, "Import Folder?");
                                if (res == MessageBoxResult.No)
                                {
                                    return;
                                }
                            }

                            if ((bool)importDest.ShowDialog())
                            {
                                ImportLocation = Path.GetFullPath(importDest.ImportDestinationPath);
                                string targetPath = ImportLocation + Path.GetFileName(oFolderDialog.SelectedPath);
                                hasImported = true;
                                // To copy a folder to a new location: Check if the directory or folder already exists.
                                if (!System.IO.Directory.Exists(targetPath))
                                {
                                    CopyDirectory(oFolderDialog.SelectedPath, targetPath, true);
                                }
                                else
                                {
                                    // Prompt for the user to overwrite the contents of the destination path if it already exists.
                                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancelWarning(DlkUserMessages.ASK_FOLDER_EXISTS_SUITE_IMPORT_FAILED, "Import Folder?");
                                    switch (res)
                                    {
                                        case MessageBoxResult.Yes:
                                            CopyDirectory(oFolderDialog.SelectedPath, targetPath, true);
                                            break;
                                        case MessageBoxResult.No:
                                            CopyDirectory(oFolderDialog.SelectedPath, targetPath, false);
                                            break;
                                        case MessageBoxResult.Cancel:
                                            hasImported = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                if (hasImported)
                                {
                                    InitializeSuitesAndTags(targetPath);
                                    //refresh
                                    tvwFoldersExisting.ItemsSource = SuiteFolders;
                                    tvwFoldersExisting.Items.Refresh();
                                }
                            }
                        }
                    }
                }
                else if (importDlg.ImportOption == ImportTestOptionDialog.ImportTestOptions.File)
                {
                    var ofDialog = new System.Windows.Forms.OpenFileDialog();
                    string destDirectory = "";
                    ofDialog.InitialDirectory = DlkEnvironment.mDirTestSuite;
                    ofDialog.Filter = "XML files (*.xml)|*.xml";
                    ofDialog.Title = "Import Suite";
                    ofDialog.Multiselect = true;

                    if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if ((bool)importDest.ShowDialog())
                        {
                            ImportLocation = Path.GetFullPath(importDest.ImportDestinationPath);
                            foreach (string importFile in ofDialog.FileNames)
                            {
                                int icount = 1;
                                bool bCont = true;
                                string destFile = System.IO.Path.Combine(ImportLocation, System.IO.Path.GetFileName(importFile));
                                //check if filename already exists
                                if (File.Exists(destFile))
                                {
                                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(string.Format(DlkUserMessages.ASK_FILE_EXISTS_IMPORT_FAILED, System.IO.Path.GetFileName(importFile)), "Save Changes?");
                                    switch (res)
                                    {
                                        case MessageBoxResult.Yes:
                                            while (File.Exists(destFile))
                                            {
                                                string tempFileName = string.Format("{0}({1})", System.IO.Path.GetFileNameWithoutExtension(importFile), icount++);
                                                destFile = System.IO.Path.Combine(ImportLocation, tempFileName + ".xml");
                                            }
                                            break;
                                        case MessageBoxResult.No:
                                            destFile = null;
                                            bCont = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                if (bCont)
                                {
                                    try
                                    {
                                        if (DlkTestSuiteXmlHandler.IsValidFormat(importFile))
                                        {
                                            File.Copy(importFile, destFile, false);
                                            File.SetAttributes(destFile, FileAttributes.Normal);                                            

                                            if (!hasImported)
                                            {
                                                hasImported = true;
                                                destDirectory = Path.GetDirectoryName(destFile);
                                            }
                                        }
                                        else
                                        {
                                            DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                                            return;
                                        }
                                    }
                                    catch
                                    {
                                        DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                                        return;
                                    }
                                }
                            }

                            if (hasImported)
                            {
                                InitializeSuitesAndTags(destDirectory);
                                //refresh
                                tvwFoldersExisting.ItemsSource = SuiteFolders;
                                tvwFoldersExisting.Items.Refresh();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSearchSuite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (grdSearchGrid.Visibility == System.Windows.Visibility.Collapsed)
                {
                    grdSearchGrid.Visibility = System.Windows.Visibility.Visible;
                    btnSearchSuite.BorderBrush = Brushes.Navy;
                    txtSearch.Focus();
                    tvwFoldersExisting.IsEnabled = false;
                }
                else
                {
                    txtSearch.Clear();
                    txtSearch.Background = Brushes.LemonChiffon;
                    RefreshTestTree(true);
                    grdSearchGrid.Visibility = System.Windows.Visibility.Collapsed;
                    BrushConverter bc = new BrushConverter();
                    btnSearchSuite.BorderBrush = (Brush)bc.ConvertFrom("#FF828790");
                    tvwFoldersExisting.IsEnabled = true;
                }
                tvwFoldersExisting.Focus();
                TreeViewItem item = tvwFoldersExisting.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
                tvwFoldersExisting.Focus();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(ex.Message);
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                ClearSelectedSuiteInfo();
                string searchtext = ((TextBox)sender).Text;

                if (String.IsNullOrEmpty(searchtext))
                {
                    if (tvwFoldersExisting.SelectedItem != null)
                    {
                        LoadSuiteList(((BFFolder)tvwFoldersExisting.SelectedItem).Path);
                    }
                    else
                    {
                        LoadSuiteList(DlkEnvironment.mDirTestSuite);
                    }
                    txtSearch.Background = Brushes.LemonChiffon;
                    return;
                }

                if (mSearchType != KwDirItem.SearchType.Owner)
                {
                    if (DlkString.IsSearchValid(searchtext))
                    {
                        txtSearch.Background = Brushes.LemonChiffon;
                        LoadSuiteList(DlkEnvironment.mDirTestSuite, searchtext, SearchOption.AllDirectories);
                    }
                    else
                    {
                        DataContext = null;
                        txtSearch.Background = Brushes.LightPink;
                    }
                }
                else
                {
                    txtSearch.Background = Brushes.LemonChiffon;
                    LoadSuiteList(DlkEnvironment.mDirTestSuite, searchtext, SearchOption.AllDirectories);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(ex.Message);
            }
        }

        private void btnSuiteDirectory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExpandCollapseTreeView();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSetSearchType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenContextMenu(sender);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Gets clicked context menu item text
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        private string ProcessMenuItem(object sender)
        {
            if (sender is MenuItem)
            {
                IEnumerable<MenuItem> menuItems = null;
                var mi = (MenuItem)sender;
                if (mi.Parent is ContextMenu)
                    menuItems = ((ContextMenu)mi.Parent).Items.OfType<MenuItem>();
                if (mi.Parent is MenuItem)
                    menuItems = ((MenuItem)mi.Parent).Items.OfType<MenuItem>();
                if (menuItems != null)
                    foreach (var item in menuItems)
                    {
                        if (item.IsCheckable && item == mi)
                        {
                            item.IsChecked = true;
                        }
                        else if (item.IsCheckable && item != mi)
                        {
                            item.IsChecked = false;
                        }
                    }
                return mi.Header.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Opens context menu of object
        /// </summary>
        /// <param name="buttonSender">Parent object of context menu</param>
        private void OpenContextMenu(object buttonSender)
        {
            var addButton = buttonSender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void SearchTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SearchName = ProcessMenuItem(sender);
                switch (SearchName)
                {
                    case MENU_TEXT_SUITESONLY:
                        mSearchType = KwDirItem.SearchType.TestsSuitesOnly;
                        break;
                    case MENU_TEXT_OWNERSONLY:
                        mSearchType = KwDirItem.SearchType.Owner;
                        break;
                    case MENU_TEXT_TAGSONLY:
                        mSearchType = KwDirItem.SearchType.TagsOnly;
                        break;
                    case MENU_TEXT_SUITESANDTAGS:
                        mSearchType = KwDirItem.SearchType.TestsSuitesAndTags;
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Refreshes data of suite explorer treeview
        /// </summary>
        public void RefreshTestTree(bool isFile)
        {
            // refresh for the left pane Suite Explorer
            m_SuiteFolders.Remove(((BFFolder)tvwFoldersExisting.SelectedItem));            
            LoadSuiteList(((BFFolder)tvwFoldersExisting.SelectedItem).Path);
            if (!isFile)
            {
                tvwFoldersExisting.ItemsSource = SuiteFolders;
                tvwFoldersExisting.Items.Refresh();
            }
        }

        /// <summary>
        /// Process worker work completed handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mProcessWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                IsBackgroundProcessInProgress = false;
                UpdateStatusBar();

                object[] resArr = (object[])e.Result;

                if (resArr.Count() != PROCESS_ACTION_RES_EXACT_COUNT)
                {
                    return;
                }

                bool bFuncRes = (bool)resArr[PROCESS_ACTION_RES_IDX_RET];
                if (bFuncRes)
                {
                    ProcessWorkerAction action = (ProcessWorkerAction)resArr[PROCESS_ACTION_RES_IDX_ACTION];

                    switch (action)
                    {
                        case ProcessWorkerAction.DeleteTestSuiteLocal:
                        case ProcessWorkerAction.DeleteAndCheckInSuiteItem:
                            RefreshTestTree(bool.Parse(resArr[PROCESS_ACTION_REX_IDX_ISFILE].ToString()));
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_DELETE_SUCCESSFUL_SELECTED);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Process worker Do Work handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mProcessWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] args = (object[])e.Argument;

                if (args.Count() != PROCESS_ACTION_ARG_EXACT_COUNT)
                {
                    e.Result = new object[] { false, false };
                    return;
                }

                ProcessWorkerAction action = (ProcessWorkerAction)args[PROCESS_ACTION_ARG_IDX_ACTION];
                string sPath = args[PROCESS_ACTION_ARG_IDX_PATH].ToString();
                string sComments = args[PROCESS_ACTION_ARG_IDX_COMMENTS].ToString();
                string sSource = args[PROCESS_ACTION_ARG_IDX_SOURCE].ToString();
                bool isFile = !File.GetAttributes(sPath).HasFlag(FileAttributes.Directory);

                switch (action)
                {
                    case ProcessWorkerAction.DeleteTestSuiteLocal:
                        e.Result = new object[] { ProcessWorkerAction.DeleteTestSuiteLocal, DeleteItemLocal(sPath), sPath, isFile };
                        break;
                    case ProcessWorkerAction.DeleteAndCheckInSuiteItem:
                        e.Result = new object[] { ProcessWorkerAction.DeleteTestSuiteLocal, DeleteItemAndCheckIn(sPath), sPath, isFile };
                        break;
                }

                if (Convert.ToBoolean(((object[])e.Result)[1]))
                {
                    mSuiteFiles.RemoveAll(dir => (isFile && dir.FullName == sPath) || (!isFile && dir.DirectoryName == sPath));
                    mSuiteOwners.RemoveAll(dir => dir != null && ((isFile && dir.FullName == sPath) || (!isFile && dir.DirectoryName == sPath)));
                    mSuiteTags.RemoveAll(dir => dir != null && ((isFile && dir.FullName == sPath) || (!isFile && dir.DirectoryName == sPath)));
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
