using System;
using System.Windows;
using CommonLib.DlkUtility;
using CommonLib.DlkSystem;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for CheckInCommentsDialog.xaml
    /// </summary>
    public partial class CheckInCommentsDialog : Window
    {
        private string m_Comments = string.Empty;
        public CheckInCommentsDialog()
        {
            InitializeComponent();
        }

        public string UserComments
        {
            get
            {
                return m_Comments;
            }
            set
            {
                m_Comments = value;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtComment.Text))
                {
                    m_Comments = "/comment:TestRunner";
                }
                else
                {
                    m_Comments = DlkString.RemoveCarriageReturn(txtComment.Text);
                }
                this.DialogResult = true;
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

    }
}
