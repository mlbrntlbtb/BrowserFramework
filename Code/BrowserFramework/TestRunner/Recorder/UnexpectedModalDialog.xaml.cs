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
using TestRunner.Common;

namespace Recorder
{
    /// <summary>
    /// Interaction logic for CustomModalDialog.xaml
    /// </summary>
    public partial class UnexpectedModalDialog : Window
    {
        public UnexpectedModalDialog(string Message, string Caption, bool IsTopMost=false)
        {
            InitializeComponent();
            this.Title = Caption;
            this.txtMessage.Text = Message;
            this.Topmost = IsTopMost;
        }

        public UnexpectedModalDialog(string Message, string Caption, System.Drawing.Point ScreenPosition, bool IsTopMost = false)
        {
            InitializeComponent();
            this.Title = Caption;
            this.txtMessage.Text = Message;
            this.Topmost = IsTopMost;

            this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            this.Left = ScreenPosition.X - this.Width/2;
            this.Top = ScreenPosition.Y - this.Height/2;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
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
    }
}
