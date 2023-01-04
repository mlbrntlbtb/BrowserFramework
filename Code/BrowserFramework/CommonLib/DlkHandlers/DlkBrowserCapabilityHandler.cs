using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.DlkHandlers
{
    public class DlkBrowserCapabilityHandler
    {
        /// <summary>
        /// Browser capabilities config filename 
        /// </summary>
        const string BROWSER_CAPABILITY_FILENAME = "BrowserCapabilities.config";

        #region Private Members
        private readonly string trPath;
        private readonly bool prodPathExists;
        private readonly string productPath;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for browser capabilities handler.
        /// Creates browser config file in tools folder if file not found in product framework config and tools folder 
        /// </summary>
        public DlkBrowserCapabilityHandler()
        {
            try
            {
                trPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), BROWSER_CAPABILITY_FILENAME);
                productPath = Path.Combine(DlkEnvironment.mDirFramework, "Configs", BROWSER_CAPABILITY_FILENAME);

                if (File.Exists(productPath))
                {
                    prodPathExists = true;
                }
                else
                {
                    if (!File.Exists(trPath))
                    {
                        CreateBrowserCapabilityConfig();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Get browser capabilities from BrowserCapabilities.config
        /// </summary>
        /// <returns>List of browser capabilities</returns>
        public List<DlkBrowserCapability> GetBrowserCapabilities()
        {
            string path = prodPathExists ? productPath : trPath;
            if (File.Exists(path))
            {
                var capabilities = XDocument.Load(path);
                var mBrowserCapabilities = new List<DlkBrowserCapability>();

                foreach (XElement browserElement in capabilities.Descendants("browser"))
                {
                    string browser = browserElement.Attribute("name").Value;

                    foreach (XElement optionElement in browserElement.Descendants("option"))
                    {
                        string type = optionElement.Attribute("type")?.Value;
                        string name = optionElement.Attribute("name")?.Value;
                        string nodeText = optionElement.Value;

                        switch (type)
                        {
                            case DlkBrowserCapability.CHROME_ARGUMENT:
                            case DlkBrowserCapability.CHROME_EXCLUDED_ARGUMENT:
                                mBrowserCapabilities.Add(new DlkBrowserCapability(browser, nodeText, type));
                                break;
                            case DlkBrowserCapability.CHROME_ADDITIONAL_CAPABILITY:
                            case DlkBrowserCapability.CHROME_USER_PROFILE_PREFERENCE:
                                mBrowserCapabilities.Add(new DlkBrowserCapability(browser, name, nodeText, type));
                                break;
                            default:
                                throw new Exception($"Browser capability option type '{type}' is not supported.");
                        }
                    }
                }
                return mBrowserCapabilities;
            }
            else
            {
                throw new Exception("Browser capabilities config file does not exist: " + path);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Create browser capabilities config
        /// </summary>
        private void CreateBrowserCapabilityConfig()
        {
            try
            {
                List<XElement> lstBrowsers = new List<XElement>();

                List<XElement> lstChromeCapabilities = new List<XElement>
                {
                     new XElement("option", new XAttribute("type", "argument"), "--test-type"),
                     new XElement("option", new XAttribute("type", "argument"), "--start-maximized=true"),
                     new XElement("option", new XAttribute("type", "excludedargument"), "enable-automation"),
                     new XElement("option", new XAttribute("type", "additionalcapability"), new XAttribute("name", "useAutomationExtension"), "false"),
                     new XElement("option", new XAttribute("type", "userprofilepreference"), new XAttribute("name", "credentials_enable_service"), "false"),
                     new XElement("option", new XAttribute("type", "userprofilepreference"), new XAttribute("name", "profile.password_manager_enabled"), "false"),
                };

                lstBrowsers.Add(new XElement("browser", new XAttribute("name", "chrome"), lstChromeCapabilities));

                XElement config = new XElement("configuration", lstBrowsers);
                XDocument xDoc = new XDocument(config);
                xDoc.Save(trPath);
            }
            catch
            {
                throw new Exception($"Failed to create browser capabilities config file: {trPath}");
            }
        }
        #endregion
    }
}
