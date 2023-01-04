using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SaveSuiteDialog.xaml
    /// </summary>
    public partial class SaveSuiteDialog : Window
    {
        private DlkTestSuiteInfoRecord m_TestSuite = null;
        private bool m_IsNew = true;
        private string m_FilePath = string.Empty;
        private bool m_IsOpen = false;
        private List<DlkExecutionQueueRecord> m_Tests = null;

        private const int INDEX_TAB_NEW = 0;
        private const int INDEX_TAB_EXISTING = 1;


        public SaveSuiteDialog(string FilePath, DlkTestSuiteInfoRecord TestSuite, List<DlkExecutionQueueRecord> Tests, bool IsOpen=false)
        {
            InitializeComponent();
            m_IsNew = string.IsNullOrEmpty(FilePath);
            m_FilePath = FilePath;
            m_TestSuite = TestSuite;
            m_Tests = Tests;

            if (IsOpen)
            {
                m_IsOpen = IsOpen;
                this.Title = "Open";
                btnOK.Content = "Open";
                //tabControl1.SelectedValuePath = "Existing";
                tabControl1.Items.Clear();
                tabControl1.Items.Add(tabSuites);
                //tabItem1.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSuiteList();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LoadSuiteList()
        {
            List<SuiteFile> lstSuites = new List<SuiteFile>();

            //DirectoryInfo di = new DirectoryInfo(DlkEnvironment.DirTestSuite);
            DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTestSuite);

            SuiteFile selectedItem = null;

            foreach (FileInfo file in di.GetFiles("*.xml"))
            {
                DlkTestSuiteInfoRecord dirItem = DlkTestSuiteXmlHandler.GetTestSuiteInfo(file.FullName);
                SuiteFile sf = new SuiteFile();
                sf.Name = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                sf.Browser = dirItem.Browser;
                sf.EnvID = dirItem.EnvID;
                sf.Language = dirItem.Language;
                //sf.Tag = dirItem.Tag;
                sf.FullName = file.FullName;
                sf.Email = dirItem.Email;
                lstSuites.Add(sf);
                if (m_FilePath == file.FullName)
                {
                    selectedItem = sf;
                }
            }

            DataContext = lstSuites;

            if (m_IsNew)
            {
                tabControl1.SelectedIndex = INDEX_TAB_NEW;
            }
            else
            {
                tabControl1.SelectedIndex = INDEX_TAB_EXISTING;
                lvwSuites.SelectedItem = selectedItem;
                lvwSuites.ScrollIntoView(selectedItem);
            }
            UpdateSelectedSuiteInfo();
        }

        private void UpdateSelectedSuiteInfo()
        {
            //txtBrowserNew.Text = m_TestSuite.Browser;
            //txtEnvironmentNew.Text = m_TestSuite.EnvID;
            //txtLanguageNew.Text = m_TestSuite.Language;
            //txtPathNew.Text = "";
            //txtEmailNew.Text = m_TestSuite.Email;

            if (lvwSuites.SelectedItem != null)
            {
                SuiteFile selection = (SuiteFile)lvwSuites.SelectedItem;
                txtNameExt.Text = System.IO.Path.GetFileNameWithoutExtension(selection.Name);
                //txtBrowserExt.Text = selection.Browser;
                //txtEnvironmentExt.Text = selection.EnvID;
                //txtLanguageExt.Text = m_TestSuite.Language;
                txtPathExt.Text = selection.FullName;
                txtPathExt.ToolTip = selection.FullName;

                //txtEmailExt.Text = selection.Email;
                m_FilePath = selection.FullName;
            }
        }

        private bool SaveSuite()
        {
            bool checkIn = false;
            bool IsMapped = ((MainWindow)this.Owner).IsMapped;
            if (File.Exists(m_FilePath) && new FileInfo(m_FilePath).IsReadOnly)
            {
                if (!DlkSourceControlHandler.SourceControlEnabled || !IsMapped)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_CANNOT_SAVE_READ_ONLY);
                    return false;
                }

                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_SAVE_SOURCE_CONTROL_FILE, "Source Control") == MessageBoxResult.No)
                {
                    return false;
                }
                DlkSourceControlHandler.CheckOut(m_FilePath, "");
                checkIn = true;
            }
            else
            {
                if (DlkSourceControlHandler.SourceControlEnabled && IsMapped)
                {
                    if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_PUT_FILE_TO_SOURCE_CONTROL, "Source Control") == MessageBoxResult.Yes)
                    {
                        checkIn = true;
                    }
                }
            }
            DlkTestSuiteXmlHandler.Save(m_FilePath, m_TestSuite, m_Tests);
            if (checkIn)
            {
                DlkSourceControlHandler.Add(m_FilePath, "/comment:TestRunner");
                DlkSourceControlHandler.CheckIn(m_FilePath, "/comment:TestRunner");
            }
            ((MainWindow)this.Owner).LoadedSuite = m_FilePath;
            //MessageBox.Show("Filed saved: " + System.IO.Path.GetFileNameWithoutExtension(m_FilePath));
            return true;
        }

        private bool LoadSuite()
        {
            if (string.IsNullOrEmpty(m_FilePath))
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_FILE_TO_OPEN);
                return false;
            }
            ((MainWindow)this.Owner).LoadedSuite = m_FilePath;
            return true;
        }

        private void lvwSuites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateSelectedSuiteInfo();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void OnNameChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Path.Combine(DlkEnvironment.mDirTestSuite,
                System.IO.Path.GetFileNameWithoutExtension(txtNameNew.Text) + ".xml");
                txtPathNew.Text = path;
                txtPathNew.ToolTip = path;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private class SuiteFile
        {
            public string Name{get;set;}
            public string Browser { get; set; }
            public string EnvID { get; set; }
            public string Language { get; set; }
            public string Tag { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m_IsOpen)
                {
                    if (LoadSuite())
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    if (tabControl1.SelectedIndex == INDEX_TAB_NEW)
                    {
                        if (string.IsNullOrEmpty(txtNameNew.Text) || txtNameNew.Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
                        {
                            DlkUserMessages.ShowError(DlkUserMessages.ERR_FILENAME_INVALID);
                            return;
                        }
                        string fileNameToSave = txtNameNew.Text.Replace(".xml", "").Replace(".XML", "");
                        m_FilePath = System.IO.Path.Combine(DlkEnvironment.mDirTestSuite, fileNameToSave + ".xml");
                    }
                    else
                    {
                        if (lvwSuites.SelectedItems.Count == 0)
                        {
                            DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_FILE_TO_OVERWRITE);
                            return;
                        }
                        m_FilePath = System.IO.Path.Combine(DlkEnvironment.mDirTestSuite, txtNameExt.Text + ".xml");
                    }

                    if (SaveSuite())
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
