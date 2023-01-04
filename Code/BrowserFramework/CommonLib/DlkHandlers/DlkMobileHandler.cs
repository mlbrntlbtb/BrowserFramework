using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;

namespace CommonLib.DlkHandlers
{
    public static class DlkMobileHandler
    {
        private static List<DlkMobileRecord> _mMobileRec;
        
        public static List<DlkMobileRecord> mMobileRec
        {
            get 
            {
                _mMobileRec = LoadMobileRec();

                return _mMobileRec;
            }
        }

        private static List<DlkMobileRecord> LoadMobileRec()
        {
            List<DlkMobileRecord> lstBrowsers = new List<DlkMobileRecord>();
            XDocument docMobileRec = XDocument.Load(DlkEnvironment.mMobileConfigFile);

            var data = from doc in docMobileRec.Descendants("Mobile")
                       select new
                       {
                           id = (string)doc.Attribute("id").Value.Trim(),
                           url = (string)doc.Attribute("url").Value,
                           type = (string)doc.Attribute("type").Value,
                           devicename = doc.Attribute("devicename") == null ? "" : (String)doc.Attribute("devicename").Value.Trim(),
                           deviceversion = doc.Attribute("deviceversion") == null ? "" : (String)doc.Attribute("deviceversion").Value,
                           application = doc.Attribute("application") == null ? "" : (String)doc.Attribute("application").Value.Trim(),
                           path = doc.Attribute("path") == null ? "" : (String)doc.Attribute("path").Value.Trim(),
                           cmdtimeout = doc.Attribute("cmdtimeout") == null ? DlkEnvironment.DefaultAppiumCommandTimeout.ToString() : (String)doc.Attribute("cmdtimeout").Value,
                           isemulator = doc.Attribute("isemulator") == null ? "False" : (String)doc.Attribute("isemulator").Value
                       };
            foreach(var rec in data)
            {
                int cmdtimeout = DlkEnvironment.DefaultAppiumCommandTimeout;
                int.TryParse(rec.cmdtimeout, out cmdtimeout);
                DlkMobileRecord newRec = new DlkMobileRecord(rec.id, rec.url, rec.type, rec.devicename, rec.deviceversion, rec.application, rec.path, cmdtimeout, Convert.ToBoolean(rec.isemulator));
                lstBrowsers.Add(newRec);
            }
            return lstBrowsers;
        }

        public static DlkMobileRecord GetRecord(string id)
        {
            for(int i=0; i < mMobileRec.Count; i++)
            {
                if(mMobileRec[i].MobileId == id)
                {
                    return mMobileRec[i];
                }
            }

            return null;
        }

        public static void Save(List<DlkMobileRecord> MobileRec)
        {
            List<XElement> recs = new List<XElement>();


            foreach (DlkMobileRecord r in MobileRec)
            {
                recs.Add(new XElement("Mobile",
                    new XAttribute("id", r.MobileId ?? string.Empty),
                    new XAttribute("url", r.MobileUrl ?? string.Empty),
                    new XAttribute("type", r.MobileType ?? string.Empty),
                    new XAttribute("devicename", r.DeviceName ?? string.Empty),
                    new XAttribute("deviceversion", r.DeviceVersion),
                    new XAttribute("application", r.Application ?? string.Empty),
                    new XAttribute("path", r.Path ?? string.Empty),
                    new XAttribute("cmdtimeout", r.CommandTimeout.ToString()),
                    new XAttribute("isemulator", r.IsEmulator.ToString())
                    )
                    );
            }

            XElement root = new XElement("MobileRec", recs);

            XDocument xDoc = new XDocument(root);
            xDoc.Save(DlkEnvironment.mMobileConfigFile);
        }
    }
}
