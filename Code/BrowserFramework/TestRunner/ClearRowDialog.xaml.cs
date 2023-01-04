using CommonLib.DlkSystem;
using System;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ClearRowDialog.xaml
    /// </summary>
    public partial class ClearRowDialog : Window
    {
        public ClearRowDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
