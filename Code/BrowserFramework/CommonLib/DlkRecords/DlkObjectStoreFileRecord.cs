using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// This record stores the data from one object store file into memory
    /// </summary>
    public class DlkObjectStoreFileRecord
    {
        /// <summary>
        /// the file path containing the control data
        /// </summary>
        public String mFile { get; set; }

        /// <summary>
        /// the screen name (an assigned key) to map the controls to
        /// </summary>
        public String mScreen { get; set; }

        /// <summary>
        /// mScreen alias (for costpoint products only)
        /// </summary>
        public string mAlias { get; set; }

        /// <summary>
        /// a list of the control records
        /// </summary>
        public List<DlkObjectStoreFileControlRecord> mControls { get; set; }

        /// <summary>
        /// constructs a new object store file record
        /// </summary>
        public DlkObjectStoreFileRecord()
        {
            mFile = "";
            mScreen = "";
            mControls = new List<DlkObjectStoreFileControlRecord>();
        }

        /// <summary>
        /// constructs a new object store file record
        /// </summary>
        public DlkObjectStoreFileRecord(String OSFile, String OSScreen, List<DlkObjectStoreFileControlRecord> lsControls)
        {
            mFile = OSFile;
            mScreen = OSScreen;
            mControls = lsControls;
        }

    }
}
