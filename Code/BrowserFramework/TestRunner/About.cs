/* uncomment if TR version to display is 2.x.x format */
//#define THREE_TOKEN_TR_VERSION
//#define ATK_RELEASE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using CommonLib.DlkUtility;
using TestRunner.Common;

namespace TestRunner
{
    partial class About : Form
    {
        private Assembly m_Launcher;

        private enum WebDriverType
        {
            IE,
            Gecko,
            Chrome,
            Edge
        }

        public About()
        {
            try
            {
                InitializeComponent();

                m_Launcher = System.Reflection.Assembly.GetExecutingAssembly();
                PopulateExpiryDate();
                this.Text = String.Format("About");
                this.labelProductName.Text = AssemblyProduct;
                this.lblApplicationValue.Text = DlkTestRunnerSettingsHandler.ApplicationUnderTest == null ? ""
                    : DlkTestRunnerSettingsHandler.ApplicationUnderTest.DisplayName;
                this.labelVersion.Text = String.IsNullOrWhiteSpace(DlkTestRunnerSettingsHandler.Release) ?
                    $"Version {AssemblyFullVersion}" :
                    $"Version {AssemblyFullVersion} ({DlkTestRunnerSettingsHandler.Release} Version)";
                this.lblDescription.Text = AssemblyDescription;
                PopulateDriverList();
            }
            catch
            {
                /* Do nothing */
            }
        }

        private string GetDriverVersion(WebDriverType driver)
        {
            string ret = string.Empty;
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string tempFile = Path.Combine(path, Guid.NewGuid().ToString() + ".log");
            string driverPath = string.Empty;
            string targetToken = string.Empty;

            switch (driver)
            {
                case WebDriverType.IE:
                    driverPath = "IEDriverServer.exe";
                    targetToken = "IEDriverServer.exe";
                    break;
                case WebDriverType.Chrome:
                    driverPath = "chromedriver.exe";
                    targetToken = "ChromeDriver";
                    break;
                case WebDriverType.Gecko:
                    driverPath = "geckodriver.exe";
                    targetToken = "geckodriver";
                    break;
                case WebDriverType.Edge:
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
                ret = " - " + tokens[tokens.IndexOf(targetToken) + 1];
                File.Delete(tempFile);
            }
            return ret;
        }

        public string GetLauncherAssemblyVersion()
        {
            return m_Launcher.GetName().Version.ToString();
        }

        public string GetLauncherProductName()
        {
            return m_Launcher.GetName().Name;
        }

        private void PopulateDriverList()
        {
            lstDrivers.Items.Add("Selenium WebDriver - " + SeleniumVersion);
            lstDrivers.Items.Add("IEDriverServer (Microsoft Internet Explorer)" + GetDriverVersion(WebDriverType.IE));
            lstDrivers.Items.Add("GeckoDriver (Mozilla Firefox)" + GetDriverVersion(WebDriverType.Gecko));
            lstDrivers.Items.Add("ChromeDriver (Google Chrome)" + GetDriverVersion(WebDriverType.Chrome));
            lstDrivers.Items.Add("MSEdgeDriver (Microsoft Edge)" + GetDriverVersion(WebDriverType.Edge));

        }

        /// <summary>
        /// Populates the info on license expiry date
        /// </summary>
        private void PopulateExpiryDate()
        {
            DateTime currentDate = DateTime.Now.Date;
            int daysBeforeExpiry = (TestRunner.EnterProductKey.expiryDate - currentDate).Days;
            lblExpiry.Text = $"License key applied: Expires in {daysBeforeExpiry.ToString()} day/s";
        }

        private void lnkUpdateLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                EnterProductKey licenseDlg = new EnterProductKey(true);
                if ((bool)licenseDlg.ShowDialog())
                {
                    PopulateExpiryDate();             
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {

                string ret = m_Launcher.GetName().Version.ToString();
#if THREE_TOKEN_TR_VERSION
#else
                ret = ret.Substring(0, ret.LastIndexOf("."));
#endif
                return ret.Substring(0, ret.LastIndexOf("."));
            }
        }

        /// <summary>
        /// Displays the full version you can see in About Page
        /// </summary>
        public string AssemblyFullVersion
        {
            get
            {
                return $"{AssemblyVersion}.{DlkTestRunnerSettingsHandler.ReleaseVersion}";
            }
        }

        public string SeleniumVersion
        {
            get
            {
                string ret = DlkAssemblyHandler.GetVersion(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WebDriver.dll"));
                const int SELENIUM_VER_LIMIT = 3; /* Based on observed Selenium versioning convention, to avoid inifinite loop */
                int trimCount = 0;

                while (ret.Last() == '0' && trimCount++ < SELENIUM_VER_LIMIT)
                {
                    ret = ret.Substring(0, ret.LastIndexOf("."));
                }
                return ret;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = m_Launcher.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = m_Launcher.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = m_Launcher.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = m_Launcher.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
