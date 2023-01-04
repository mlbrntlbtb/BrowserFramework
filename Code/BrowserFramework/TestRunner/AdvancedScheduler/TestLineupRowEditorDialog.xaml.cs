using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TestRunner.AdvancedScheduler.Model;
using TestRunner.Common;
using TestRunner.Controls;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for TestLineupRowEditorDialog.xaml
    /// </summary>
    public partial class TestLineupRowEditorDialog : Window
    {
        private const int COL_INDEX_STARTTIME = 4;
        private List<TestLineupRecord> _rowEditorSource;
        private List<TestLineupRecord> _editList;
        private ListCollectionView _agentsListView;
        private List<string> _environmentList;
        private ListCollectionView _browserList;
        private TestLineupRecord _suite;
        private AdvancedSchedulerMainForm advSchedulerMainForm;

        public TestLineupRowEditorDialog(AdvancedSchedulerMainForm Owner, List<TestLineupRecord> editList, ListCollectionView agentsListView, List<string> environmentList, ListCollectionView browserList, bool hasGroupMember = false)
        {
            InitializeComponent();
            this.Owner = Owner;
            advSchedulerMainForm = Owner;
            _editList = editList;
            _agentsListView = agentsListView;
            _environmentList = environmentList;
            _browserList = browserList;

            if (editList == null || editList.Count < 1)
                return;

            var defaultEnabledValue = editList.FirstOrDefault().Enabled;
            var defaultAgentValue = editList.FirstOrDefault().RunningAgent;
            var defaultEnvironmentValue = editList.FirstOrDefault().Environment;
            var defaultBrowserValue = editList.FirstOrDefault().Browser;
            var defaultRecurrenceValue = editList.FirstOrDefault().Recurrence;
            var defaultScheduleValue = editList.FirstOrDefault().Schedule;
            var defaultStartTimeValue = editList.FirstOrDefault().StartTime;
            var defaultLibrary = editList.FirstOrDefault().Library;
            var defaultIsBlacklistedValue = defaultEnvironmentValue == TestLineupRecord.DEFAULT_ENVIRONMENT ? false : editList.FirstOrDefault().IsBlacklisted;
            var product = editList.FirstOrDefault().Product;
            _suite = new TestLineupRecord
            {
                Id = Guid.NewGuid().ToString(),
                GroupID = hasGroupMember ? Guid.NewGuid().ToString() : null,
                Enabled = defaultEnabledValue,
                Recurrence = defaultRecurrenceValue,
                Schedule = defaultScheduleValue,
                RunningAgent = defaultAgentValue,
                Environment = defaultEnvironmentValue,
                Browser = defaultBrowserValue,
                StartTime = defaultStartTimeValue,
                IsBlacklisted = defaultIsBlacklistedValue,
                Library = defaultLibrary,
                Product = product
            };
            _suite.PropertyChanged += suite_PropertyChanged;
            _rowEditorSource = new List<TestLineupRecord>() { _suite };
            dgRowEditor.ItemsSource = _rowEditorSource;
        }

        private void suite_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_editList.Any(x => x.Status == Enumerations.TestStatus.Running || x.Status == Enumerations.TestStatus.Pending || x.Status == Enumerations.TestStatus.Cancelling))
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_ROWEDITOR_SAVE_FAIL);
            }
            else
            {
                var updatedRow = sender as TestLineupRecord;
                switch (e.PropertyName)
                {
                    case "Enabled":
                        _editList.ForEach(x => x.Enabled = updatedRow.Enabled);
                        break;
                    case "RunningAgent":
                        _editList.ForEach(x => x.RunningAgent = updatedRow.RunningAgent);
                        break;
                    case "Environment":
                        _editList.ForEach(x => {
                            if (!x.IsDifferentProduct)
                            {
                                x.Environment = updatedRow.Environment;
                            }
                            x.NonDefaultBlacklistedEnvironment = "";
                            x.IsBlacklisted = updatedRow.Environment != TestLineupRecord.DEFAULT_ENVIRONMENT ? updatedRow.IsBlacklisted : false;
                        });
                        advSchedulerMainForm.CheckQueueEnvironmentsForBlacklisted();
                        break;
                    case "Browser":
                        _editList.ForEach(x => x.Browser = updatedRow.Browser);
                        break;
                    case "StartTime":
                        _editList.ForEach(x => x.StartTime = updatedRow.StartTime);
                        break;
                    case "Recurrence":
                        _editList.ForEach(x => x.Recurrence = updatedRow.Recurrence);
                        break;
                    case "Schedule":
                        _editList.ForEach(x => x.Schedule = updatedRow.Schedule);
                        break;
                }
            }
        }

        public ListCollectionView AgentsListView
        {
            get
            {
                return _agentsListView;
            }
            set
            {
                _agentsListView = value;
            }
        }

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

        public List<string> RecurrenceTypes
        {
            get
            {
                return new List<string> { Enumerations.ConvertToString(Enumerations.RecurrenceType.Once),
                                           Enumerations.ConvertToString(Enumerations.RecurrenceType.Daily),
                                           Enumerations.ConvertToString(Enumerations.RecurrenceType.Weekly),
                                           Enumerations.ConvertToString(Enumerations.RecurrenceType.Weekdays),
                                           Enumerations.ConvertToString(Enumerations.RecurrenceType.Monthly) };
            }
        }

        public List<string> DaysOfWeek
        {
            get
            {
                return new List<string> { Enumerations.ConvertToString(DayOfWeek.Monday),
                                            Enumerations.ConvertToString(DayOfWeek.Tuesday),
                                            Enumerations.ConvertToString(DayOfWeek.Wednesday),
                                            Enumerations.ConvertToString(DayOfWeek.Thursday),
                                            Enumerations.ConvertToString(DayOfWeek.Friday),
                                            Enumerations.ConvertToString(DayOfWeek.Saturday),
                                            Enumerations.ConvertToString(DayOfWeek.Sunday) };
            }
        }

        private void dgTestLineup_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            if (dgRowEditor.Columns.IndexOf(e.Column) == COL_INDEX_STARTTIME)
            {
                var presenter = e.Column.GetCellContent(e.Row) as ContentPresenter;
                var tpckr = presenter.ContentTemplate.FindName("tpckrStartTime", presenter) as TimePickerCtrl;
                var tlr = dgRowEditor.SelectedItem as TestLineupRecord;

                tpckr.Hours = tlr.StartTime.Hour;
                tpckr.Minutes = tlr.StartTime.Minute;

                if (tlr.StartTime.Hour >= 12) // PM
                {
                    if (tlr.StartTime.Hour > 12)
                    {
                        tpckr.Hours = tlr.StartTime.Hour - 12;
                    }
                    tpckr.AMPM = "PM";
                }
                else // AM
                {
                    if (tlr.StartTime.Hour == 0)
                    {
                        tpckr.Hours = 12;
                    }
                    tpckr.AMPM = "AM";
                }
            }
        }

        private void dgTestLineup_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var tlr = dgRowEditor.SelectedItem as TestLineupRecord;
            if (dgRowEditor.Columns.IndexOf(e.Column) == COL_INDEX_STARTTIME)
            {
                var presenter = e.Column.GetCellContent(e.Row) as ContentPresenter;
                var tpckr = presenter.ContentTemplate.FindName("tpckrStartTime", presenter) as TimePickerCtrl;

                int hours = tpckr.Hours;
                if (tpckr.AMPM == "PM" && hours < 12)
                {
                    hours += 12;
                }
                else if (tpckr.AMPM == "AM" && hours == 12)
                {
                    hours = 0;
                }

                tlr.StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hours, tpckr.Minutes, 0);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _suite.PropertyChanged -= suite_PropertyChanged;
        }

        private void dgTestLineup_DropDownClosed(object sender, EventArgs e)
        {
            
        }
    }
}
