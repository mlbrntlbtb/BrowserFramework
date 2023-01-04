using CommonLib.DlkSystem;
using System;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestSuiteSummaryDialog.xaml
    /// </summary>
    public partial class TestSuiteSummaryDialog : Window
    {
        public TestSuiteSummaryDialog()
        {
            InitializeComponent();
        }

        private void btnCloseSummary_Click(object sender, RoutedEventArgs e)
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
