using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using CommonLib.DlkUtility;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    public class DlkConfigHandler
    {
        #region DECLARATIONS
        private const int DEFAULT_LOAD_TIMEOUT = 10;
        public static readonly string mainConfig = Path.Combine(GetConfigRoot("Configs"), "config.xml");
        private static DlkEncryptionHelper encryptor = new DlkEncryptionHelper();
        private static DlkDecryptionHelper decryptor = new DlkDecryptionHelper();
        public static string MainConfig
        {
            get{ return mainConfig;}
        }

        #endregion

        #region PROPERTIES
        
        public static bool ConfigExists(string Node)
        {
            bool nodeExists = false;
            if (File.Exists(MainConfig))
            {
                var configFile = LoadConfigFile(MainConfig, DEFAULT_LOAD_TIMEOUT);
                CheckRootElement(configFile);
                var item = configFile.Root.Element(Node);
                if (item != null)
                {
                    nodeExists = true;
                }
            }
            return nodeExists;
        }

        /// <summary>
        /// holds the test results db connection info
        /// </summary>
        public static DlkResultsDatabaseConfigRecord mResultsDbConfigRecord
        {
            get
            {
                return GetDatabaseConfigRecord();
            }
        }

        /// <summary>
        /// Used for test results logging; determines if we are logging or not
        /// </summary>
        public static Boolean mResultsDbRecordResults
        {
            get
            {
                bool ret = Boolean.TryParse(GetConfigValue("recordresults"), out ret) ? ret : false;
                return ret;
            }
        }

        /// <summary>
        /// Used for checking if Dashboard feature is enabled
        /// </summary>
        public static Boolean mDashboardEnabled
        {
            get
            {
                bool ret = Boolean.TryParse(GetConfigValue("dashboardenabled"), out ret) ? ret : false;
                return ret;
            }
        }

        /// <summary>
        /// Used for checking if tags are to be included in test/suite search
        /// </summary>
        public static Boolean mTagSearchEnabled
        {
            get
            {
                bool ret = Boolean.TryParse(GetConfigValue("includetagsinsearch"), out ret) ? ret : false;
                return ret;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Saves config file with default xml node
        /// </summary>
        /// <param name="ConfigPath">Path of Config File</param>
        public static void Save(String ConfigPath)
        {
            XElement config = new XElement("config");
            XDocument xDoc = new XDocument(config);
            SaveConfigFile(ConfigPath, xDoc, DEFAULT_LOAD_TIMEOUT);
        }

        public static string GetConfigRoot(string ConfigType)
        {
            string binDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mRootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(mRootPath)
                .GetDirectories().Count(x => x.FullName.Contains("Products")) == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }

            var mDirProductRoot = Path.Combine(mRootPath, "Products") + @"\";
            if (!Directory.Exists(mDirProductRoot))
            {
                Directory.CreateDirectory(mDirProductRoot);
                //throw new Exception("Required product root directory does not exist: " + mDirProductRoot);
            }

            var mDirCommonRoot = Path.Combine(mDirProductRoot, "Common") + @"\";
            if (!Directory.Exists(mDirCommonRoot))
            {
                Directory.CreateDirectory(mDirCommonRoot);
                //throw new Exception("Required common config root directory does not exist: " + mDirCommonRoot);
            }

            var mdirConfigRoot = string.Empty;
            switch (ConfigType.ToLower())
            {
                case "scheduler":
                    mdirConfigRoot = Path.Combine(mDirCommonRoot, "Scheduler") + @"\";
                    break;
                case "configs":
                    mdirConfigRoot = Path.Combine(mDirCommonRoot, "Configs") + @"\";
                    break;
            }

            if (!Directory.Exists(mdirConfigRoot))
            {
                Directory.CreateDirectory(mdirConfigRoot);
                //throw new Exception("Required common config root directory does not exist: " + mdirConfigRoot);
            }

            return mdirConfigRoot;
        }

        /// <summary>
        /// Gets value of a specific element in config file
        /// </summary>
        /// <param name="Node">Element Name</param>
        /// <returns></returns>
        public static string GetConfigValue(string Node)
        {
            string nodeVal = string.Empty;
            if (File.Exists(MainConfig))
            {
                var configFile = LoadConfigFile(MainConfig, DEFAULT_LOAD_TIMEOUT);
                CheckRootElement(configFile);
                var item = configFile.Root.Element(Node);
                if (item != null)
                {
                    nodeVal = decryptor.IsDecrpytable(item.Value) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(item.Value)) : item.Value;                  
                }
            }
            return nodeVal;
        }

        /// <summary>
        /// Get config value without checking for encryption
        /// </summary>
        /// <param name="Node">Node to get value</param>
        /// <returns>Value of node</returns>
        public static string GetConfigUnencryptedValue(string Node)
        {
            string nodeVal = string.Empty;
            if (File.Exists(MainConfig))
            {
                var configFile = LoadConfigFile(MainConfig, DEFAULT_LOAD_TIMEOUT);
                CheckRootElement(configFile);
                var item = configFile.Root.Element(Node);
                if (item != null)
                {
                    nodeVal = item.Value;
                }
            }
            return nodeVal;
        }

        /// <summary>
        /// Updates specific element in a config file
        /// </summary>
        /// <param name="Node">Element Name</param>
        /// <param name="NodeValue">Element Value</param>
        public static void UpdateConfigValue(string Node, string NodeValue)
        {
            CreateConfigFile(MainConfig);
            var configFile = LoadConfigFile(MainConfig, DEFAULT_LOAD_TIMEOUT);
            CheckRootElement(configFile);
            var element = configFile.Root.Element(Node);

            if (element != null)
            {
                //encrypt here
                if (Node.Equals("smtpuser") || Node.Equals("smtppass") || Node.Equals("dbuser") || Node.Equals("dbpassword"))
                {
                    element.Value = encryptor.IsEncryptable(NodeValue) ? Convert.ToBase64String(encryptor.EncryptStringToByteArray(NodeValue)) : NodeValue;
                }
                else
                {
                    element.Value = NodeValue;
                }
            }
            else
            {
                //create the child node on XML
                XElement childNode = new XElement(Node)
                {
                    Value = (Node.Equals("smtpuser") || Node.Equals("smtppass") || Node.Equals("dbuser") || Node.Equals("dbpassword")) ? Convert.ToBase64String(encryptor.EncryptStringToByteArray(NodeValue)) : NodeValue
                };
                configFile.Root.Add(childNode);
            }
            SaveConfigFile(MainConfig, configFile, DEFAULT_LOAD_TIMEOUT);

        }
        
        /// <summary>
        /// Creates default config file
        /// </summary>
        /// <param name="FilePath">Config File Path</param>
        public static void CreateConfigFile(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                Save(FilePath);
            }
        }

        /// <summary>
        /// Updates multiple element in a config file
        /// </summary>
        /// <param name="FilePath">Config File path</param>
        /// <param name="ElementList">List of element name and its corresponding values</param>
        public static void UpdateMultipleNodeInConfig(string FilePath, List<XElement> ElementList)
        {
            CreateConfigFile(FilePath);
            XDocument configFile = LoadConfigFile(FilePath, DEFAULT_LOAD_TIMEOUT);
            CheckRootElement(configFile);
            foreach (var e in ElementList)
            {
                var xElement = configFile.Root.Element(e.Name);
                if (xElement == null)
                {
                    configFile.Root.Add(e);
                }
                else
                {
                    xElement.Value = e.Value;
                }
            }
            SaveConfigFile(FilePath, configFile, DEFAULT_LOAD_TIMEOUT);
        }

        public static void TransferXmlNode(string SourceFile, string DestFile, string Node)
        {
            if (File.Exists(SourceFile) && File.Exists(DestFile))
            {
                var sourceXml = XDocument.Load(SourceFile);
                var destXml = XDocument.Load(DestFile);

                var sourceNode = sourceXml.Root.Element(Node);
                if (sourceNode != null && destXml.Root != null)
                {
                    destXml.Root.Add(sourceNode);
                    destXml.Save(DestFile);
                }
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Check if Root element exists.
        /// </summary>
        /// <param name="configFile"></param>
        private static void CheckRootElement(XDocument configFile)
        {
            try
            {
                var rootElement = configFile.Element("config");
            }
            catch (NullReferenceException)
            {
                //root element config is missing
                ReCreateConfigFile();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create a clean copy of config file
        /// </summary>
        private static void ReCreateConfigFile()
        {
            if (File.Exists(MainConfig))
            {
                File.Delete(MainConfig);
            }
            CreateConfigFile(MainConfig);
        }


        private static XDocument LoadConfigFile(string path, int timeout)
        {
            XDocument ret = null;
            for (int i = 1; i < timeout; i++)
            {
                try
                {
                    ret = XDocument.Load(path);
                    break;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    if (e.Message.Equals("Root element is missing."))
                    {
                        ReCreateConfigFile();
                    }
                }
            }
            return ret;
        }

        private static void SaveConfigFile(string path, XDocument input, int timeout)
        {
            if (input == null)
                return;
            for (int i = 1; i < timeout; i++)
            {
                try
                {
                    input.Save(path);
                    break;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {
                    throw;
                }
            }
        }

        private static DlkResultsDatabaseConfigRecord GetDatabaseConfigRecord()
        {
            string dbserver = GetConfigValue("dbserver");
            string dbname = GetConfigValue("dbname");
            string dbuser = GetConfigValue("dbuser");
            string dbpw = GetConfigValue("dbpassword");

            DlkResultsDatabaseConfigRecord _rec = new DlkResultsDatabaseConfigRecord(dbserver, dbname, dbuser, dbpw);
            return _rec;
        }

        #endregion
    }
}