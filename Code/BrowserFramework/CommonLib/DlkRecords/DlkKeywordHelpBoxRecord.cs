using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkKeywordHelpBoxRecord
    {
        public String mKeyword { get; set; }
        public String mInformation { get; set; }
        public String mParameters { get; set; }
        public String mControls { get; set; }
        public string mHelpFile { get; set; }
        public string mControlType { get; set; }

        public DlkKeywordHelpBoxRecord(String Keyword, String Information, String Parameters, String Controls, String HelpFile, String ControlType)
        {
            mKeyword = Keyword;
            mInformation = Information;
            mParameters = Parameters;
            mControls = Controls;
            mHelpFile = HelpFile;
            mControlType = ControlType;
        }
    }
}
