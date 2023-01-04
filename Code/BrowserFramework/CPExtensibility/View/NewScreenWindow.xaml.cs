using CPExtensibility.ViewModel;
using System.Windows;

namespace CPExtensibility.View
{
    /// <summary>
    /// No code behind
    /// </summary>
    public partial class NewScreenWindow : Window
    {
        public NewScreenWindow()
        {
            InitializeComponent();
            var ViewModel = new NewScreenWindowViewModel();
            this.DataContext = ViewModel;
        }
    }
}
