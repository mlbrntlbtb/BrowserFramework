using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Xml.Linq;
using System.ComponentModel;
using CommonLib.DlkRecords;
using METHConverter.Templates;
using METHConverter.Utilities;
using System.Collections;
using System.Configuration;
using System.Text.RegularExpressions;

namespace METHConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string TestScriptPath;
        private static string SelectedProduct;
        private readonly BackgroundWorker bgWorker = new BackgroundWorker();
        private List<string> files = new List<string>();

        private static string CustomDestinationPath;
        private static bool IncludeSubDirectories;
        private static string SourceRoot;
        private static bool IsTestScript;

        public static int failCount;
        public static Logger RunLogs = new Logger();

        public MainWindow()
        {
            InitializeComponent();
            rBtnTestScript.IsChecked = true;

            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SelectedProduct"]))
            {

            }
            else
            {
                SelectedProduct = ConfigurationManager.AppSettings["SelectedProduct"];
            }

            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.WorkerReportsProgress = true;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.Button).ContextMenu.IsEnabled = true;
            (sender as System.Windows.Controls.Button).ContextMenu.PlacementTarget = (sender as System.Windows.Controls.Button);
            (sender as System.Windows.Controls.Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as System.Windows.Controls.Button).ContextMenu.IsOpen = true;
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtPath.Text)) System.Windows.MessageBox.Show("Please enter a folder/file to convert", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                TestScriptPath = txtPath.Text;
                UpdateButtonState(false);
                IncludeSubDirectories = chkIncludeSubs.IsChecked.Value;
                IsTestScript = rBtnTestScript.IsChecked.Value;

                failCount = 0;
                //Check whether directory or path
                FileAttributes attrib = File.GetAttributes(TestScriptPath);
                bool isDirectory = attrib.HasFlag(FileAttributes.Directory);
                files = new List<string>();

                RunLogs.Logs.Clear();


                if (!String.IsNullOrEmpty(txtDestinationPath.Text))
                {
                    if (Directory.Exists(txtDestinationPath.Text))
                        CustomDestinationPath = txtDestinationPath.Text;
                    else
                        throw new Exception("Please choose a valid destination directory.");
                }

                if (isDirectory)
                {
                    if (!Directory.Exists(TestScriptPath))
                    {
                        System.Windows.MessageBox.Show("Unable to find directory: " + TestScriptPath);
                        return;
                    }
                    else
                    {
                        if (IncludeSubDirectories)
                        {
                            files = Directory.GetFiles(TestScriptPath, "*.xml", SearchOption.AllDirectories).ToList();
                        }
                        else
                        {
                            files = Directory.GetFiles(TestScriptPath, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                        }
                    }
                    SourceRoot = txtPath.Text;
                }
                else
                {
                    if (!File.Exists(TestScriptPath))
                    {
                        System.Windows.MessageBox.Show("Unable to find file: " + TestScriptPath);
                        return;
                    }
                    else
                    {
                        files.Add(TestScriptPath);
                    }
                }

                if (isDirectory)
                {
                    RunLogs.WriteLine("Using diretory: " + TestScriptPath, Logger.MessageType.INF);
                    RunLogs.WriteLine("Total Files: " + files.Count.ToString());
                }
                bgWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    RunLogs.WriteLine(ex.InnerException.Message, Logger.MessageType.ERR);
                }
                RunLogs.WriteLine(ex.Message, Logger.MessageType.ERR);
                RunLogs.WriteToFile();
                txtProgress.Text = ex.Message;
                UpdateButtonState(true);
            }

        }

        private void UpdateButtonState(bool IsFinishedLoading)
        {
            btnBrowse.IsEnabled = IsFinishedLoading;
            btnConvert.IsEnabled = IsFinishedLoading;
            chkIncludeSubs.IsEnabled = IsFinishedLoading;
            txtPath.IsEnabled = IsFinishedLoading;
            txtDestinationPath.IsEnabled = IsFinishedLoading;
            //rBtnFile.IsEnabled = IsFinishedLoading;
            //rBtnDirectory.IsEnabled = IsFinishedLoading;
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int idx = 0;
            int fileCount = files.Count;

            string objectStorePath = GetObjectStoreFolder();
            foreach (string fileName in files)
            {
                try
                {
                    RunLogs.WriteLine("Using file: " + fileName, Logger.MessageType.INF);
                    string res = String.Empty;
                    if (IsTestScript)
                    {
                        string testName = string.Empty;
                        List<DlkTestStepRecord> testSteps = new List<DlkTestStepRecord>();
                        testSteps = GetScriptSteps(fileName, out testName);

                        CPTemplate mTemplate = new CPTemplate();
                        mTemplate.Session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
                        mTemplate.Session["TestName"] = testName.Trim().Replace(" ", "_");
                        mTemplate.Session["TestSteps"] = testSteps.Where(x => x.mExecute.Equals("true", StringComparison.InvariantCultureIgnoreCase)).ToList();
                        mTemplate.Session["ObjectStorePath"] = objectStorePath;
                        mTemplate.Session["MainWindow"] = this;
                        mTemplate.Initialize();

                        res = mTemplate.TransformText();
                    }
                    else
                    {
                        string testrunname = string.Empty;
                        List<TestInfo> tests = new List<TestInfo>();
                        tests = GetSuiteScripts(fileName, out testrunname);

                        TestRunTemplate mTemplate = new TestRunTemplate();
                        mTemplate.Session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
                        mTemplate.Session["TestRunName"] = testrunname.Trim();
                        mTemplate.Session["Tests"] = tests.Where(x => x.execute.Equals("true", StringComparison.InvariantCultureIgnoreCase)).ToList();
                        mTemplate.Session["MainWindow"] = this;
                        mTemplate.Initialize();

                        res = mTemplate.TransformText();
                    }

                    string mFilePath = string.Empty;

                    if (!String.IsNullOrEmpty(CustomDestinationPath))
                    {
                        if (IncludeSubDirectories)
                        {
                            if (IsInASubdirectory(SourceRoot, fileName))
                            {
                                string subDir = Directory.GetParent(fileName).FullName.Remove(0, SourceRoot.Length + 1);
                                subDir = Path.Combine(CustomDestinationPath, subDir);
                                if (!Directory.Exists(subDir))
                                    Directory.CreateDirectory(subDir);

                                mFilePath = Path.Combine(CustomDestinationPath, subDir, Path.GetFileNameWithoutExtension(fileName));
                            }
                            else
                            {
                                mFilePath = Path.Combine(CustomDestinationPath, Path.GetFileNameWithoutExtension(fileName));
                            }
                        }
                        else
                        {
                            mFilePath = Path.Combine(CustomDestinationPath, Path.GetFileNameWithoutExtension(fileName));
                        }
                    }
                    else
                    {
                        mFilePath = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
                    }
                    File.WriteAllText(mFilePath + ".cs", res);
                }
                catch (Exception ex)
                {
                    failCount++;
                    RunLogs.WriteLine(fileName + " failed :" + ex.Message, Logger.MessageType.ERR);
                }

                idx++;
                bgWorker.ReportProgress(idx, fileName);
            }
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultView resultView = new ResultView(failCount, files.Count, RunLogs.Logs);
            resultView.ShowDialog();
            UpdateButtonState(true);

        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                string[] log = RunLogs.Logs.Last().Split(new char[] { '|' }, 2);
                //double actualPercent = ((Double)e.ProgressPercentage / (Double)files.Count) * 100;
                //prgBar.Value = actualPercent;
                txtLogType.Text = log[0].Remove(5);
                txtCurrFile.Text = log[1];
                txtProgress.Text = String.Format("{0} / {1}", e.ProgressPercentage, files.Count);
            }
            catch (Exception ex)
            {
                RunLogs.WriteLine(ex.Message, Logger.MessageType.ERR);
            }
        }

        private void menuOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Browse Test Script";
            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.xml)|*.xml";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = dialog.FileName;
                txtPath.ScrollToEnd();
            }
        }

        private void menuOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            dialog.Description = "Select folder of tests to convert:";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = dialog.SelectedPath;
                txtPath.ScrollToEnd();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                if (System.Windows.MessageBox.Show("There are scripts being converted.\nAre you sure you want to exit?", "Conversion in progress", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    bgWorker.CancelAsync();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private List<DlkTestStepRecord> GetScriptSteps(string FileName, out string testName)
        {
            List<DlkTestStepRecord> stepRecords = new List<DlkTestStepRecord>();

            XDocument xDoc = XDocument.Load(FileName);
            testName = xDoc.Descendants("test").First().Element("name").Value;

            var data = from doc in xDoc.Descendants("step")
                       select new
                       {
                           execute = doc.Element("execute").Value,
                           screen = doc.Element("screen").Value,
                           control = doc.Element("control").Value,
                           keyword = doc.Element("keyword").Value,
                           parameters = doc.Element("parameters").Descendants().Select(x => x.Value),
                           delay = doc.Element("delay").Value
                       };

            foreach (var step in data)
            {
                DlkTestStepRecord newStep = new DlkTestStepRecord();
                newStep.mExecute = step.execute;
                newStep.mScreen = step.screen;
                newStep.mControl = step.control;
                newStep.mKeyword = step.keyword;
                newStep.mParameters = step.parameters.ToList<string>();
                newStep.mStepDelay = Convert.ToInt32(step.delay);
                stepRecords.Add(newStep);
            }

            DlkData mData = new DlkData(Path.Combine(Path.GetDirectoryName(FileName), Path.GetFileNameWithoutExtension(FileName) + ".trd"));
            if (StepUsesDataVariable(stepRecords))
            {
                SubstituteDataVariables(stepRecords, mData);
            }
            SubstituteOutputVariable(stepRecords);
            return stepRecords;
        }

        private List<TestInfo> GetSuiteScripts(string FileName, out string testRunName)
        {
            List<TestInfo> suiteRecords = new List<TestInfo>();

            XDocument xDoc = XDocument.Load(FileName);
            testRunName = Path.GetFileNameWithoutExtension(FileName);
            testRunName = IsNumeric(testRunName.Substring(0,1)) ? "CP" + testRunName : testRunName;

            var data = from doc in xDoc.Descendants("test")
                       select new
                       {
                           identifier = doc.Attribute("identifier").Value,
                           row = doc.Attribute("row").Value,
                           folder = doc.Attribute("folder").Value,
                           name = doc.Attribute("name").Value,
                           description = doc.Attribute("description").Value,
                           keepopen = doc.Attribute("keepopen") == null ? "false" : doc.Attribute("keepopen").Value,
                           file = doc.Attribute("file").Value,
                           testid = doc.Attribute("testid").Value,
                           environment = doc.Attribute("environment").Value,
                           browser = doc.Attribute("browser").Value,
                           teststatus = doc.Attribute("teststatus").Value,
                           duration = doc.Attribute("duration").Value,
                           executiondate = doc.Attribute("executiondate").Value,
                           assigned = doc.Attribute("assigned") == null ? "" : doc.Attribute("assigned").Value,
                           execute = doc.Attribute("execute") == null ? "True" : doc.Attribute("execute").Value,
                           dependent = doc.Attribute("dependent") == null ? "False" : doc.Attribute("dependent").Value,
                           executedependencyresult = doc.Attribute("executedependencyresult") == null ? "" : doc.Attribute("executedependencyresult").Value,
                           executedependencytestrow = doc.Attribute("executedependencytestrow") == null ? "" : doc.Attribute("executedependencytestrow").Value,
                           totalsteps = doc.Attribute("totalsteps") == null ? "--" : doc.Attribute("totalsteps").Value
                       };

            foreach (var val in data)
            {
                TestInfo newScript = new TestInfo();
                newScript.name = val.name;
                newScript.browser = val.browser;
                newScript.environment = val.environment;
                newScript.execute = val.execute;
                suiteRecords.Add(newScript);
            }

            return suiteRecords;
        }

        private static void SubstituteDataVariables(List<DlkTestStepRecord> Steps, DlkData Data, int TargetInstance = 0)
        {
            try
            {
                if (TargetInstance < 0)
                {
                    throw new Exception("Test instance for execution cannot be less than 0.");
                }
                foreach (DlkTestStepRecord step in Steps)
                {
                    step.mParameters[TargetInstance] = GetDataValue(step.mParameters[TargetInstance], Data);
                }
            }
            catch
            {
                throw;
            }
        }

        private static bool StepUsesDataVariable(List<DlkTestStepRecord> Steps)
        {
            foreach (DlkTestStepRecord step in Steps)
            {
                foreach (string param in step.mParameters)
                {
                    if (param.Contains("D{"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void SubstituteOutputVariable(List<DlkTestStepRecord> Steps)
        {
            foreach (DlkTestStepRecord step in Steps)
            {
                string mVar = null;
                int index = 0;
                foreach (string param in step.mParameters)
                {
                    if (param.Contains("O{"))
                    {
                        mVar = param.Replace("O{", "").Replace("}", "");
                        index = step.mParameters.IndexOf(param);
                    }
                }
                if (mVar != null)
                {
                    step.mParameters[index] = mVar;
                }
            }
        }

        private static string GetDataValue(string OldValue, DlkData Data, int instance = 0, bool isParam = false)
        {
            try
            {
                string delimiter = isParam ? DlkTestStepRecord.globalParamDelimiter : "|";
                string ret = "";
                foreach (string param in OldValue.Split(new[] { delimiter }, StringSplitOptions.None))
                {
                    string newValue = param;
                    if (param.Contains("D{"))
                    {
                        foreach (DlkDataRecord rec in Data.Records)
                        {
                            if (rec.Name == param.Replace("D{", "").Replace("}", ""))
                            {
                                newValue = rec.Values[instance];
                                break;
                            }
                        }
                    }
                    ret += newValue + delimiter;
                }
                ret = ret.Substring(0, ret.Length - delimiter.Length);
                return ret;
            }
            catch
            {
                throw;
            }
        }

        private static string GetObjectStoreFolder()
        {
            string result = string.Empty;
            string currDir = Directory.GetCurrentDirectory();
            //string sProduct = ConfigurationManager.AppSettings[SelectedProduct];
            string sProduct = "Costpoint_711";

            if (currDir.Contains("\\Code\\"))
            {
                result = currDir.Remove(currDir.IndexOf("\\Code"));
            }
            else if (currDir.Contains("\\Tools\\"))
            {
                result = currDir.Remove(currDir.IndexOf("\\Tools"));
            }
            result = Path.Combine(result, "Products");
            result = Path.Combine(result, sProduct);

            if (!Directory.Exists(result))
            {
                throw new Exception("Objectstore folder not found.");
            }
            return result;
        }

        private static bool IsInASubdirectory(string Root, string file)
        {
            if (Root == Path.GetDirectoryName(file))
            {
                return false;
            }
            return true;
        }

        private static bool IsNumeric (string Text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(Text);
        }
                
    }
}
