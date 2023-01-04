using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for SchedulerMultiHost.xaml
    /// </summary>
    public partial class SchedulerMultiHostv2 : Window
    {
        #region PRIVATE MEMBERS
        private ObservableCollection<Host> m_Hosts = new ObservableCollection<Host>();
        /// <summary>
        /// Used to manage the scedule data in the grid
        /// </summary>
        private ObservableCollection<DlkScheduleRecord> _SchedRecs;
        private bool dataContextChanged = true;
        private bool isChanged = false;
        BackgroundWorker schedulerWatcher = new BackgroundWorker();
        private List<DlkScheduleRecord> _tempSched = new List<DlkScheduleRecord>();
        private bool isManuallyTriggered = false;
        private string allowRmtAccessPath = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "AllowRemoteAccess.dat");

        private bool AddCurrentProductOnly
        {
            get
            {
                return false;
                //return (bool)chkShowCurrentProductOnly.IsChecked;
            }
        }

        private ObservableCollection<Host> LiveHosts
        {
            get
            {
                return m_Hosts;
            }
        }

        private List<Host> Hosts
        {
            get
            {
                return GetHostsFromFile();
            }
        }
        #endregion

        #region PUBLIC PROPERTIES
        public static bool IsOpen { get; set; }
        public string UpdatedHost { get; set; }
        public delegate void InvokerDelegate(Host LiveHost);
        public delegate void UpdateStatus();
        /// <summary>
        /// Looks for the scheduler process and reports back on if it is running
        /// </summary>
        public Boolean SchedulerStatus
        {
            get
            {
                bool ret = true;
                if (lstHosts.SelectedIndex == 0)
                {
                    ret = DlkProcess.IsProcessRunning("TestRunnerScheduler");
                    btnDeleteHost.IsEnabled = false;
                }
                else if (lstHosts.SelectedIndex != -1)
                {
                    if (((Host)(lstHosts.SelectedItem)).Status == "OFF")
                    {
                        ret = false;
                    }
                    else
                    {
                        bool? cmd = IsSchedulerRunning(((Host)lstHosts.SelectedItem).Name);
                        if (cmd == null)
                        {
                            ret = false;
                            //Initialize();
                        }
                        else
                        {
                            ret = (bool)cmd;
                        }
                    }
                    btnDeleteHost.IsEnabled = true;
                }
                btnTurnOn.Content = ret ? "Turn Off" : "Turn On";
                lblSchedulerStatus.Background = ret ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.MistyRose);
                lblSchedulerStatus.Content = ret ? "Scheduler is currently running. Click \"Turn Off\" to stop."
                    : "Scheduler is not running. Click \"Turn On\" to start.";
                //btnTurnOn.Foreground = ret ? new SolidColorBrush(Colors.MistyRose) : new SolidColorBrush(Colors.LightGreen);
                return ret;
            }
        }

        public List<DlkScheduleRecord> SchedRecs
        {
            get
            {
                //if (dataContextChanged)
                //{
                //    _SchedRecs = new ObservableCollection<DlkScheduleRecord>();
                //    if (lstHosts.SelectedItem != null && ((Host)lstHosts.SelectedItem).Status != "OFF")
                //    {
                //        foreach (DlkScheduleRecord sr in DlkSchedulerFileHandler.GetSchedule())
                //        {
                //            //if (AddCurrentProductOnly && sr.mproduct != DlkEnvironment.mProduct)
                //            //{
                //            //    continue;
                //            //}
                //            _SchedRecs.Add(sr);
                //        }
                //    }
                //    dataContextChanged = false;
                //}
                //return _SchedRecs.OrderBy(x => x.msuitedisplay).ToList();
                return GetSchedulesFromFile();
            }
        }

        public List<DlkScheduleRecord> schedMon = new List<DlkScheduleRecord>();
        public List<DlkScheduleRecord> schedTues = new List<DlkScheduleRecord>();
        public List<DlkScheduleRecord> schedWed = new List<DlkScheduleRecord>();
        public List<DlkScheduleRecord> schedThurs = new List<DlkScheduleRecord>();
        public List<DlkScheduleRecord> schedFri = new List<DlkScheduleRecord>();
        public List<DlkScheduleRecord> schedSat = new List<DlkScheduleRecord>();
        public List<DlkScheduleRecord> schedSun = new List<DlkScheduleRecord>();
        #endregion

        #region PUBLIC METHODS

        public SchedulerMultiHostv2()
        {
            InitializeComponent();
            //dgScheduler.DataContext = SchedRecs;
            //SchedulerStatus.ToString(); // to change states
            // create a listner for scheduler changes
            schedulerWatcher.WorkerReportsProgress = true;
            schedulerWatcher.DoWork += schedulerWatcher_DoWork;
            schedulerWatcher.ProgressChanged += schedulerWatcher_ProgressChanged;
            schedulerWatcher.RunWorkerAsync();
        }

        #endregion

        #region PRIVATE METHODS

        private DlkScheduleRecord CreateDummyRec(DayOfWeek day)
        {
            List<DlkExternalScript> lsEscr = new List<DlkExternalScript>();
            DlkScheduleRecord _mDummyRec = new DlkScheduleRecord(day, DateTime.Today, 1000, "", DlkEnvironment.mLibrary, DlkEnvironment.mProductFolder, true, false, "dummy", lsEscr);

            return _mDummyRec;
        }

        private void UpdateSchedulerStatus()
        {
            SchedulerStatus.ToString();
        }

        private List<DlkScheduleRecord> GetSchedulesFromFile()
        {
            if (dataContextChanged)
            {
                _SchedRecs = new ObservableCollection<DlkScheduleRecord>();
                if (lstHosts.SelectedItem != null && ((Host)lstHosts.SelectedItem).Status != "OFF")
                {
                    //foreach (DlkScheduleRecord sr in DlkSchedulerFileHandler.GetSchedule())
                    //{
                    //    //if (AddCurrentProductOnly && sr.mproduct != DlkEnvironment.mProduct)
                    //    //{
                    //    //    continue;
                    //    //}
                    //    _SchedRecs.Add(sr);
                    //}
                    foreach (DlkScheduleRecord sr in DlkSchedulerFileHandler.LoadSchedule())
                    {
                        _SchedRecs.Add(sr);
                    }
                }
                dataContextChanged = false;
                RefreshButtonStates();
            }
            //return _SchedRecs.OrderBy(x => x.msuitedisplay).ToList();
            return _SchedRecs.OrderBy(x => x.TestSuiteDisplay).ToList();

        }

        private void RemoveSuitesWithoutSchedules()
        {

            for (int i = 0; i < _SchedRecs.Count; i++)
            {
                bool IsEmptyRecord = false;
                if (_SchedRecs[i].TestSuiteRelativePath == "dummy") { IsEmptyRecord = true; }
                if (IsEmptyRecord)
                {
                    _SchedRecs.Remove(_SchedRecs[i]);
                }
            }
        }

        //need to update
        private void SaveSched(string updatedHost = "")
        {
            try
            {
                RemoveSuitesWithoutSchedules();
                string currFile = string.Empty;
                var host = string.Empty;
                if (lstHosts.SelectedItem != null)
                {
                    host = ((Host)lstHosts.SelectedItem).Name;
                }

                if (!string.IsNullOrEmpty(updatedHost))
                {
                    host = updatedHost;
                }

                currFile = System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), host + ".xml");


                DlkSchedulerFileHandler.mScheduleFile = currFile;
                //DlkSchedulerFileHandler.UpdateScheduleFile(_SchedRecs.ToList());

                DlkSchedulerFileHandler.UpdateSchedule(_SchedRecs.ToList()); //new save

                string msg = "Schedule Saved.";
                isChanged = false;
                if (lstHosts.SelectedIndex == 0 && host == ((Host)lstHosts.Items[0]).Name)
                {
                    if (SchedulerStatus)
                    {
                        DlkEnvironment.KillProcessByName("TestRunnerScheduler");
                        DlkProcess.RunProcess(
                            System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestRunnerScheduler.exe"),
                            "",
                            System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            false,
                            -1);
                        msg += "\n\nScheduler was restarted.";
                    }
                }
                else
                {
                    if (SchedulerStatus)
                    {
                        // just stop start, dont display message
                        byte[] byt = SendCommandToServer(host, "stop");
                        if (byt != null && bool.Parse(Encoding.ASCII.GetString(byt)))
                        {
                            SendCommandToServer(host, "setschedule");
                            string Response = Encoding.ASCII.GetString(SendDataToServer(host,
                                System.IO.File.ReadAllBytes(currFile)));
                            msg += "\n\nFile location on remote machine: " + Response;
                            Thread.Sleep(1000);
                            SendCommandToServer(host, "start");
                        }
                        else
                        {
                            // what? let's see
                            Initialize();
                            return;
                        }
                    }
                    else
                    {
                        byte[] byt = SendCommandToServer(host, "setschedule");
                        if (byt == null)
                        {
                            Initialize();
                            return;
                        }
                        string Response = Encoding.ASCII.GetString(SendDataToServer(host,
                            System.IO.File.ReadAllBytes(currFile)));
                        msg += "\n\nFile location on remote machine: " + Response;
                    }

                }
                //SchedulerStatus.ToString(); // to change states
                MessageBox.Show(msg, "Scheduler", MessageBoxButton.OK, MessageBoxImage.Information);
                //AddDummyRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PromptToSave()
        {
            if (isChanged)
            {
                MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancelWarning(DlkUserMessages.ASK_UNSAVED_SCHEDULE_CHANGES, "Save changes?");
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        SaveSched(UpdatedHost);
                        UpdatedHost = "";
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    default:
                        break;
                }
                isChanged = false;
            }
        }

        private bool PromptToSaveAndRun()
        {
            if (isChanged)
            {
                MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancelWarning(DlkUserMessages.ASK_UNSAVED_SCHEDULE_CHANGES_RUNNOW, "Save changes?");
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        SaveSched(UpdatedHost);
                        UpdatedHost = "";
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void Initialize()
        {
            LiveHosts.Clear();
            // Add local
            //LiveHosts.Add(new Host(Environment.MachineName, HostType.LOCAL, Environment.MachineName + ".xml"));
            if (LiveHosts.Count == 0) //To prevent duplicate hosts in the view
            {
                LiveHosts.Add(new Host(Environment.MachineName, HostType.LOCAL, "ON"));
            }

            foreach (Host hst in Hosts)
            {
                BackgroundWorker myBgw = new BackgroundWorker();
                myBgw.DoWork += new DoWorkEventHandler(bw_DoExecute);
                myBgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_ExecuteCompleted);
                myBgw.RunWorkerAsync(hst);
            }
            lstHosts.SelectedIndex = 0;
        }

        private void LoadListUsingBackgroundWorker()
        {
            if (SchedRecs.Count <= 0)
            {
                ClearListContents();
                LoadListDataContext();
                RefreshListData();
                if (SchedRecs.Count < 0)
                {
                    return;
                }
            }
            if (((Host)lstHosts.SelectedItem).Status != "OFF")
            {
                EnableContextMenuItem("copysched", true);

                BackgroundWorker lvBgw = new BackgroundWorker();
                lvBgw.DoWork += new DoWorkEventHandler(lvbw_DoExecute);
                lvBgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(lvbw_ExecuteCompleted);
                lvBgw.RunWorkerAsync();
            }
            else
            {
                EnableContextMenuItem("copysched", false);
                EnableContextMenuItem("pastesched",false);
            }
        }

        //private void UpdateLiveHosts(List<Host> RespondingHosts)
        private void UpdateLiveHosts(Host RespondingHost)
        {
            //LiveHosts.Clear();

            //// Add local
            //LiveHosts.Add(new Host(Environment.MachineName, HostType.LOCAL, Environment.MachineName + ".xml"));

            //foreach (Host hst in RespondingHosts)
            //{
            //    LiveHosts.Add(hst);
            //}
            //lstHosts.SelectedIndex = 0;
            if (RespondingHost != null)
            {
                foreach (Host hst in LiveHosts)
                {
                    if (hst.Name == RespondingHost.Name)
                    {
                        return;
                    }
                }
                LiveHosts.Add(RespondingHost);
            }
        }

        private List<Host> GetHostsFromFile()
        {
            if (!System.IO.File.Exists(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "Hosts.xml")))
            {
                SaveHostsToFile(new List<Host>());
            }
            List<Host> ret = new List<Host>();

            //mTestSuiteRecs = new List<DlkExecutionQueueRecord>();
            XDocument DlkXml = XDocument.Load(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "Hosts.xml"));

            var data = from doc in DlkXml.Descendants("host")
                       select new
                       {
                           name = doc.Attribute("name").Value,
                           type = doc.Attribute("type").Value == "local" ? HostType.LOCAL : HostType.NETWORK,
                           status = doc.Attribute("status").Value,
                       };
            foreach (var val in data)
            {
                Host hst = new Host(val.name, val.type, val.status);
                ret.Add(hst);
            }

            return ret;
        }

        private void SaveHostsToFile(List<Host> ListOfHosts)
        {
            List<XElement> hosts = new List<XElement>();
            foreach (Host hst in ListOfHosts)
            {
                hosts.Add(new XElement("host",
                    new XAttribute("name", hst.Name),
                    new XAttribute("type", hst.Type == HostType.LOCAL ? "local" : "network"),
                    new XAttribute("status", hst.Status))
                    );
            }

            XElement hostsRoot = new XElement("hosts", hosts);
            XDocument xDoc = new XDocument(hostsRoot);
            xDoc.Save(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "Hosts.xml"));
        }

        private bool IsHostLive(string HostName)
        {
            try
            {
                string Response = Encoding.ASCII.GetString(SendCommandToServer(HostName, "alive"));
                return bool.Parse(Response);
            }
            catch
            {
                return false;
            }
        }

        private bool? IsSchedulerRunning(string HostName)
        {
            try
            {
                string Response = Encoding.ASCII.GetString(SendCommandToServer(HostName, "running"));
                return bool.Parse(Response);
            }
            catch
            {
                return null;
            }
        }

        private void CheckProcessCompleted(object sender, DoWorkEventArgs e)
        {
            while (DlkProcess.IsProcessRunning("TestRunnerScheduler"))
            {
                Thread.Sleep(2000);
            }
        }

        private byte[] SendCommandToServer(string Server, string Command)
        {
            UdpClient udpc = new UdpClient(Server, 2055);
            byte[] tdata = new byte[1472];
            byte[] sdata, rdata;
            try
            {
                IPEndPoint ep = null;
                sdata = Encoding.ASCII.GetBytes(Command);
                //udpc.Client.ReceiveTimeout = Command == "alive" ? (5 * 1000) : ((60 * 1000) * (60 * 8)); // 5 sec if alive command else 8 hrs
                udpc.Client.ReceiveTimeout = 10000;
                udpc.Send(sdata, sdata.Length);
                int currLength = 0;
                do
                {
                    if (currLength > 0)
                    {
                        Array.Resize(ref tdata, tdata.Length + 1472);
                    }
                    rdata = udpc.Receive(ref ep);
                    Buffer.BlockCopy(rdata, 0, tdata, currLength, rdata.Length);
                    currLength += rdata.Length;
                }
                while (rdata.Length >= 1472);

                // trim to use only 
                byte[] ret = new byte[currLength];
                Array.Copy(tdata, 0, ret, 0, currLength);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        private byte[] SendDataToServer(string Server, byte[] Data)
        {
            UdpClient udpc = new UdpClient(Server, 2055);
            byte[] rdata;
            try
            {
                IPEndPoint ep = null;

                udpc.Send(Data, Data.Length);
                rdata = udpc.Receive(ref ep);
                return rdata;
            }
            catch
            {
                return null;
            }
        }

        private string RetrieveEmailfromXML(string filepath, string mySuite)
        {
            XDocument xDoc = XDocument.Load(filepath);
            var node = xDoc.Descendants("schedrec").Where(e => e.Attribute("suite").Value == mySuite).FirstOrDefault();
            return (string)node.Attribute("email");
        }

        private void MoveScheduleUp(int order, int index, DayOfWeek weekday, ListView lv)
        {
            if (order < 2)
            {
                //do nothing
            }
            else
            {
                int currorder = order;
                DateTime timeSched = SchedRecs.Find(x => x.Order == (currorder - 1) & x.Day == weekday).Time;

                //replace timeschedule of current order
                _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == currorder & x.Day == weekday)))].Time = timeSched;

                if (order == 2) //set RunStatus to true if topmost suite
                    _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == currorder & x.Day == weekday)))].RunStatus = true;

                //swap order
                _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == (currorder - 1) & x.Day == weekday)))].Order = currorder;
                _SchedRecs[index].Order = currorder - 1;
                //_SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == currorder & x.Day == weekday)))].Order = currorder - 1;

            }

            RefreshPerList(lv, weekday.ToString().ToLower());

        }

        private void MoveScheduleDown(int maxCount, int order, int index, DayOfWeek weekday, ListView lv)
        {
            if (order == maxCount)
            {
                //do nothing
            }
            else
            {
                int currorder = order;
                DateTime timeSched = SchedRecs.Find(x => x.Order == currorder && x.Day == weekday).Time;

                //swap timeschedules
                _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == (currorder + 1) && x.Day == weekday)))].Time = timeSched;
                //_SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == currorder && x.Day == weekday)))].Time = timeSched;

                //swap order
                _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == (currorder + 1) && x.Day == weekday)))].Order = currorder;
                _SchedRecs[index].Order = currorder + 1;

            }

            RefreshPerList(lv, weekday.ToString().ToLower());

        }

        private int GetVisibleItemCount(ListView lv)
        {
            int count = 0;
            foreach (ListViewItem lvitem in FindVisualChildren<ListViewItem>(lv))
            {
                if (lvitem.IsVisible)
                {
                    count++;
                }
            }
            return count;
        }

        private string GetLastDisplayedSuite(ListView lv)
        {
            string suite = "";
            foreach (ListViewItem lvitem in FindVisualChildren<ListViewItem>(lv))
            {
                if (lvitem.IsVisible)
                {
                    ContentPresenter cp = FindVisualChild<ContentPresenter>(lvitem);
                    DataTemplate myDataTemplate = cp.ContentTemplate;
                    TextBlock tb = (TextBlock)myDataTemplate.FindName("txtSuite", cp);
                    if (!(tb.Text.Equals("dummy [" + DlkEnvironment.mProductFolder + "]")))
                    {
                        suite = tb.Text;
                    }
                }
            }
            return suite;
        }

        private string GetItemSchedule(ListView lv, GridViewColumn col)
        {
            return ((DlkScheduleRecord)lv.SelectedItem).Time.ToString();
        }

        private DayOfWeek GetWeekdayByListView(String lvName)
        {
            switch (lvName.ToLower())
            {
                case "lvmonday":
                    return DayOfWeek.Monday;
                case "lvtuesday":
                    return DayOfWeek.Tuesday;
                case "lvwednesday":
                    return DayOfWeek.Wednesday;
                case "lvthursday":
                    return DayOfWeek.Thursday;
                case "lvfriday":
                    return DayOfWeek.Friday;
                case "lvsaturday":
                    return DayOfWeek.Saturday;
                case "lvsunday":
                    return DayOfWeek.Sunday;
                default:
                    return DayOfWeek.Monday;
            }
        }

        private void RefreshScheduler()
        {
            //dataContextChanged = true;
            LoadSchedPerDay();
            LoadListDataContext();
            RefreshListData();
            //dgScheduler.DataContext = SchedRecs;
            //dgScheduler.Items.Refresh();
            RefreshButtonStates();
        }

        private void RefreshPerList(ListView lv, String weekday)
        {
            switch (weekday)
            {
                case "monday":
                    schedMon = _SchedRecs.Where(x => x.Day == DayOfWeek.Monday).OrderBy(x => x.Order).ToList();
                    if (!schedMon.Any(x => x.TestSuiteRelativePath == "dummy")) { schedMon.Add(CreateDummyRec(DayOfWeek.Monday)); }
                    lvMonday.DataContext = schedMon;
                    lvMonday.Items.Refresh();
                    break;
                case "tuesday":
                    schedTues = _SchedRecs.Where(x => x.Day == DayOfWeek.Tuesday).OrderBy(x => x.Order).ToList();
                    if (!schedTues.Any(x => x.TestSuiteRelativePath == "dummy")) { schedTues.Add(CreateDummyRec(DayOfWeek.Tuesday)); }
                    lvTuesday.DataContext = schedTues;
                    lvTuesday.Items.Refresh();
                    break;
                case "wednesday":
                    schedWed = _SchedRecs.Where(x => x.Day == DayOfWeek.Wednesday).OrderBy(x => x.Order).ToList();
                    if (!schedWed.Any(x => x.TestSuiteRelativePath == "dummy")) { schedWed.Add(CreateDummyRec(DayOfWeek.Wednesday)); }
                    lvWednesday.DataContext = schedWed;
                    lvWednesday.Items.Refresh();
                    break;
                case "thursday":
                    schedThurs = _SchedRecs.Where(x => x.Day == DayOfWeek.Thursday).OrderBy(x => x.Order).ToList();
                    if (!schedThurs.Any(x => x.TestSuiteRelativePath == "dummy")) { schedThurs.Add(CreateDummyRec(DayOfWeek.Thursday)); }
                    lvThursday.DataContext = schedThurs;
                    lvThursday.Items.Refresh();
                    break;
                case "friday":
                    schedFri = _SchedRecs.Where(x => x.Day == DayOfWeek.Friday).OrderBy(x => x.Order).ToList();
                    if (!schedFri.Any(x => x.TestSuiteRelativePath == "dummy")) { schedFri.Add(CreateDummyRec(DayOfWeek.Friday)); }
                    lvFriday.DataContext = schedFri;
                    lvFriday.Items.Refresh();
                    break;
                case "saturday":
                    schedSat = _SchedRecs.Where(x => x.Day == DayOfWeek.Saturday).OrderBy(x => x.Order).ToList();
                    if (!schedSat.Any(x => x.TestSuiteRelativePath == "dummy")) { schedSat.Add(CreateDummyRec(DayOfWeek.Saturday)); }
                    lvSaturday.DataContext = schedSat;
                    lvSaturday.Items.Refresh();
                    break;
                case "sunday":
                    schedSun = _SchedRecs.Where(x => x.Day == DayOfWeek.Sunday).OrderBy(x => x.Order).ToList();
                    if (!schedSun.Any(x => x.TestSuiteRelativePath == "dummy")) { schedSun.Add(CreateDummyRec(DayOfWeek.Sunday)); }
                    lvSunday.DataContext = schedSun;
                    lvSunday.Items.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void LoadListDataContext()
        {
            lvMonday.DataContext = schedMon;
            lvTuesday.DataContext = schedTues;
            lvWednesday.DataContext = schedWed;
            lvThursday.DataContext = schedThurs;
            lvFriday.DataContext = schedFri;
            lvSaturday.DataContext = schedSat;
            lvSunday.DataContext = schedSun;
        }

        private void ClearListContents()
        {
            schedMon.Clear();
            schedTues.Clear();
            schedWed.Clear();
            schedThurs.Clear();
            schedFri.Clear();
            schedSat.Clear();
            schedSun.Clear();
        }

        private void RefreshListData()
        {
            lvMonday.Items.Refresh();
            lvTuesday.Items.Refresh();
            lvWednesday.Items.Refresh();
            lvThursday.Items.Refresh();
            lvFriday.Items.Refresh();
            lvSaturday.Items.Refresh();
            lvSunday.Items.Refresh();
        }

        private void LoadSchedPerDay()
        {
            schedMon = _SchedRecs.Where(x => x.Day == DayOfWeek.Monday).OrderBy(x => x.Order).ToList();
            schedTues = _SchedRecs.Where(x => x.Day == DayOfWeek.Tuesday).OrderBy(x => x.Order).ToList();
            schedWed = _SchedRecs.Where(x => x.Day == DayOfWeek.Wednesday).OrderBy(x => x.Order).ToList();
            schedThurs = _SchedRecs.Where(x => x.Day == DayOfWeek.Thursday).OrderBy(x => x.Order).ToList();
            schedFri = _SchedRecs.Where(x => x.Day == DayOfWeek.Friday).OrderBy(x => x.Order).ToList();
            schedSat = _SchedRecs.Where(x => x.Day == DayOfWeek.Saturday).OrderBy(x => x.Order).ToList();
            schedSun = _SchedRecs.Where(x => x.Day == DayOfWeek.Sunday).OrderBy(x => x.Order).ToList();

            if (!schedMon.Any(x => x.TestSuiteRelativePath == "dummy")) { schedMon.Add(CreateDummyRec(DayOfWeek.Monday)); }
            if (!schedTues.Any(x => x.TestSuiteRelativePath == "dummy")) { schedTues.Add(CreateDummyRec(DayOfWeek.Tuesday)); }
            if (!schedWed.Any(x => x.TestSuiteRelativePath == "dummy")) { schedWed.Add(CreateDummyRec(DayOfWeek.Wednesday)); }
            if (!schedThurs.Any(x => x.TestSuiteRelativePath == "dummy")) { schedThurs.Add(CreateDummyRec(DayOfWeek.Thursday)); }
            if (!schedFri.Any(x => x.TestSuiteRelativePath == "dummy")) { schedFri.Add(CreateDummyRec(DayOfWeek.Friday)); }
            if (!schedSat.Any(x => x.TestSuiteRelativePath == "dummy")) { schedSat.Add(CreateDummyRec(DayOfWeek.Saturday)); }
            if (!schedSun.Any(x => x.TestSuiteRelativePath == "dummy")) { schedSun.Add(CreateDummyRec(DayOfWeek.Sunday)); }

        }

        private void RemoveExtraDummy()
        {
            //for removing duplicates created by diff backgroundworkers

            if ((schedMon.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedMon.Remove(schedMon.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
            if ((schedTues.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedTues.Remove(schedTues.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
            if ((schedWed.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedWed.Remove(schedWed.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
            if ((schedThurs.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedThurs.Remove(schedThurs.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
            if ((schedFri.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedFri.Remove(schedFri.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
            if ((schedSat.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedSat.Remove(schedSat.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
            if ((schedSun.Where(x => x.TestSuiteRelativePath == "dummy").Count()) > 1) { schedSun.Remove(schedSun.Where(x => x.TestSuiteRelativePath == "dummy").First()); }
        }

        private void ClearAndReorder(ListView lv, DayOfWeek weekday, int index, int maxCount)
        {
            int currorder = 0;
            DateTime timeSched = new DateTime();

            //NEW

            currorder = _SchedRecs[index].Order;
            timeSched = _SchedRecs[index].Time;
            _SchedRecs.RemoveAt(index);

            if (currorder != maxCount)
            {
                _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == (currorder + 1) & x.Day == weekday)))].Time = timeSched;
            }
            for (int i = currorder; i < maxCount; i++)
            {
                _SchedRecs[_SchedRecs.IndexOf(SchedRecs.Find(x => (x.Order == (i + 1) & x.Day == weekday)))].Order = i;
            }

            RefreshPerList(lv, weekday.ToString().ToLower());
        }

        private void CopySchedule(ListView lv, DayOfWeek weekday)
        {
            _tempSched = new List<DlkScheduleRecord>();
            foreach (DlkScheduleRecord dsr in _SchedRecs.Where(x => x.Day == weekday).OrderBy(x => x.Order).ToList())
            {
                _tempSched.Add(dsr.Clone());
            }

            if (_tempSched.Count == 0)
            {
                DlkUserMessages.ShowInfo(DlkUserMessages.INF_SUITE_COPY_NOT_SUCCESSFUL);
                EnableContextMenuItem("pastesched", false);

            }
            else
            {
                DlkUserMessages.ShowInfo(DlkUserMessages.INF_SUITE_COPY_SUCCESSFUL + GetWeekdayByListView(lv.Name).ToString());
                EnableContextMenuItem("pastesched", true);
            }
        }

        private void PasteSchedule(ListView lv, DayOfWeek weekday)
        {
            /* Clear all schedules for that day */
            foreach (DlkScheduleRecord dsr in _SchedRecs.ToList())
            {
                if (dsr.Day == weekday)
                {
                    _SchedRecs.Remove(dsr);
                }
            }

            /* Add the new schedules for that day*/
            foreach (DlkScheduleRecord _dsr in _tempSched)
            {
                _dsr.Day = weekday;
                _SchedRecs.Add(_dsr);
            }

            RefreshPerList(lv, weekday.ToString().ToLower());
        }

        private void EnableContextMenuItem(String menuitem, Boolean isEnable)
        {
            switch (menuitem)
            {
                case "copysched":
                    mniCopySchedMon.IsEnabled = isEnable;
                    mniCopySchedTues.IsEnabled = isEnable;
                    mniCopySchedWed.IsEnabled = isEnable;
                    mniCopySchedThu.IsEnabled = isEnable;
                    mniCopySchedFri.IsEnabled = isEnable;
                    mniCopySchedSat.IsEnabled = isEnable;
                    mniCopySchedSun.IsEnabled = isEnable;
                    break;
                case "pastesched":
                    mniPasteSchedMon.IsEnabled = isEnable;
                    mniPasteSchedTues.IsEnabled = isEnable;
                    mniPasteSchedWed.IsEnabled = isEnable;
                    mniPasteSchedThu.IsEnabled = isEnable;
                    mniPasteSchedFri.IsEnabled = isEnable;
                    mniPasteSchedSat.IsEnabled = isEnable;
                    mniPasteSchedSun.IsEnabled = isEnable;
                    break;
                case "runnow":
                    mniRunNowMon.IsEnabled = isEnable;
                    mniRunNowTues.IsEnabled = isEnable;
                    mniRunNowWed.IsEnabled = isEnable;
                    mniRunNowThu.IsEnabled = isEnable;
                    mniRunNowFri.IsEnabled = isEnable;
                    mniRunNowSat.IsEnabled = isEnable;
                    mniRunNowSun.IsEnabled = isEnable;
                    if (isEnable)
                        RefreshRunNowState();
                    break;
            }
        }

        /// <summary>
        /// Enables/disables right click context menu item "Run Now" on each day depending on if there are any scheduled suite on the particular day 
        /// </summary>
        private void RefreshRunNowState()
        {
            mniRunNowMon.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Monday);
            mniRunNowTues.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Tuesday);
            mniRunNowWed.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Wednesday);
            mniRunNowThu.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Thursday);
            mniRunNowFri.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Friday);
            mniRunNowSat.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Saturday);
            mniRunNowSun.IsEnabled = _SchedRecs.Any(x => x.Day == DayOfWeek.Sunday);
        }

        #region //methods to find child/parent objects

        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement
        {
            FrameworkElement child = null;
            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)
            {
                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;
                System.Diagnostics.Debug.WriteLine(child);
                if (child != null && child.GetType() == typeof(T))
                { break; }
                else if (child != null)
                {
                    child = GetFrameworkElementByName<T>(child);
                    if (child != null && child.GetType() == typeof(T))
                    {
                        break;
                    }
                }
            }
            return child as T;
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
        #endregion

        private void ImportSchedule(object sender, RoutedEventArgs e)
        {
            try
            {
                //import variable
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog()
                {
                    InitialDirectory = DlkEnvironment.mDirUserData,
                    Title = "Open Scheduler File",
                    Filter = "XML file (*.xml)|*.xml",
                };
                var dialogResult = openFile.ShowDialog();
                if (dialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    var currentmScheduleFileValue = DlkSchedulerFileHandler.mScheduleFile;
                    DlkSchedulerFileHandler.mScheduleFile = openFile.FileName;
                    OverwriteScheduledSuites();
                    DlkSchedulerFileHandler.mScheduleFile = currentmScheduleFileValue;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        private void ExportSchedule(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog()
                {
                    AddExtension = true,
                    InitialDirectory = DlkEnvironment.mDirUserData,
                    OverwritePrompt = true,
                    Title = "Save Scheduled Suites As",
                    Filter = "XML file (*.xml)|*.xml",
                };
                var dialogResult = saveFile.ShowDialog();
                if (dialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    //save changes
                    SaveSched(UpdatedHost);
                    UpdatedHost = "";
                    string userMachineName = Environment.MachineName;
                    //store the contents of the scheduler file file of the current machine
                    string schedulerFileContents = File.ReadAllText(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), userMachineName + ".xml"));
                    string fileName = saveFile.FileName;
                    if (fileName != "")
                    {
                        File.WriteAllText(fileName, schedulerFileContents);
                    }
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void OverwriteScheduledSuites()
        {
            var result = MessageBox.Show(DlkUserMessages.ASK_OVERWRITE_EXISTING_SCHED, "Import file", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                //remove the contents of the grid
                _SchedRecs.Clear();
                ClearListContents();
                RefreshScheduler();
                RemoveExtraDummy();
                isChanged = true;
                UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                //display contents of the imported file
                foreach (DlkScheduleRecord sr in DlkSchedulerFileHandler.LoadSchedule())
                {
                    _SchedRecs.Add(sr);
                }
                RefreshScheduler();
            }
        }

        private void RefreshButtonStates()
        {
            btnClearAll.IsEnabled = _SchedRecs.Any();
            RefreshRunNowState();
        }

        #endregion

        #region EVENTS

        private void schedulerWatcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Dispatcher.BeginInvoke(new UpdateStatus(UpdateSchedulerStatus), null);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void schedulerWatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    schedulerWatcher.ReportProgress(0);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSched(UpdatedHost);
                UpdatedHost = "";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        private void btnTurnOn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isManuallyTriggered = true;
                if (lstHosts.SelectedIndex == 0)
                {
                    if (SchedulerStatus) // kill
                    {
                        DlkEnvironment.KillProcessByName("TestRunnerScheduler");
                    }
                    else // start
                    {
                        DlkProcess.RunProcess(
                            System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestRunnerScheduler.exe"),
                            "",
                            System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            false,
                            -1);
                    }
                }
                else
                {
                    if (SchedulerStatus) // kill
                    {
                        SendCommandToServer(((Host)lstHosts.SelectedItem).Name, "stop");
                    }
                    else // start
                    {
                        SendCommandToServer(((Host)lstHosts.SelectedItem).Name, "start");
                    }
                }
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            //SchedulerStatus.ToString(); // to change states
        }

        //private void btnTurnOff_Click(object sender, RoutedEventArgs e)
        //{
        //    DlkEnvironment.KillProcessByName("TestRunnerScheduler");
        //    Thread.Sleep(2000);
        //    if (SchedulerStatus)
        //    {
        //        btnTurnOff.IsEnabled = true;
        //        btnTurnOn.IsEnabled = false;
        //        lblSchedulerStatus.Content = "Scheduler is currently running. Click \"Turn Off\" to stop.";
        //    }
        //    else
        //    {
        //        btnTurnOff.IsEnabled = false;
        //        btnTurnOn.IsEnabled = true;
        //        lblSchedulerStatus.Content = "Scheduler is not running. Click \"Turn On\" to start.";
        //    }
        //}

        private void Window_StateChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == System.Windows.WindowState.Normal)
                {
                    this.Height = MinHeight;
                    this.Width = MinWidth;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (isChanged)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancelWarning(DlkUserMessages.ASK_UNSAVED_SCHEDULE_CHANGES, "Save Changes?");
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            SaveSched(UpdatedHost);
                            UpdatedHost = "";
                            e.Cancel = false;
                            break;
                        case MessageBoxResult.No:
                            e.Cancel = false;
                            break;
                        case MessageBoxResult.Cancel:
                            e.Cancel = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Set static flag to inform callers of window visiblity state */
                IsOpen = true;

                Initialize();
                //lstHosts.DataContext = LiveHosts;
                lstHosts.ItemsSource = LiveHosts;
                lstHosts.DisplayMemberPath = "Name";
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstHosts.ItemsSource);
                PropertyGroupDescription groupDesc = new PropertyGroupDescription("Type");
                view.GroupDescriptions.Add(groupDesc);

                // Agent status
                if (File.Exists(allowRmtAccessPath) && !DlkProcess.IsProcessRunning("AutomationAgent"))
                {
                    DlkProcess.RunProcess("AutomationAgent", "", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, -1);
                }
                else if (!File.Exists(allowRmtAccessPath) && DlkProcess.IsProcessRunning("AutomationAgent"))
                {
                    DlkEnvironment.KillProcessByName("AutomationAgent");
                }
                chkAllowRemote.IsChecked = File.Exists(allowRmtAccessPath);

                //load list contents
                LoadListUsingBackgroundWorker();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lvbw_DoExecute(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadSchedPerDay();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lvbw_ExecuteCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                LoadListDataContext();
                RefreshListData();
                RemoveExtraDummy();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void bw_DoExecute(object sender, DoWorkEventArgs e)
        {
            try
            {
                Host live = (Host)e.Argument;
                if (IsHostLive(live.Name))
                {
                    live.Status = "ON";
                    e.Result = live;
                }
                else
                {
                    live.Status = "OFF";
                    e.Result = live;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            //List<Host> live = new List<Host>();

            //foreach (Host currHost in (List<Host>)e.Argument)
            //{
            //    if (IsHostLive(currHost.Name))
            //    {
            //        live.Add(currHost);
            //    }
            //}

            //e.Result = live;
        }

        private void bw_ExecuteCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Dispatcher.BeginInvoke(new InvokerDelegate(UpdateLiveHosts), (List<Host>)e.Result);
                Dispatcher.BeginInvoke(new InvokerDelegate(UpdateLiveHosts), (Host)e.Result);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lstHosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PromptToSave();
                if (lstHosts.SelectedIndex != -1)
                {
                    string currFile = System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), ((Host)lstHosts.SelectedItem).Name + ".xml");
                    if (System.IO.File.Exists(currFile) && lstHosts.SelectedIndex != 0)
                    {
                        System.IO.File.Delete(currFile);
                    }
                    DlkSchedulerFileHandler.mScheduleFile = currFile;
                    if (lstHosts.SelectedIndex != 0 & ((Host)lstHosts.SelectedItem).Status != "OFF")
                    {
                        byte[] byt = SendCommandToServer(((Host)lstHosts.SelectedItem).Name, "getschedule");
                        if (byt == null)
                        {
                            // Do nothing
                        }
                        else
                        {
                            System.IO.File.WriteAllBytes(currFile, byt);
                        }
                    }
                    dataContextChanged = true;
                    LoadListUsingBackgroundWorker();
                    //SchedulerStatus.ToString();
                    //RefreshScheduler();
                    //dgScheduler.DataContext = SchedRecs;
                    //dgScheduler.Items.Refresh();

                    //local machine
                    if (lstHosts.SelectedIndex == 0)
                    {
                        EnableContextMenuItem("runnow", true);
                    }
                    else //remote test machine
                    {
                        //if in the list
                        if (LiveHosts.Contains(((Host)lstHosts.SelectedItem)))
                        {
                            //and scheduler is on
                            if (((Host)lstHosts.SelectedItem).Status.ToLower()=="on")
                            {
                                //enable Run Now item on context menu
                                EnableContextMenuItem("runnow", true);
                            }
                            else
                            {
                                //disable Run Now item on context menu
                                EnableContextMenuItem("runnow", false);
                            }
                        }
                        else
                        {
                            EnableContextMenuItem("runnow", false);
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteHost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string hostname = ((Host)lstHosts.SelectedItem).Name;
                List<Host> lstToPreserve = Hosts.ToList();
                //foreach(Host hst in LiveHosts)
                //{
                //    if (hst.Name.ToLower() == hostname)
                //    {

                //    }
                //}
                for (int idx = 0; idx < lstToPreserve.Count; idx++)
                {
                    if (lstToPreserve[idx].Name == hostname)
                    {
                        lstToPreserve.RemoveAt(idx);
                        break;
                    }
                }
                // TODO
                // remove any remnant file
                if (System.IO.File.Exists(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), hostname + ".xml")))
                {
                    System.IO.File.Delete(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), hostname + ".xml"));
                }
                SaveHostsToFile(lstToPreserve);
                Initialize();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Initialize();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddHost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddHost dlg = new AddHost();
                dlg.Owner = this;
                dlg.ShowDialog();

                if(dlg.DialogResult.Value)
                    Initialize();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkAllowRemote_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!DlkProcess.IsProcessRunning("AutomationAgent"))
                {
                    DlkProcess.RunProcess("AutomationAgent", "", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, -1);
                }
                if (!File.Exists(allowRmtAccessPath))
                {
                    File.Create(allowRmtAccessPath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkAllowRemote_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkProcess.IsProcessRunning("AutomationAgent"))
                {
                    DlkEnvironment.KillProcessByName("AutomationAgent");
                }
                if (File.Exists(allowRmtAccessPath))
                {
                    File.Delete(allowRmtAccessPath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearSched_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button _btn = (Button)sender;
                ListView lv = FindParent<ListView>(_btn);
                GridViewColumn col = ((GridView)lv.View).Columns[0];
                DlkScheduleRecord _ClrSourceRec = ((DlkScheduleRecord)lv.SelectedItem);
                int clrIndex = _SchedRecs.IndexOf(_ClrSourceRec);
                MessageBoxResult result = DlkUserMessages.ShowQuestionOkCancelWarning(DlkUserMessages.ASK_REMOVE_SUITE_SCHEDULE, "Remove schedule?");
                if (result == MessageBoxResult.OK)
                {
                    ClearAndReorder(lv, _SchedRecs[clrIndex].Day, clrIndex, GetVisibleItemCount(lv) - 1);
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
                //dgScheduler.Items.Refresh();
                RefreshButtonStates();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOptions_Loaded(object sender, RoutedEventArgs e)
        {
            //Button btn = (Button)sender;
            //btn.Visibility = lstHosts.SelectedIndex == 0 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        private void btnSchedSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = ((Host)lstHosts.SelectedItem).Name + ".xml";
                string filePath = System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), fileName);
                Button _btn = (Button)sender;

                ListView lv = FindParent<ListView>(_btn);
                DlkScheduleRecord _ClrSourceRec = ((DlkScheduleRecord)lv.SelectedItem);
                int clrIndex = _SchedRecs.IndexOf(_ClrSourceRec);
                string mySuite = _SchedRecs.ElementAt(clrIndex).TestSuiteRelativePath;
                SchedulerSettingsDialog settingsdlg = new SchedulerSettingsDialog(filePath, mySuite, _ClrSourceRec);
                settingsdlg.Owner = this;
                //emaildlg.mEmails = RetrieveEmailfromXML(filePath, mySuite);
                settingsdlg.mEmails = _SchedRecs.ElementAt(clrIndex).Email;
                if (settingsdlg.ShowDialog() == true)
                {
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEmailSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = ((Host)lstHosts.SelectedItem).Name + ".xml";
                string filePath = System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), fileName);
                Button _btn = (Button)sender;

                ListView lv = FindParent<ListView>(_btn);
                DlkScheduleRecord _ClrSourceRec = ((DlkScheduleRecord)lv.SelectedItem);
                int clrIndex = _SchedRecs.IndexOf(_ClrSourceRec);
                string mySuite = _SchedRecs.ElementAt(clrIndex).TestSuiteRelativePath;
                EmailSettingsDialog emaildlg = new EmailSettingsDialog(filePath, mySuite, _ClrSourceRec);
                emaildlg.Owner = this;
                //emaildlg.mEmails = RetrieveEmailfromXML(filePath, mySuite);
                emaildlg.mEmails = _SchedRecs.ElementAt(clrIndex).Email;
                if (emaildlg.ShowDialog() == true)
                {
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSetSched_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button _btn = (Button)sender;
                SetScheduleDialog setscheddlg;
                ListView lv = FindParent<ListView>(_btn);
                GridViewColumn col = ((GridView)lv.View).Columns[0];

                if (!_btn.Name.Equals("btnEditSched"))
                {
                    //new code
                    setscheddlg = new SetScheduleDialog(SchedRecs, col.Header.ToString(), GetLastDisplayedSuite(lv), "00:00", GetVisibleItemCount(lv), false);
                    #region //old code when not using add dummy
                    //DockPanel dp = FindParent<DockPanel>(_btn);
                    //ListView lv = FindVisualChild<ListView>(dp);
                    //GridViewColumn col = ((GridView)lv.View).Columns[0];
                    //setscheddlg = new SetScheduleDialog(SchedRecs, col.Header.ToString(),GetLastDisplayedSuite(lv),"", false);
                    #endregion

                }
                else if (lv.SelectedIndex > 0)
                {
                    setscheddlg = new SetScheduleDialog(SchedRecs, col.Header.ToString(), ((DlkScheduleRecord)lv.Items[lv.SelectedIndex - 1]).TestSuiteDisplay.ToString(), "00:00", lv.SelectedIndex + 1, true, ((DlkScheduleRecord)lv.Items[lv.SelectedIndex]).RunStatus);
                }
                else
                {
                    #region //old code using datagrid
                    //DlkScheduleRecord _ClrSourceRec = ((DlkScheduleRecord)lv.SelectedItem);
                    //ContentPresenter templateParent = GetFrameworkElementByName<ContentPresenter>(lv);
                    //DataGridRow row = this.dgScheduler.ItemContainerGenerator.ContainerFromItem(dgScheduler.CurrentCell.Item) as DataGridRow;
                    //ContentPresenter CP = dgScheduler.CurrentCell.Column.GetCellContent(row) as ContentPresenter;
                    //ContentPresenter CP = dgScheduler.CurrentCell.Column.GetCellContent(row) as ContentPresenter;
                    //TextBlock mySuite = FindVisualChild<TextBlock>(CP);
                    //DataGridColumn col = this.dgScheduler.CurrentCell.Column;
                    #endregion
                    setscheddlg = new SetScheduleDialog(SchedRecs, col.Header.ToString(), ((DlkScheduleRecord)lv.SelectedItem).TestSuiteDisplay.ToString(), GetItemSchedule(lv, col), ((DlkScheduleRecord)lv.SelectedItem).Order, true, ((DlkScheduleRecord)lv.SelectedItem).RunStatus);
                }

                setscheddlg.Owner = this;
                if (setscheddlg.ShowDialog() == true)
                {
                    if (this._SchedRecs.Count != setscheddlg.mSchedRecs.Count)
                    {
                        this._SchedRecs.Add(setscheddlg.mRec);
                    }
                    for (int i = 0; i < _SchedRecs.Count; i++)
                    {
                        this._SchedRecs[i] = setscheddlg.mSchedRecs[i];
                    }
                    RefreshScheduler();
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button _btn = (Button)sender;
                ListView lv = FindParent<ListView>(_btn);
                GridViewColumn col = ((GridView)lv.View).Columns[0];
                DlkScheduleRecord _SourceRec = ((DlkScheduleRecord)lv.SelectedItem);
                int currindex = _SchedRecs.IndexOf(_SourceRec);

                if (GetVisibleItemCount(lv) < 1)
                {
                    //do nothing
                }else{
                    MoveScheduleUp(_SchedRecs[currindex].Order, currindex, _SchedRecs[currindex].Day,lv);
                    //MoveScheduleUp(col.Header.ToString(),currindex,GetVisibleItemCount(lv)-1,lv);
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button _btn = (Button)sender;
                ListView lv = FindParent<ListView>(_btn);
                GridViewColumn col = ((GridView)lv.View).Columns[0];
                DlkScheduleRecord _SourceRec = ((DlkScheduleRecord)lv.SelectedItem);
                int currindex = _SchedRecs.IndexOf(_SourceRec);

                if (GetVisibleItemCount(lv) < 1)
                {
                    //do nothing
                }
                else
                {
                    MoveScheduleDown(GetVisibleItemCount(lv) - 1,_SchedRecs[currindex].Order, currindex, _SchedRecs[currindex].Day, lv);
                    //MoveScheduleDown(col.Header.ToString(), currindex,GetVisibleItemCount(lv)-1,lv);
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(DlkUserMessages.ASK_CLEAR_ALL_SCHED, "Clear All Test", MessageBoxButton.YesNo, MessageBoxImage.Question,
               MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    _SchedRecs.Clear();
                    ClearListContents();
                    RefreshScheduler();
                    RemoveExtraDummy();

                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCopySched_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem _mni = (MenuItem)sender;
                ListView lv = ((ContextMenu)_mni.Parent).PlacementTarget as ListView;
                CopySchedule(lv, GetWeekdayByListView(lv.Name));
                lv.BorderThickness = new Thickness(1.0);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnPasteSched_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem _mni = (MenuItem)sender;
                ListView lv = ((ContextMenu)_mni.Parent).PlacementTarget as ListView;
                MessageBoxResult result = DlkUserMessages.ShowQuestionOkCancelWarning(DlkUserMessages.ASK_OVERWRITE_SUITE_SCHEDULE, "Overwrite schedule?");
                if (result == MessageBoxResult.OK)
                {
                    PasteSchedule(lv, GetWeekdayByListView(lv.Name));
                    EnableContextMenuItem("pastesched", false);
                    isChanged = true;
                    UpdatedHost = ((Host)lstHosts.SelectedItem).Name;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mniRunNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if Run Now context menu item was clicked while your local machine is selected in the listbox
                if (lstHosts.SelectedIndex == 0)
                {
                    MenuItem _mni = (MenuItem)sender;
                    ListView lv = ((ContextMenu)_mni.Parent).PlacementTarget as ListView;
                    string runDay = GetWeekdayByListView(lv.Name).ToString();
                    bool isTRSRunningInitially = SchedulerStatus;
                    isManuallyTriggered = false;

                    if (PromptToSaveAndRun())
                    {
                        if (isTRSRunningInitially) // kill
                        {
                            DlkEnvironment.KillProcessByName("TestRunnerScheduler");
                        }

                        //run scheduled suites immediately on local machine.
                        DlkProcess.RunProcess(
                            System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestRunnerScheduler.exe"),
                            "/r true /d " + runDay,
                            System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            false,
                            -1);

                        //disable Run Now context menu item while scheduler is running.
                        EnableContextMenuItem("runnow", false);
                        Thread.Sleep(2000);

                        var bw = new BackgroundWorker();
                        //sleep while scheduler is running.
                        bw.DoWork += new DoWorkEventHandler(CheckProcessCompleted);
                        bw.RunWorkerCompleted += delegate
                        {
                            if (isTRSRunningInitially && !isManuallyTriggered)
                            {
                                DlkProcess.RunProcess(
                                    System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestRunnerScheduler.exe"),
                                    "/r false",
                                    System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                    false,
                                    -1);
                            }
                            EnableContextMenuItem("runnow", true);
                        };
                        bw.RunWorkerAsync();
                    }
                }
                else if (lstHosts.SelectedIndex > 0)//if run now was clicked while on a remote test machine.
                {
                    try
                    {
                        MenuItem _mni = (MenuItem)sender;
                        ListView lv = ((ContextMenu)_mni.Parent).PlacementTarget as ListView;
                        string runDay = GetWeekdayByListView(lv.Name).ToString();
                        var remoteMachineName = ((Host)lstHosts.SelectedItem).Name;
                        var args = "start /r true /d " + runDay.ToLower();
                        byte[] byt = SendCommandToServer(remoteMachineName, args);
                        if (byt != null)
                        {
                            string Response = Encoding.ASCII.GetString(byt);
                            if (Response.ToLower().Equals("true"))
                            {
                                DlkUserMessages.ShowInfo(DlkUserMessages.INF_RUN_NOW_REMOTE + remoteMachineName);
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
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

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            try
            {
                ListView lv = ((ContextMenu)sender).PlacementTarget as ListView;
                lv.BorderThickness = new Thickness(5.0);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            try
            {
                ListView lv = ((ContextMenu)sender).PlacementTarget as ListView;
                lv.BorderThickness = new Thickness(1.0);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Closed event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            IsOpen = false;
        }
        #endregion
    }

    public class AlphaNumericComparer : IComparer<string>
    {

        public int Compare(string x, string y){
            x = x.Trim('~');
            y = y.Trim('~');

            int firstNumber, secondNumber;
            bool firstIsNumber = int.TryParse(x, out firstNumber);
            bool secondIsNumber = int.TryParse(y, out secondNumber);

            if (firstIsNumber)
            {
                return secondIsNumber ? firstNumber.CompareTo(secondNumber) : -1;
            }
            return secondIsNumber ? 1 : x.CompareTo(y);
      }
    }

    public class Host
    {
        public Host(string name, HostType type, string status)
        {
            Name = name;
            Type = type;
            Status = status;
        }
        public string Name { get; set; }
        public HostType Type { get; set; }
        public string Status { get; set; }
    }

    public enum HostType
    {
        LOCAL,
        NETWORK
    }
      
}
