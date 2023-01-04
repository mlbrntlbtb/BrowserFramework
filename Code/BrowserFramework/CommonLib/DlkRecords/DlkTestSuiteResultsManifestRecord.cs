using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkTestSuiteResultsManifestRecord
    {
        public string suite;
        public string suitepath;
        public string executiondate;
        public int passed;
        public int failed;
        public int notrun;
        public string resultsdirectory;

        //public DlkTestSuiteResultsManifestRecord(string suite, string executiondate, int passed, int failed, int notrun)
        //{
        //    this.suite = suite;
        //    this.executiondate = executiondate;
        //    this.passed = passed;
        //    this.failed = failed;
        //    this.notrun = notrun;
        //}

        public DlkTestSuiteResultsManifestRecord(string suite, string suitepath, string executiondate, int passed, int failed, int notrun, string resultsdirectory)
        {
            this.suite = suite;
            this.suitepath = suitepath;
            this.executiondate = executiondate;
            this.passed = passed;
            this.failed = failed;
            this.notrun = notrun;
            this.resultsdirectory = resultsdirectory;
        }

        public bool IsEqual(DlkTestSuiteResultsManifestRecord ComparisonObject)
        {
            return this.resultsdirectory == ComparisonObject.resultsdirectory;
        }

    }
}
