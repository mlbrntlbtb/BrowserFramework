using CPExtensibility.ViewModel;
using System.Windows;

namespace CPExtensibility.View
{
    /// <summary>
    /// No code behind
    /// </summary>
    public partial class EditScreenWindow : Window
    {
        public EditScreenWindow()
        {
            InitializeComponent();
            var ViewModel = new EditScreenWindowViewModel();
            this.DataContext = ViewModel;
        }
    }
}
