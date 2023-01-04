using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using TestRunner.Common;
using TestRunner.Designer.View;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;

namespace CPExtensibility.View
{
    public partial class ProductSelection : Window, IProductSelectionView
    {
        #region PRIVATE MEMBERS
        private bool mContinue = false;
        private ProductSelectionPresenter mPresenter = null;
        private List<DlkTargetApplication> mAllProducts = new List<DlkTargetApplication>();
        private DlkTargetApplication mTarget = new DlkTargetApplication();
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
                // Initializes the entire DlkEnvironment based on the selected product
                this.mPresenter.CommitSelectedProduct();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.DialogResult = mContinue;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboTargetApplication_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedProduct = cboTargetApplication.SelectedItem as DlkTargetApplication;
        }
        #endregion
    }
}
