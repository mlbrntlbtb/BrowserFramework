using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.Common;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ImportResultsDialog.xaml
    /// </summary>
    public partial class VMsDialog : Window
    {
        public VMsDialog()
        {
            InitializeComponent();
            //lboxVMs.DataContext = DlkVMs.mVMs;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgVMs.DataContext = DlkVMs.mVMs;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditVMDialog dlgAddVM = new AddEditVMDialog("Add", "", "");
            dlgAddVM.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgVMs.SelectedItem != null)
            {
                AddEditVMDialog dlgEditVM = new AddEditVMDialog("Edit", ((VM)dgVMs.SelectedItem).Name, ((VM)dgVMs.SelectedItem).DataRoot);
                dlgEditVM.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgVMs.SelectedItem != null)
            {
                DlkVMs.DeleteVM(((VM)dgVMs.SelectedItem).Name);
            }
        }
        

    }
}
