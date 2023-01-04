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

namespace WorkBookLib.DlkSystem
{
    public class DlkWorkBookReleaseConfigHandler
    {
        #region DECLARATIONS
        private string mReleaseConfig;
        private XDocument mReleaseConfigXDoc;
        #endregion

        #region METHODS
        public DlkWorkBookReleaseConfigHandler(string ReleaseConfigPath)
        {
            mReleaseConfig = ReleaseConfigPath;
            CreateConfigFile(mReleaseConfig);
            mReleaseConfigXDoc = XDocument.Load(mReleaseConfig);
        }

        /// <summary>
        /// Checks if the node element exist on the onfig file
        /// </summary>
        /// <param name="Node">Element to verify if exist</param>
        /// <returns></returns>
        public bool ConfigNodeExists(string Node)
        {
            var item = mReleaseConfigXDoc.Root.Element(Node);
            return mReleaseConfigXDoc.Root.Element(Node) != null;
        }

        /// <summary>
        /// Get the value of the element
        /// </summary>
        /// <param name="Node">Element to get the value</param>
        /// <returns></returns>
        public string GetConfigValue(string Node)
        {
            var item = mReleaseConfigXDoc.Root.Element(Node);
            return item == null ? string.Empty : item.Value;
        }

        /// <summary>
        /// Update the value of the specified element
        /// </summary>
        /// <param name="Node">Element</param>
        /// <param name="NodeValue"> Element value</param>
        public void UpdateConfigValue(string Node, string NodeValue)
        {
            var element = mReleaseConfigXDoc.Root.Element(Node);

            if (element != null)
            {
                element.Value = NodeValue;
            }
            else
            {
                //create the child node on XML
                XElement childNode = new XElement(Node)
                {
                    Value = NodeValue
                };
                mReleaseConfigXDoc.Root.Add(childNode);
            }

            mReleaseConfigXDoc.Save(mReleaseConfig);
            mReleaseConfigXDoc = XDocument.Load(mReleaseConfig);
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
                File.WriteAllText(FilePath, "<releaseconfig />");
            }
        }
        #endregion
    }
}
