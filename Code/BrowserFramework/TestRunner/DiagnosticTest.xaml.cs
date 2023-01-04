using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for DiagnosticTest.xaml
    /// </summary>
    public partial class DiagnosticTest : Window
    {
        List<DiagnosticTestProgress> Tests = new List<DiagnosticTestProgress>();
        List<DiagnosticTestProgress> DirOptions = new List<DiagnosticTestProgress>();
        string[] testName =
            {
                "Minimum System Requirements",
                "Core Directories and Files",
                "Installed Browser and WebDriver Version",
                "Shared Folder and Permissions"
            };

        string[] dirOptions =
            {
                "Test Script",
                "Test Script Result",
                "Test Suite",
                "Test Suite Result"
            };

        string DESC_FULL_DIAGNOSTIC = "Run all diagnostic tests.\n\nEstimated Duration:\n3 minutes to 5 minutes";
        string DESC_MIN_REQUIREMENT = "Run checks on the machine if it meets the minimum system requirements defined in the Test Runner User Guide.\n\nEstimated Duration:\nUnder 1 minute";
        string DESC_CORE_DIR_FILES = "Run checks on important files and directories required by Test Runner. The duration of the test may vary depending on the size of the directory.\n\nEstimated Duration:\n2 minutes to 5 minutes";
        string DESC_INSTALLED_BROWSER = "Run checks on installed browsers and identify compatibility with installed webdrivers.\n\nEstimated Duration:\nUnder 1 minute";
        string DESC_SHARED_FOLDER = "Run checks on shared folder settings to allow correct email links.\n\nEstimated Duration:\nUnder 1 minute";

        string DESC_TESTSCRIPT = "Run checks for test script files that have malformed XML structure. Duration may vary depending on the number of files.";
        string DESC_TESTSCRIPT_RESULTS = "Run checks for test script result files that have malformed XML structure. Duration may vary depending on the number of files.";
        string DESC_TESTSUITE = "Run checks for test suite files that have malformed XML structure, missing environments, and incorrect test file paths. Duration may vary depending on the number of files.";
        string DESC_TESTSUITE_RESULTS = "Run checks for test suite result files that have malformed XML structure. Duration may vary depending on the number of files";


        readonly string mTRPath;
        public DiagnosticTest()
        {
            InitializeComponent();
            InitializeTestProperty();
            InitializeDirOptionProperty();
            CheckBoxDirOptionEnable(false);

            mTRPath = GetTestRunnerPath();
            tbToolPath.Text = mTRPath;
        }

        private string SetDirOptions(List<DiagnosticTestProgress> DirOptions)
        {
            string _dir = "";
            int index = 1;
            for(int i = 0; i < DirOptions.Count(); i++)
            {
                if(!DirOptions.Where(x => x.DirOption.Equals(dirOptions[i])).Select(x => x.IsSelected).SingleOrDefault())
                {
                    _dir = _dir + index.ToString();
                }
                index++;
            }

            if(string.IsNullOrEmpty(_dir))
            {
                _dir = "0";
            }

            return _dir;
        }

        private void InitializeDirOptionProperty()
        {
            DirOptions.Add(new DiagnosticTestProgress { IsSelected = false, DirOption = dirOptions[0] });
            DirOptions.Add(new DiagnosticTestProgress { IsSelected = false, DirOption = dirOptions[1] });
            DirOptions.Add(new DiagnosticTestProgress { IsSelected = false, DirOption = dirOptions[2] });
            DirOptions.Add(new DiagnosticTestProgress { IsSelected = false, DirOption = dirOptions[3] });
        }

        private void InitializeTestProperty()
        {
            Tests.Add(new DiagnosticTestProgress { IsSelected = false, TestName = testName[0] });
            Tests.Add(new DiagnosticTestProgress { IsSelected = false, TestName = testName[1] });
            Tests.Add(new DiagnosticTestProgress { IsSelected = false, TestName = testName[2] });
            Tests.Add(new DiagnosticTestProgress { IsSelected = false, TestName = testName[3] });
        }

        private void SetDirOptionProperty(bool isSelected, int testRec)
        {
            try
            {
                DirOptions.Where(x => x.DirOption.Equals(dirOptions[testRec]))
                    .First(x => x.IsSelected = isSelected);
            }
            catch { }
        }

        private void SetTestProperty(bool isSelected, int testRec)
        {
            try
            {
                Tests.Where(x => x.TestName.Equals(testName[testRec]))
                    .First(x => x.IsSelected = isSelected);
            }
            catch { }
        }

        private string GetTestRunnerPath()
        {            
            string trPath = "";

            //Uncomment lines 71 - 74 and 81 for debugging purposes only
            //#if DEBUG
            //            trPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")).
            //                                TrimEnd(System.IO.Path.DirectorySeparatorChar);
            //#else
                string trFolderPath = System.IO.Path.Combine(DlkEnvironment.mDirTools, "TestRunner");
                if (Directory.Exists(trFolderPath))
                {
                    if (File.Exists(System.IO.Path.Combine(trFolderPath, "TestRunner.exe")))
                        trPath = trFolderPath;
                }
            //#endif

            return trPath;
        }

        private void EnableStartButton()
        {
            if(Tests.Any(x => x.IsSelected.Equals(true)))
            {
                btnStart.IsEnabled = true;
            }
            else
            {
                btnStart.IsEnabled = false;
            }
        }

        private void CheckBoxDirOptionEnable(bool flag)
        {
            cbxTestScript.IsEnabled = flag;
            cbxTestScriptResult.IsEnabled = flag;
            cbxTestSuite.IsEnabled = flag;
            cbxTestSuiteResult.IsEnabled = flag;
        }

        private void CheckBoxEnable(bool flag)
        {
            cbxMinReq.IsEnabled = flag;
            cbxCoreDir.IsEnabled = flag;
            cbxInsBrowser.IsEnabled = flag;
            cbxSharedFolder.IsEnabled = flag;
        }

        private void CheckFullDiagnostic()
        {
            if(cbxMinReq.IsChecked == true
                && cbxCoreDir.IsChecked == true
                && cbxInsBrowser.IsChecked == true
                && cbxSharedFolder.IsChecked == true)
            {
                cbxFull.IsChecked = true;
            }
            else
            {
                cbxFull.IsChecked = false;
            }
        }

        private void DescriptionView()
        {
            if (cbxMinReq.IsChecked == false
                && cbxCoreDir.IsChecked == false
                && cbxInsBrowser.IsChecked == false
                && cbxSharedFolder.IsChecked == false)
            {
                txtDesc.Text = string.Empty;
            }
            else if (cbxFull.IsChecked == true)
            {
                txtDesc.Text = DESC_FULL_DIAGNOSTIC;
            }
        }

        private void cbxFull_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_FULL_DIAGNOSTIC;

            if (cbxFull.IsChecked ?? false)
            {
                cbxFull.IsChecked = true;
                cbxMinReq.IsChecked = true;
                cbxCoreDir.IsChecked = true;
                cbxInsBrowser.IsChecked = true;
                cbxSharedFolder.IsChecked = true;

                SetTestProperty(true, 0);
                SetTestProperty(true, 1);
                SetTestProperty(true, 2);
                SetTestProperty(true, 3);

                cbxTestScript.IsChecked = true;
                cbxTestScriptResult.IsChecked = true;
                cbxTestSuite.IsChecked = true;
                cbxTestSuiteResult.IsChecked = true;

                SetDirOptionProperty(true, 0);
                SetDirOptionProperty(true, 1);
                SetDirOptionProperty(true, 2);
                SetDirOptionProperty(true, 3);

                CheckBoxEnable(false);
                CheckBoxDirOptionEnable(true);
            }
            else
            {
                cbxFull.IsChecked = false;
                cbxMinReq.IsChecked = false;
                cbxCoreDir.IsChecked = false;
                cbxInsBrowser.IsChecked = false;
                cbxSharedFolder.IsChecked = false;

                SetTestProperty(false, 0);
                SetTestProperty(false, 1);
                SetTestProperty(false, 2);
                SetTestProperty(false, 3);

                cbxTestScript.IsChecked = false;
                cbxTestScriptResult.IsChecked = false;
                cbxTestSuite.IsChecked = false;
                cbxTestSuiteResult.IsChecked = false;

                SetDirOptionProperty(false, 0);
                SetDirOptionProperty(false, 1);
                SetDirOptionProperty(false, 2);
                SetDirOptionProperty(false, 3);

                CheckBoxEnable(true);
                CheckBoxDirOptionEnable(false);
            }
            EnableStartButton();
            DescriptionView();
        }

        private void cbxMinReq_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_MIN_REQUIREMENT;

            if (cbxMinReq.IsChecked ?? false)
            {
                cbxMinReq.IsChecked = true;
                SetTestProperty(true, 0);
            }
            else
            {
                cbxMinReq.IsChecked = false;
                SetTestProperty(false, 0);
            }
            EnableStartButton();
            CheckFullDiagnostic();
            DescriptionView();
        }

        private void cbxCoreDir_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_CORE_DIR_FILES;

            if (cbxCoreDir.IsChecked ?? false)
            {
                cbxCoreDir.IsChecked = true;
                SetTestProperty(true, 1);

                cbxTestScript.IsChecked = true;
                cbxTestScriptResult.IsChecked = true;
                cbxTestSuite.IsChecked = true;
                cbxTestSuiteResult.IsChecked = true;

                SetDirOptionProperty(true, 0);
                SetDirOptionProperty(true, 1);
                SetDirOptionProperty(true, 2);
                SetDirOptionProperty(true, 3);

                CheckBoxDirOptionEnable(true);
            }
            else
            {
                cbxCoreDir.IsChecked = false;
                SetTestProperty(false, 1);

                cbxTestScript.IsChecked = false;
                cbxTestScriptResult.IsChecked = false;
                cbxTestSuite.IsChecked = false;
                cbxTestSuiteResult.IsChecked = false;

                SetDirOptionProperty(false, 0);
                SetDirOptionProperty(false, 1);
                SetDirOptionProperty(false, 2);
                SetDirOptionProperty(false, 3);

                CheckBoxDirOptionEnable(false);
            }
            EnableStartButton();
            CheckFullDiagnostic();
            DescriptionView();
        }

        private void cbxInsBrowser_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_INSTALLED_BROWSER;

            if (cbxInsBrowser.IsChecked ?? false)
            {
                cbxInsBrowser.IsChecked = true;
                SetTestProperty(true, 2);
            }
            else
            {
                cbxInsBrowser.IsChecked = false;
                SetTestProperty(false, 2);
            }
            EnableStartButton();
            CheckFullDiagnostic();
            DescriptionView();
        }

        private void cbxSharedFolder_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_SHARED_FOLDER;

            if (cbxSharedFolder.IsChecked ?? false)
            {
                cbxSharedFolder.IsChecked = true;
                SetTestProperty(true, 3);
            }
            else
            {
                cbxSharedFolder.IsChecked = false;
                SetTestProperty(false, 3);
            }
            EnableStartButton();
            CheckFullDiagnostic();
            DescriptionView();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            DiagnosticTestProgress dgTestProg = new DiagnosticTestProgress(Tests, SetDirOptions(DirOptions), mTRPath) { Owner = this };
            dgTestProg.ShowDialog();
        }

        private void cbxTestScript_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_TESTSCRIPT;

            if(cbxTestScript.IsChecked ?? false)
            {
                cbxTestScript.IsChecked = true;
                SetDirOptionProperty(true, 0);
            }
            else
            {
                cbxTestScript.IsChecked = false;
                SetDirOptionProperty(false, 0);
            }
            EnableStartButton();
            CheckFullDiagnostic();
        }

        private void cbxTestScriptResult_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_TESTSCRIPT_RESULTS;

            if (cbxTestScriptResult.IsChecked ?? false)
            {
                cbxTestScriptResult.IsChecked = true;
                SetDirOptionProperty(true, 1);
            }
            else
            {
                cbxTestScriptResult.IsChecked = false;
                SetDirOptionProperty(false, 1);
            }
            EnableStartButton();
            CheckFullDiagnostic();
        }

        private void cbxTestSuite_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_TESTSUITE;

            if (cbxTestSuite.IsChecked ?? false)
            {
                cbxTestSuite.IsChecked = true;
                SetDirOptionProperty(true, 2);
            }
            else
            {
                cbxTestSuite.IsChecked = false;
                SetDirOptionProperty(false, 2);
            }
            EnableStartButton();
            CheckFullDiagnostic();
        }

        private void cbxTestSuiteResult_Click(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = DESC_TESTSUITE_RESULTS;

            if (cbxTestSuiteResult.IsChecked ?? false)
            {
                cbxTestSuiteResult.IsChecked = true;
                SetDirOptionProperty(true, 3);
            }
            else
            {
                cbxTestSuiteResult.IsChecked = false;
                SetDirOptionProperty(false, 3);
            }
            EnableStartButton();
            CheckFullDiagnostic();
        }
    }
}
