using System;
using System.Collections.Generic;
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
using CommonLib.DlkSystem;
using TestRunner.Common;
using System.IO;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ImportDestination.xaml
    /// </summary>
    public partial class ImportDestination : Window
    {
        #region PRIVATE MEMBERS
        private string mCurrentPath;
        private string mImportDestinationPath;
        private List<BFFolder> m_TestFolders = new List<BFFolder>();
        private List<BFFolder> m_SuiteFolders = new List<BFFolder>();
        private DlkTestSuiteLoader m_Loader = new DlkTestSuiteLoader();
        private bool m_IsOpen = false;
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CurrentPath"></param>
        public ImportDestination(string CurrentPath)
        {
            InitializeComponent();
            mCurrentPath = CurrentPath;
            mImportDestinationPath = string.Empty;
            if (CurrentPath == DlkEnvironment.mDirTestSuite)
            {
                tvwFoldersExisting.ItemsSource = TestSuiteFolders;
            }
            else
            {
                tvwFoldersExisting.ItemsSource = TestFolders;
            }
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Contains all folders and subfolders to be displayed in the screen
        /// </summary>
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

        /// <summary>
        /// Contains all folders and subfolders to be displayed in the screen
        /// </summary>
        public List<BFFolder> TestSuiteFolders
        {
            get
            {
                m_SuiteFolders.Clear();
                DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTestSuite);
                BFFolder root = new BFFolder
                {
                    Name = di.Name,
                    Path = di.FullName,
                    DirItems = m_Loader.GetSuiteDirectories(DlkEnvironment.mDirTestSuite, mCurrentPath),
                    IsSelected = m_IsOpen || string.IsNullOrEmpty(mCurrentPath)
                        || (System.IO.Path.GetDirectoryName(mCurrentPath)) == System.IO.Path.GetDirectoryName(di.FullName),
                    IsExpanded = true
                };
                m_SuiteFolders.Add(root);
                return m_SuiteFolders;
            }
        }

        public string ImportDestinationPath
        {
            get
            {
                return mImportDestinationPath;
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Sets the IsSelected property of each Folder
        /// </summary>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private bool SetIsSelected(BFFolder Folder)
        {
            if (System.IO.Path.Equals(Folder.Path, mCurrentPath))
            {
                Folder.IsSelected = true;
                return true;
            }
            else
            {
                foreach (BFFolder item in Folder.DirItems)
                {
                    if (System.IO.Path.Equals(item.Path, mCurrentPath))
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
        #endregion

        #region EVENT HANDLERS
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

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mImportDestinationPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path + "\\";
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
                mCurrentPath = ((BFFolder)tvwFoldersExisting.SelectedItem).Path;
                btnImport.IsEnabled = tvwFoldersExisting.SelectedItem == null ? false : true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
