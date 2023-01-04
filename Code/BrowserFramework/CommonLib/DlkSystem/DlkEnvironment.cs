#define EDGE_SUPPORT

using CommonLib.DlkRecords;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CommonLib.DlkUtility;
using CommonLib.DlkHandlers;
using Microsoft.Win32;
using Microsoft.Edge.SeleniumTools;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonLib.DlkSystem
{
    /// <summary>
    /// This class stores information about the test enviroment
    /// </summary>
    public static class DlkEnvironment
    {
        #region PRIVATE MEMBERS
        private static String _DirTestResultsCurrent { get; set; }
        private static String mWebView = "WEBVIEW";
        private static string mInitialAppActivity = string.Empty;
        private static List<DlkBrowserCapability> mBrowserCapabilities;
        #endregion

        #region CONSTANTS
        /// <summary>
        /// Default short wait : 1/4 s
        /// </summary>
        public const int mShortWaitMs = 250;
        /// <summary>
        /// Default medium wait : 1s
        /// </summary>
        public const int mMediumWaitMs = 1000;
        /// <summary>
        /// Default Medium Long wait : 3s
        /// </summary>
        public const int mMediumLongWaitMs = 3000;
        /// <summary>
        /// Default Long wait : 10s
        /// </summary>
        public const int mLongWaitMs = 10000;
        /// <summary>
        /// Default status bar height : 100px
        /// </summary>
        public const int defaultStatusBarHeight = 100;

        public const string DefaultPin = "1111";

        /// <summary>
        /// Connect Test file
        /// </summary>
        public const string STR_TEST_CONNECT_FILE = "test_connect.dat";

        /// <summary>
        /// Ad-hoc test results manifest file
        /// </summary>
        public const string STR_TEST_RESULTS_MANIFEST_FILE = "tr_results_manifest.xml";

        /// <summary>
        /// Process used to start/terminate AVD (emulator)
        /// </summary>
        public const string STR_AVD_PROCESS = "qemu-system-i386";

        /// <summary>
        /// Default device name for the first instance of emulator
        /// </summary>
        public const string STR_DEFAULT_FIRST_INSTANCE_EMULATOR = "emulator-5554";
        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// all directories are childs of this root directory
        /// </summary>
        public static String mRootDir = @"C:\QEAutomation\Selenium\BrowserFramework\";
        public static String mProductFolder { get; set; }
        public static String mLibrary { get; set; }

        /// <summary>
        /// List of URL blacklist from ProdConfig
        /// </summary>
        public static String[] URLBlacklist { get; set; }

        public static bool mDoNotKillDriversOnTearDown { get; set; }

        /// <summary>
        /// This is the root directory for the product under test. All data files will be found under there (e.g. object store files, user data) 
        /// </summary>
        public static String mDirProduct { get; set; }

        /// <summary>
        /// This is the root directory for the product under test. All data files will be found under there (e.g. object store files, user data) 
        /// </summary>
        public static String mDirProductsRoot { get; set; }

        /// <summary>
        /// This folder is under the given product directory. It holds a number of directories (for Results, Object Store, etc). 
        /// </summary>
        public static String mDirFramework { get; set; }

        /// <summary>
        /// With each session we create a new folder within TestResults. 
        /// Everything from the session is put here : e.g. logs, screen captures, copy of the test, etc. 
        /// </summary>
        public static String mDirTestResultsCurrent
        {
            get
            {
                if (_DirTestResultsCurrent == null)
                {
                    _DirTestResultsCurrent = mDirTestResults + CommonLib.DlkUtility.DlkString.GetDateAsText("file") + @"\";
                    if (!Directory.Exists(_DirTestResultsCurrent))
                    {
                        Directory.CreateDirectory(_DirTestResultsCurrent);
                    }
                }
                return _DirTestResultsCurrent;
            }
            set
            {
                _DirTestResultsCurrent = value;
            }
        }

        // needed to track which test in suite
        public static string mCurrentTestIdentifier = "";

        /// <summary>
        /// This folder is under the given framework directory. It holds the published executables testers use
        /// </summary>
        public static String mDirTools { get; set; }

        /// <summary>
        /// This folder is under the framework dir. It holds the object store files.
        /// </summary>
        public static String mDirObjectStore { get; set; }

        /// <summary>
        /// This folder is under the framework dir. It holds the remote browsers reference file.
        /// </summary>
        public static String mDirRemoteBrowsers { get; set; }

        /// <summary>
        /// This folder holds the test suites
        /// </summary>
        public static String mDirTestSuite { get; set; }

        /// <summary>
        /// This folder holds the tests
        /// </summary>
        public static String mDirTests { get; set; }

        /// <summary>
        /// This folder holds the test data that might be used as part of a test
        /// </summary>
        public static String mDirUserData { get; set; }

        /// <summary>
        /// This folder holds the test data that might be used as part of a test
        /// </summary>
        public static String mDirDocDiff { get; set; }

        /// <summary>
        /// This folder holds the test data retrieved from the database
        /// </summary>
        public static String mDirData { get; set; }

        /// <summary>
        /// This folder holds the test data that might be used as part of a test
        /// </summary>
        public static String mDirDocDiffActualFile { get; set; }

        /// <summary>
        /// This folder holds the test data that might be used as part of a test
        /// </summary>
        public static String mDirDocDiffExpectedFile { get; set; }

        /// <summary>
        /// This folder holds the test data that might be used as part of a test
        /// </summary>
        public static String mDirDocDiffConfigFile { get; set; }

        /// <summary>
        /// This folder is child to the Framework directory. Result files will be here. 
        /// </summary>
        public static String mDirTestResults { get; set; }

        /// <summary>
        /// This folder is child to the Framework directory. Result files will be here. 
        /// </summary>
        public static String mDirSuiteResults { get; set; }

        /// <summary>
        /// This folder is child to the Framework directory. Config files will be here. 
        /// </summary>
        public static String mDirConfigs { get; set; }

        /// <summary>
        /// This folder is child to the Common directory. Common config files will be here. 
        /// </summary>
        public static String mDirCommonConfigs { get; set; }

        /// <summary>
        /// This is the LoginConfig id that is used to look up login data
        /// </summary>
        public static String mLoginConfig { get; set; }

        /// <summary>
        /// This is the location of the login config file under a given product
        /// </summary>
        public static String mLoginConfigFile { get; set; }

        /// <summary>
        /// This is the location of the remote browsers reference file under a given product
        /// </summary>
        public static String mRemoteBrowsersFile { get; set; }

        /// <summary>
        /// This is the location of the remote browsers reference file under a given product
        /// </summary>
        public static String mMobileConfigFile { get; set; }

        /// <summary>
        /// This is the location of the test editor help reference file under a given product
        /// </summary>
        public static String mHelpConfigFile { get; set; }

        /// <summary>
        /// This is the location of the test editor help reference file under common folder
        /// </summary>
        public static String mCommonHelpConfigFile { get; set; }

        /// <summary>
        /// This is the location of dashboard related files
        /// </summary>
        public static String mDirDataFrameworkDashboardRepository { get; set; }

        /// <summary>
        /// This is the location of published dashboard  files
        /// </summary>
        public static String mDirDataFrameworkDashboardRepositoryPublished { get; set; }

        /// <summary>
        /// This is the location of working dashboard  files
        /// </summary>
        public static String mDirDataFrameworkDashboardRepositoryWorking { get; set; }

        /// <summary>
        /// this is the relative path to the current test
        /// </summary>
        public static String mTestPath { get; set; }

        /// <summary>
        /// this is the instance of the test we are executing; all tests are data driven and so have data for 1 to n tests embedded
        /// </summary>
        public static int mTestInstance { get; set; }

        /// <summary>
        /// this is the current product name loaded in advanced scheduler
        /// </summary>
        public static string mCurrentProductName { get; set; }

        /// <summary>
        /// This is the setting level where we set our error log notifications
        /// </summary>
        public static String mErrorLogLevel
        {
            get
            {
                return new DlkSettingsConfigHandler().MErrorLogLevel;
            }
        }

        /// <summary>
        /// This is the browser we are executing the test against
        /// </summary>
        public static String mBrowser { get; set; }

        public static String mBrowserID { get; set; }

        public static String mAndroidHomePath = Environment.GetEnvironmentVariable("ANDROID_HOME");

        public static String mEmulatorPath = mAndroidHomePath + "\\tools";

        public static String mToolsChromeDriverPath { get; set; }

        public static String mAdbPath = mAndroidHomePath + "\\platform-tools";

        public static String mEmulatorConfigFile = mAdbPath + "\\devices.txt";

        public static bool mKeepBrowserOpen { get; set; }

        public static bool mLockContext { get; set; }

        public static List<DlkBrowser> mAvailableBrowsersList = null;

        /// <summary>
        /// This is the list of installed browsers installed on the machine
        /// </summary>
        public static List<DlkBrowser> mAvailableBrowsers
        {
            get
            {
                if (mAvailableBrowsersList == null)
                {
                    mAvailableBrowsersList = GetAvailableBrowsers();
                }
                return mAvailableBrowsersList;
            }
        }

        /// <summary>
        /// used during image comparison... sets a threshold for what percentage of difference is ok
        /// </summary>
        public static int mImageCapturePixelMismatchThreshold { get; set; }

        /// <summary>
        /// true/false if the browser is currently open based on the process existing
        /// </summary>
        public static Boolean IsBrowserOpen
        {
            get
            {
                Boolean bRunning = false;
                switch (mBrowser.ToLower())
                {
                    case "chrome":
                        if (ProcessExists("chrome"))
                        {
                            bRunning = true;
                        }
                        break;
                    case "firefox":

                        if (ProcessExists("firefox"))
                        {
                            bRunning = true;

                        }
                        break;
                    case "ie":
                        if (ProcessExists("iexplore"))
                        {
                            bRunning = true;
                        }
                        break;
                    case "ios":
                    case "android":
                    case "safari":
                        bRunning = true;
                        break;
                    default:
                        throw new Exception("Need to add support for this browser: " + mBrowser);
                }
                return bRunning;
            }
        }

        /// <summary>
        /// This is the WebDriver we are interacting with to use Selenium with the selected Browser
        /// </summary>
        public static IWebDriver AutoDriver { get; set; }

        public static string mPreviousTitle;

        /// <summary>
        /// Boolean to determine if driver switched to iframe
        /// </summary>
        public static bool mSwitchediFrame = false;

        /// <summary>
        /// Boolean to denotes if the browser is in an actual mobile device
        /// </summary>
        public static bool mIsMobile { get; set; }

        public static bool mIsMobileBrowser { get; set; }

        public static bool mIsDeviceEmulator { get; set; }

        public static bool mIsMobileSettingChange { get; set; }

        private static bool mIsRemote { get; set; }

        public static string mPin { get; set; }

        /// <summary>
        /// Moblie device screen resolution
        /// </summary>
        public static int mDeviceWidth { get; set; }
        public static int mDeviceHeight { get; set; }

        public static int mStatusBarHeight { get; set; }

        public static string InitialAppActivity
        {
            set
            {
                mInitialAppActivity = value;
            }
        }

        /// <summary>
        /// Checks if current product allows showing of app name
        /// </summary>
        public static bool IsShowAppNameProduct
        {
            get 
            {
                return new string[]
                {
                                    "BudgetingAndPlanning",
                                    "Costpoint_711",
                                    "TimeAndExpense",
                                    "Costpoint_701"
                }.Contains(DlkEnvironment.mProductFolder);
            }
        }

        /// <summary>
        /// Checks if current product allows using downloads folder as default folder for downloaded files
        /// </summary>
        public static bool IsUseDownloadDirProduct
        {
            get
            {
                return new string[]
                {
                                    "AcumenTouchStone",
                                    "AcumenTouchStone_81"
                }.Contains(DlkEnvironment.mProductFolder);
            }
        }

        /// <summary>
        /// Checks if current product allows disabling notifications
        /// </summary>
        public static bool IsDisableNotifProduct
        {
            get
            {
                return new string[]
                {
                                    "WorkBook",
                }.Contains(DlkEnvironment.mProductFolder);
            }
        }

        public static int DefaultAppiumCommandTimeout = 300;
        public static Boolean IsLoggedIn = false;
        public static int MediumWaitMs = 1000;
        public static int ShortWaitMs = 500;
        public static int LongWaitMs = 3000;
        public static int VerifyRetryDelay = 500;
        public static int VerifyRetryCount = 10;
        public static String LastUrl = "";
        public static String existingWindowHandle = "";
        public static string LastKnownBrowserTitle = "";

        public static string mDirFrameworkLibraryTags { get; set; }
        public static string mDirFrameworkLibraryQueries { get; set; }
        public static string mTagsFilePath { get; set; }
        public static string mVersionSupportListPath { get; set; }

        /// <summary>
        /// Used for storing custom info values then later used in summary results
        /// </summary>
        public static Dictionary<string, string[]> CustomInfo { get; set; }

        public static int mWindowIndex { get; set; }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Check if the environment contains blacklisted URL
        /// </summary>
        /// <param name="Environment">Environment to be checked</param>
        /// <param name="SkipInitialize">Whether or not to skip initializing blacklist, default false</param>
        /// <returns>True if environment contains blacklist URL, False if not</returns>
        public static bool IsURLBlacklist(string Environment, bool SkipInitialize=false)
        {
            bool isBlacklist = false;
            if (!SkipInitialize)
            {
                InitializeBlacklistedURLs();
            }
            if (DlkEnvironment.URLBlacklist == null)
            {
                return isBlacklist;
            }

            DlkLoginConfigHandler mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, Environment);

            isBlacklist = DlkEnvironment.URLBlacklist.Any(x => IsSameURL(mLoginConfigHandler.mUrl, x));
            return isBlacklist;
        }

        /// <summary>
        /// Check if prodconfig is Show App Name enabled
        /// </summary>
        /// <returns></returns>
        public static bool IsShowAppNameEnabled()
        {            
            string result = DlkConfigHandler.GetConfigValue("showappname");
            return string.IsNullOrEmpty(result) ? false : bool.Parse(result);
        }

        /// <summary>
        /// Check if two URLs are the same, ignoring URL security differences, character case, same indexes and empty queries
        /// </summary>
        /// <param name="Url1">First URL to be compared</param>
        /// <param name="Url2">Second URL to be compared</param>
        /// <returns>True if both URLs are the same, False otherwise</returns>
        public static bool IsSameURL(string Url1, string Url2)
        {
            if (Url1 == "" || Url2 == "")
            {
                return false;
            }

            Url1 = Url1.TrimEnd(' ', '/', '?').ToLower().Replace("https://", "http://");
            Url2 = Url2.TrimEnd(' ', '/', '?').ToLower().Replace("https://", "http://");
            return GetRootHost(Url1) == GetRootHost(Url2);
        }

        /// <summary>
        /// Convert any URL to base root host
        /// </summary>
        /// <param name="Url">URL to convert</param>
        /// <returns>Base URL host</returns>
        public static string GetRootHost(string Url)
        {
            Url = Url.Replace("https://www.", "https://").Replace("http://www.", "http://");
            string baseHost = new Uri(Url).Host.Split('.').First();
            return baseHost;
        }

        /// <summary>
        /// Load blacklisted URLs from ProdConfig to environment list
        /// </summary>
        public static void InitializeBlacklistedURLs()
        {
            DlkProductConfigHandler mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(mDirFramework, "Configs\\ProdConfig.xml"));
            URLBlacklist = mProdConfigHandler.GetConfigValue("blacklisturl").Split(';');
        }

        /// <summary>
        /// Initializes the test environment
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="LoginConfig"></param>
        /// <param name="TestPath"></param>
        /// <param name="TestInstance"></param>
        /// <param name="Browser"></param>
        public static void InitializeEnvironment(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBroserOpen)
        {
            // Assign base class props
            mProductFolder = Product;
            mLoginConfig = LoginConfig;
            mTestPath = TestPath;
            mTestInstance = Convert.ToInt32(TestInstance);
            mBrowser = Browser;
            if (DlkTestExecute.IsBrowserOpen && mProductFolder == "CPSmartPhone")
            {
                DlkMobileRecord mobileDev = DlkMobileHandler.GetRecord(mBrowser);
                if (mobileDev != null)
                {
                    mBrowser = mobileDev.Application;
                    DlkLogger.LogInfo("CPSmartPhone browser set to original value.");
                }
            }
            mKeepBrowserOpen = bool.Parse(KeepBroserOpen);

            InitializeProductPaths();
            InitializeBlacklistedURLs();
            mImageCapturePixelMismatchThreshold = 2;

        }

        /// <summary>
        /// Initializes the test environment
        /// </summary>
        /// <param name="ProductFolder"></param>
        /// <param name="LoginConfig"></param>
        /// <param name="TestPath"></param>
        /// <param name="TestInstance"></param>
        /// <param name="Browser"></param>
        public static void InitializeEnvironment(String ProductFolder, String RootPath, String Library, String ProductName = "")
        {
            // Assign base class props
            mProductFolder = ProductFolder;
            mRootDir = RootPath;
            mLibrary = Library;

            if (ProductName != "")
                mCurrentProductName = ProductName;

            InitializeProductPaths();

            mImageCapturePixelMismatchThreshold = 2;

        }

        /// <summary>
        /// Assigns the correct driver to AutoDriver based on the browser type (firefox, ie, etc) and starts the browser
        /// </summary>
        /// <param name="HideDriver">Flag whether to display or hide driver console window</param>
        /// <param name="BrowserExtension">Full filepath of the browser extension</param>
        public static void StartBrowser(bool HideDriver=false, string BrowserExtension="")
        {
            Cursor.Position = new Point(0, 0);
            mIsMobile = false;
            mIsMobileBrowser = false;
            mIsRemote = false;
            mBrowserID = mBrowser;
            InitializeBrowserCapabilities();

            switch (mBrowser.ToLower())
            {
                case "edge":
                case "firefox":
                case "chrome":
                case "ie":
                //case "phantomjs (headless)":
                case "chrome (headless)":
                    string error;

                    if (BrowserExtension != "")
                    {
                        if (File.Exists(BrowserExtension))
                        {
                            if (!CreateWebDriver(mBrowser, out error, HideDriver, BrowserExtension))
                            {
                                DlkLogger.LogWarning("WebDriver failed to initialize browser. Retrying...");
                                CloseSession();
                                if (!CreateWebDriver(mBrowser, out error, HideDriver, BrowserExtension))
                                {
                                    CloseSession();
                                    throw new Exception("WebDriver could not recover from fatal error and exited with the following message: \""
                                        + error + "\"");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Extension file for " + Path.GetFileName(BrowserExtension) + " is missing.");
                        }
                    }
                    else
                    {
                        /* Implement a recovery logic, to try again once if first attempt to instantiate webdriver fails */
                        if (!CreateWebDriver(mBrowser, out error, HideDriver))
                        {
                            DlkLogger.LogWarning("WebDriver failed to initialize browser. Retrying...");
                            CloseSession();
                            if (!CreateWebDriver(mBrowser, out error, HideDriver))
                            {
                                CloseSession();
                                throw new Exception("WebDriver could not recover from fatal error and exited with the following message: \""
                                    + error + "\"");
                            }
                        }
                    }
                    break;
                default:
                    //check for mobile
                    DlkMobileRecord mobileDev = DlkMobileHandler.GetRecord(mBrowser);

                    if (mobileDev != null)
                    {
                        //Start up emulator first before launching Appium to prevent crash.
                        if (mobileDev.IsEmulator)
                        {
                            //discard remaining instances of emulator to start a new one
                            KillProcessByName(STR_AVD_PROCESS);
                            KillProcessByName("adb");
                            RunProcess("emulator", "-avd " + mobileDev.DeviceName, mEmulatorPath, false, -1);
                            //programmatically wait AVDs here before proceeding to run Appium
                            for (int i = 0; i <= mobileDev.CommandTimeout; i++)
                            {
                                if (File.Exists(mEmulatorConfigFile))
                                {
                                    RunProcess("cmd", "/c del devices.txt", mAdbPath, true, 0);
                                }

                                RunProcess("cmd", "/c adb devices > devices.txt", mAdbPath, true, 0);
                                String emulators = File.ReadAllText(mEmulatorConfigFile);
                                if (!emulators.Contains(STR_DEFAULT_FIRST_INSTANCE_EMULATOR) || emulators.Contains("offline"))
                                {
                                    DlkLogger.LogInfo("Waiting for emulator to load...");
                                    continue;
                                }
                                else
                                {
                                    Thread.Sleep(mMediumLongWaitMs);
                                    DlkLogger.LogInfo("Emulator has loaded successfully!");
                                    break;
                                }
                            }
                        }
                        
                        mIsDeviceEmulator = mobileDev.IsEmulator;
                        mIsMobile = true;
                        KillProcessByName("node");
                        KillProcessByName("chromedriver");
                        
                        //Spawn Appium only if environment is Android. Does not apply to iOS since we are running tests on remote Mac.
                        if(mobileDev.MobileType.ToLower() == "android")
                        {
                            RunProcess("appium", "--session-override --chromedriver-executable " + mToolsChromeDriverPath, "", false, -1);
                        }
                        
                        Thread.Sleep(2 * mLongWaitMs);
                    }

                    if (!mIsMobile) /* Remote execution */
                    {
                        mIsRemote = true;
                        DlkRemoteBrowserRecord remoteBrowser = DlkRemoteBrowserHandler.GetRecord(mBrowser);
                        if (remoteBrowser != null)
                        {
                            ICapabilities capabilities;
                            mBrowser = remoteBrowser.Browser;
                            switch (remoteBrowser.Browser.ToLower())
                            {
                                case "ie":
                                    capabilities = new InternetExplorerOptions().ToCapabilities();
                                    break;
                                case "firefox":
                                    capabilities = new FirefoxOptions().ToCapabilities();
                                    break;
                                case "chrome":
                                    capabilities = new ChromeOptions().ToCapabilities();
                                    break;
                                case "safari":
                                    capabilities = new SafariOptions().ToCapabilities();
                                    break;
                                case "edge":
                                    capabilities = new Microsoft.Edge.SeleniumTools.EdgeOptions().ToCapabilities();
                                    break;
                                default:
                                    throw new Exception("Unsupported test browser. Browser: " + remoteBrowser.Browser);
                            }
                            AutoDriver = new RemoteWebDriver(new Uri(remoteBrowser.Url), capabilities);
                            AutoDriver.Manage().Window.Maximize();
                        }
                    }
                    else /* Mobile */
                    {
                        DlkMobileRecord mobileDevice = DlkMobileHandler.GetRecord(mBrowser);
                        if (mobileDevice != null)
                        {
                            DriverOptions options = new AppiumOptions();
                            mBrowser = mobileDevice.MobileType;
                            switch (mobileDevice.MobileType.ToLower())
                            {
                                case "android":
                                    options.AddAdditionalCapability("platformName", "Android");
                                    options.AddAdditionalCapability("setWebContentsDebuggingEnabled", true);
                                    //capabilities.SetCapability("autoWebView", true);
                                    options.AddAdditionalCapability("unicodeKeyboard", true); // disable soft keyboard
                                    options.AddAdditionalCapability("app", GetAbsolutePath(mobileDevice.Path, mProductFolder));
                                    options.AddAdditionalCapability("appPackage", mobileDevice.Application);
                                    if (mobileDevice.Application != "Chrome")
                                    {
                                        options.AddAdditionalCapability("androidDeviceSocket", mobileDevice.Application + "_devtools_remote");
                                        if (!string.IsNullOrEmpty(mInitialAppActivity))
                                        {
                                            options.AddAdditionalCapability("appActivity", mInitialAppActivity);
                                        }
                                    }
                                    options.AddAdditionalCapability("automationName", "UIAutomator2");
                                    options.AddAdditionalCapability("newCommandTimeout", mobileDevice.CommandTimeout);
                                    break;
                                case "ios":
                                    options.AddAdditionalCapability("platformName", "iOS");
                                    options.AddAdditionalCapability("newCommandTimeout", mobileDevice.CommandTimeout);
                                    options.AddAdditionalCapability("wdaStartupRetries", 4);
                                    options.AddAdditionalCapability("wdaStartupRetryInterval", 1000);
                                    options.AddAdditionalCapability("nativeWebTap", true);
                                    options.AddAdditionalCapability("connectHardwareKeyboard", false);
                                    if (mobileDevice.Application.ToLower() != "safari")
                                    {
                                        options.AddAdditionalCapability("app", mobileDevice.Application);
                                    }
                                    break;
                                default:
                                    throw new Exception("Unsupported test browser. Browser: " + mobileDevice.MobileType);
                            }

                            options.AddAdditionalCapability(CapabilityType.IsJavaScriptEnabled, true);
                            options.AddAdditionalCapability("platformVersion", mobileDevice.DeviceVersion);
                            if (mobileDevice.IsEmulator)
                            {
                                options.AddAdditionalCapability("deviceName", STR_DEFAULT_FIRST_INSTANCE_EMULATOR);
                            }
                            else
                            {
                                options.AddAdditionalCapability("deviceName", mobileDevice.DeviceName);
                            }

                            // run on mobile browser -> need to set this capability before driver instantiation
                            if (mobileDevice.Application.ToLower() == "chrome")
                            {
                                //change automationName to "Appium" when run in mobile browser.
                                //"UIAutomator2" is unable to start mobile browser sessions.
                                mWebView = "CHROMIUM";
                                options.AddAdditionalCapability("automationName", "Appium");
                                options.AddAdditionalCapability("appPackage", "");
                                options.AddAdditionalCapability(CapabilityType.BrowserName, mobileDevice.Application);
                            }
                            else if (mobileDevice.Application.ToLower() == "safari")
                            {
                                mWebView = "WEBVIEW";
                                options.AddAdditionalCapability("nativeWebTap", false);
                                options.AddAdditionalCapability(CapabilityType.BrowserName, mobileDevice.Application);
                            }

                            // Instantiate Mobile driver based on OS info
                            if (mobileDevice.MobileType.ToLower() == "ios")
                            {
                                AutoDriver = new IOSDriver<AppiumWebElement>(new Uri(mobileDevice.MobileUrl), options, TimeSpan.FromSeconds(mobileDevice.CommandTimeout));
                                var driver = (IOSDriver<AppiumWebElement>)AutoDriver;
                                driver.HideKeyboard();
                            }
                            else
                            {
                                AutoDriver = new AndroidDriver<AppiumWebElement>(new Uri(mobileDevice.MobileUrl), options, TimeSpan.FromSeconds(mobileDevice.CommandTimeout));
                            }

                            // run on mobile browser -> need to revert to WebView
                            if (mobileDevice.Application.ToLower() == "safari"
                                || mobileDevice.Application.ToLower() == "chrome")
                            {
                                SetContext("WEBVIEW");
                                mIsMobile = false;
                                mIsMobileBrowser = true;
                                mBrowser = mobileDevice.Application;
                            }
                            else // run on mobile app -> need to compute for device dimensions
                            {
                                Size deviceScreenSize = new Size();

                                if (mobileDevice.MobileType.ToLower() == "ios") // iOS
                                {
                                    Thread.Sleep(DlkEnvironment.mMediumWaitMs);//add wait to avoid intermittent error in finding ios app 
                                    deviceScreenSize = AutoDriver.FindElement(By.XPath("//UIAApplication[1]")).Size;
                                }
                                else // Android
                                {
                                    deviceScreenSize = AutoDriver.Manage().Window.Size;
                                }

                                mDeviceHeight = deviceScreenSize.Height;
                                mDeviceWidth = deviceScreenSize.Width;
                                /* get status bar height */
                                mStatusBarHeight = GetStatusBarHeight();
                            }
                        }
                    }
                    break;
            }

            // default timeout settings
            if (!mIsMobile)
            {
                AutoDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 1);
                AutoDriver.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 1);
            }

        }

        public static void BackgroundApp(int Seconds)
        {
            if (mIsMobile)
            {
                ((AppiumDriver<AppiumWebElement>)AutoDriver).BackgroundApp(Seconds);
                return;
            }
        }

        /// <summary>
        /// Close the browser
        /// </summary>
        public static void CloseBrowser()
        {
            try
            {
                AutoDriver.Close();
            }
            catch
            {
                // Do nothing
            }
        }

        /// <summary>
        /// Kills WebDriver instance and closes all associated browser windows
        /// </summary>
        public static void CloseSession()
        {
            if (mIsMobile)
            {
                try
                {
                    ((AppiumDriver<AppiumWebElement>)AutoDriver).CloseApp();
                    ((AppiumDriver<AppiumWebElement>)AutoDriver).Quit();  
                    AutoDriver = null;
                }
                catch
                {
                    /* Do nothing */
                }
                return;
            }
            try
            {
                AutoDriver.Quit(); // quit nicely
                AutoDriver = null;
                Thread.Sleep(3000); // wait
                if (!mDoNotKillDriversOnTearDown)
                {
                    KillProcesses(new string[] { "IEDriverServer", "chromedriver", "geckodriver", "msedgedriver" });
                }
            }
            catch
            {
                /* Do nothing */
            }
        } 

        /// <summary>
        /// Navigate to the specified URL
        /// </summary>
        public static void GoToUrl(string Url, int PageLoadWaitInMs = mLongWaitMs)
        {
            AutoDriver.Url = Url;
            try
            {
                new WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(mLongWaitMs)).Until(
                    d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                
                /* Hard-coded Safari wait. SafariDriver has bug when Finding elements on unloaded page */
                if (mBrowser.ToLower() == "safari")
                {
                    Thread.Sleep(mMediumLongWaitMs);
                }
            }
            catch
            {
                /* Do nothing */
            }
        }

        /// <summary>
        /// For mobile browser only. Sets the context for Hybrid apps (between Native and WebView)
        /// </summary>
        public static void SetContext(string Context, bool LockContext=false)
        {
            if (mLockContext)
            {
                DlkLogger.LogInfo("Context is locked. Cannot change to '" + Context + "' context.");
                return;
            }
            if (LockContext)
            {
                DlkLogger.LogInfo("This is final context set. Context '" + Context + "' will be locked for the duration of this session.");
            }
            mLockContext = LockContext;
            if (mIsMobile)
            {
                /* Crosswalk fix */
                if (Context.Contains("WEBVIEW"))
                {
                    Context = mWebView;
                }

                AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)AutoDriver;

                //wait for webview to exists in contexts
                bool bFound = false;
                string contextName = "";

                for (int i = 0; i < 60; i++)
                {
                    IReadOnlyCollection<string> contexts = appiumDriver.Contexts;

                    foreach (string ctx in contexts)
                    //for (int j = 0; j < contexts.Count; j++)
                    {
                        if (ctx.Contains(Context))
                        //if (contexts[j].Contains(Context))
                        {
                            bFound = true;
                            //contextName = contexts[j];
                            contextName = ctx;
                            break;
                        }
                    }
                    if (bFound)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }

                }

                //switch to webview context
                if (bFound)
                    appiumDriver.Context = contextName;
            }
        }


        /// <summary>
        /// Kill Browsers using a bat file of process kill commands
        /// </summary>
        public static void KillBrowsers()
        {
            DlkProcess.RunProcess(DlkEnvironment.mDirTools + "KillBrowsers.bat", "", DlkEnvironment.mDirTools, true, 45);
            DlkLogger.LogInfo("Successfully executed KillBrowsers.bat");
        }

        /// <summary>
        /// kills named processes
        /// </summary>
        /// <param name="ProccessesToKill">exact names of process to kill</param>
        public static void KillProcesses(String[] ProccessesToKill)
        {
            foreach (string ProcName in ProccessesToKill)
            {
                KillProcessByName(ProcName);
            }
        }

        /// <summary>
        /// kills a single process using the exact name of the process
        /// </summary>
        /// <param name="ProcessName"></param>
        public static void KillProcessByName(string ProcessName)
        {
            //if (CloseProcessByName(ProcessName))
            //{
            //    return;
            //}
            try
            {
                List<Process> processes = new List<Process>(Process.GetProcesses());
                int killCount = 0;
                foreach (Process process in processes.FindAll(x => x.ProcessName == ProcessName))
                {
                    process.Kill();
                    DlkLogger.LogInfo("Killed process: " + ProcessName + " [" + ++killCount + "]");
                }
            }
            catch
            {
                DlkLogger.LogWarning("Couldn't kill process: " + ProcessName);
            }
        }

        /// <summary>
        /// Looks to see if the process exists.
        /// </summary>
        /// <param name="ProcessName">must be an exact match</param>
        /// <returns></returns>
        public static Boolean ProcessExists(string ProcessName)
        {
            List<Process> processes = new List<Process>(Process.GetProcesses());
            Boolean bExists = processes.Any(x => x.ProcessName == ProcessName);   
            return bExists;
        }

        public static void RunProcess(String Filename, String Arguments, String WorkingDir, Boolean bHideWin, int iTimeoutSec)
        {
            Process p = new Process();
            p.StartInfo.FileName = Filename;
            p.StartInfo.Arguments = Arguments;
            p.StartInfo.WorkingDirectory = WorkingDir;
            p.StartInfo.UseShellExecute = true;
            if (bHideWin)
            {
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            p.Start();
            if (iTimeoutSec > 0) // wait for a specific time and then kill
            {
                p.WaitForExit(iTimeoutSec * 1000);
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
            else if (iTimeoutSec == 0) // wait until finished - no timeout
            {
                p.WaitForExit();
            }
            else // if a negative number, don't wait
            {
                // don't wait
            }
        }


        public static void CaptureUrl()
        {
            LastUrl = AutoDriver.Url;
            existingWindowHandle = AutoDriver.CurrentWindowHandle;
        }

        public static void WaitUrlUpdate()
        {
            try
            {
                int i;
                for (i = 0; i < 20; i++)
                {
                    if (LastUrl != AutoDriver.Url)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (i >= 20)
                {
                    DlkLogger.LogWarning("WaitUrlUpdate() has timed out.");
                }
            }
            catch (InvalidOperationException invalid)
            {
                throw new InvalidOperationException(invalid.Message, invalid);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Exception of type {0} caught in WaitUrlUpdate method.", e.GetType()), e);
            }
        }

        public static void FocusNewWindowHandle()
        {
            existingWindowHandle = AutoDriver.CurrentWindowHandle;

            //get the current window handles 
            string popupHandle = existingWindowHandle;
            System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = AutoDriver.WindowHandles;

            foreach (string handle in windowHandles)
            {
                if (handle != existingWindowHandle)
                {
                    popupHandle = handle; break;
                }
            }

            //switch to new window 
            AutoDriver.SwitchTo().Window(popupHandle);
        }

        public static void SetBrowserFocusByUrl(String sUrl, int iTimeout)
        {
            Boolean bFound = false;

            try
            {
                for (int i = 0; i < iTimeout; i++)
                {
                    if (!PageUrlContains(sUrl))
                    {
                        DlkLogger.LogInfo(string.Format("{0} Initial Find", sUrl));
                        foreach (string sHandle in AutoDriver.WindowHandles)
                        {
                            DlkLogger.LogInfo(string.Format("Switching to window with handle {0}.", sHandle));
                            AutoDriver.SwitchTo().Window(sHandle);
                            if (PageUrlContains(sUrl))
                            {
                                DlkLogger.LogInfo(string.Format("{0} Found!", sUrl));
                                bFound = true;
                                break;
                            }

                        }
                    }
                    else
                    {
                        DlkLogger.LogInfo(string.Format("Current page contains {0}", sUrl));
                        bFound = true;
                        break;
                    }
                    if (bFound)
                    {
                        break;
                    }

                    DlkLogger.LogInfo(string.Format("Retrying to looking for {0}", sUrl));
                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Exception of type {0} caught in SetBrowserFocusByUrl method.", e.GetType()), e);
            }

            if (!bFound)
            {
                throw new Exception("SetBrowserFocusByUrl() failed. Browser with url '" + sUrl + "' not found.");
            }
        }

        public static void SetBrowserFocus(String sTitle, int iTimeout)
        {
            if (mIsMobile)
            {
                ((AppiumDriver<AppiumWebElement>)AutoDriver).LaunchApp();
                return;
            }
            Boolean bFound = false;
            for (int i = 0; i < iTimeout; i++)
            {
                if (!PageTitleContains(sTitle))
                {
                    foreach (string sHandle in AutoDriver.WindowHandles)
                    {
                        AutoDriver.SwitchTo().Window(sHandle);
                        if (PageTitleContains(sTitle))
                        {
                            bFound = true;
                            break;
                        }

                    }
                }
                else
                {
                    break;
                }
                if (bFound)
                {
                    break;
                }
            }

            if (!bFound)
            {
                throw new Exception("SetBrowserFocus() failed. Browser with title '" + sTitle + "' not found.");
            }
        }

        public static void EmptyFolder(string FolderName, string Pattern="")
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(FolderName);

                foreach (FileInfo fi in string.IsNullOrEmpty(Pattern) ? dir.GetFiles() : dir.GetFiles(Pattern))
                {
                    fi.Delete();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    EmptyFolder(di.FullName);
                    di.Delete();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(string.Format("Error clearing files on folder {0}", FolderName), ex);
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initialize browser capabilities
        /// </summary>
        private static void InitializeBrowserCapabilities()
        {
            try
            {
                if (mBrowser.ToLower() == "chrome")
                {
                    DlkBrowserCapabilityHandler browserCapability = new DlkBrowserCapabilityHandler();
                    mBrowserCapabilities = browserCapability.GetBrowserCapabilities();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }         
         }
        
        /// <summary>
        /// Initialize product-specific files/folders
        /// </summary>
        private static void InitializeProductPaths()
        {
            // validate product folder exists
            mDirProductsRoot = Path.Combine(mRootDir, "Products") + @"\";
            if (!Directory.Exists(mDirProductsRoot))
            {
                throw new Exception("Required products root directory does not exist: " + mDirProductsRoot);
            }

            mDirTools = Path.Combine(mRootDir, "Tools") + @"\";
            if (!Directory.Exists(mDirTools))
            {
                throw new Exception("Required Tools directory does not exist: " + mDirTools);
            }

            mDirProduct = Path.Combine(mDirProductsRoot, mProductFolder) + @"\";
            if (!Directory.Exists(mDirProduct))
            {
                throw new Exception("Required product directory does not exist: " + mDirProduct);
            }

            mDirFramework = Path.Combine(mDirProduct, "Framework") + @"\";
            if (!Directory.Exists(mDirFramework))
            {
                throw new Exception("Required Framework directory does not exist: " + mDirFramework);
            }

            DlkProductConfigHandler mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(mDirFramework, "Configs\\ProdConfig.xml"));

            mDirObjectStore = Path.Combine(mDirFramework, "ObjectStore") + @"\";
            if (!Directory.Exists(mDirObjectStore))
            {
                throw new Exception("Required Object Store directory does not exist: " + mDirObjectStore);
            }

            mDirRemoteBrowsers = Path.Combine(mDirFramework, "RemoteBrowsers") + @"\";
            if (!Directory.Exists(mDirRemoteBrowsers))
            {
                throw new Exception("Required Remote Browsers directory does not exist: " + mDirObjectStore);
            }

            mDirTestSuite = Path.Combine(mDirProduct, "Suites") + @"\";
            if (!Directory.Exists(mDirTestSuite))
            {
                throw new Exception("Required Suites directory does not exist: " + mDirTestSuite);
            }

            if (mProdConfigHandler.ConfigNodeExists("suitepath"))
            {
                string val = mProdConfigHandler.GetConfigValue("suitepath");
                if (val != null && Directory.Exists(val))
                {
                    mDirTestSuite = val.TrimEnd('\\') + "\\";
                }
            }

            mDirTests = Path.Combine(mDirProduct, "Tests") + @"\";
            if (!Directory.Exists(mDirTests))
            {
                throw new Exception("Required Tests directory does not exist: " + mDirTestSuite);
            }

            if (mProdConfigHandler.ConfigNodeExists("testpath"))
            {
                string val = mProdConfigHandler.GetConfigValue("testpath");
                if (val != null && Directory.Exists(val))
                {
                    mDirTests = val.TrimEnd('\\') + "\\";
                }
            }

            mDirUserData = Path.Combine(mDirProduct, "UserTestData") + @"\";
            if (!Directory.Exists(mDirUserData))
            {
                throw new Exception("Required UserTestData directory does not exist: " + mDirUserData);
            }

            mDirData = Path.Combine(mDirUserData, "Data") + @"\";
            if (!Directory.Exists(mDirData))
            {
                Directory.CreateDirectory(mDirData);
            }

            mDirDocDiff = Path.Combine(mDirUserData, "DocDiff") + @"\";
            if (!Directory.Exists(mDirDocDiff))
            {
                throw new Exception("Required DocDiff directory does not exist: " + mDirDocDiff);
            }

            mDirDocDiffActualFile = Path.Combine(mDirDocDiff, "ActualFile") + @"\";
            if (!Directory.Exists(mDirDocDiffActualFile))
            {
                throw new Exception("Required ActualFile directory does not exist: " + mDirDocDiffActualFile);
            }

            mDirDocDiffConfigFile = Path.Combine(mDirDocDiff, "ConfigFile") + @"\";
            if (!Directory.Exists(mDirDocDiffConfigFile))
            {
                throw new Exception("Required ConfigFile directory does not exist: " + mDirDocDiffConfigFile);
            }

            mDirDocDiffExpectedFile = Path.Combine(mDirDocDiff, "ExpectedFile") + @"\";
            if (!Directory.Exists(mDirDocDiffExpectedFile))
            {
                throw new Exception("Required ExpectedFile directory does not exist: " + mDirDocDiffExpectedFile);
            }

            mDirTestResults = Path.Combine(mDirFramework, "TestResults") + @"\";
            if (!Directory.Exists(mDirTestResults))
            {
                throw new Exception("Required TestResults directory does not exist: " + mDirTests);
            }

            mDirSuiteResults = Path.Combine(mDirFramework, "SuiteResults") + @"\";
            if (!Directory.Exists(mDirSuiteResults))
            {
                throw new Exception("Required SuiteResults directory does not exist: " + mDirTests);
            }

            mDirConfigs = Path.Combine(mDirFramework, "Configs") + @"\";
            if (!Directory.Exists(mDirConfigs))
            {
                throw new Exception("Required Configs directory does not exist: " + mDirTests);
            }

            mDirCommonConfigs = Path.Combine(mDirProductsRoot, @"Common\Configs\");
            if (!Directory.Exists(mDirCommonConfigs))
            {
                throw new Exception("Required Common configs directory does not exist: " + mDirCommonConfigs);
            }

            /* create folders for dashboard if not existing */

            mDirDataFrameworkDashboardRepository = Path.Combine(mDirFramework, @"DashboardRepository\");
            if (!Directory.Exists(mDirDataFrameworkDashboardRepository))
            {
                Directory.CreateDirectory(mDirDataFrameworkDashboardRepository);
            }

            mDirDataFrameworkDashboardRepositoryPublished = Path.Combine(mDirDataFrameworkDashboardRepository, @"Published\");
            if (!Directory.Exists(mDirDataFrameworkDashboardRepositoryPublished))
            {
                Directory.CreateDirectory(mDirDataFrameworkDashboardRepositoryPublished);
            }

            mDirDataFrameworkDashboardRepositoryWorking = Path.Combine(mDirDataFrameworkDashboardRepository, @"Working\");
            if (!Directory.Exists(mDirDataFrameworkDashboardRepositoryWorking))
            {
                Directory.CreateDirectory(mDirDataFrameworkDashboardRepositoryWorking);
            }

            // validate loginconfig file exists
            mLoginConfigFile = Path.Combine(mDirConfigs, @"LoginConfig.xml");

            if (!File.Exists(mLoginConfigFile))
            {
                throw new Exception("LoginConfig does not exist for product: " + mLoginConfigFile);
            }

            // validate remotebrowsers file exists
            mRemoteBrowsersFile = Path.Combine(mDirRemoteBrowsers, @"RemoteBrowsers.xml");

            if (!File.Exists(mRemoteBrowsersFile))
            {
                throw new Exception("RemoteBrowsers does not exist for product: " + mRemoteBrowsersFile);
            }

            // validate mobileconfig file exists
            mMobileConfigFile = Path.Combine(mDirRemoteBrowsers, @"MobileConfig.xml");
            if (!File.Exists(mMobileConfigFile))
            {
                throw new Exception("Mobile Configuration does not exist for product: " + mMobileConfigFile);
            }

            // Library - Tags
            mDirFrameworkLibraryTags = Path.Combine(mDirFramework, @"Library\Tags\");
            if (!Directory.Exists(mDirFrameworkLibraryTags))
            {
                Directory.CreateDirectory(mDirFrameworkLibraryTags);
            }

            mTagsFilePath = Path.Combine(mDirFrameworkLibraryTags, "tags.xml");
            if (!File.Exists(mTagsFilePath)) /* Force create Tags file if not exist */
            {
                File.WriteAllText(mTagsFilePath, "<tags />");
            }

            //OS Version List - lists the current supported versions for mobile
            mVersionSupportListPath = Path.Combine(mDirProductsRoot, @"Common\Configs\version_support.xml");
            if (!File.Exists(mVersionSupportListPath))
            {
                throw new Exception("Version support list does not exist.");
            }

            // Test Editor Help config files
            mHelpConfigFile = Path.Combine(DlkEnvironment.mDirConfigs + @"TestEditorHelp.xml");
            mCommonHelpConfigFile = Path.Combine(DlkEnvironment.mDirCommonConfigs, @"TestEditorHelp.xml");

            //Chromedriver path to be used exclusively for mobile browser testing
            if( File.Exists(Path.Combine(mDirTools, @"TestRunner\Mobile\chromedriver.exe")))
                mToolsChromeDriverPath = Path.Combine(mDirTools, @"TestRunner\Mobile\chromedriver.exe");
            else 
                mToolsChromeDriverPath = Path.Combine(mDirTools, @"TestRunner\chromedriver.exe");
        }

        public static int GetStatusBarHeight()
        {
            int ret = defaultStatusBarHeight;
            int maxThreshold = 10;
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)AutoDriver;

            if (mBrowser.ToLower() == "ios")
            {
                SetContext("NATIVE");
                var iosDriver = ((IOSDriver<AppiumWebElement>)AutoDriver);

                try
                {
                    var statusBarHeight = Convert.ToInt32(appiumDriver.GetSessionDetail("statBarHeight"));
                    
                    if(statusBarHeight >= 20)
                    {
                        ret = statusBarHeight;
                    }
                    else
                    {
                        AppiumWebElement statusBar = iosDriver.FindElementsByClassName("XCUIElementTypeStatusBar").FirstOrDefault();//get status bar element if there's any

                        if (statusBar == null)
                        {
                            AppiumWebElement viewPort = iosDriver.FindElementsByClassName("XCUIElementTypeOther").FirstOrDefault(x => (mDeviceHeight - x.Size.Height >= 20 && mDeviceHeight - x.Size.Height <= 40));//search and get viewport size if status bar is unavailable
                            ret = viewPort != null ? mDeviceHeight - viewPort.Size.Height : 20; //set to default if viewport is null
                        }
                        else
                        {
                            ret = statusBar.Size.Height;//set to default if status bar does not exist
                        }
                    }
                }
                catch
                {
                    
                }
            }
            else if (mBrowser.ToLower() == "android")
            {
                SetContext("NATIVE");

                for (int i = 0; i < maxThreshold; i++)
                {
                    try
                    {
                        ret = ((AppiumDriver<AppiumWebElement>)AutoDriver).FindElement(By.XPath("//*[contains(@resource-id,'ext-viewport')]")).Location.Y;
                        break;
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        //Do nothing
                    }
                    Thread.Sleep(1000);
                }
            }

            IReadOnlyCollection<string> contexts = appiumDriver.Contexts;

            foreach (string ctx in contexts)
            {
                if (ctx.Contains("WEBVIEW"))
                {
                    SetContext("WEBVIEW");
                    break;
                }
            }
            DlkLogger.LogInfo($"Status bar height set to {ret}px");

            return ret;
        }

        /// <summary>
        /// Get default browser name or index
        /// </summary>
        /// <param name="name">true/false</param>
        /// <returns></returns>
        public static string GetDefaultBrowserNameOrIndex(bool returnName)
        {
            if (returnName)
                return mAvailableBrowsers.FirstOrDefault(browser => browser.DefaultBrowser)?.Alias ?? "";
            else
                return mAvailableBrowsers.FindIndex(browser => browser.DefaultBrowser).ToString();
        }

        /// <summary>
        /// Retrieves the list of installed browsers on the machine
        /// </summary>
        private static List<DlkBrowser> GetAvailableBrowsers()
        {
            List<DlkBrowser> ret = new List<DlkBrowser>();

            try
            {
                var defaultBrowser = GetDefaultBrowser();
                var supportedBrowsers = new string[] { "firefox", "google chrome", "internet explorer" };
                /*Check for supported browsers installed */
                foreach (var supportedBrowser in supportedBrowsers)
                {
                    DlkBrowser browser = GetBrowserDetailsFromRegistry(supportedBrowser);
                    if (browser != null)
                    {
                        ret.Add(browser);
                    }
                }

                /*If firefox is not found in registry, try finding in another directory */
                if (!ret.Any(x => x.Name.ToLower().Contains("firefox")))
                {
                    try
                    {
                        string ffPath = RemoveQuotesFromPath((new FirefoxBinary()).ToString());
                        if (File.Exists(ffPath))
                        {
                            DlkBrowser mBrowser = new DlkBrowser()
                            {
                                Name = "Mozilla Firefox",
                                Alias = GetBrowserAlias("Mozilla Firefox"),
                                Path = ffPath,
                                Version = FileVersionInfo.GetVersionInfo(ffPath).FileVersion,
                                DriverVersion = GetDriverVersion(GetBrowserAlias("Mozilla Firefox"))
                            };
                            ret.Add(mBrowser);
                        }
                    }
                    catch
                    {
                        //Firefox not found, do nothing
                    }
                }

                /*If chrome is not found in registry, try finding in another directory */
                if (!ret.Any(x => x.Name.ToLower().Contains("google chrome")))
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Software\Google\Chrome") != null)
                    {
                        DlkBrowser mBrowser = new DlkBrowser()
                        {
                            Name = "Google Chrome",
                            Alias = GetBrowserAlias("Google Chrome"),
                            Path = "Unidentified",
                            Version = "Unidentified",
                            DriverVersion = GetDriverVersion(GetBrowserAlias("Google Chrome"))
                        };
                        ret.Add(mBrowser);
                    }
                }
#if DEBUG
                /* PhantomJS: Not supported for now.
                ret.Add(new DlkBrowser()
                    {
                        Name = "PhantomJS (Headless)",
                        Alias = "PhantomJS (Headless)",
                        Path = "-N/A-",
                        Version = "-N/A-"
                    }
                    );
                 */
                ret.Add(new DlkBrowser()
                {
                    Name = "Chrome (Headless)",
                    Alias = "Chrome (Headless)",
                    Path = "-N/A-",
                    Version = ret.Where(x => x.Name.ToLower().Contains("chrome")).Select(x => x.Version).Single(),
                    DriverVersion = ret.Where(x => x.Name.ToLower().Contains("chrome")).Select(x => x.DriverVersion).Single()
                }
                    );
#endif
#if EDGE_SUPPORT
                // Identify if machine currently uses edge chromium
                // No longer checks if OS is Win10 since edge chromium can run at any windows version
                if (UsesEdgeChromium(out string edgePath)) 
                {
                    DlkBrowser mBrowser = new DlkBrowser();
                    mBrowser.Name = "Edge";
                    mBrowser.Alias = GetBrowserAlias(mBrowser.Name);
                    mBrowser.Path = edgePath;
                    mBrowser.Version = FileVersionInfo.GetVersionInfo(mBrowser.Path).FileVersion;
                    mBrowser.DriverVersion = GetDriverVersion(mBrowser.Alias);
                    ret.Add(mBrowser);
                }
                else // If not then default to edge legacy
                {
                    /*Check if OS is Win 10. If yes, add Edge to browser list */
                    if (IsOSWindows10())
                    {
                        try
                        {
                            var reg = Registry.ClassesRoot.OpenSubKey(@"Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel\PackageRepository\Packages");
                            string[] mSubKeys = reg.GetSubKeyNames();

                            foreach (string subKey in mSubKeys)
                            {
                                if (subKey.StartsWith("Microsoft.MicrosoftEdge_"))
                                {
                                    DlkBrowser mBrowser = new DlkBrowser();
                                    mBrowser.Name = "Edge";
                                    mBrowser.Alias = GetBrowserAlias(mBrowser.Name);
                                    mBrowser.Path = "Unidentified";
                                    mBrowser.Version = "Unidentified";
                                    mBrowser.DriverVersion = GetDriverVersion(mBrowser.Alias);
                                    if (!ret.Exists(x => x.Name == "Edge")) //Microsoft Edge has 2 sub key entries in the registry
                                    {
                                        ret.Add(mBrowser);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            //Windows 10 not found in Registry, do nothing
                        }
                    }
                }
#endif  
                foreach (var browser in ret)
                {
                    if (browser.Alias == defaultBrowser)
                    {
                        browser.DefaultBrowser = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogError(ex);
            }

            return ret;
        }

        /// <summary>
        /// Get the default browser from Common config. Creates defaultbrowser node if not found
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultBrowser()
        {
            string browserConfigName = "defaultbrowser";
            if (DlkConfigHandler.ConfigExists(browserConfigName))
            {
                return DlkConfigHandler.GetConfigValue(browserConfigName);
            }
            else
                return "";
        }

        /// <summary>
        /// Removes extra characters from file path
        /// </summary>
        private static String RemoveQuotesFromPath(String text)
        {
            if (text.EndsWith("\"") && text.StartsWith("\""))
            {
                return text.Substring(1, text.Length - 2);
            }
            else if (text.StartsWith("FirefoxBinary("))
            {
                return text.Substring(14, text.Length - 15);
            }
            else
            {
                return text;
            }
        }

        /// <summary>
        /// Gets the browser alias
        /// </summary>
        private static String GetBrowserAlias(String browserName)
        {
            if (browserName.ToLower().Contains("firefox"))
            {
                return "Firefox";
            }
            if (browserName.ToLower().Contains("google chrome"))
            {
                return "Chrome";
            }
            if (browserName.ToLower().Contains("internet explorer"))
            {
                return "IE";
            }
            if (browserName.ToLower().Contains("edge"))
            {
                return "Edge";
            }
            return browserName;
        }

       private static String GetDriverVersion(String driver)
        {
            string ret = string.Empty;
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string tempFile = Path.Combine(path, Guid.NewGuid().ToString() + ".log");
            string driverPath = string.Empty;
            string targetToken = string.Empty;

            switch (driver.ToLower())
            {
                case "ie":
                    driverPath = "IEDriverServer.exe";
                    targetToken = "IEDriverServer.exe";
                    break;
                case "chrome":
                    driverPath = "chromedriver.exe";
                    targetToken = "ChromeDriver";
                    break;
                case "firefox":
                    driverPath = "geckodriver.exe";
                    targetToken = "geckodriver";
                    break;
                case "edge":
                    driverPath = "msedgedriver.exe";
                    targetToken = "MSEdgeDriver";
                    break;
                default:
                    throw new Exception("Unknown driver type");
            }

            DlkProcess.RunProcess("cmd", "/c " + driverPath + " --version > \"" + tempFile + "\"", path, true, 2);
            if (File.Exists(tempFile))
            {
                List<string> tokens = File.ReadAllLines(tempFile).First().Split().ToList();
                ret = tokens[tokens.IndexOf(targetToken) + 1];
                File.Delete(tempFile);
            }
            return ret;
        }

        /// <summary>
        /// Checks if the path is relative and gets the absolute path 
        /// </summary>
        /// <param name="path">Path of the application (.apk file) provided</param>
        /// <param name="product">Target product</param>
        /// <returns>Returns the absolute path of the apk file</returns>
        private static String GetAbsolutePath(string path, string product)
        {
            string mAbsolutePath = null;
            try
            {
                if (Directory.Exists(Directory.GetParent(path).ToString()) && File.Exists(path))
                {
                    mAbsolutePath = path;
                }
                else
                {
                    string binDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string mRootPath = Directory.GetParent(binDir).FullName;
                    while (new DirectoryInfo(mRootPath)
                        .GetDirectories().Count(x => x.FullName.Contains("Products")) == 0)
                    {
                        mRootPath = Directory.GetParent(mRootPath).FullName;
                    }

                    string mDirProductRoot = Path.Combine(mRootPath, @"Products\" + product + path);

                    if (Directory.Exists(Directory.GetParent(mDirProductRoot).ToString()) && File.Exists(mDirProductRoot))
                    {
                        //eventhough windows app accepts this condition, selenium on the other hand don't. Removing existing dot from the path
                        mAbsolutePath = mDirProductRoot.Contains("..") ? mDirProductRoot.Replace("..", string.Empty) : mDirProductRoot;
                    }
                    else
                    {
                        //Directory does not exist or the application file does not exist
                        throw new Exception("Couldn't find the file at " + path);
                    }
                }
            }
            catch
            {
                //catches exception if package path is empty
            }
            
            return mAbsolutePath;
        }

        /// <summary>
        /// Checks registry for installed browsers
        /// </summary>
        /// <param name="browserName"></param>
        /// <returns></returns>
        private static DlkBrowser GetBrowserDetailsFromRegistry(string browserName)
        {
            try
            {
                /*Determine whether the OS is 64bit/32bit and set the registry path */
                string regPath = @"SOFTWARE\Clients\StartMenuInternet"; //default for 32bit systems
                if (System.Environment.Is64BitOperatingSystem)
                    regPath = @"SOFTWARE\WOW6432Node\Clients\StartMenuInternet";

                RegistryKey mInstalledBrowsers = Registry.LocalMachine.OpenSubKey(regPath);
                string[] mBrowsers = mInstalledBrowsers.GetSubKeyNames();
                var selectedBrowser = mBrowsers.FirstOrDefault(x => mInstalledBrowsers.OpenSubKey(x).GetValue(null).ToString().ToLower().Contains(browserName));

                if (!string.IsNullOrEmpty(selectedBrowser))
                {
                    RegistryKey browserKey = mInstalledBrowsers.OpenSubKey(selectedBrowser);
                    RegistryKey browserPath = browserKey.OpenSubKey(@"shell\open\command");

                    DlkBrowser mBrowser = new DlkBrowser();
                    mBrowser.Name = (string)browserKey.GetValue(null);
                    mBrowser.Alias = GetBrowserAlias(mBrowser.Name);
                    mBrowser.Path = RemoveQuotesFromPath((string)browserPath.GetValue(null).ToString());
                    mBrowser.Version = !String.IsNullOrEmpty(mBrowser.Path) ?
                        FileVersionInfo.GetVersionInfo(mBrowser.Path).FileVersion :
                        "unknown";
                    mBrowser.DriverVersion = GetDriverVersion(mBrowser.Alias);
                    return mBrowser;
                }
            }
            catch
            {
                //do nothing if error occured
            }

            return null;
        }

        /// <summary>
        /// Closes a process
        /// </summary>
        /// <param name="ProcessName">must be an exact name match</param>
        /// <returns></returns>
        private static Boolean CloseProcessByName(string ProcessName)
        {
            Boolean bIsClosed = false;
            List<Process> processes = new List<Process>(Process.GetProcesses());
            int closeCount = 0;

            foreach (Process process in processes.FindAll(x => x.ProcessName == ProcessName))
            {
                try
                {
                    process.Close();
                    DlkLogger.LogInfo("Closed process: " + ProcessName + " [" + ++closeCount + "]");
                }
                catch
                {
                    // do nothing
                }
            }
            if (ProcessExists(ProcessName))
            {
                bIsClosed = false;
            }
            else
            {
                bIsClosed = true;
            }
            return bIsClosed;
        }

        private static Boolean PageUrlContains(String sUrl)
        {

            try
            {
                string trimmedUrl = sUrl;
                string http = "http://";
                string https = "https://";
                if (sUrl.StartsWith(http))
                {
                    trimmedUrl = sUrl.Remove(0, http.Count());
                }
                else if (sUrl.StartsWith(https))
                {
                    trimmedUrl = sUrl.Remove(0, https.Count());
                }
                return AutoDriver.Url.Contains(trimmedUrl);
            }
            catch (Exception e)
            {
                //ignore window not found. and let it timeout if window does not really exist
                if (e.Message.Contains("Window not found"))
                {
                    DlkLogger.LogInfo(string.Format("Window of {0} cannot be found. ", sUrl));
                    DlkLogger.LogInfo("Retrying...");
                    return false;
                }

                //ignore "Unable to get browser" error. Selenium does not
                //refresh its WindowHandles whenever a browser is closed 
                //by means other than Close() method
                //Example: close button within the page to close the whole browser
                if (!e.Message.Contains("Unable to get browser"))
                {
                    DlkLogger.LogInfo(e.Message);
                    DlkLogger.LogError(e);
                    DlkLogger.LogInfo("Retrying...");
                    return false;
                }
                else
                {
                    DlkLogger.LogInfo("Retrying...");
                    return false;
                }
            }
        }

        private static Boolean PageTitleContains(String sTitle)
        {

            try
            {
                return AutoDriver.Title.Contains(sTitle);
            }
            catch (Exception e)
            {
                //ignore window not found. and let it timeout if window does not really exist
                if (e.Message.Contains("Window not found"))
                {
                    return false;
                }

                //ignore "Unable to get browser" error. Selenium does not
                //refresh its WindowHandles whenever a browser is closed 
                //by means other than Close() method
                //Example: close button within the page to close the whole browser
                if (!e.Message.Contains("Unable to get browser"))
                {
                    DlkLogger.LogError(new Exception("SetBrowserFocus() failed. Error encountered while switching browser focuse."));
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Create driver instance
        /// </summary>
        /// <param name="BrowserName">Name of driver to create</param>
        /// <param name="ErrorMessage">Error container in case of error</param>
        /// <param name="HideDriver">Flag whether to display or hide driver console window</param>
        /// <param name="BrowserExtension">Full filepath of the browser extension</param>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private static bool CreateWebDriver(string BrowserName, out string ErrorMessage, bool HideDriver=false, string BrowserExtension="")
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {                
                switch (BrowserName.ToLower())
                {
                    case "edge":
                        if (UsesEdgeChromium(out string edgePath))
                        {
                            var options = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                            options.UseChromium = true;
                            options.BinaryLocation = edgePath;
                            options.AddArgument("use-fake-ui-for-media-stream");
                            if (HideDriver)
                            {
                                var svc = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
                                svc.HideCommandPromptWindow = true;
                                AutoDriver = new Microsoft.Edge.SeleniumTools.EdgeDriver(svc, options);
                            }
                            else
                            {
                                AutoDriver = new Microsoft.Edge.SeleniumTools.EdgeDriver(options);
                            }
                            AutoDriver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 60);
                            AutoDriver.Manage().Window.Maximize();
                            break;
                        }
                        else
                        {
                            OpenQA.Selenium.Edge.EdgeOptions options = new OpenQA.Selenium.Edge.EdgeOptions();
                            options.PageLoadStrategy = (PageLoadStrategy)OpenQA.Selenium.Edge.EdgePageLoadStrategy.Eager;
                            if (HideDriver)
                            {
                                var svc = OpenQA.Selenium.Edge.EdgeDriverService.CreateDefaultService();
                                svc.HideCommandPromptWindow = true;
                                AutoDriver = new OpenQA.Selenium.Edge.EdgeDriver(svc, options);
                            }
                            else
                            {
                                AutoDriver = new OpenQA.Selenium.Edge.EdgeDriver(options);
                            }
                            AutoDriver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 60);
                            AutoDriver.Manage().Window.Maximize();
                            break;
                        }
                    case "firefox":
                        FirefoxOptions ffo = new FirefoxOptions();
                        ffo.BrowserExecutableLocation = GetBrowserDetailsFromRegistry("firefox").Path;
                        /* unsupported yet by Maroinette (geckodriver) but default is Ignore as tested */
                        //ffo.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;
                        ffo.SetPreference("browser.fixup.alternate.enabled", false);
                        if (!IsUseDownloadDirProduct)
                        {
                            ffo.SetPreference("browser.download.useDownloadDir", false);
                        }
                        else
                        {
                            ffo.SetPreference("browser.download.useDownloadDir", true);
                            ffo.SetPreference("browser.helperApps.neverAsk.saveToDisk", string.Join(",", MimeTypes()));
                        }
                        ffo.AcceptInsecureCertificates = true;
                        if (HideDriver)
                        {
                            FirefoxDriverService svc = FirefoxDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                            svc.HideCommandPromptWindow = true;
                            AutoDriver = new FirefoxDriver(svc, ffo, TimeSpan.FromMilliseconds(mLongWaitMs));
                        }
                        else
                        {
                            AutoDriver = new FirefoxDriver(ffo);
                        }
                        AutoDriver.Manage().Window.Maximize();
                        break;
                    case "chrome":                        
                        List<DlkBrowserCapability> capabilities = mBrowserCapabilities.FindAll(f => f.Browser.ToLower() == BrowserName.ToLower());
                        ChromeOptions ChromeCapabilities = new ChromeOptions();
                        bool windowResized = false;

                        foreach (var capability in capabilities)
                        {
                            switch (capability.CapabilityType)
                            {
                                case DlkBrowserCapability.CHROME_ARGUMENT:
                                    if (capability.DisableNotificationArgument)
                                    {
                                        if (IsDisableNotifProduct)
                                        {
                                            ChromeCapabilities.AddArgument(capability.Argument);
                                        }
                                    }
                                    else
                                    {
                                        ChromeCapabilities.AddArgument(capability.Argument);
                                        if (capability.WindowSizeArgument)
                                        {
                                            windowResized = true;
                                        }
                                    }
                                    break;
                                case DlkBrowserCapability.CHROME_EXCLUDED_ARGUMENT:
                                    ChromeCapabilities.AddExcludedArgument(capability.Argument);
                                    break;
                                case DlkBrowserCapability.CHROME_ADDITIONAL_CAPABILITY:
                                    if (bool.TryParse(capability.Parameter, out bool capabilityParameter))
                                    {
                                        ChromeCapabilities.AddAdditionalCapability(capability.CapabilityName, capabilityParameter);
                                    }
                                    else
                                    {
                                        ChromeCapabilities.AddAdditionalCapability(capability.CapabilityName, capability.Parameter);
                                    }
                                    break;
                                case DlkBrowserCapability.CHROME_USER_PROFILE_PREFERENCE:
                                    if (bool.TryParse(capability.Parameter, out bool preferenceParameter))
                                    {
                                        ChromeCapabilities.AddUserProfilePreference(capability.CapabilityName, preferenceParameter);
                                    }
                                    else
                                    {
                                        ChromeCapabilities.AddUserProfilePreference(capability.CapabilityName, capability.Parameter);
                                    }                                    
                                    break;
                                default:
                                    break;
                            }
                        }

                        ChromeCapabilities.AcceptInsecureCertificates = true;
                        if (BrowserExtension != "")
                        {
                            ChromeCapabilities.AddExtension(BrowserExtension);
                        }
                        if (HideDriver)
                        {
                            ChromeDriverService svc = ChromeDriverService.CreateDefaultService();
                            svc.HideCommandPromptWindow = true;
                            AutoDriver = new ChromeDriver(svc, ChromeCapabilities);
                        }
                        else
                        {
                            AutoDriver = new ChromeDriver(ChromeCapabilities);
                        }

                        if (!windowResized)
                        {
                            AutoDriver.Manage().Window.Maximize();
                        }
                        break;
                    case "ie":
                        InternetExplorerOptions opt = new InternetExplorerOptions();
                        opt.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;
                        opt.EnablePersistentHover = false;
                        if (HideDriver)
                        {
                            var svc = InternetExplorerDriverService.CreateDefaultService();
                            svc.HideCommandPromptWindow = true;
                            AutoDriver = new InternetExplorerDriver(svc, opt);
                        }
                        else
                        {
                            AutoDriver = new InternetExplorerDriver(opt);
                        }
                        AutoDriver.Manage().Window.Maximize();
                        break;
                    /* PhantomJS: Not supported for now. Code kept here for reference.
                    case "phantomjs (headless)":
                        var options = new PhantomJSOptions();
                        string uaIE11 = "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko AppleWebKit/535.1 (KHTML, like Gecko)";
                        options.AddAdditionalCapability("phantomjs.page.settings.userAgent", uaIE11);
                        options.AddAdditionalCapability("phantomjs.page.settings.javascriptEnabled", "true");
                        AutoDriver = new PhantomJSDriver(options);
                        AutoDriver.Manage().Window.Maximize();
                        break;
                     */
                    case "chrome (headless)":
                        ChromeOptions ChromeHeadlessCapabilities = new ChromeOptions();
                        ChromeHeadlessCapabilities.AddArgument("headless");
                        //Chrome Headless must run in fixed size for now. Ideal dimension for Storm VMs is 1920x1080.
                        ChromeHeadlessCapabilities.AddArgument("window-size=1920x1080");
                        ChromeHeadlessCapabilities.AddArgument("disable-gpu");
                        ChromeHeadlessCapabilities.AddArgument("proxy-server='direct://'");
                        ChromeHeadlessCapabilities.AddArgument("proxy-bypass-list=*");
                        if (HideDriver)
                        {
                            ChromeDriverService svc = ChromeDriverService.CreateDefaultService();
                            svc.HideCommandPromptWindow = true;
                            AutoDriver = new ChromeDriver(svc, ChromeHeadlessCapabilities);
                        }
                        else
                        {
                            AutoDriver = new ChromeDriver(ChromeHeadlessCapabilities);
                        }
                        //AutoDriver.Manage().Window.Maximize();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Checks if the OS of the machine is Windows 10
        /// </summary>
        /// <returns>True if the OS is Windows 10 and False if not</returns>
        private static bool IsOSWindows10()
        {
            try
            {
                var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

                string productName = (string)reg.GetValue("ProductName");
                return productName.StartsWith("Windows 10");
            }
            catch
            {
                return false;
            }
        }        
        /// <summary>
        /// Identifies if machine uses edge chromium or edge legacy
        /// </summary>
        /// <returns>True if uses edge chromium; false for legacy</returns>
        private static bool UsesEdgeChromium(out string EdgeChromiumPath)
        {
            EdgeChromiumPath = "";
            try
            {
                string edgeChromiumPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Microsoft\Edge\Application\msedge.exe";
                EdgeChromiumPath = edgeChromiumPath;
                if (File.Exists(edgeChromiumPath))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// MIME type list for accepted file types to never ask to be saved
        /// </summary>
        private static string[] MimeTypes()
        {
            string[] mimeTypes = new string[]
            {
                "text/plain",
                "attachment/vnd.ms-excel",
                "text/csv",
                "application/csv",
                "text/comma-separated-values",
                "application/download",
                "application/octet-stream",
                "binary/octet-stream",
                "application/binary",
                "application/x-unknown",
                "application/excel",
                "attachment/csv",
                "attachment/excel",
                "application/vnd.ms-excel",
                "application/msexcel",
                "application/x-msexcel",
                "application/x-ms-excel",
                "application/x-excel",
                "application/x-dos_ms_excel",
                "application/xls",
                "application/x-xls",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
            return mimeTypes;
        }

        #endregion
    }
}
