using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics.Eventing.Reader;

namespace TRDiagnosticsCore.Tests
{
    public class CoreFilesTest : DiagnosticTest
    {
        const int ROOT_FOLDERS_COUNT = 2;
        const int PRODUCT_SUBFOLDER_COUNT = 4;
        const int PRODUCT_FRAMEWORK_FOLDER_COUNT = 5;
        const int PRODUCT_LIBRARY_FOLDER_COUNT = 2;
        const int PRODUCT_LIBRARY_TAGS_FILE_COUNT = 1;
        const int PRODUCT_REMOTEBROWSERS_FILE_COUNT = 2;
        const int PRODUCT_USERTESTDATA_FOLDER_COUNT = 1;
        const int PRODUCT_USERTESTDATA_DOCDIFF_FOLDER_COUNT = 3;
        const int PRODUCTS_COMMON_CONFIGS_FILE_COUNT = 1;
        const int PRODUCTS_COMMON_STYLESHEET_FILE_COUNT = 1;

        const int TEST_PRODUCTS_MULTIPLIER = 13;
        const int TEST_FIXED_CHECKS_COUNT = 15;

        bool isInitMissing = true;
        bool isInitMalformed = true;
        bool hasCommonFolder = false;
        string mainPath = "";
        string initPath = "";
        string productPath = "";
        string productsCommonPath = "";
        string productsCommonConfigsPath = "";
        string productsCommonDashboardPath = "";
        string productsCommonStyleSheetPath = "";
        string toolsPath = "";
        string toolsExternalLibPath = "";
        string toolsSeleniumPath = "";
        string toolsTestRunnerPath = "";
        string toolsTestRunnerResourcesPath = "";
        string genericExternalReleaseMessage = "It is recommended to repair or re-install Test Runner using the provided installer. If this issue persists after repair, please submit a report to deltektestrunner@deltek.com";
        string requiredGenericFolderErrorMessage = " This is a required folder, and will not be automatically created by Test Runner if it is missing.";
        string requiredProductFolderErrorMessage = " This is a required folder, and the product will not appear in Target Applications in Settings>Preferences if it is missing.";
        List<string> productsWithAlias = new List<string>();
        List<string> productsWithHelpBox = new List<string>();
        List<string> productFolders = new List<string>();
        List<string> productsCommonFolders = new List<string>();
        List<string> productsCommonFolderStrings = new List<string>();
        List<string> productsCommonConfigsFileStrings = new List<string>();
        List<string> productsCommonDashboardFileStrings = new List<string>();
        List<string> toolsFolders = new List<string>();
        List<string> toolsFolderStrings = new List<string>();
        List<string> toolsTestRunnerExecutableStrings = new List<string>();
        List<string> toolsTestRunnerBatchFileStrings = new List<string>();
        List<string> toolsTestRunnerBrowserDriverStrings = new List<string>();
        List<string> toolsTestRunnerLibraryStrings = new List<string>();
        List<string> toolsTestRunnerMiscellaneousFileStrings = new List<string>();
        List<string> toolsTestRunnerResourcesFileStrings = new List<string>();
        List<string> toolsExternalLibFileStrings = new List<string>();
        List<string> toolsSeleniumFileStrings = new List<string>();
        List<string> envs = new List<string>();
        IDictionary<string, string> productLibraryFileStrings = new Dictionary<string, string>();
        List<MissingTest> _missingTestScript = null;
        List<MissingEnvironment> _missingEnvironment = null;

        public CoreFilesTest() : base() { }

        protected override void DefineTestName()
        {
            TotalTestCount = CalculateTestCount();
            TestName = "Core Directories and Files Check";
        }

