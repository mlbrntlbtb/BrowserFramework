using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for PopulateReportDBDialog.xaml
    /// </summary>
    public partial class PopulateReportDBDialog : Window
    {
        public PopulateReportDBDialog(String Progress)
        {
            InitializeComponent();
            double dScreenHeight = SystemParameters.FullPrimaryScreenHeight;
            double dScreenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = dScreenHeight - this.Height - 5;
            this.Left = dScreenWidth - this.Width - 5;

            lblStatus.Content = "Importing Results " + Progress + ":";
        }

        public void UpdateProgress(String Progress)
        {
            lblStatus.Content = "Importing Results " + Progress + ":";
        }

    }
}
