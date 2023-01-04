using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using CommonLib.DlkUtility;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using System.Xml.XPath;

namespace WorkBookLib.DlkSystem
{
    public class DlkWorkBookRerunConfigHandler
    {
        #region DECLARATIONS
        private string mRerunConfig;
        private string mBuildID;
        private XDocument mRerunConfigXDoc;
        private XElement mRerunDescendant;
        public bool isInitialRun = false;
        #endregion

        #region METHODS
        public DlkWorkBookRerunConfigHandler(string RerunConfigPath, string BuildID)
        {
            mRerunConfig = RerunConfigPath;
            mBuildID = BuildID;
            CreateConfigFile(mRerunConfig);
            mRerunConfigXDoc = XDocument.Load(mRerunConfig);
            mRerunDescendant = mRerunConfigXDoc.XPathSelectElement("//build[@id='" + BuildID + "']");
        }

        /// <summary>
        /// Checks if the node element exist on the onfig file
        /// </summary>
        /// <param name="Node">Element to verify if exist</param>
        /// <returns></returns>
        public bool ConfigBuildExists()
        {
            return mRerunDescendant != null;
        }

        /// <summary>
        /// Get the value of the element
        /// </summary>
        /// <param name="Node">Element to get the value</param>
        /// <returns></returns>
        public string GetConfigValue(string Node)
        {
            var item = mRerunDescendant.Element(Node);
            return item == null ? string.Empty : item.Value;
        }

        /// <summary>
        /// Update the value of the specified element
        /// </summary>
        /// <param name="Node">Element</param>
        /// <param name="NodeValue"> Element value</param>
        public void UpdateRerunConfig()
        {
            mRerunConfigXDoc = XDocument.Load(mRerunConfig);
            mRerunConfigXDoc.Root.Add(new XElement("build",
                   new XAttribute("id", mBuildID),
                   new XElement("latestrundate", DateTime.Now.ToString("MM/dd/yyyy")),
                   new XElement("latestruntime", DateTime.Now.ToString("hh:mm tt"))
                   )
                   );

            mRerunConfigXDoc.Save(mRerunConfig);
        }

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Creates configuration file if not yet existing
        /// </summary>
        /// <param name="FilePath">File path</param>
        private void CreateConfigFile(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "<rerunconfig />");
                UpdateRerunConfig();
                isInitialRun = true;
            }
        }
        #endregion
    }
}
