using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using TestRunner.Common;
using System.Linq;
using CommonLib.DlkUtility;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ImportResultsDialog.xaml
    /// </summary>
    public partial class ExecuteScriptDialog : Window
    {
        private DlkTest mTest;
        private bool mShouldCollapseLogRow = true;
        private DataGridRow mCurrentLogRow = null;

        public ExecuteScriptDialog(DlkTest ExecutedTest)
        {
            InitializeComponent();
            DlkExecuteConverter.SetUpStep = true;
            mTest = ExecutedTest;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //StopExecution();
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //StopExecution();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void StopExecution()
        {
            
            //DlkEnvironment.KillProcessByName("nunit-agent");
            //DlkEnvironment.KillProcessByName("nunit-console");
            //this.Title = "Execution Stopped";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Title = "Execution Results : " + mTest.mTestName;
                lblResult.Content = mTest.mTestStatus.ToUpper();
                lblResult.Content = DlkTestRunnerApi.mCancellationPending ? "CANCELED" : lblResult.Content;
                lblResult.Foreground = lblResult.Content.ToString() != "PASSED" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
                DisplayLogResults();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        //void bw_ExecuteScript_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    this.Title = "Execution Finished.";
        //    DisplayLogResults();
        //}

        //void bw_ExecuteScript_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    lblScript.Content = DlkTestRunnerApi.mConsoleTestScript;
        //    txtLog.Text = DlkTestRunnerApi.ConsoleOutput;
        //}

        //void bw_ExecuteScript_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    //this.Title = "Executing...";
        //    DlkTestRunnerApi.ExecuteTest("");
        //    //lblScript.Content = DlkTestRunnerApi.mConsoleTestScript;
        //    while (DlkTestRunnerApi.mExecutionStatus != "idle")
        //    {
        //        bw_ExecuteScript.ReportProgress(0, DlkTestRunnerApi.ConsoleOutput);
        //        Thread.Sleep(500);
        //    }
        //    bw_ExecuteScript.ReportProgress(100);
        //}

        private void dgLogTestSteps_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
                if (e.Row.Item is DlkTestStepRecord)
                {
                }
                else
                {
                    return;
                }


                SolidColorBrush mStatusBrush = new SolidColorBrush(Colors.LightGreen);
                SolidColorBrush mRedBrush = new SolidColorBrush(Colors.Tomato);
                SolidColorBrush mSilverBrush = new SolidColorBrush(Colors.Silver);
                SolidColorBrush mSalmonBrush = new SolidColorBrush(Colors.Salmon);

                DlkTestStepRecord mRowRec = (DlkTestStepRecord)e.Row.DataContext;

                if (mRowRec.mStepNumber == 0)
                {
                    e.Row.Background = mSilverBrush;
                }
                foreach (DlkLoggerRecord lRec in mRowRec.mStepLogMessages)
                {
                    if (lRec.mMessageType.ToLower().Contains("exception"))
                    {
                        e.Row.Background = mRowRec.mStepNumber == 0 ? mSalmonBrush : mRedBrush;
                        return;
                    }
                }
                if (mRowRec.mStepStatus.ToLower() != "not run" & mRowRec.mStepNumber > 0)
                {
                    e.Row.Background = mStatusBrush;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportResultsDialog rs = new ExportResultsDialog(mTest);
                rs.Owner = this;
                rs.ShowDialog();
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private String mCurrentlyDisplayedLog { get; set; }
        private void DisplayLogResults()
        {
            
            dgLogTestSteps.DataContext = null;

            DlkTestStepRecord setup = new DlkTestStepRecord
            {
                //mStepNumber = 0,
                mExecute = "true",
                mScreen = "Test Setup",
                mControl = "",
                mKeyword = "",
                mStepDelay = 0,
                mStepStatus = "",
                mStepLogMessages = mTest.mTestSetupLogMessages,
                mStepStart = new DateTime(),
                mStepEnd = new DateTime(),
                mStepElapsedTime = "",
                mParameters = new List<string>()
            };

            setup.mStepStatus = setup.mStepLogMessages.FindAll(x => x.mMessageType.ToLower().Contains("exception")).Count > 0 ? "failed" : "passed";

            for (int idx = 0; idx < mTest.mInstanceCount; idx++)
            {
                setup.mParameters.Add("");
            }
            mTest.mTestSteps.Insert(0, setup);

            foreach (DlkTestStepRecord step in mTest.mTestSteps)
            {
                step.mCurrentInstance = mTest.mTestInstanceExecuted;
            }

            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct)
            {
                MaskedParameters();
            }
           
            dgLogTestSteps.DataContext = mTest.mTestSteps;
        }

        /// <summary>
        /// Masked password masked enabled parameters
        /// </summary>
        private void MaskedParameters()
        {
            foreach (DlkTestStepRecord step in mTest.mTestSteps)
            {
                if (step.mPasswordParameters != null)
                {
                    for (int index = 0; index < step.mParameters.Count(); index++)
                    {
                        string[] arrParameters = step.mParameters[index].Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                        for (int i = 0; i < arrParameters.Count(); i++)
                        {
                            if ((DlkPasswordMaskedRecord.IsMaskedParameter(step, i) && !DlkData.IsDataDrivenParam(step.mPasswordParameters[i])) ||
                                (DlkPasswordMaskedRecord.IsMaskedParameter(step, i) && !DlkData.IsOutPutVariableParam(step.mPasswordParameters[i])) ||
                                (DlkPasswordMaskedRecord.IsMaskedParameter(step, i) && !DlkData.IsGlobalVarParam(step.mPasswordParameters[i])))
                            {
                                string maskedText = "";
                                foreach (var item in arrParameters[i])
                                    maskedText += DlkPasswordMaskedRecord.PasswordChar;

                                arrParameters[i] = !String.IsNullOrWhiteSpace(maskedText) ? maskedText : DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE;
                            }
                        }
                        step.mParameters[index] = string.Join(DlkTestStepRecord.globalParamDelimiter, arrParameters);
                    }
                }
            }
        }

        #region CLICK_ERROR_IMAGE
        private DataGrid m_loggrid;

        private void LoadLogDetailsGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                string errorLogLevel = DlkEnvironment.mErrorLogLevel;
                m_loggrid = (DataGrid)sender;

                foreach (object dgr in m_loggrid.Items)
                {
                    DataGridRow _dgr = ((DataGridRow)(m_loggrid.ItemContainerGenerator.ContainerFromItem(dgr)));
                    foreach (DataGridCell cell in GetLogCellsFromRow(_dgr))
                    {
                        /* Hide rows based on settings */
                        DataGridColumn dgc = cell.Column;
                        if (dgc.Header.Equals("Type"))
                        {
                            switch (errorLogLevel)
                            {
                                case "default":
                                    _dgr.Visibility = Visibility.Visible;
                                    break;
                                case "errorinfo":
                                    if (((TextBlock)(cell.Content)).Text.Contains("EXCEPTIONIMG") || ((TextBlock)(cell.Content)).Text.Contains("INFO") || ((TextBlock)(cell.Content)).Text.Contains("EXCEPTIONMSG"))
                                    {
                                        _dgr.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        _dgr.Visibility = Visibility.Collapsed;
                                    }
                                    break;
                                case "erroronly":
                                    if (((TextBlock)(cell.Content)).Text.Contains("EXCEPTIONIMG") || ((TextBlock)(cell.Content)).Text.Contains("EXCEPTIONMSG"))
                                    {
                                        _dgr.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        _dgr.Visibility = Visibility.Collapsed;
                                    }
                                    break;
                                default:
                                    _dgr.Visibility = Visibility.Visible;
                                    //((DataGridRow)(m_loggrid.ItemContainerGenerator.ContainerFromItem(dgr))).Visibility = Visibility.Visible;
                                    break;
                            }
                        }


                        /* Make error image clickable */
                        string myCellContent = ((TextBlock)(cell.Content)).Text;
                        if (myCellContent.Contains("Image: ") || myCellContent.Contains("outputfile: ") || DlkString.IsFile(myCellContent))
                        {
                            cell.MouseLeftButtonUp -= LogDetailsClick;
                            cell.MouseLeftButtonUp += LogDetailsClick;
                            cell.MouseEnter += LogDetailsMouseOver;
                            break;
                        }
                    }
                }
            }
            catch
            {
                // do nothing. Just ignore, this is just to make error image clickable
            }
        }

        private List<DataGridCell> GetLogCellsFromRow(DataGridRow dgr)
        {
            List<DataGridCell> lst = new List<DataGridCell>();
            if (dgr != null)
            {
                GetVisualChildren<DataGridCell>(dgr, lst);
            }
            return lst;
        }

        public static T GetVisualChildren<T>(Visual parent, List<T> children) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChildren<T>(v, children);
                }
                else if (child != null)
                {
                    children.Add(child);
                }
            }
            return child;
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        private void LogDetailsMouseOver(object sender, RoutedEventArgs e)
        {
            try
            {
                ((DataGridCell)sender).Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LogDetailsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string pathToFile = "";

                var cellContent = ((TextBlock)(((DataGridCell)sender).Content)).Text;
                if (DlkString.IsFile(cellContent))
                {
                    pathToFile = cellContent;
                }
                else if(cellContent.Contains("Image:"))
                {
                    pathToFile = cellContent.Replace("Image: ", "");
                }
                else if (cellContent.Contains("outputfile:"))
                {
                    pathToFile = cellContent.Substring(cellContent.LastIndexOf("outputfile:") + 12);
                }

                if (pathToFile.Contains(".xml") || pathToFile.Contains(".html"))
                {
                    System.Diagnostics.Process.Start(pathToFile);
                }
                else 
                {
                    Process process = new Process();
                    ProcessStartInfo si = new ProcessStartInfo("OpenMspaint.bat");
                    si.Arguments = "\"" + pathToFile + "\"";
                    si.WindowStyle = ProcessWindowStyle.Hidden;
                    si.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    process.StartInfo = si;
                    process.Start();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        private void LogRow_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                /* Expand if there is a collapsed row OR user switched rows */
                if (dgLogTestSteps.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.Collapsed || sender != mCurrentLogRow)
                {
                    dgLogTestSteps.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                }
                else if (mShouldCollapseLogRow) /* Do not collapse if log row details is clicked */
                {
                    dgLogTestSteps.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
                }
                mShouldCollapseLogRow = true;
                mCurrentLogRow = sender as DataGridRow; // cache clicked row
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LogRowDetails_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                mShouldCollapseLogRow = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion

    }

    public class DlkStepNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (int.Parse(value.ToString()) <= 0)
            {
                return "SETUP";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class DlkScreenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "Test Setup")
            {
                return string.Empty;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class DlkExecuteConverter : IValueConverter
    {
        public static bool SetUpStep = true;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (SetUpStep)
            {
                SetUpStep = false;
                return string.Empty;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
