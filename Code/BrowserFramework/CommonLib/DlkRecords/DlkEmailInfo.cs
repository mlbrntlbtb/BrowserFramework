using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    public class DlkEmailInfo
    {
        #region PROPERTIES
        public string mSubject { get; set; }
        public List<string> mRecepient { get; set; }
        public string mSMTPHost { get; set; }
        public int mSMTPPort { get; set; }
        public string mSMTPUser { get; set; }
        public string mSMTPPass { get; set; }
        public string mSender { get; set; }
        #endregion

        #region METHODS
        public static string GetEmailRecepients(List<string> Recepients)
        {
            string ret = "";
            foreach (string to in Recepients)
            {
                ret += to + " ";
            }
            return ret.Trim(' ');
        }
        #endregion
    }
}