using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Designer.Model
{
    public class TLSuite : DlkTestSuiteInfoRecord
    {
        private List<DlkExecutionQueueRecord> mTests = new List<DlkExecutionQueueRecord>();
        public List<DlkExecutionQueueRecord> Tests
        {
            get
            {
                //List<DlkExecutionQueueRecord> tempTests = mTests.FindAll(x => System.IO.File.Exists(x.fullpath));
                //tempTests.ForEach(y => { y.testrow = (tempTests.IndexOf(y) + 1).ToString(); });
                //return tempTests;
                return mTests;
            }
            set
            {
                mTests = value;
                mTests = mTests.FindAll(x => System.IO.File.Exists(x.fullpath));
                mTests.ForEach(y => { y.testrow = (mTests.IndexOf(y) + 1).ToString(); });
            }
        }

        public DlkTestSuiteInfoRecord SuiteInfo { get; set; }
        public String path { get; set; }
        public String name { get; set; }
        public string ListIndex { get; set; }
    }
}
