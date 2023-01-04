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
    public static class DlkRemoteBrowserHandler
    {
        private static List<DlkRemoteBrowserRecord> _mRemoteBrowsers;
        public static List<DlkRemoteBrowserRecord> mRemoteBrowsers
        {
            get 
            {
                _mRemoteBrowsers = LoadRemoteBrowsers();

                return _mRemoteBrowsers;
            }
        }

        private static List<DlkRemoteBrowserRecord> LoadRemoteBrowsers()
        {
            List<DlkRemoteBrowserRecord> lstBrowsers = new List<DlkRemoteBrowserRecord>();
            XDocument docRemoteBrowsers = XDocument.Load(DlkEnvironment.mRemoteBrowsersFile);
            var data = from doc in docRemoteBrowsers.Descendants("Browser")
                       select new
                       {
                           id = (string)doc.Attribute("id").Value,
                           url = (string)doc.Attribute("url").Value,
                           type = (string)doc.Attribute("type").Value,
                       };
            foreach(var rec in data)
            {
                DlkRemoteBrowserRecord newRec = new DlkRemoteBrowserRecord(rec.id, rec.url, rec.type);
                lstBrowsers.Add(newRec);
            }
            return lstBrowsers;
        }

        public static DlkRemoteBrowserRecord GetRecord(string id)
        {
            for(int i=0; i < mRemoteBrowsers.Count; i++)
            {
                if(mRemoteBrowsers[i].Id == id)
                {
                    return mRemoteBrowsers[i];
                }
            }

            return null;
        }

        public static void Save(List<DlkRemoteBrowserRecord> RemoteBrowsers)
        {
            List<XElement> recs = new List<XElement>();


            foreach (DlkRemoteBrowserRecord r in RemoteBrowsers)
            {
                recs.Add(new XElement("Browser",
                    new XAttribute("id", r.Id),
                    new XAttribute("url", r.Url),
                    new XAttribute("type", r.Browser)
                    )
                    );
            }

            XElement root = new XElement("RemoteBrowsers", recs);

            XDocument xDoc = new XDocument(root);
            xDoc.Save(DlkEnvironment.mRemoteBrowsersFile);
        }
    }
}
