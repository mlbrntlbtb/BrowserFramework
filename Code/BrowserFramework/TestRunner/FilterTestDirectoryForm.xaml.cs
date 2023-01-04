using System;
using System.Collections.Generic;
using System.Windows;
using TestRunner.Common;
using CommonLib.DlkSystem;
using System.IO;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for FilterTestDirectoryForm.xaml
    /// </summary>
    public partial class FilterTestDirectoryForm : Window
    {
        public FilterTestDirectoryForm(string CurrentFilterPath)
        {
            InitializeComponent();
            m_CurrentFilterPath = CurrentFilterPath;
            m_SelectedTestPath = string.Empty;
            tvwFoldersExisting.ItemsSource = TestFolders;
        }

        private string m_CurrentFilterPath;
        private string m_SelectedTestPath;
        private List<BFFolder> m_TestFolders = new List<BFFolder>();
        private DlkTestSuiteLoader m_Loader = new DlkTestSuiteLoader();

        public List<BFFolder> TestFolders
        {
            get
            {
                m_TestFolders.Clear();
                DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTests);
                BFFolder root = new BFFolder
                {
                    Name = di.Name,
                    Path = di.FullName,
                    DirItems = m_Loader.GetSuiteDirectories(DlkEnvironment.mDirTests),
                    IsSelected = false
                };
                m_TestFolders.Add(root);
                SetIsSelected(root);
                return m_TestFolders;
            }
        }

        public string SelectedTestPath
        {
            get { return m_SelectedTestPath; }
        }

        private bool SetIsSelected(BFFolder Folder)
        {
            if (System.IO.Path.Equals(Folder.Path, m_CurrentFilterPath))
            {
                Folder.IsSelected = true;
                return true;
            }
            else
            {
                foreach (BFFolder item in Folder.DirItems)
                {
                    if (System.IO.Path.Equals(item.Path, m_CurrentFilterPath))
                    {
                        item.IsSelected = true;
                        return true;
                    }
                    else
                    {
                        if (SetIsSelected(item))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tvwFoldersExisting.Focus();
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
                m_SelectedTestPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvwFoldersExisting_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                m_CurrentFilterPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                txtCurrentPath.Text = m_CurrentFilterPath;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
