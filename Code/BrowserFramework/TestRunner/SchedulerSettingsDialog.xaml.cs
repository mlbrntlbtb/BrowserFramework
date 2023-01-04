using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SchedulerSettingsDialog.xaml
    /// </summary>
    public partial class SchedulerSettingsDialog : Window
    {
        #region PRIVATE MEMBERS
        private string filePath = string.Empty;
        private string _mySuite;
        private DlkScheduleRecord mCurrentSched;
        private string _mEmails;
        private bool bEmailDirty = false;
        private bool bScriptDirty = false;
        private List<DlkExternalScript> mPreScripts;
        private List<DlkExternalScript> mPostScripts;
        private List<DlkExternalScript> _mScripts;
        private DlkExternalScriptType mScriptType;
        #endregion

        #region PUBLIC PROPERTIES

        public List<DlkExternalScript> mScripts
        {
            get
            {
                return _mScripts;
            }
            set
            {
                _mScripts = value;
            }
        }

        public string mEmails
        {
            get
            {
                return _mEmails;
            }
            set
            {
                _mEmails = value;
            }
        }

        public bool mSendEmailOnExecution
        {
            get
            {
                return mCurrentSched.SendEmailOnExecutionStart;
            }
            set
            {
                mCurrentSched.SendEmailOnExecutionStart = (bool)chkSendEmailOnExecutionStart.IsChecked;
            }
        }
        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Checks if input string is valid email address
        /// </summary>
        /// <param name="inputEmail">Input email string</param>
        /// <returns>True if valid, otherwise False</returns>
        private bool IsEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private bool ValidateEmails(string input)
        {
            String eMails = input;
            string[] eMailAdds = eMails.Split(';');

            foreach (string m in eMailAdds)
            {
                if (string.IsNullOrEmpty(m))
                {
                    continue;
                }
                else if (IsEmail(m) == true)
                {
                    continue;
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_EMAIL_INVALID);
                    return false;
                }
            }

            return true;
        }

        private void UpdateButtonStates()
        {
            int idx = dgExtScripts.SelectedIndex;
            if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
            {
                btnMoveUp.IsEnabled = mPreScripts.Count > 1 && idx != 0;
                btnMoveDown.IsEnabled = mPreScripts.Count > 1 && idx != mPreScripts.Count - 1;
            }
            else
            {
                btnMoveUp.IsEnabled = mPostScripts.Count > 1 && idx != 0;
                btnMoveDown.IsEnabled = mPostScripts.Count > 1 && idx != mPostScripts.Count - 1;
            }
            
        }

        private void RefreshScripts()
        {
            mPreScripts = _mScripts.Where(x => x.Type == DlkExternalScriptType.PreExecutionScript).OrderBy(x => x.Order).ToList();
            mPostScripts = _mScripts.Where(x => x.Type == DlkExternalScriptType.PostExecutionScript).OrderBy(x => x.Order).ToList();

            if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
            {                
                dgExtScripts.DataContext = mPreScripts;
            }
            else
            {                
                dgExtScripts.DataContext = mPostScripts;
            }
           
            dgExtScripts_SelectionChanged(null, null);
        }

        private void MergeScripts()
        {
            _mScripts = new List<DlkExternalScript>();
            mPreScripts.AddRange(mPostScripts);
            _mScripts.AddRange(mPreScripts);
        }

        private void MoveDown(int index)
        {
            if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
            {
                int currorder = mPreScripts.ElementAt(index).Order;             
                mPreScripts[mPreScripts.IndexOf(mPreScripts.Find(x => (x.Order == (currorder + 1))))].Order = currorder;
                mPreScripts[index].Order = currorder + 1;
            }
            else
            {
                int currorder = mPostScripts.ElementAt(index).Order;
                mPostScripts[mPostScripts.IndexOf(mPostScripts.Find(x => (x.Order == (currorder + 1))))].Order = currorder;
                mPostScripts[index].Order = currorder + 1;
            }
        }

        private void MoveUp(int index)
        {
            if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
            {
                int currorder = mPreScripts.ElementAt(index).Order;
                mPreScripts[mPreScripts.IndexOf(mPreScripts.Find(x => (x.Order == (currorder - 1))))].Order = currorder;
                mPreScripts[index].Order = currorder - 1;
            }
            else
            {
                int currorder = mPostScripts.ElementAt(index).Order;
                mPostScripts[mPostScripts.IndexOf(mPostScripts.Find(x => (x.Order == (currorder - 1))))].Order = currorder;
                mPostScripts[index].Order = currorder - 1;
            }
        }

        private void DeleteScript(int index)
        {
            if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
            {
                int currorder = mPreScripts.ElementAt(index).Order;
                mPreScripts.RemoveAt(index);
                for (int i = currorder; i < mPreScripts.Count; i++)
                {
                    mPreScripts[mPreScripts.IndexOf(mPreScripts.Find(x => (x.Order == (i + 1))))].Order = i;
                }
            }
            else
            {
                int currorder = mPostScripts.ElementAt(index).Order;
                mPostScripts.RemoveAt(index);
                for (int i = currorder; i < mPostScripts.Count; i++)
                {
                    mPostScripts[mPostScripts.IndexOf(mPostScripts.Find(x => (x.Order == (i + 1))))].Order = i;
                }
            }
        }

        private int GetNewOrder()
        {
            int ret = 1;

            if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
            {
                if (mPreScripts.Count > 0)
                {
                    ret = mPreScripts.Last().Order + 1;                    
                }
            }
            else
            {
                if (mPostScripts.Count > 0)
                {
                    ret = mPostScripts.Last().Order + 1;
                }
            }
            return ret;
        }

        private DlkExternalScriptType GetScriptType(object scriptType){
            if (scriptType.ToString().Equals("Pre-execution Script"))
            {
                return DlkExternalScriptType.PreExecutionScript;
            }
            else
            {
                return DlkExternalScriptType.PostExecutionScript;
            }
        }

        #endregion

        #region PUBLIC METHODS
        public SchedulerSettingsDialog(string ScheduleFilePath, string SuitePath, DlkScheduleRecord CurrentSched)
        {
            InitializeComponent();
            filePath = ScheduleFilePath;
            _mySuite = SuitePath;
            mCurrentSched = CurrentSched;
            mPreScripts = new List<DlkExternalScript>();
            mPostScripts = new List<DlkExternalScript>();
            _mScripts = new List<DlkExternalScript>();
            foreach (DlkExternalScript scr in mCurrentSched.ExternalScripts)
            {
                _mScripts.Add(scr.Clone());
            }
        }
        #endregion

        #region EVENT HANDLERS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtMailAddresses.Text = mEmails;
                dgExtScripts.DataContext = _mScripts;
                chkSendEmailOnExecutionStart.IsChecked = mSendEmailOnExecution;

                List<string> lstScriptTypes = new List<string> { "Pre-execution Script", "Post-execution Script" };
                cbScriptType.DataContext = lstScriptTypes;
                cbScriptType.SelectedIndex = 0;
                cbScriptType_SelectionChanged(null, null);
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
                bool bIsValidEmail = true;
                if (bEmailDirty)
                {
                    bIsValidEmail = ValidateEmails(txtMailAddresses.Text);
                    if (bIsValidEmail)
                    {
                        mCurrentSched.Email = txtMailAddresses.Text;
                        mCurrentSched.SendEmailOnExecutionStart = (bool)chkSendEmailOnExecutionStart.IsChecked;
                    }
                    else if (tpgEmail.IsSelected != true)
                    {
                        //Check if tab is focused to Email Notification. If not, go to tab.
                        tpgEmail.IsSelected = true;
                    }
                }
                if (bScriptDirty)
                {
                        mCurrentSched.ExternalScripts = mScripts;                                       
                }
                if (bIsValidEmail)
                {
                    this.DialogResult = true;
                    this.Close();
                }
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
                this.Close();
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

        private void btnNewScript_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                SetExternalScriptDialog esDialog = new SetExternalScriptDialog(this, new DlkExternalScript(GetNewOrder(),mScriptType));
                esDialog.Owner = this;
                if (esDialog.ShowDialog() == true)
                {
                    bScriptDirty = true;
                    RefreshScripts();
                }
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
                DlkExternalScript mScript = ((DlkExternalScript)dgExtScripts.SelectedItem);
                SetExternalScriptDialog esDialog = new SetExternalScriptDialog(this,mScript);
                esDialog.Owner = this;
                if (esDialog.ShowDialog() == true)
                {
                    bScriptDirty = true;
                    //add to list
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
                    bScriptDirty = true;
                    DeleteScript(dgExtScripts.SelectedIndex);
                    MergeScripts();
                    RefreshScripts();
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
                bScriptDirty = true;
                MoveUp(dgExtScripts.SelectedIndex);
                RefreshScripts();
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
                bScriptDirty = true;
                MoveDown(dgExtScripts.SelectedIndex);
                RefreshScripts();
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

        private void cbScriptType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mScriptType = GetScriptType(cbScriptType.SelectedValue);
                if (mScriptType.Equals(DlkExternalScriptType.PreExecutionScript))
                {
                    tbScriptDesc.Text = "Scripts that execute before the suite run.";
                }
                else
                {
                    tbScriptDesc.Text = "Scripts that execute after the suite run.";
                }

                dgExtScripts.SelectedIndex = 0;
                RefreshScripts();
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
        #endregion
    }
}
