using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TestRunner.AdvancedScheduler;
using System.Management;
using System.Diagnostics;

namespace AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ProductSelection productSelection;
        private static string mArgs = null;
        private const string warning_TitleCaption = "Warning";
        private const string warning_Message = "Scheduler is currently running. You may launch the scheduler by going to the system tray and clicking on the scheduler icon.";
        private const string warning_Message_OtherUser = "Scheduler is currently running at another user in this machine. Check the Task Manager for more details.";
        private const string appGUID = "5C0A108E-EAB2-4D8C-809C-9EF4C54FE7FF";

        [STAThread]
        public static void Main(string[] args)
        {
            using (var appLock = new DlkSingleInstanceLock(appGUID))
            {
                if (!appLock.TryAcquireExclusiveLock())
                {
                    string message = IsDifferentUserInstance() ? warning_Message_OtherUser : warning_Message;
                    MessageBox.Show(message, warning_TitleCaption, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }

                //checks if there are no parameters passed by Test Runner
                if (args.FirstOrDefault() != null)
                {
                    //Gets the product parameter passed by TestRunner
                    mArgs = args.FirstOrDefault();

                    productSelection = new ProductSelection();
                    productSelection.LoadFromTR(mArgs);

                    var application = new App();
                    application.InitializeComponent();
                    application.Run();
                }
                else
                {
                    productSelection = new ProductSelection();
                    if ((bool)productSelection.ShowDialog())
                    {
                        var application = new App();
                        application.InitializeComponent();
                        application.Run();
                    }
                }
            }            
        }

        /// <summary>
        /// On App Startup
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AdvancedSchedulerMainForm advancedScheduler;
            if (mArgs != null)
            {
                advancedScheduler = new AdvancedSchedulerMainForm(mArgs);
            }
            else
            {
                advancedScheduler = new AdvancedSchedulerMainForm(productSelection);
            }
            if (!advancedScheduler.mIsSchedulerLoaderClosing)
            {
                advancedScheduler.Show();
            }
        }

        /// <summary>
        /// Returns boolean value if the process owner is different from the current logged in user
        /// </summary>
        /// <returns>True if the user who started the instance is different, False if the user is the same</returns>
        private static bool IsDifferentUserInstance()
        {
            try
            {
                string user = string.Empty;

                Process[] processName = Process.GetProcessesByName("scheduler");
                if (processName == null) return false;

                foreach (var proc in processName)
                {
                    if (proc != null)
                    {
                        user = GetProcessUserByID(proc.Id);
                        string currentUser = Environment.UserName;

                        if (!user.Equals(Environment.UserName))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Get process user by given ID
        /// </summary>
        /// <param name="processID">ID of the process</param>
        /// <returns>String of username who owns/started the process</returns>
        private static string GetProcessUserByID(int processID)
        {
            string user = "Unknown";
            try
            {
                // Query the Win32_Process
                string query = $"Select * From Win32_Process Where ProcessID ='{processID}'";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection processList = searcher.Get();

                foreach (ManagementObject obj in processList)
                {
                    string[] argList = new string[] { string.Empty, string.Empty };
                    int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                    if (returnVal == 0)
                    {
                        user = argList[0];
                    }
                }
            }
            catch (Exception e)
            {
                //do nothing
            }
            return user;
        }
    }
}
