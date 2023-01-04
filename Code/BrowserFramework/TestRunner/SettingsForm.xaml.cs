using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CommonLib.DlkUtility;
using TestRunner.Common;
using TestRunner.Controls;
using TestRunner.Designer;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window, INotifyPropertyChanged
    {
        #region DECLARATIONS
        private readonly string STR_TEST_TEMPLATE_PATH = Path.Combine(DlkEnvironment.mDirProduct, "Tests\\template.dat");
        private readonly string STR_TEST_CONNECT_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "test_connect.dat");

        private const int DEFAULT_TAB_INDEX = 0;
        private const int MAX_ID_CHARS = 50;
        private const int MAX_NAME_CHARS = 100;
        private const int MAX_PATH_CHARS = 260;
        private bool bIsDirty;
        private bool bSettingsDirty;
        private bool bSchedulerDirty;
        private bool bIsTagDirty = false;
        private bool bIsOkOrCancelClose = false;
        private string mOrigURLBlacklist;
        private string mOrigTestDir;
        private string mOrigSuiteDir;
        private string mOrigEnvID;
        private string mOrigRemoteID;
        private string mOrigMobileID;
        //private bool bUseDatabase;
        private bool bDashboardDirty = false;
        private DlkTag mSelectedTag = new DlkTag(string.Empty, string.Empty); /* Dummy default tag */
        private TagPageMode mTagPageMode = TagPageMode.Default;
        public Dictionary<string, string> errLogDict = new Dictionary<string, string>();
        private List<DlkLoginConfigRecord> m_Records = new List<DlkLoginConfigRecord>();
        public event PropertyChangedEventHandler PropertyChanged;
        private DlkBackgroundWorkerWithAbort mTestConnectionWorker = new DlkBackgroundWorkerWithAbort();
        private List<DlkMobileOSRecord> verList = new List<DlkMobileOSRecord>();
        private List<String> versionList = new List<string>();

        private bool bIsTestDirExist = true;
        private bool bIsSuiteDirExist = true;
        private bool bIsURLValid = true;
        private bool bIsURLRecentlyBlank = true;
        private bool bIsCloning = false;
        private string invalidURLMessage = string.Empty;
        private string defaultBrowser = string.Empty;
        private string newDefaultBrowser = string.Empty;
        private string mTestDirectory = string.Empty;
        private string mSuiteDirectory = string.Empty;
        private string mURLBlacklist = string.Empty;
        private DlkProductConfigHandler mProdConfigHandler;

        /// <summary>
        /// UI states of Tab page
        /// </summary>
        public enum TagPageMode
        {
            Default,
            Add,
            Edit
        }
        #endregion

        #region PROPERTIES
        public string URLBlacklist
        {
            get
            {
                return mURLBlacklist;
            }
            set
            {
                mURLBlacklist = value;
                OnPropertyChanged("URLBlacklist");
            }
        }

        public bool IsBrowserSelected
        {
            get
            {
                return lstBrowsers.SelectedIndex > -1;
            }
            set
            {
                OnPropertyChanged("IsBrowserSelected");
            }
        }

        public string TestDirectory
        {
            get
            {
                return mTestDirectory;
            }
            set
            {
                mTestDirectory = value;
                OnPropertyChanged("TestDirectory");
            }
        }


        public string SuiteDirectory
        {
            get
            {
                return mSuiteDirectory;
            }
            set
            {
                mSuiteDirectory = value;
                OnPropertyChanged("SuiteDirectory");
            }
        }

        public bool IsSuiteDirectoryDirty
        {
            get
            {
                return txtSuiteDir.Text.Trim() != mOrigSuiteDir;
            }
        }

        public bool IsTestDirectoryDirty
        {
            get
            {
                return txtTestDir.Text.Trim() != mOrigTestDir;
            }
        }

        public bool isURLBlacklistDirty
        {
            get
            {
                return txtURLBlacklist.Text != mOrigURLBlacklist;
            }
        }

        public bool IsShowAppNameDirty
        {
            get
            {
                return grpBoxObjectStore.Visibility == Visibility.Visible && chkShowApplicationName.IsChecked != mShowAppName;
            }
        }

        public List<DlkLoginConfigRecord> Environments
        {
            get
            {
                return m_Records;
            }
        }

        private bool IsTargetApplicationChanged()
        {
            return ((DlkTargetApplication)cmbTargetApplication.SelectedItem).ID != DlkTestRunnerSettingsHandler.ApplicationUnderTest.ID;
        }

        public bool IsURLValid
        {
            get
            {
                return bIsURLValid;
            }
            set
            {
                bIsURLValid = value;
                OnPropertyChanged("IsURLValid");
            }
        }

        public bool IsURLRecentlyBlank
        {
            get
            {
                return bIsURLRecentlyBlank;
            }
            set
            {
                bIsURLRecentlyBlank = value;
                OnPropertyChanged("IsURLRecentlyBlank");
            }
        }

        public string InvalidURLMessage
        {
            get
            {
                return invalidURLMessage;
            }
            set
            {
                invalidURLMessage = value;
                OnPropertyChanged("InvalidURLMessage");
            }
        }

        private bool mUseDefaultEmail
        {
            get
            {
                bool useDefaultEmail;
                Boolean.TryParse(DlkConfigHandler.GetConfigValue("usedefaultemail"), out useDefaultEmail);
                return chkUseDefaultEmail.IsEnabled && useDefaultEmail;
            }
            set
            {
                try
                {
                    DlkConfigHandler.UpdateConfigValue("usedefaultemail", value.ToString());
                }
                catch
                {
                    // do nothing
                }
            }
        }

        private bool DoNotCheckInEnvironment
        {
            get
            {
                bool doNotcheckinEnvInfo;
                Boolean.TryParse(DlkConfigHandler.GetConfigValue("donotcheckinenvironmentinfo"), out doNotcheckinEnvInfo);
                return chkDoNotCheckInEnv.IsEnabled && doNotcheckinEnvInfo;
            }
            set
            {
                try
                {
                    DlkConfigHandler.UpdateConfigValue("donotcheckinenvironmentinfo", value.ToString());
                }
                catch
                {
                    // do nothing
                }
            }
        }

        private bool mShowAppName
        {
            get
            {
                bool showAppNameVal = false;
                Boolean.TryParse(DlkConfigHandler.GetConfigValue("showappname"), out showAppNameVal);
                return showAppNameVal;
            }
        }

        public bool IsTargetAppIncludedShowAppName
        {
            get
            {
                string currentApp = ((DlkTargetApplication)cmbTargetApplication.SelectedItem).ProductFolder;
                return new string[]
                {
                                    "BudgetingAndPlanning",
                                    "Costpoint_711",
                                    "TimeAndExpense",
                                    "Costpoint_701"
                }.Contains(currentApp);
            }
        }

        private bool GetlatestOnLaunch
        {
            get
            {
                bool getLatestOnLaunch;
                Boolean.TryParse(DlkConfigHandler.GetConfigValue("getlatestversiononlaunch"), out getLatestOnLaunch);
                return chkGetLatestOnLaunch.IsEnabled && getLatestOnLaunch;
            }
            set
            {
                try
                {
                    DlkConfigHandler.UpdateConfigValue("getlatestversiononlaunch", value.ToString());
                }
                catch
                {
                    // do nothing
                }
            }
        }

        private bool mUseCustomEmailAddress
        {
            get
            {
                return Convert.ToBoolean(DlkConfigHandler.GetConfigValue("usecustomsenderadd"));
            }
            set
            {
                DlkConfigHandler.UpdateConfigValue("usecustomsenderadd", value.ToString());
            }
        }
 
        private bool IsSourceControlSupported()
        {
            return DlkSourceControlHandler.SourceControlSupported;
        }

        #endregion

        #region CONSTRUCTOR
        public SettingsForm(MainWindow Owner)
        {
            InitializeComponent();
            this.Owner = Owner;
            btnTestConnection.Visibility = DlkTestRunnerSettingsHandler.ApplicationUnderTest.Type.ToLower() == "internal" ? Visibility.Collapsed : Visibility.Visible;
#if DEBUG
#else
            tpgMobile.Visibility = Visibility.Collapsed;
            tpgRemoteBrowser.Visibility = Visibility.Collapsed;
#endif
            if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Platform.ToLower().Equals("mobile"))
            {
                PinPanel.Visibility = Visibility.Visible;
            }
            else
            {
                PinPanel.Visibility = Visibility.Collapsed;
            }
            MetaDataPanel.Visibility = DlkTestRunnerSettingsHandler.ApplicationUnderTest.Type.ToLower() == "internal" ? Visibility.Collapsed : Visibility.Visible;

            //Source control feature available only for internal clients
#if DEBUG
            gbSourceControl.Visibility = Visibility.Visible;
            gbSourceControl.IsEnabled = IsSourceControlSupported();
#else
            gbSourceControl.Visibility = Visibility.Collapsed;
