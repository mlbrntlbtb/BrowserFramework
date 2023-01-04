using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// A schedule record
    /// </summary>
    public class DlkTargetApplication
    {
        public DlkTargetApplication()
        {

        }

        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        public string DisplayName
        {
            get
            {
                return string.IsNullOrEmpty(Version) ? Name : Name + " - " + Version;
            }
        }

        public string Library
        {
            get;
            set;
        }


        public string ProductFolder
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Platform
        {
            get;
            set;
        }

        //public string TestFolder
        //{
        //    get;
        //    set;
        //}

        //public string TestSuiteFolder
        //{
        //    get;
        //    set;
        //}
    }
}