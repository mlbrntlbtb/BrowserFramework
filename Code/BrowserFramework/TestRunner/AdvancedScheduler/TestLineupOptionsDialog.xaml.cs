using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for EmailSettingsDialog.xaml
    /// </summary>
    public partial class TestLineupOptionsDialog : Window
    {
        private List<TestLineupRecord> _recordList;
        private List<ExternalScript> postScripts;
        private List<ExternalScript> preScripts;
        private Enumerations.ExetrnalScriptType mScriptType;
        private bool bEmailDirty = false;
        private bool  bScriptsDirty;
        private bool? bRerunFailedTestOnlyOrigState = null;
        private bool? bRerunConsolidatedReportOrigState = null;

        #region Constructor
        public TestLineupOptionsDialog(List<TestLineupRecord> recordList)
        {
            InitializeComponent();

            _recordList = recordList;

            preScripts = new List<ExternalScript>();
            postScripts = new List<ExternalScript>();

            if (recordList.Count == 1)
            {
                //Using a copy of the pre and post scripts since there is a backgroundworker constantly updating the schedule file
                foreach (ExternalScript s in _recordList.First().PreExecutionScripts)
                {
                    preScripts.Add((ExternalScript)s.Clone());
                }

                foreach (ExternalScript s in _recordList.First().PostExecutionScripts)
                {
                    postScripts.Add((ExternalScript)s.Clone());
                }

                //setup UI
                txtMailAddresses.Text = string.Join(";", _recordList.First().DistributionList);
                chkSendEmailOnExecutionStart.IsChecked = _recordList.First().EmailOnStart;
            }

            dgExtScripts.ItemsSource = preScripts;
            cboRerunLimit.ItemsSource = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
        }
        #endregion

        #region Properties
        public List<string> ExternalScriptType
        {
            get
            {
                return new List<string>()
                {
                    Enumerations.ExetrnalScriptType.PreExecution.GetDescription(),
                    Enumerations.ExetrnalScriptType.PostExecution.GetDescription()
                };
            }
        }
        #endregion

        #region Event Handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbScriptType.SelectedItem = ExternalScriptType.FirstOrDefault();

            /* re-run values */
            var rerunLimit = _recordList.First().NumberOfRuns;
            var rerunOnlyFailed = _recordList.First().RerunFailedTestsOnly;
            var sendConsolidatedReport = _recordList.First().SendConsolidatedReport;

            cboRerunLimit.Text = rerunLimit.ToString();
            chkRerunOnlyFailed.IsChecked = rerunOnlyFailed;
            chkConsolidatedReport.IsChecked = sendConsolidatedReport;

            if(!_recordList.All(x=>x.NumberOfRuns == rerunLimit))
                cboRerunLimit.Text = "--";

            if(!_recordList.All(x=>x.RerunFailedTestsOnly == rerunOnlyFailed))
                chkRerunOnlyFailed.IsChecked = null;

            if(!_recordList.All(x=>x.SendConsolidatedReport == sendConsolidatedReport))
                chkConsolidatedReport.IsChecked = null;

            bRerunFailedTestOnlyOrigState = chkRerunOnlyFailed.IsChecked;
            bRerunConsolidatedReportOrigState = chkConsolidatedReport.IsChecked;
            /* re-run values */

            UpdateButtonStates();
            bEmailDirty = false;
        }

        private void btnNewScript_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newOrder = mScriptType == Enumerations.ExetrnalScriptType.PreExecution ? preScripts.Count : postScripts.Count;
                ExternalScript newScript = new ExternalScript() { Type = mScriptType, Order = newOrder };
                AddEditExternalScript addEScriptDlg = new AddEditExternalScript(newScript, true);
                addEScriptDlg.Owner = this;
                if ((bool)addEScriptDlg.ShowDialog())
                {
                    if (mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
                    {
                        preScripts.Add(newScript);
                    }
                    else
                    {
                        postScripts.Add(newScript);
                    }
                    RefreshUI(newOrder);
                    bScriptsDirty = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cbScriptType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mScriptType = Enumerations.GetValueFromDescription<Enumerations.ExetrnalScriptType>(cbScriptType.SelectedValue.ToString());
                if (mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
                {
                    tbScriptDesc.Text = "* Scripts that execute before the suite run.";
                    dgExtScripts.ItemsSource = preScripts;

                }
                else
                {
                    tbScriptDesc.Text = "* Scripts that execute after the suite run.";
                    dgExtScripts.ItemsSource = postScripts;
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rerunLimitValue;
                bool bReRunLimitDirty = IsRerunLimitConfigChanged(out rerunLimitValue);
                bool bRerunFailedTestOnlyDirty = chkRerunOnlyFailed.IsChecked != null && bRerunFailedTestOnlyOrigState != chkRerunOnlyFailed.IsChecked;
                bool bRerunConsolidatedReportDirty = chkConsolidatedReport.IsChecked != null && bRerunConsolidatedReportOrigState != chkConsolidatedReport.IsChecked;
                bool isModified = bEmailDirty || bScriptsDirty || bReRunLimitDirty || bRerunFailedTestOnlyDirty || bRerunConsolidatedReportDirty;

                //validate emails
                if (!AreEmailAddressesValid(txtMailAddresses.Text))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_EMAIL_INVALID);
                    return;
                }

                //save changes if any
                if (isModified)
                {
                    foreach (var record in _recordList)
                    {
                        //update emails
                        if (bEmailDirty)
                        {
                            record.DistributionList = txtMailAddresses.Text.Split(';').Select(x=>x.Trim()).ToList();
                            record.EmailOnStart = (bool)chkSendEmailOnExecutionStart.IsChecked;
                        }

                        //update scripts
                        if (bScriptsDirty)
                        {
                            record.PreExecutionScripts = preScripts;
                            record.PostExecutionScripts = postScripts;
                        }

                        //update rerun limit
                        if (bReRunLimitDirty)
                        {
                            record.NumberOfRuns = rerunLimitValue;
                        }

                        //update rerun run failed test flag
                        if (bRerunFailedTestOnlyDirty)
                        {
                            record.RerunFailedTestsOnly = (bool)chkRerunOnlyFailed.IsChecked;
                        }

                        //update rerun consolidated report flag
                        if (bRerunConsolidatedReportDirty)
                        {
                            record.SendConsolidatedReport = (bool)chkConsolidatedReport.IsChecked;
                        }
                    }
                }

                this.DialogResult = isModified;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEditScript_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExternalScript mScript = ((ExternalScript)dgExtScripts.SelectedItem);
                AddEditExternalScript editESDlg = new AddEditExternalScript(mScript);
                editESDlg.Owner = this;
                if (editESDlg.ShowDialog() == true)
                {
                    if (mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
                    {
                        preScripts[dgExtScripts.SelectedIndex] = mScript;
                    }
                    else
                    {
                        postScripts[dgExtScripts.SelectedIndex] = mScript;
                    }
                    RefreshUI(dgExtScripts.SelectedIndex);
                    bScriptsDirty = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteScript_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_EXTERNAL_SCRIPT, "Delete External Script") == MessageBoxResult.Yes)
                {
                    int currIndex = dgExtScripts.SelectedIndex;
                    DeleteScript(currIndex);
                    RefreshUI(dgExtScripts.Items.Count > 0 ? currIndex - (currIndex == 0 ? 0 : 1) : -1);
                    bScriptsDirty = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgExtScripts.SelectedIndex <= 0)
                {
                    return;
                }

                int currIndex = dgExtScripts.SelectedIndex;
                MoveUp(currIndex);
                RefreshScripts();
                RefreshUI(currIndex - 1);
                bScriptsDirty = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgExtScripts.SelectedIndex >= dgExtScripts.Items.Count)
                {
                    return;
                }

                int currIndex = dgExtScripts.SelectedIndex;
                MoveDown(currIndex);
                RefreshScripts();
                RefreshUI(currIndex + 1);
                bScriptsDirty = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgExtScripts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateButtonStates();
                if (dgExtScripts.SelectedIndex >= 0)
                {
                    btnEditScript.IsEnabled = true;
                    btnDeleteScript.IsEnabled = true;
                }
                else
                {
                    btnEditScript.IsEnabled = false;
                    btnDeleteScript.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ToolBar toolBar = sender as ToolBar;
                var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
                if (overflowGrid != null)
                {
                    overflowGrid.Visibility = Visibility.Collapsed;
                }

                var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
                if (mainPanelBorder != null)
                {
                    mainPanelBorder.Margin = new Thickness(0);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkSendEmailOnExecutionStart_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bEmailDirty = true;
                chkSendEmailOnExecutionStart.IsChecked = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkSendEmailOnExecutionStart_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                bEmailDirty = true;
                chkSendEmailOnExecutionStart.IsChecked = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtMailAddresses_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bEmailDirty = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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

        private void cboRerunLimit_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValidNum = int.TryParse(cboRerunLimit.Text, out int NewValue);
            if (!isValidNum)
            {
                cboRerunLimit.Text = "1";
                cboRerunLimit.Focus();
                MessageBox.Show("Value is not a valid number of reruns. Only numerical values are allowed.", "No. of Runs", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (NewValue >= 10 || NewValue <= 0)
            {
                cboRerunLimit.Text = "1";
                cboRerunLimit.Focus();
                MessageBox.Show("Value exceeds maximum number of reruns. Allowed number of reruns is from [1 - 9] only.", "No. of Runs", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cboRerunLimit_GotFocus(object sender, RoutedEventArgs e)
        {
            var obj = (ComboBox)sender;
            if (obj != null)
            {
                var tBoxRerun = (TextBox)obj.Template.FindName("PART_EditableTextBox", obj);
                if (tBoxRerun != null)
                {
                    tBoxRerun.MaxLength = 1;
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Refreshes the UI
        /// </summary>
        /// <param name="selectedIndex">The currently selected index</param>
        private void RefreshUI(int selectedIndex = -1)
        {
            if(mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
            {
                dgExtScripts.ItemsSource = preScripts;
            }
            else
            {
                dgExtScripts.ItemsSource = postScripts;
            }
            
            dgExtScripts.Items.Refresh();
            dgExtScripts.SelectedIndex = dgExtScripts.Items.Count > 0 ? selectedIndex : -1;
            UpdateButtonStates();
        }

        /// <summary>
        /// Update the button states
        /// </summary>
        private void UpdateButtonStates()
        {
            btnMoveUp.IsEnabled = dgExtScripts.SelectedIndex > 0;
            btnMoveDown.IsEnabled = dgExtScripts.SelectedIndex < dgExtScripts.Items.Count - 1 && dgExtScripts.SelectedIndex >= 0;
        }

        /// <summary>
        /// Delete script at the specified index from the list
        /// </summary>
        /// <param name="index">Index of script to be deleted</param>
        private void DeleteScript(int index)
        {
            if (mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
            {
                int currorder = preScripts.ElementAt(index).Order;
                preScripts.RemoveAt(index);
                for (int i = currorder; i < preScripts.Count; i++)
                {
                    preScripts[preScripts.IndexOf(preScripts.Find(x => (x.Order == (i + 1))))].Order = i;
                }
            }
            else
            {
                int currorder = postScripts.ElementAt(index).Order;
                postScripts.RemoveAt(index);
                for (int i = currorder; i < postScripts.Count; i++)
                {
                    postScripts[postScripts.IndexOf(postScripts.Find(x => (x.Order == (i + 1))))].Order = i;
                }
            }
        }
        
        /// <summary>
        /// Move script up 
        /// </summary>
        /// <param name="index"></param>
        private void MoveUp(int index)
        {
            if (mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
            {
                int destIndex = index - 1; //Move one place up
                preScripts[index].Order = destIndex;
                preScripts[destIndex].Order = index;
            }
            else
            {
                int destIndex = index - 1; //Move one place up
                postScripts[index].Order = destIndex;
                postScripts[destIndex].Order = index;
            }
        }

        /// <summary>
        /// Move script down
        /// </summary>
        /// <param name="index"></param>
        private void MoveDown(int index)
        {
            if (mScriptType == Enumerations.ExetrnalScriptType.PreExecution)
            {
                int destIndex = index + 1; //Move one place up
                preScripts[index].Order = destIndex;
                preScripts[destIndex].Order = index;
            }
            else
            {
                int destIndex = index + 1; //Move one place up
                postScripts[index].Order = destIndex;
                postScripts[destIndex].Order = index;
            }
        }
       
        /// <summary>
        /// Reorder the script based on Order
        /// </summary>
        private void RefreshScripts()
        {
            preScripts = preScripts.OrderBy(x => x.Order).ToList();
            postScripts = postScripts.OrderBy(x => x.Order).ToList();
        }

        /// <summary>
        /// Check if email addresses are valid. Emails are expected to be seperated by semi colon.
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        private bool AreEmailAddressesValid(string emails)
        {
            List<string> distributionList = emails.Split(';').ToList();

            //if empty - allow it
            if (string.IsNullOrEmpty(emails.Trim()))
            {
                return true;
            }

            //if there are invalid emails, return null
            if(distributionList.Any(x => !DlkString.IsValidEmail(x.Trim())))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if re-run limit settings were changed
        /// </summary>
        /// <returns>Flag if Re-run settings are changed</returns>
        private bool IsRerunLimitConfigChanged(out int NewValue)
        {
            NewValue = -1;
            bool isValidNum = int.TryParse(cboRerunLimit.Text, out NewValue);
            /* if 1 item -? return if value changed, if mulitple, return if '--' initial value was change to valid value */
            return _recordList.Count == 1 ? _recordList.First().NumberOfRuns != NewValue
                : isValidNum;
        }
        #endregion
    }
}
