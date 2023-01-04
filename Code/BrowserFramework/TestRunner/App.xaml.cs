using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using CommonLib.DlkSystem;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                SplashScreen ss = new SplashScreen();
                ss.ShowDialog();
                var application = new App();
                application.InitializeComponent();
                application.Run();

                //EnterProductKey license = new EnterProductKey();
                //if (license.GetLicenseKeyStatus() != LicenseStatusType.Valid )
                //{
                //    if ((bool)license.ShowDialog())
                //    {
                //        SplashScreen ss = new SplashScreen();
                //        ss.ShowDialog();
                //        var application = new App();
                //        application.InitializeComponent();
                //        application.Run();
                //    }
                //}
                //else
                //{
                //    SplashScreen ss = new SplashScreen();
                //    ss.ShowDialog();
                //    var application = new App();
                //    application.InitializeComponent();
                //    application.Run();
                //}
            }
            catch (Exception e)
            {
                // this is where you handle all test runner 'unhandled' UI related errors
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, e);
            }
        }
    }
}
