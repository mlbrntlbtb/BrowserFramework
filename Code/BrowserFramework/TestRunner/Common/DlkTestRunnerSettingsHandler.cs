using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner.Common
{
    public static class DlkTestRunnerSettingsHandler
    {
        private static bool mInitialized = false;
        private static readonly string INIT_FILE = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Init.dat");
        private static readonly string CONFIG_FILE = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.xml");
        public static void Initialize(string FrameworkRootPath)
        {
            if (!mInitialized)
            {
                XDocument init = XDocument.Load(INIT_FILE);
#if DEBUG

                var applist = from app in init.Descendants("application")
                              .Where(x => x.Attribute("type").Value != "external")
#else
                var applist = from app in init.Descendants("application")
                              .Where(x => x.Attribute("type").Value != "internal")
#endif
                              select new
                              {
                                   id = app.Attribute("id").Value,
                                   type = app.Attribute("type").Value,
                                   name = app.Attribute("name").Value,
                                   version = app.Attribute("version").Value,
                                   library = app.Attribute("library").Value,
                                   productfolder = app.Attribute("productfolder").Value
                              };

                /* Set application list */
                ApplicationList = new List<DlkTargetApplication>();
                
                foreach (var app in applist)
                {
                    if (Directory.Exists(Path.Combine(FrameworkRootPath, "Products", app.productfolder)))
                    {
                        ApplicationList.Add(new DlkTargetApplication
                            {
                                ID = app.id,
                                Name = app.name,
                                Version = app.version,
                                Library = app.library,
                                ProductFolder = app.productfolder,
                                Type = app.type
                            }
                            );
                    }
                }

                ApplicationList = ApplicationList.OrderBy(x => x.Name).ToList();
                // create
                if (!File.Exists(CONFIG_FILE))
                {
                    /* Create config from init */
                    XElement currentApp = new XElement("currentapp");
                    currentApp.Value = init.Descendants("currentapp").First().Value;

                    XElement initialLaunch = new XElement("initiallaunch");
                    initialLaunch.Value = init.Descendants("initiallaunch").First().Value;

                    XElement root = new XElement("config", currentApp, initialLaunch);
                    XDocument cfgFile = new XDocument(root);
                    cfgFile.Save(CONFIG_FILE);
                }

                XDocument cfg = XDocument.Load(CONFIG_FILE);

                /* Set current application */
                var currAppId = cfg.Descendants("currentapp").First().Value;
                ApplicationUnderTest = ApplicationList.Find(x => x.ID == currAppId);
                IsFirstTimeLaunch = Convert.ToBoolean(cfg.Descendants("initiallaunch").First().Value);
                mInitialized = true;
            }
        }

        public static void Save()
        {
            XElement currentApp = new XElement("currentapp");
            currentApp.Value = ApplicationUnderTest.ID.ToString();

            XElement initialLaunch = new XElement("initiallaunch");
            initialLaunch.Value = IsFirstTimeLaunch.ToString();

            XElement root = new XElement("config", currentApp, initialLaunch);

            XDocument cfg = new XDocument(root);
            cfg.Save(CONFIG_FILE);
        }

        public static DlkTargetApplication ApplicationUnderTest
        {
            get;
            set;
        }

        public static bool IsFirstTimeLaunch
        {
            get;
            set;
        }

        public static bool NeedsRefresh
        {
            get;
            set;
        }

        //public static bool IsSourceConytolEnabled
        //{
        //    get;
        //    set;
        //}

        //public static bool IsTestExplorerTreeExpanded
        //{
        //    get;
        //    set;
        //}

        public static List<DlkTargetApplication> ApplicationList
        {
            get;
            set;
        }

        public static XDocument GenerateSummaryResults()
        {
            
            XElement suite;

            int TotalPassed = 0;
            int TotalFailed = 0;
            int TotalNotRun = 0;

            XElement root = new XElement("suiteresults");
            string[] suiteResults = Directory.GetDirectories(DlkEnvironment.mDirSuiteResults);
            for (int i = 0; i < suiteResults.Count(); i++)
            {
                suite = new XElement("suite");
                suite.Add(new XAttribute("name", suiteResults[i]));

                string resultFolder = "";
                List<DlkExecutionQueueRecord> results = DlkTestSuiteResultsFileHandler.GetLatestSuiteResults(Path.Combine(DlkEnvironment.mDirSuiteResults, suiteResults[i]), out resultFolder);
                int NotRun = 0;
                int Passed = 0;
                int Failed = 0;
                for(int j=0; j < results.Count; j++)
                {
                    XElement testresult = new XElement("test");
                    testresult.Add(new XAttribute("name", results[j].name));
                    testresult.Add(new XAttribute("instance", results[j].instance));
                    testresult.Add(new XAttribute("status", results[j].teststatus));
                    switch(results[j].teststatus)
                    {
                        case "Passed":
                            Passed++;
                            break;
                        case "Failed":
                            Failed++;
                            break;
                        default:
                            NotRun++;
                            break;
                    }

                    suite.Add(testresult);

                }

                suite.Add(new XAttribute("passed", Passed.ToString()));
                suite.Add(new XAttribute("failed", Failed.ToString()));
                suite.Add(new XAttribute("notrun", NotRun.ToString()));

                root.Add(suite);
                TotalPassed = TotalPassed + Passed;
                TotalFailed = TotalFailed + Failed;
                TotalNotRun = TotalNotRun + NotRun;
            }

            root.Add(new XAttribute("machine", Environment.MachineName));
            root.Add(new XAttribute("date", DateTime.Today.ToShortDateString()));
            root.Add(new XAttribute("passed", TotalPassed.ToString()));
            root.Add(new XAttribute("failed", TotalFailed.ToString()));
            root.Add(new XAttribute("notrun", TotalNotRun.ToString()));

            XDocument doc = new XDocument(root);
            return doc;
        }
    }
}
