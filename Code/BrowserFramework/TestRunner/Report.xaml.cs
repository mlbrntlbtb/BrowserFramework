using CommonLib.DlkSystem;
using System;
using System.Windows;
using System.Windows.Controls;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
        }

        private void dgExecutions_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
