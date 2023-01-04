#define IS_DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Reflection;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class LoadingDialog : Window
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
        private Window mOwner;

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
        public LoadingDialog(Window Owner)
        {
            InitializeComponent();
            this.Owner = Owner;
            mOwner = Owner;
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
                        txtPercent.DataContext = KWLoader;
                        progressBar.DataContext = KWLoader;
                        this.txtDescriptionTrailer.Text = " test script file...";
                        break;
                    case SplashLoadState.STARTING_APPLICATION:
                        this.txtDescription.DataContext = null;
                        this.txtLoading.Text = "Reloading Test Runner...";
                        this.txtDescriptionTrailer.Text = string.Empty;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Initialize()
        {
            {
                try
                {
                    OSHandler.FileLoadProgress = 0;
                    /* Reset static members of converter prior to use */
                    DlkProgressConverter.FinalStageInitiated = false;
                    DlkProgressConverter.FinalStageLock = false;

                    /* Set Root Path based from executing binary */
                    string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string mRootPath = Directory.GetParent(binDir).FullName;
                    while (new DirectoryInfo(mRootPath).GetDirectories()
                        .Where(x => x.FullName.Contains("Products")).Count() == 0)
                    {
                        mRootPath = Directory.GetParent(mRootPath).FullName;
                    }

                    ///* Initialize config file */
                    //DlkTestRunnerSettingsHandler.Initialize(mRootPath);

                    /* Set Version info */
                    About abt = new About();

                    /* Check if this is first time launch */
//#if IS_DEBUG
//#else
//                    if (DlkTestRunnerSettingsHandler.IsFirstTimeLaunch)
//#endif
//                    {
//                        ChooseApplication initialLaunchDialog = new ChooseApplication(abt.AssemblyVersion);
//                        if (!(bool)initialLaunchDialog.ShowDialog())
//                        {
//                            System.Environment.Exit(0);
//                        }
//#if IS_DEBUG
//#else
//                        else
//                        {
//                            DlkTestRunnerSettingsHandler.Save();
//                        }
//#endif
//                    }

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

                    progressBar.DataContext = OSHandler;
                    txtPercent.DataContext = OSHandler;
                    txtDescription.DataContext = OSHandler;
                }
                catch
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_INITIALIZE_ERROR);
                    return;
                }
//#if DEBUG
//#else
//            /* Update Registry settings */
//            //UpdateRegistrySettings();
//#endif
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
                ((MainWindow)mOwner).Initialize();
                ((MainWindow)mOwner).RefreshTestTree();
                ((MainWindow)mOwner).Activate();
                ((MainWindow)mOwner).BringIntoView();
                ((MainWindow)mOwner).Focus();
                ((MainWindow)mOwner).ShowInTaskbar = true;
                //Thread.Sleep(10000);
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
}
