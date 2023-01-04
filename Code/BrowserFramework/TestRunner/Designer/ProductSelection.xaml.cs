using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using TestRunner.Common;
using TestRunner.Designer.View;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;

namespace TestRunner.Designer
{
    public partial class ProductSelection : Window, IProductSelectionView
    {
        #region PRIVATE MEMBERS
        private string mRootPath = string.Empty;
        //private bool mContinue = false;
        private ProductSelectionPresenter mPresenter = null;
        private List<DlkTargetApplication> mAllProducts = new List<DlkTargetApplication>();
        private DlkTargetApplication mTarget = new DlkTargetApplication();
        private string mDirectoryFilter = string.Empty;
        private string mDirectoryFilterXML = string.Empty;
        #endregion

        #region PUBLIC MEMBERS
        /// <summary>
        /// All products available for selection
        /// </summary>
        public List<DlkTargetApplication> AvailableProducts
        {
            get
            {
                return mAllProducts;
            }
            set
            {
                mAllProducts = value;
            }
        }

        /// <summary>
        /// User selection from list
        /// </summary>
        public DlkTargetApplication SelectedProduct
        {
            get
            {
                return mTarget;
            }
            set
            {
                mTarget = value;
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        public ProductSelection()
        {
            InitializeComponent();
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Inititialize critical class resources and data bindings
        /// </summary>
        private void Initialize()
        {
            mPresenter = new ProductSelectionPresenter(this);
            mPresenter.LoadAvailable();
            cboTargetApplication.ItemsSource = AvailableProducts;
        }
        #endregion

        #region EVENT HANDLERS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Initialize();
                this.Activate();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mPresenter.CommitSelectedProduct();
                mPresenter.DirectoryFilter = txtSuiteDirectory.Text;
                mPresenter.SaveDirectory(txtSuiteDirectory.Text, mDirectoryFilterXML);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboTargetApplication_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedProduct = cboTargetApplication.SelectedItem as DlkTargetApplication;
            mDirectoryFilter = mPresenter.mRootPath + "\\Products\\" + SelectedProduct.ProductFolder + "\\Suites";
            mDirectoryFilterXML = mPresenter.mRootPath + "\\Products\\" + SelectedProduct.ProductFolder + 
                                    "\\Framework\\Library\\Directory\\Directory.xml";
            
            if (File.Exists(mDirectoryFilterXML))
            {
                XDocument doc = XDocument.Load(mDirectoryFilterXML);
                var query = doc.Descendants("directory").Select(s => new
                {
                    PATH = s.Element("path").Value
                }).FirstOrDefault();
                mDirectoryFilter = query.PATH;
                txtSuiteDirectory.Text = mDirectoryFilter;
            }
            else
            {
                txtSuiteDirectory.Text = mDirectoryFilter;
            }
        }

        private void btnbrowse_Click(object sender, RoutedEventArgs e)
        {
            var browseDialog = new FolderBrowserDialog();
            browseDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            browseDialog.SelectedPath = mDirectoryFilter;
            browseDialog.Description = "Select Suite Directory";

            if (browseDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSuiteDirectory.Text = browseDialog.SelectedPath;
            }
        }

        #endregion
    }
}
