using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CPExtensibility;
using CPExtensibility.View;

namespace CPExtensibility
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex myMutex;
        public static ProductSelection productSelection;

        [STAThread]
        public static void Main()
        {
            //Make sure that only one instance is allowed to run at a single time
            bool aIsNewInstance = false;
            myMutex = new Mutex(true, "CPExtensibility", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("Another instance of the extensibility tool is currently running.");
                return;
            }

            productSelection = new ProductSelection();
            if ((bool)productSelection.ShowDialog())
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CPExtensibilityMainForm mainForm = new CPExtensibilityMainForm(productSelection);
            mainForm.Show();
        }
    }
}
