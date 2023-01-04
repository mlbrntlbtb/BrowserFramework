//#define IS_DEBUG

using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using TestRunner.Common;



namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        #region PRIVATE MEMBERS
        private const int INT_MIN_DELAY = 1000;
        private const int INT_NORMAL_DELAY = 2000;
        private const int INT_MAX_DELAY = 3000;
        private static List<KwDirItem> mTestsToLoad;

        private BackgroundWorker mMyWorker = new BackgroundWorker();
        private BackgroundWorker mTestWorker = new BackgroundWorker();
        private DlkKeywordTestsLoader mKWTestLoader;
        private string mRegUpdateOk = string.Empty;
        private const string mUserRoot = "HKEY_CURRENT_USER";

        private enum SplashLoadState
        {
            OS_FILES_LOADED,
            READY_TO_LOAD_TESTS,
            STARTING_APPLICATION
        }
        #endregion

        #region PUBLIC MEMBERS
        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// Test that are loaded to display in Test Explorer tree
        /// </summary>
        public static List<KwDirItem> TestsToLoad
        {
            get
            {
                return mTestsToLoad;
            }
            set
            {
                mTestsToLoad = value;
            }
        }

        /// <summary>
        /// Object Store handler
        /// </summary>
        public DlkDynamicObjectStoreHandler OSHandler
        {
            get
            {
                return DlkDynamicObjectStoreHandler.Instance;
            }
        }

        /// <summary>
        /// Test loader handler
        /// </summary>
        public DlkKeywordTestsLoader KWLoader
        {
            get
            {
                return mKWTestLoader;
            }
        }
        #endregion

        #region PRIVATE METHODS
        private SplashLoadState State
        {
            set
            {
                switch (value)
                {
                    case SplashLoadState.OS_FILES_LOADED:
                        this.txtDescription.DataContext = null;
                        this.txtLoading.Text = DlkUserMessages.INF_SPLASH_FINISHED_LOAD_OS_FILES;
                        this.txtDescriptionTrailer.Text = string.Empty;
                        break;
                    case SplashLoadState.READY_TO_LOAD_TESTS:
                        this.txtLoading.Text = "Loading";
                        this.txtDescription.DataContext = KWLoader;
                        this.txtPercent.DataContext = KWLoader;
                        this.txtDescriptionTrailer.Text = " test script file...";
                        break;
                    case SplashLoadState.STARTING_APPLICATION:
                        this.txtDescription.DataContext = null;
                        this.txtLoading.Text = DlkUserMessages.INF_SPLASH_STARTING_APP;
                        this.txtDescriptionTrailer.Text = string.Empty;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Initialize()
        {
            CheckExistingConfigs();
            
            Process[] processName = Process.GetProcessesByName("TestRunner");
            if (processName.Length > 1) {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_TESTRUNNER_RUNNING);
                System.Environment.Exit(0); 
            }
            else
            {
                try
                {
                    /* Set Root Path based from executing binary */
                    string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string mRootPath = Directory.GetParent(binDir).FullName;
                    while (new DirectoryInfo(mRootPath).GetDirectories()
                        .Where(x => x.FullName.Contains("Products")).Count() == 0)
                    {
                        mRootPath = Directory.GetParent(mRootPath).FullName;
                    }

                    /* Initialize config file */
                    DlkTestRunnerSettingsHandler.Initialize(mRootPath);

                    /* Set Version info */
                    About abt = new About();
                    txtVersionNo.Text = abt.AssemblyVersion;

                    /* Check if this is first time launch */
#if IS_DEBUG
#else
                    if (DlkTestRunnerSettingsHandler.IsFirstTimeLaunch)
#endif
                    {
                        ChooseApplication initialLaunchDialog = new ChooseApplication(abt.AssemblyVersion);
                        if (!(bool)initialLaunchDialog.ShowDialog())
                        {
                            System.Environment.Exit(0);
                        }
#if IS_DEBUG
#else
                        else
                        {
                            DlkTestRunnerSettingsHandler.Save();
                        }
#endif
                    }

                    String mProductPath = DlkTestRunnerSettingsHandler.ApplicationUnderTest.ProductFolder;
                    String mLibrary = System.IO.Path.Combine(binDir, DlkTestRunnerSettingsHandler.ApplicationUnderTest.Library);

                    /* Initialize directories */
                    DlkEnvironment.InitializeEnvironment(mProductPath, mRootPath, mLibrary);

                    /* Intialize private members */
                    mKWTestLoader = new DlkKeywordTestsLoader();
                    mMyWorker.DoWork += mMyWorker_DoWork;
                    mMyWorker.RunWorkerCompleted += mMyWorker_RunWorkerCompleted;
                    mMyWorker.ProgressChanged += mMyWorker_ProgressChanged;
                    mMyWorker.WorkerReportsProgress = true;

                    /* Initialize data bindings */
                    txtPercent.DataContext = OSHandler;
                    txtDescription.DataContext = OSHandler;

                    if (!DlkTestRunnerSettingsHandler.ApplicationUnderTest.Type.ToLower().Equals("internal"))
                    {
                        /* Update Registry settings */
                        UpdateRegistrySettings();
                    }

                    
                    //Check/Set applications on start up
                    string targetLocation = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                    string appName= Path.Combine(DlkEnvironment.mDirTools, @"TestRunner\AutoAgentStartUp.bat");

                    if(!System.IO.File.Exists(Path.Combine(targetLocation,Path.GetFileNameWithoutExtension(appName)+".lnk")))
                    {
                        CreateShortcutOnStartUp(appName, targetLocation);
                    }
                }
                catch (Exception ex)
                {
                    //handle .NET Framework version error on startup
                    string errMessage = !IsSupportedDotNetFramework() ? DlkUserMessages.ERR_UNSUPPORTED_DOTNETFRAMEWORK : DlkUserMessages.ERR_GENERIC_STARTUP_ERROR;
                    DlkUserMessages.ShowError(errMessage);
                    DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                    System.Environment.Exit(0);
                }
            }

            this.Activate();
        }

        /// <summary>
        /// Creates a shortcut for an application on the specified location
        /// </summary>
        /// <param name="appName">application source and name for shortcut creation</param>
        /// <param name="targetLocation">target location where shortcut would be created</param>
        private void CreateShortcutOnStartUp(string appName, string targetLocation)
        {
            try
            {
                string shortcutPath = System.IO.Path.Combine(targetLocation, Path.GetFileNameWithoutExtension(appName) + ".lnk");
                WshShell myShell = new WshShell();
                WshShortcut myShortcut = (WshShortcut)myShell.CreateShortcut(shortcutPath);
                myShortcut.TargetPath = appName;
                myShortcut.IconLocation = appName + ",0"; 
                myShortcut.WorkingDirectory = Path.Combine(DlkEnvironment.mDirTools, "TestRunner"); 
                myShortcut.Arguments = ""; 
                myShortcut.Save(); 
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        //Clean Up
        private void CheckExistingConfigs()
        {

            var sourceConfigDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //For internal builds: Find previous test runner config files and merge into 1 config file
            var mainConfig = Path.Combine(sourceConfigDir, "config.xml");

            if (System.IO.File.Exists(mainConfig))
            {
                System.IO.File.Move(mainConfig, DlkConfigHandler.MainConfig);
            }
            
            //For internal builds: Find previous test scheduler config files
            var destConfigDir = DlkConfigHandler.GetConfigRoot("Scheduler");

            //move local host
            var localHost = Path.Combine(sourceConfigDir, Environment.MachineName + ".xml");
            if (System.IO.File.Exists(localHost))
            {
                System.IO.File.Move(localHost, Path.Combine(destConfigDir, Environment.MachineName + ".xml"));
            }

            var hostFile = Path.Combine(sourceConfigDir, "Hosts.xml");
            if (System.IO.File.Exists(hostFile))
            {
                XDocument DlkXml = XDocument.Load(hostFile);

                var data = from doc in DlkXml.Descendants("host")
                           select new
                           {
                               name = doc.Attribute("name").Value,
                               type = doc.Attribute("type").Value == "local" ? HostType.LOCAL : HostType.NETWORK,
                               status = doc.Attribute("status").Value,
                           };

                //move network host
                foreach (var host in data)
                {
                    var sourceConfigFile = Path.Combine(sourceConfigDir, host.name + ".xml");

                    //move network hosts
                    if (System.IO.File.Exists(sourceConfigFile) && Directory.Exists(destConfigDir))
                    {
                        //move file to product config directory
                        System.IO.File.Move(sourceConfigFile, Path.Combine(destConfigDir, host.name + ".xml"));
                    }
                }

                //move host file
                System.IO.File.Move(hostFile, Path.Combine(destConfigDir, "Hosts.xml"));
            }
        }

        /// <summary>
        /// Update the Registry settings of the machine for test automation execution
        /// </summary>
        private void UpdateRegistrySettings()
        {
            try
            {
                /* Commented out this parts, just set all the time for external release */
                // Get the application configuration file.
                //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                //mRegUpdateOk = config.AppSettings.Settings["RegUpdate"].Value;

                //if (mRegUpdateOk == "false")
                {
                    UpdateEnableProtectedMode();
                    SetZoomLevel();
                    //SaveConfigFile(config);
                }
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_REG_UPDATE_ERROR, e);
            }
        }

        /// <summary>
        /// Update the Enable Protected Mode setting
        /// </summary>
        private void UpdateEnableProtectedMode()
        {
            const string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones";
            string machineOS = Environment.OSVersion.ToString();

            try
            {
                if (!machineOS.Contains("Microsoft Windows XP"))
                {
                    //Local Intranet setting
                    const string keyName1 = mUserRoot + "\\" + subKey + "\\" + "1\\";
                    Registry.SetValue(keyName1, "2500", 0);

                    //Trusted sites setting
                    const string keyName2 = mUserRoot + "\\" + subKey + "\\" + "2\\";
                    Registry.SetValue(keyName2, "2500", 0);

                    //Internet setting
                    const string keyName3 = mUserRoot + "\\" + subKey + "\\" + "3\\";
                    Registry.SetValue(keyName3, "2500", 0);

                    //Restricted sites setting
                    const string keyName4 = mUserRoot + "\\" + subKey + "\\" + "4\\";
                    Registry.SetValue(keyName4, "2500", 0);
                }
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_REG_UPDATE_ERROR, e);
            }
        }

        /// <summary>
        /// Update the Zoom Level setting.
        /// </summary>
        private void SetZoomLevel()
        {
            const string subKey = "Software\\Microsoft\\Internet Explorer\\Zoom";

            try
            {
                var ieVersion = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer").GetValue("Version");

                if (ieVersion.ToString().StartsWith("11"))
                {
                    const string zoomLevelKeyName = mUserRoot + "\\" + subKey;
                    Registry.SetValue(zoomLevelKeyName, "ZoomFactor", 0x000186a0, RegistryValueKind.DWord);
                }
                else
                {
                    const string zoomLevelKeyName = mUserRoot + "\\" + subKey;
                    Registry.SetValue(zoomLevelKeyName, "ZoomFactor", 100000, RegistryValueKind.DWord);
                }
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_REG_UPDATE_ERROR, e);
            }
        }

        /// <summary>
        /// Save the configuration file after the settings have been modified.
        /// This will serve as the flag so that the registry settings method will not be performed
        /// the next time the user launches the TestRunner.
        /// </summary>
        /// <param name="config"></param>
        private void SaveConfigFile(System.Configuration.Configuration config)
        {
            config.AppSettings.Settings.Remove("RegUpdate");
            config.AppSettings.Settings.Add("RegUpdate", "true");

            // Save the configuration file.
            try
            {
                //config.Save();
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_REG_UPDATE_ERROR, e);
            }

        }

        /// <summary>
        /// Returns true if Microsoft DotNet Framework 4.7 or later versions is installed.
        /// </summary>
        private bool IsSupportedDotNetFramework()
        {
            try
            {
                const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

                using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
                {
                    if (ndpKey != null && ndpKey.GetValue("Release") != null)
                    {
                        var version = CheckForDotnetVersion((int)ndpKey.GetValue("Release"));
                        if (version == null)
                            return false;
                        else
                            return true;

                    }
                    else return false;
                }

                string CheckForDotnetVersion(int releaseKey)
                {
                    if (releaseKey >= 528040)
                        return "4.8";
                    if (releaseKey >= 461808)
                        return "4.7.2";
                    if (releaseKey >= 393295)
                        return "4.6";
                    if (releaseKey >= 378389)
                        return "4.5";

                    // less than 4.5
                    return null;
                }
            }
            catch
            {
                // In case extracting value from registry fails, use generic error message but log this information in error log:
                DlkLogger.LogToFile("Failed to extract Microsoft .NET version.");
                return true;              
            }
        }

        #endregion

        #region EVENT HANDLERS
        private void mMyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                State = (SplashLoadState)(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mMyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                /* Self kill */
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mMyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                /* Initialize Object Store */
                OSHandler.Initialize();
                Thread.Sleep(INT_NORMAL_DELAY);
                while (OSHandler.StillLoading)
                {
                    Thread.Sleep(INT_MIN_DELAY);
                }
                mMyWorker.ReportProgress((int)SplashLoadState.OS_FILES_LOADED);

                if (OSHandler.OutDatedOS && DlkEnvironment.IsShowAppNameProduct)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_OUTDATED_OBJECTSTORE);
                    Environment.Exit(0);
                }

                /* Load tests to display in Test Explorer */
                KWLoader.Initialize();
                mMyWorker.ReportProgress((int)SplashLoadState.READY_TO_LOAD_TESTS);
                Thread.Sleep(INT_NORMAL_DELAY);
                while (KWLoader.StillLoading)
                {
                    Thread.Sleep(INT_MIN_DELAY);
                }
                mMyWorker.ReportProgress((int)SplashLoadState.STARTING_APPLICATION);
                Thread.Sleep(INT_NORMAL_DELAY);
                TestsToLoad = KWLoader.TestsLoaded;
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
                mMyWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }

    /// <summary>
    /// Value converter for loading progress computation
    /// </summary>
    public class DlkProgressConverter : IValueConverter
    {
        #region DECLARATIONS
        public static bool FinalStageInitiated;
        public static bool FinalStageLock;
        #endregion

        #region CONSTRUCTOR
        public DlkProgressConverter()
        {
            FinalStageInitiated = false;
            FinalStageLock = false;
        }
        #endregion

        #region METHODS
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int ret = -1;
            if (FinalStageLock && int.Parse(value.ToString()) >= 100)
            {
                FinalStageLock = true;
                ret = 100;
            }
            else if ((FinalStageInitiated && int.Parse(value.ToString()) < 100) || FinalStageLock)
            {
                FinalStageLock = true;
                ret = System.Convert.ToInt32((int.Parse(value.ToString()) * 0.25)) + 75;
            }
            else
            {
                if (int.Parse(value.ToString()) == 100)
                {
                    FinalStageInitiated = true;
                }
                ret = System.Convert.ToInt32((int.Parse(value.ToString()) * 0.75));
            }
            return Math.Min(ret, 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        #endregion
    }
}
