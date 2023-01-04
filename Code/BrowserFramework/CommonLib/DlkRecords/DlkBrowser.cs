using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkBrowser
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Alias { get; set; }
        public string BrowserType { get; set; }
        public string DriverVersion { get; set; }
        public bool DefaultBrowser { get; set; }

        /// <summary>
        /// constructs a new browser record without default values
        /// </summary>
        public DlkBrowser()
        {

        }

        /// <summary>
        /// constructs a new browser record defaulting the Name and BrowserType
        /// </summary>
        public DlkBrowser(string name)
        {
            this.BrowserType = GetBrowserType(name);
            this.Name = name;
        }

        /// <summary>
        /// constructs a new browser record defaulting the Name and BrowserType
        /// </summary>
        public DlkBrowser(string type, string name)
        {
            this.BrowserType = type;
            this.Name = name;
        }

        /// <summary>
        /// Returns the browser type based on the supplied browser name
        /// </summary>
        private string GetBrowserType(string browserName)
        {
            foreach (DlkBrowser browser in DlkEnvironment.mAvailableBrowsers)
            {
                if (browser.Alias == browserName)
                    return "Web";
            }

            foreach (DlkMobileRecord mob in DlkMobileHandler.mMobileRec)
            {
                if (mob.MobileId == browserName)
                    return "Mobile";
            }

            foreach (DlkRemoteBrowserRecord rec in DlkRemoteBrowserHandler.mRemoteBrowsers)
            {
                if (rec.Id == browserName)
                    return "Remote";
            }
            
            return "";
        }
    }
}
