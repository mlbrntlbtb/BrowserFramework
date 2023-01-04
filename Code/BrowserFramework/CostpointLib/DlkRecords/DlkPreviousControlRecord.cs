using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostpointLib.DlkRecords
{
    /// <summary>
    /// Class for CP previous control records. Used to optimize table performance
    /// </summary>
    internal class DlkPreviousControlRecord:DlkControl
    {
        public DlkPreviousControlRecord(string screen, string keyword, string controlName, int row, string controlType) : base(screen, keyword, controlName, row, controlType) { }

        #region PROPERTIES
        /// <summary>
        /// Property used to store current table control
        /// </summary>
        public static DlkControl CurrentControl { get; set; }

        /// <summary>
        /// Property used to store previous table control
        /// </summary>
        public static DlkControl PreviousControl { get; set; }

        /// <summary>
        /// Returns true if previous and current control is same row, same control name and control type
        /// </summary>
        public static bool IsRepeatedRow { get; set; }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Clear previous control record
        /// </summary>
        public static void ClearPreviousControlRecord()
        {
            PreviousControl = new DlkControl();
            IsRepeatedRow = false;
        }

        /// <summary>
        /// Store previous control
        /// </summary>
        /// <param name="screen">previous step screen</param>
        /// <param name="controlName">previous step control</param>
        /// <param name="controlType">previous step control type (ex. table)</param>
        public static void SetPreviousControl()
        {
            if (CurrentControl.Screen != "" && CurrentControl.Row != -1)
                PreviousControl = new DlkControl(CurrentControl.Screen, CurrentControl.Keyword, CurrentControl.ControlName, CurrentControl.Row, CurrentControl.ControlType);
            else
                ClearPreviousControlRecord();
        }

        /// <summary>
        /// Set current control row number. It will only set if current control screen is not empty
        /// </summary>
        /// <param name="row">table row or assigned value for user defined variable for row</param>
        public static void SetCurrentControl(string row)
        {
            if (CurrentControl.Screen != "")
                CurrentControl.Row = int.Parse(row);

            IsRepeatedRow = CurrentControl.ControlName == PreviousControl.ControlName && CurrentControl.Row == PreviousControl.Row && CurrentControl.ControlType == PreviousControl.ControlType;
        }

        /// <summary>
        /// Store current table control 
        /// </summary>
        /// <param name="screen">test step screen</param>
        /// <param name="keyword">test step control keyword</param>
        /// <param name="control">test step control</param>
        /// <param name="controlType">test step control type (ex. table..)</param>
        public static void SetCurrentControl(string screen, string keyword, string control, string controlType)
        {
            if (new string[] { "table", "multiparttable" }.Contains(controlType.ToLower()))
                CurrentControl = new DlkControl(screen, keyword, control, -1, controlType);
            else
                CurrentControl = new DlkControl();

            IsRepeatedRow = false;
        }
        #endregion

    }

    /// <summary>
    /// Class for Current and Previous control data to be used only for COSTPOINT
    /// </summary>
    internal class DlkControl
    {

        #region Properties
        /// <summary>
        /// step screen
        /// </summary>
        public string Screen { get; private set; }

        /// <summary>
        /// step keyword
        /// </summary>
        public string Keyword { get; private set; }

        /// <summary>
        /// step control
        /// </summary>
        public string ControlName { get; private set; }

        /// <summary>
        /// table row
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// step control type, limited to table and multiparttable
        /// </summary>
        public string ControlType { get; private set; }
        #endregion

        /// <summary>
        /// Contructor for Control data
        /// </summary>
        /// <param name="screen">screen of current control</param>
        /// <param name="keyword">keyword for selected control</param>
        /// <param name="controlName">selected control of screen</param>
        /// <param name="row">table row or assigned value for user defined variable for row</param>
        public DlkControl(string screen, string keyword, string controlName, int row, string controlType)
        {
            Screen = screen;
            Keyword = keyword;
            ControlName = controlName;
            Row = row;
            ControlType = ControlType;
        }

        /// <summary>
        /// Constructor for new control
        /// </summary>
        public DlkControl()
        { 
        }
    }
}
