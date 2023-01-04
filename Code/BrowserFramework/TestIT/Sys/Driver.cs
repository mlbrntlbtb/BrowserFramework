using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using CommonLib.DlkSystem;
using System.Reflection;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace TestIT.Sys
{
    public static class Driver
    {

        public enum Browser
        {
            IE,
            FIREFOX,
            CHROME,
            CHROME_HEADLESS,
            MOBILE
        }

        private static Browser mBrowser = Browser.CHROME_HEADLESS;

        public static string BrowserType
        {
            get { return GetBrowserTypeToWrite(mBrowser); }
        }

        public static Driver.Browser TargetBrowser
        {
            get
            {
                return mBrowser;
            }
            set
            {
                mBrowser = value;
            }
        }
        public static IWebDriver Instance { get; set; }
        public static Logger SessionLogger = new Logger();

        public static bool mIsMobile { get; set; }
        public static bool mIsMobileBrowser { get; set; }
        public static string mBrowserName { get; set; }
        public static int mDeviceWidth { get; set; }
        public static int mDeviceHeight { get; set; }
        public static string mAndroidHomePath = Environment.GetEnvironmentVariable("ANDROID_HOME");
        public static string mAdbPath = mAndroidHomePath + @"\platform-tools";
        public static String mEmulatorConfigFile = mAdbPath + "\\devices.txt";
        public static string mEmulatorPath { get; set; }
        private static string mWebView = "WEBVIEW";
        private static string mInitialAppActivity = string.Empty;
        /// <summary>
        /// Process used to start/terminate AVD (emulator)
        /// </summary>
        public const string STR_AVD_PROCESS = "qemu-system-i386";

        /// <summary>
        /// Copy files from one location to another
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="SourceFilePath"></param>
        /// <param name="DestinationFilePath"></param>
        /// <param name="OverwriteExisting"></param>
        public static void CopyFile(String FileName, String SourceFilePath, String DestinationFilePath, Boolean OverwriteExisting)
        {
            string sourceFile = Path.Combine(SourceFilePath, FileName);
            string destinationFile = Path.Combine(DestinationFilePath, FileName);
            try
            {
                File.Copy(sourceFile, destinationFile, OverwriteExisting);
            }
            catch (Exception e)
            {
                throw new Exception("CopyFile() failed : " + e.Message);
            }
        }

        private static void ClearReadOnly(string parentDirectory)
        {
            DirectoryInfo di = new DirectoryInfo(parentDirectory);
            di.Attributes = FileAttributes.Normal;

            foreach (string fileName in System.IO.Directory.GetFiles(parentDirectory))
            {
                FileInfo fileInfo = new System.IO.FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Normal;
            }

        }

        /// <summary>
        /// Assigns the correct driver to AutoDriver based on the browser type (firefox, ie, etc) and starts the browser
        /// </summary>
        /// <param name="HideDriver">Flag whether to display or hide driver console window</param>
        public static void StartBrowser(bool HideDriver = false, String mobileID = "", String ProductName= "")
        {
            Cursor.Position = new Point(0, 0);
            mIsMobile = false;
            mIsMobileBrowser = false;
            switch (TargetBrowser)
            {
                case Browser.FIREFOX:
                case Browser.CHROME:
                case Browser.IE:
                case Browser.CHROME_HEADLESS:
                    string error;
                    /* Implement a recovery logic, to try again once if first attempt to instantiate webdriver fails */
                    if (!CreateWebDriver(mBrowser, out error, HideDriver))
                    {
                        SessionLogger.WriteLine("WebDriver failed to initialize browser. Retrying...", Logger.MessageType.WRN);
                        CloseSession();
                        if (!CreateWebDriver(mBrowser, out error, HideDriver))
                        {
                            CloseSession();
                            throw new Exception("WebDriver could not recover from fatal error and exited with the following message: \""
                                + error + "\"");
                        }
                    }
                    break;
                case Browser.MOBILE:
                    if (mobileID != null)
                    {
                        int commandTimeout = 50000;
                        DlkEnvironment.mIsMobile = true;
                        MobileRecord mobileDev = MobileConfig.MobileRecords.First(x => x.MobileId == mobileID);
                        //copy latest apk
                        if (!Directory.Exists(Path.GetDirectoryName(mobileDev.Path)))
                        {
                            ClearReadOnly(Path.GetDirectoryName(mobileDev.Path));
                            if(File.Exists(mobileDev.Path))
                                File.Delete(mobileDev.Path);
                            CopyFile(Path.GetFileName(mobileDev.Path), mobileDev.APKSource, Path.GetDirectoryName(mobileDev.Path), true);
                            ClearReadOnly(Path.GetDirectoryName(mobileDev.Path));
                        }

                        
                        //Start up emulator first before launching Appium to prevent crash.
                        if (mobileDev.IsEmulator)
                        {
                            string mEmulatorFolder = Path.Combine(mAdbPath, "emulator") + @"\";
                            if (Directory.Exists(mEmulatorFolder))
                            {
                                mEmulatorPath = mEmulatorFolder;
                            }
                            else
                            {
                                mEmulatorPath = mAndroidHomePath + @"\tools";
                            }
                            //discard remaining instances of emulator to start a new one
                            KillProcessByName(STR_AVD_PROCESS);
                            KillProcessByName("adb");
                            SessionLogger.WriteLine("Starting emulator : " + mEmulatorPath + "- " + mobileDev.DeviceName, Logger.MessageType.INF);
                            RunProcess("emulator", "-avd " + mobileDev.DeviceName, mEmulatorPath, false, -1);
                            //programmatically wait AVDs here before proceeding to run Appium
                            for (int i = 0; i <= commandTimeout; i++)
                            {
                                if (File.Exists(mEmulatorConfigFile))
                                {
                                    RunProcess("cmd", "/c del devices.txt", mAdbPath, true, 0);
                                }

                                RunProcess("cmd", "/c adb devices > devices.txt", mAdbPath, true, 0);
                                String emulators = File.ReadAllText(mEmulatorConfigFile);
                                if (!emulators.Contains("emulator-5554") || emulators.Contains("offline"))
                                {
                                    DlkLogger.LogInfo("Waiting for emulator to load...");
                                    continue;
                                }
                                else
                                {
                                    Thread.Sleep(1000);
                                    DlkLogger.LogInfo("Emulator has loaded successfully!");
                                    SessionLogger.WriteLine("Emulator has loaded successfully!", Logger.MessageType.INF);
                                    Thread.Sleep(50000);
                                    
                                    //Pressing the lock button
                                    //RunProcess("cmd", "/c adb shell input keyevent 26", mAdbPath, false, 0); 
                                    //Swipe Up
                                    RunProcess("cmd","/c adb shell input touchscreen swipe 930 880 930 380", mAdbPath, true, 60);
                                    Thread.Sleep(20000);
                                    //Enter Passcode
                                    RunProcess("cmd", "/c adb shell input text 1234", mAdbPath, true, 60);
                                    Thread.Sleep(10000);
                                    //Presing OK/Check
                                    RunProcess("cmd", "/c adb shell input keyevent 66", mAdbPath, true, 60);
                                    Thread.Sleep(10000);
                                    break;
                                }
                            }
                        }

                        mIsMobile = true;
                        KillProcessByName("node");
                        KillProcessByName("chromedriver");

                        //Spawn Appium only if environment is Android. Does not apply to iOS since we are running tests on remote Mac.
                        if (mobileDev.MobileType.ToLower() == "android")
                        {
                            if (mobileDev.Application.ToLower() == "chrome" || ProductName == "MaconomyTouch")
                            {
                                string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                                string mRootPath = Directory.GetParent(binDir).FullName;
                                while (new DirectoryInfo(mRootPath).GetDirectories()
                                    .Where(x => x.FullName.Contains("Products")).Count() == 0)
                                {
                                    mRootPath = Directory.GetParent(mRootPath).FullName;
                                }
                                string mToolsChromeDriverPath = Path.Combine(mRootPath, @"Tools\TestRunner\chromedriver.exe");
                                RunProcess("appium", "--session-override --chromedriver-executable " + mToolsChromeDriverPath, "", false, -1);
                            }
                            else
                            {
                                RunProcess("appium", "--session-override", "", false, -1);
                            }
                        }

                        Thread.Sleep(20000); //approx. 20s wait

                        DriverOptions options;
                        mBrowserName = mobileDev.MobileType;
                        DlkEnvironment.mBrowser = mBrowserName;
                        switch (mobileDev.MobileType.ToLower())
                        {
                            case "android":
                                options = new AppiumOptions();
                                options.AddAdditionalCapability("platformName", "Android");
                                options.AddAdditionalCapability("setWebContentsDebuggingEnabled", true);
                                options.AddAdditionalCapability("unicodeKeyboard", true); // disable soft keyboard
                                options.AddAdditionalCapability("app", mobileDev.Path);
                                options.AddAdditionalCapability("appPackage", mobileDev.Application);
                                if (mobileDev.Application != "Chrome")
                                {
                                    options.AddAdditionalCapability("androidDeviceSocket", mobileDev.Application + "_devtools_remote");
                                    if (!string.IsNullOrEmpty(mInitialAppActivity))
                                    {
                                        options.AddAdditionalCapability("appActivity", mInitialAppActivity);
                                    }
                                }
                                options.AddAdditionalCapability("automationName", "UIAutomator2");
                                options.AddAdditionalCapability("newCommandTimeout", commandTimeout);
                                break;
                            case "ios":
                                options = new AppiumOptions();
                                options.AddAdditionalCapability("platformName", "iOS");
                                options.AddAdditionalCapability("nativeWebTap", true);
                                options.AddAdditionalCapability("app", mobileDev.Application);
                                options.AddAdditionalCapability("newCommandTimeout", "300");
                                options.AddAdditionalCapability("startIWDP", true);
                                break;
                            default:
                                throw new Exception("Unsupported test browser. Browser: " + mobileDev.MobileType);
                        }

                        options.AddAdditionalCapability(CapabilityType.IsJavaScriptEnabled, true);
                        options.AddAdditionalCapability("platformVersion", mobileDev.DeviceVersion);
                        if (mobileDev.IsEmulator)
                        {
                            options.AddAdditionalCapability("deviceName", "emulator-5554");
                        }
                        else
                        {
                            options.AddAdditionalCapability("deviceName", mobileDev.DeviceName);
                        }

                        // run on mobile browser -> need to set this capability before driver instantiation
                        if (mobileDev.Application.ToLower() == "chrome")
                        {
                            //change automationName to "Appium" when run in mobile browser.
                            //"UIAutomator2" is unable to start mobile browser sessions.
                            mWebView = "CHROMIUM";
                            options.AddAdditionalCapability("automationName", "Appium");
                            options.AddAdditionalCapability(CapabilityType.BrowserName, mobileDev.Application);
                        }
                        else if (mobileDev.Application.ToLower() == "safari")
                        {
                            mWebView = "WEBVIEW";
                            options.AddAdditionalCapability("nativeWebTap", false);
                            options.AddAdditionalCapability(CapabilityType.BrowserName, mobileDev.Application);
                        }

                        // Instantiate Mobile driver based on OS info
                        if (mobileDev.MobileType.ToLower() == "ios")
                        {
                            Instance = new IOSDriver<AppiumWebElement>(new Uri(mobileDev.MobileUrl), options);
                            DlkEnvironment.AutoDriver = Instance;
                        }
                        else
                        {
                            Instance = new AndroidDriver<AppiumWebElement>(new Uri(mobileDev.MobileUrl), options);
                            DlkEnvironment.AutoDriver = Instance;
                        }

                        // run on mobile browser -> need to revert to WebView
                        if (mobileDev.Application.ToLower() == "safari"
                            || mobileDev.Application.ToLower() == "chrome")
                        {
                            SetContext("WEBVIEW");
                            mIsMobile = false;
                            mIsMobileBrowser = true;
                            mBrowserName = mobileDev.Application;
                        }
                        else // run on mobile app -> need to compute for device dimensions
                        {
                            Size deviceScreenSize = new Size();

                            if (mobileDev.MobileType.ToLower() == "ios") // iOS
                            {
                                deviceScreenSize = Instance.FindElement(By.XPath("//UIAApplication[1]")).Size;
                            }
                            else // Android
                            {
                                deviceScreenSize = Instance.Manage().Window.Size;
                            }

                            mDeviceHeight = deviceScreenSize.Height;
                            mDeviceWidth = deviceScreenSize.Width;
                            DlkEnvironment.mDeviceHeight = mDeviceHeight;
                            DlkEnvironment.mDeviceWidth = mDeviceWidth;
                        }
                    }
                    else
                    {
                        throw new Exception("No record retrieved. Mobile ID cannot be null.");
                    }
                    break;
                default:
                    break;
            }

            // default timeout settings
            if (!mIsMobile)
            {
                Instance.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 1);
                Instance.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 1);
            }
        }

        public static void CaptureScreenshot(string FileName)
        {
            try
            {
                ((ITakesScreenshot)Instance).GetScreenshot().SaveAsFile(FileName, ScreenshotImageFormat.Png);
            }
            catch
            {
                // swallow
            }
        }

        public static void SetContext(string Context)
        {
            if (mIsMobile)
            {
                /* Crosswalk fix */
                if (Context.Contains("WEBVIEW"))
                {
                    Context = mWebView;
                }

                AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)Instance;

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


        /// <summary>
        /// Checks registry for installed browsers
        /// </summary>
        /// <param name="browserName"></param>
        /// <returns></returns>
        private static string GetBrowserPathFromRegistry(string browserName)
        {
            string ret = string.Empty;
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


                    ret = RemoveQuotesFromPath((string)browserPath.GetValue(null).ToString());
                }
            }
            catch
            {
                //do nothing if error occured
            }

            return null;
        }

        /// <summary>
        /// Removes extra characters from file path
        /// </summary>
        private static string RemoveQuotesFromPath(string text)
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
        /// Create driver instance
        /// </summary>
        /// <param name="BrowserName">Name of driver to create</param>
        /// <param name="ErrorMessage">Error container in case of error</param>
        /// <param name="HideDriver">Flag whether to display or hide driver console window</param>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private static bool CreateWebDriver(Browser BrowserToCreate, out string ErrorMessage, bool HideDriver = false)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {
                switch (BrowserToCreate)
                {
                    case Browser.FIREFOX:
                        FirefoxOptions ffo = new FirefoxOptions();
                        ffo.BrowserExecutableLocation = GetBrowserPathFromRegistry("firefox");
                        /* unsupported yet by Maroinette (geckodriver) but default is Ignore as tested */
                        //ffo.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;
                        ffo.SetPreference("browser.fixup.alternate.enabled", false);
                        ffo.SetPreference("browser.download.useDownloadDir", false);
                        if (HideDriver)
                        {
                            FirefoxDriverService svc = FirefoxDriverService.CreateDefaultService();
                            svc.HideCommandPromptWindow = true;
                            Instance = new FirefoxDriver(svc, ffo, new TimeSpan(0, 0, 1));
                        }
                        else
                        {
                            Instance = new FirefoxDriver(ffo);
                        }
                        Instance.Manage().Window.Maximize();
                        break;
                    case Browser.CHROME:
                        ChromeOptions ChromeCapabilities = new ChromeOptions();
                        ChromeCapabilities.AddArgument("--test-type");
                        ChromeCapabilities.AddArgument("--start-maximized=true");
                        ChromeCapabilities.AddArgument("disable-infobars");
                        if (HideDriver)
                        {
                            ChromeDriverService svc = ChromeDriverService.CreateDefaultService();
                            svc.HideCommandPromptWindow = true;
                            Instance = new ChromeDriver(svc, ChromeCapabilities);
                        }
                        else
                        {
                            Instance = new ChromeDriver(ChromeCapabilities);
                        }
                        Instance.Manage().Window.Maximize();
                        break;
                    case Browser.IE:
                        InternetExplorerOptions opt = new InternetExplorerOptions();
                        opt.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;
                        opt.EnablePersistentHover = false;
                        if (HideDriver)
                        {
                            var svc = InternetExplorerDriverService.CreateDefaultService();
                            svc.HideCommandPromptWindow = true;
                            Instance = new InternetExplorerDriver(svc, opt);
                        }
                        else
                        {
                            Instance = new InternetExplorerDriver(opt);
                        }
                        Instance.Manage().Window.Maximize();
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
                    case Browser.CHROME_HEADLESS:
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
                            Instance = new ChromeDriver(svc, ChromeHeadlessCapabilities);
                        }
                        else
                        {
                            Instance = new ChromeDriver(ChromeHeadlessCapabilities);
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
        /// Kills WebDriver instance and closes all associated browser windows
        /// </summary>
        public static void CloseSession()
        {
            try
            {
                Instance.Quit(); // quit nicely
                Instance = null;
                Thread.Sleep(3000); // wait
            }
            catch
            {
                /* Do nothing */
            }
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
            try
            {
                List<Process> processes = new List<Process>(Process.GetProcesses());
                int killCount = 0;
                foreach (Process process in processes.FindAll(x => x.ProcessName == ProcessName))
                {
                    process.Kill();
                    SessionLogger.WriteLine("Killed process: " + ProcessName + " [" + ++killCount + "]", Logger.MessageType.INF);
                }
            }
            catch
            {
                SessionLogger.WriteLine("Couldn't kill process: " + ProcessName, Logger.MessageType.WRN);
            }
        }

        private static string GetBrowserTypeToWrite(Browser BrowserType)
        {
            string ret = string.Empty;
            switch (BrowserType)
            {
                case Browser.CHROME:
                    ret = "chrome";
                    break;
                case Browser.FIREFOX:
                    ret = "firefox";
                    break;
                case Browser.IE:
                    ret = "ie";
                    break;
                default:
                    break;
            }
            return ret;
        }
        }
}
