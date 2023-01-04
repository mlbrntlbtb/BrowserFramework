using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CPExtensibility.ViewModel;

namespace CPExtensibility.View
{
    /// <summary>
    /// Interaction logic for CPExtensibilityMainForm.xaml, since we are using MVVM, aim for a 'NO CODE BEHIND' architecture
    /// </summary>
    public partial class CPExtensibilityMainForm : Window
    {
        CPExtensibilityMainFormViewModel viewModel = new CPExtensibilityMainFormViewModel();
        public CPExtensibilityMainForm()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
        }
        public CPExtensibilityMainForm(ProductSelection product)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
        }
    }
}
