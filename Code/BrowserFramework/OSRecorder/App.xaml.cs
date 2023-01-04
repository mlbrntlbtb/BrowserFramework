using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestRunner.AdvancedScheduler;
using TestRunner.Common;

namespace OSRecorder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Process[] processName = Process.GetProcessesByName("OSRecorder");
            if (processName.Length > 1)
            {
                DlkUserMessages.ShowError("Another instance of Object Store Recorder is currently running.");
                System.Environment.Exit(0);
            }
            else
            {
                var productSelection = new ProductSelection("Object Store Recorder");
               
                if ((bool)productSelection.ShowDialog())
                {
                    var application = new App();
                    application.InitializeComponent();
                    application.Run();
                }
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var osRecorderWindow = new TestRunner.OSRecorder() { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            osRecorderWindow.Show();
        }
    }
}
