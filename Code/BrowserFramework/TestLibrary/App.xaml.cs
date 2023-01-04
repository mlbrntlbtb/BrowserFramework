using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestRunner.Designer;

namespace TestLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            ProductSelection mf = new ProductSelection();
            if ((bool)mf.ShowDialog())
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
            }
        }
        
    }
}
