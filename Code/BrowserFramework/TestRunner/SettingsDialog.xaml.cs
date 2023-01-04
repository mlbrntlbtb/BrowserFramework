using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System.IO;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingDialog : Window
    {
        public bool ReloadOwner = false;
        private BackgroundWorker m_GetFilesBgw;
        private BackgroundWorker m_LoadDataMappingsBgw;
        private BackgroundWorker m_MapBgw;

        private Delegate m_Process;
        private bool? m_InitialLoadDone = false;
        private bool m_IsMapped = false;
        private bool? m_ShouldMap = false;
        private string m_Server = string.Empty;

        private const string STR_MAPPED = "Folder is mapped";
        private const string STR_UNMAPPED = "Folder is unmapped";
        private const string STR_GETTING_FILES = "Getting files...";
        private const string STR_SERVER_INFO = "Getting server info...";
        private const string STR_MAPPING_FOLDER = "Mapping folder...";
        private const string STR_UNMAPPING_FOLDER = "Removing mapping...";
        private const string STR_GETFILES = "Get Files";
        private const string STR_UPDATING = "Updating";


        private const string FLDR_REFERENCE = @"Reference\";
        private const string FLDR_SCREENSHOTS = @"Screenshots\";
        private const string FLDR_SUITES = @"Suites\";
        private const string FLDR_SCRIPTS = @"Scripts\";
        private const string FLDR_REPORT = @"Report\";
        private const string FLDR_RESULTS = @"Results\";
        private const string FLDR_SCHEDULER = @"Scheduler\";

        public const string FILE_REF_USER = "User.xlsx";
        public const string FILE_REF_ENVIRONMENT = "Environment.xlsx";

        public SettingDialog()
        {
            // get files
            m_GetFilesBgw = new BackgroundWorker();
            m_GetFilesBgw.DoWork += new DoWorkEventHandler(m_GetFilesBgw_DoWork);
            m_GetFilesBgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

            // initial load of mappings
            m_LoadDataMappingsBgw = new BackgroundWorker();
            m_LoadDataMappingsBgw.DoWork += new DoWorkEventHandler(m_LoadDataMappingsBgw_DoWork);
            m_LoadDataMappingsBgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

            // mapping
            m_MapBgw = new BackgroundWorker();
            m_MapBgw.DoWork += new DoWorkEventHandler(m_MapBgw_DoWork);
            m_MapBgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

            m_Server = DlkRegistry.GetProductKeyValue(DlkRegistry.Name, "DataTFSPath");

            InitializeComponent();
            LoadDataMappings();
        }

        #region BACKGROUND_WORKERS
        // Initial load of mappings
        private void LoadDataMappingsBegin()
        {
            m_InitialLoadDone = null;
            ChangeStatus(STR_SERVER_INFO);
            m_Process = new MethodInvoker(LoadDataMappingsDone);
            m_LoadDataMappingsBgw.RunWorkerAsync(txtLocalPath.Text);
        }

        private void m_LoadDataMappingsBgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DlkSourceControlHandler.IsMapped(e.Argument.ToString(), "");
        }

        private void LoadDataMappingsDone()
        {
            m_InitialLoadDone = true;
            string serverPath = GetServerPath();
            IsMapped = !string.IsNullOrEmpty(serverPath);
            chkMap.IsChecked = IsMapped;
            ChangeStatus(IsMapped ? STR_MAPPED : STR_UNMAPPED);
        }

        // Map
        private void MapBegin(bool? Map)
        {
            if (Map == null)
            {
                return;
            }
            ChangeStatus((bool)Map ? STR_MAPPING_FOLDER : STR_UNMAPPING_FOLDER);
            m_Process = new MethodInvoker(MapDone);
            string param = (bool)Map ? txtLocalPath.Text + "~" + m_Server : txtLocalPath.Text;
            m_MapBgw.RunWorkerAsync(param);
        }

        private void m_MapBgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string local = e.Argument.ToString().Split('~').First();
            string server = e.Argument.ToString().Split('~').Last();
            if (!local.Equals(server))
            {
                DlkSourceControlHandler.MapFolder(local, server, "");
                DlkSourceControlHandler.IsMapped(local, "");
            }
            else
            {
                DlkSourceControlHandler.UnmapFolder(local, "");
            }
        }

        private void MapDone()
        {
            string serverPath = GetServerPath();
            IsMapped = !string.IsNullOrEmpty(serverPath);
            chkMap.IsChecked = IsMapped;
            ChangeStatus(IsMapped ? STR_MAPPED : STR_UNMAPPED);
        }

        private void GetFilesBegin()
        {
            ChangeStatus(STR_GETTING_FILES);
            m_Process = new MethodInvoker(GetFilesDone);
            m_GetFilesBgw.RunWorkerAsync(!IsMapped ? txtLocalPath.Text + "~" + m_Server : txtLocalPath.Text);
        }

        private void m_GetFilesBgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string local = e.Argument.ToString().Split('~').First();
            string server = e.Argument.ToString().Split('~').Last();
            bool mapped = local.Equals(server);
            if (!mapped)
            {
                string param = local + "~" + server;
                m_MapBgw_DoWork(null, new DoWorkEventArgs(param));
            }
            DlkSourceControlHandler.GetFiles(local, "/recursive /all");
        }

        private void GetFilesDone()
        {
            ChangeStatus(STR_MAPPED);
            this.ReloadOwner = true;
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(m_Process, null);
        }
        #endregion

        /// <summary>
        /// Flag to indicate if local folder is mapped to server
        /// </summary>
        public bool IsMapped
        {
            get
            {
                return m_IsMapped;
            }
            set
            {
                m_IsMapped = value;
                ((MainWindow)this.Owner).IsMapped = value;
            }
        }

        /// <summary>
        /// Set correct state of chkMap on form load
        /// </summary>
        private void LoadDataMappings()
        {
            try
            {
                chkMap.IsEnabled = DlkSourceControlHandler.SourceControlEnabled;
                if (chkMap.IsEnabled)
                {
                    LoadDataMappingsBegin();
                }
            }
            catch
            {
                // Ignore for now
            }
        }

        /// <summary>
        /// Set values of controls based from checked state of chkMap
        /// </summary>
        /// <param name="State">Checked state of chkMap</param>
        private void ChangeMapCheckState(bool State)
        {
            ChangeStatus(State ? STR_MAPPED : STR_UNMAPPED);
            m_ShouldMap = State;
            txtServerPath.Text = State ? m_Server : string.Empty;
        }

        /// <summary>
        /// Change state of controls based from current status of form
        /// </summary>
        /// <param name="Status">Status of form</param>
        private void ChangeStatus(string Status)
        {
            switch (Status)
            {
                case STR_MAPPED:
                    prbBackground.IsIndeterminate = false;
                    prbBackground.Value = 100;
                    btnGetFiles.Visibility = System.Windows.Visibility.Visible;
                    chkMap.IsEnabled = true;
                    break;
                case STR_UNMAPPED:
                    prbBackground.IsIndeterminate = false;
                    prbBackground.Value = 0;
                    btnGetFiles.Visibility = System.Windows.Visibility.Hidden;
                    chkMap.IsEnabled = true;
                    break;
                default:
                    prbBackground.IsIndeterminate = true;
                    btnGetFiles.Visibility = System.Windows.Visibility.Hidden;
                    chkMap.IsEnabled = false;
                    break;
            }
            lblStatus.Content = Status;
        }

        /// <summary>
        /// Returns the server path of local mapped directory
        /// </summary>
        /// <returns>Returns server path of local mapped directory or Empty string if not determined</returns>
        private string GetServerPath()
        {
            string ret = string.Empty;
            string log = System.IO.Path.Combine(DlkEnvironment.DirRoot, DlkSourceControlHandler.FILE_SRC_CONTROL_LOG);
            if (File.Exists(log))
            {
                foreach (string line in File.ReadAllLines(log))
                {
                    if (line.Contains("$"))
                    {
                        ret = line.Substring(0, line.IndexOf(':')).Trim();
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Set text to be displayed in localpath textbox on load
        /// </summary>
        private void LoadDataFolderValue()
        {
            txtLocalPath.Text = DlkRegistry.DataRoot;
        }

        /// <summary>
        /// Save the chosen local directory to registry and performs neccessary cloning of required directories 
        /// and files
        /// </summary>
        private void SaveDataDirectory()
        {
            string oldAbsPathFramework = DlkRegistry.DataRoot;
            string newAbsPathFramework = txtLocalPath.Text.TrimEnd('\\') + "\\";

            // Copy over required env files to new dir if not existing
            if (!oldAbsPathFramework.Equals(newAbsPathFramework))
            {
                // Create required dirs as needed
                CreateRequiredDirectories(newAbsPathFramework);

                // Copy over required files as needed
                CreateRequiredFilesIfNotExist(oldAbsPathFramework, newAbsPathFramework);
                DlkRegistry.UpdateProductKey(DlkRegistry.Name, new KeyValuePair<string, string>(DlkRegistry.REGVAL_DATAROOT, newAbsPathFramework));
                DlkRegistry.DataRoot = newAbsPathFramework;
            }
        }

        /// <summary>
        /// Creates required directories if not exist
        /// </summary>
        /// <param name="ParentDir">Absolute path of parent directory</param>
        private void CreateRequiredDirectories(string ParentDir)
        {
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_SCREENSHOTS));
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_REFERENCE));
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_SUITES));
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_SCRIPTS));
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_REPORT));
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_RESULTS));
            Directory.CreateDirectory(System.IO.Path.Combine(ParentDir, FLDR_SCHEDULER));
        }

        /// <summary>
        /// Copies required files to new chosen directory
        /// </summary>
        /// <param name="oldDir">Source directory</param>
        /// <param name="newDir">Destination directory</param>
        private void CreateRequiredFilesIfNotExist(string oldDir, string newDir)
        {
            // ref file environment
            if (!File.Exists(System.IO.Path.Combine(newDir, FLDR_REFERENCE) + FILE_REF_ENVIRONMENT))
            {
                File.Copy(System.IO.Path.Combine(oldDir, FLDR_REFERENCE) + FILE_REF_ENVIRONMENT,
                    System.IO.Path.Combine(newDir, FLDR_REFERENCE) + FILE_REF_ENVIRONMENT);
            }

            // ref file user
            if (!File.Exists(System.IO.Path.Combine(newDir, FLDR_REFERENCE) + FILE_REF_USER))
            {
                File.Copy(System.IO.Path.Combine(oldDir, FLDR_REFERENCE) + FILE_REF_USER,
                    System.IO.Path.Combine(newDir, FLDR_REFERENCE) + FILE_REF_USER);
            }
        }

        #region EVENT_HANDLERS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadDataFolderValue();
                if (m_InitialLoadDone == null)
                {
                    return;
                }
                else if (!((bool)m_InitialLoadDone))
                {
                    LoadDataMappings();
                }
                else
                {
                    chkMap.IsChecked = IsMapped;
                    ChangeMapCheckState(IsMapped);
                }
            }
            catch
            {
                // Ignore for now
            }
        }

        private void chkMap_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeMapCheckState(true);
            }
            catch
            {
                // Ignore for now
            }
        }

        private void chkMap_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeMapCheckState(false);
            }
            catch
            {
                // Ignore for now
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveDataDirectory();
                if (lblStatus.Content.ToString() != STR_GETTING_FILES)
                {
                    MapBegin(IsMapped != m_ShouldMap ? m_ShouldMap : null);
                }
                this.Hide();
            }
            catch
            {

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window_Loaded(null, null);
                this.ReloadOwner = false;
                this.Hide();
            }
            catch
            {
                // Ignore for now
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.SelectedPath = txtLocalPath.Text;
                if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtLocalPath.Text = folderBrowser.SelectedPath;
                    if (txtLocalPath.Text.Length <= 3 && txtLocalPath.Text.EndsWith(":\\"))
                    {
                        txtLocalPath.Text += "Data";
                    }
                    LoadDataMappings();
                }
            }
            catch
            {
                // Ignore for now
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                Window_Loaded(null, null);
                this.ReloadOwner = false;
                this.Hide();
            }
            catch
            {
                // Ignore for now
            }
        }

        private void btnGetFiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetFilesBegin();
            }
            catch
            {
                // Ignore for now
            }
        }
        #endregion


    }
}
