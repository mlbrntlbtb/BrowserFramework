using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ConvertToDataModificationWindow.xaml
    /// </summary>
    public partial class ConvertToDataModificationWindow : Window
    {
        #region DECLARATIONS

        private TestEditor _owner; 
        public DlkTest mLoadedTest;
        bool isChanged = false;
        bool convertData = false;

        private ObservableCollection<DlkTestStepRecord> _mTestStepRecordsForConversion;
        private ObservableCollection<DlkKeywordParameterRecord> _mKeywordParameters;
        Dictionary<int, string> convertParametersSettingsDict = new Dictionary<int, string>();
        Dictionary<int, ObservableCollection<DlkKeywordParameterRecord>> stepParamList = new Dictionary<int, ObservableCollection<DlkKeywordParameterRecord>>();

        #endregion

        #region PROPERTIES
        public Dictionary<int, string> ConvertParametersSettingsDict
        {
            get { return convertParametersSettingsDict; }
            set { convertParametersSettingsDict = value; }
        }

        public Dictionary<int, ObservableCollection<DlkKeywordParameterRecord>> StepParamList
        {
            get { return stepParamList; }
            set { stepParamList = value; }
        }

        public ObservableCollection<DlkKeywordParameterRecord> KeywordParameters
        {
            get
            {
                if (_mKeywordParameters == null)
                {
                    _mKeywordParameters = new ObservableCollection<DlkKeywordParameterRecord>();
                }
                return _mKeywordParameters;
            }
            set
            {
                _mKeywordParameters = value;
            }
        }

        private ObservableCollection<DlkTestStepRecord> TestStepRecordsForConversion
        {
            get
            {
                if (_mTestStepRecordsForConversion == null)
                {
                    _mTestStepRecordsForConversion = new ObservableCollection<DlkTestStepRecord>();
                }
                return _mTestStepRecordsForConversion;
            }
            set
            {
                _mTestStepRecordsForConversion = value;
            }
        }     

        public DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        private List<DlkKeywordParameterRecord> GetParameterList(int stepNumber)
        {
            List<DlkKeywordParameterRecord> prms = new List<DlkKeywordParameterRecord>();

            try
            {
                DlkTestStepRecord mRec = TestStepRecordsForConversion.Where(x => x.mStepNumber == stepNumber).First();
                var mControl = "";
                if (mRec.mControl == "Function")
                {
                    mControl = "Function";
                }
                else if (mRec.mScreen == "Database")
                {
                    mControl = "Database";
                }
                else
                {
                    mControl = DlkDynamicObjectStoreHandler.GetControlType(mRec.mScreen, mRec.mControl);
                }
                for (int idx = 0; idx < DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, mRec.mKeyword).Count; idx++)
                {
                    prms.Add(new DlkKeywordParameterRecord(DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, mRec.mKeyword)[idx], "", idx));
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

            return prms;
        }

        #endregion

        #region CONSTRUCTOR

        public ConvertToDataModificationWindow(TestEditor Owner, String AssemblyPath, DlkTest Test, List<int> stepsForDataGeneration)
        {
            InitializeComponent();
            _owner = Owner;
            
            // Set start position
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            if (_owner is Window)
            {
                var window = _owner as Window;
                this.Left = window.Left;
                this.Top = window.Top;
            }

            dgTestSteps.DataContext = null;
            dgParameters.DataContext = null;

            LoadTest(Test, stepsForDataGeneration);
            ToggleParameters();
            ColorParams();
        }

        #endregion           

        #region EVENTS

        private void dgTestSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadParameters();
            ToggleParameters();
            ColorParams();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChanged && !convertData)
            {
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_UNSAVED_CHANGES_CONVERTDATA, "Discard Changes") == MessageBoxResult.Yes)
                {
                    //discard changes and close
                    e.Cancel = false;
                    this.DialogResult = false;
                    ConvertParametersSettingsDict = new Dictionary<int, string>();
                }
                else
                {
                    //cancel closing
                    e.Cancel = true;
                }
            }
            else
            {
                //close
                e.Cancel = false;
                this.DialogResult = this.DialogResult??false;
            }
        }

        private void ConvertModifiedData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                convertData = true;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }   
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }   
        }

        private void Parameter_Click(object sender, RoutedEventArgs e)
        {
            DlkTestStepRecord selectedStep = dgTestSteps.SelectedItem as DlkTestStepRecord;
            ConvertParametersSettingsDict[selectedStep.mStepNumber] = GetStepParameterSetting();
            isChanged = true;
            ToggleParameters();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize Parameters with Conversion Settings
        /// </summary>
        private void InitializeParameters()
        {
            try
            {
                foreach (DlkTestStepRecord test in TestStepRecordsForConversion)
                {
                    string[] myParams = test.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                    var stepParam = new ObservableCollection<DlkKeywordParameterRecord>();
                    var mParameterList = GetParameterList(test.mStepNumber);
                    for (int idx = 0; idx < mParameterList.Count(); idx++)
                    {
                        stepParam.Add(new DlkKeywordParameterRecord(mParameterList[idx].mParameterName, myParams[idx], idx, true));
                    }
                    StepParamList.Add(test.mStepNumber, stepParam);
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Creates default parameter conversion setting
        /// </summary>
        private void CreateDefaultConversionSetting()
        {
            foreach (var step in TestStepRecordsForConversion)
            {
                string conversionSetting = string.Empty;
                var parameters = step.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                foreach (var param in parameters)
                {
                    //if (param.Contains("D{"))
                    //{
                    //    conversionSetting += "True" + '|';
                    //}
                    //else
                    //{
                    //    conversionSetting += "False" + '|';
                    //}

                    //initial value is set to true always since we tick checkboxes upon opening of convert to data modification window
                    conversionSetting += "True" + '|';
                }
                conversionSetting = conversionSetting.Trim('|');

                ConvertParametersSettingsDict.Add(step.mStepNumber, conversionSetting);
            }

        }

        /// <summary>
        /// Update and gets parameter conversion setting
        /// </summary>
        /// <returns></returns>
        private string GetStepParameterSetting()
        {
            //update keyword parameters with latest conversion setting
            if(dgTestSteps.SelectedIndex >= 0)
            {
                var step = StepParamList.Where(x => x.Key == TestStepRecordsForConversion[dgTestSteps.SelectedIndex].mStepNumber).First();
                StepParamList[step.Key] = KeywordParameters;
            }

            //get updated conversion setting
            string paramConversionSetting = string.Empty;
            foreach (var param in KeywordParameters)
            {
                paramConversionSetting += param.mParamConversionSetting.ToString() + '|';
            }
            paramConversionSetting = paramConversionSetting.Trim('|');
            return paramConversionSetting;
        }

        /// <summary>
        /// Load Parameters for selected step
        /// </summary>
        private void LoadParameters()
        {
            if (dgTestSteps.SelectedIndex >= 0)
            {
                DlkTestStepRecord mRec = TestStepRecordsForConversion[dgTestSteps.SelectedIndex];
                KeywordParameters = StepParamList[mRec.mStepNumber];
                dgParameters.ItemsSource = KeywordParameters;
                dgParameters.Items.Refresh();
            }
        }

        /// <summary>
        /// Show/Hide Parameters of a selected step
        /// </summary>
        private void ToggleParameters()
        {
            if (dgTestSteps.SelectedItems.Count > 0)
            {
                lblSelect.Visibility = Visibility.Visible;
                dgParameters.Visibility = Visibility.Visible;
            }
            else
            {
                lblSelect.Visibility = Visibility.Hidden;
                dgParameters.Visibility = Visibility.Hidden;
            }

            if (ConvertParametersSettingsDict.Any(x => x.Value.ToString().Contains("True")))
                btnConvertModifiedData.IsEnabled = true;
            else
                btnConvertModifiedData.IsEnabled = false;
        }

        /// <summary>
        /// Loads test on the convert to data modification window
        /// </summary>
        /// <param name="TestPath">Path of the Test</param>
        /// <param name="stepsForDataGeneration">step numbers of the steps to be converted</param>
        private void LoadTest(DlkTest test, List<int> stepsForDataGeneration)
        {
            // load the test
            mLoadedTest = test;

            // populate the test steps that are only selected for conversion
            PopulateStepsForConversion(stepsForDataGeneration);

            // populate the gen info
            txtTestName.Text = mLoadedTest.mTestPath;

            // select the first row in the test steps
            if (TestStepRecordsForConversion.Count > 0)
            {
                dgTestSteps.SelectedIndex = 0;
            }

            dgTestSteps.DataContext = TestStepRecordsForConversion;
            dgTestSteps.Items.Refresh();

            InitializeParameters();
            CreateDefaultConversionSetting();
        }

        /// <summary>
        /// Populates the test steps from the file
        /// </summary>
        private void PopulateStepsForConversion(List<int> stepsForDataGeneration, int instance = 1)
        {
            try
            {
                ClearTestStepsGrid();

                foreach (DlkTestStepRecord tsr in mLoadedTest.mTestSteps)
                {
                    if (stepsForDataGeneration.Contains(tsr.mStepNumber))
                    {
                        tsr.mCurrentInstance = instance;
                        TestStepRecordsForConversion.Add(tsr);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Removes all data from the grid
        /// </summary>
        private void ClearTestStepsGrid()
        {
            TestStepRecordsForConversion = new ObservableCollection<DlkTestStepRecord>();
            dgTestSteps.DataContext = _mTestStepRecordsForConversion;
        }

        /// <summary>
        /// Updates parameter's colors in the grid
        /// </summary>
        private void ColorParams()
        {
            string executeVal = "";
            bool bInitPass = true;
            int ctr = 0;
            const int INT_EXECUTE_COL = 2;
            const int INT_DATAGRID_COL_HEADERS = 7;

            try
            {
                if (TestStepRecordsForConversion.Count > 0)
                {
                    foreach (TextBlock block in FindVisualChildren<TextBlock>(dgTestSteps)) // look for txtParams child control
                    {
                        ctr++;
                        // This will skip the initial 7 blocks as these are the datagrid column headers.
                        if (bInitPass)
                        {
                            if (ctr == INT_DATAGRID_COL_HEADERS)
                            {
                                ctr = 0;
                                bInitPass = false;
                            }
                            continue;
                        }
                        // To retrieve the value of the second column - Execute column
                        if (ctr == INT_EXECUTE_COL)
                        {
                            executeVal = block.Text;
                            if (executeVal.Contains("D{"))
                            {
                                block.Inlines.Clear();
                                block.Inlines.Add(new Run(executeVal) { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
                            }
                        }
                        else if (ctr == INT_DATAGRID_COL_HEADERS)
                        {
                            ctr = 0; // To reset the counter
                        }

                        if (block.Name == "txtParams")
                        {
                            if (executeVal != "False")
                            {
                                string[] paramFields = block.Text.Split('|');
                                block.Inlines.Clear();
                                int fieldCount = 1; // counter for number of parameters
                                foreach (string param in paramFields)
                                {
                                    if (param.Substring(0, param.Length > 1 ? 2 : 0) == "D{") // data-driven fields
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
                                    }
                                    else if (param.Substring(0, param.Length > 1 ? 2 : 0) == "O{") // output fields
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.Blue, FontWeight = FontWeights.Bold });
                                    }
                                    else
                                    {
                                        block.Inlines.Add(new Run(param));
                                    }
                                    if (fieldCount < paramFields.Length)
                                    {
                                        block.Inlines.Add(new Run("|") { Foreground = Brushes.Black });
                                    }
                                    fieldCount++;
                                }
                            }
                            else // For rows that contain D{ and O{ but have been set to False (execute)
                            {
                                string paramVal = block.Text;
                                block.Inlines.Clear();
                                block.Inlines.Add(new Run(paramVal) { Foreground = Brushes.Gray, FontWeight = FontWeights.Normal, FontStyle = FontStyles.Italic });
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Finds visual children of the selected object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        #endregion

    }

    public class ParameterContainsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Contains("D{") ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
