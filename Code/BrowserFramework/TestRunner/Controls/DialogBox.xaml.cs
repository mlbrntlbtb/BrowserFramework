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

namespace TestRunner.Controls
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    public partial class DialogBox : Window
    {
        public DialogBox(string title, string message, bool hasInput, string defaultText="")
        {
            InitializeComponent();
            Title = title;
            lblMessage.Text = message;
            txtBox.Text = defaultText;
            if (!hasInput)
            {
                txtBox.Visibility = System.Windows.Visibility.Collapsed;
                btnOK.Content = "Yes";
                btnCancel.Content = "No";
            }
        }

        /// <summary>
        /// Constructor for confirmation box only
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public DialogBox(string title, string message)
        {
            InitializeComponent();
            Title = title;
            lblMessage.Text = message;
            txtBox.Visibility = System.Windows.Visibility.Collapsed;
            btnOK.Content = "OK";
            btnCancel.Visibility = System.Windows.Visibility.Collapsed;
        }

        public string TextBoxValue
        {
            get { return txtBox.Text; }
            set { txtBox.Text = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
