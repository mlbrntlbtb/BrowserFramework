using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Designer.Model
{
    public class Match
    {
        public String TestName { get; set; }
        public String Path { get; set; }
        public int StepCount { get; set; }
        public String Description { get; set; }
        public int MatchCount { get; set; }
        public int MismatchCount
        {
            get
            {
                return (StepCount - MatchCount);
            }
        }
        public bool IsExactMatch { get; set; }
        public String IsExactMatchString { get; set; }
        public List<DlkTestStepRecord> MatchedRows { get; set; }
        public string GoToText
        {
            get
            {
                return "Link";
            }
        }
        public int StepCountDifference { get; set; }
    }
}
