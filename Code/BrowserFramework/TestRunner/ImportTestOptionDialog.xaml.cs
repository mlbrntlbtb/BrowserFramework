using CommonLib.DlkSystem;
using System;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ImportTestOptionDialog.xaml
    /// </summary>
    public partial class ImportTestOptionDialog : Window
    {
        #region DECLARATIONS

        private ImportTestOptions mImportOption;
        private ImportTestOptions selectedOption;

        #endregion

        #region ENUMS

        public enum ImportTestOptions
        {
            File,
            Folder,
            AbortImport
        }

        #endregion

        #region CONSTRUCTORS

        public ImportTestOptionDialog(String fileType)
        {
            InitializeComponent();
            saveCheckinOptions.Text = string.Format(saveCheckinOptions.Text, fileType);
            mImportOption = ImportTestOptions.AbortImport; //default
        }

        #endregion

        #region PROPERTIES

        public ImportTestOptions ImportOption
        {
            get { return mImportOption; }
            set { mImportOption = value; }
        }

        #endregion

        #region EVENTS

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
                ImportOption = selectedOption;
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
                //Cancel Import
                this.DialogResult = false;
                ImportOption = ImportTestOptions.AbortImport;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbImportFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = ImportTestOptions.File;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbImportFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = ImportTestOptions.Folder;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion
    }
}
