using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// This record is holds both the details for the test and its execution results
    /// </summary>
    [Serializable]
    public class DlkTestStepRecord
    {
        #region DECLARATIONS
        public const string globalParamDelimiter = "q7*;";
        #endregion

        /// <summary>
        /// the test step number
        /// </summary>
        public int mStepNumber { get; set; }

        /// <summary>
        /// used to turn execution of the step on/off
        /// </summary>
        public string mExecute { get; set; }

        /// <summary>
        /// used for case-insensitivity for formatting under ResourcesDictionary.xaml
        /// </summary>
        public string mDisplayExecute 
        {
            get
            {
                return mExecute.ToLower();
            }
        }

        /// <summary>
        /// the screen where the contol is located
        /// </summary>
        public String mScreen { get; set; }

        /// <summary>
        ///  the control that is being interacted with
        /// </summary>
        public String mControl { get; set; }

        /// <summary>
        /// the keyword we are using on the control
        /// </summary>
        public String mKeyword { get; set; }

        /// <summary>
        /// the parameters for that keyword
        /// </summary>
        public List<String> mParameters 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// the real password parameters for that keyword
        /// </summary>
        public List<String> mPasswordParameters
        {
            get;
            set;
        }

        public string mParameterOrigString
        {
            get
            {
                string ret = "";
                if (mParameters.Count > 0)
                {
                    ret = mParameters[mCurrentInstance - 1];
                }
                return ret;
            }
        }

        //For UI display only
        public string mParameterString
        {
            get
            {
                return mParameterOrigString.Replace(DlkTestStepRecord.globalParamDelimiter, "|");
            }
        }
        public int mCurrentInstance = 1;

        /// <summary>
        /// The delay that is applied after the step executes
        /// </summary>
        public int mStepDelay { get; set; }

        /// <summary>
        /// passed or failed for test status
        /// </summary>
        public String mStepStatus 
        { 
            get
            {
                string ret = string.IsNullOrEmpty(_stepstatus) ? _stepstatus :
                    CommonLib.DlkUtility.DlkString.ToUpperIndex(_stepstatus, 0);
                return ret;
            }
            set
            {
                _stepstatus = value;
            }
        }
        string _stepstatus = "";

        /// <summary>
        /// the logged data during execution
        /// </summary>
        public List<DlkLoggerRecord> mStepLogMessages { get; set; }

        /// <summary>
        /// the moment the test step started executing
        /// </summary>
        public DateTime mStepStart { get; set; }

        /// <summary>
        /// the moment the test step finished executing
        /// </summary>
        public DateTime mStepEnd { get; set; }

        /// <summary>
        /// how much time elapsed (end - start)
        /// </summary>
        public String mStepElapsedTime { get; set; }
    }
}
