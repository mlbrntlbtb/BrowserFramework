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

namespace CommonLib.DlkHandlers
{
    public class DlkProductConfigHandler
    {
        #region DECLARATIONS
        private string mProdConfig;
        private XDocument mProdConfigXDoc;
        #endregion

        #region METHODS
        public DlkProductConfigHandler(string ProductConfigPath)
        {
            mProdConfig = ProductConfigPath;
            CreateConfigFile(mProdConfig);
            mProdConfigXDoc = XDocument.Load(mProdConfig);

            /* Delete obsolete nodes */
            DeleteNodeIfExists("defaultbrowser");
        }

        /// <summary>
        /// Checks if the node element exist on the onfig file
        /// </summary>
        /// <param name="Node">Element to verify if exist</param>
        /// <returns></returns>
        public bool ConfigNodeExists(string Node)
        {
            var item = mProdConfigXDoc.Root.Element(Node);
            return mProdConfigXDoc.Root.Element(Node) != null;
        }

        /// <summary>
        /// Get the value of the element
        /// </summary>
        /// <param name="Node">Element to get the value</param>
        /// <returns></returns>
        public string GetConfigValue(string Node)
        {
            var item = mProdConfigXDoc.Root.Element(Node);
            return item == null ? string.Empty : item.Value;
        }

        /// <summary>
        /// Update the value of the specified element
        /// </summary>
        /// <param name="Node">Element</param>
        /// <param name="NodeValue"> Element value</param>
        public void UpdateConfigValue(string Node, string NodeValue)
        {
            var element = mProdConfigXDoc.Root.Element(Node);

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
                mProdConfigXDoc.Root.Add(childNode);
            }

            mProdConfigXDoc.Save(mProdConfig);
            mProdConfigXDoc = XDocument.Load(mProdConfig);
        }

        /// <summary>
        /// Delete the node that is not needed
        /// </summary>
        /// <param name="Node">Element</param>
        public void DeleteNodeIfExists(string Node)
        {
            var element = mProdConfigXDoc.Root.Element(Node);

            if (element != null)
            {
                element.Remove();
                mProdConfigXDoc.Save(mProdConfig);
                mProdConfigXDoc = XDocument.Load(mProdConfig);
            }
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
                File.WriteAllText(FilePath, "<prodconfig />");
            }
        }
        #endregion
    }
}
