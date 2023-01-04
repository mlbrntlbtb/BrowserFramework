using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// This record holds the details for the actions during test capture
    /// </summary>
    public class DlkActionRecord
    {
        public int mBlock { get; set; }
        public String mAction { get; set; }
        public String mScreenTarget { get; set; }
        public String mTargetType { get; set; }
        public String mDescType { get; set; }
        public String mDescValue { get; set; }
        public String mValue { get; set; }
    }
}