#endif
            //Show app name feature available only for specific products: [CP, BnP, TE]
            if (DlkEnvironment.IsShowAppNameProduct)
                grpBoxObjectStore.Visibility = Visibility.Visible;
            else
                grpBoxObjectStore.Visibility = Visibility.Collapsed;

            Initialize();
        }

        private void Initialize()
        {
            LockFields(true);
            m_Records = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords.ToList<DlkLoginConfigRecord>();
            //dgvEnvironments.ItemsSource = Environments;
            lstEnvs.ItemsSource = Environments;
            lstEnvs.DisplayMemberPath = "mID";

            tabEnvironments.SelectedIndex = DEFAULT_TAB_INDEX;
            if (lstEnvs.Items.Count > 0)
            {
                lstEnvs.SelectedIndex = 0;
            }
            else
            {
                rdoDefault.IsChecked = true;
            }

            //remote browsers
            LockRemoteBrowsersFields(true);
            m_RemoteBrowsersRec = DlkRemoteBrowserHandler.mRemoteBrowsers;
            lstRemoteBrowsers.ItemsSource = RemoteBrowsers;
            lstRemoteBrowsers.DisplayMemberPath = "Id";
            if (lstRemoteBrowsers.Items.Count > 0)
            {
                lstRemoteBrowsers.SelectedIndex = 0;
            }
            cboRemoteBrowserType.ItemsSource = new string[] {"IE", "Firefox", "Chrome", "Safari" };
            cboRemoteBrowserType.DataContext = lstRemoteBrowsers.SelectedItem;

            /* target application */
            cmbTargetApplication.ItemsSource = DlkTestRunnerSettingsHandler.ApplicationList;
            cmbTargetApplication.SelectedItem = DlkTestRunnerSettingsHandler.ApplicationUnderTest;

            //mobile devices
            LockMobileFields(true, true);
            m_MobileRec = DlkMobileHandler.mMobileRec;
            lstMobile.ItemsSource = MobileDevices;
            lstMobile.DisplayMemberPath = "MobileId";
            if(lstMobile.Items.Count > 0)
            {
                lstMobile.SelectedIndex = 0;
            }

            chkEmulator.DataContext = lstMobile.SelectedItem;
            cboMobileType.ItemsSource = new string[] { "Android", "iOS" };
            cboMobileType.DataContext = lstMobile.SelectedItem;
            txtApplication.DataContext = lstMobile.SelectedItem;
            cmbMobBrowser.DataContext = lstMobile.SelectedItem;
            txtPath.DataContext = lstMobile.SelectedItem;

            if (lstMobile.SelectedItem != null && (((DlkMobileRecord)(lstMobile.SelectedItem)).Application != null) 
                && (((DlkMobileRecord)(lstMobile.SelectedItem)).Application.ToLower() == "safari"
                || ((DlkMobileRecord)(lstMobile.SelectedItem)).Application.ToLower() == "chrome"))
            {
                optMobBrowser.IsChecked = true;
            }
            else
            {
                optAppID.IsChecked = true;
            }

            if (cboMobileType.Text == "Android")
            {
                cmbMobBrowser.ItemsSource = new string[] { "Chrome" };
            }
            else if (cboMobileType.Text == "iOS")
            {
                cmbMobBrowser.ItemsSource = new string[] { "Safari" };
            }

            versionList.Clear();
            if (lstMobile.Items.Count != 0)
            {
                versionList = verList.Select(x => x.Target).ToList();
                cboDeviceVersion.ItemsSource = versionList;
                cboDeviceVersion.DataContext = lstMobile.SelectedItem;
            }
            else
            {
                verList = DlkMobileOSRecord.LoadVersionList();
            }

            // source control
            chkEnableSourceControl.IsChecked = DlkSourceControlHandler.SourceControlEnabled;
            chkGetLatestOnLaunch.IsChecked = Boolean.Parse(DlkConfigHandler.GetConfigValue("getlatestversiononlaunch"));
            chkDoNotCheckInEnv.IsChecked = Boolean.Parse(DlkConfigHandler.GetConfigValue("donotcheckinenvironmentinfo"));
            chkGetLatestOnLaunch.IsEnabled = (bool)chkEnableSourceControl.IsChecked;
            chkDoNotCheckInEnv.IsEnabled = (bool)chkEnableSourceControl.IsChecked;
            //chkDoNotCheckInEnv.DataContext = this;

            // show app name object store settings
            chkShowApplicationName.IsChecked = mShowAppName;

            //installed browsers
            defaultBrowser = DlkEnvironment.GetDefaultBrowserNameOrIndex(returnName: true);
            newDefaultBrowser = defaultBrowser;
            lstBrowsers.ItemsSource = DlkEnvironment.mAvailableBrowsers;
            lstBrowsers.DisplayMemberPath = "Name";

            //configurations
            errLogDict.Add("default", "Show All");
            errLogDict.Add("errorinfo", "Show Basic Error Info");
            errLogDict.Add("erroronly", "Show Error Only");
            cmbErrLogLevel.ItemsSource = errLogDict.Values;
            string errValue;
            if(errLogDict.TryGetValue(DlkEnvironment.mErrorLogLevel,out errValue)){
                cmbErrLogLevel.SelectedItem = errValue;
            }

            // source control tab
            cmbSourceControlType.ItemsSource = new string[] { "MS Team Foundation Server" };
            cmbSourceControlType.SelectedIndex = 0;

            txtServerTestDirectory.Text = DlkEnvironment.mDirTests;

            //schedulersettings
            txtSMTPServer.Text = DlkConfigHandler.GetConfigValue("smtphost");
            txtSMTPPort.Text = DlkConfigHandler.GetConfigValue("smtpport");

            txtSMTPUser.Text = DlkConfigHandler.GetConfigValue("smtpuser");
            pbSMTPPass.Password = DlkConfigHandler.GetConfigValue("smtppass");


            chkUseCustomSender.IsChecked = mUseCustomEmailAddress;
            txtCustomSenderAdd.Text = DlkConfigHandler.GetConfigValue("customsenderadd");
            txtMailAddresses.Text = DlkConfigHandler.GetConfigValue("defaultemail");
            chkUseDefaultEmail.IsChecked = mUseDefaultEmail;
            txtMailAddresses.IsEnabled = (bool)chkUseDefaultEmail.IsChecked;

            ////dashboard results settings
            //if (Convert.ToBoolean(DlkConfigHandler.GetConfigValue("usedatabase")))
            //{
            //    optDatabase.IsChecked = true;
            //    bUseDatabase = true;
            //}
            //else
            //{
            //    optLocal.IsChecked = true;
            //    bUseDatabase = false;
            //}
            //txtServer.Text = DlkConfigHandler.GetConfigValue("dbserver");
            //txtResultDatabase.Text = DlkConfigHandler.GetConfigValue("dbname");
            //txtUserDatabase.Text = DlkConfigHandler.GetConfigValue("dbuser");
            //txtPasswordDatabase.Password = DlkConfigHandler.GetConfigValue("dbpassword");
            //chkSubmitResults.IsChecked = Convert.ToBoolean(DlkConfigHandler.mResultsDbRecordResults);
            chkEnableDashboardRecording.IsChecked = DlkConfigHandler.mDashboardEnabled;
            //disable edit/clone/delete if there are no items
            btnEdit.IsEnabled = lstEnvs.Items.Count > 0;
            btnClone.IsEnabled = lstEnvs.Items.Count > 0;
            btnDelete.IsEnabled = lstEnvs.Items.Count > 0;
            btnEditRemoteBrowser.IsEnabled = lstRemoteBrowsers.Items.Count > 0;
            btnCloneRemoteBrowser.IsEnabled = lstRemoteBrowsers.Items.Count > 0;
            btnDeleteRemoteBrowser.IsEnabled = lstRemoteBrowsers.Items.Count > 0;
            btnEditMobile.IsEnabled = lstMobile.Items.Count > 0;
            btnCloneMobile.IsEnabled = lstMobile.Items.Count > 0;
            btnDeleteMobile.IsEnabled = lstMobile.Items.Count > 0;

            /* Tags */
            InitializeTags();

            mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\ProdConfig.xml"));
            mProdConfigHandler.UpdateConfigValue("defaultsuitepath", Path.Combine(DlkEnvironment.mDirProduct, "Suites"));
            mProdConfigHandler.UpdateConfigValue("defaulttestpath", Path.Combine(DlkEnvironment.mDirProduct, "Tests"));
            TestDirectory = GetTestDirectory();
            SuiteDirectory = GetSuiteDirectory();
            URLBlacklist = GetURLBlacklist();
            mOrigSuiteDir = SuiteDirectory;
            mOrigTestDir = TestDirectory;
            mOrigURLBlacklist = URLBlacklist;

            DlkGlobalVariables.mIsTestDirChanged = false;
            DlkGlobalVariables.mIsSuiteDirChanged = false;
            DlkGlobalVariables.mIsApplicationChanged = false;

#if DEBUG
            grpTestDirectories.IsEnabled = false;
#endif
        }

        private bool ValidateURL(String URL)
        {
            Uri uriResult;
            bool isURLValid = Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
            && Uri.IsWellFormedUriString(URL, UriKind.Absolute);
            return isURLValid;
        }

        private string GetURLBlacklist()
        {
            return mProdConfigHandler.GetConfigValue("blacklisturl");
        }

        private bool UpdateURLBlacklist()
        {
            string[] urlToSave = txtURLBlacklist.Text.Split(';');

            if (!string.IsNullOrEmpty(txtURLBlacklist.Text.Trim()) && urlToSave.Count() > 0)
            {
                string formatUrl(string url) => url.TrimEnd(' ', '/', '?').ToLower().Replace("https://", "http://");
                int urlCount = urlToSave.Count();

                for (int i = 0; i < urlCount; i++)
                {
                    string url = urlToSave[i];

                    //multiple delimiter
                    if (url == "" && i + 1 < urlCount && !(i == 0 && urlToSave[i + 1] != ""))
                    {
                        DlkUserMessages.ShowWarning(DlkUserMessages.WRN_DUPLICATE_BLACKLIST_DELIMITER);
                        return false;
                    }

                    //url format validation
                    bool isURLValid = url.ToLower().StartsWith("http://") || url.ToLower().StartsWith("https://");
                    if (!isURLValid)
                    {
                        DlkUserMessages.ShowWarning(string.Format("URL Blacklist '{0}' should start with http:// or https://", url)); ;
                        return false;
                    }
                    else if (!ValidateURL(url))
                    {
                        DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.WRN_BLACKLIST_URL_INCORRECT_FORMAT, url));
                        return false;
                    }

                    if (i == 0)
                        continue;

                    for (int j = 0; j < i; j++)
                    {
                        string urlToMatch = urlToSave[j];
                        //check for existing domain
                        if (url != urlToMatch && DlkEnvironment.GetRootHost(formatUrl(url)) == DlkEnvironment.GetRootHost(formatUrl(urlToMatch)))
                        {
                            DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.WRN_DUPLICATE_BLACKLIST_URL_DOMAIN, url, urlToMatch));
                            return false;
                        }
                        //check for duplicates
                        else if (formatUrl(urlToMatch) == formatUrl(url)) 
                        {
                            DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.WRN_DUPLICATE_BLACKLIST_URL, url));
                            return false;
                        }
                    }
                }
            }

            string url_ToSave = "";
            for(int i = 0; i < urlToSave.Count(); i++)
            {
                if (!urlToSave[i].Equals(""))
                {
                    if (i == urlToSave.Count() - 1)
                    {
                        url_ToSave = url_ToSave + urlToSave[i].Trim().ToLower();
                    }
                    else
                    {
                        url_ToSave = url_ToSave + urlToSave[i].Trim().ToLower() + ";";
                    }
                }
            }

            mProdConfigHandler.UpdateConfigValue("blacklisturl", url_ToSave);
            DlkEnvironment.InitializeBlacklistedURLs();
            return true;
        }

        private string GetTestDirectory()
        {
            string retPath = mProdConfigHandler.GetConfigValue("defaulttestpath");

            if (mProdConfigHandler.ConfigNodeExists("testpath"))
            {
                string val = mProdConfigHandler.GetConfigValue("testpath");
                if (val != null && Directory.Exists(val))
                {
                    retPath = val;
                }
            }
            return retPath;
        }

        private void UpdateTestDirectory()
        {
            if (Directory.Exists(TestDirectory) && !(new Regex(@"(?<=.{2})(\\)\1+").IsMatch(txtTestDir.Text)) && hasWriteAccessToFolder(txtTestDir.Text))
            {
                mProdConfigHandler.UpdateConfigValue("testpath", txtTestDir.Text.Trim());
                string testConnect = Path.Combine(txtTestDir.Text.Trim(), "test_connect.dat");
                if (!File.Exists(testConnect))
                {
                    File.Copy(STR_TEST_CONNECT_PATH, testConnect);
                }
                string testTemplate = Path.Combine(txtTestDir.Text.Trim(), "template.dat");
                if (!File.Exists(testTemplate))
                {
                    File.Copy(STR_TEST_TEMPLATE_PATH, testTemplate);
                }
                bIsTestDirExist = true;
                DlkGlobalVariables.mIsTestDirChanged = true;
            }
            else
            {
                DlkUserMessages.ShowError(String.IsNullOrWhiteSpace(txtTestDir.Text) ? DlkUserMessages.ERR_TEST_DIR_PATH_EMPTY : string.Format(DlkUserMessages.ERR_TEST_DIR_PATH_NOT_EXIST, txtTestDir.Text.Trim()));
                bIsTestDirExist = false;
                DlkGlobalVariables.mIsTestDirChanged = false;
            }
        }

        private string GetSuiteDirectory()
        {
            string retPath = mProdConfigHandler.GetConfigValue("defaultsuitepath");

            if (mProdConfigHandler.ConfigNodeExists("suitepath"))
            {
                string val = mProdConfigHandler.GetConfigValue("suitepath");
                if (val != null && Directory.Exists(val))
                {
                    retPath = val;
                }
            }
            return retPath;
        }

        private void UpdateSuiteDirectory()
        {
            if (Directory.Exists(SuiteDirectory) && !(new Regex(@"(?<=.{2})(\\)\1+").IsMatch(txtSuiteDir.Text)) && hasWriteAccessToFolder(txtSuiteDir.Text))
            {
                mProdConfigHandler.UpdateConfigValue("suitepath", txtSuiteDir.Text.Trim());
                bIsSuiteDirExist = true;
                DlkGlobalVariables.mIsSuiteDirChanged = true;
            }
            else
            {
                DlkUserMessages.ShowError(String.IsNullOrWhiteSpace(txtSuiteDir.Text) ? DlkUserMessages.ERR_SUITE_DIR_PATH_EMPTY : string.Format(DlkUserMessages.ERR_SUITE_DIR_PATH_NOT_EXIST, txtSuiteDir.Text.Trim()));
                bIsSuiteDirExist = false;
                DlkGlobalVariables.mIsSuiteDirChanged = false;
            }
        }

        private bool hasWriteAccessToFolder(string folderPath)
        {
            try
            {
                string tempPath = Path.Combine(folderPath, Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempPath);

                if(Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///<summary>
        /// Saves currently updated list config file 
        /// for settings environment, remote browsers, mobile and tags tab
        ///</summary>
        private void SaveSettingsList()
        {
            if (bIsDirty)
            {
                //Save current list of environments
                FileInfo fi = new FileInfo(DlkEnvironment.mLoginConfigFile) { IsReadOnly = false };
                new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).Save(DlkEnvironment.mLoginConfigFile, Environments);
                bIsDirty = false;
            }
            if (bIsRemoteBrowsersDirty)
            {
                //Save current list of remote browsers
                FileInfo fi = new FileInfo(DlkEnvironment.mRemoteBrowsersFile) { IsReadOnly = false };
                DlkRemoteBrowserHandler.Save(RemoteBrowsers);
                ((MainWindow)this.Owner).InitializeBrowserLists();
                bIsRemoteBrowsersDirty = false;
            }
            if (bIsMobileDirty)
            {
                //Save current list of mobile devices
                DlkEnvironment.mIsMobileSettingChange = bIsMobileDirty;
                FileInfo fi = new FileInfo(DlkEnvironment.mMobileConfigFile) { IsReadOnly = false };
                DlkMobileHandler.Save(MobileDevices);
                ((MainWindow)this.Owner).InitializeBrowserLists();
                bIsMobileDirty = false;
            }
            if (bIsTagDirty)
            {
                //Save current list of tags
                DlkTag.SaveTags(AllTags.ToList());
                ((MainWindow)this.Owner).InitializeBackgroundRefresh();
                bIsTagDirty = false;
            }
        }
        #endregion

        #region EVENTS
        private void lnkURLBlacklist_Click(object sender, RoutedEventArgs e)
        {
            if (tbURLBlackList.Text == "Show URL Blacklist")
            {
                grpURLBlackList.Visibility = Visibility.Visible;
                tbURLBlackList.Text = "Hide URL Blacklist";
            }
            else if (tbURLBlackList.Text == "Hide URL Blacklist")
            {
                grpURLBlackList.Visibility = Visibility.Hidden;
                tbURLBlackList.Text = "Show URL Blacklist";
            }
        }

        private void btnBrowseTestDir_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select Test Directory";
                DialogResult result = dlg.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    DlkGlobalVariables.mOldTestDirPath = txtTestDir.Text;
                    txtTestDir.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnBrowseSuiteDir_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select Suite Directory";
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    DlkGlobalVariables.mOldSuiteDirPath = txtSuiteDir.Text;
                    txtSuiteDir.Text = dlg.SelectedPath;
                }
            }
        }

        private void txtTestDir_GotFocus(object sender, RoutedEventArgs e)
        {
            DlkGlobalVariables.mOldTestDirPath = txtTestDir.Text;
        }

        private void txtSuiteDir_GotFocus(object sender, RoutedEventArgs e)
        {
            DlkGlobalVariables.mOldSuiteDirPath = txtSuiteDir.Text;
        }

        private void txtUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            popupTxtUrl.IsOpen = false;
            IsURLRecentlyBlank = txtUrl.Text == "";
            if (txtUrl.IsEnabled)
            {
                if (txtUrl.Text.Length == 2083)
                {
                    popupTxtUrl.IsOpen = true;
                }
                ChangeURLErrorText(txtUrl.Text);
                IsURLValid = ValidateURL();
            }
        }

        private void txtMobileBrowserUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            popupMobileBrowserUrl.IsOpen = false;
            IsURLRecentlyBlank = txtMobileBrowserUrl.Text == "";
            if (txtMobileBrowserUrl.IsEnabled)
            {
                if (txtMobileBrowserUrl.Text.Length == 2083)
                {
                    popupMobileBrowserUrl.IsOpen = true;
                }
                ChangeURLErrorText(txtMobileBrowserUrl.Text);
                IsURLValid = ValidateURL(txtMobileBrowserUrl.Text);
            }
        }

        private void txtRemoteBrowserUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            popupRemoteBrowserUrl.IsOpen = false;
            IsURLRecentlyBlank = txtRemoteBrowserUrl.Text == "";
            if (txtRemoteBrowserUrl.IsEnabled)
            {
                if (txtRemoteBrowserUrl.Text.Length == 2083)
                {
                    popupRemoteBrowserUrl.IsOpen = true;
                }
                ChangeURLErrorText(txtRemoteBrowserUrl.Text);
                IsURLValid = ValidateURL(txtRemoteBrowserUrl.Text);
            }
        }

        private void txtDeviceName_TextChanged(object sender, TextChangedEventArgs e)
        {
           popuptxtDeviceName.IsOpen = false;

           if (txtDeviceName.Text.Length == MAX_NAME_CHARS && txtDeviceName.IsEnabled)
           {
                popuptxtDeviceName.IsOpen = true;
           }
        }

        private void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {
            popuptxtID.IsOpen = false;

            if (txtID.Text.Length == MAX_ID_CHARS && txtID.IsEnabled)
            {
                popuptxtID.IsOpen = true;
            }
        }

        private void txtRemoteBrowserID_TextChanged(object sender, TextChangedEventArgs e)
        {
            popupRemoteBrowserID.IsOpen = false;

            if (txtRemoteBrowserID.Text.Length == MAX_ID_CHARS && txtRemoteBrowserID.IsEnabled)
            {
                popupRemoteBrowserID.IsOpen = true;
            }
        }

        private void txtMobileID_TextChanged(object sender, TextChangedEventArgs e)
        {
            popupMobileID.IsOpen = false;

            if (txtMobileID.Text.Length == MAX_ID_CHARS && txtMobileID.IsEnabled)
            {
                popupMobileID.IsOpen = true;
            }
        }

        private void txtTagName_TextChanged(object sender, TextChangedEventArgs e)
        {
            popuptxtTageName.IsOpen = false;
            if (txtTagName.Text.Length == MAX_NAME_CHARS && txtTagName.IsEnabled)
            {
                popuptxtTageName.IsOpen = true;
            }
        }

        private void txtPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            popuptxtMobilePackagePath.IsOpen = false;
            if(txtPath.Text.Length == MAX_PATH_CHARS && txtPath.IsEnabled)
            {
                popuptxtMobilePackagePath.IsOpen = true;
            }
        }

        private void txtPath_LostFocus(object sender, RoutedEventArgs e)
        {
            popuptxtMobilePackagePath.IsOpen = false;
        }

        private void txtApplication_TextChanged(object sender, TextChangedEventArgs e)
        {
            popuptxtMobileApp.IsOpen = false;
            if (txtApplication.Text.Length == MAX_NAME_CHARS 
                && txtApplication.Visibility == Visibility.Visible
                && txtApplication.IsEnabled)
            {
                popuptxtMobileApp.IsOpen = true;
            }
        }

        private void txtApplication_LostFocus(object sender, RoutedEventArgs e)
        {
            popuptxtMobileApp.IsOpen = false;
        }

        private void btnResetSuiteDir_Click(object sender, RoutedEventArgs e)
        {
            string defDir = mProdConfigHandler.GetConfigValue("defaultsuitepath");
            if (DlkUserMessages.ShowQuestionYesNo("Are you sure you want to reset Test Suite directory to default?\n\n" + defDir) == MessageBoxResult.Yes)
            {
                SuiteDirectory = defDir;
                bIsSuiteDirExist = true;
            }
        }

        private void btnResetTestDir_Click(object sender, RoutedEventArgs e)
        {
            string defDir = mProdConfigHandler.GetConfigValue("defaulttestpath");
            if (DlkUserMessages.ShowQuestionYesNo("Are you sure you want to reset Test directory to default?\n\n" + defDir) == MessageBoxResult.Yes)
            {
                TestDirectory = defDir;
                bIsTestDirExist = true;
            }
        }
        private void lstEnvs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LockFields(lstEnvs.SelectedIndex != -1);
                ResetBindings();
                if (lstEnvs.SelectedIndex >= 0)
                {
                    ChangeEnvironmentInterfaceValue(((DlkLoginConfigRecord)(lstEnvs.SelectedItem)).mMetaData);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = false;
                grpURLBlackList.Visibility = Visibility.Hidden;
                tbURLBlacklistContainer.Visibility = Visibility.Hidden;
                LockFields(false);
                LockList(true, lstEnvs.SelectedIndex);
                mOrigEnvID = txtID.Text;
                if (txtUrl.Text == "")
                {
                    IsURLRecentlyBlank = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grpURLBlackList.Visibility = Visibility.Hidden;
                tbURLBlacklistContainer.Visibility = Visibility.Hidden;
                if (lstEnvs.SelectedIndex >= 0)
                {
                    ChangeEnvironmentInterfaceValue(Environments[lstEnvs.SelectedIndex].mMetaData);
                }
                else
                {
                    rdoDefault.IsChecked = true;
                }
                LockList(true, -1);
                ClearFields();
                LockFields(false);
                IsURLRecentlyBlank = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool contDelete = isEnvScheduled(txtID.Text)
                    ? DlkUserMessages.ShowQuestionYesNo(string.Format(DlkUserMessages.ASK_ENV_USED_IN_SCHEDULER, txtID.Text)) == MessageBoxResult.Yes
                    : DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_ENVIRONMENT + txtID.Text + "?") == MessageBoxResult.Yes;

                // show dialog
                if (contDelete)
                {
                    Environments.RemoveAt(lstEnvs.SelectedIndex);
                    lstEnvs.Items.Refresh();
                    lstEnvs.SelectedIndex = 0;
                    bIsDirty = true;
                    SaveSettingsList();
                }

                // To clear all the text fields after the last item in the list is deleted.
                if (lstEnvs.Items.Count == 0)
                {
                    ClearFields();
                    rdoDefault.IsChecked = true;
                }
                LockFields(true);
                LockList(false, lstEnvs.SelectedIndex);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool isEnvScheduled(string currEnv)
        {
            int foundCount = 0;
            var schedules = $@"{DlkEnvironment.mDirProductsRoot}Common\Scheduler\schedules.xml";

            if (File.Exists(schedules))
            {
                var schedDoc = XDocument.Load(schedules);
                var records = schedDoc.Descendants("record").ToList();
                if (records.Count > 0)
                {
                    foreach (var record in records)
                    {
                        var env = record.Attribute("environment")?.Value;
                        if (env == currEnv) foundCount++;
                    }
                }
            }
            return foundCount > 0;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isBlacklist = DlkEnvironment.URLBlacklist.Any(x => DlkEnvironment.IsSameURL(txtUrl.Text, x));
                if (isBlacklist)
                {
                    DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.ERR_URLBLACKLIST_SETTING, txtID.Text));
                    return;
                }

                if (String.IsNullOrWhiteSpace(txtID.Text))
                {
                    System.Windows.MessageBox.Show(DlkUserMessages.ERR_SETTINGS_NO_ENVIRONMENT_ID, "Environment Settings", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool bRecordExists = Environments.Any(x => x.mID.Equals(txtID.Text.Trim()));
                if (lstEnvs.SelectedIndex == -1)
                {
                    if(bRecordExists)
                    {
                        DlkUserMessages.ShowError("Environment ID '" + txtID.Text.Trim() + "' already exists. Please use a different ID.", "Environment Settings");
                        txtID.Focus();
                        txtID.SelectAll();
                        return;
                    }
                    else
                    {
                    Environments.Add(new DlkLoginConfigRecord(txtID.Text.Trim(), txtUrl.Text.Replace(" ", String.Empty), txtUser.Text, txtPassword.Password, txtDatabase.Text, txtPin.Text,
                        DlkEnvironment.mLoginConfigFile, (bool)rdoClassic.IsChecked ? "classic" : (bool)rdoNew.IsChecked ? "new" : string.Empty));
                    }
                    //lstEnvs.Items.Refresh();
                    lstEnvs.SelectedIndex = lstEnvs.Items.IndexOf(Environments.Where(record => record.mID.Equals(txtID.Text.Trim())).First()); // focus newly added item but don't scroll
                }
                else
                {
                    if (!bIsCloning)
                    {
                        if (Environments[lstEnvs.SelectedIndex].mID != txtID.Text.Trim() 
                            && isEnvScheduled(Environments[lstEnvs.SelectedIndex].mID) 
                            && DlkUserMessages.ShowQuestionYesNo(string.Format(DlkUserMessages.ASK_ENV_USED_IN_SCHEDULER, Environments[lstEnvs.SelectedIndex].mID)) == MessageBoxResult.No)
                            return;

                        if (!mOrigEnvID.Equals(txtID.Text.Trim()) && bRecordExists)
                        {
                            DlkUserMessages.ShowError("Environment ID '" + txtID.Text.Trim() + "' already exists. Please use a different ID.", "Environment Settings");
                            txtID.Focus();
                            txtID.SelectAll();
                            return;
                        }

                        Environments[lstEnvs.SelectedIndex].mID = txtID.Text.Trim();
                        Environments[lstEnvs.SelectedIndex].mUrl = txtUrl.Text.Replace(" ", String.Empty);
                        Environments[lstEnvs.SelectedIndex].mUser = txtUser.Text;
                        Environments[lstEnvs.SelectedIndex].mPassword = txtPassword.Password;
                        Environments[lstEnvs.SelectedIndex].mDatabase = txtDatabase.Text;
                        Environments[lstEnvs.SelectedIndex].mPin = txtPin.Text;
                        Environments[lstEnvs.SelectedIndex].mMetaData = (bool)rdoClassic.IsChecked ? "classic" : (bool)rdoNew.IsChecked ? "new" : string.Empty;
                    }
                    else
                    {
                        if (bRecordExists)
                        {
                            DlkUserMessages.ShowError("Environment ID '" + txtID.Text.Trim() + "' already exists. Please use a different ID.", "Environment Settings");
                            txtID.Focus();
                            txtID.SelectAll();
                            return;
                        }
                        else
                        {
                            Environments.Add(new DlkLoginConfigRecord(txtID.Text.Trim(), txtUrl.Text.Replace(" ", String.Empty), txtUser.Text, txtPassword.Password, txtDatabase.Text, txtPin.Text,
                                DlkEnvironment.mLoginConfigFile, (bool)rdoClassic.IsChecked ? "classic" : (bool)rdoNew.IsChecked ? "new" : string.Empty));
                        }
                        lstEnvs.SelectedIndex = lstEnvs.Items.IndexOf(Environments.Where(record => record.mID.Equals(txtID.Text.Trim())).First());
                    }
                }

                grpURLBlackList.Visibility = Visibility.Hidden;
                tbURLBlacklistContainer.Visibility = Visibility.Visible;
                tbURLBlackList.Text = "Show URL Blacklist";

                m_Records.Sort((item1, item2) => item1.mID.CompareTo(item2.mID)); // in-place sort
                LockFields(true);
                LockList(false, lstEnvs.SelectedIndex);
                lstEnvs.Items.Refresh();
                txtID.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtUrl.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                bIsDirty = true;
                bIsCloning = false;
                SaveSettingsList();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnNotCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = false;
                LockFields(true);
                lstEnvs.SelectedIndex = lstEnvs.Items.Count > 0 ? 0 : -1;
                LockList(false, lstEnvs.SelectedIndex);

                grpURLBlackList.Visibility = Visibility.Hidden;
                tbURLBlacklistContainer.Visibility = Visibility.Visible;
                tbURLBlackList.Text = "Show URL Blacklist";
                if (lstEnvs.SelectedIndex < 0)
                {
                    ClearFields();
                    rdoDefault.IsChecked = true;
                }
                else
                {
                    ResetBindings();
                    UpdateEnvironmentPropertyValue();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        
        /// <summary>
        /// Force environment settings property to update its source value
        /// </summary>
        private void UpdateEnvironmentPropertyValue()
        {
            txtID.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtUser.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtPassword.GetBindingExpression(MaskedPassword.BoundPassword).UpdateTarget();
            txtUrl.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtDatabase.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtPin.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            ChangeEnvironmentInterfaceValue(((DlkLoginConfigRecord)(lstEnvs.SelectedItem)).mMetaData);
        }

        /// <summary>
        /// Force mobile settings property to update its source value
        /// </summary>
        private void UpdateMobilePropertyValue()
        {
            txtMobileID.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtMobileBrowserUrl.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtDeviceName.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtApplication.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            chkEmulator.IsChecked = ((DlkMobileRecord)lstMobile.SelectedItem).IsEmulator;
            cboMobileType.GetBindingExpression(System.Windows.Controls.ComboBox.TextProperty).UpdateTarget();
            PopulateDeviceVersionDropdown();
            cboDeviceVersion.IsEnabled = false;
            cboDeviceVersion.GetBindingExpression(System.Windows.Controls.ComboBox.TextProperty).UpdateTarget();
            txtPath.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            if (((DlkMobileRecord)(lstMobile.SelectedItem)).Application.ToLower() == "safari"
                || ((DlkMobileRecord)(lstMobile.SelectedItem)).Application.ToLower() == "chrome")
            {
                optMobBrowser.IsChecked = true;
                cmbMobBrowser.GetBindingExpression(System.Windows.Controls.ComboBox.TextProperty).UpdateTarget();
            }
            else
            {
                optAppID.IsChecked = true;
            }
            txtPath.IsEnabled = false;
        }

        /// <summary>
        /// Force remote settings property to update its source value
        /// </summary>
        private void UpdateRemotePropertyValue()
        {
            txtRemoteBrowserID.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            txtRemoteBrowserUrl.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            cboRemoteBrowserType.Text = ((DlkRemoteBrowserRecord)(lstRemoteBrowsers.SelectedItem)).Browser;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
#region Validations Before Saving
                if ((chkUseCustomSender.IsChecked == true) && (!ValidateEmails(txtCustomSenderAdd.Text)))
                {
                    DlkUserMessages.ShowError("Custom Sender Address : Invalid email address.", "Error");
                    return;
                }
                if ((chkUseDefaultEmail.IsChecked == true) && (!ValidateEmails(txtMailAddresses.Text)))
                {
                    DlkUserMessages.ShowError("Default Email Recipients : Invalid email address.", "Error");
                    tpgScheduler.IsSelected = true;
                    return;
                }
                #endregion

                if (IsShowAppNameDirty)
                {
                    bool saveChanges = false;
                    Dictionary<string,IntPtr> activeWindows = DlkEditorWindowHandler.GetActiveTEWindows("testrunner", "scheduler");

                    if (activeWindows.Count > 0)
                    {
                        if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_CLOSE_TEST_EDITOR_WINDOW) == MessageBoxResult.Yes)
                        {
                            if (DlkEditorWindowHandler.TestEditorWindowClosed(activeWindows.Select(s => s.Value).ToList()))
                                saveChanges = true;
                        }
                        else
                        {
                            if (!IsTargetApplicationChanged())
                                return;
                        }
                    }
                    else
                        saveChanges = true;

                    //if (windows.Count() > 0 || DlkEditorWindowHandler.IsTestEditorWindowOpen)
                    //{
                    //    if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_CLOSE_TEST_EDITOR_WINDOW) == MessageBoxResult.Yes)
                    //    {
                    //        if (TestEditorWindowClosed(windows) && DlkEditorWindowHandler.TestEditorWindowClosed())
                    //            saveChanges = true;
                    //    }
                    //    else
                    //    {
                    //        if (!IsTargetApplicationChanged())
                    //            return;
                    //    }
                    //}
                    //else
                    //    saveChanges = true;

                    if (saveChanges)
                        DlkConfigHandler.UpdateConfigValue("showappname", chkShowApplicationName.IsChecked.ToString());
                }

                if (isURLBlacklistDirty)
                {
                    if (!UpdateURLBlacklist())
                    {
                        return;
                    }
                }

                if (bSettingsDirty)
                {
                    if (((MainWindow)this.Owner).IsFormOpen<Window>("Test Capture") & IsTargetApplicationChanged())
                    {
                        DlkUserMessages.ShowWarning(DlkUserMessages.WRN_TEST_CAPTURE_IS_OPEN);
                        return;
                    }

                    FileInfo fi = new FileInfo(DlkConfigHandler.MainConfig) { IsReadOnly = false };
                    string errKey = errLogDict.FirstOrDefault(x => x.Value == cmbErrLogLevel.SelectedValue.ToString()).Key;
                    DlkConfigHandler.UpdateConfigValue("errorloglevel", errKey); 
                }
                if (bSchedulerDirty)
                {
                    DlkConfigHandler.UpdateConfigValue("smtphost", txtSMTPServer.Text);
                    DlkConfigHandler.UpdateConfigValue("smtpport", txtSMTPPort.Text);
                    DlkConfigHandler.UpdateConfigValue("smtpuser", txtSMTPUser.Text);
                    DlkConfigHandler.UpdateConfigValue("smtppass", pbSMTPPass.Password);
                    DlkConfigHandler.UpdateConfigValue("usecustomsenderadd", chkUseCustomSender.IsChecked.ToString());
                    DlkConfigHandler.UpdateConfigValue("customsenderadd", txtCustomSenderAdd.Text);
                    DlkConfigHandler.UpdateConfigValue("usedefaultemail", chkUseDefaultEmail.IsChecked.ToString());
                    DlkConfigHandler.UpdateConfigValue("defaultemail", txtMailAddresses.Text);
                }

                //dashboard settings
                //if (bDashboardDirty)
                //{
                //    DlkConfigHandler.UpdateConfigValue("dbserver", txtServer.Text);
                //    DlkConfigHandler.UpdateConfigValue("dbname", txtResultDatabase.Text);
                //    DlkConfigHandler.UpdateConfigValue("dbuser", txtUserDatabase.Text);
                //    DlkConfigHandler.UpdateConfigValue("dbpassword", txtPasswordDatabase.Password);
                //    DlkConfigHandler.UpdateConfigValue("usedatabase", bUseDatabase.ToString());
                //    DlkConfigHandler.UpdateConfigValue("recordresults", chkSubmitResults.IsChecked.ToString());
                //}

                if (DlkConfigHandler.mDashboardEnabled != chkEnableDashboardRecording.IsChecked)
                {
                    DlkConfigHandler.UpdateConfigValue("dashboardenabled", chkEnableDashboardRecording.IsChecked.ToString());
                }

                if (IsTestDirectoryDirty)
                {
                    UpdateTestDirectory();
                }
                else
                {
                    bIsTestDirExist = true;
                }

                if (IsSuiteDirectoryDirty)
                {
                    UpdateSuiteDirectory();
                }
                else
                {
                    bIsSuiteDirExist = true;
                }

                SaveDefaultBrowser();

                if(IsTargetApplicationChanged())
                {
                    DlkGlobalVariables.mIsApplicationChanged = true;
                }

                DlkTestRunnerSettingsHandler.NeedsRefresh = IsTargetApplicationChanged() || DlkGlobalVariables.mIsTestDirChanged || DlkGlobalVariables.mIsSuiteDirChanged;
                if (DlkTestRunnerSettingsHandler.NeedsRefresh)
                {
                    DlkTestRunnerSettingsHandler.ApplicationUnderTest = (DlkTargetApplication)cmbTargetApplication.SelectedItem;
                    DlkTestRunnerSettingsHandler.Save();
                }
                bIsOkOrCancelClose = true;
                this.DialogResult = ((bIsDirty || bIsRemoteBrowsersDirty || bSettingsDirty || bIsMobileDirty || bSchedulerDirty || bDashboardDirty || bIsTagDirty) 
                    && (chkDoNotCheckInEnv.IsEnabled && !DoNotCheckInEnvironment) 
                    && (chkGetLatestOnLaunch.IsEnabled && !GetlatestOnLaunch));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(ex.Message);
            }
        }

        /// <summary>
        /// Closed opened Test Editor windows in TR
        /// </summary>
        /// <param name="windows">Opened TR test editor windows</param>
        /// <returns>true=if all TR test editor windows are closed;false=if not</returns>
        private bool TestEditorWindowClosed(List<Window> windows)
        {
            int closedCount = 0;
            foreach (Window win in windows)
            {
                win.Close();
                closedCount++;
            }
            return closedCount == windows.Count();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsTestDirExist = true;
                bIsSuiteDirExist = true;
                bIsOkOrCancelClose = true;
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lstBrowsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstBrowsers.SelectedIndex == -1)
                {
                    tbVersion.Text = "";
                }
                else
                {
                    DlkBrowser browser = ((DlkBrowser)lstBrowsers.SelectedItem);
                    tbVersion.Text = browser.Version;
                    tbDriver.Text = browser.DriverVersion;
                    chkDefaultBrowser.IsChecked = newDefaultBrowser == browser.Alias;
                    SetNotes(browser.Name, browser.DriverVersion);
                    OnPropertyChanged("IsBrowserSelected");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Reselects listbox item after keyboard navigation
        /// </summary>
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBoxItem listBoxItem = e.Source as ListBoxItem;

                if (listBoxItem != null)
                {
                    Keyboard.Focus(listBoxItem);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private string GetDriverVersion(string browser)
        {
            if (!File.Exists(DlkEnvironment.mVersionSupportListPath))
            {
                throw new Exception("Version support list does not exist.");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(DlkEnvironment.mVersionSupportListPath);
            XmlNode ver = doc.SelectSingleNode("versions/browser[@name = '" + browser + "']");
            return ver.Attributes["driver"].Value.ToString(); 
        }

        private List<String> GetSupportedVersions(string browser)
        {
            if (!File.Exists(DlkEnvironment.mVersionSupportListPath))
            {
                throw new Exception("Version support list does not exist.");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(DlkEnvironment.mVersionSupportListPath);
            XmlNode ver = doc.SelectSingleNode("versions/browser[@name = '" + browser + "']");
            if (ver.Attributes["supportedversions"] == null) { return null; }
            return ver.Attributes["supportedversions"].Value.ToString().Split(',').ToList();
        }

        /// <summary>
        /// Determines the Notes section of the Browsers tab
        /// </summary>
        private void SetNotes(String browserName, String driverVersion)
        {
            string msg = "";
            string defaultMsg = "Driver was changed. Cannot determine supported versions";
            string browser = browserName.ToLower();

            string DisplayMessage(string mBrowser)
            {
                string mDriver = GetDriverVersion(mBrowser);
                string retMsg = "Current installed driver doesn't match the recommended driver. Unable to determine supported versions. " +
                                 $"\n\nRecommended driver version: {mDriver}";
                if (mDriver.Equals(driverVersion.Trim()))
                {
                    if (GetSupportedVersions(mBrowser) == null)
                    {
                        return "Unable to determine supported versions.";
                    }
                    retMsg = "Supported versions: ";
                    foreach (string s in GetSupportedVersions(mBrowser))
                    {
                        retMsg += "\n- " + s;
                    }
                }
                if (mBrowser =="edge" && tbVersion.Text == "Unidentified")
                {
                    return "Legacy version of Edge is detected in your machine. This is not supported by Test Runner. It is recommended to upgrade to the new Edge browser.";
                }
                return retMsg;
            }

            switch (browser)
            {
                case string a when browser.Contains("chrome"):
                    msg = DisplayMessage("chrome");
                    break;
                case string b when browser.Contains("firefox"):
                    msg = DisplayMessage("firefox");
                    break;
                case string c when browser.Contains("internet explorer"):
                    msg = DisplayMessage("ie");
                    break;
                case string d when browser.Contains("edge"):
                    msg = DisplayMessage("edge");
                    break;
                default:
                    msg = defaultMsg;
                    break;
            }
            tbNotes.Text = msg;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (!ValidateEmails(txtMailAddresses.Text))
            //{
            //    DlkUserMessages.ShowError(DlkUserMessages.ERR_EMAIL_INVALID);
            //    //txtMailAddresses.Text = DlkSchedulerSettingsHandler.mEmails;
            //    tpgScheduler.IsSelected = true;
            //    e.Cancel = true;
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
            if (bIsOkOrCancelClose && (!bIsSuiteDirExist || !bIsTestDirExist))
            {
                e.Cancel = true;
            }
            bIsOkOrCancelClose = false;
        }


        //public bool IsDoNotCheckInEnvEnabled
        //{
        //    get
        //    {
        //        return (bool)chkEnableSourceControl.IsChecked;
        //    }
        //}

        //private void EnableIncludeEnvironment(bool Enabled)
        //{
        //    chkDoNotCheckInEnv.IsEnabled = Enabled;
        //}


        //private String SetErrorLevel(String errorLevel)
        //{
        //    if(errorLevel.ToLower().Contains("info")){
        //        return "errorinfo";
        //    }
        //    if(errorLevel.ToLower().Contains("only")){
        //        return "erroronly";
        //    }
        //    return "default";
        //}
        private void btnCleanUpDashboard_Click(object sender, RoutedEventArgs e)
        {
            DashboardCleanupWindow dw = new DashboardCleanupWindow();
            dw.Owner = this;
            dw.ShowDialog();
        }

        private void chkDefaultBrowser_Click(object sender, RoutedEventArgs e)
        {
            string selectedBrowser = ((DlkBrowser)lstBrowsers.SelectedItem).Alias;
            if (chkDefaultBrowser.IsChecked == true)
            {
                newDefaultBrowser = selectedBrowser;
            }
            else
            {
                newDefaultBrowser = "";
            }
        }
        #endregion

        #region METHODS

        private void ResetBindings()
        {
            rdoClassic.DataContext = null;
            rdoDefault.DataContext = null;
            rdoNew.DataContext = null;
            txtID.DataContext = lstEnvs.SelectedItem;
            txtUrl.DataContext = lstEnvs.SelectedItem;
            txtUser.DataContext = lstEnvs.SelectedItem;
            txtPassword.DataContext = lstEnvs.SelectedItem;
            txtDatabase.DataContext = lstEnvs.SelectedItem;
            txtPin.DataContext = lstEnvs.SelectedItem;
            rdoClassic.DataContext = lstEnvs.SelectedItem;
            rdoNew.DataContext = lstEnvs.SelectedItem;
            rdoDefault.DataContext = lstEnvs.SelectedItem;
            lstEnvs.Items.Refresh();
        }

        private void LockList(bool IsLocked, int SelectedIndex)
        {
            lstEnvs.SelectedIndex = SelectedIndex;
            lstEnvs.IsEnabled = !IsLocked;
            btnAdd.IsEnabled = !IsLocked;
            btnOk.IsEnabled = !IsLocked;
            btnCancel.IsEnabled = !IsLocked;
            if (IsLocked)
            {
                LockOtherTabPages();
            }
            else
            {
                UnlockAllTabPages();
            }

            if (SelectedIndex < 0)
            {
                btnEdit.IsEnabled = false;
                btnClone.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            else
            {
                btnEdit.IsEnabled = !IsLocked;
                btnClone.IsEnabled = !IsLocked;
                btnDelete.IsEnabled = !IsLocked;
            }
        }

        /// <summary>
        /// Saves default browser
        /// </summary>
        private void SaveDefaultBrowser()
        {
            try
            {
                bool updateAvailableBrowsers = true;
                if (newDefaultBrowser == "")
                {
                    DlkConfigHandler.UpdateConfigValue("defaultbrowser", "");
                }
                else if (defaultBrowser == "" || (defaultBrowser != newDefaultBrowser && DlkUserMessages.ShowQuestionYesNo(string.Format(DlkUserMessages.ASK_CHANGE_DEFAULT_BROWSER, newDefaultBrowser, defaultBrowser)) == MessageBoxResult.Yes))
                {
                    DlkConfigHandler.UpdateConfigValue("defaultbrowser", newDefaultBrowser);
                }
                else
                    updateAvailableBrowsers = false;

                if (updateAvailableBrowsers)
                {
                    foreach (var browser in DlkEnvironment.mAvailableBrowsers)
                    {
                        browser.DefaultBrowser = newDefaultBrowser == browser.Alias;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LockOtherTabPages()
        {
            foreach (object tab in tabEnvironments.Items)
            {
                if (tab is TabItem)
                {
                    ((TabItem)tab).IsEnabled = ((TabItem)tab).IsSelected;
                }

            }
        }

        private void UnlockAllTabPages()
        {
            foreach (object tab in tabEnvironments.Items)
            {
                if (tab is TabItem)
                {
                    ((TabItem)tab).IsEnabled = true;
                }

            }
        }

        private void LockFields(bool IsLocked)
        {
            txtID.IsEnabled = !IsLocked;
            txtUrl.IsEnabled = !IsLocked;
            IsURLValid = IsLocked ? true : ValidateURL();
            txtUser.IsEnabled= !IsLocked;
            txtPassword.IsEnabled= !IsLocked;
            txtDatabase.IsEnabled= !IsLocked;
            txtPin.IsEnabled = !IsLocked;
            tabMetaData.IsEnabled = !IsLocked;
            btnTestConnection.IsEnabled = lstEnvs.Items.Count == 0 ? false : IsLocked;
            btnCheck.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            btnNotCheck.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }

        private void ClearFields()
        {
            txtID.Clear();
            txtUrl.Clear();
            txtUser.Clear();
            txtPassword.Clear();
            txtDatabase.Clear();
            txtPin.Clear();
        }

        /// <summary>
        /// Returns whether or not the URL field's text is a valid URL
        /// </summary>
        private bool ValidateURL()
        {
            Uri uriResult;
            bool isURLValid = Uri.TryCreate(txtUrl.Text, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
            && Uri.IsWellFormedUriString(txtUrl.Text, UriKind.Absolute);
            return isURLValid;
        }

        /// <summary>
        /// Changes selected Interface value based on environment metadata
        /// </summary>
        private void ChangeEnvironmentInterfaceValue(string EnvMetaData)
        {
            switch (EnvMetaData)
            {
                case "classic":
                    rdoClassic.IsChecked = true;
                    break;
                case "new":
                    rdoNew.IsChecked = true;
                    break;
                default:
                    rdoDefault.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// Changes URL field error text depending on URL validity
        /// </summary>
        private void ChangeURLErrorText(string URL)
        {
            if (!URL.ToLower().StartsWith("http://") && !URL.ToLower().StartsWith("https://"))
            {
                InvalidURLMessage = DlkUserMessages.ERR_SETTINGS_INVALID_URL_SCHEME;
            }
            else if (!ValidateURL(URL))
            {
                InvalidURLMessage = DlkUserMessages.ERR_SETTINGS_INVALID_URL_FORMAT;
            }
        }

        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name">Name of property</param>
        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
#endregion

#region Remote Browsers management codes
        private bool bIsRemoteBrowsersDirty = false;

        public List<DlkRemoteBrowserRecord> RemoteBrowsers
        {
            get
            {
                return m_RemoteBrowsersRec;
            }
        }

        private List<DlkRemoteBrowserRecord> m_RemoteBrowsersRec = new List<DlkRemoteBrowserRecord>();

        private void lstRemoteBrowsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LockRemoteBrowsersFields(lstRemoteBrowsers.SelectedIndex != -1);
                ResetRemoteBrowsersBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddRemoteBrowser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LockRemoteBrowsersList(true, -1);
                ClearRemoteBrowsersFields();
                LockRemoteBrowsersFields(false);
                IsURLRecentlyBlank = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEditRemoteBrowser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = false;
                LockRemoteBrowsersFields(false);
                LockRemoteBrowsersList(true, lstRemoteBrowsers.SelectedIndex);
                mOrigRemoteID = txtRemoteBrowserID.Text;
                if (txtRemoteBrowserUrl.Text == "")
                {
                    IsURLRecentlyBlank = true;
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCloneRemoteBrowser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = true;
                LockRemoteBrowsersList(true, lstRemoteBrowsers.SelectedIndex);
                txtRemoteBrowserID.Clear();
                LockRemoteBrowsersFields(false);
                IsURLRecentlyBlank = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = true;
                grpURLBlackList.Visibility = Visibility.Hidden;
                tbURLBlacklistContainer.Visibility = Visibility.Hidden;
                LockList(true, lstEnvs.SelectedIndex);
                txtID.Clear();
                LockFields(false);
                IsURLRecentlyBlank = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCloneMobile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = true;
                LockMobileFields(false);
                txtMobileID.Clear();
                LockMobileList(true, lstMobile.SelectedIndex);
                IsURLRecentlyBlank = true;
               
                //reset application and path values if browser is selected.
                if ((bool)optMobBrowser.IsChecked)
                {
                    if (txtApplication.Text == cmbMobBrowser.SelectedItem.ToString())
                    {
                        txtApplication.Clear();
                    }
                    txtPath.Clear();
                    txtPath.IsEnabled = false;
                }

                SetAppBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteRemoteBrowser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_REMOTE_BROWSER + txtRemoteBrowserID.Text + "?") == MessageBoxResult.Yes)
                {
                    RemoteBrowsers.RemoveAt(lstRemoteBrowsers.SelectedIndex);
                    lstRemoteBrowsers.Items.Refresh();
                    lstRemoteBrowsers.SelectedIndex = 0;
                    bIsRemoteBrowsersDirty = true;
                    SaveSettingsList();
                }

                // To clear all the text fields after the last item in the list is deleted.
                if (lstRemoteBrowsers.Items.Count == 0)
                {
                    ClearRemoteBrowsersFields();
                    cboRemoteBrowserType.Text = "";
                }
                LockRemoteBrowsersFields(true);
                LockRemoteBrowsersList(false, lstRemoteBrowsers.SelectedIndex);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRemoteBrowserCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtRemoteBrowserID.Text))
                {
                    System.Windows.MessageBox.Show(DlkUserMessages.ERR_SETTINGS_NO_REMOTE_ID, "Remote Settings", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool bRecordExists = RemoteBrowsers.Any(x => x.Id.Equals(txtRemoteBrowserID.Text.Trim()));
                if (lstRemoteBrowsers.SelectedIndex == -1)
                {
                    if (bRecordExists)
                    {
                        DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_SETTINGS_DUPLICATE_REMOTE_ID, txtRemoteBrowserID.Text.Trim()), "Remote Settings");
                        txtRemoteBrowserID.Focus();
                        txtRemoteBrowserID.SelectAll();
                        return;
                    }
                    else
                    {
                        RemoteBrowsers.Add(new DlkRemoteBrowserRecord(txtRemoteBrowserID.Text.Trim(), txtRemoteBrowserUrl.Text.Replace(" ", String.Empty), cboRemoteBrowserType.Text));
                        //lstEnvs.Items.Refresh();
                    }
                    lstRemoteBrowsers.SelectedIndex = lstRemoteBrowsers.Items.IndexOf(RemoteBrowsers.Where(record => record.Id.Equals(txtRemoteBrowserID.Text.Trim())).First());
                }
                else
                {
                    if (!bIsCloning)
                    {
                        if (!mOrigRemoteID.Equals(txtRemoteBrowserID.Text.Trim()) && bRecordExists)
                        {
                            DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_SETTINGS_DUPLICATE_REMOTE_ID, txtRemoteBrowserID.Text.Trim()), "Remote Settings");
                            txtRemoteBrowserID.Focus();
                            txtRemoteBrowserID.SelectAll();
                            return;
                        }

                        RemoteBrowsers[lstRemoteBrowsers.SelectedIndex].Id = txtRemoteBrowserID.Text.Trim();
                        RemoteBrowsers[lstRemoteBrowsers.SelectedIndex].Url = txtRemoteBrowserUrl.Text.Replace(" ", String.Empty);
                        RemoteBrowsers[lstRemoteBrowsers.SelectedIndex].Browser = cboRemoteBrowserType.Text;
                    }
                    else
                    {
                        if (bRecordExists)
                        {
                            DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_SETTINGS_DUPLICATE_REMOTE_ID, txtRemoteBrowserID.Text.Trim()), "Remote Settings");
                            txtRemoteBrowserID.Focus();
                            txtRemoteBrowserID.SelectAll();
                            return;
                        }
                        else
                        {
                            RemoteBrowsers.Add(new DlkRemoteBrowserRecord(txtRemoteBrowserID.Text.Trim(), txtRemoteBrowserUrl.Text.Replace(" ", String.Empty), cboRemoteBrowserType.Text));
                        }
                        lstRemoteBrowsers.SelectedIndex = lstRemoteBrowsers.Items.IndexOf(RemoteBrowsers.Where(record => record.Id.Equals(txtRemoteBrowserID.Text.Trim())).First());
                    }
                }
                RemoteBrowsers.Sort((item1, item2) => item1.Id.CompareTo(item2.Id));
                LockRemoteBrowsersFields(true);
                LockRemoteBrowsersList(false, lstRemoteBrowsers.SelectedIndex);
                lstRemoteBrowsers.Items.Refresh();
                txtRemoteBrowserID.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtRemoteBrowserUrl.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                bIsRemoteBrowsersDirty = true;
                bIsCloning = false;
                SaveSettingsList();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRemoteBrowserNotCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = false;
                LockRemoteBrowsersFields(true);
                lstRemoteBrowsers.SelectedIndex = lstRemoteBrowsers.Items.Count > 0 ? 0 : -1;
                LockRemoteBrowsersList(false, lstRemoteBrowsers.SelectedIndex);
                if (lstRemoteBrowsers.SelectedIndex < 0)
                {
                    ClearRemoteBrowsersFields();
                    cboRemoteBrowserType.Text = "";
                }
                else
                {
                    UpdateRemotePropertyValue();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ClearRemoteBrowsersFields()
        {
            txtRemoteBrowserID.Clear();
            txtRemoteBrowserUrl.Clear();
            cboRemoteBrowserType.Text = "Firefox";
            //txtDeviceName.Clear();
            //txtDeviceVersion.Clear();
            //txtApplication.Text = "";
        }

        private void ResetRemoteBrowsersBindings()
        {
            txtRemoteBrowserID.DataContext = lstRemoteBrowsers.SelectedItem;
            txtRemoteBrowserUrl.DataContext = lstRemoteBrowsers.SelectedItem;
            cboRemoteBrowserType.DataContext = lstRemoteBrowsers.SelectedItem;       
            //txtDeviceName.DataContext = lstRemoteBrowsers.SelectedItem;
            //txtDeviceVersion.DataContext = lstRemoteBrowsers.SelectedItem;
            //txtApplication.DataContext = lstRemoteBrowsers.SelectedItem;
            lstRemoteBrowsers.Items.Refresh();
        }

        private void LockRemoteBrowsersList(bool IsLocked, int SelectedIndex)
        {
            lstRemoteBrowsers.SelectedIndex = SelectedIndex;
            lstRemoteBrowsers.IsEnabled = !IsLocked;
            btnAddRemoteBrowser.IsEnabled = !IsLocked;
            btnOk.IsEnabled = !IsLocked;
            btnCancel.IsEnabled = !IsLocked;
            if (IsLocked)
            {
                LockOtherTabPages();
            }
            else
            {
                UnlockAllTabPages();
            }

            if (SelectedIndex < 0)
            {
                btnEditRemoteBrowser.IsEnabled = false;
                btnCloneRemoteBrowser.IsEnabled = false;
                btnDeleteRemoteBrowser.IsEnabled = false;
            }
            else
            {
                btnEditRemoteBrowser.IsEnabled = !IsLocked;
                btnCloneRemoteBrowser.IsEnabled = !IsLocked;
                btnDeleteRemoteBrowser.IsEnabled = !IsLocked;
            }
        }
        private void LockRemoteBrowsersFields(bool IsLocked)
        {
            txtRemoteBrowserID.IsEnabled = !IsLocked;
            txtRemoteBrowserUrl.IsEnabled = !IsLocked;
            cboRemoteBrowserType.IsEnabled = !IsLocked;
            IsURLValid = IsLocked ? true : ValidateURL(txtRemoteBrowserUrl.Text);
            //txtDeviceName.IsEnabled = !IsLocked;
            //txtDeviceVersion.IsEnabled = !IsLocked;
            //txtApplication.IsEnabled = !IsLocked;
            btnRemoteBrowserCheck.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            btnRemoteBrowserNotCheck.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }


        #endregion

        #region Mobile management code

        private bool bIsMobileDirty = false;

        public List<DlkMobileRecord> MobileDevices
        {
            get
            {
                return m_MobileRec;
            }
        }

        private List<DlkMobileRecord> m_MobileRec = new List<DlkMobileRecord>();

        private void lstMobile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LockMobileFields(lstMobile.SelectedIndex != -1);
                ResetMobileBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            
        }

        private void btnAddMobile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LockMobileList(true, -1);
                ClearMobileFields();
                LockMobileFields(false, true);
                IsURLRecentlyBlank = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEditMobile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = false;
                LockMobileFields(false);
                LockMobileList(true, lstMobile.SelectedIndex);
                mOrigMobileID = txtMobileID.Text;
                if (txtMobileBrowserUrl.Text == "")
                {
                    IsURLRecentlyBlank = true;
                }

                //reset application value and disable path if browser is selected.
                if ((bool)optMobBrowser.IsChecked)
                {
                    if (txtApplication.Text == cmbMobBrowser.SelectedItem.ToString())
                    {
                        txtApplication.Clear();
                    }
                    txtPath.Clear();
                    txtPath.IsEnabled = false;
                }
                SetAppBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteMobile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_MOBILE_DEVICE + txtMobileID.Text + "?") == MessageBoxResult.Yes)
                {
                    MobileDevices.RemoveAt(lstMobile.SelectedIndex);
                    lstMobile.Items.Refresh();
                    lstMobile.SelectedIndex = 0;
                    bIsMobileDirty = true;
                    SaveSettingsList();
                }
                // To clear all the text fields after the last item in the list is deleted.
                if (lstMobile.Items.Count == 0)
                {
                    ClearMobileFields();
                }
                LockMobileFields(true);
                LockMobileList(false, lstMobile.SelectedIndex);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMobileCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMobileID.Text))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_SETTINGS_NO_DEVICE_ID, "Mobile Settings");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtDeviceName.Text))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_SETTINGS_NO_DEVICE_NAME, "Mobile Settings");
                    return;
                }
                else if (cboMobileType.SelectedValue == null)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_SETTINGS_NO_MOBILE_TYPE, "Mobile Settings");
                    return;
                }
                else if (cboDeviceVersion.SelectedValue == null)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_SETTINGS_NO_DEVICE_VERSION, "Mobile Settings");
                    return;
                }
                else if (cmbMobBrowser.IsVisible && cmbMobBrowser.SelectedValue == null)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_SETTINGS_NO_MOBILE_BROWSER, "Mobile Settings");
                    return;
                }

                bool bRecordExists = MobileDevices.Any(x => x.MobileId.Equals(txtMobileID.Text.Trim()));
                if (lstMobile.SelectedIndex == -1)
                {
                    if (bRecordExists)
                    {
                        DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_SETTINGS_DUPLICATE_DEVICE_ID, txtMobileID.Text.Trim()), "Mobile Settings");
                        txtMobileID.Focus();
                        txtMobileID.SelectAll();
                        return;
                    }
                    else
                    {
                        MobileDevices.Add(new DlkMobileRecord(txtMobileID.Text.Trim(), txtMobileBrowserUrl.Text.Replace(" ", String.Empty), cboMobileType.Text, txtDeviceName.Text.Trim(),
                            cboDeviceVersion.Text, (bool)optAppID.IsChecked ? txtApplication.Text.Trim() : cmbMobBrowser.Text, txtPath.Text.Trim(), DlkEnvironment.DefaultAppiumCommandTimeout, (bool)chkEmulator.IsChecked));
                        //lstEnvs.Items.Refresh();
                    }
                    // lstMobile.SelectedIndex = MobileDevices.Count - 1;
                    lstMobile.SelectedIndex = lstMobile.Items.IndexOf(MobileDevices.Where(record => record.MobileId.Equals(txtMobileID.Text.Trim())).First());
                }
                else
                {
                    if (!bIsCloning)
                    {
                        if (!mOrigMobileID.Equals(txtMobileID.Text.Trim()) && bRecordExists)
                        {
                            DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_SETTINGS_DUPLICATE_DEVICE_ID, txtMobileID.Text.Trim()), "Mobile Settings");
                            txtMobileID.Focus();
                            txtMobileID.SelectAll();
                            return;
                        }

                        if ((bool)optMobBrowser.IsChecked || ((bool)optAppID.IsChecked && cboMobileType.Text == "iOS"))
                        {
                            txtPath.Clear();
                        }

                        MobileDevices[lstMobile.SelectedIndex].MobileId = txtMobileID.Text.Trim();
                        MobileDevices[lstMobile.SelectedIndex].MobileUrl = txtMobileBrowserUrl.Text.Replace(" ", String.Empty);
                        MobileDevices[lstMobile.SelectedIndex].MobileType = cboMobileType.Text;
                        MobileDevices[lstMobile.SelectedIndex].DeviceName = txtDeviceName.Text.Trim();
                        MobileDevices[lstMobile.SelectedIndex].IsEmulator = (bool)chkEmulator.IsChecked;
                        MobileDevices[lstMobile.SelectedIndex].DeviceVersion = cboDeviceVersion.Text;
                        MobileDevices[lstMobile.SelectedIndex].Application = (bool)optAppID.IsChecked ? txtApplication.Text.Trim() : cmbMobBrowser.Text;
                        MobileDevices[lstMobile.SelectedIndex].Path = txtPath.Text.Trim();
                    }
                    else
                    {
                        if (bRecordExists)
                        {
                            DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_SETTINGS_DUPLICATE_DEVICE_ID, txtMobileID.Text.Trim()), "Mobile Settings");
                            txtMobileID.Focus();
                            txtMobileID.SelectAll();
                            return;
                        }
                        else
                        {
                            if((bool)optMobBrowser.IsChecked)
                            {
                                txtPath.Clear();
                            }

                            MobileDevices.Add(new DlkMobileRecord(txtMobileID.Text.Trim(), txtMobileBrowserUrl.Text.Replace(" ", String.Empty), cboMobileType.Text, txtDeviceName.Text.Trim(),
                                cboDeviceVersion.Text, (bool)optAppID.IsChecked ? txtApplication.Text.Trim() : cmbMobBrowser.Text, txtPath.Text.Trim(), DlkEnvironment.DefaultAppiumCommandTimeout, (bool)chkEmulator.IsChecked));
                        }
                        lstMobile.SelectedIndex = lstMobile.Items.IndexOf(MobileDevices.Where(record => record.MobileId.Equals(txtMobileID.Text.Trim())).First());
                    }
                }
                MobileDevices.Sort((item1, item2) => item1.MobileId.CompareTo(item2.MobileId));
                LockMobileFields(true);
                LockMobileList(false, lstMobile.SelectedIndex);
                lstMobile.Items.Refresh();
                txtMobileID.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtMobileBrowserUrl.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtDeviceName.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtApplication.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtPath.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                bIsMobileDirty = true;
                bIsCloning = false;
                SaveSettingsList();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMobileNotCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsCloning = false;
                LockMobileFields(true);
                lstMobile.SelectedIndex = lstMobile.Items.Count > 0 ? 0 : -1;
                LockMobileList(false, lstMobile.SelectedIndex);
                if (lstMobile.SelectedIndex < 0)
                {
                    ClearMobileFields();
                    optAppID.IsChecked = true;
                }
                else
                {
                    UpdateMobilePropertyValue();
                    SetAppBindings();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboMobileType_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //resets respective version bindings when mobile type is changed after selecting from dropdown
                PopulateDeviceVersionDropdown();
                SetMobileBrowserBindings();
                SetAppBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboMobileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboMobileType.IsEnabled)
                {
                    PopulateDeviceVersionDropdown();//reset device version dropdown
                    SetMobileBrowserBindings();
                    SetAppBindings();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void cboDeviceVersion_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                optAppID.IsEnabled = cboDeviceVersion.SelectedItem != null;
                optMobBrowser.IsEnabled = cboDeviceVersion.SelectedItem != null;
                SetAppBindings();
                SetMobileBrowserBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboDeviceVersion_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                optAppID.IsEnabled = cboDeviceVersion.SelectedItem != null && cboMobileType.SelectedItem != null;
                optMobBrowser.IsEnabled = cboDeviceVersion.SelectedItem != null && cboMobileType.SelectedItem != null;
                SetAppBindings();
                SetMobileBrowserBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboDeviceVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bool isEnabled = (cboDeviceVersion.SelectedItem != null && cboMobileType.SelectedItem != null) && txtDeviceName.IsEnabled;
                optAppID.IsEnabled = isEnabled;
                optMobBrowser.IsEnabled = isEnabled;
                SetAppBindings();
                SetMobileBrowserBindings();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void optAppID_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtApplication.Visibility = System.Windows.Visibility.Visible;
                cmbMobBrowser.Visibility = System.Windows.Visibility.Collapsed;
                optMobBrowser.IsChecked = false;

                SetAppBindings();
                SetMobileBrowserBindings();
                if(txtApplication.IsVisible && txtApplication.IsEnabled)
                {
                    txtApplication.Clear();

                    if(txtPath.IsEnabled && txtPath.IsVisible)
                    {
                        txtPath.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void optMobBrowser_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtApplication.Visibility = System.Windows.Visibility.Collapsed;
                cmbMobBrowser.Visibility = System.Windows.Visibility.Visible;
                optAppID.IsChecked = false;
                SetMobileBrowserBindings();
                SetAppBindings();

                if(cmbMobBrowser.IsVisible && cmbMobBrowser.IsEnabled)
                {
                    cmbMobBrowser.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkEmulator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)chkEmulator.IsChecked)
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_EMULATOR_ANDROID_ONLY, "Warning");
                    cboMobileType.SelectedValue = "Android";
                    cboMobileType.IsEnabled = false;
                    cboDeviceVersion.SelectedValue = "";
                    cboDeviceVersion.IsEnabled = true;
                    PopulateDeviceVersionDropdown();
                }
                else
                {
                    cboMobileType.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ClearMobileFields()
        {
            txtMobileID.Clear();
            txtMobileBrowserUrl.Clear();
            txtDeviceName.Clear();
            chkEmulator.IsChecked = false;
            cboMobileType.SelectedIndex = -1;
            cboDeviceVersion.SelectedIndex = -1;
            txtApplication.Clear();
            txtPath.Clear();
        }

        private void ResetMobileBindings()
        {
            txtMobileID.DataContext = lstMobile.SelectedItem;
            txtMobileBrowserUrl.DataContext = lstMobile.SelectedItem;
            txtDeviceName.DataContext = lstMobile.SelectedItem;
            chkEmulator.DataContext = lstMobile.SelectedItem;

            if ((bool)chkEmulator.IsChecked)
            {
                cboMobileType.IsEnabled = false;
            }

            verList = DlkMobileOSRecord.LoadVersionList();

            versionList.Clear();
            versionList = verList.Select(x => x.Target).ToList();
            
            cboMobileType.DataContext = lstMobile.SelectedItem;
            cboDeviceVersion.ItemsSource = versionList;
            cboDeviceVersion.DataContext = lstMobile.SelectedItem;
            txtApplication.DataContext = lstMobile.SelectedItem;
            txtPath.DataContext = lstMobile.SelectedItem;
            if (cboMobileType.Text != null && cboMobileType.Text != string.Empty)
            {
                cmbMobBrowser.ItemsSource = cboMobileType.Text == "Android" ? new string[] { "Chrome" } : new string[] { "Safari" };
            }
            cmbMobBrowser.DataContext = lstMobile.SelectedItem;

            if (lstMobile.SelectedItem != null)
            {
                if (((DlkMobileRecord)(lstMobile.SelectedItem)).Application.ToLower() == "safari"
                    || ((DlkMobileRecord)(lstMobile.SelectedItem)).Application.ToLower() == "chrome")
                {
                    optMobBrowser.IsChecked = true;
                }
                else
                {
                    optAppID.IsChecked = true;
                }
            }
            lstMobile.Items.Refresh();
        }

        private void LockMobileList(bool IsLocked, int SelectedIndex)
        {
            lstMobile.SelectedIndex = SelectedIndex;
            lstMobile.IsEnabled = !IsLocked;
            btnAddMobile.IsEnabled = !IsLocked;
            btnOk.IsEnabled = !IsLocked;
            btnCancel.IsEnabled = !IsLocked;
            if (IsLocked)
            {
                LockOtherTabPages();
            }
            else
            {
                UnlockAllTabPages();
            }
            if (SelectedIndex < 0)
            {
                btnEditMobile.IsEnabled = false;
                btnCloneMobile.IsEnabled = false;
                btnDeleteMobile.IsEnabled = false;
            }
            else
            {
                btnEditMobile.IsEnabled = !IsLocked;
                btnCloneMobile.IsEnabled = !IsLocked;
                btnDeleteMobile.IsEnabled = !IsLocked;
            }
        }
        private void LockMobileFields(bool IsLocked, bool isAddOrInit = false)
        {
            txtMobileID.IsEnabled = !IsLocked;
            txtMobileBrowserUrl.IsEnabled = !IsLocked;
            IsURLValid = IsLocked ? true : ValidateURL(txtMobileBrowserUrl.Text);
            txtDeviceName.IsEnabled = !IsLocked;
            cboMobileType.IsEnabled = !IsLocked && !(bool)chkEmulator.IsChecked;
            if (isAddOrInit)
            {
                cboDeviceVersion.IsEnabled = cboMobileType.SelectedIndex < 0 ? false : true;
            }
            else
            {
                cboDeviceVersion.IsEnabled = !IsLocked;
            }
            chkEmulator.IsEnabled = !IsLocked;
            txtApplication.IsEnabled = !IsLocked;
            txtPath.IsEnabled = !IsLocked;
            btnMobileCheck.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            btnMobileNotCheck.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            optAppID.IsEnabled = !IsLocked && !isAddOrInit;
            optMobBrowser.IsEnabled = !IsLocked && !isAddOrInit;
            cmbMobBrowser.IsEnabled = !IsLocked && !isAddOrInit;
            SetAppBindings();
        }

        private void PopulateDeviceVersionDropdown()
        {
            versionList.Clear();

            if (cboMobileType.SelectedItem != null && cboMobileType.SelectedItem.ToString() == "Android")
            {
                cboDeviceVersion.IsEnabled = true;
                versionList = verList.FindAll(x => x.Type == "Android").Select(x => x.Target).ToList();
            }
            else if (cboMobileType.SelectedItem != null && cboMobileType.SelectedItem.ToString() == "iOS")
            {
                cboDeviceVersion.IsEnabled = true;
                cmbMobBrowser.ItemsSource = new string[] { "Safari" };
                versionList = verList.FindAll(x => x.Type == "iOS").Select(x => x.Target).ToList();
            }
            else if (cboMobileType.SelectedIndex < 0)
            {
                cboDeviceVersion.IsEnabled = false;
            }
            else
            {
                versionList = verList.Select(x => x.Target).ToList();
            }

            cboDeviceVersion.ItemsSource = versionList;
        }

        /// <summary>
        /// Sets the values for mobile browser combobox IsEnabled property and ItemsSource
        /// The values will depend if the mobile browser radio button and a device version has been selected.
        /// </summary>
        private void SetMobileBrowserBindings()
        {
            if (cboDeviceVersion.SelectedItem != null && (bool)optMobBrowser.IsChecked)
            {
                cmbMobBrowser.IsEnabled = optMobBrowser.IsEnabled;
                cmbMobBrowser.ItemsSource = cboMobileType.Text == "Android" ? new string[] { "Chrome" } : new string[] { "Safari" };
            }
            else
            {
                cmbMobBrowser.IsEnabled = false;
            }

            //reset selected mobile browser if there is no device version selected
            if (cboDeviceVersion.SelectedItem == null && (bool)optMobBrowser.IsChecked)
            {
                cmbMobBrowser.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sets the values for App and Package Path IsEnabled and IsVisible property.
        /// Values will depend on the device type selected and if the App radio button and device version has been selected.
        /// </summary>
        private void SetAppBindings()
        {
            //Set path visibility
            bool isAndroid = cboMobileType.SelectedItem != null && cboMobileType.SelectedItem.ToString() == "Android";
            if (cboDeviceVersion.SelectedItem != null)
            {
                txtPath.Visibility = isAndroid && ((bool)optAppID.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
                lblPackagePath.Visibility = isAndroid && ((bool)optAppID.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                txtPath.Visibility = isAndroid && ((bool)optAppID.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
                lblPackagePath.Visibility = isAndroid && ((bool)optAppID.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
            }
            //reset app and path values if there is no device version selected
            if (cboDeviceVersion.SelectedItem == null && (bool)optAppID.IsChecked)
            {
                txtPath.Clear();
                txtApplication.Clear();
            }
            //Enable app and path depending on the mobile type and if a version has been selected
            txtApplication.IsEnabled = (cboMobileType.IsEnabled || (bool)chkEmulator.IsChecked) && !string.IsNullOrEmpty(cboMobileType.Text) &&
                                       cboDeviceVersion.IsEnabled && (cboDeviceVersion.SelectedItem != null && !string.IsNullOrEmpty(cboDeviceVersion.SelectedItem.ToString()));
            txtPath.IsEnabled = txtPath.IsVisible &&
                                (cboMobileType.IsEnabled || (bool)chkEmulator.IsChecked) && !string.IsNullOrEmpty(cboMobileType.Text) && 
                                cboDeviceVersion.IsEnabled && (cboDeviceVersion.SelectedItem != null && !string.IsNullOrEmpty(cboDeviceVersion.SelectedItem.ToString()));
        }

        #endregion

        #region Scheduler Settings management code
        private void txtSMTPServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void txtSMTPPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
        private void txtSMTPUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void pbSMTPPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void chkUseCustomSender_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
            mUseCustomEmailAddress = true;
            txtCustomSenderAdd.IsEnabled = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkUseCustomSender_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
            mUseCustomEmailAddress = false;
            txtCustomSenderAdd.IsEnabled = false;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtCustomSenderAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkUseDefaultEmail_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            mUseDefaultEmail = true;
            txtMailAddresses.IsEnabled = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkUseDefaultEmail_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
            mUseDefaultEmail = false;
            txtMailAddresses.IsEnabled = false;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEmailUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (ValidateEmails(txtMailAddresses.Text))
            {
                bSchedulerDirty = true;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtMailAddresses_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            bSchedulerDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        //private void txtMailAddresses_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    bSchedulerDirty = true;
        //}


        private static bool IsEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private bool ValidateEmails(string input)
        {
            String eMails = input;
            string[] eMailAdds = eMails.Split(';');

            if(!eMailAdds.Any(s => String.IsNullOrWhiteSpace(s)))
            {
                foreach (string m in eMailAdds)
                {
                    if (IsEmail(m) == true)
                        continue;
                    else
                        return false;
                }
                return true;
            }
            else
                return false;
         }

      
#endregion

#region SourceControl management code

        private void chkEnableSourceControl_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            DlkSourceControlHandler.SourceControlEnabled = true;
            chkDoNotCheckInEnv.IsEnabled = true;
            chkGetLatestOnLaunch.IsEnabled = true;
            //EnableIncludeEnvironment(true);
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkEnableSourceControl_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
            DlkSourceControlHandler.SourceControlEnabled = false;
            chkDoNotCheckInEnv.IsEnabled = false;
            chkGetLatestOnLaunch.IsEnabled = false;
            //EnableIncludeEnvironment(false);
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion

#region Target application management code
        private void cmbTargetApplication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Show app name feature available only for specific products: [CP, BnP, TE]
                if (IsTargetAppIncludedShowAppName)
                    grpBoxObjectStore.Visibility = Visibility.Visible;
                else
                    grpBoxObjectStore.Visibility = Visibility.Collapsed;

                bSettingsDirty = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
#endregion

        #region ErrorLogLevel management code
        private void cmbErrLogLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            bSettingsDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
#endregion

#region Checkin Management code

        private void chkGetLatestOnLaunch_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            GetlatestOnLaunch = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkGetLatestOnLaunch_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
            GetlatestOnLaunch = false;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkDoNotCheckInEnv_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            DoNotCheckInEnvironment = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkDoNotCheckInEnv_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
            DoNotCheckInEnvironment = false;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

#endregion

#region Dashboard Results Management code

        private void optDatabase_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            //bUseDatabase = true;
            chkSubmitResults.IsChecked = true;
            grpDatabase.IsEnabled = true;
            bDashboardDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void optLocal_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            //bUseDatabase = false;
            chkSubmitResults.IsChecked = false;
            grpDatabase.IsEnabled = false;
            bDashboardDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDbTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            DlkResultsDatabaseConfigRecord mDbConfig = new DlkResultsDatabaseConfigRecord(txtServer.Text, txtResultDatabase.Text, txtUserDatabase.Text, txtPasswordDatabase.Password);
            Boolean bResult = DlkDatabaseAPI.VerifyDatabaseConnection(mDbConfig);
            if (bResult)
            {
                DlkUserMessages.ShowInfo(DlkUserMessages.INF_DBCONN_SUCCESS);
            }
            else
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_DBCONN_FAIL);
            }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void DashboardInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            bDashboardDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkSubmitResults_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            bDashboardDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtPasswordDatabase_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
            bDashboardDirty = true;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

#endregion

#region Tag Management code
        /// <summary>
        /// Current UI state of Tab page
        /// </summary>
        public TagPageMode CurrentTagPageMode
        {
            set
            {
                mTagPageMode = value;
                if (value == TagPageMode.Add)
                {
                    lstTags.SelectedItem = null;
                    SelectedTag = new DlkTag(string.Empty, string.Empty);
                }
                LockTagFields(value == TagPageMode.Default);
            }
        }

        /// <summary>
        /// All Tags list
        /// </summary>
        public ObservableCollection<DlkTag> AllTags { get; set; }

        /// <summary>
        /// Currently selected Tag
        /// </summary>
        public DlkTag SelectedTag
        {
            get
            {
                return mSelectedTag;
            }
            set
            {
                mSelectedTag = value;
                OnPropertyChanged("SelectedTag");
                OnPropertyChanged("TagConflicts");
            }
        }

        /// <summary>
        /// Datacontext of currently displayed Tag Conflicts
        /// </summary>
        public DlkTag[] TagConflicts
        {
            get
            {
                if (mSelectedTag != null)
                {
                    return AllTags.ToList().FindAll(x => mSelectedTag.ConflictsIDList.Contains(x.Id)).ToArray();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Initialize Tags page
        /// </summary>
        private void InitializeTags()
        {
            AllTags = new ObservableCollection<DlkTag>(DlkTag.LoadAllTags());
            CurrentTagPageMode = TagPageMode.Default;
        }

        
        /// <summary>
        /// Enable/Disable UI fields in Tab Page
        /// </summary>
        /// <param name="IsLocked">TRUE if lock fields; FALSE for unlock</param>
        private void LockTagFields(bool IsLocked)
        {
            btnOk.IsEnabled = IsLocked;
            btnCancel.IsEnabled = IsLocked;
            lstTags.IsEnabled = IsLocked;
            pnlAddEditDelTag.IsEnabled = IsLocked;
            txtTagName.IsEnabled = !IsLocked;
            txtTagDescription.IsEnabled = !IsLocked;
            btnManageConflicts.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            btnCheckTag.Visibility = IsLocked ?  System.Windows.Visibility.Hidden: System.Windows.Visibility.Visible;
            btnNotCheckTag.Visibility = IsLocked ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            if (IsLocked)
            {
                UnlockAllTabPages();
            }
            else
            {
                LockOtherTabPages();
            }
        }

        /// <summary>
        /// Add new tag
        /// </summary>
        private void AddNewTag()
        {
            SelectedTag.Name = txtTagName.Text.Trim();
            SelectedTag.Description = txtTagDescription.Text.Trim();
            AllTags.Insert(GetTagInsertIndex(SelectedTag.Name), SelectedTag);
        }

        /// <summary>
        /// Edit tag
        /// </summary>
        private void EditTag()
        {
            SelectedTag.Name = txtTagName.Text.Trim();
            SelectedTag.Description = txtTagDescription.Text.Trim();
            CollectionViewSource.GetDefaultView(AllTags).Refresh(); /* Force update of observablecollection */
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        private void DeleteTag()
        {
            /* Delete confirmation */
            if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_DELETE_TAG_APPEND_TAGNAME 
                + SelectedTag.Name + "'?") == MessageBoxResult.Yes)
            {
                foreach (DlkTag tag in AllTags) /* Update affected tag conflicts */
                {
                    if (tag.ConflictsIDList.Any(x => x == SelectedTag.Id))
                    {
                        tag.RemoveTagConflict(SelectedTag.Id);
                    }
                }
                AllTags.Remove(SelectedTag);
                bIsTagDirty = true;
            }
        }

        /// <summary>
        /// Cache or Clear cached values of tag
        /// </summary>
        /// <param name="IsClear">TRUE if clear; FALSE if cache</param>
        private void CacheOrClearOldValuesofSelectedTag(bool IsClear)
        {
            txtTagName.Tag = IsClear ? null : txtTagName.Text.Trim();
            txtTagDescription.Tag = IsClear ? null : txtTagDescription.Text.Trim();
            lstTagConflicts.Tag = IsClear ? null : lstTagConflicts.ItemsSource;
        }

        /// <summary>
        /// Revert to cached value of old tag
        /// </summary>
        private void RevertCachedValuesofSelectedTag()
        {
            SelectedTag.Name = txtTagName.Tag == null ? string.Empty : txtTagName.Tag.ToString().Trim();
            SelectedTag.Description = txtTagDescription.Tag == null ? string.Empty : txtTagDescription.Tag.ToString().Trim();
            SelectedTag.ReplaceTagConflicts(lstTagConflicts.Tag == null ? null : ((DlkTag[])lstTagConflicts.Tag).Select(x => x.Id).ToArray());
            OnPropertyChanged("SelectedTag");
            OnPropertyChanged("TagConflicts");
        }

        /// <summary>
        /// Check if Tag name is valid
        /// </summary>
        /// <param name="OldTagName">Old Tag name</param>
        /// <param name="NewTagName">New Tag name</param>
        /// <returns>TRUE if valid; FALSE otherwise</returns>
        private bool IsTagNameValid(string OldTagName, string NewTagName)
        {
            return !string.IsNullOrEmpty(NewTagName) && !AllTags.ToList().FindAll(x => x.Name != OldTagName).Any(y => y.Name == NewTagName);
        }

        /// <summary>
        /// Check if Tag name's characters are all white spaces
        /// </summary>
        private bool IsTagNameAllSpaces(string TagName)
        {
            char[] tagArray = TagName.ToCharArray();
            return (tagArray.All(x => Char.IsWhiteSpace(x)));
        }

        /// <summary>
        /// Get index where to insert tag
        /// </summary>
        /// <param name="newTagName">Tag name</param>
        /// <returns>Index where to insert tag</returns>
        private int GetTagInsertIndex(string newTagName)
        {
            /* JP: convert to binary search in future */
            List<DlkTag> temp = AllTags.OrderBy(x => x.Name).ToList();
            int i;
            for (i=0; i < temp.Count; i++)
            {
                if (string.Compare(temp[i].Name, newTagName) > 0)
                {
                    return i;
                }
            }
            return temp.Any() ? temp.Count : 0;
        }

        /// <summary>
        /// Handler of Add tag click
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CacheOrClearOldValuesofSelectedTag(true);
                CurrentTagPageMode = TagPageMode.Add;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for EditTag click
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void btnEditTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CacheOrClearOldValuesofSelectedTag(false);
                CurrentTagPageMode = TagPageMode.Edit;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for DeleteTag click
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void btnDeleteTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteTag();
                CurrentTagPageMode = TagPageMode.Default;
                bIsTagDirty = true;
                SaveSettingsList();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for OK confirmation of Add/Edit
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void btnCheckTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsTagNameValid(SelectedTag.Name.Trim(), txtTagName.Text.Trim()))
                {
                    DlkUserMessages.ShowError(this, string.IsNullOrEmpty(txtTagName.Text)
                        ? DlkUserMessages.ERR_TAG_NAME_BLANK : string.Format(DlkUserMessages.ERR_TAG_NAME_EXISTS, txtTagName.Text), "Tag Settings");
                    RevertCachedValuesofSelectedTag();
                    txtTagName.Focus();
                    txtTagName.SelectAll();
                    return;
                }
                else if (IsTagNameAllSpaces(txtTagName.Text))
                {
                    DlkUserMessages.ShowError(this, DlkUserMessages.ERR_TAG_NAME_BLANK, "Tag Settings");
                    return;
                }

                if (mTagPageMode == TagPageMode.Add)
                    AddNewTag();
                else if (mTagPageMode == TagPageMode.Edit)
                    EditTag();

                txtTagName.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
                txtTagDescription.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget(); 
                bIsTagDirty = true;
                SaveSettingsList();

                foreach (DlkTag tag in AllTags)
                {
                    if (!lstTagConflicts.Items.Cast<DlkTag>().Any(x => x.Id == tag.Id))
                    {
                        tag.RemoveTagConflict(SelectedTag.Id);
                    }
                }
                CurrentTagPageMode = TagPageMode.Default;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Cancel if Add/Edit operation
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void btnNotCheckTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RevertCachedValuesofSelectedTag();
                CurrentTagPageMode = TagPageMode.Default;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for ManageTagConflicts click
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void btnManageConflicts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* use new list of all tags */
                List<DlkTag> allTags = AllTags.ToList();
                int tagIdx = allTags.FindIndex(x => x.Id == SelectedTag.Id);
                if (tagIdx > -1)
                {
                    allTags.RemoveAt(allTags.FindIndex(x => x.Id == SelectedTag.Id));
                }
                ManageTags conflictsDlg = new ManageTags(allTags, AllTags.ToList().FindAll(x => mSelectedTag.ConflictsIDList.Contains(x.Id)), true);
                // JPV: Check if we can put in ManageTags dialog, we should not modify UI like this
                conflictsDlg.Title = "Manage Tag Conflicts - " + SelectedTag.Name;
                conflictsDlg.lblCurrentTags.Content = "Tag Conflicts";
                conflictsDlg.btnAddNewTag.Visibility = Visibility.Collapsed;
                conflictsDlg.Owner = this;
                if (conflictsDlg.ShowDialog() == true)
                {
                    SelectedTag.ReplaceTagConflicts(conflictsDlg.lbxCurrentTags.Items.Cast<DlkTag>().Select(x => x.Id).ToArray());
                    /* Add Tag ID of selected Tag to conflict list of affected tags */
                    foreach (DlkTag tag in AllTags)
                    {
                        if (conflictsDlg.lbxCurrentTags.Items.Cast<DlkTag>().Any(x => x.Id == tag.Id))
                        {
                            tag.AddTagConflict(SelectedTag.Id);
                        }
                    }
                    OnPropertyChanged("TagConflicts");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// SelectionChanged handler for Tag list
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event arguments</param>
        private void lstTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedTag = lstTags.SelectedItem as DlkTag ?? new DlkTag(string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region Test Connection code

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkEnvironment.URLBlacklist.Any(x => DlkEnvironment.IsSameURL(txtUrl.Text,x)))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_CONNECTION_URL_BLACKLIST);
                }
                else
                {
                    TestConnectionDialog testConnectionDialog = new TestConnectionDialog(txtID.Text);
                    testConnectionDialog.ShowDialog();
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

