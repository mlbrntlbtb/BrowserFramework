using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// Mobile OS class to use for selecting supported Android/iOS versions
    /// </summary>
    public class DlkMobileOSRecord
    {
        public const string STR_XML_VERSIONS = "versions";
        public const string STR_XML_VERSION = "version";
        public const string STR_XML_VERSION_TYPE = "type";
        public const string STR_XML_VERSION_TARGET = "target";
        public const string STR_XML_API = "api";
        public const string STR_XML_NAME = "name";

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="OsType">Android or iOS</param>
        /// <param name="OsTarget">Version number (4.2, 4.2.1, etc)</param>
        /// <param name="OsApi">API Level (if applicable)</param>
        /// <param name="OsName">Name</param>
        public DlkMobileOSRecord(string OsType, string OsTarget, string OsName, string OsApi = "")
        {
            Type = OsType;
            Target = OsTarget;
            Api = OsApi;
            Name = OsName;
        }

        /// <summary>
        /// OS Type: Android/iOS
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Version number
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// API Level (if applicable)
        /// </summary>
        public string Api { get; set; }

        /// <summary>
        /// Name of OS Version
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Load all supported versions
        /// </summary>
        /// <returns>All version profiles in version_support file</returns>
        public static List<DlkMobileOSRecord> LoadVersionList()
        {
            /* Check if version_support file is existing */
            if (!File.Exists(DlkEnvironment.mVersionSupportListPath))
            {
                throw new Exception("Version support list does not exist.");
            }

            XDocument doc = XDocument.Load(DlkEnvironment.mVersionSupportListPath);

            var versions = from version in doc.Descendants(STR_XML_VERSION)
                       select new
                       {
                           type = version.Attribute(STR_XML_VERSION_TYPE).Value.ToString(),
                           target = version.Attribute(STR_XML_VERSION_TARGET).Value.ToString(),
                           api = version.Attribute(STR_XML_API).Value.ToString(),
                           name = version.Attribute(STR_XML_NAME).Value.ToString()
                       };

            /* Get Rows */
            List<DlkMobileOSRecord> lstVersions = new List<DlkMobileOSRecord>();

            foreach (var itm in versions)
            {
                DlkMobileOSRecord ver = new DlkMobileOSRecord(itm.type, itm.target, itm.api, itm.name);
                lstVersions.Add(ver);
            }
            return lstVersions;
        }
    }
}
