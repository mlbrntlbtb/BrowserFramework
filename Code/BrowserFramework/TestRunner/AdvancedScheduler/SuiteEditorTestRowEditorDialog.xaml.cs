using CommonLib.DlkRecords;
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
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for SuiteEditorTestRowEditorDialog.xaml
    /// </summary>
    public partial class SuiteEditorTestRowEditorDialog : Window
    {
        #region PRIVATE MEMBERS
        private List<DlkExecutionQueueRecord> _rowEditorSource;
        private List<DlkExecutionQueueRecord> _editList;
        private List<string> _executeList = new List<string> { "True", "False" };
        private List<string> _environmentList;
        private ListCollectionView _browserList;
        private DlkExecutionQueueRecord _record;
        private SuiteEditorForm mainWindow;
        #endregion

        #region CONSTRUCTOR
        public SuiteEditorTestRowEditorDialog(SuiteEditorForm Owner, List<DlkExecutionQueueRecord> editList, List<string> environmentList, ListCollectionView browserList)
        {
            InitializeComponent();
            this.Owner = Owner;
            mainWindow = Owner;
            _editList = editList;
            _environmentList = environmentList;
            _browserList = browserList;

            if (editList == null || editList.Count < 1)
                return;

            //Set default value of columns from first selected record
            var defaultExecuteValue = editList.FirstOrDefault().execute;
            var defaultEnvironmentValue = editList.FirstOrDefault().environment;
            var defaultBrowserValue = editList.FirstOrDefault().Browser.Name;
            var defaultKeepOpenValue = editList.FirstOrDefault().keepopen;

            _record = new DlkExecutionQueueRecord(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                defaultEnvironmentValue,
                defaultBrowserValue,
                defaultKeepOpenValue,
                string.Empty,
                string.Empty,
                defaultExecuteValue,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
                );

            _record.PropertyChanged += _record_PropertyChanged;
            _rowEditorSource = new List<DlkExecutionQueueRecord>() { _record };
            dgRowEditor.ItemsSource = _rowEditorSource;
        }
        #endregion

        #region PROPERTIES
        public List<string> EnvironmentList
        {
            get
            {
                return _environmentList;
            }
            set
            {
                _environmentList = value;
            }
        }

        public List<string> ExecuteList
        {
            get
            {
                return _executeList;
            }
            set
            {
                _executeList = value;
            }
        }

        public ListCollectionView BrowserList
        {
            get
            {
                return _browserList;
            }
            set
            {
                _browserList = value;
            }
        }
        #endregion

        #region EVENT HANDLERS
        private void _record_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var updatedRow = sender as DlkExecutionQueueRecord;
            bool hasKeepOpenChanges = false;
            switch (e.PropertyName)
            {
                case "execute":
                    //Ask user if deleting ALL dependency conditions is OK
                    bool eContinue = true;

                    if (_editList.FindAll(x => x.dependent.ToLower() == "true").Count > 0
                        && DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_DELETE_ALL_CURRENT_EXEC_CONDITIONS) == MessageBoxResult.No)
                    {
                        eContinue = false;
                    }

                    //Remove also dependencies if execute property has changed
                    if (eContinue & _editList.FirstOrDefault().execute != updatedRow.execute)
                    {
                        _editList.ForEach(x => x.execute = updatedRow.execute);
                        _editList.ForEach(x => x.dependent = "false");
                        _editList.ForEach(x => x.executedependency = null);
                        _editList.ForEach(x => x.executedependencyresult = string.Empty);
                        _editList.ForEach(x => x.executedependencytestrow = string.Empty);
                        mainWindow.isChanged = true;
                    }

                    break;
                case "environment":
                    if (_editList.FirstOrDefault().environment != updatedRow.environment)
                    {
                        _editList.ForEach(x => x.environment = x.keepopenfieldsenabled ? updatedRow.environment : x.environment);
                        hasKeepOpenChanges = true;
                        mainWindow.isChanged = true;
                    }
                    break;
                case "Browser":
                    if (_editList.FirstOrDefault().Browser.Name != updatedRow.Browser.Name)
                    {
                        _editList.ForEach(x => x.Browser = x.keepopenfieldsenabled ? updatedRow.Browser : x.Browser);
                        hasKeepOpenChanges = true;
                        mainWindow.isChanged = true;
                    }
                    break;
                case "keepopen":
                    _editList.ForEach(x => x.keepopen = updatedRow.keepopen);
                    hasKeepOpenChanges = true;
                    mainWindow.isChanged = true;
                    break;
            }
            mainWindow.TestQueue.CommitEdit();
            mainWindow.TestQueue.CancelEdit();
            mainWindow.TestQueue.Items.Refresh();
            if (hasKeepOpenChanges)
            {
                mainWindow.ChangeKeepOpenBrowsers();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _record.PropertyChanged -= _record_PropertyChanged;
        }
        #endregion
    }
}
