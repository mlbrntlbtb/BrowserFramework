using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddScheduleDialog.xaml
    /// </summary>
    public partial class SetExternalScriptDialog : Window
    {
        #region PRIVATE MEMBERS

        private DlkExternalScript mExtScript;
        private SchedulerSettingsDialog mOwner;
        private List<DlkExternalScript> _mScripts;

        #endregion
        
        #region PRIVATE METHODS

        private bool SetScript()
        {
            try
            {
                if (_mScripts.FindAll(x => x.Type == mExtScript.Type && x.Order == mExtScript.Order).Count() > 0)
                {
                    //if script exists, edit
                    _mScripts[_mScripts.IndexOf(_mScripts.Find(x => x.Type == mExtScript.Type && x.Order == mExtScript.Order))].Path = txtPath.Text;
                    _mScripts[_mScripts.IndexOf(_mScripts.Find(x => x.Type == mExtScript.Type && x.Order == mExtScript.Order))].Arguments = txtArgs.Text;
                    _mScripts[_mScripts.IndexOf(_mScripts.Find(x => x.Type == mExtScript.Type && x.Order == mExtScript.Order))].StartIn = txtStartIn.Text;
                    _mScripts[_mScripts.IndexOf(_mScripts.Find(x => x.Type == mExtScript.Type && x.Order == mExtScript.Order))].WaitToFinish = cbWait.IsChecked.Value;
                }
                else
                {
                    //if script is not existing, add new
                    DlkExternalScript mNewScript = new DlkExternalScript(txtPath.Text, mExtScript.Order, txtArgs.Text, txtStartIn.Text, cbWait.IsChecked.Value, mExtScript.Type);
                    mOwner.mScripts.Add(mNewScript);
                }               
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region PUBLIC METHODS

        public SetExternalScriptDialog(SchedulerSettingsDialog Owner, DlkExternalScript Script)
        {
            InitializeComponent();
            mExtScript = Script;
            mOwner = Owner;
            _mScripts = mOwner.mScripts;
        }

        #endregion

        #region EVENT HANDLERS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtPath.Text = mExtScript.Path;
                txtArgs.Text = mExtScript.Arguments;
                txtStartIn.Text = mExtScript.StartIn;
                cbWait.IsChecked = mExtScript.WaitToFinish;

                if (mExtScript.Type == DlkExternalScriptType.PreExecutionScript)
                {
                    lbTypeDesc.Content = "Run a program/script before suite execution.";
                    gbScript.Header = "Pre-execution Script";
                }
                else
                {
                    lbTypeDesc.Content = "Run a program/script after suite execution.";
                    gbScript.Header = "Post-execution Script";
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                if (!String.IsNullOrWhiteSpace(txtPath.Text))
                {
                    if (SetScript())
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_UNEXPECTED_ERROR, "Set script failed");
                    }                    
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_SCRIPT_PATH, "Set script failed");

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

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var ofDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    ofDialog.InitialDirectory = DlkEnvironment.mRootDir;
                    ofDialog.Filter = "Batch file (*.bat)|*.bat|Executable file (*.exe)|*.exe";
                    ofDialog.Title = "Set Execution Script";
                    ofDialog.Multiselect = false;

                    if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtPath.Text = ofDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion
    }
}
