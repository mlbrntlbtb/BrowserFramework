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

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for OpenInBrowserOptionDialog.xaml
    /// </summary>
    public partial class OpenInBrowserOptionDialog : Window
    {
        #region DECLARATIONS

        private EmailOpenBrowserOptions mEmailOpenBrowserChoice;
        private EmailOpenBrowserOptions selectedOption;

        #endregion

        #region ENUMS

        public enum EmailOpenBrowserOptions
        {
            EmailAndOpenBrowser,
            EmailOnly,
            AbortEmail
        }

        #endregion

        #region CONSTRUCTORS

        public OpenInBrowserOptionDialog()
        {
            InitializeComponent();
            EmailOpenBrowserChoice = EmailOpenBrowserOptions.AbortEmail; //default
        }

        #endregion

        #region PROPERTIES

        public EmailOpenBrowserOptions EmailOpenBrowserChoice
        {
            get { return mEmailOpenBrowserChoice; }
            set { mEmailOpenBrowserChoice = value; }
        }

        #endregion

        #region EVENTS

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
                EmailOpenBrowserChoice = selectedOption;
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
                EmailOpenBrowserChoice = EmailOpenBrowserOptions.AbortEmail;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbEmailAndOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = EmailOpenBrowserOptions.EmailAndOpenBrowser;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }  
        }

        private void rbEmailOnly_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = EmailOpenBrowserOptions.EmailOnly;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion
    }
}
