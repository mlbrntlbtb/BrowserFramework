using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkLoginConfigRecord
    {
        public String mID { get; set; }
        public String mUrl { get; set; }
        public String mUser { get; set; }
        public String mPassword { get; set; }
        public String mDatabase { get; set; }
        public String mPin { get; set; }
        public String mMetaData { get; set; }
        public String mConfigFile { get; set; }

        public DlkLoginConfigRecord(String ID, String Url, String User, String Password, String Database, String Pin, String ConfigFile, String MetaData="")
        {
            mID = ID;
            mUrl = Url;
            mUser = User;
            mPassword = Password;
            mDatabase = Database;
            mPin = Pin;
            mMetaData = MetaData;
            mConfigFile = ConfigFile;
        }
    }
}
