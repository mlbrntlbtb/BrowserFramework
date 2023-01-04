using CommonLib.DlkSystem;
using System;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for CheckinSourceControlDialog.xaml
    /// </summary>
    public partial class CheckinSourceControlDialog : Window
    {
        #region DECLARATIONS

        private SaveCheckinOptions mSaveCheckinChoice;
        private SaveCheckinOptions selectedOption;

        #endregion

        #region ENUMS

        public enum SaveCheckinOptions
        {
            SaveAndCheckin,
            SaveOnly,
            AbortSave
        }

        #endregion

        #region CONSTRUCTORS

        public CheckinSourceControlDialog(string message)
        {
            InitializeComponent();
            saveCheckinOptions.Text = message;
            SaveCheckinChoice = SaveCheckinOptions.AbortSave; //default
        }

        #endregion

        #region PROPERTIES

        public SaveCheckinOptions SaveCheckinChoice
        {
            get { return mSaveCheckinChoice; }
            set { mSaveCheckinChoice = value; }
        }

        #endregion

        #region EVENTS

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
                SaveCheckinChoice = selectedOption;
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
                //Abort Save
                this.DialogResult = false;
                SaveCheckinChoice = SaveCheckinOptions.AbortSave;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbSaveAndCheckin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = SaveCheckinOptions.SaveAndCheckin;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }  
        }

        private void rbSaveOnly_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = SaveCheckinOptions.SaveOnly;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion
    }
}
