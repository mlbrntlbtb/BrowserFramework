using System;
using System.Windows;
using System.IO;
using TestRunner.Common;
using CommonLib.DlkSystem;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddHost.xaml
    /// </summary>
    public partial class AddNewFolder : Window
    {
        private string m_ParentFolder = "";
        public AddNewFolder(string ParentFolderFullPath)
        {
            m_ParentFolder = ParentFolderFullPath;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate(out bool isDuplicate))
                {
                    Add();
                    //SaveHostsToFile(mHosts);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    DlkUserMessages.ShowError(isDuplicate ? 
                        DlkUserMessages.ERR_FOLDER_NAME_ALREADY_EXISTS : DlkUserMessages.ERR_FOLDERNAME_INVALID);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool Validate(out bool IsDuplicate)
        {
            IsDuplicate = false;

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                return false;
            }
            if (txtName.Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                return false;
            }
            if (Directory.Exists(System.IO.Path.Combine(m_ParentFolder, txtName.Text.Trim())))
            {
                IsDuplicate = true;
                return false;
            }
            return true;
        }

        private void Add()
        {
            Directory.CreateDirectory(System.IO.Path.Combine(m_ParentFolder, txtName.Text));
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
    }
}
