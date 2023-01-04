using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;

namespace CommonLib.DlkHandlers
{
    public static class DlkTestRunnerSettingsHandler
    {
        #region DECLARATIONS
        private static bool mInitialized;
        private static readonly string INIT_FILE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Init.dat");
        private static DlkDecryptionHelper decryptor = new DlkDecryptionHelper();
        #endregion

        #region PROPERTIES

        public static DlkTargetApplication ApplicationUnderTest { get; set; }
        public static bool IsFirstTimeLaunch { get; set; }
        public static bool NeedsRefresh { get; set; }
        public static List<DlkTargetApplication> ApplicationList { get; set; }
        public static string Release { get; set; }
        public static string ReleaseVersion { get; set; }

        #endregion

        #region METHODS

        public static void Initialize(string FrameworkRootPath)
        {
            if (!mInitialized)
            {
                XDocument init = LoadInitFile(INIT_FILE, 10);

                //#if DEBUG

                //                var applist = from app in init.Descendants("application")
                //                              .Where(x => x.Attribute("type").Value != "external")
                //#else
                //                var applist = from app in init.Descendants("application")
                //                              .Where(x => x.Attribute("type").Value != "internal")
                //#endif
                var applist = from app in init.Descendants("application")
                              select new
                              {
                                  id = app.Attribute("id").Value,
                                  type = app.Attribute("type").Value,
                                  name = app.Attribute("name").Value,
                                  version = app.Attribute("version").Value,
                                  library = app.Attribute("library").Value,
                                  productfolder = app.Attribute("productfolder").Value,
                                  platform = app.Attribute("platform").Value
                              };

                /* Set application list */
                ApplicationList = new List<DlkTargetApplication>();

                foreach (var app in applist)
                {
                    if (CompleteProductFiles(FrameworkRootPath, app.productfolder))
                    {

                        ApplicationList.Add(new DlkTargetApplication
                            {
                                ID = app.id,
                                Name = app.name,
                                Version = app.version,
                                Library = app.library,
                                ProductFolder = app.productfolder,
                                Type = app.type,
                                Platform = app.platform
                            }
                            );
                    }
                }

                ApplicationList = ApplicationList.OrderBy(x => x.Name).ToList();

                /*Set Default Configurations from INIT File*/
                SetDefaultConfigurations();

                XDocument cfg = XDocument.Load(DlkConfigHandler.MainConfig);

                /* Set current application */
                var currAppId = cfg.Descendants("currentapp").First().Value;
                ApplicationUnderTest = ApplicationList.Find(x => x.ID == currAppId);
                IsFirstTimeLaunch = Convert.ToBoolean(cfg.Descendants("initiallaunch").First().Value);
                Release = cfg.Descendants("release").First().Value;
                ReleaseVersion = cfg.Descendants("releaseversion").First().Value;
                mInitialized = true;
            }
        }

