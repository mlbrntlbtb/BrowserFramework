using METHConverter.Utilities;
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

namespace METHConverter
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : Window
    {
        public ResultView(int FailScriptCount, int TotalScriptCount, List<String> RunLogs)
        {
            InitializeComponent();
            int SuccessCount = TotalScriptCount - FailScriptCount;

            lblSuccessful.Content = SuccessCount.ToString();
            lblFailed.Content = FailScriptCount.ToString();
            lblTotal.Content = TotalScriptCount.ToString();

            StringBuilder mLogs = new StringBuilder();
            foreach(string log in RunLogs)
            {
                mLogs.AppendLine(log);
            }

            txtLogs.Text = mLogs.ToString();

            StringBuilder mErrorLogs = new StringBuilder();
            List<string> errorLogs = RunLogs.Where(x => x.Contains("[ERR]") || x.Contains("[WRN]")).ToList();
            foreach(string log in errorLogs)
            {
                mErrorLogs.AppendLine(log);
            }
            txtErrorLogs.Text = mErrorLogs.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
