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
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditVMDialog : Window
    {
        private String mMode = "";

        public AddEditVMDialog()
        {
            InitializeComponent();
        }

        public AddEditVMDialog(String Mode, String Name, String DataRoot)
        {
            InitializeComponent();

            mMode = Mode;
            if (mMode == "add")
            {
                this.Title = "Add VM";
            }
            else
            {
                this.Title = "Edit VM";
                txtName.Text = Name;
                txtDataRoot.Text = DataRoot;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            
            DlkVMs.AddEditVM(txtName.Text, txtDataRoot.Text);
            
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
