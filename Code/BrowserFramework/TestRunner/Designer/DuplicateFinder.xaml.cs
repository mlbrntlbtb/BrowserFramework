using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using TestRunner.Common;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using TestRunner.Designer.View;

namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for DuplicateFinder.xaml
    /// </summary>
    public partial class DuplicateFinder : Window, IDuplicateView, INotifyPropertyChanged
    {
        #region PRIVATE MEMBERS
        //private BFFolder mSuites;
        //private KwFolder mTests;
        private List<string> mScreens = new List<String>();
        private List<string> mControls = new List<String>();
        private List<string> mKeywords = new List<String>();
        private List<Match> mFinderMatches;
        private List<DlkTest> mLoadedTests;
        private string mCurrentControl;
        private string mCurrentKeyword;
        private string mCurrentParameter;
        private string mCurrentScreen;
        private List<DlkTestStepRecord> mFinderTest = new List<DlkTestStepRecord>();
        private bool mIncludeParameters;
        //private bool mIncludeParametersCanceled = false;
        private ObservableCollection<ExcludedStepRecord> mExcludedSteps = new ObservableCollection<ExcludedStepRecord>();
        //private bool mIncludeDupParameters;
        private bool mIncludeDupParametersCanceled = false;
        private List<Duplicate> mDuplicateTests;
        public List<DlkObjectStoreFileRecord> ObjectStoreFiles;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool SelectionHandled = false;
        private DuplicatePresenter mDuplicatePresenter { get; set; }
        private List<KwDirItem> SelectedNodes;
        #endregion

        #region PUBLIC MEMBERS
        public List<DlkObjectStoreFileRecord> mObjectStoreFiles { get; set; }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            mDuplicatePresenter = new DuplicatePresenter(this);
            tbxFolderPath.Text = string.Join("\n", SelectedNodes.Select(x => x.Path).ToArray());

            /* Initialize Data Bindings */
            dgExcludedSteps.ItemsSource = ExcludedSteps;
            dgDuplicates.DataContext = DuplicateTests;
            
            /* Clear Variables */
            DuplicateTests = new List<Duplicate>();
            ExcludedSteps = new ObservableCollection<ExcludedStepRecord>();
        }
        #endregion

        #region IDuplicateView
        /// <summary>
        /// Update status of UI
        /// </summary>
        /// <param name="Status"></param>
        public void UpdateViewStatus(object Status)
        {

        }
        public List<string> Screens
        {
            get
            {
                return mScreens;
            }
            set
            {
                mScreens = value;
            }
        }

        public List<string> Controls
        {
            get
            {
                return mControls;
            }
            set
            {
                mControls = value;
            }
        }

        public List<string> Keywords
        {
            get
            {
                return mKeywords;
            }
            set
            {
                mKeywords = value;

            }
        }

        public List<Match> FinderMatches
        {
            get
            {
                return mFinderMatches;
            }
            set
            {
                mFinderMatches = value;
            }
        }


        public List<DlkTest> LoadedTests
        {
            get
            {
                return mLoadedTests;
            }
            set
            {
                mLoadedTests = value;
            }
        }
        public string CurrentControl
        {
            get
            {
                return mCurrentControl;
            }
            set
            {
                mCurrentControl = value;
            }
        }

        public string CurrentKeyword
        {
            get
            {
                return mCurrentKeyword;
            }
            set
            {
                mCurrentKeyword = value;
            }
        }

        public string CurrentParameter
        {
            get
            {
                return mCurrentParameter;
            }
            set
            {
                mCurrentParameter = value;
            }
        }

        public string CurrentScreen
        {
            get
            {
                return mCurrentScreen;
            }
            set
            {
                mCurrentScreen = value;
            }
        }

        public List<DlkTestStepRecord> FinderTest
        {
            get
            {
                return mFinderTest;
            }

            set
            {
                mFinderTest = value;
            }
        }

        public bool IncludeParameters
        {
            get
            {
                return mIncludeParameters;
            }
            set
            {
                mIncludeParameters = value;
            }
        }

        public bool IncludeDupParameters
        {
            get
            {
                return mIncludeParameters;
            }
            set
            {
                mIncludeParameters = value;
            }
        }
        public ObservableCollection<ExcludedStepRecord> ExcludedSteps
        {
            get
            {
                if (mExcludedSteps == null)
                {
                    mExcludedSteps = new ObservableCollection<ExcludedStepRecord>();
                }
                return mExcludedSteps;
            }
            set
            {
                mExcludedSteps = value;
                dgExcludedSteps.ItemsSource = mExcludedSteps;
                dgExcludedSteps.Items.Refresh();
                OnPropertyChanged("ExcludedSteps");
            }
        }

        public List<Duplicate> DuplicateTests
        {
            get
            {
                return mDuplicateTests;
            }
            set
            {
                mDuplicateTests = value;
                dgDuplicates.ItemsSource = value;
                dgDuplicates.Items.Refresh();
            }
        }

        public Filter Filter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region CONSTRUCTOR
        public DuplicateFinder(List<KwDirItem> mSelectedNodes, List<DlkObjectStoreFileRecord> mOsfr, List<String> mAllScreens, List<DlkTest> mAllTests)
        {
            SelectedNodes = mSelectedNodes;
            ObjectStoreFiles = mOsfr;
            Screens = mAllScreens;
            LoadedTests = mAllTests;

            InitializeComponent();
            Initialize();
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name"></param>
        private void OnPropertyChanged(string Name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(Name));
            }
        }
        private static Parent FindParent<Parent>(DependencyObject child)
            where Parent : DependencyObject
        {
            DependencyObject parentObject = child;

            //We are not dealing with Visual, so either we need to fnd parent or
            //get Visual to get parent from Parent Heirarchy.
            while (!((parentObject is System.Windows.Media.Visual)
                    || (parentObject is System.Windows.Media.Media3D.Visual3D)))
            {
                if (parentObject is Parent || parentObject == null)
                {
                    return parentObject as Parent;
                }
                else
                {
                    parentObject = (parentObject as FrameworkContentElement).Parent;
                }
            }

            //We have not found parent yet , and we have now visual to work with.
            parentObject = VisualTreeHelper.GetParent(parentObject);

            //check if the parent matches the type we're looking for
            if (parentObject is Parent || parentObject == null)
            {
                return parentObject as Parent;
            }
            else
            {
                //use recursion to proceed with next level
                return FindParent<Parent>(parentObject);
            }
        }
        #endregion

        #region EVENT HANDLERS
        private void cbxScreenExclude_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SelectionHandled)
                {
                    SelectionHandled = false;
                    return;
                }
                var cbx = sender as ComboBox;
                var screen = cbx.SelectedItem.ToString();
                DataGridRow dgr = FindParent<DataGridRow>(cbx);
                int index = dgr.GetIndex();
                Controls = new List<String>();
                mDuplicatePresenter.LoadControls(screen);
                ExcludedSteps[index].Controls = Controls;
                mDuplicatePresenter.LoadKeywords(screen, string.Empty);
                ExcludedSteps[index].Keywords = Keywords;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void cbxControlExclude_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SelectionHandled)
                {
                    SelectionHandled = false;
                    return;
                }
                var cbx = sender as ComboBox;
                var control = cbx.SelectedItem.ToString();
                DataGridRow dgr = FindParent<DataGridRow>(cbx);
                int index = dgr.GetIndex();
                Keywords = new List<String>();
                var screen = ExcludedSteps[index].mScreen;
                mDuplicatePresenter.LoadKeywords(screen, control);
                ExcludedSteps[index].Keywords = Keywords;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void FileToFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBlock tbClicked = sender as TextBlock;
                Duplicate selectedTest = ((Hyperlink)e.OriginalSource).DataContext as Duplicate;
                TestDetails td = new TestDetails(selectedTest.TestToFind);
                td.Owner = this;
                td.Show();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void FileToCompare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Duplicate selectedTest = ((Hyperlink)e.OriginalSource).DataContext as Duplicate;
                TestDetails td = new TestDetails(selectedTest.TestDuplicate);
                td.Owner = this;
                td.Show();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void FolderToFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Duplicate target = ((FrameworkElement)sender).DataContext as Duplicate;
                mDuplicatePresenter.GoToFolder(target.TestToFind.Path);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void FolderToCompare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Duplicate target = ((FrameworkElement)sender).DataContext as Duplicate;
                mDuplicatePresenter.GoToFolder(target.TestDuplicate.Path);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void addExcludedStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mDuplicatePresenter.AddExcludedStep();   
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void deleteExcludedStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ExcludedStepRecord> esr = new List<ExcludedStepRecord>();
                foreach (ExcludedStepRecord item in dgExcludedSteps.SelectedItems)
                {
                    esr.Add(item);
                }
                mDuplicatePresenter.DeleteExcludedSteps(esr);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
        private void clearExcludedSteps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkUserMessages.ShowQuestionOkCancel(DlkUserMessages.ASK_CLEAR_EXSTEPS) == MessageBoxResult.Yes)
                {
                    mDuplicatePresenter.ClearExcludedSteps();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
      
        private void btnFindDuplicates_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] testPaths = SelectedNodes.Select(x => x.Path).ToArray();
                mDuplicatePresenter.FindDuplicates(testPaths);
                if (DuplicateTests.Count == 0)
                {
                    DlkUserMessages.ShowInfo(DlkUserMessages.INF_NO_MATCHES_FOUND);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkIncludeDupParameters_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIncludeDupParametersCanceled)
                {
                    mIncludeDupParametersCanceled = false;
                    return;
                }
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_CLEAR_FIND_RESULTS)
                    == MessageBoxResult.Yes)
                {
                    IncludeDupParameters = true;
                    DuplicateTests = new List<Duplicate>();
                }
                else
                {
                    mIncludeDupParametersCanceled = true;
                    chkIncludeDupParameters.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void chkIncludeDupParameters_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIncludeDupParametersCanceled)
                {
                    mIncludeDupParametersCanceled = false;
                    return;
                }
                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_CLEAR_FIND_RESULTS) 
                    == MessageBoxResult.Yes)
                {
                    IncludeDupParameters = false;
                    DuplicateTests = new List<Duplicate>();
                }
                else
                {
                    mIncludeDupParametersCanceled = true;
                    chkIncludeDupParameters.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
        /// <summary>
        /// Handler for click event of Close button
        /// </summary>
        /// <param name="sender">Sender objects</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
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
        /// Handler for loaded event of toolbar
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ToolBar toolBar = sender as ToolBar;

                var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
                if (mainPanelBorder != null)
                {
                    mainPanelBorder.Margin = new Thickness(0);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        private void btnDeleteExcludedStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ExcludedStepRecord> esr = new List<ExcludedStepRecord>();
                foreach (ExcludedStepRecord item in dgExcludedSteps.SelectedItems)
                {
                    esr.Add(item);
                }
                mDuplicatePresenter.DeleteExcludedSteps(esr);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearExcludedSteps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkUserMessages.ShowQuestionOkCancel(DlkUserMessages.ASK_CLEAR_EXSTEPS) == MessageBoxResult.Yes)
                {
                    mDuplicatePresenter.ClearExcludedSteps();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        private void btnAddExcludedStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mDuplicatePresenter.AddExcludedStep();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
