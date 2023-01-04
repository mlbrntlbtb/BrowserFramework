using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    public class DlkTestSuiteManifestRecord
    {
        public String suitepath { get; set; }
        public String tag { get; set; }
        public String resultsdirectory { get; set; }

        public DlkTestSuiteManifestRecord()
        {
            suitepath = "";
            tag = "";
            resultsdirectory = "";
        }
        public DlkTestSuiteManifestRecord(String _suitepath, String _tag, String _resultsdirectory)
        {
            suitepath = _suitepath;
            resultsdirectory = _resultsdirectory;
            tag = _tag;
        }
    }
}
