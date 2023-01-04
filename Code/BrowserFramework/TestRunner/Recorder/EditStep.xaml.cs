 using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using TestRunner.Common;
using Recorder.Presenter;
using Recorder.View;

namespace TestRunner.Recorder
{
    /// <summary>
    /// Interaction logic for EditStep.xaml
    /// </summary>
    public partial class EditStep : IEditStepView
    {
        #region PRIVATE MEMBERS
        private EditStepPresenter mPresenter;
        private bool mResult = false;
        #endregion

        #region PUBLIC MEMBERS
        public DlkTestStepRecord TargetStep { get; set; }
        public ObservableCollection<DlkKeywordParameterRecord> Parameters { get; set; }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="Owner">Owner window</param>
        /// <param name="Step">Step to edit</param>
        public EditStep(Window Owner, DlkTestStepRecord Step)
        {
            this.Owner = Owner;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            InitializeComponent();
            TargetStep = Step;
            Initialize();
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            mPresenter = new EditStepPresenter(this);
            mPresenter.GetParameterCollection();
            dgParameters.ItemsSource = Parameters;
            txtControl.DataContext = TargetStep;
            txtKeyword.DataContext = TargetStep;
            txtScreen.DataContext = TargetStep;
            this.Title = "Step " + TargetStep.mStepNumber;
        }

        /// <summary>
        /// UI Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        private static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
        #endregion

        #region EVENT HANDLERS
        private void EditCell(object sender, EventArgs e)
        {
            try
            {
                DataGridCell cell = sender as DataGridCell;
                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        {
                            cell.IsSelected = true;
                            dataGrid.BeginEdit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Ok button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgParameters.Focus();
                dgParameters.CommitEdit();
                dgParameters.CommitEdit(); // need to commit twice to save - for shortcut key
                dgParameters.Items.Refresh();
                mPresenter.SaveStep();
                mResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Cancel button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Window closing handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.DialogResult = mResult;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