        protected override void PerformCheck(out string ErrorMessage)
        {
            try
            {
                ErrorMessage = string.Empty;
                InitializeProductsWithHelpBox();
                InitializeToolsSubfolders();
                InitializeToolsTestRunnerExecutables();
                InitializeToolsTestRunnerBatchFiles();
                InitializeToolsTestRunnerBrowserDrivers();
                InitializeToolsTestRunnerLibraries();
                InitializeToolsTestRunnerMiscellaneousFiles();
                InitializeToolsTestRunnerResourcesFiles();
                InitializeToolsExternalLibFiles();
                InitializeToolsSeleniumFiles();
                InitializeProductsCommonFolders();
                InitializeProductsCommonConfigsFiles();
                InitializeProductsCommonDashboardFiles();

                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for core files and directories...");
                CheckRootFolders();
                if (productFolders.Count > 0)
                {
                    CheckOutdatedOSFiles();
                    CheckMalformedOSFiles();
                    if (DirectoryArgument.RunTestScript) CheckMalformedTests();
                    if (DirectoryArgument.RunTestSuite) CheckMalformedSuites();
                }

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

            switch (Record.MessageType)
            {
                case Logger.MessageType.WARNING:
                    if (Record.MessageDetails.Contains("is read-only"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is recommended to remove the read-only flag by right-clicking the file, going to Properties, and unchecking the Read-only checkbox.");
                    }
                    else if (Record.MessageDetails.Contains("No valid global variable file"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is recommended to open a suite and click on the Variables link in the Main Window to generate a new GlobalVar file.");
                    }
                    else if (Record.MessageDetails.Contains("query file is malformed"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "It is recommended to open Test Library and save a new query, or fix the current query xml file.");
                    }
                    else if (Record.MessageDetails.Contains("is malformed"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to either fix the XML file's incomplete content, or download the latest version of the file from TFS." : genericExternalReleaseMessage);
                    }
                    break;
                case Logger.MessageType.ERROR:
                    if (Record.MessageDetails.Contains("in Configs folder not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the latest version of the missing file in Products\Framework\Configs folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("Init.dat") && Record.MessageDetails.Contains("not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "It is recommended to get the latest Init.dat file in TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("Init.dat") && Record.MessageDetails.Contains("malformed"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "It is recommended to fix your copy of Init.dat by getting the latest file in TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("is read-only"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, @"It is recommended to remove the read-only flag by right-clicking the file, going to Properties, and unchecking the Read-only checkbox.");
                    }
                    else if (Record.MessageDetails.Contains(" product not found")
                        || Record.MessageDetails.Contains(@"folder for Products\Common")
                        || Record.MessageDetails.Contains("in DocDiff not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the complete folder structure of the product folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("Framework folder not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the complete folder structure of the Products\Framework folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("Products folder not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the required Products folders, including any product folder and the Common folder, from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("Common folder not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the complete Common folder in Products from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains(" in Tests folder not found."))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the Template.dat file in the product's Tests folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("folder not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "Folder structure must be complete, it is recommended to Get Latest files from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails == "Products folder doesn't have any valid folders.")
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "It is recommended to download at least 1 product, as well as the Common folder, from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("main executable not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "It is recommended to download TestRunner.exe located inside Tools folder in TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("No valid tools subfolder")
                        || Record.MessageDetails.Contains("library needed for")
                        || Record.MessageDetails.Contains("executable not found")
                        || Record.MessageDetails.Contains("batch file not found")
                        || Record.MessageDetails.Contains("webdriver not found")
                        || Record.MessageDetails.Contains("library not found")
                        || Record.MessageDetails.Contains("miscellaneous file not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "It is recommended to download the full content of the Tools folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("product not found")
                        || Record.MessageDetails.Contains(@"for Common\Configs not found")
                        || Record.MessageDetails.Contains(@"for Common\StyleSheet not found")
                        || Record.MessageDetails.Contains(@"Products\Common\Dashboard not found"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? "It is recommended to download the full content of the product folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("Resources folder not found."))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the full content of the Tools\TestRunner folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("resource file not found."))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the full content of the Tools\TestRunner\Resources folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("in ExternalLib folder is missing"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the full content of the Tools\ExternalLib folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("in Selenium folder is missing"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the full content of the Tools\Selenium folder from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("is outdated"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to download the latest version of the Object Store file from TFS." : genericExternalReleaseMessage);
                    }
                    else if (Record.MessageDetails.Contains("is malformed"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, IsInternal() ? @"It is recommended to either fix the XML file's incomplete content, or download the latest version of the Object Store file from TFS." : genericExternalReleaseMessage);
                    }
                    else if(Record.MessageDetails.Contains("The following suites are affected"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, @"It is recommended to update the affected suites with any of the existing environments.");
                    }
                    else if (Record.MessageDetails.Contains("deleted or missing test scripts:"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, @"It is recommended to update the affected suites and remove the missing scripts from the lineup.");
                    }
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Add products with alias to list to be used for checking for outdated OS files
        /// </summary>
        private void InitializeProductsWithAlias()
        {
            productsWithAlias.Add("Costpoint_701");
            productsWithAlias.Add("Costpoint_711");
            productsWithAlias.Add("BudgetingAndPlanning");
            productsWithAlias.Add("TimeAndExpense");
        }

        /// <summary>
        /// Add products that use Test Editor Help box to list to be used for checking for TestEditorHelp file
        /// </summary>
        private void InitializeProductsWithHelpBox()
        {
            productsWithHelpBox = new List<string>(productsWithAlias);
            productsWithHelpBox.Add("MaconomyiAccess_20");
            productsWithHelpBox.Add("MaconomyiAccess_223");
            productsWithHelpBox.Add("WorkBook");
            productsWithHelpBox.Sort();
        }

        /// <summary>
        /// Add products with corresponding dll file needed to run tests to dictionary for checking
        /// <param name="path">init.dat path</param>
        /// </summary>
        private void InitializeProductLibraryFiles(string path)
        {
            XDocument init = XDocument.Load(path);
            var appList = from app in init.Descendants("application")
                          select new
                          {
                              library = app.Attribute("library").Value,
                              productfolder = app.Attribute("productfolder").Value
                          };
            foreach (var val in appList)
            {
                productLibraryFileStrings.Add(val.productfolder, val.library);
            }
        }

        /// <summary>
        /// Add needed Tools subfolders to list for checking
        /// </summary>
        private void InitializeToolsSubfolders()
        {
            toolsFolderStrings.Add("TestRunner");
            if (IsInternal())
            {
                toolsFolderStrings.Add("ExternalLib");
                toolsFolderStrings.Add("Selenium");
                toolsFolderStrings.Sort();
            }
        }

        /// <summary>
        /// Add needed Products\Common folders to list for checking
        /// </summary>
        private void InitializeProductsCommonFolders()
        {
            productsCommonFolderStrings.Add("Configs");
            productsCommonFolderStrings.Add("Dashboard");
            productsCommonFolderStrings.Add("Scheduler");
            productsCommonFolderStrings.Add("StyleSheet");
        }

        /// <summary>
        /// Add required Tools\TestRunner executables to list for checking
        /// </summary>
        private void InitializeToolsTestRunnerExecutables()
        {
            toolsTestRunnerExecutableStrings.Add("AutoMail.exe");
            toolsTestRunnerExecutableStrings.Add("AutomationAgent.exe");
            toolsTestRunnerExecutableStrings.Add("DocDiff.exe");
            toolsTestRunnerExecutableStrings.Add("GenerateDashboard.exe");
            toolsTestRunnerExecutableStrings.Add("scheduler.exe");
            toolsTestRunnerExecutableStrings.Add("SchedulingAgent.exe");
            toolsTestRunnerExecutableStrings.Add("TestRunner.exe");
            toolsTestRunnerExecutableStrings.Add("TestRunnerCmd.exe");
            toolsTestRunnerExecutableStrings.Add("TestRunnerScheduler.exe");
            if (IsInternal())
            {
                toolsTestRunnerExecutableStrings.Add("LoginConfig Updater.exe");
                toolsTestRunnerExecutableStrings.Add("TestIT.exe");
                toolsTestRunnerExecutableStrings.Add("TestLibrary.exe");
                toolsTestRunnerExecutableStrings.Sort();
            }
        }

        /// <summary>
        /// Add required Tools\TestRunner batch files to list for checking
        /// </summary>
        private void InitializeToolsTestRunnerBatchFiles()
        {
            toolsTestRunnerBatchFileStrings.Add("AutoAgentStartUp.bat");
            toolsTestRunnerBatchFileStrings.Add("OpenMspaint.bat");
            toolsTestRunnerBatchFileStrings.Add("SharedFolder.bat");
            toolsTestRunnerBatchFileStrings.Add("SharedFolderFull.bat");
            if (IsInternal())
            {
                toolsTestRunnerBatchFileStrings.Add("SchedulerGetLatest.bat");
                toolsTestRunnerBatchFileStrings.Add("SourceControl.bat");
                toolsTestRunnerBatchFileStrings.Add("TestRunner_Navigator.bat");
                toolsTestRunnerBatchFileStrings.Add("UnitTest.bat");
                toolsTestRunnerBatchFileStrings.Sort();
            }
        }

        /// <summary>
        /// Add required Tools\TestRunner browser drivers to list for checking
        /// </summary>
        private void InitializeToolsTestRunnerBrowserDrivers()
        {
            toolsTestRunnerBrowserDriverStrings.Add("chromedriver.exe");
            toolsTestRunnerBrowserDriverStrings.Add("geckodriver.exe");
            toolsTestRunnerBrowserDriverStrings.Add("IEDriverServer.exe");
            toolsTestRunnerBrowserDriverStrings.Add("MicrosoftWebDriver.exe");
            toolsTestRunnerBrowserDriverStrings.Add("msedgedriver.exe");
            toolsTestRunnerBrowserDriverStrings.Add("WebDriver.dll");
            toolsTestRunnerBrowserDriverStrings.Add("WebDriver.Support.dll");
        }

        /// <summary>
        /// Add required Tools\TestRunner libraries to list for checking
        /// </summary>
        private void InitializeToolsTestRunnerLibraries()
        {
            toolsTestRunnerLibraryStrings.Add("Appium.Net.dll");
            toolsTestRunnerLibraryStrings.Add("Castle.Core.dll");
            toolsTestRunnerLibraryStrings.Add("CommonLib.dll");
            toolsTestRunnerLibraryStrings.Add("De.TorstenMandelkow.MetroChart.dll");
            toolsTestRunnerLibraryStrings.Add("Diff.Match.Patch.dll");
            toolsTestRunnerLibraryStrings.Add("DocumentFormat.OpenXml.dll");
            toolsTestRunnerLibraryStrings.Add("EPPlus.dll");
            toolsTestRunnerLibraryStrings.Add("FSharp.Core.dll");
            toolsTestRunnerLibraryStrings.Add("HtmlAgilityPack.dll");
            toolsTestRunnerLibraryStrings.Add("Ionic.Zip.dll");
            toolsTestRunnerLibraryStrings.Add("Microsoft.Edge.SeleniumTools.dll");
            toolsTestRunnerLibraryStrings.Add("Newtonsoft.Json.dll");
            toolsTestRunnerLibraryStrings.Add("Oracle.ManagedDataAccess.dll");
            toolsTestRunnerLibraryStrings.Add("Selenium.WebDriverBackedSelenium.dll");
            toolsTestRunnerLibraryStrings.Add("SgmlReaderDll.dll");
            toolsTestRunnerLibraryStrings.Add("ThoughtWorks.Selenium.Core.dll");
            toolsTestRunnerLibraryStrings.Add("Xceed.Wpf.Toolkit.dll");
        }

        /// <summary>
        /// Add required Tools\TestRunner miscellaneous files to list for checking
        /// </summary>
        private void InitializeToolsTestRunnerMiscellaneousFiles()
        {
            toolsTestRunnerMiscellaneousFileStrings.Add("DeltekTestRunnerUserGuide.chm");
            toolsTestRunnerMiscellaneousFileStrings.Add("ReleaseNews.xml");
            toolsTestRunnerMiscellaneousFileStrings.Add("Selenium.WebDriverBackedSelenium.xml");
            toolsTestRunnerMiscellaneousFileStrings.Add("test_connect.dat");
            toolsTestRunnerMiscellaneousFileStrings.Add("TestCapture.pdf");
            toolsTestRunnerMiscellaneousFileStrings.Add("WebDriver.Support.xml");
            toolsTestRunnerMiscellaneousFileStrings.Add("WebDriver.xml");
            if (IsInternal())
            {
                toolsTestRunnerMiscellaneousFileStrings.Add("DeltekTestRunnerUserGuide.chw");
                toolsTestRunnerMiscellaneousFileStrings.Add("GenerateStandardScript.xslt");
                toolsTestRunnerMiscellaneousFileStrings.Add("LoginConfig_Updater_Readme.txt");
                toolsTestRunnerMiscellaneousFileStrings.Add("selenium-server-standalone-3.11.0.jar");
                toolsTestRunnerMiscellaneousFileStrings.Add("shortcut.txt");
                toolsTestRunnerMiscellaneousFileStrings.Sort();
            }
        }

        /// <summary>
        /// Add required Tools\TestRunner\Resources images to list for checking
        /// </summary>
        private void InitializeToolsTestRunnerResourcesFiles()
        {
            toolsTestRunnerResourcesFileStrings.Add("add.png");
            toolsTestRunnerResourcesFileStrings.Add("add_folder.png");
            toolsTestRunnerResourcesFileStrings.Add("addblue.png");
            toolsTestRunnerResourcesFileStrings.Add("addtwo.png");
            toolsTestRunnerResourcesFileStrings.Add("agent.ico");
            toolsTestRunnerResourcesFileStrings.Add("arrow_down.png");
            toolsTestRunnerResourcesFileStrings.Add("arrow_left.png");
            toolsTestRunnerResourcesFileStrings.Add("arrow_right.png");
            toolsTestRunnerResourcesFileStrings.Add("arrow_up.png");
            toolsTestRunnerResourcesFileStrings.Add("bullet_arrow_down.png");
            toolsTestRunnerResourcesFileStrings.Add("bullet_arrow_up.png");
            toolsTestRunnerResourcesFileStrings.Add("bullet_toggle_minus.png");
            toolsTestRunnerResourcesFileStrings.Add("bullet_toggle_plus.png");
            toolsTestRunnerResourcesFileStrings.Add("button_cancel.ico");
            toolsTestRunnerResourcesFileStrings.Add("button_download.ico");
            toolsTestRunnerResourcesFileStrings.Add("button_enable.png");
            toolsTestRunnerResourcesFileStrings.Add("buttondown.png");
            toolsTestRunnerResourcesFileStrings.Add("buttonup.png");
            toolsTestRunnerResourcesFileStrings.Add("check.ico");
            toolsTestRunnerResourcesFileStrings.Add("Checkout.png");
            toolsTestRunnerResourcesFileStrings.Add("clearbroom.png");
            toolsTestRunnerResourcesFileStrings.Add("controller.ico");
            toolsTestRunnerResourcesFileStrings.Add("copy.png");
            toolsTestRunnerResourcesFileStrings.Add("database.ico");
            toolsTestRunnerResourcesFileStrings.Add("delete.ico");
            toolsTestRunnerResourcesFileStrings.Add("delete.png");
            toolsTestRunnerResourcesFileStrings.Add("DeltekBackground.png");
            toolsTestRunnerResourcesFileStrings.Add("documentedit.png");
            toolsTestRunnerResourcesFileStrings.Add("edit.png");
            toolsTestRunnerResourcesFileStrings.Add("editgray.png");
            toolsTestRunnerResourcesFileStrings.Add("envelope.png");
            toolsTestRunnerResourcesFileStrings.Add("eraser.png");
            toolsTestRunnerResourcesFileStrings.Add("file.png");
            toolsTestRunnerResourcesFileStrings.Add("filter.png");
            toolsTestRunnerResourcesFileStrings.Add("find.png");
            toolsTestRunnerResourcesFileStrings.Add("folder.png");
            toolsTestRunnerResourcesFileStrings.Add("folder_sync.png");
            toolsTestRunnerResourcesFileStrings.Add("Gear.png");
            toolsTestRunnerResourcesFileStrings.Add("generate_data.png");
            toolsTestRunnerResourcesFileStrings.Add("glasses3.jpg");
            toolsTestRunnerResourcesFileStrings.Add("GreenCorner.png");
            toolsTestRunnerResourcesFileStrings.Add("group_bottom.png");
            toolsTestRunnerResourcesFileStrings.Add("group_left.png");
            toolsTestRunnerResourcesFileStrings.Add("group_top.png");
            toolsTestRunnerResourcesFileStrings.Add("Import.ico");
            toolsTestRunnerResourcesFileStrings.Add("info.png");
            toolsTestRunnerResourcesFileStrings.Add("insert_table_row.png");
            toolsTestRunnerResourcesFileStrings.Add("instance.png");
            toolsTestRunnerResourcesFileStrings.Add("loading_tr2.gif");
            toolsTestRunnerResourcesFileStrings.Add("offline.png");
            toolsTestRunnerResourcesFileStrings.Add("online.png");
            toolsTestRunnerResourcesFileStrings.Add("paste.png");
            toolsTestRunnerResourcesFileStrings.Add("play.png");
            toolsTestRunnerResourcesFileStrings.Add("record.png");
            toolsTestRunnerResourcesFileStrings.Add("recordpause.png");
            toolsTestRunnerResourcesFileStrings.Add("recordresume.ico");
            toolsTestRunnerResourcesFileStrings.Add("recordstart.ico");
            toolsTestRunnerResourcesFileStrings.Add("recordstart.png");
            toolsTestRunnerResourcesFileStrings.Add("recordstop.ico");
            toolsTestRunnerResourcesFileStrings.Add("refresh.ico");
            toolsTestRunnerResourcesFileStrings.Add("refresh.jpg");
            toolsTestRunnerResourcesFileStrings.Add("refresh.png");
            toolsTestRunnerResourcesFileStrings.Add("rename.png");
            toolsTestRunnerResourcesFileStrings.Add("rename1.png");
            toolsTestRunnerResourcesFileStrings.Add("reset.png");
            toolsTestRunnerResourcesFileStrings.Add("save.png");
            toolsTestRunnerResourcesFileStrings.Add("search.png");
            toolsTestRunnerResourcesFileStrings.Add("Splash.png");
            toolsTestRunnerResourcesFileStrings.Add("stoprecord.png");
            toolsTestRunnerResourcesFileStrings.Add("table_delete_column.png");
            toolsTestRunnerResourcesFileStrings.Add("table_delete_row.png");
            toolsTestRunnerResourcesFileStrings.Add("table_export.png");
            toolsTestRunnerResourcesFileStrings.Add("table_import.png");
            toolsTestRunnerResourcesFileStrings.Add("table_insert_column.png");
            toolsTestRunnerResourcesFileStrings.Add("table_insert_row.png");
            toolsTestRunnerResourcesFileStrings.Add("table-insert-icon.png");
            toolsTestRunnerResourcesFileStrings.Add("table-row-insert-icon.png");
            toolsTestRunnerResourcesFileStrings.Add("text.png");
        }

        /// <summary>
        /// Add required Tools\ExternalLib files to list for checking
        /// </summary>
        private void InitializeToolsExternalLibFiles()
        {
            toolsExternalLibFileStrings.Add("Appium.Net.dll");
            toolsExternalLibFileStrings.Add("Castle.Core.dll");
            toolsExternalLibFileStrings.Add("De.TorstenMandelkow.MetroChart.dll");
            toolsExternalLibFileStrings.Add("Diff.Match.Patch.dll");
            toolsExternalLibFileStrings.Add("DocumentFormat.OpenXml.dll");
            toolsExternalLibFileStrings.Add("EPPlus.dll");
            toolsExternalLibFileStrings.Add("FSharp.Core.dll");
            toolsExternalLibFileStrings.Add("FSharp.Core.xml");
            toolsExternalLibFileStrings.Add("HtmlAgilityPack.dll");
            toolsExternalLibFileStrings.Add("Oracle.ManagedDataAccess.dll");
            toolsExternalLibFileStrings.Add("PdfSharp.Charting.dll");
            toolsExternalLibFileStrings.Add("PdfSharp.dll");
            toolsExternalLibFileStrings.Add("SgmlReaderDll.dll");
            toolsExternalLibFileStrings.Add("System.Windows.Interactivity.dll");
            toolsExternalLibFileStrings.Add("Xceed.Wpf.Toolkit.dll");
        }

        /// <summary>
        /// Add required Tools\TestRunner browser drivers to list for checking
        /// </summary>
        private void InitializeToolsSeleniumFiles()
        {
            toolsSeleniumFileStrings.Add("Ionic.Zip.dll");
            toolsSeleniumFileStrings.Add("Microsoft.Edge.SeleniumTools.dll");
            toolsSeleniumFileStrings.Add("Newtonsoft.Json.dll");
            toolsSeleniumFileStrings.Add("Selenium.WebDriverBackedSelenium.dll");
            toolsSeleniumFileStrings.Add("Selenium.WebDriverBackedSelenium.xml");
            toolsSeleniumFileStrings.Add("ThoughtWorks.Selenium.Core.dll");
            toolsSeleniumFileStrings.Add("ThoughtWorks.Selenium.Core.xml");
            toolsSeleniumFileStrings.Add("WebDriver.chm");
            toolsSeleniumFileStrings.Add("WebDriver.dll");
            toolsSeleniumFileStrings.Add("WebDriver.Support.dll");
            toolsSeleniumFileStrings.Add("WebDriver.Support.xml");
            toolsSeleniumFileStrings.Add("WebDriver.xml");
        }

        /// <summary>
        /// Add required Products\Common\Configs files to list for checking
        /// </summary>
        private void InitializeProductsCommonConfigsFiles()
        {
            productsCommonConfigsFileStrings.Add("version_support.xml");
        }

        /// <summary>
        /// Add required Products\Common\Dashboard files to list for checking
        /// </summary>
        private void InitializeProductsCommonDashboardFiles()
        {
            productsCommonDashboardFileStrings.Add("logo1.png");
            productsCommonDashboardFileStrings.Add("logo2.png");
            productsCommonDashboardFileStrings.Add("logo3.png");
            productsCommonDashboardFileStrings.Add("StyleSheet1.css");
            productsCommonDashboardFileStrings.Add("SuiteDetails.xsl");
            productsCommonDashboardFileStrings.Add("Summary.xsl");
        }

        /// <summary>
        /// Retrieves current existing product folders
        /// </summary>
        private void GetCurrentProductFolders()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking valid product folders...");
            bool hasProductFolder = false;
            if (productFolders.Count == 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Products folder doesn't have any valid folders.");
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, productFolders.Count.ToString() + " product folders found.");
                hasProductFolder = true;
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Products\Common folder...");
            if (hasCommonFolder)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, @"Products\Common folder found.");
                productsCommonPath = productPath + @"Common\";
                GetCurrentProductsCommonFolders();
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"Products\Common folder not found." + requiredGenericFolderErrorMessage);
            }
            if (hasProductFolder)
            {
                CheckProductFolderDirectories();
            }
        }

        /// <summary>
        /// Retrieves current existing Tools folders
        /// </summary>
        private void GetCurrentToolsFolders()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking valid Tools subfolders...");
            toolsFolders = RemoveInvalidFolders(toolsFolderStrings, toolsPath);
            if (toolsFolders.Count == 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "No valid Tools subfolder found.");
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsFolders.Count(), toolsFolderStrings.Count())  + " Tools subfolder/s found.");
                CheckMainTestRunnerExecutable();
                CheckToolsFolderDirectories();
            }
        }

        /// <summary>
        /// Retrieves current existing Products\Common folders
        /// </summary>
        private void GetCurrentProductsCommonFolders()
        {
            
            productsCommonFolders = RemoveInvalidFolders(productsCommonFolderStrings, productsCommonPath);
            if (productsCommonFolders.Count == 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "No Products/Common subfolder found.");
            }
            else
            {
                CheckProductsCommonFolderDirectories();
            }
        }

