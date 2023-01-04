using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// This record contains the details for Selenium to find a control on the screen
    /// </summary>
    public class DlkObjectStoreFileControlRecord
    {
        /// <summary>
        /// the key is how both a user and the software references a control. It is assigned when creating an object store file,
        /// </summary>
        public String mKey { get; set; }

        /// <summary>
        /// we assign objects with control types. The control types (button, textbox, etc) provide the keywords available to use on that control
        /// </summary>
        public String mControlType { get; set; }

        /// <summary>
        /// The assigned search method is used along with search parameters by Selenium to find the object
        /// </summary>
        public String mSearchMethod { get; set; }

        /// <summary>
        /// The assigned search method is used along with search parameters by Selenium to find the object
        /// </summary>
        public String mSearchParameters { get; set; }

        /// <summary>
        /// constructs a new object store file record
        /// </summary>
        public DlkObjectStoreFileControlRecord()
        {
            mKey = "";
            mControlType = "";
            mSearchMethod = "";
            mSearchParameters = "";
        }

        /// <summary>
        /// constructs a new object store file record
        /// </summary>
        public DlkObjectStoreFileControlRecord(String Key, String ControlType, String SearchMethod, String SearchParameters)
        {
            mKey = Key;
            mControlType = ControlType;
            mSearchMethod = SearchMethod;
            mSearchParameters = SearchParameters;
        }
    }
}
