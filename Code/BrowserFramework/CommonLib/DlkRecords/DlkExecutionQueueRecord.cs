using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using CommonLib.DlkSystem;

namespace CommonLib.DlkRecords
{
    public class DlkExecutionQueueRecord
    {
        private string mExecuteDependencyTestRow = string.Empty;
        private string mExecute;
        private string mEnvironment;
        private string mKeepOpen;
        private DlkBrowser mBrowser;
        private string mDependent;
        private string mExecutedependencyresult;
        private DlkExecutionQueueRecord meExecutedependency;
        private bool mIsModified = false;
        private bool mKeepOpenFieldsEnabled = true;

        public event PropertyChangedEventHandler PropertyChanged;
        public String testrow { get; set; }
        public String executedsteps { get; set; }
        public String teststatus { get; set; }
        public String folder { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String file { get; set; }
        public String instance { get; set; }
        public String duration { get; set; }
        public String executiondate { get; set; }
        public String assigned { get; set; }
        public String errormessage { get; set; }
        public String identifier { get; set; }
        public String instanceRange { get; set; }
        public String totalsteps { get; set; }

        public bool isModified
        {
            get
            {
                return mIsModified;
            }
            set
            {
                mIsModified = value;
            }
        }

        public String dependent
        {
            get
            {
                return mDependent;
            }
            set
            {
                if (mDependent != value)
                {
                    mIsModified = true;
                }
                mDependent = value;
            }
        }

        public string executedependencyresult
        {
            get
            {
                return mExecutedependencyresult;
            }
            set
            {
                if (mExecutedependencyresult != value)
                {
                    mIsModified = true;
                }
                mExecutedependencyresult = value;
            }
        }

        public DlkExecutionQueueRecord executedependency
        {
            get
            {
                return meExecutedependency;
            }
            set
            {
                if (meExecutedependency != null && value != null &&
                    meExecutedependency.identifier != value.identifier)
                {
                    mIsModified = true;
                }
                meExecutedependency = value;
            }
        }

        public String execute
        {
            get
            {
                return mExecute;
            }
            set
            {
                if (mExecute != value)
                {
                    mIsModified = true;
                }
                mExecute = value;
                OnPropertyChanged("execute");
            }
        }

        public String environment
        {
            get
            {
                return mEnvironment;
            }
            set
            {
                if (mEnvironment != value)
                {
                    mIsModified = true;
                }
                mEnvironment = value;
                OnPropertyChanged("environment");
            }
        }

        public DlkBrowser Browser
        {
            get
            {
                return mBrowser;
            }
            set
            {
                if (mBrowser != null && value != null &&
                    mBrowser.Name != value.Name)
                {
                    mIsModified = true;
                }
                mBrowser = value;
                OnPropertyChanged("Browser");
            }
        }
        
        public String executedependencytestrow
        {
            get
            {
                return executedependency == null ? mExecuteDependencyTestRow : executedependency.testrow;
            }
            set
            {
                mExecuteDependencyTestRow = value;
            }
        }

        public String fullpath
        {
            get
            {
                return Path.Combine(DlkEnvironment.mDirTests.TrimEnd('\\'), folder.Trim('\\'), file);
            }
        }
        
        public String keepopen
        {
            get
            {
                return mKeepOpen;
            }
            set
            {
                if (mKeepOpen != value)
                {
                    mIsModified = true;
                }
                mKeepOpen = value;
                OnPropertyChanged("keepopen");
            }
        }

        public bool keepopenfieldsenabled
        {
            get
            {
                return mKeepOpenFieldsEnabled;
            }
            set
            {
                if (mKeepOpenFieldsEnabled != value)
                {
                    mIsModified = true;
                }
                mKeepOpenFieldsEnabled = value;
                OnPropertyChanged("keepopenfieldsenabled");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public DlkExecutionQueueRecord(String _testrow, String _teststatus, String _folder,
        //    String _name, String _description, String _file, String _testid, String _duration, String _executiondate, String _assigned, String _errormessage)
        //{
        //    testrow = _testrow;
        //    teststatus = _teststatus;
        //    folder = _folder;
        //    name = _name;
        //    description = _description;
        //    file = _file;
        //    instance = _testid;
        //    duration = _duration;
        //    executiondate = _executiondate;
        //    assigned = _assigned;
        //    errormessage = _errormessage;
        //}
        public DlkExecutionQueueRecord(String _identifier, String _testrow, String _executedsteps, String _teststatus, String _folder,
            String _name, String _description, String _path, String _testid, String _environment, String _browser,
            String _isOpen, String _duration, String _executiondate, String _execute, String _dependent, String _executedependencyresult, String _executedependencytestrow,
            String _instanceRange = "", String _totalsteps = "")
        {
            identifier = _identifier;
            testrow = _testrow;
            executedsteps = _executedsteps;
            teststatus = _teststatus;
            folder = _folder;
            name = _name;
            description = _description;
            file = _path;
            instance = _testid;
            duration = _duration;
            executiondate = _executiondate;
            environment = _environment;
            Browser = new DlkBrowser(_browser);
            keepopen = _isOpen;
            assigned = "";
            dependent = _dependent;
            execute = _execute;
            executedependency = null;
            executedependencyresult = _executedependencyresult;
            executedependencytestrow = _executedependencytestrow;
            instanceRange = _instanceRange;
            totalsteps = _totalsteps;
        }

        //public DlkExecutionQueueRecord()
        //{
        //    testrow = "";
        //    teststatus = "";
        //    folder = "";
        //    name = "";
        //    file = "";
        //    instance = "";
        //    duration = "";
        //    executiondate = "";
        //    assigned = "";
        //    errormessage = "";
        //}
    }
}