        /// <summary>
        /// Retrieves current existing Tools\TestRunner folders
        /// </summary>
        private void GetCurrentToolsTestRunnerFolders()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking TestRunner\Resources folder...");
            if (Directory.Exists(toolsTestRunnerPath + "Resources"))
            {
                toolsTestRunnerResourcesPath = Path.Combine(toolsTestRunnerPath, "Resources") + @"\";
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, @"TestRunner\Resources folder found.");
                CheckToolsTestRunnerResources();
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"TestRunner\Resources folder not found.");
            }
        }

        /// <summary>
        /// Retrieves current existing root folders
        /// </summary>
        private void CheckRootFolders()
        {
            int rootFoldersCount = 0;
            bool hasProductsFolder = false;
            bool hasToolsFolder = false;
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking root folders...");
            string mDirToolsRoot = Path.Combine(mainPath, "Tools") + @"\";
            if (Directory.Exists(productPath))
            {
                rootFoldersCount++;
                hasProductsFolder = true;
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Products folder not found." + requiredGenericFolderErrorMessage);
            }
            if (Directory.Exists(mDirToolsRoot))
            {
                rootFoldersCount++;
                toolsPath = mDirToolsRoot;
                hasToolsFolder = true;
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Tools folder not found." + requiredGenericFolderErrorMessage);
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(rootFoldersCount, ROOT_FOLDERS_COUNT) + " root folder/s found.");
            if (hasProductsFolder)
            {
                GetCurrentProductFolders();
            }
            if (hasToolsFolder)
            {
                GetCurrentToolsFolders();
            }
        }

        /// <summary>
        /// Checks whether the required Products subfolders exist
        /// </summary>
        private void CheckProductFolderDirectories()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking specific product subfolders...");
            foreach (string product in productFolders)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking product subfolders for " + product + "...");
                bool hasFrameworkFolder = false;
                bool hasTestsFolder = false;
                bool hasUserTestDataFolder = false;
                int currentProductSubfolderCount = 0;
                string mDirProductPath = Path.Combine(productPath, product) + @"\";
                List<string> productFolderDirectories = new DirectoryInfo(mDirProductPath).GetDirectories()
                .Select(x => x.Name).ToList();
                if (!productFolderDirectories.Any(x => x == "Framework"))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Framework folder for " + product + " product not found." + requiredProductFolderErrorMessage);
                }
                else
                {
                    currentProductSubfolderCount++;
                    hasFrameworkFolder = true;
                }
                if (!productFolderDirectories.Any(x => x == "Suites"))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Suites folder for " + product + " product not found." + requiredProductFolderErrorMessage);
                }
                else
                {
                    currentProductSubfolderCount++;
                }
                if (!productFolderDirectories.Any(x => x == "Tests"))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Tests folder for " + product + " product not found." + requiredProductFolderErrorMessage);
                }
                else
                {
                    currentProductSubfolderCount++;
                    hasTestsFolder = true;
                }
                if (!productFolderDirectories.Any(x => x == "UserTestData"))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "UserTestData folder for " + product + " product not found." + requiredProductFolderErrorMessage);
                }
                else
                {
                    currentProductSubfolderCount++;
                    hasUserTestDataFolder = true;
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(currentProductSubfolderCount, PRODUCT_SUBFOLDER_COUNT) + " subfolder/s in " + product + " found.");
                if (hasFrameworkFolder)
                {
                    CheckProductFrameworkFolders(product);
                }
                if (hasTestsFolder)
                {
                    CheckProductTestsFiles(product);
                }
                if (hasUserTestDataFolder)
                {
                    CheckProductUserTestDataFolders(product);
                }
            }
        }

        /// <summary>
        /// Checks whether the required Products\Common subfolders exist
        /// </summary>
        private void CheckProductsCommonFolderDirectories()
        {
            bool hasConfigsFolder = false;
            bool hasDashboardFolder = false;
            bool hasStyleSheetFolder = false;
            bool hasSchedulerFolder = false;
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Products\Common subfolders...");
            if (!productsCommonFolders.Any(x => x == "Configs"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"Configs folder for Products\Common not found." + requiredGenericFolderErrorMessage);
            }
            else
            {
                productsCommonConfigsPath = Path.Combine(productsCommonPath, "Configs") + @"\";
                hasConfigsFolder = true;
            }
            if (!productsCommonFolders.Any(x => x == "Dashboard"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"Dashboard folder for Products\Common not found." + requiredGenericFolderErrorMessage);
            }
            else
            {
                productsCommonDashboardPath = Path.Combine(productsCommonPath, "Dashboard") + @"\";
                hasDashboardFolder = true;
            }
            if (!productsCommonFolders.Any(x => x == "Scheduler"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"Scheduler folder for Products\Common not found." + requiredGenericFolderErrorMessage);
            }
            else
            {
                hasSchedulerFolder = true;
            }
            if (!productsCommonFolders.Any(x => x == "StyleSheet"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"StyleSheet folder for Products\Common not found." + requiredGenericFolderErrorMessage);
            }
            else
            {
                productsCommonStyleSheetPath = Path.Combine(productsCommonPath, "StyleSheet") + @"\";
                hasStyleSheetFolder = true;
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productsCommonFolders.Count, productsCommonFolderStrings.Count) + @" Products\Common subfolder/s found.");
            if (hasConfigsFolder)
            {
                CheckProductsCommonConfigsFiles();
            }
            if (hasSchedulerFolder)
            {
                CheckProductsCommonSchedulerFiles();
            }
            if (hasDashboardFolder)
            {
                CheckProductsCommonDashboardFiles();
            }
            if (hasStyleSheetFolder)
            {
                CheckProductsCommonStyleSheetFiles();
            }
        }

        /// <summary>
        /// Checks whether the required Tools subfolders exist
        /// </summary>
        private void CheckToolsFolderDirectories()
        {
            if (IsInternal())
            {
                if (!toolsFolders.Any(x => x == "ExternalLib"))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "ExternalLib folder for Tools folder not found." + requiredGenericFolderErrorMessage);
                }
                else
                {
                    toolsExternalLibPath = Path.Combine(toolsPath, "ExternalLib") + @"\";
                    CheckToolsExternalLibFiles();
                }
                if (!toolsFolders.Any(x => x == "Selenium"))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Selenium folder for Tools folder not found." + requiredGenericFolderErrorMessage);
                }
                else
                {
                    toolsSeleniumPath = Path.Combine(toolsPath, "Selenium") + @"\";
                    CheckToolsSeleniumFiles();
                }
            }
            if (!toolsFolders.Any(x => x == "TestRunner"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "TestRunner folder for Tools folder not found." + requiredGenericFolderErrorMessage);
            }
            else
            {
                toolsTestRunnerPath = Path.Combine(toolsPath, "TestRunner") + @"\";
                GetCurrentToolsTestRunnerFolders();
                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Init.dat in Tools\TestRunner");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Init.dat in Tools\TestRunner...");
                if (isInitMissing)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"Init.dat in Tools\TestRunner not found.");
                }
                else
                {
                    if (isInitMalformed)
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"Init.dat in Tools\TestRunner is malformed.");
                    }
                    else
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, @"Init.dat in Tools\TestRunner has no issues.");
                    }
                }
                if (productFolders.Count > 0)
                {
                    if (!isInitMissing && !isInitMalformed)
                    {
                        CheckProductLibraryFiles();
                    }
                }
                CheckToolsTestRunnerExecutables();
                CheckToolsTestRunnerBatchFiles();
                CheckToolsTestRunnerBrowserDrivers();
                CheckToolsTestRunnerLibraries();
                CheckToolsTestRunnerMiscellaneousFiles();
            }
        }

        /// <summary>
        /// Checks whether the main TestRunner executable in Tools folder exists
        /// </summary>
        private void CheckMainTestRunnerExecutable()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking main Test Runner executable");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking main Test Runner executable...");
            string testRunnerExecutableError = "TestRunner.exe main executable not found.";
            string testRunnerPath = toolsPath + "TestRunner.exe";
            if (IsFileExisting(testRunnerPath, testRunnerExecutableError))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "TestRunner.exe main executable found.");
            }
        }

        /// <summary>
        /// Checks whether the required product Framework subfolders exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductFrameworkFolders(string product)
        {
            int productFrameworkFolderCount = 0;
            bool hasConfigsFolder = false;
            bool hasLibraryFolder = false;
            bool hasRemoteBrowsersFolder = false;
            bool hasSuiteResultsFolder = false;
            bool hasTestResultsFolder = false;
            string mDirProductFrameworkFolder = Path.Combine(productPath, product + @"\Framework\");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Framework subfolders for " + product + "...");
            List<string> currentFrameworkFolders = new DirectoryInfo(mDirProductFrameworkFolder).GetDirectories()
                .Select(x => x.Name).ToList();
            if (!currentFrameworkFolders.Any(x => x == "Configs"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Configs folder for " + product + "'s Framework folder not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                productFrameworkFolderCount++;
                hasConfigsFolder = true;
            }
            if (currentFrameworkFolders.Any(x => x == "Library"))
            {
                hasLibraryFolder = true;
            }
            if (!currentFrameworkFolders.Any(x => x == "ObjectStore"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "ObjectStore folder for " + product + "'s Framework folder not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                productFrameworkFolderCount++;
            }
            if (!currentFrameworkFolders.Any(x => x == "RemoteBrowsers"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "RemoteBrowsers folder for " + product + "'s Framework folder not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                productFrameworkFolderCount++;
                hasRemoteBrowsersFolder = true;
            }
            if (!currentFrameworkFolders.Any(x => x == "SuiteResults"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "SuiteResults folder for " + product + "'s Framework folder not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                productFrameworkFolderCount++;
                hasSuiteResultsFolder = true;
            }
            if (!currentFrameworkFolders.Any(x => x == "TestResults"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "TestResults folder for " + product + "'s Framework folder not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                productFrameworkFolderCount++;
                hasTestResultsFolder = true;
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productFrameworkFolderCount, PRODUCT_FRAMEWORK_FOLDER_COUNT) + @" required Products\Framework folders found.");
            if (hasConfigsFolder)
            {
                CheckProductSpecificConfigsFiles(product);
            }
            if (hasLibraryFolder)
            {
                CheckProductSpecificLibraryFolders(product);
            }
            if (hasRemoteBrowsersFolder)
            {
                CheckProductSpecificRemoteBrowsersFiles(product);
            }
            if (hasSuiteResultsFolder && DirectoryArgument.RunTestSuiteResult)
            {
                CheckProductSpecificSuiteResultsFiles(product);
            }
            if (hasTestResultsFolder && DirectoryArgument.RunTestScriptResults)
            {
                CheckProductSpecificTestResultsFiles(product);
            }
        }

        /// <summary>
        /// Checks whether the required product UserTestData subfolders exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductUserTestDataFolders(string product)
        {
            int productUserTestDataFolderCount = 0;
            bool hasDataFolder = false;
            bool hasDocDiffFolder = false;
            string mDirProductUserTestDataFolder = Path.Combine(productPath, product + @"\UserTestData\");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking UserTestData subfolders for " + product + "...");
            List<string> currentUserTestDataFolders = new DirectoryInfo(mDirProductUserTestDataFolder).GetDirectories()
                .Select(x => x.Name).ToList();
            if (currentUserTestDataFolders.Any(x => x == "Data"))
            {
                hasDataFolder = true;
            }
            if (!currentUserTestDataFolders.Any(x => x == "DocDiff"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "DocDiff folder for " + product + " in UserTestData not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                productUserTestDataFolderCount++;
                hasDocDiffFolder = true;
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productUserTestDataFolderCount, PRODUCT_USERTESTDATA_FOLDER_COUNT) + @" required UserTestData folders for " + product + " found.");
            if (hasDataFolder)
            {
                CheckUserTestDataDataFiles(product);
            }
            if (hasDocDiffFolder)
            {
                CheckUserTestDataDocDiffFolders(product);
            }
        }

        /// <summary>
        /// Checks whether the required dll files for the downloaded products exist
        /// </summary>
        private void CheckProductLibraryFiles()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking needed library files for each installed product");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking needed library files for each installed product...");
            List<string> skippedProducts = new List<string>();
            foreach (string productFolder in productFolders)
            {
                if (skippedProducts.Contains(productFolder))
                {
                    continue;
                }
                string productString = "";
                List<string> productsWithSameLibraryFile = productLibraryFileStrings.Where(x => x.Value == productLibraryFileStrings[productFolder]).Select(x => x.Key).ToList();
                productString = productsWithSameLibraryFile.Aggregate((x, y) => x + ", " + y).ToString();
                string productLibraryPath = toolsTestRunnerPath + productLibraryFileStrings[productFolder];
                string libraryError = productLibraryFileStrings[productFolder] + " library needed for " + productString + " not found.";
                if (IsFileExisting(productLibraryPath, libraryError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, productLibraryFileStrings[productFolder] + " needed for " + productString + " found.");
                }
                skippedProducts.AddRange(productsWithSameLibraryFile);
            }
        }

        /// <summary>
        /// Checks whether the required Tools\TestRunner executables exist
        /// </summary>
        private void CheckToolsTestRunnerExecutables()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\Test Runner folder executables");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\Test Runner folder executables...");
            int toolsTestRunnerExecutableCount = CountExistingFiles(toolsTestRunnerPath, toolsTestRunnerExecutableStrings, " executable not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerExecutableCount, toolsTestRunnerExecutableStrings.Count) + @" required Tools\TestRunner executables found.");
        }

        /// <summary>
        /// Checks whether the required Tools\TestRunner batch files exist
        /// </summary>
        private void CheckToolsTestRunnerBatchFiles()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\Test Runner batch files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\Test Runner batch files...");
            int toolsTestRunnerBatchFileCount = CountExistingFiles(toolsTestRunnerPath, toolsTestRunnerBatchFileStrings, " batch file not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerBatchFileCount, toolsTestRunnerBatchFileStrings.Count) + @" required Tools\TestRunner batch files found.");
        }

        /// <summary>
        /// Checks whether the required Tools\TestRunner browser drivers exist
        /// </summary>
        private void CheckToolsTestRunnerBrowserDrivers()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\Test Runner webdrivers");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\Test Runner webdrivers...");
            int toolsTestRunnerBrowserDriverCount = CountExistingFiles(toolsTestRunnerPath, toolsTestRunnerBrowserDriverStrings, " webdriver not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerBrowserDriverCount, toolsTestRunnerBrowserDriverStrings.Count) + @" required Tools\TestRunner webdrivers found.");
        }

        /// <summary>
        /// Checks whether the required Tools\TestRunner libraries exist
        /// </summary>
        private void CheckToolsTestRunnerLibraries()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\Test Runner library files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\Test Runner library files...");
            int toolsTestRunnerLibraryCount = CountExistingFiles(toolsTestRunnerPath, toolsTestRunnerLibraryStrings, " library not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerLibraryCount, toolsTestRunnerLibraryStrings.Count) + @" required Tools\TestRunner libraries found.");
        }

        /// <summary>
        /// Checks whether the required Tools\TestRunner miscellaneous files exist
        /// </summary>
        private void CheckToolsTestRunnerMiscellaneousFiles()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\Test Runner miscellaneous files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\Test Runner miscellaneous files...");
            int toolsTestRunnerMiscellaneousFileCount = CountExistingFiles(toolsTestRunnerPath, toolsTestRunnerMiscellaneousFileStrings, " miscellaneous file not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerMiscellaneousFileCount, toolsTestRunnerMiscellaneousFileStrings.Count) + @" required Tools\TestRunner miscellaneous files found.");
            CheckToolsTestRunnerXMLFiles();
        }

        /// <summary>
        /// Checks whether the required Tools\TestRunner\Resources images exist
        /// </summary>
        private void CheckToolsTestRunnerResources()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Test Runner\Resources files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Test Runner\Resources files...");
            int toolsTestRunnerResourceFileCount = CountExistingFiles(toolsTestRunnerResourcesPath, toolsTestRunnerResourcesFileStrings, " resource file not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerResourceFileCount, toolsTestRunnerResourcesFileStrings.Count) + @" required TestRunner resources found.");
        }

        /// <summary>
        /// Checks whether the required Tools\ExternalLib files exist
        /// </summary>
        private void CheckToolsExternalLibFiles()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\ExternalLib files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\ExternalLib files...");
            int toolsExternalLibFileCount = CountExistingFiles(toolsExternalLibPath, toolsExternalLibFileStrings, " in ExternalLib folder is missing.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsExternalLibFileCount, toolsExternalLibFileStrings.Count) + @" required Tools\ExternalLib files found.");
        }

        /// <summary>
        /// Checks whether the required Tools\Selenium files exist
        /// </summary>
        private void CheckToolsSeleniumFiles()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Tools\Selenium files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Tools\Selenium files...");
            int toolsSeleniumFileCount = CountExistingFiles(toolsSeleniumPath, toolsSeleniumFileStrings, " in Selenium folder is missing.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsSeleniumFileCount, toolsSeleniumFileStrings.Count) + @" required Tools\Selenium files found.");
        }

        /// <summary>
        /// Checks whether the required Products\Common\Configs files exist
        /// </summary>
        private void CheckProductsCommonConfigsFiles()
        {
            int commonConfigsFilesCount = 0;
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Products\Common\Configs files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Products\Common\Configs files...");
            string productsCommonConfigsError = @"version_support.xml for Common\Configs not found.";
            string versionSupportPath = productsCommonConfigsPath + "version_support.xml";
            if (IsFileExisting(versionSupportPath, productsCommonConfigsError))
            {
                commonConfigsFilesCount++;
                if (!IsXMLValid(new FileInfo(versionSupportPath), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"version_support.xml in Common\Configs folder is malformed" + xmlLineError);
                }
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(commonConfigsFilesCount, PRODUCTS_COMMON_CONFIGS_FILE_COUNT) + @" required Common\Configs file/s found.");
            string configPath = productsCommonConfigsPath + "config.xml";
            if (IsFileExisting(configPath, ""))
            {
                if (IsFileReadOnly(new FileInfo(configPath)))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"config.xml in Common\Configs folder is read-only.");
                }
                if (!IsXMLValid(new FileInfo(configPath), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, @"config.xml in Common\Configs folder is malformed" + xmlLineError);
                }
            }
        }

        /// <summary>
        /// Checks for any malformed XML files in Scheduler folder
        /// </summary>
        private void CheckProductsCommonSchedulerFiles()
        {
            int commonSchedulerFilesCount = 0;
            string commonSchedulerPath = Path.Combine(productsCommonPath, "Scheduler") + @"\";
            DirectoryInfo folderInfo = new DirectoryInfo(commonSchedulerPath);
            FileInfo[] schedulerXMLFiles = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Products\Common\Scheduler files");
            if (schedulerXMLFiles.Length > 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + schedulerXMLFiles.Length.ToString() + @" XML files in Products\Common\Scheduler...");
                for (int idx = 0; idx < schedulerXMLFiles.Count(); idx++)
                {
                    try
                    {
                        if (!IsXMLValid(schedulerXMLFiles[idx], out string xmlLineError))
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "XML file " + schedulerXMLFiles[idx].Name + @" in Products\Common\Scheduler is malformed" + xmlLineError);
                        }
                        else
                        {
                            commonSchedulerFilesCount++;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(commonSchedulerFilesCount, schedulerXMLFiles.Length) + @" XML file/s in Products\Common\Scheduler have no issues.");
            }
        }

        /// <summary>
        /// Checks whether the required Products\Common\Dashboard files exist
        /// </summary>
        private void CheckProductsCommonDashboardFiles()
        {
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Products\Common\Dashboard files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Products\Common\Dashboard files...");
            int commonDashboardFilesCount = CountExistingFiles(productsCommonDashboardPath, productsCommonDashboardFileStrings, @" file in Products\Common\Dashboard not found.", true);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(commonDashboardFilesCount, productsCommonDashboardFileStrings.Count) + @" Products\Common\Dashboard files found.");
        }

        /// <summary>
        /// Checks whether the required Products\Common\StyleSheet files exist
        /// </summary>
        private void CheckProductsCommonStyleSheetFiles()
        {
            int commonStyleSheetFilesCount = 0;
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking Products\Common\StyleSheet files");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking Products\Common\StyleSheet files...");
            string styleSheetPath = productsCommonStyleSheetPath + "DocDiff.xsl";
            if (IsFileExisting(styleSheetPath, @" DocDiff.xsl for Common\StyleSheet not found."))
            {
                commonStyleSheetFilesCount++;
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(commonStyleSheetFilesCount, PRODUCTS_COMMON_STYLESHEET_FILE_COUNT) + @" required Common\StyleSheet file/s found.");
        }

        /// <summary>
        /// Checks for any malformed XML files in DocDiff Output folder
        /// </summary>
        private void CheckDocDiffOutputFiles(DirectoryInfo outputFolder, string product)
        {
            int outputFolderFilesCount = 0;
            FileInfo[] outputXMLFiles = outputFolder.GetFiles("*.xml", SearchOption.AllDirectories);
            if (outputXMLFiles.Length > 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + outputXMLFiles.Length.ToString() + @" XML files in UserTestData\DocDiff\OutputFile folder for " + product + "...");
                for (int idx = 0; idx < outputXMLFiles.Count(); idx++)
                {
                    try
                    {
                        if (!IsXMLValid(outputXMLFiles[idx], out string xmlLineError))
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "XML file " + outputXMLFiles[idx].Name + @" in DocDiff\OutputFile for " + product + " product is malformed" + xmlLineError);
                        }
                        else
                        {
                            outputFolderFilesCount++;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(outputFolderFilesCount, outputXMLFiles.Length) + @" XML file/s in UserTestData\DocDiff\OutputFile folder for product " + product + " have no issues.");
            }
        }

        /// <summary>
        /// Checks for any malformed XML files in TestRunner folder
        /// </summary>
        private void CheckToolsTestRunnerXMLFiles()
        {
            int toolsTestRunnerFilesCount = 0;
            DirectoryInfo folderInfo = new DirectoryInfo(toolsTestRunnerPath);
            FileInfo[] toolsTestRunnerXMLFiles = folderInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            if (toolsTestRunnerXMLFiles.Length > 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + toolsTestRunnerXMLFiles.Length.ToString() + @" XML files in Tools\TestRunner...");
                for (int idx = 0; idx < toolsTestRunnerXMLFiles.Count(); idx++)
                {
                    try
                    {
                        if (!IsXMLValid(toolsTestRunnerXMLFiles[idx], out string xmlLineError))
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "XML file " + toolsTestRunnerXMLFiles[idx].Name + @" in Tools\TestRunner is malformed" + xmlLineError);
                        }
                        else
                        {
                            toolsTestRunnerFilesCount++;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(toolsTestRunnerFilesCount, toolsTestRunnerXMLFiles.Length) + @" XML file/s in Tools\TestRunner have no issues.");
            }
        }

        /// <summary>
        /// Checks whether the required product Tests files exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductTestsFiles(string product)
        {
            string mDirProductTestsFolder = Path.Combine(productPath, product + @"\Tests\");
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Tests files for " + product);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Tests files for " + product + "...");
            string templatePath = mDirProductTestsFolder + "template.dat";
            if (IsFileExisting(templatePath, "template.dat for " + product + " in Tests folder not found."))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "template.dat for " + product + " in Tests folder found.");
            }
        }

        /// <summary>
        /// Checks whether the required product Framework\Configs files exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductSpecificConfigsFiles(string product)
        {
            int productConfigFileCount = 0;
            int productConfigValidFileCount = 2;
            string mDirProductConfigsFolder = Path.Combine(productPath, product + @"\Framework\Configs\");
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Framework Configs files for " + product);
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Framework Configs files for " + product + "...");
            string loginConfigPath = mDirProductConfigsFolder + "LoginConfig.xml";
            if (IsFileExisting(loginConfigPath, "LoginConfig.xml for " + product + " in Configs folder not found."))
            {
                productConfigFileCount++;
                if (!IsXMLValid(new FileInfo(mDirProductConfigsFolder + "LoginConfig.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "LoginConfig.xml for " + product + " in Configs folder is malformed" + xmlLineError);
                }
            }
            if (!productsWithHelpBox.Contains(product))
            {
                productConfigValidFileCount--;
            }
            else
            {
                string testEditorHelpPath = mDirProductConfigsFolder + "TestEditorHelp.xml";
                if (IsFileExisting(testEditorHelpPath, "TestEditorHelp.xml for " + product + " in Configs folder not found."))
                {
                    productConfigFileCount++;
                    if (!IsXMLValid(new FileInfo(mDirProductConfigsFolder + "TestEditorHelp.xml"), out string xmlLineError))
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "TestEditorHelp.xml for " + product + " in Configs folder is malformed" + xmlLineError);
                    }
                }
            }
            string prodConfigPath = mDirProductConfigsFolder + "ProdConfig.xml";
            if (IsFileExisting(prodConfigPath, ""))
            {
                if (!IsXMLValid(new FileInfo(mDirProductConfigsFolder + "ProdConfig.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "ProdConfig.xml for " + product + " in Configs folder is malformed" + xmlLineError);
                }
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productConfigFileCount, productConfigValidFileCount) + @" required Configs files for " + product + " found.");
        }

        /// <summary>
        /// Checks whether the required product Framework\Library folders exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductSpecificLibraryFolders(string product)
        {
            bool hasQueriesFolder = false;
            bool hasTagsFolder = false;
            int productLibraryFolderCount = 0;
            string mDirProductFrameworkLibraryFolder = Path.Combine(productPath, product + @"\Framework\Library\");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Library subfolders for " + product + "...");
            List<string> currentLibraryFolders = new DirectoryInfo(mDirProductFrameworkLibraryFolder).GetDirectories()
                .Select(x => x.Name).ToList();
            if (currentLibraryFolders.Count > 0)
            {
                if (currentLibraryFolders.Any(x => x == "Queries"))
                {
                    productLibraryFolderCount++;
                    hasQueriesFolder = true;
                }
                if (currentLibraryFolders.Any(x => x == "Tags"))
                {
                    productLibraryFolderCount++;
                    hasTagsFolder = true;
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productLibraryFolderCount, PRODUCT_LIBRARY_FOLDER_COUNT) + @" valid Library folders for " + product + " found.");                
                if (hasQueriesFolder)
                {
                    DirectoryInfo folderInfo = new DirectoryInfo(mDirProductFrameworkLibraryFolder + @"Queries\");
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Framework Library Queries files for " + product);
                    CheckLibraryQueriesFiles(folderInfo);
                }
                if (hasTagsFolder)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Framework Library Tags files for " + product);
                    CheckLibraryTagsFiles(product);
                }
            }
        }

        /// <summary>
        /// Checks whether the required product Framework\Library\Tags files don't have issues
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckLibraryTagsFiles(string product)
        {
            int productLibraryTagsFileCount = 0;
            string mDirProductLibraryTagsFolder = Path.Combine(productPath, product + @"\Framework\Library\Tags\");
            string tagsPath = mDirProductLibraryTagsFolder + "tags.xml";
            if (IsFileExisting(mDirProductLibraryTagsFolder + "tags.xml", ""))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Library tags.xml for " + product + "...");
                productLibraryTagsFileCount++;
                if (!IsXMLValid(new FileInfo(mDirProductLibraryTagsFolder + "tags.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "tags.xml for " + product + " in Library/Tags folder is malformed" + xmlLineError);
                }
                else
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "tags.xml for " + product + " in Library/Tags folder doesn't have issues.");
                }
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productLibraryTagsFileCount, PRODUCT_LIBRARY_TAGS_FILE_COUNT) + @" required Tags files for " + product + " found.");
        }

        /// <summary>
        /// Checks whether the required product Framework\Library\Queries files don't have issues
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckLibraryQueriesFiles(DirectoryInfo queriesFolder)
        {
            FileInfo[] queryFiles = queriesFolder.GetFiles("*.xml");
            if (queryFiles.Any())
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking available query files in Queries folder for issues...");
                foreach (FileInfo queryFile in queryFiles)
                {
                    if (!IsXMLValid(queryFile, out string xmlLineError))
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.WARNING, queryFile.Name + " query file is malformed" + xmlLineError);
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether any DocDiff config files have issues
        /// </summary>
        /// <param name="configFolder">ConfigFile folder for DocDiff</param>
        private void CheckDocDiffConfigFileFiles(DirectoryInfo configFolder)
        {
            FileInfo[] configFiles = configFolder.GetFiles("*.xml");
            if (configFiles.Any())
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Config files found in ConfigFile folder. Checking files for issues...");
                int validConfigFileCount = 0;
                foreach (FileInfo configFile in configFiles)
                {
                    if (!IsXMLValid(configFile, out string xmlLineError))
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.WARNING, configFile.Name + " config file is malformed" + xmlLineError);
                    }
                    else
                    {
                        validConfigFileCount++;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(validConfigFileCount, configFiles.Length) + @" config files found to have no issues.");
            }
            
        }

        /// <summary>
        /// Checks whether the required product Framework\RemoteBrowsers files exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductSpecificRemoteBrowsersFiles(string product)
        {
            int productRemoteBrowsersFileCount = 0;
            string mDirProductRemoteBrowsersFolder = Path.Combine(productPath, product + @"\Framework\RemoteBrowsers\");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking Framework RemoteBrowsers files for " + product + "...");
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Framework RemoteBrowsers files for " + product);
            string mobileConfigPath = mDirProductRemoteBrowsersFolder + "MobileConfig.xml";
            if (IsFileExisting(mobileConfigPath, "MobileConfig.xml for " + product + " in RemoteBrowsers folder not found."))
            {
                productRemoteBrowsersFileCount++;
                if (!IsXMLValid(new FileInfo(mDirProductRemoteBrowsersFolder + "MobileConfig.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "MobileConfig.xml for " + product + " in RemoteBrowsers folder is malformed" + xmlLineError);
                }
            }
            string remoteBrowsersPath = mDirProductRemoteBrowsersFolder + "RemoteBrowsers.xml";
            if (IsFileExisting(remoteBrowsersPath, "RemoteBrowsers.xml for " + product + " in RemoteBrowsers folder not found."))
            {
                productRemoteBrowsersFileCount++;
                if (!IsXMLValid(new FileInfo(mDirProductRemoteBrowsersFolder + "RemoteBrowsers.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "RemoteBrowsers.xml for " + product + " in RemoteBrowsers folder is malformed" + xmlLineError);
                }
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(productRemoteBrowsersFileCount, PRODUCT_REMOTEBROWSERS_FILE_COUNT) + @" required RemoteBrowsers files for " + product + " found.");
        }

        /// <summary>
        /// Checks whether the product Framework\SuiteResults files don't have issues
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductSpecificSuiteResultsFiles(string product)
        {
            string mDirProductSuiteResultsFolder = Path.Combine(productPath, product + @"\Framework\SuiteResults\");
            string suiteManifestPath = mDirProductSuiteResultsFolder + "SuiteResultsManifest.xml";
            bool hasSuiteResultsManifest = false;
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Framework SuiteResults files for " + product);
            if (IsFileExisting(suiteManifestPath, ""))
            {
                hasSuiteResultsManifest = true;
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "SuiteResultsManifest found for " + product + ", checking file...");
                if (IsXMLValid(new FileInfo(mDirProductSuiteResultsFolder + "SuiteResultsManifest.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "SuiteResultsManifest.xml for " + product + " has no XML errors.");
                    if (IsFileReadOnly(new FileInfo(mDirProductSuiteResultsFolder + "SuiteResultsManifest.xml")))
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "SuiteResultsManifest.xml for " + product + " in SuiteResults folder is read-only.");
                    }
                    else
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "SuiteResultsManifest.xml for " + product + " is not read-only.");
                    }
                }
                else
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "SuiteResultsManifest.xml for " + product + " is malformed" + xmlLineError);
                }
            }
            int suiteResultsFilesCount = 0;
            DirectoryInfo folderInfo = new DirectoryInfo(mDirProductSuiteResultsFolder);
            FileInfo[] suiteResultsXMLFiles = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
            suiteResultsFilesCount = suiteResultsXMLFiles.Length;
            if (hasSuiteResultsManifest)
            {
                List<FileInfo> listToClean = new List<FileInfo>(suiteResultsXMLFiles);
                for (int index = listToClean.Count - 1; index >= 0; index--)
                {
                    if (listToClean[index].Name.ToLower() == "suiteresultsmanifest.xml")
                    {
                        listToClean.RemoveAt(index);
                        break;
                    }
                }
                suiteResultsXMLFiles = listToClean.ToArray();
            }
            if (suiteResultsXMLFiles.Length > 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + suiteResultsXMLFiles.Length.ToString() + @" XML file/s in SuiteResults for " + product + "...");
                for (int idx = 0; idx < suiteResultsXMLFiles.Count(); idx++)
                {
                    try
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.FILEPROGRESS, "Checking " + (idx+1).ToString() + " out of " + suiteResultsXMLFiles.Count().ToString() + " SuiteResults XML files for " + product);
                        if (!IsXMLValid(suiteResultsXMLFiles[idx], out string xmlLineError))
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "XML file " + suiteResultsXMLFiles[idx].Name + @" in SuiteResults folder for " + product + " is malformed" + xmlLineError);
                        }
                        else
                        {
                            suiteResultsFilesCount++;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(suiteResultsFilesCount, suiteResultsXMLFiles.Length) + @" XML file/s in SuiteResults folder for " + product + " have no issues.");
            }
        }

        /// <summary>
        /// Checks whether the product Framework\TestResults files don't have issues
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckProductSpecificTestResultsFiles(string product)
        {
            bool hasTestResultsManifest = false;
            string mDirProductTestResultsFolder = Path.Combine(productPath, product + @"\Framework\TestResults\");
            string testManifestPath = mDirProductTestResultsFolder + "tr_results_manifest.xml";
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking Framework TestResults files for " + product);
            if (IsFileExisting(testManifestPath, ""))
            {
                hasTestResultsManifest = true;
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "tr_results_manifest found for " + product + ", checking file...");
                if (IsXMLValid(new FileInfo(mDirProductTestResultsFolder + "tr_results_manifest.xml"), out string xmlLineError))
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "tr_results_manifest.xml for " + product + " has no XML errors.");
                    if (IsFileReadOnly(new FileInfo(mDirProductTestResultsFolder + "tr_results_manifest.xml")))
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "tr_results_manifest.xml for " + product + " in TestResults folder is read-only.");
                    }
                    else
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "tr_results_manifest.xml for " + product + " is not read-only.");
                    }
                }
                else
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "tr_results_manifest.xml for " + product + " is malformed" + xmlLineError);
                }
            }
            int testResultsFilesCount = 0;
            DirectoryInfo folderInfo = new DirectoryInfo(mDirProductTestResultsFolder);
            FileInfo[] testResultsXMLFiles = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
            if (hasTestResultsManifest)
            {
                List<FileInfo> listToClean = new List<FileInfo>(testResultsXMLFiles);
                for (int index = listToClean.Count - 1; index >= 0; index--)
                {
                    if (listToClean[index].Name.ToLower() == "tr_results_manifest.xml")
                    {
                        listToClean.RemoveAt(index);
                        break;
                    }
                }
                testResultsXMLFiles = listToClean.ToArray();
            }
            if (testResultsXMLFiles.Length > 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + testResultsXMLFiles.Length.ToString() + @" XML files in TestResults for " + product + "...");
                for (int idx = 0; idx < testResultsXMLFiles.Count(); idx++)
                {
                    try
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.FILEPROGRESS, "Checking " + (idx+1).ToString() + " out of " + testResultsXMLFiles.Count().ToString() + " TestResults XML files for " + product);
                        if (!IsXMLValid(testResultsXMLFiles[idx], out string xmlLineError))
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "XML file " + testResultsXMLFiles[idx].Name + @" in TestResults folder for " + product + " is malformed" + xmlLineError);
                        }
                        else
                        {
                            testResultsFilesCount++;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(testResultsFilesCount, testResultsXMLFiles.Length) + @" XML file/s in TestResults folder for " + product + " have no issues.");
            }
        }

        /// <summary>
        /// Checks whether the product has any valid global variable files
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckUserTestDataDataFiles(string product)
        {
            string mDirProductUserTestDataDataFolder = Path.Combine(productPath, product + @"\UserTestData\Data\");
            DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking UserTestData\Data files for " + product);
            FileInfo[] userTestDataDataFiles = new DirectoryInfo(mDirProductUserTestDataDataFolder).GetFiles("globalvar*.csv");
            if (userTestDataDataFiles.Length > 0)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking UserTestData\Data files for " + product + "...");
                DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, userTestDataDataFiles.Length.ToString() + " valid global variable file/s for " + product + @" in UserTestData\Data folder found.");
            }
            else
            {
                DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "No valid global variable file for " + product + @" in UserTestData\Data folder found.");
            }
        }

        /// <summary>
        /// Checks whether the required product UserTestData\DocDiff folders exist
        /// </summary>
        /// <param name="product">product name</param>
        private void CheckUserTestDataDocDiffFolders(string product)
        {
            int userTestDataDocDiffFolderCount = 0;
            bool hasConfigFilesFolder = false;
            bool hasOutputFileFolder = false;
            string mDirProductUserTestDataDocDiffFolder = Path.Combine(productPath, product + @"\UserTestData\DocDiff\");
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, @"Checking UserTestData\DocDiff subfolders for " + product + "...");
            List<string> currentDocDiffFolders = new DirectoryInfo(mDirProductUserTestDataDocDiffFolder).GetDirectories()
                .Select(x => x.Name).ToList();
            if (!currentDocDiffFolders.Any(x => x == "ActualFile"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "ActualFile folder for " + product + " in DocDiff not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                userTestDataDocDiffFolderCount++;
            }
            if (!currentDocDiffFolders.Any(x => x == "ConfigFile"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "ConfigFile folder for " + product + " in DocDiff not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                userTestDataDocDiffFolderCount++;
                hasConfigFilesFolder = true;
            }
            if (!currentDocDiffFolders.Any(x => x == "ExpectedFile"))
            {
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "ExpectedFile folder for " + product + " in DocDiff not found." + requiredProductFolderErrorMessage);
            }
            else
            {
                userTestDataDocDiffFolderCount++;
            }
            if (currentDocDiffFolders.Any(x => x == "OutputFile"))
            {
                hasOutputFileFolder = true;
            }
            DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(userTestDataDocDiffFolderCount, PRODUCT_USERTESTDATA_DOCDIFF_FOLDER_COUNT) + @" required UserTestData\DocDiff folders for " + product + " found.");
            if (hasConfigFilesFolder)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking DocDiff\ConfigFile files for " + product);
                CheckDocDiffConfigFileFiles(new DirectoryInfo(mDirProductUserTestDataDocDiffFolder + "ConfigFile"));
            }
            if (hasOutputFileFolder)
            {
                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking DocDiff\OutputFile files for " + product);
                CheckDocDiffOutputFiles(new DirectoryInfo(mDirProductUserTestDataDocDiffFolder + "OutputFile"), product);
            }
        }

        /// <summary>
        /// Checks for Costpoint family product OS files that have missing alias attribute
        /// </summary>
        private void CheckOutdatedOSFiles()
        {
            foreach (string validProduct in productsWithAlias)
            {
                if (productFolders.Contains(validProduct))
                {
                    int totalValidOSFiles = 0;
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking outdated and malformed OS files for Costpoint-family product " + validProduct);
                    string objectStorePath = productPath + validProduct + @"\Framework\ObjectStore\";
                    if (!Directory.Exists(objectStorePath))
                    {
                        continue;
                    }
                    DirectoryInfo folderInfo = new DirectoryInfo(objectStorePath);
                    FileInfo[] objectStoreFiles = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
                    int totalObjectStoreFiles = objectStoreFiles.Length;
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + objectStoreFiles.Length.ToString() + " Object Store files for " + validProduct + " product...");
                    for (int idx = 0; idx < objectStoreFiles.Count(); idx++)
                    {
                        try
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.FILEPROGRESS, "Checking " + (idx + 1).ToString() + " out of " + objectStoreFiles.Count().ToString() + " Object Store files for " + validProduct);
                            if (IsXMLValid(objectStoreFiles[idx], out string xmlLineError))
                            {
                                if (IsOSFileValid(objectStoreFiles[idx]) == null)
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, "File " + objectStoreFiles[idx].Name + " in product folder " + validProduct + " is not an Object Store file, and will be ignored.");
                                    totalObjectStoreFiles--;
                                }
                                else if ((bool)IsOSFileValid(objectStoreFiles[idx]))
                                {
                                    totalValidOSFiles++;
                                }
                                else
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Object Store file " + objectStoreFiles[idx].Name + " in product folder " + validProduct + " is outdated.");
                                }
                            }
                            else
                            {
                                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Object Store file " + objectStoreFiles[idx].Name + " in product folder " + validProduct + " is malformed" + xmlLineError);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(totalValidOSFiles, totalObjectStoreFiles) + " OS file/s in product folder " + validProduct + " have no issues.");
                }
            }
        }

        /// <summary>
        /// Checks whether non-Costpoint family products have any malformed OS files
        /// </summary>
        private void CheckMalformedOSFiles()
        {
            foreach (string validProduct in productFolders)
            {
                if (!productsWithAlias.Contains(validProduct))
                {
                    int totalValidOSFiles = 0;
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking malformed OS files for product " + validProduct);
                    string objectStorePath = productPath + validProduct + @"\Framework\ObjectStore\";
                    if (!Directory.Exists(objectStorePath))
                    {
                        continue;
                    }
                    DirectoryInfo folderInfo = new DirectoryInfo(objectStorePath);
                    FileInfo[] objectStoreFiles = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
                    int totalObjectStoreFiles = objectStoreFiles.Length;
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + objectStoreFiles.Length.ToString() + " Object Store files for " + validProduct + " product...");
                    for (int idx = 0; idx < objectStoreFiles.Count(); idx++)
                    {
                        try
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.FILEPROGRESS, "Checking " + (idx + 1).ToString() + " out of " + objectStoreFiles.Count().ToString() + " Object Store files for " + validProduct);
                            if (IsXMLValid(objectStoreFiles[idx], out string xmlLineError))
                            {
                                if (IsOSFileValid(objectStoreFiles[idx]) == null)
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, "File " + objectStoreFiles[idx].Name + " in product folder " + validProduct + " is not an Object Store file, and will be ignored.");
                                    totalObjectStoreFiles--;
                                }
                                else
                                {
                                    totalValidOSFiles++;
                                }
                            }
                            else
                            {
                                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Object Store file " + objectStoreFiles[idx].Name + " in product folder " + validProduct + " is malformed" + xmlLineError);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(totalValidOSFiles, totalObjectStoreFiles) + " OS file/s in product folder " + validProduct + " have no issues.");
                }
            }
        }

        /// <summary>
        /// Checks for tests that have malformed XML structure
        /// </summary>
        private void CheckMalformedTests()
        {
            foreach (string productFolder in productFolders)
            {
                int totalValidTests = 0;
                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking malformed tests for product " + productFolder);
                string productTestPath = productPath + productFolder + @"\Tests\";
                if (!Directory.Exists(productTestPath))
                {
                    continue;
                }
                DirectoryInfo folderInfo = new DirectoryInfo(productTestPath);
                FileInfo[] testScripts = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
                if (testScripts.Length > 0)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + testScripts.Length.ToString() + " test scripts in Tests folder of " + productFolder + " product...");
                    for (int idx = 0; idx < testScripts.Count(); idx++)
                    {
                        try
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.FILEPROGRESS, "Checking " + (idx + 1).ToString() + " out of " + testScripts.Count().ToString() + " test scripts for " + productFolder);
                            if (IsXMLValid(testScripts[idx], out string xmlLineError))
                            {
                                totalValidTests++;
                            }
                            else
                            {
                                DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "Test script file " + testScripts[idx].Name + " in product folder " + productFolder + " is malformed" + xmlLineError);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(totalValidTests, testScripts.Length) + " tests don't have issues.");
                }
            }
        }

        /// <summary>
        /// Checks for suites that have malformed XML structure
        /// </summary>
        private void CheckMalformedSuites()
        {
            foreach (string productFolder in productFolders)
            {
                try
                {
                    int totalValidSuites = 0;
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, @"Checking malformed suites for product " + productFolder);
                    string productSuitePath = productPath + productFolder + @"\Suites\";
                    if (!Directory.Exists(productSuitePath))
                    {
                        continue;
                    }
                    DirectoryInfo folderInfo = new DirectoryInfo(productSuitePath);
                    FileInfo[] suiteFiles = folderInfo.GetFiles("*.xml", SearchOption.AllDirectories);
                    if (suiteFiles.Length > 0)
                    {
                        string mDirEnvironmentFile = Path.Combine(productPath, productFolder + @"\Framework\Configs\LoginConfig.xml");
                        envs = GetEnvironments(mDirEnvironmentFile);

                        if (envs.Count == 0 || envs == null)
                        {
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, string.Format("LoginConfig.xml is missing or empty. Unable to proceed with environment checks for Suites in {0} product", productFolder), false);
                        }

                        _missingEnvironment = new List<MissingEnvironment>();
                        DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for issues with " + suiteFiles.Length.ToString() + " suites in Suites folder of " + productFolder + " product...");
                        for (int idx = 0; idx < suiteFiles.Count(); idx++)
                        {
                            try
                            {
                                _missingTestScript = new List<MissingTest>();
                                DiagnosticLogger.LogResult(Logger.MessageType.FILEPROGRESS, "Checking " + (idx + 1).ToString() + " out of " + suiteFiles.Count().ToString() + " suite files for " + productFolder);
                                if (IsXMLValid(suiteFiles[idx], out string xmlLineError))
                                {
                                    totalValidSuites++;
                                    CheckValidSuiteContent(suiteFiles[idx], productFolder);
                                }
                                else
                                {
                                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "Suite file " + suiteFiles[idx].Name + " in product folder " + productFolder + " is malformed" + xmlLineError);
                                }

                                if (_missingTestScript.Count() > 0)
                                {
                                    GenerateMissingTestLog(_missingTestScript, productFolder);
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        if (_missingEnvironment.Count() > 0)
                        {
                            GenerateMissingEnvLog(_missingEnvironment, productFolder);
                        }

                        DiagnosticLogger.LogResult(Logger.MessageType.INFO, DisplayTotalInfoMessage(totalValidSuites, suiteFiles.Length) + " suites don't have issues.");
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Check if the environment and test file exist on the given suite
        /// </summary>
        /// <param name="suiteFile">Suite file to check</param>
        private void CheckValidSuiteContent(FileInfo suiteFile, string productFolder)
        {
            string mDirProductTestsFolder = Path.Combine(productPath, productFolder + @"\Tests");

            XDocument mXmlDoc = XDocument.Load(suiteFile.FullName);
            var data = from doc in mXmlDoc.Descendants("test")
                       select new
                       {
                           folder = doc.Attribute("folder") != null ? doc.Attribute("folder").Value : "",
                           file = doc.Attribute("file") != null ? doc.Attribute("file").Value : "",
                           environment = doc.Attribute("environment") != null ? doc.Attribute("environment").Value : ""
                       };

            List<string> missingEnv = new List<string>();
            foreach (var val in data)
            {   
                string filePath = string.Format(@"{0}{1}\{2}", mDirProductTestsFolder.Trim(), val.folder.Trim(), val.file.Trim());
                if (!File.Exists(filePath))
                {
                    if (!_missingTestScript.Any(x => x.SuiteName.Equals(suiteFile.FullName.Trim())
                         && x.TestScript.Equals(filePath.Trim())))
                    {
                        _missingTestScript.Add(new MissingTest()
                        {
                            SuiteName = suiteFile.FullName.Trim(),
                            TestScript = filePath.Trim()
                        });
                    }
                }

                if (envs.Count == 0 || envs == null)
                {
                    continue;
                }

                if (!envs.Any(x => x == val.environment))
                {
                    if (!missingEnv.Any(x => x == val.environment))
                    {
                        missingEnv.Add(val.environment);
                        _missingEnvironment.Add(new MissingEnvironment()
                        {
                            EnvironmentName = val.environment.Trim(),
                            SuiteName = suiteFile.FullName.Trim()
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Generate log entry for missing environment per product
        /// </summary>
        /// <param name="missingEnv">list missing environment per suite in a product</param>
        private void GenerateMissingEnvLog(List<MissingEnvironment> missingEnv, string product)
        {
            List<string> distinctEnv = missingEnv.Select(x => x.EnvironmentName).Distinct().ToList();
            for(int i = 0; i < distinctEnv.Count(); i++)
            {
                string log = "";
                log = string.Format("Environment '{0}' from '{1}' product does not exist. The following suites are affected:\n", distinctEnv[i], product);
                foreach(var env in missingEnv)
                {
                    if(env.EnvironmentName.Trim() == distinctEnv[i].Trim())
                    {
                        string logSuite = string.Format("\t\u2022{0}\n", env.SuiteName.Trim());
                        log = log + logSuite;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, log, false);
            }
        }

        /// <summary>
        /// Generate log entry for missing test script per suite
        /// </summary>
        /// <param name="missingTest">list of missing test script per suite</param>
        private void GenerateMissingTestLog(List<MissingTest> missingTest, string product)
        {
            List<string> distinctSuite = missingTest.Select(x => x.SuiteName).Distinct().ToList();
            for (int i = 0; i < distinctSuite.Count(); i++)
            {
                string log = "";
                log = string.Format("Suite '{0}' from '{1}' product contains the following deleted or missing test scripts:\n", distinctSuite[i], product);
                foreach (var test in missingTest)
                {
                    if (test.SuiteName.Trim() == distinctSuite[i].Trim())
                    {
                        string logSuite = string.Format("\t\u2022{0}\n", test.TestScript.Trim());
                        log = log + logSuite;
                    }
                }
                DiagnosticLogger.LogResult(Logger.MessageType.ERROR, log, false);
            }
        }

        /// <summary>
        /// Get the environment list from LoginConfig.xml
        /// </summary>
        /// <param name="envPath">login path</param>
        /// <returns>return the list of environment from LoginConfig</returns>
        private List<string> GetEnvironments(string envPath)
        {
            try
            {
                List<string> envs = new List<string>();

                XDocument xDoc = XDocument.Load(envPath);
                var data = from doc in xDoc.Descendants("login")
                           select new
                           {
                               id = doc.Attribute("id") != null ? doc.Attribute("id").Value : ""
                           };

                foreach (var val in data)
                {
                    envs.Add(val.id);
                }

                return envs;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Checks for outdated OS files
        /// </summary>
        /// <param name="objectStore">OS file to check</param>
        /// <returns>whether or not OS file has alias</returns>
        private bool? IsOSFileValid(FileInfo objectStore)
        {
            bool? ret = true;
            XDocument mXmlDoc = XDocument.Load(objectStore.FullName);
            var data = from doc in mXmlDoc.Descendants("objectstore")
                       select new
                       {
                           screen = doc.Attribute("screen")?.Value,
                           alias = doc.Attribute("alias")?.Value
                       };
            foreach (var val in data)
            {
                if (val.screen == null)
                {
                    ret = null;
                }
                else if (val.alias == null)
                {
                    ret = false;
                }
            }
            return ret;
        }

        /// <summary>
        /// Checks for malformed XMLs
        /// </summary>
        /// <param name="targetXML">XML file to check</param>
        /// <returns>whether or not XML file is malformed</returns>
        private bool IsXMLValid(FileInfo targetXML, out string lineError)
        {
            bool ret = true;
            lineError = ".";
            try
            {
                XDocument mXmlDoc = XDocument.Load(targetXML.FullName);
            }
            catch (XmlException e)
            {
                string errorMessage = e.Message;
                int positionIndex = errorMessage.LastIndexOf(", position", StringComparison.Ordinal);
                int lineIndex = errorMessage.LastIndexOf("Line ", StringComparison.Ordinal);
                lineError = (positionIndex > 0 && lineIndex > 0) ? " in line " + errorMessage.Substring(lineIndex, positionIndex - lineIndex).Replace("Line ", "") + "." : ".";
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Checks for read-only files
        /// </summary>
        /// <param name="fileToCheck">file to check</param>
        /// <returns>whether or not file is read-only</returns>
        private bool IsFileReadOnly(FileInfo fileToCheck)
        {
            bool ret = false;
            if (fileToCheck.IsReadOnly)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Checks for existence of files
        /// </summary>
        /// <param name="filePath">path of file to check</param>
        /// <param name="errorMessage">error message to display if file doesn't exist</param>
        /// <param name="isWarning">whether or not to log a warning message instead</param>
        /// <returns>whether or not file exists</returns>
        private bool IsFileExisting(string filePath, string errorMessage, bool isWarning = false)
        {
            bool ret = false;
            FileInfo file = new FileInfo(filePath);
            if (File.Exists(file.FullName))
            {
                ret = true;
            }
            else
            {
                if (errorMessage != "")
                {
                    DiagnosticLogger.LogResult(isWarning ? Logger.MessageType.WARNING : Logger.MessageType.ERROR, errorMessage);
                }
            }
            return ret;
        }

        /// <summary>
        /// Checks multiple files and returns the number of files found to be existing
        /// </summary>
        /// <param name="basePath">base path of file</param>
        /// <param name="fileList">list of file names to check</param>
        /// <param name="errorMessage">error message to display if file doesn't exist</param>
        /// <param name="addFileToErrorMessage">whether or not to append file name to beginning of error message</param>
        /// <param name="isWarning">whether or not to log a warning message instead</param>
        /// <returns>number of files found to be existing</returns>
        private int CountExistingFiles(string basePath, List<string> fileList, string errorMessage, bool addFileToErrorMessage = false, bool isWarning = false)
        {
            int total = 0;
            foreach (string file in fileList)
            {
                string error = errorMessage;
                error = addFileToErrorMessage ? file + errorMessage : errorMessage;
                if (IsFileExisting(basePath + file, error, isWarning))
                {
                    total++;
                }
            }
            return total;
        }

        /// <summary>
        /// Constructs a string composed of a quantity compared to a total number
        /// </summary>
        /// <param name="quantity">first number, usually the quantity to compare to a total</param>
        /// <param name="total">second number, total number to compare the quantity to</param>
        /// <returns>constructed string of comparison of a quantity over a total number</returns>
        private string DisplayTotalInfoMessage(int quantity, int total)
        {
            return quantity + " out of " + total;
        }

        /// <summary>
        /// Retrieves folders from a path, then removes folders that are not in a list
        /// </summary>
        /// <param name="listOfValidFolders">list of valid folders to serve as basis of items to retain</param>
        /// <param name="basePath">base path of folders to retrieve from</param>
        /// <returns>list of valid folders</returns>
        private List<string> RemoveInvalidFolders(List<string> listOfValidFolders, string basePath)
        {
            List<string> listToClean = new DirectoryInfo(basePath).GetDirectories()
                .Select(x => x.Name).ToList();
            for (int index = listToClean.Count - 1; index >= 0; index--)
            {
                if (!listOfValidFolders.Contains(listToClean[index]))
                {
                    listToClean.RemoveAt(index);
                }
            }
            return listToClean;
        }

        /// <summary>
        /// Checks if Test Runner is potentially running in an internal release folder or not
        /// </summary>
        /// <returns>whether or not Test Runner is potentially an internal release</returns>
        private bool IsInternal()
        {
            bool ret = false;
            if (mainPath.Contains("QEAutomation") || mainPath.Contains("TFS"))
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Calculates the test count for test progress
        /// </summary>
        /// <returns>Test count used for test progress</returns>
        private int CalculateTestCount()
        {
            int ret = TEST_FIXED_CHECKS_COUNT;
            int totalProductFolders = 0;
            mainPath = GetRootPath(TestRunnerPath);
            initPath = mainPath + @"\Tools\TestRunner\Init.dat";
            InitializeProductsWithAlias();
            if (IsFileExisting(initPath, ""))
            {
                isInitMissing = false;
                if (IsXMLValid(new FileInfo(initPath), out string xmlLineError))
                {
                    isInitMalformed = false;
                    InitializeProductLibraryFiles(initPath);
                }
                else
                {
                    return ret;
                }
            }
            string mDirProductsRoot = Path.Combine(mainPath, "Products") + @"\";
            if (!Directory.Exists(mDirProductsRoot))
            {
                return ret;
            }
            productPath = mDirProductsRoot;
            productFolders = new DirectoryInfo(productPath).GetDirectories()
                .Select(x => x.Name).ToList();
            if (productFolders.Contains("Common"))
            {
                hasCommonFolder = true;
            }
            for (int index = productFolders.Count - 1; index >= 0; index--)
            {
                if (!productLibraryFileStrings.ContainsKey(productFolders[index]))
                {
                    productFolders.RemoveAt(index);
                }
            }
            totalProductFolders = productFolders.Count;
            ret += totalProductFolders * TEST_PRODUCTS_MULTIPLIER;
            return ret;
        }

        private static string GetRootPath(string testRunnerPath)
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
    }

    public class MissingTest
    {
        public string SuiteName { get; set; }
        public string TestScript { get; set; }
    }

    public class MissingEnvironment
    {
        public string EnvironmentName { get; set; }
        public string SuiteName { get; set; }
    }
}
