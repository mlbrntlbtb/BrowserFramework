using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkRemoteBrowserRecord
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Browser { get; set; }

        public DlkRemoteBrowserRecord(string pId, string pUrl, string pBrowser)
        {
            Id = pId;
            Url = pUrl;
            Browser = pBrowser;
        }
        
    }
}
