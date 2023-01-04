using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
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
using TestRunner.AdvancedScheduler.Model;
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for AddEditExternalScript.xaml
    /// </summary>
    public partial class AddEditExternalScript : Window
    {
        private ExternalScript _script;
        private bool _isNew;

        public AddEditExternalScript(ExternalScript script, bool isNew = false)
        {
            InitializeComponent();
            _script = script;
            _isNew = isNew;
        }

        #region Private Functions
        /// <summary>
        /// Assign corresponding UI values to script
        /// </summary>
        /// <returns></returns>
        private bool SetScript()
        {
            try
            {
                _script.Path = txtPath.Text;
                _script.Arguments = txtArgs.Text;
                _script.StartIn = txtStartIn.Text;
                _script.WaitToFinish = cbWait.IsChecked.Value;
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handler for window loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = _isNew ? "New External Script" : "Edit External Script";
            txtPath.Text = _script.Path;
            txtArgs.Text = _script.Arguments;
            txtStartIn.Text = _script.StartIn;

            cbWait.IsChecked = _script.WaitToFinish;
            if (_script.Type == Enumerations.ExetrnalScriptType.PreExecution)
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

        /// <summary>
        /// Handler for Browse button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handler for Cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handler for OK button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtPath.Text))
                {
                    if (SetScript())
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_UNEXPECTED_ERROR);
                    }
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_SCRIPT_PATH);
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