        private static XDocument LoadInitFile(string path, int timeout)
        {
            XDocument ret = null;
            for (int i = 1; i < timeout; i++)
            {
                try
                {
                    ret = XDocument.Load(path);
                    break;
                }
                catch(IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {
                    throw;
                }
            }
            return ret;
        }

        private static bool CompleteProductFiles(string rootPath, string productFolder)
        {
            string dirProductRoot = Path.Combine(rootPath, "Products") + @"\";
            if (!Directory.Exists(dirProductRoot))
            {
                return false;
            }

            string dirTools = Path.Combine(rootPath, "Tools") + @"\";
            if (!Directory.Exists(dirTools))
            {
                return false;
            }

            string dirProduct = Path.Combine(dirProductRoot, productFolder) + @"\";
            if (!Directory.Exists(dirProduct))
            {
                return false;
            }

            string dirFramework = Path.Combine(dirProduct, "Framework") + @"\";
            if (!Directory.Exists(dirFramework))
            {
                return false;
            }

            string dirObjectStore = Path.Combine(dirFramework, "ObjectStore") + @"\";
            if (!Directory.Exists(dirObjectStore))
            {
                return false;
            }

            string dirRemoteBrowsers = Path.Combine(dirFramework, "RemoteBrowsers") + @"\";
            if (!Directory.Exists(dirRemoteBrowsers))
            {
                return false;
            }

            string dirTestSuite = Path.Combine(dirProduct, "Suites") + @"\";
            if (!Directory.Exists(dirTestSuite))
            {
                return false;
            }

            string dirTests = Path.Combine(dirProduct, "Tests") + @"\";
            if (!Directory.Exists(dirTests))
            {
                return false;
            }

            string dirUserData = Path.Combine(dirProduct, "UserTestData") + @"\";
            if (!Directory.Exists(dirUserData))
            {
                return false;
            }

            string dirDocDiff = Path.Combine(dirUserData, "DocDiff") + @"\";
            if (!Directory.Exists(dirDocDiff))
            {
                return false;
            }

            string dirDocDiffActualFile = Path.Combine(dirDocDiff, "ActualFile") + @"\";
            if (!Directory.Exists(dirDocDiffActualFile))
            {
                return false;
            }

            string dirDocDiffConfigFile = Path.Combine(dirDocDiff, "ConfigFile") + @"\";
            if (!Directory.Exists(dirDocDiffConfigFile))
            {
                return false;
            }

            string dirDocDiffExpectedFile = Path.Combine(dirDocDiff, "ExpectedFile") + @"\";
            if (!Directory.Exists(dirDocDiffExpectedFile))
            {
                return false;
            }
            string dirTestResults = Path.Combine(dirFramework, "TestResults") + @"\";
            if (!Directory.Exists(dirTestResults))
            {
                return false;
            }

            string dirSuiteResults = Path.Combine(dirFramework, "SuiteResults") + @"\";
            if (!Directory.Exists(dirSuiteResults))
            {
                return false;
            }

            string dirConfigs = Path.Combine(dirFramework, "Configs") + @"\";
            if (!Directory.Exists(dirConfigs))
            {
                return false;
            }

            string loginConfigFile = Path.Combine(dirConfigs, @"LoginConfig.xml");

            if (!File.Exists(loginConfigFile))
            {
                return false;
            }

            string remoteBrowsersFile = Path.Combine(dirRemoteBrowsers, @"RemoteBrowsers.xml");

            if (!File.Exists(remoteBrowsersFile))
            {
                return false;
            }

            string mobileConfigFile = Path.Combine(dirRemoteBrowsers, @"MobileConfig.xml");
            if (!File.Exists(mobileConfigFile))
            {
                return false;
            }
            return true;
        }

        public static void Save()
        {
            XElement currentApp = new XElement("currentapp");
            currentApp.Value = ApplicationUnderTest.ID;

            XElement initialLaunch = new XElement("initiallaunch");
            initialLaunch.Value = IsFirstTimeLaunch.ToString();

            List<XElement> elementList = new List<XElement>();
            elementList.Add(currentApp);
            elementList.Add(initialLaunch);

            DlkConfigHandler.UpdateMultipleNodeInConfig(DlkConfigHandler.MainConfig, elementList);
        }

        public static void SetDefaultConfigurations()
        {
            XDocument init = XDocument.Load(INIT_FILE);
            if (!DlkConfigHandler.ConfigExists("currentapp"))
                DlkConfigHandler.UpdateConfigValue("currentapp", init.Descendants("currentapp").First().Value);
            if (!DlkConfigHandler.ConfigExists("initiallaunch"))
                DlkConfigHandler.UpdateConfigValue("initiallaunch", init.Descendants("initiallaunch").First().Value);            
            if (!DlkConfigHandler.ConfigExists("sourcecontrolenabled"))
                DlkConfigHandler.UpdateConfigValue("sourcecontrolenabled", init.Descendants("sourcecontrolenabled").First().Value);
            if (!DlkConfigHandler.ConfigExists("donotcheckinenvironmentinfo"))
                DlkConfigHandler.UpdateConfigValue("donotcheckinenvironmentinfo", init.Descendants("donotcheckinenvironmentinfo").First().Value);
            if (!DlkConfigHandler.ConfigExists("getlatestversiononlaunch"))
                DlkConfigHandler.UpdateConfigValue("getlatestversiononlaunch", init.Descendants("getlatestversiononlaunch").First().Value);
            if (!DlkConfigHandler.ConfigExists("testtreeexpanded"))
                DlkConfigHandler.UpdateConfigValue("testtreeexpanded", init.Descendants("testtreeexpanded").First().Value);
            if (!DlkConfigHandler.ConfigExists("errorloglevel"))
                DlkConfigHandler.UpdateConfigValue("errorloglevel", init.Descendants("errorloglevel").First().Value);
            if (!DlkConfigHandler.ConfigExists("smtphost"))
                DlkConfigHandler.UpdateConfigValue("smtphost", init.Descendants("smtphost").First().Value);
            if (!DlkConfigHandler.ConfigExists("smtpport"))
                DlkConfigHandler.UpdateConfigValue("smtpport", init.Descendants("smtpport").First().Value);

            /* SMTP username/pass check */
            if (!DlkConfigHandler.ConfigExists("smtpuser"))
            {
                DlkConfigHandler.UpdateConfigValue("smtpuser", decryptor.IsDecrpytable(init.Descendants("smtpuser").First().Value) 
                    ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(init.Descendants("smtpuser").First().Value)) 
                    : init.Descendants("smtpuser").First().Value);
            }
            else /* ensure that saved value on config is decrypted, just re-save */
            {
                DlkConfigHandler.UpdateConfigValue("smtpuser", DlkConfigHandler.GetConfigValue("smtpuser"));
            }
            if (!DlkConfigHandler.ConfigExists("smtppass"))
            {
                DlkConfigHandler.UpdateConfigValue("smtppass", decryptor.IsDecrpytable(init.Descendants("smtppass").First().Value)
                    ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(init.Descendants("smtppass").First().Value))
                    : init.Descendants("smtppass").First().Value);
            }
            else /* ensure that saved value on config is decrypted, just re-save */
            {
                DlkConfigHandler.UpdateConfigValue("smtppass", DlkConfigHandler.GetConfigValue("smtppass"));
            }
            /* end */
            if (!DlkConfigHandler.ConfigExists("usecustomsenderadd"))
                DlkConfigHandler.UpdateConfigValue("usecustomsenderadd", init.Descendants("usecustomsenderadd").First().Value);
            if (!DlkConfigHandler.ConfigExists("customsenderadd"))
                DlkConfigHandler.UpdateConfigValue("customsenderadd", init.Descendants("customsenderadd").First().Value);
            if (!DlkConfigHandler.ConfigExists("defaultsenderadd"))
                DlkConfigHandler.UpdateConfigValue("defaultsenderadd", init.Descendants("defaultsenderadd").First().Value);
            if (!DlkConfigHandler.ConfigExists("usedefaultemail"))
                DlkConfigHandler.UpdateConfigValue("usedefaultemail", init.Descendants("usedefaultemail").First().Value);
            if (!DlkConfigHandler.ConfigExists("defaultemail"))
                DlkConfigHandler.UpdateConfigValue("defaultemail", init.Descendants("defaultemail").First().Value);
            //dashboard settings
            if (!DlkConfigHandler.ConfigExists("usedatabase"))
                DlkConfigHandler.UpdateConfigValue("usedatabase", init.Descendants("usedatabase").First().Value);
            if (!DlkConfigHandler.ConfigExists("recordresults"))
                DlkConfigHandler.UpdateConfigValue("recordresults", init.Descendants("recordresults").First().Value);
            if (!DlkConfigHandler.ConfigExists("dbserver"))
                DlkConfigHandler.UpdateConfigValue("dbserver", init.Descendants("dbserver").First().Value);
            if (!DlkConfigHandler.ConfigExists("dbname"))
                DlkConfigHandler.UpdateConfigValue("dbname", init.Descendants("dbname").First().Value);      
            DlkConfigHandler.UpdateConfigValue("dbuser", decryptor.IsDecrpytable(init.Descendants("dbuser").First().Value)
                ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(init.Descendants("dbuser").First().Value))
                : init.Descendants("dbuser").First().Value);
            DlkConfigHandler.UpdateConfigValue("dbpassword", decryptor.IsDecrpytable(init.Descendants("dbpassword").First().Value)
                ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(init.Descendants("dbpassword").First().Value))
                : init.Descendants("dbpassword").First().Value);
            if (!DlkConfigHandler.ConfigExists("dashboardenabled"))
                DlkConfigHandler.UpdateConfigValue("dashboardenabled", init.Descendants("dashboardenabled").First().Value);
            //if (!DlkConfigHandler.ConfigExists("dbuser"))
            //    DlkConfigHandler.UpdateConfigValue("dbuser", init.Descendants("dbuser").First().Value);
            //if (!DlkConfigHandler.ConfigExists("dbpassword"))
            //    DlkConfigHandler.UpdateConfigValue("dbpassword", init.Descendants("dbpassword").First().Value);
            if (!DlkConfigHandler.ConfigExists("maxeditorwindows"))
                DlkConfigHandler.UpdateConfigValue("maxeditorwindows", init.Descendants("maxeditorwindows").First().Value);
            if (!DlkConfigHandler.ConfigExists("defaultbrowser"))
                DlkConfigHandler.UpdateConfigValue("defaultbrowser", "");
            if (!DlkConfigHandler.ConfigExists("release"))
                DlkConfigHandler.UpdateConfigValue("release", init.Descendants("release").First().Value);
            /* Always update release version based on Init.dat info */
            DlkConfigHandler.UpdateConfigValue("releaseversion", init.Descendants("releaseversion").First().Value);
        }
        #endregion
    }
}
