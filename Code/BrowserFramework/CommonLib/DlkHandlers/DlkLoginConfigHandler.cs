using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;

namespace CommonLib.DlkHandlers
{
    public class DlkLoginConfigHandler
    {
        public String mID { get; set; }
        public String mUrl { get; set; }
        public String mUser { get; set; }
        public String mPassword { get; set; }
        public String mDatabase { get; set; }
        public String mPin { get; set; }
        public String mConfigFile { get; set; }
        public String mMetaData { get; set; }
        public ObservableCollection<DlkLoginConfigRecord> mLoginConfigRecords { get; set; }
        private XDocument mXml { get; set; }
        private static XDocument DlkXml;
        private DlkDecryptionHelper decryptor = new DlkDecryptionHelper();

        public DlkLoginConfigHandler(String LoginConfigFilePath)
        {
            mID = "";
            mUrl = "";
            mUser = "";
            mPassword = "";
            mDatabase = "";
            mPin = "";
            mMetaData = "";
            mConfigFile = LoginConfigFilePath;
            mLoginConfigRecords = new ObservableCollection<DlkLoginConfigRecord>();
            mXml = XDocument.Load(mConfigFile);
            //try decryption, throw exception if not decryptable
            foreach (XElement node in mXml.Descendants("login"))
            {
                mID = (String)node.Attribute("id").Value;
                mUrl = string.IsNullOrEmpty((String)node.Element("url")) ? "" : (String)node.Element("url");
                mUser = string.IsNullOrEmpty((String)node.Element("user")) ? "" : (decryptor.IsDecrpytable((String)node.Element("user")) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String((String)node.Element("user"))) : (String)node.Element("user"));
                mPassword = string.IsNullOrEmpty((String)node.Element("password")) ? "" : (decryptor.IsDecrpytable((String)node.Element("password")) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String((String)node.Element("password"))) : (String)node.Element("password"));
                mDatabase = string.IsNullOrEmpty((String)node.Element("database")) ? "" : (String)node.Element("database");
                mPin = string.IsNullOrEmpty((String)node.Element("pin")) ? "" : (decryptor.IsDecrpytable((String)node.Element("pin")) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String((String)node.Element("pin"))) : (String)node.Element("pin"));
                mMetaData = string.IsNullOrEmpty((String)node.Element("metadata")) ? "" : (String)node.Element("metadata");
                DlkLoginConfigRecord rec = new DlkLoginConfigRecord(mID, mUrl, mUser,
                    mPassword, mDatabase, mPin, mConfigFile, mMetaData);
                mLoginConfigRecords.Add(rec);
            }
        }

        public DlkLoginConfigHandler(String LoginConfigFilePath, String ConfigName)
        {
            mUrl = "";
            mUser = "";
            mPassword = "";
            mDatabase = "";
            mMetaData = "";
            mConfigFile = LoginConfigFilePath;
            mXml = XDocument.Load(mConfigFile);
            //try decryption, throw exception if not decryptable
            //get the first occurence
            XElement node = mXml.Descendants("login").Where(x => (String)x.Attribute("id") == ConfigName).First();
            mUrl = string.IsNullOrEmpty((String)node.Element("url")) ? "" : (String)node.Element("url");
            mUser = string.IsNullOrEmpty((String)node.Element("user")) ? "" : (decryptor.IsDecrpytable((String)node.Element("user")) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String((String)node.Element("user"))) : (String)node.Element("user"));
            mPassword = string.IsNullOrEmpty((String)node.Element("password")) ? "" : (decryptor.IsDecrpytable((String)node.Element("password")) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String((String)node.Element("password"))) : (String)node.Element("password"));
            mDatabase = string.IsNullOrEmpty((String)node.Element("database")) ? "" : (String)node.Element("database");
            mPin = string.IsNullOrEmpty((String)node.Element("pin")) ? "" : (decryptor.IsDecrpytable((String)node.Element("pin")) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String((String)node.Element("pin"))) : (String)node.Element("pin"));
            mMetaData = string.IsNullOrEmpty((String)node.Element("metadata")) ? "" : (String)node.Element("metadata");
        }

        public DlkLoginConfigHandler(String Url, String User, String Password, String Database, String Pin, String MetaData)
        {
            mUrl = string.IsNullOrEmpty(Url) ? "" : Url;
            mUser = string.IsNullOrEmpty(User) ? "" : (decryptor.IsDecrpytable(User) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(User)) : User);
            mPassword = string.IsNullOrEmpty(Password) ? "" : (decryptor.IsDecrpytable(Password) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(Password)) : Password);
            mDatabase = string.IsNullOrEmpty(Database) ? "" : Database;
            mPin = string.IsNullOrEmpty(Pin) ? "" : Pin;
            mMetaData = string.IsNullOrEmpty(MetaData) ? "" : MetaData;
        }

        public void Save(String LoginConfigFilePath, List<DlkLoginConfigRecord> records)
        {
            List<XElement> recs = new List<XElement>();
            DlkEncryptionHelper helper = new DlkEncryptionHelper();
          
            foreach (DlkLoginConfigRecord r in records)
            {
                recs.Add(new XElement("login",
                    new XAttribute("id", r.mID),
                    new XElement("url", r.mUrl),
                    new XElement("user", Convert.ToBase64String(helper.EncryptStringToByteArray(r.mUser))),
                    new XElement("password", Convert.ToBase64String(helper.EncryptStringToByteArray(r.mPassword))),
                    new XElement("database", r.mDatabase),
                    new XElement("pin", Convert.ToBase64String(helper.EncryptStringToByteArray(r.mPin))),
                    new XElement("metadata", r.mMetaData)
                    )
                    );
            }

            XElement loginconfig = new XElement("loginconfig", recs);

            XDocument xDoc = new XDocument(loginconfig);
            xDoc.Save(LoginConfigFilePath);
        }

        public static DlkLoginConfigRecord GetLoginConfigInfo(String LoginConfigPath, String LoginConfig)
        {
            DlkXml = XDocument.Load(LoginConfigPath);
            var data = from doc in DlkXml.Descendants("login")
                       where (String)doc.Attribute("id") == LoginConfig
                select new
                {
                    id = (String)doc.Attribute("id").Value,
                    url = string.IsNullOrEmpty((String)doc.Element("url")) ? "" : (String)doc.Element("url"),
                    user = string.IsNullOrEmpty((String)doc.Element("user")) ? "" : (String)doc.Element("user"),
                    password = string.IsNullOrEmpty((String)doc.Element("password")) ? "" : (String)doc.Element("password"),
                    database = string.IsNullOrEmpty((String)doc.Element("database")) ? "" : (String)doc.Element("database"),
                    pin = string.IsNullOrEmpty((String)doc.Element("pin")) ? "" : (String)doc.Element("pin"),
                    metadata = string.IsNullOrEmpty((String)doc.Element("metadata")) ? "" : (String)doc.Element("metadata"),
                };
            return new DlkLoginConfigRecord(data.First().id, data.First().url, data.First().user, data.First().password, 
                data.First().database, data.First().pin, LoginConfigPath, data.First().metadata);
        }

    }
}