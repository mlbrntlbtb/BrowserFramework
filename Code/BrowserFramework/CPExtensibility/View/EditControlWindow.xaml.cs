using CPExtensibility.Messages;
using CPExtensibility.Utility;
using CPExtensibility.ViewModel;
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

namespace CPExtensibility.View
{
    /// <summary>
    /// Interaction logic for EditControlWindow.xaml
    /// </summary>
    public partial class EditControlWindow : Window
    {
        public EditControlWindow()
        {
            InitializeComponent();
            var ViewModel = new EditControlWindowViewModel();
            this.DataContext = ViewModel;
        }
    }
}
