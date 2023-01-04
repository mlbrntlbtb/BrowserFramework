using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using OpenQA.Selenium.Firefox;
using System.Xml;

namespace TRDiagnosticsCore.Tests
{
    public class BrowserTest : DiagnosticTest
    {
        public BrowserTest() : base() { }

        private static List<DlkBrowser> mAvailableBrowsersList = null;
        private const string mUserRoot = "HKEY_CURRENT_USER";
        private static bool isEdgeLegacyInstalled = false;
        private static string TRPath = "";
        private static string TRRootPath = "";
        private string genericExternalReleaseMessage = "It is recommended to repair or re-install Test Runner using the provided installer. If this issue persists after repair, please submit a report to deltektestrunner@deltek.com";

        private static string[] supportedBrowsers = new[]
        {
                "Mozilla Firefox",
                "Google Chrome",
                "Internet Explorer",
                "Edge"
        };

        /// <summary>
        /// This is the list of installed browsers on the machine
        /// </summary>
        private List<DlkBrowser> mAvailableBrowsers
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

        protected override void DefineTestName()
        {
            TotalTestCount = 2;
            TestName = "Installed Browser and WebDriver Versions Check";
        }

        protected override void PerformCheck(out string ErrorMessage)
        {
            try
            {
                ErrorMessage = string.Empty;

                TRPath = TestRunnerPath;
                TRRootPath = GetRootPath(TRPath);

                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for installed browsers");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for installed browsers...");
                CheckInstalledBrowsers();

                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Internet Explorer settings");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Internet Explorer settings...");
                UpdateRegistrySettings();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                throw new Exception(e.Message);
            }
        }

        protected override LogRecord IdentifyRecommendation(LogRecord Record)
        {
            LogRecord ret = new LogRecord(Logger.MessageType.NULL, "No recommendation available.");
            string browserURL = string.Empty;
            string driverURL = string.Empty;
            string latestSupportedVer = string.Empty;

            switch (Record.MessageType)
            {
                case Logger.MessageType.WARNING:
                    if (Record.MessageDetails.Contains("not installed"))
                    {
                        if (Record.MessageDetails.ToLower().Contains("firefox"))
                        {
                            browserURL = "https://www.mozilla.org/en-US/firefox/windows/";
                        }
                        if (Record.MessageDetails.ToLower().Contains("google chrome"))
                        {
                            browserURL = "https://www.google.com/chrome/";
                        }
                        if (Record.MessageDetails.ToLower().Contains("internet explorer"))
                        {
                            browserURL = "https://www.microsoft.com/en-us/download/internet-explorer.aspx";
                        }
                        if (Record.MessageDetails.ToLower().Contains("edge"))
                        {
                            browserURL = "https://www.microsoft.com/en-us/edge";
                        }
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is recommended to download the browser here: " + browserURL + "\n" + "Install it if you would like to perform test runs using this browser.");
                    }

                    if (Record.MessageDetails.Contains("Legacy version of Edge") && isEdgeLegacyInstalled)
                    {
                        browserURL = "https://www.microsoft.com/en-us/edge";
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is recommended to update to the newer version of Edge based on Chromium. You may download the browser here: " + browserURL + "\n" + "Install it if you would like to perform test runs using this browser.");
                    }

                    if (Record.MessageDetails.Contains("does not match with the currently supported version"))
                    {
                        if (Record.MessageDetails.ToLower().Contains("geckodriver"))
                        {
                            driverURL = "https://github.com/mozilla/geckodriver/releases";
                        }
                        if (Record.MessageDetails.ToLower().Contains("chromedriver"))
                        {
                            driverURL = "https://chromedriver.chromium.org/downloads";
                        }
                        if (Record.MessageDetails.ToLower().Contains("iedriver"))
                        {
                            driverURL = "https://selenium-release.storage.googleapis.com/index.html";
                        }
                        if (Record.MessageDetails.ToLower().Contains("edgedriver"))
                        {
                            driverURL = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/";
                        }
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is recommended to update your driver version to the supported version. You can download the webdriver here: " + driverURL);
                    }

                    if (Record.MessageDetails.Contains("exceeded supported browser version"))
                    {
                        if (Record.MessageDetails.ToLower().Contains("firefox"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("firefox");
                        }
                        if (Record.MessageDetails.ToLower().Contains("chrome"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("chrome");
                        }
                        if (Record.MessageDetails.ToLower().Contains("internet explorer"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("ie");
                        }
                        if (Record.MessageDetails.ToLower().Contains("edge"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("edge");
                        }
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is suggested to downgrade to a currently supported version of " + latestSupportedVer + ".");
                    }

                    if (Record.MessageDetails.Contains("Scaling and Layout property is set to"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Manually set Internet Explorer zoom level to 100% so that automated tests will run as expected. Open Internet Explorer. Go to Settings > Zoom and select 100%.");
                    }
                    break;
                case Logger.MessageType.ERROR:
                    if (Record.MessageDetails.Contains("was encountered during"))
                    {                       
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Check if your user account has registry access.");
                    }

                    if (Record.MessageDetails.Contains("Version support file does not exist"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Check the \\BrowserFramework\\Products\\Common\\Configs folder to check if the version_support.xml file exists.");
                    }

                    if (Record.MessageDetails.Contains("is an outdated version"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Please get the latest version_support.xml file and rerun your diagnostic check.");
                    }

                    if (Record.MessageDetails.Contains("is incompatible with"))
                    {
                        if (Record.MessageDetails.ToLower().Contains("firefox"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("firefox");
                        }
                        if (Record.MessageDetails.ToLower().Contains("chrome"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("chrome");
                        }
                        if (Record.MessageDetails.ToLower().Contains("internet explorer"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("ie");
                        }
                        if (Record.MessageDetails.ToLower().Contains("edge"))
                        {
                            latestSupportedVer = GetLatestSupportedVersion("edge");
                        }
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is suggested to upgrade to a currently supported version of " + latestSupportedVer + ".");
                    }

                    if (Record.MessageDetails.Contains("does not exist in the specified"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"Kindly get the latest files from the \Tools\TestRunner folder in TFS." : genericExternalReleaseMessage);
                    }
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Method to check the installed browsers
        /// </summary>
        private void CheckInstalledBrowsers()
        {
            foreach (DlkBrowser browser in mAvailableBrowsers)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, browser.Name + " " + browser.Version + " is installed.");
                CheckIfBrowserVersionIsSupported(browser);
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, browser.DriverName.Remove(browser.DriverName.Length-4,4) + " version: " + browser.DriverVersion);
                CheckBrowserDriverVersion(browser);
            }

            if (mAvailableBrowsersList.Count != supportedBrowsers.Length)
            {
                foreach (string s in supportedBrowsers)
                {
                    if (!mAvailableBrowsers.Any(x => x.Name.ToLower().Contains(s.ToLower())))
                        if (s == "Edge" && isEdgeLegacyInstalled)
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "Legacy version of Edge is detected. This version is not supported by Test Runner.");
                        }
                        else
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.WARNING, s + " is not installed in this machine.");
                        }
                }
            }
        }

        /// <summary>
        /// Retrieves the list of installed browsers on the machine
        /// </summary>
        private List<DlkBrowser> GetAvailableBrowsers()
        {
            List<DlkBrowser> ret = new List<DlkBrowser>();

            try
            {
                /*Check for supported browsers installed */
                foreach (string supportedBrowser in supportedBrowsers)
                {
                    DlkBrowser browser = GetBrowserDetailsFromRegistry(supportedBrowser.ToLower());
                    if (browser != null)
                    {
                        ret.Add(browser);
                    }
                }

                // Check for Edge browser
                // No longer checks if OS is Win10 since edge chromium can run at any windows version
                if (!ret.Any(x => x.Name.ToLower().Contains("edge")))
                {
                    string edgeChromiumPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Microsoft\Edge\Application\msedge.exe";
                    if (File.Exists(edgeChromiumPath))
                    {
                        DlkBrowser mBrowser = new DlkBrowser();
                        mBrowser.Name = "Microsoft Edge";
                        mBrowser.Alias = "Edge";
                        mBrowser.Path = edgeChromiumPath;
                        mBrowser.Version = FileVersionInfo.GetVersionInfo(mBrowser.Path).FileVersion;
                        mBrowser.DriverName = "msedgedriver.exe";
                        mBrowser.DriverVersion = GetDriverVersion(mBrowser.Alias);
                        ret.Add(mBrowser);
                    }
                }

                // Check if Edge Legacy is installed in the machine. Check first if OS is Win 10.
                if (IsOSWindows10())
                { 
                    if (!ret.Any(x => x.Name.ToLower().Contains("edge")))
                    {
                        var regKey = Registry.ClassesRoot.OpenSubKey(@"Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel\PackageRepository\Packages");
                        string[] mSubKeys = regKey.GetSubKeyNames();

                        foreach (string subKey in mSubKeys)
                        {
                            if (subKey.StartsWith("Microsoft.MicrosoftEdge_"))
                            {
                                DlkBrowser mBrowser = new DlkBrowser();
                                string[] edgeInfo = subKey.Split('_');
                                mBrowser.Name = "Edge";
                                mBrowser.Alias = GetBrowserAlias(mBrowser.Name);
                                mBrowser.Path = "Unidentified";
                                mBrowser.Version = edgeInfo[1];
                                mBrowser.DriverVersion = GetDriverVersion(mBrowser.Alias);
                                if (!String.IsNullOrEmpty(subKey))
                                {
                                    isEdgeLegacyInstalled = true;
                                }
                            }
                        }
                    }
                }

                // If firefox is not found in registry, try finding in another directory
                if (!ret.Any(x => x.Name.ToLower().Contains("firefox")))
                {
                    try
                    {
                        string ffPath = RemoveQuotesFromPath(new FirefoxBinary().ToString());
                        if (File.Exists(ffPath))
                        {
                            DlkBrowser mBrowser = new DlkBrowser()
                            {
                                Name = "Mozilla Firefox",
                                Alias = "Firefox",
                                Path = ffPath,
                                Version = FileVersionInfo.GetVersionInfo(ffPath).FileVersion,
                                DriverName = "geckodriver.exe",
                                DriverVersion = GetDriverVersion("Firefox")
                            };
                            ret.Add(mBrowser);
                        }
                    }
                    catch
                    {
                        //Firefox not found, do nothing
                    }
                }

                // If chrome is not found in registry, try finding in another directory
                if (!ret.Any(x => x.Name.ToLower().Contains("google chrome")))
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Software\Google\Chrome") != null)
                    {
                        var reg = Registry.CurrentUser.OpenSubKey(@"Software\Google\Chrome\BLBeacon");
                        DlkBrowser mBrowser = new DlkBrowser()
                        {
                            Name = "Google Chrome",
                            Alias = "Chrome",
                            Path = "Unidentified",
                            DriverName = "chromedriver.exe",
                            Version = !String.IsNullOrEmpty(reg.GetValue("version").ToString()) ? reg.GetValue("version").ToString() : "Unidentified",
                            DriverVersion = GetDriverVersion("Chrome")
                        };
                        ret.Add(mBrowser);
                    }
                }            
            }
            catch (Exception)
            {
                //Do nothing
            }

            return ret;
        }

        /// <summary>
        /// Populates the browser object details from Registry
        /// </summary>
        /// <param name="browserName">Name of browser</param>
        /// <returns>DlkBrowser object with details</returns>
        private DlkBrowser GetBrowserDetailsFromRegistry(string browserName)
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
                    mBrowser.Path = RemoveQuotesFromPath((string)browserPath.GetValue(null).ToString());
                    mBrowser.Alias = GetBrowserAlias(mBrowser.Name);
                    mBrowser.Version = !String.IsNullOrEmpty(mBrowser.Path) ? FileVersionInfo.GetVersionInfo(mBrowser.Path).FileVersion : "Unidentified";
                    mBrowser.DriverName = GetBrowserDriverName(mBrowser.Name);                                                                                                 
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
        /// Removes extra characters from file path
        /// </summary>
        private string RemoveQuotesFromPath(string text)
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
        /// Retrieves the driver version
        /// </summary>
        /// <param name="driver">Driver alias</param>
        /// <returns>browser driver version</returns>
        private string GetDriverVersion(string driver)
        {
            string ret = string.Empty;
            string path = TRPath;
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
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "cmd";
                proc.StartInfo.Arguments = "/c " + driverPath + " --version > \"" + tempFile + "\"";
                proc.StartInfo.WorkingDirectory = path;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                proc.Start();

                proc.WaitForExit(2 * 1000);
                if (!proc.HasExited)
                {
                    proc.Kill();
                }

                if (File.Exists(tempFile))
                {
                    List<string> tokens = File.ReadAllLines(tempFile).First().Split().ToList();
                    ret = tokens[tokens.IndexOf(targetToken) + 1];
                    File.Delete(tempFile);
                }
            }
            catch
            {
                //Do nothing
            }
            return ret;
        }

        /// <summary>
        /// Gets the browser alias
        /// </summary>
        private string GetBrowserAlias(string browserName)
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

        /// <summary>
        /// Gets the browser driver name
        /// </summary>
        private static string GetBrowserDriverName(string browserName)
        {
            if (browserName.ToLower().Contains("firefox"))
            {
                return "geckodriver.exe";
            }
            if (browserName.ToLower().Contains("google chrome"))
            {
                return "chromedriver.exe";
            }
            if (browserName.ToLower().Contains("internet explorer"))
            {
                return "IEDriverServer.exe";
            }
            if (browserName.ToLower().Contains("edge"))
            {
                return "msedgedriver.exe";
            }
            return browserName;
        }

        /// <summary>
        /// Checks if the OS of the machine is Windows 10
        /// </summary>
        /// <returns>True if the OS is Windows 10 and False if not</returns>
        private bool IsOSWindows10()
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
        /// Perform IE Registry settings update
        /// </summary>
        private void UpdateRegistrySettings()
        {
            UpdateEnableProtectedMode();
            SetZoomLevel();
            CheckMachineScaleAndLayout();
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

                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "Internet security enabled protected mode settings was successfully set.");
            }
            catch (Exception e)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, e + " was encountered during update of Internet security enabled protected mode settings");
            }
        }

        /// <summary>
        /// Update the Zoom Level setting.
        /// </summary>
        private void SetZoomLevel()
        {
            const string subKey = "Software\\Microsoft\\Internet Explorer\\Zoom";
            string IEVersion = string.Empty;

            try
            {
                /*Determine whether the OS is 64bit/32bit and set the registry path */
                string regPath = @"SOFTWARE\Clients\StartMenuInternet"; //default for 32bit systems
                if (System.Environment.Is64BitOperatingSystem)
                    regPath = @"SOFTWARE\WOW6432Node\Clients\StartMenuInternet";

                RegistryKey mInstalledBrowsers = Registry.LocalMachine.OpenSubKey(regPath);
                string[] mBrowsers = mInstalledBrowsers.GetSubKeyNames();
                var IEBrowser = mBrowsers.FirstOrDefault(x => mInstalledBrowsers.OpenSubKey(x).GetValue(null).ToString().ToLower().Contains("internet explorer"));

                if (!string.IsNullOrEmpty(IEBrowser))
                {
                    RegistryKey IEbrowserKey = mInstalledBrowsers.OpenSubKey(IEBrowser);
                    RegistryKey IEbrowserPath = IEbrowserKey.OpenSubKey(@"shell\open\command");
                    string IEPath = RemoveQuotesFromPath((string)IEbrowserPath.GetValue(null).ToString());
                    IEVersion = !String.IsNullOrEmpty(IEPath) ? FileVersionInfo.GetVersionInfo(IEPath).FileVersion : "Unidentified";
                }

                if (IEVersion.ToString().StartsWith("11"))
                {
                    const string zoomLevelKeyName = mUserRoot + "\\" + subKey;
                    Registry.SetValue(zoomLevelKeyName, "ZoomFactor", 0x000186a0, RegistryValueKind.DWord);
                }
                else
                {
                    const string zoomLevelKeyName = mUserRoot + "\\" + subKey;
                    Registry.SetValue(zoomLevelKeyName, "ZoomFactor", 100000, RegistryValueKind.DWord);
                }

                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "Internet Explorer zoom level settings was successfully set.");
            }
            catch (Exception e)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, e + " was encountered during setting of Internet Explorer zoom level");
            }
        }
        
        /// <summary>
        /// Checks the machine's scale and layout wherein the IE zoom level is dependent on
        /// </summary>
        private void CheckMachineScaleAndLayout()
        {
            string dpiValue = string.Empty;

            try
            {
                // Get DPI setting.
                RegistryKey dpiRegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
                int dpi = (int)dpiRegistryKey.GetValue("AppliedDPI");
                switch (dpi)
                {
                    case 96: // Small / 100%
                        dpiValue = "100%";
                        break;
                    case 120: // Medium / 125%
                        dpiValue = "125%";
                        break;
                    case 144: // Large / 150%
                        dpiValue = "150%";
                        break;
                }
                if (dpi > 96)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "Scaling and Layout property is set to " + dpiValue + ". This may affect Internet Explorer zoom Level.");
                }
            }
            catch (Exception e)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, e + " was encountered during checking of Scale and Layout");
            }
        }

        /// <summary>
        /// Checks the compatibility of the browser driver to the supported version 
        /// </summary>
        /// <param name="browser">DlkBrowser object</param>
        private void CheckBrowserDriverVersion(DlkBrowser browser)
        {
            string mVersionSupportListPath = Path.Combine(TRRootPath, @"Products\Common\Configs\version_support.xml");
            if (File.Exists(mVersionSupportListPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mVersionSupportListPath);

                XmlNode ver = doc.SelectSingleNode("versions /browser[@name = '" + browser.Alias.ToLower() + "']");
                if (ver != null)
                {
                    if (!String.IsNullOrEmpty(browser.DriverVersion))
                    {
                        string deployedVer = ver.Attributes["driver"].Value.ToString();
                        List<string> supportedVersions = ver.Attributes["supportedversions"].Value.ToString().Split(',').ToList();

                        if (browser.Alias.ToLower() != "edge")
                        {
                            int startIdx = browser.Version.Contains(".") ? browser.Version.IndexOf(".") : browser.Version.Length;
                            string mainVer = browser.Version.Remove(startIdx);

                            if (browser.DriverVersion == deployedVer && supportedVersions.Contains(mainVer))
                            {
                                DiagnosticLogger.LogResult(Logger.MessageType.INFO, browser.Name + " is compatible with " + browser.DriverName.Remove(browser.DriverName.Length - 4, 4) + " version.");
                            }
                            else
                            {
                                if (browser.DriverVersion != deployedVer)
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, browser.DriverName + " does not match with the currently supported version " + deployedVer + " and may result in errors when executing automated tests.");
                                }
                            }
                        }
                        else
                        {
                            if (browser.DriverVersion == deployedVer && browser.Version == supportedVersions[0])
                            {
                                DiagnosticLogger.LogResult(Logger.MessageType.INFO, browser.Name + " is compatible with " + browser.DriverName.Remove(browser.DriverName.Length - 4, 4) + " version.");
                            }
                            else
                            {
                                if (browser.DriverVersion != deployedVer)
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, browser.DriverName + " does not match with the currently supported version " + deployedVer + " and may result in errors when executing automated tests.");
                                }
                            }
                        }
                    }
                    else
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, browser.DriverName + " does not exist in the specified Test Runner path.");
                    }
                }
                else
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Version support file is an outdated version.");
                }
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Version support file does not exist.");
            }
        }

        /// <summary>
        /// Checks if the installed browser is supported or not
        /// </summary>
        /// <param name="browser">DlkBrowser object</param>
        private void CheckIfBrowserVersionIsSupported(DlkBrowser browser)
        {
            bool newer = false;
            bool older = false;

            string mVersionSupportListPath = Path.Combine(TRRootPath, @"Products\Common\Configs\version_support.xml");
            if (File.Exists(mVersionSupportListPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mVersionSupportListPath);

                XmlNode ver = doc.SelectSingleNode("versions /browser[@name = '" + browser.Alias.ToLower() + "']");
                if (ver != null)
                {
                    if (!String.IsNullOrEmpty(browser.Version) || browser.Version != "Unidentified")
                    {
                        List<string> supportedVersions = ver.Attributes["supportedversions"].Value.ToString().Split(',').ToList();

                        int startIdx = browser.Version.Contains(".") ? browser.Version.IndexOf(".") : browser.Version.Length;
                        string mainVer = browser.Version.Remove(startIdx);

                        if (!supportedVersions.Contains(mainVer))
                        {
                            if (supportedVersions.Count == 1)
                            {
                                int beginIdx = supportedVersions[0].Contains(".") ? supportedVersions[0].IndexOf(".") : supportedVersions[0].Length;
                                string supportedVerMain = supportedVersions[0].Length != beginIdx ? supportedVersions[0].Remove(beginIdx) : supportedVersions[0];
                                older = Convert.ToInt32(mainVer) < Convert.ToInt32(supportedVerMain);

                                if (!older)
                                {
                                    newer = Convert.ToInt32(mainVer) > Convert.ToInt32(supportedVerMain);
                                    if (newer)
                                    {
                                        DiagnosticLogger.LogResult(Logger.MessageType.WARNING, browser.Name + " " + browser.Version + " exceeded supported browser version and may be incompatible with the webdriver version packaged with Test Runner.");
                                    }
                                }
                                else
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, browser.Name + " " + browser.Version + " is incompatible with the webdriver and might fail.");
                                }
                            }
                            else
                            {
                                older = Convert.ToInt32(mainVer) < Convert.ToInt32(supportedVersions[0]);
                                if (!older)
                                {
                                    newer = Convert.ToInt32(mainVer) > Convert.ToInt32(supportedVersions.Count - 1);
                                    if (newer)
                                    {
                                        DiagnosticLogger.LogResult(Logger.MessageType.WARNING, browser.Name + " " + browser.Version + " exceeded supported browser version and may be incompatible with the webdriver version packaged with Test Runner.");
                                    }
                                }
                                else
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, browser.Name + " " + browser.Version + " is incompatible with the webdriver and might fail.");
                                }
                            }
                        }
                    }
                    else
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, browser.Name + " " + "is Unidentified and cannot be determined.");
                    }
                }
            }
        }

        /// <summary>
        /// Gets root path from Test Runner path found
        /// </summary>
        /// <param name="testRunnerPath">Test Runner path from Main</param>
        /// <returns>TR Root path</returns>
        private string GetRootPath(string testRunnerPath)
        {
            string testRunnerDir = testRunnerPath;
            var rootPath = Directory.GetParent(testRunnerDir).FullName;
            while (new DirectoryInfo(rootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                rootPath = Directory.GetParent(rootPath).FullName;
            }
            return rootPath;
        }

        /// <summary>
        /// Gets latest supported version from version support file
        /// </summary>
        /// <param name="browserName">Browser name</param>
        /// <returns>latest supported version</returns>
        private string GetLatestSupportedVersion(string browserName)
        {
            string ret = String.Empty;
            string mVersionSupportListPath = Path.Combine(TRRootPath, @"Products\Common\Configs\version_support.xml");
            if (File.Exists(mVersionSupportListPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mVersionSupportListPath);

                XmlNode ver = doc.SelectSingleNode("versions /browser[@name = '" + browserName + "']");
                List<string> supportedVersions = ver.Attributes["supportedversions"].Value.ToString().Split(',').ToList();

                if (ver != null)
                {
                    if (browserName.ToLower().Contains("firefox"))
                    {
                        if (supportedVersions.Count > 1)
                        {
                            return "Mozilla Firefox " + supportedVersions[supportedVersions.Count - 1];
                        }
                        else
                        {
                            return "Mozilla Firefox " + supportedVersions[0];
                        }
                    }
                    if (browserName.ToLower().Contains("chrome"))
                    {
                        if (supportedVersions.Count > 1)
                        {
                            return "Google Chrome " + supportedVersions[supportedVersions.Count - 1];
                        }
                        else
                        {
                            return "Google Chrome " + supportedVersions[0];
                        }
                    }
                    if (browserName.ToLower().Contains("internet explorer"))
                    {
                        if (supportedVersions.Count > 1)
                        {
                            return "Internet Explorer " + supportedVersions[supportedVersions.Count - 1];
                        }
                        else
                        {
                            return "Internet Explorer " + supportedVersions[0];
                        }
                    }
                    if (browserName.ToLower().Contains("edge"))
                    {
                        if (supportedVersions.Count > 1)
                        {
                            return "Microsoft Edge " + supportedVersions[supportedVersions.Count - 1];
                        }
                        else
                        {
                            return "Microsoft Edge " + supportedVersions[0];
                        }
                    }
                }
            }
            return browserName;
        }

        /// <summary>
        /// Checks if Test Runner is potentially running in an internal release folder or not
        /// </summary>
        /// <returns>whether or not Test Runner is potentially an internal release</returns>
        private bool IsInternal()
        {
            bool ret = false;
            if (TRRootPath.Contains("QEAutomation") || TRRootPath.Contains("TFS"))
            {
                ret = true;
            }
            return ret;
        }
    }

    /// <summary>
    /// Class for Browsers installed in machine
    /// </summary>
    internal class DlkBrowser
    {

        #region Properties

        /// <summary>
        /// Browser Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Browser Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Browser version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Browser alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Browser driver name
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// Browser driver version
        /// </summary>
        public string DriverVersion { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Contructor for Browser data
        /// </summary>
        /// <param name="path">path of the installed browser</param>
        /// <param name="name">browser name</param>
        /// <param name="version">browser version</param>
        /// <param name="alias">browser alias</param>
        /// <param name="driverName">browser driver name</param>
        /// <param name="driverVersion">browser driver version</param>
        public DlkBrowser(string path, string name, string version, string alias, string driverName, string driverVersion)
        {
            Path = path;
            Name = name;
            Version = version;
            Alias = alias;
            DriverName = driverName;
            DriverVersion = driverVersion;
        }

        /// <summary>
        /// Browser without any values
        /// </summary>
        public DlkBrowser()
        {
        }

        #endregion

    }
}
