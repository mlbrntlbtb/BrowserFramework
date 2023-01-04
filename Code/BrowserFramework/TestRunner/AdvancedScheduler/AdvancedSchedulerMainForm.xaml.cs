using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.Common;
using TestRunner.Controls;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.AdvancedScheduler.View;
using TestRunner.AdvancedScheduler.Presenter;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Data;
using CommonLib.DlkHandlers;
using System.Collections;
using TestRunner.AdvancedScheduler.Classes;
using System.Globalization;
using Xceed.Wpf.Toolkit;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestRunner.AdvancedScheduler
{
	/// <summary>
	/// Interaction logic for AdvancedScheduler.xaml
	/// </summary>
	public partial class AdvancedSchedulerMainForm : Window, IControllerView, IScheduleRunnerView, IAgentsView, ITestLineupStatusView
	{
		private const int COL_INDEX_STARTTIME = 6;
		private const int COL_INDEX_SCHEDULE = 8;
		private const int CALCULATE_TEST_COUNT_PROGRESS_PERCENTAGE = 25;
		private const int CHECK_TEST_COUNT_PROGRESS_PERCENTAGE = 75;
		private const int OVERALL_PROGRESS_PERCENTAGE = 100;
		private const string RUN_NOW = "Run Now";
		private const string RUN_NOW_CANCEL = "Cancel";
		private const string QUEUE_FAILED = "Queue failed";
		private const string RUN_NOW_FAILED = "Run Now failed";
		private const string MENU_TEXT_SUITESONLY = "Search for suites only";
		private const string MENU_TEXT_OWNERSONLY = "Search for owners only";
		private const string MENU_TEXT_TAGSONLY = "Search for tags only";
		private const string MENU_TEXT_SUITESANDTAGS = "Include suites and tags in search";
		private KwDirItem.SearchType mSearchType;
		private System.Windows.Forms.NotifyIcon mNotifyIcon = new System.Windows.Forms.NotifyIcon();
		private System.Threading.ManualResetEvent _busy = new System.Threading.ManualResetEvent(false);
		private bool mIsSchedulerFileReadonly = false;
		private bool mIsExplicitClose = false;
		private bool mIsClosing = false;
		private bool mIsEditMode = true;
        private bool mIsLoaded = false; //this is flag for file system watcher onchanged event.
		private bool mIsBlacklistBeingCheckedInBackground = false;
		private string mDateControllerFromOriginalString = "";
		private string mDateControllerToOriginalString = "";
		private string mDateAgentFromOriginalString = "";
		private string mDateAgentToOriginalString = "";
		private string mApplicationName = null;
		private string mParams = null;
		private string schedRecurrencePrevVal = null;
		private int mInitialApp;
		private bool mIsDropTargetATreeViewItem = false;
		private bool mIsBlacklistWarningDisplayed = false;
		private TreeViewItem mSelectedFavorite = null;
		private String mSearchSuitesPreviousFilter = string.Empty;
		private System.Windows.Forms.Timer mTxtSearchSuitesTimer;
		private System.Windows.Forms.Timer mTxtSearchFavoritesTimer;
		private delegate void SearchDelegateType(object sender, System.EventArgs e);
		private ListCollectionView mAllBrowsers;
		private FileSystemWatcher mConfigWatcher = new FileSystemWatcher();
		private ObservableCollection<DlkLoginConfigRecord> mLoginRecords;
		private TestLineupStatus mTestLineupStatus;
		private List<FileInfo> mSuiteFiles;
		private List<DlkTag> mAllTags = new List<DlkTag>();
		private readonly List<SuiteFile> mSuiteTags = new List<SuiteFile>();
		private readonly List<SuiteFile> mSuiteOwners = new List<SuiteFile>();
		private BlacklistTestCheckLoadingDialog mLoader = null;
		private List<int> mTotalTestCount = new List<int>();
		private int mCurrentTestCount = 0;
		private int mCurrentSuiteCount = 0;
		private double mLoaderProgress = 0;
		private double mSuiteUnitCount = 0;
		private double mTestUnitCount = 0;
		private double mCurrentSuiteUnitCount = 0;
		private double mCurrentSuiteBlockCount = 0;
		private bool mIsInitialTestCheck = false;
		private bool mHasAddedToBlockCount = false;

		//Presenter
		private ControllerPresenter mControllerPresenter = null;
		private ScheduleRunnerPresenter mExecutePresenter = null;
		private AgentPresenter mAgentPresenter = null;
		private TestLineupStatusPresenter mTestLineupStatusPresenter = null;

		//BgWorker
		private DlkBackgroundWorkerWithAbort mFileSaveWorker = new DlkBackgroundWorkerWithAbort();
		private DlkBackgroundWorkerWithAbort mExecuteWorker = new DlkBackgroundWorkerWithAbort();
		private DlkBackgroundWorkerWithAbort mAgentWorker = new DlkBackgroundWorkerWithAbort();
		private DlkBackgroundWorkerWithAbort mLoadDirectoryWorker = new DlkBackgroundWorkerWithAbort();
		private DlkBackgroundWorkerWithAbort mTestBlacklistCheckWorker = new DlkBackgroundWorkerWithAbort();
		private DlkBackgroundWorkerWithAbort mTestCheckLoaderWorker = new DlkBackgroundWorkerWithAbort();

		// Backing fields of the implemented IControllerView properties
		private List<KwDirItem> mAllTestSuites = null;
		private List<KwDirItem> mFilteredTestSuites = new List<KwDirItem>();
		private List<KwDirItem> mFilteredFavorites = new List<KwDirItem>();
		private List<TreeViewSelectedItem> mTreeItemSelectionList_Favorites = new List<TreeViewSelectedItem>();
		private List<TreeViewSelectedItem> mTreeItemSelectionList_AllSuites = new List<TreeViewSelectedItem>();
		private ObservableCollection<string> mWebBrowsers;
		private ObservableCollection<string> mRemoteBrowsers;
		private ObservableCollection<string> mMobileBrowsers;
		private ObservableCollection<TestLineupRecord> mTestLineup = new ObservableCollection<TestLineupRecord>();
		private ObservableCollection<KwDirItem> mTestSuiteFavorites = new ObservableCollection<KwDirItem>();
		private ObservableCollection<ExecutionHistory> mTestSuiteExecutionHistory = new ObservableCollection<ExecutionHistory>();
		// Backing fields of the implemented IScheduleRunnerView properties
		private List<TestLineupRecord> mAllSchedules = new List<TestLineupRecord>();
		// Backing fields of the implemented IAgentsView properties
		private ObservableCollection<Agent> mAgentsPool = new ObservableCollection<Agent>();
		private ObservableCollection<Agent> mAgentsFilteredPool = new ObservableCollection<Agent>();
		private ObservableCollection<AgentGroup> mAgentsGroupPool = new ObservableCollection<AgentGroup>();
		private ObservableCollection<ExecutionHistory> mAgentHistory = new ObservableCollection<ExecutionHistory>();

		private ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> colorList = new ObservableCollection<Xceed.Wpf.Toolkit.ColorItem>();

		public bool mIsSchedulerLoaderClosing = false;

		public AdvancedSchedulerMainForm(ProductSelection productSelection)
		{
			mApplicationName = ((DlkTargetApplication)productSelection.SelectedProduct).DisplayName;
			DlkEnvironment.mCurrentProductName = mApplicationName;
			Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
			Dispatcher.UnhandledException += Dispatcher_UnhandledException;

			InitializeComponent();
			Initialize();
		}

		/// <summary>
		/// Constructor for Advanced Scheduler initialized from TR MainWindow
		/// </summary>
		/// <param name="_">Do not remove this dummy parameter. This is needed to pass APP ID in TR Main Window</param>
		public AdvancedSchedulerMainForm(string _)
		{
			mApplicationName = DlkEnvironment.mCurrentProductName;
			Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
			Dispatcher.UnhandledException += Dispatcher_UnhandledException;

			InitializeComponent();
			Initialize();
		}

		/// <summary>
		/// Returns a MenuItem with the correct properties
		/// Purpose is for proper and uniform format of context menu headers
		/// </summary>
		/// <param name="menuHeader"></param>
		private MenuItem SetGroupMenu(string menuHeader)
		{
			MenuItem groupMenu = new MenuItem();
			groupMenu.Header = menuHeader;
			groupMenu.IsEnabled = false;
			groupMenu.FontWeight = FontWeights.Bold;
			groupMenu.Margin = new Thickness(-15, 0, 0, 0);

			return groupMenu;
		}

		/// <summary>
		/// Initializes context menus in Execution Queue grid
		/// </summary>
		public void InitializeBrowserLists()
		{
			mWebBrowsers = new ObservableCollection<string>();
			mRemoteBrowsers = new ObservableCollection<string>();
			mMobileBrowsers = new ObservableCollection<string>();

			foreach (DlkBrowser browser in DlkEnvironment.mAvailableBrowsers)
			{
				mWebBrowsers.Add(browser.Alias);
			}
#if DEBUG
			foreach (DlkRemoteBrowserRecord rec in DlkRemoteBrowserHandler.mRemoteBrowsers)
			{
				mRemoteBrowsers.Add(rec.Id);
			}
			foreach (DlkMobileRecord mob in DlkMobileHandler.mMobileRec)
			{
				mMobileBrowsers.Add(mob.MobileId);
			}
#endif
		}

		#region Private methods
		private void Initialize()
		{
			//share products folder
			ShareProductsFolder();

			//notify icon
			SetupNotifyIcon();

			InitializeSuitesAndTags();

			//Get latest item option check for internal and external user
#if DEBUG
			GetLatestMenuItem.Visibility = Visibility.Visible;
			btnAgentGetLatest.Visibility = Visibility.Visible;
#else
			GetLatestMenuItem.Visibility = Visibility.Collapsed;
			btnAgentGetLatest.Visibility = Visibility.Collapsed;
#endif

			//setup ui file name of default scheduler
			txtSchedulerFileName.Text = string.Format("({0})", Path.GetFileNameWithoutExtension(DlkAdvancedSchedulerFileHandler.SchedulesFilePath));

			//set initial value of current app on load
			mInitialApp = int.Parse(DlkConfigHandler.GetConfigValue("currentapp"));

			//Backward Compatibility - copy over previous schedulingagents.xml as schedulingagents.agent
			var oldSchedulingAgentFile = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "schedulingagents" + ".xml");
			var newSchedulingAgentFile = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "schedulingagents" + ".agent");

			if (File.Exists(oldSchedulingAgentFile) && !File.Exists(newSchedulingAgentFile))
			{
				File.Copy(oldSchedulingAgentFile, newSchedulingAgentFile);
				File.Delete(oldSchedulingAgentFile);
			}

			//controller presenter
			mControllerPresenter = new ControllerPresenter(this);
			mControllerPresenter.LoadLineup();
			dgTestLineup.ItemsSource = TestLineup;
			tvAllSuites.DataContext = AllSuites;
			dgLastRunResult.DataContext = TestLineup;
			dgTestHistory.ItemsSource = History;
			LoadFavoritesTree();
			ToggleTreeViewButtons();

			string[] searchType = { MENU_TEXT_SUITESONLY, MENU_TEXT_OWNERSONLY, MENU_TEXT_TAGSONLY, MENU_TEXT_SUITESANDTAGS };
			cmnuSearchTypes.Items.Clear();
			for (int countType = 0; countType < searchType.Length; countType++)
			{
				MenuItem searchItem = new MenuItem();
				searchItem.Header = searchType[countType];
				searchItem.IsCheckable = true;
				if (countType == 0)
				{
					searchItem.IsChecked = true;
				}
				searchItem.Click += SearchTypeMenuItem_Click;
				cmnuSearchTypes.Items.Add(searchItem);
			}

			//execute presenter
			mExecutePresenter = new ScheduleRunnerPresenter(this);

			//agent presenter
			mAgentPresenter = new AgentPresenter(this);
			mAgentPresenter.LoadAgents();
			dgAgentLineup.ItemsSource = AgentsPool;
			dgAgentGroups.DataContext = AgentsGroupPool;

			dgAgentLineup.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

			ToggleAgentLineupButtons();

			//testLineupStatus presenter
			mTestLineupStatusPresenter = new TestLineupStatusPresenter(this);

			//status log
			TestLineupStatus = new TestLineupStatus((context) =>
			{
				Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
				{
					StatusBar.Background = context.Color.Background;
					StatusBar.Foreground = context.Color.Foreground;
					StatusTitle.Text = context.Title;
					StatusMessage.Text = context.Message;

					if (context.Title == string.Empty) btnCloseMessage.Visibility = Visibility.Hidden;
					else btnCloseMessage.Visibility = Visibility.Visible;
				}));
			});

			var watcher = new FileSystemWatcher(DlkEnvironment.mDirConfigs);
			watcher.Changed += OnSchedulesChanged;
			watcher.Filter = "LoginConfig.xml";
			watcher.IncludeSubdirectories = true;
			watcher.EnableRaisingEvents = true;

			//register collection change listener
			mTestLineup.CollectionChanged += TestLineupRecord_CollectionChanged;

			SetLineupButtonIsEnabled();

			//if no agents in pool, set all loaded lineup agents to local machine
			if (AgentsPool.Count == 0)
				mControllerPresenter.ChangeAgentsInAllLineup(AvailableAgents.FirstOrDefault(x => x.Name == Agent.LOCAL_MACHINE));

			MonitorConfigFileDirectory();
			mLoginRecords = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords;

			//Update ColorList 
			PopulateColorList();
			InitializeBrowserLists();
			DlkEnvironment.InitializeBlacklistedURLs();
			if (DlkEnvironment.URLBlacklist != null && TestLineup.Count() > 0)
			{
				if (DlkEnvironment.URLBlacklist.First() != "")
				{
					CheckQueueEnvironmentsForBlacklisted();
					StartTestCheckLoader();
				}
			}

		}

		/// <summary>
		/// Cache suite files and tags
		/// </summary>
		private void InitializeSuitesAndTags()
		{
			try
			{
				DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirTestSuite);
				mSuiteFiles = di.GetFiles("*.xml", SearchOption.AllDirectories).ToList();
				mAllTags = DlkTag.LoadAllTags();

				Task.Factory.StartNew(() =>
				{
					Parallel.ForEach(mSuiteFiles, file =>
					{
						SuiteFile suite = new SuiteFile();
						lock (suite)
						{
							try
							{
								suite.FullName = file.FullName;
								suite.DirectoryName = file.DirectoryName;
								suite.Name = file.Name;

								using (XmlReader reader = XmlReader.Create(file.FullName))
								{
									reader.ReadToFollowing("suite");
									string owner = reader.GetAttribute("owner");

									if (!string.IsNullOrEmpty(owner))
									{
										suite.Owner = owner;
										mSuiteOwners.Add(suite);
									}

									while (reader.ReadToFollowing("tag"))
									{
										string name = reader.GetAttribute("name");
										string id = reader.GetAttribute("id");
										suite.Tags.Add(new DlkTag(id, name, "", ""));
									}
									if (suite.Tags.Count > 0)
										mSuiteTags.Add(suite);
								}
							}
							catch
							{
								//skip malformed xml file
							}
						}
					});
				});
			}
			catch (Exception e)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, e);
			}
		}

		/// <summary>
		/// Displays the suites that will satisfy the searchValue and searchOption to the ListView
		/// </summary>
		private void LoadSuiteList(string FolderPath, string searchValue = "", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			List<SuiteFile> lstSuites = new List<SuiteFile>();
			List<FileInfo> files = mSuiteFiles;
			DirectoryInfo di = new DirectoryInfo(FolderPath);
			string fileNameWithoutExtension = "";
			searchValue = searchValue.ToLower();

			if (mSearchType == KwDirItem.SearchType.TestsSuitesOnly || searchValue == "")
			{
				if (searchValue == "")
					files = mSuiteFiles.FindAll(f => f.DirectoryName == FolderPath.TrimEnd('\\'));
				else
					files = mSuiteFiles.FindAll(f => f.Name.ToLower().Contains(searchValue));
			}
			else if (mSearchType == KwDirItem.SearchType.TagsOnly)
			{
				files = mSuiteFiles.Join(mSuiteTags, suiteFiles => suiteFiles.FullName, suiteTags => suiteTags.FullName, (suiteFiles, suiteTags) => suiteFiles).ToList();
			}
			else if (mSearchType == KwDirItem.SearchType.Owner)
			{
				files = mSuiteFiles.Join(mSuiteOwners, suiteFiles => suiteFiles.FullName, suiteOwner => suiteOwner.FullName, (suiteFiles, suiteOwner) => suiteFiles).ToList();
			}

			foreach (var file in files)
			{
				bool addSuiteToTree = true;
				fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
				if (searchValue != "")
				{
					switch (mSearchType)
					{

						case KwDirItem.SearchType.TestsSuitesOnly:
							if (!fileNameWithoutExtension.ToLower().Contains(searchValue))
							{
								addSuiteToTree = false;
							}
							break;
						case KwDirItem.SearchType.TagsOnly:
							List<DlkTag> tags = mSuiteTags.Find(f => f.FullName == file.FullName).Tags;
							if (!tags.Any(x => x.Name.ToLower().Contains(searchValue) && mAllTags.Any(y => y.Name == x.Name)))
							{
								addSuiteToTree = false;
							}
							break;
						case KwDirItem.SearchType.Owner:
							string suiteOwner = mSuiteOwners.Find(f => f.FullName == file.FullName).Owner;
							if (!suiteOwner.ToLower().Contains(searchValue))
							{
								addSuiteToTree = false;
							}
							break;
						case KwDirItem.SearchType.TestsSuitesAndTags:
							tags = mSuiteTags.Find(f => f.FullName == file.FullName)?.Tags;
							if (!(fileNameWithoutExtension.ToLower().Contains(searchValue) || (tags != null && tags.Any(x => x.Name.ToLower().Contains(searchValue) && mAllTags.Any(y => y.Name == x.Name)))))
							{
								addSuiteToTree = false;
							}
							break;
						default:
							break;
					}
				}
				if (addSuiteToTree)
				{
					SuiteFile suiteToAdd = new SuiteFile { Name = fileNameWithoutExtension, FullName = file.FullName };
					lstSuites.Add(suiteToAdd);
				}
			}

			FilteredSuites = SearchForSuites.GlobalCollectionOfSuites.Join(lstSuites, suiteFiles => suiteFiles.Path, suiteOwner => suiteOwner.FullName, (suiteFiles, suiteOwner) => suiteFiles).ToList();
			tvAllSuites.DataContext = FilteredSuites;
		}

		/// <summary>
		/// Initializes loader for startup test blacklist check
		/// </summary>
		public void StartTestCheckLoader()
		{
			try
			{
				mIsInitialTestCheck = true;
				BlacklistTestCheckLoadingDialog loaderDialog = new BlacklistTestCheckLoadingDialog(this);
				mLoader = loaderDialog;
				if (loaderDialog.ShowDialog() == true)
				{
					mIsInitialTestCheck = false;
					mLoader.Close();
					if (mIsSchedulerLoaderClosing)
					{
						ExitScheduler();
					}
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// Removes blacklisted status from all scheduled suites
		/// </summary>
		public void RemoveAllBlacklistStatus()
		{
			try
			{
				List<string> SuitesToSkip = new List<string>();
				foreach (TestLineupRecord tlr in TestLineup)
				{
					if (SuitesToSkip.Any(x => x == tlr.GroupID))
					{
						continue;
					}
					tlr.NonDefaultBlacklistedEnvironment = string.Empty;
					tlr.IsBlacklisted = false;
					if (tlr.IsInGroup)
					{
						foreach (var suite in TestLineup.Where(x => x.GroupID == tlr.GroupID))
						{
							if (SuitesToSkip.Any(x => x == suite.Id))
							{
								continue;
							};
							suite.NonDefaultBlacklistedEnvironment = string.Empty;
							suite.IsBlacklisted = false;
							SuitesToSkip.Add(suite.Id);
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
		/// Checks queue's override environments for blacklisted URLs
		/// </summary>
		public void CheckQueueEnvironmentsForBlacklisted()
		{
			try
			{
				if (DlkEnvironment.URLBlacklist == null || DlkEnvironment.URLBlacklist.First() == "")
				{
					RemoveAllBlacklistStatus();
					return;
				}
				List<string> SuitesToSkip = new List<string>();
				List<DlkLoginConfigRecord> mLoginRecords = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords.ToList();
				foreach (TestLineupRecord tlr in TestLineup)
				{
					if (SuitesToSkip.Any(x => x == tlr.GroupID))
					{
						continue;
					}
					bool isBlacklisted = false;
					if (tlr.Environment != TestLineupRecord.DEFAULT_ENVIRONMENT)
					{
						if (mLoginRecords.Any(x => x.mID == tlr.Environment)) // check if environment exists
						{
							if (DlkEnvironment.URLBlacklist != null)
							{
								DlkLoginConfigHandler mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, tlr.Environment);
								isBlacklisted = DlkEnvironment.URLBlacklist.Any(x => DlkEnvironment.IsSameURL(mLoginConfigHandler.mUrl, x));
								tlr.NonDefaultBlacklistedEnvironment = string.Empty;
								tlr.IsBlacklisted = isBlacklisted;
								SuitesToSkip.Add(tlr.Id);
							}
						}
						if (tlr.IsInGroup)
						{
							foreach (var suite in TestLineup.Where(x => x.GroupID == tlr.GroupID))
							{
								if (SuitesToSkip.Any(x => x == suite.Id))
								{
									continue;
								};
								suite.NonDefaultBlacklistedEnvironment = string.Empty;
								suite.IsBlacklisted = isBlacklisted;
								SuitesToSkip.Add(suite.Id);
							}
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
		/// Checks queue's suites for non-default environments, and checks their tests for blacklisted URLs
		/// </summary>
		public void CheckSuiteDefaultsForBlacklisted()
		{
			if (DlkEnvironment.URLBlacklist == null || DlkEnvironment.URLBlacklist.First() == "")
			{
				return;
			}
			if (!mIsBlacklistBeingCheckedInBackground)
			{
				mIsBlacklistBeingCheckedInBackground = true;
				try
				{
					mCurrentSuiteCount = 0;
					if (mIsInitialTestCheck)
					{
						if (mTotalTestCount.Count() > 0)
						{
							mSuiteUnitCount = Convert.ToDouble(CHECK_TEST_COUNT_PROGRESS_PERCENTAGE) / Convert.ToDouble(mTotalTestCount.Count());
						}
						else
						{
							mLoader.mTestCheckLoaderWorker.ReportProgress(OVERALL_PROGRESS_PERCENTAGE, DlkUserMessages.INF_SCHEDULER_LOADER_NO_DEFAULT_ENVIRONMENT_IN_SUITES);
							mIsBlacklistBeingCheckedInBackground = false;
							return;
						}
					}
					foreach (var record in TestLineup)
					{
						if (record.Environment == TestLineupRecord.DEFAULT_ENVIRONMENT)
						{
							if (mIsInitialTestCheck)
							{
								mHasAddedToBlockCount = false;
								mCurrentSuiteUnitCount = 0;
								mCurrentTestCount = 0;
								if (mTotalTestCount[mCurrentSuiteCount] < 0) // test count failed
								{
									mCurrentSuiteBlockCount = mCurrentSuiteBlockCount += mSuiteUnitCount;
									bool mExceedsOverallProgress = mLoaderProgress + mSuiteUnitCount > OVERALL_PROGRESS_PERCENTAGE;
									mLoaderProgress = mExceedsOverallProgress ? OVERALL_PROGRESS_PERCENTAGE : mLoaderProgress + mSuiteUnitCount;
									mLoader.mTestCheckLoaderWorker.ReportProgress(Convert.ToInt32(mLoaderProgress), string.Format(DlkUserMessages.INF_SCHEDULER_LOADER_SUITE_TEST_COUNT_FAILURE, record.TestSuiteToRun.Name));
									Thread.Sleep(500);
									continue;
								}
								else
								{
									mTestUnitCount = mSuiteUnitCount / mTotalTestCount[mCurrentSuiteCount];
								}
							}
							mLoginRecords = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords;
							bool isBlacklisted = CheckSuiteURLsIfBlacklisted(record);
							record.IsBlacklisted = isBlacklisted;
							if (mIsInitialTestCheck)
							{
								mCurrentSuiteCount++;
							}
						}
					}
					mIsBlacklistBeingCheckedInBackground = false;
				}
				catch
				{
					mIsSchedulerLoaderClosing = true;
					mIsBlacklistBeingCheckedInBackground = false;
				}
			}
		}

		/// <summary>
		/// Counts the number of tests in default environment suites for blacklist checking
		/// </summary>
		public void CalculateTestCount()
		{
			double testCalculateUnitCount = Convert.ToDouble(CALCULATE_TEST_COUNT_PROGRESS_PERCENTAGE) / Convert.ToDouble(TestLineup.Count());
			int suiteCount = 0;
			foreach (var record in TestLineup)
			{
				suiteCount++;
				mLoader.mTestCheckLoaderWorker.ReportProgress(Convert.ToInt32(mLoaderProgress), string.Format(DlkUserMessages.INF_SCHEDULER_LOADER_SUITE_TEST_COUNT_CALCULATING, suiteCount.ToString(), TestLineup.Count().ToString()));
				if (record.Environment == TestLineupRecord.DEFAULT_ENVIRONMENT)
				{
					try
					{
						if (File.Exists(record.TestSuiteToRun.Path))
						{
							XDocument doc = XDocument.Load(record.TestSuiteToRun.Path);
							mTotalTestCount.Add(doc.Descendants("test").Count());
						}
						else
						{
							mTotalTestCount.Add(-1);
						}
						
					}
					catch
					{
						mTotalTestCount.Add(-1); // something in the suite cannot be checked, will skip in initial load
					}
				}
				mLoaderProgress += testCalculateUnitCount;
			}
			mLoaderProgress = CALCULATE_TEST_COUNT_PROGRESS_PERCENTAGE;
			mLoader.mTestCheckLoaderWorker.ReportProgress(Convert.ToInt32(mLoaderProgress), string.Format(DlkUserMessages.INF_SCHEDULER_LOADER_SUITE_COUNT_FINISHED, suiteCount.ToString(), TestLineup.Count().ToString()));
			Thread.Sleep(500);
		}

		/// <summary>
		/// Monitors the config file of Test Runner if there are changes or modifications
		/// </summary>
		private void MonitorConfigFileDirectory()
		{
			mConfigWatcher.Path = DlkConfigHandler.GetConfigRoot("Configs");
			mConfigWatcher.IncludeSubdirectories = true;
			mConfigWatcher.Filter = "config.xml";
			mConfigWatcher.EnableRaisingEvents = true;
			mConfigWatcher.Changed += ConfigFileWatcher_Changed;
		}

		private void ShareProductsFolder()
		{
			try
			{
				Process p = new Process();
				p.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SharedFolderFull.bat");
				p.StartInfo.Arguments = DlkEnvironment.mRootDir + "\\Products";
				p.StartInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				p.StartInfo.UseShellExecute = true;
				p.StartInfo.Verb = "runas"; //request admin rights
				p.Start();
				p.WaitForExit(300000);
				if (!p.HasExited)
				{
					p.Kill();
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void SetupNotifyIcon()
		{
			mNotifyIcon.Visible = true;
			mNotifyIcon.Icon = Properties.Resources.TestRunner;
			mNotifyIcon.DoubleClick += NotifyIcon_DoubleClick;
			mNotifyIcon.Text = "Scheduler";
			var contextMenu = new System.Windows.Forms.ContextMenu();
			var exitMenuItem = new System.Windows.Forms.MenuItem();
			contextMenu.MenuItems.AddRange(
					new System.Windows.Forms.MenuItem[] { exitMenuItem });
			exitMenuItem.Index = 0;
			exitMenuItem.Text = "Exit";
			exitMenuItem.Click += new System.EventHandler(ExitMenuItem_Click);
			mNotifyIcon.ContextMenu = contextMenu;
		}

		private void QueueSelectedTreeItem(KwDirItem suite)
		{
			AddToTestLineup(new TestSuite()
			{
				Name = suite.Name,
				Path = suite.Path,
				Description = "DUMMY"
			});
		}

		private void IterateFolder(BFFolder folder, Action<KwDirItem> method)
		{
			foreach (var dirItem in folder.DirItems)
			{
				if (dirItem != null && dirItem is BFFile)
				{
					method(dirItem);
				}
				else if (dirItem != null && dirItem is BFFolder)
				{
					var innerFolder = dirItem as BFFolder;
					IterateFolder(innerFolder, QueueSelectedTreeItem);
				}
			}
		}

		private void AddToTestLineup(TestSuite TestToAdd)
		{
			var agent = AvailableAgents.FirstOrDefault(x => x.Name == Agent.ANY_AGENT_NAME);
			if (agent == null)
				agent = AvailableAgents.FirstOrDefault();

			mControllerPresenter.AddToLineup(TestToAdd, null, mApplicationName, agent);
		}

		/// <summary>
		/// Get selected suite's group members to carry condition
		/// </summary>
		private List<TestLineupRecord> GetConditionGroupLineup(string GroupID)
		{
			List<TestLineupRecord> ret = mTestLineup.ToList().FindAll(x => x.GroupID == GroupID);
			return ret;
		}

		/// <summary>
		/// Pause background workers, try saving last changes of test lineup if needed
		/// </summary>
		private void PauseSchedulerWorker()
		{
			try
			{
				//pause background workers
				_busy.Reset();

				//this is to make sure we have no conflict in saving
				if (!IsSaveInProgress)
				{
					this.mControllerPresenter.SaveLineup();
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}


		/// <summary>
		/// Add item inside favorites tree
		/// </summary>
		/// <param name="draggedItem"></param>
		private void AddToFavoritesTree(KwDirItem draggedItem)
		{
			// kwdiritem should work because bffile/bffolder is substitutable for kwdiritem due to inheritance, this way, we can support folder drag and drop for bffile and bffolder.
			Favorites.Add(draggedItem);
			// line below sorts by type (folders first) then by name
			Favorites.Sort(observableCollection => observableCollection.OrderBy(item => item.GetType() == typeof(BFFile)).ThenBy(item => item.Name)); 
		}

		/// <summary>
		/// Add draggedItem inside targetFolder
		/// </summary>
		/// <param name="targetFolder"></param>
		/// <param name="draggedItem"></param>
		private void AddItemToFolderInFavoritesTree(BFFolder targetFolder, KwDirItem draggedItem)
		{
			targetFolder.DirItems.Add(draggedItem);
			// this will maintain a sorted list
			targetFolder.DirItems = targetFolder.DirItems.OrderBy(item => item.GetType() == typeof(BFFile)).ThenBy(item => item.Name).ToList(); 
		}

		/// <summary>
		/// Remove item from sourceFolder
		/// </summary>
		/// <param name="sourceFolder"></param>
		/// <param name="draggedItem"></param>
		private void RemoveFromFavoritesTree(BFFolder sourceFolder, KwDirItem draggedItem)
		{
			KwDirItem itemToRemove;

			if (!string.IsNullOrEmpty(sourceFolder.Path))
			{
				if (draggedItem is BFFolder)
				{
					//check equality by path and diritems
					itemToRemove = sourceFolder.DirItems.FirstOrDefault(x => x.Path == draggedItem.Path && 
									x is BFFolder &&
									string.Join(",", ((BFFolder)x).DirItems.Select(y => y.Name)) == string.Join(",", ((BFFolder)draggedItem).DirItems.Select(y => y.Name)));
				}
				else
				{
					itemToRemove = sourceFolder.DirItems.FirstOrDefault(x => x.Path == draggedItem.Path);
				}

				if (itemToRemove != null)
					sourceFolder.DirItems.Remove(itemToRemove);
			}
			else
			{
				if (draggedItem is BFFolder)
				{
					//check equality by path and diritems
					itemToRemove = Favorites.FirstOrDefault(x => x.Path == draggedItem.Path && 
									x is BFFolder &&
									string.Join(",", ((BFFolder)x).DirItems.Select(y => y.Name)) == string.Join(",", ((BFFolder)draggedItem).DirItems.Select(y => y.Name)));
				}
				else
				{
					itemToRemove = Favorites.FirstOrDefault(x => x.Path == draggedItem.Path);
				}

				if (itemToRemove != null)
					Favorites.Remove(itemToRemove);
			}
		}

		/// <summary>
		/// Check if destinationFolder is located within draggedItem
		/// </summary>
		/// <param name="draggedItem"></param>
		/// <param name="destinationFolder"></param>
		/// <returns></returns>
		private bool IsDestinationWithinDraggedItem(KwDirItem draggedItem, BFFolder destinationFolder)
		{
			if (draggedItem is BFFolder)
			{
				BFFolder draggedFolder = draggedItem as BFFolder;
				//check equality by path and diritems
				if (draggedFolder.Path == destinationFolder.Path && string.Join(",", draggedFolder.DirItems.Select(x => x.Name)) == string.Join(",", destinationFolder.DirItems.Select(x => x.Name)))
				{
					return true;
				}

				foreach (var item in draggedFolder.DirItems)
				{
					if (item is BFFolder)
					{
						var currentFolder = item as BFFolder;
						if (IsDestinationWithinDraggedItem(currentFolder, destinationFolder))
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Deep copy of KwDirItem to create new instance of the directory
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private KwDirItem CopyKwDirItem(KwDirItem item)
		{
			if (item is BFFile)
			{
				BFFile copyItem = new BFFile();
				copyItem.Name = item.Name;
				copyItem.Path = item.Path;

				return copyItem;
			}
			else
			{
				BFFolder itemAsFoler = item as BFFolder;
				BFFolder copyItem = new BFFolder();
				copyItem.Name = item.Name;
				copyItem.Path = item.Path;
				List<KwDirItem> dirItemsList = new List<KwDirItem>();
				foreach (var dirItem in itemAsFoler.DirItems)
				{
					dirItemsList.Add(CopyKwDirItem(dirItem));
				}
				copyItem.DirItems = dirItemsList;
				copyItem.IsExpanded = itemAsFoler.IsExpanded;

				return copyItem;
			}
		}

		/// <summary>
		/// Gets the immediate TreeViewItem parent of the interacted TreeView item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private ItemsControl GetTreeViewItemParent(TreeViewItem item)
		{
			DependencyObject parent = VisualTreeHelper.GetParent(item);

			if (parent != null)
			{
				while (!(parent is TreeViewItem || parent is TreeView))
				{
					parent = VisualTreeHelper.GetParent(parent);
				}
			}

			return parent as ItemsControl;
		}

		/// <summary>
		/// Copy fields from one record to another
		/// </summary>
		private void CopyRecordFields(TestLineupRecord originalRecord, TestLineupRecord record)
		{
			IAgentList agent = AvailableAgents.FirstOrDefault(x => x != null && x.Name == originalRecord.RunningAgent.Name);
			if (agent == null)
				agent = AgentsGroupPool.FirstOrDefault(x => x.Name == originalRecord.RunningAgent.Name);

			record.RunningAgent = agent;
			record.Browser = originalRecord.Browser;
			record.StartTime = originalRecord.StartTime;
			record.Recurrence = originalRecord.Recurrence;
			record.Schedule = originalRecord.Schedule;
			record.Enabled = originalRecord.Enabled;
			record.NonDefaultBlacklistedEnvironment = string.Empty;
			record.IsBlacklisted = originalRecord.Environment != TestLineupRecord.DEFAULT_ENVIRONMENT ? originalRecord.IsBlacklisted : false;
		}

		/// <summary>
		/// Create new group from selected lineup
		/// </summary>
		private void CreateGroupingForSelectedLineUp(IEnumerable<TestLineupRecord> selectedLineups)
		{
			if (selectedLineups == null || selectedLineups.Count() == 0)
				return;

			var orderedSelectedItems = selectedLineups.OrderBy(x => TestLineup.IndexOf(x));
			var groupId = orderedSelectedItems.First().Id;
			var firstItemIndex = TestLineup.IndexOf(orderedSelectedItems.First());
			int counter = 1;
			//remove and insert items below the first in group - set items' group id
			foreach (var record in orderedSelectedItems)
			{
				record.GroupID = groupId;
				//copy group header fields
				CopyRecordFields(orderedSelectedItems.First(), record);

				if (firstItemIndex < TestLineup.IndexOf(record))
				{
					TestLineup.Remove(record);
					TestLineup.Insert(firstItemIndex + counter++, record);
				}
			}
		}

		/// <summary>
		/// Merge ungrouped items into group
		/// </summary>
		/// <param name="groupId"></param>
		private void AddIntoGrouping(string groupId)
		{
			var selectedLineups = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();

			if (selectedLineups == null || selectedLineups.Count == 0)
				return;

			var orderedSelectedItems = selectedLineups.Where(x => !x.IsInGroup).OrderBy(x => TestLineup.IndexOf(x));
			var lastItemIndex = TestLineup.IndexOf(TestLineup.Last(x => x.GroupID == groupId));
			int counter = 1;
			var groupHeader = TestLineup.First(x => x.GroupID == groupId);
			//remove and insert items below the last in group - set items' group id
			foreach (var record in orderedSelectedItems)
			{
				record.GroupID = groupId;
				//copy group header fields
				CopyRecordFields(groupHeader, record);

				if (TestLineup.IndexOf(record) < lastItemIndex)
				{
					//item is before the group - removing it will reduce index by 1
					counter -= 1;
				}

				TestLineup.Remove(record);
				TestLineup.Insert(lastItemIndex + counter++, record);
			}
		}

		/// <summary>
		/// Ungroup all items with the same group id
		/// </summary>
		private void UngroupGrouping(IEnumerable<TestLineupRecord> selectedLineups)
		{
			if (selectedLineups == null || selectedLineups.Count() == 0)
				return;

			var groupIds = selectedLineups.Select(x => x.GroupID).Distinct().ToList();
			//remove all group ids same with all the groups ids of selected items
			foreach (var record in TestLineup.Where(x => groupIds.Contains(x.GroupID)))
			{
				record.GroupID = string.Empty;
				record.Execute = true;
				record.Dependent = "false";
				record.ExecuteDependencyResult = "";
				record.ExecuteDependencySuiteRow = "";
			}
		}

		/// <summary>
		/// Replace item as the group head
		/// </summary>
		/// <param name="record"></param>
		private void SetAsGroupHead(TestLineupRecord record)
		{
			var currentIndex = TestLineup.IndexOf(record);
			var oldGroupHeadIndex = TestLineup.IndexOf(TestLineup.First(x => x.GroupID == record.GroupID));
			var oldGroupHead = TestLineup[oldGroupHeadIndex];

			//try to change pending item in presenter - if it fails, do not continue set
			if (!TryReplacePendingSchedules(oldGroupHead, record))
			{
				return;
			}

			if (currentIndex > oldGroupHeadIndex)
			{
				//swap fields in selected item and in group header
				var temp = new TestLineupRecord();
				CopyRecordFields(record, temp);
				CopyRecordFields(oldGroupHead, record);
				CopyRecordFields(temp, oldGroupHead);
				//swap execution condition properties
				if (record.Dependent == "True")
				{
					oldGroupHead.Dependent = "True";
					oldGroupHead.Execute = record.Execute;
					oldGroupHead.ExecuteDependencySuiteRow = record.ExecuteDependencySuiteRow;
					oldGroupHead.ExecuteDependencyResult = record.ExecuteDependencyResult;
					record.Dependent = "false";
					record.Execute = true;
					record.ExecuteDependencySuiteRow = "";
					record.ExecuteDependencyResult = "";
				}
				//set new group id and update the group
				record.GroupID = record.Id;
				foreach (var suite in TestLineup.Where(x => x.GroupID == oldGroupHead.GroupID).ToList())
				{
					suite.GroupID = record.GroupID;
				}
				//update image
				var tempImage = oldGroupHead.GroupImage;
				oldGroupHead.GroupImage = record.GroupImage;
				record.GroupImage = tempImage;
				//swap position
				TestLineup.Move(currentIndex, oldGroupHeadIndex);
				TestLineup.Remove(oldGroupHead);
				TestLineup.Insert(currentIndex, oldGroupHead);
			}
		}

		/// <summary>
		/// update all individual or all items in group - if current item or any item in group is running, disable them
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		private void UpdateRecordIsDisabled(TestLineupRecord record)
		{
			if (mIsSchedulerFileReadonly)
				return;

			if(record.IsInGroup)
			{
				bool recordGroupIsRunning = TestLineup.Where(x => x.GroupID == record.GroupID).Any(x => x.Status == Enumerations.TestStatus.Pending || 
																										x.Status == Enumerations.TestStatus.Running ||
																										x.Status == Enumerations.TestStatus.Cancelling);
				//update all items in group
				foreach (var item in TestLineup.Where(x => x.GroupID == record.GroupID))
				{
					item.IsEnabled = !recordGroupIsRunning;
				}
			}
			else
			{
				//update individual item 
				record.IsEnabled = record.Status != Enumerations.TestStatus.Pending && 
									record.Status != Enumerations.TestStatus.Running &&
									record.Status != Enumerations.TestStatus.Cancelling;
			}
		}

		/// <summary>
		/// Refresh test lineup isenabled of each individual rows
		/// </summary>
		private void RefreshTestLineupIsDisabled()
		{
			foreach (var item in TestLineup)
			{
				item.IsEnabled = !mIsSchedulerFileReadonly;
			}
		}

		/// <summary>
		/// Check if any item in group is currently running
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		private bool IsAnyItemInGroupRunning(TestLineupRecord record, bool includePending = true)
		{
			bool recordGroupIsRunning = false;
			if (record.IsInGroup)
			{
				if (includePending)
				{
					recordGroupIsRunning = TestLineup.Where(x => x.GroupID == record.GroupID).Any(x => x.Status == Enumerations.TestStatus.Pending || x.Status == Enumerations.TestStatus.Running ||
																										x.Status == Enumerations.TestStatus.Cancelling);
				}
				else
				{
					recordGroupIsRunning = TestLineup.Where(x => x.GroupID == record.GroupID).Any(x => x.Status == Enumerations.TestStatus.Running || x.Status == Enumerations.TestStatus.Cancelling);
				}
			}

			return recordGroupIsRunning;
		}

		/// <summary>
		/// Move group into a new position
		/// </summary>
		/// <param name="groupId"></param>
		/// <param name="newPosition"></param>
		private void MoveGroupPosition(string groupId, int newPosition)
		{
			var currentPosition = TestLineup.IndexOf(TestLineup.First(x => x.GroupID == groupId));

			//try to move pending items in presenter - if it fails, do not continue move
			if (!TrySwitchPendingSchedules(TestLineup[currentPosition], TestLineup[newPosition]))
			{
				return;
			}

			int counter = 0;
			foreach (var record in TestLineup.Where(x => x.GroupID == groupId).ToList())
			{
				TestLineup.Move(currentPosition + counter, newPosition + counter);

				if (newPosition < currentPosition)
					counter++;
			}
		}

		/// <summary>
		/// Set IsEnabled property of lineup buttons
		/// </summary>
		private void SetLineupButtonIsEnabled()
		{
			var isEnabled = dgTestLineup != null && dgTestLineup.SelectedItems.Count > 0
							&& dgTestLineup.Visibility == Visibility.Visible &&
							!mIsSchedulerFileReadonly && TestLineup.Any();

			btnLineupUp.IsEnabled = isEnabled;
			btnLineupDown.IsEnabled = isEnabled;
			btnLineupEdit.IsEnabled = isEnabled && (dgTestLineup.SelectedItems.Count > 0 && !dgTestLineup.SelectedItems.Cast<TestLineupRecord>().Any(test => test.IsDifferentProduct));
			btnLineupDelete.IsEnabled = isEnabled;
		}

		/// <summary>
		/// Check if schedule file is readonly - if yes, prompt user for actions
		/// </summary>
		private void CheckIfSchedulerIsReadonly()
		{
			if (mControllerPresenter.IsSchedulesFilePathReadOnly())
			{
				if (DlkUserMessages.ShowQuestionYesNoWarning(this, DlkUserMessages.ASK_ADVANCED_SCHEDULER_OVERRIDE_READONLY, "Schedule file is readonly") == MessageBoxResult.Yes)
				{
					mControllerPresenter.SetSchedulesFileReadOnly(false);
					mIsSchedulerFileReadonly = false;
				}
				else
				{
					mIsSchedulerFileReadonly = true;
				}
			}
			else
			{
				mIsSchedulerFileReadonly = false;
			}

			RefreshTestLineupIsDisabled();
			SetLineupButtonIsEnabled();
		}

		/// <summary>
		/// Try to switch order of the the items from presenter's scheudling queue
		/// </summary>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <returns></returns>
		private bool TrySwitchPendingSchedules(TestLineupRecord item1, TestLineupRecord item2)
		{
			bool success = true;

			if (item1.Status == Enumerations.TestStatus.Pending && item2.Status == Enumerations.TestStatus.Pending)
			{
				success = mExecutePresenter.TrySwitchPendingSchedules(item1.Id, item2.Id);
			}

			return success;
		}

		/// <summary>
		/// Try to replace an item in queue with another item
		/// </summary>
		/// <param name="itemToRemove"></param>
		/// <param name="itemToChangeWith"></param>
		/// <returns></returns>
		private bool TryReplacePendingSchedules(TestLineupRecord itemToRemove, TestLineupRecord itemToChangeWith)
		{
			bool success = true;

			if (itemToRemove.Status == Enumerations.TestStatus.Pending && 
				itemToChangeWith.Status == Enumerations.TestStatus.Pending &&
				itemToRemove.GroupID == itemToRemove.Id &&
				itemToRemove.GroupID == itemToChangeWith.GroupID)
			{
				success = mExecutePresenter.TryReplacePendingSchedules(itemToRemove.Id, itemToChangeWith.Id);
			}

			return success;
		}

		/// <summary>
		/// Select current tab with index
		/// </summary>
		/// <param name="index"></param>
		private void SelectTab(int index)
		{
			if (tabMain != null)
			{
				tabMain.SelectedIndex = index;
			}
		}

		/// <summary>
		/// Filter agent grid based on selected group
		/// </summary>
		/// <param name="selectedGroup"></param>
		/// <param name="isEditMode"></param>
		private void FilterAgentGrid(AgentGroup selectedGroup, bool isEditMode)
		{
			mAgentsFilteredPool = new ObservableCollection<Agent>();
			if (isEditMode)
			{
				foreach (var agent in AgentsPool)
				{
					agent.IsInGroup = selectedGroup.Members.Contains(agent.Name);
					mAgentsFilteredPool.Add(agent);
				}
			}
			else
			{
				foreach (var agentName in selectedGroup.Members)
				{
					mAgentsFilteredPool.Add(AgentsPool.FirstOrDefault(x => x.Name == agentName));
				}
			}

			dgAgentLineup.ItemsSource = mAgentsFilteredPool;
		}

		/// <summary>
		/// Agent group name validations
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private bool ValidateAgentGroupName(string name)
		{
			var cleanName = name.ToLower();

			if (string.IsNullOrEmpty(cleanName))
			{
				DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_EMPTY_AGENT_GROUP_NAME, "Agent Group Name Error");
				return false;
			}

			if (!DlkString.IsAgentNameValid(cleanName))
			{
				DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_INVALID_AGENT_GROUP_NAME, "Agent Group Name Error");
				return false;
			}

			if (mAgentsGroupPool.Any(x => x.Name.ToLower() == cleanName))
			{
				DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_SCHEDULER_DUPLICATE_AGENT_GROUP_NAME, name.ToUpper()), "Agent Group Name Error");
				return false;
			}

			if (mAgentsPool.Any(x => x.Name.ToLower() == cleanName) ||
				cleanName.Equals(Agent.LOCAL_MACHINE.ToLower()) ||
				cleanName.Equals(Agent.ANY_AGENT_NAME.ToLower()))
			{
				DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_SCHEDULER_CONFLICT_AGENT_GROUP_NAME, name.ToUpper()), "Agent Group Name Error");
				return false;
			}

			return true;
		}

		private void SetupSearchSuites(SearchDelegateType searchDelegateType)
		{
			int typeLettersInSuccessionDelay = 2500;
			//timer where filtering occurs only ~3 seconds after the user pressed a key.
			if (mTxtSearchSuitesTimer != null)
			{
				if (mTxtSearchSuitesTimer.Interval < typeLettersInSuccessionDelay)
				{
					//increment interval every time the user is typing.
					mTxtSearchSuitesTimer.Interval += typeLettersInSuccessionDelay;
				}
			}
			else
			{
				mTxtSearchSuitesTimer = new System.Windows.Forms.Timer();
				mTxtSearchSuitesTimer.Tick += new EventHandler(searchDelegateType);
				mTxtSearchSuitesTimer.Interval = typeLettersInSuccessionDelay;
				mTxtSearchSuitesTimer.Start();
			}
		}

		private void SetupSearchFavorites(SearchDelegateType searchDelegateType)
		{
			int typeLettersInSuccessionDelay = 2500;
			//timer where filtering occurs only ~3 seconds after the user pressed a key.
			if (mTxtSearchFavoritesTimer != null)
			{
				if (mTxtSearchFavoritesTimer.Interval < typeLettersInSuccessionDelay)
				{
					//increment interval every time the user is typing.
					mTxtSearchFavoritesTimer.Interval += typeLettersInSuccessionDelay;
				}
			}
			else
			{
				mTxtSearchFavoritesTimer = new System.Windows.Forms.Timer();
				mTxtSearchFavoritesTimer.Tick += new EventHandler(searchDelegateType);
				mTxtSearchFavoritesTimer.Interval = typeLettersInSuccessionDelay;
				mTxtSearchFavoritesTimer.Start();
			}
		}

		public void SearchSuites(String filterString)
		{
			filterString = filterString.ToLower();

			if (string.IsNullOrEmpty(filterString))
			{
				FilteredSuites.Clear();
				mSearchSuitesPreviousFilter = "";
			}
			else
			{
				if (FilteredSuites.Count > 0 && 
					filterString.Length > mSearchSuitesPreviousFilter.Length && 
					filterString.Contains(mSearchSuitesPreviousFilter))
				{
					FilteredSuites = FilteredSuites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
				}
				else
				{
					FilteredSuites = SearchForSuites.GlobalCollectionOfSuites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
				}

				mSearchSuitesPreviousFilter = filterString;
			}
			}

		public void SearchFavorites(String filterString)
		{
			filterString = filterString.ToLower();

			if (string.IsNullOrEmpty(filterString))
			{
				FilteredFavorites.Clear();
				mSearchSuitesPreviousFilter = "";
			}
			else
			{
				txtSearchFavorites.Background = DlkString.IsSearchValid(filterString) ? Brushes.LemonChiffon : Brushes.LightPink;
				if (FilteredFavorites.Count > 0 && 
					filterString.Length > mSearchSuitesPreviousFilter.Length && 
					filterString.Contains(mSearchSuitesPreviousFilter))
				{
					FilteredFavorites = FilteredFavorites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
				}
				else
				{
					FilteredFavorites = SearchForSuites.GlobalCollectionOfFavorites.FindAll(s => s.Name.ToLower().Contains(filterString) && s is BFFile).AsParallel().ToList();
				}

				mSearchSuitesPreviousFilter = filterString;
			}
		}

		/// <summary>
		/// Display results details of selected History/Agent History item/s
		/// </summary>
		private void ViewHistoryResults(DataGrid Source)
		{
			foreach (ExecutionHistory result in (Source as DataGrid).SelectedItems)
			{
				ResultsDetails resultsDetails = new ResultsDetails(result.SuiteFullPath, result.ResultsFolderFullPath,
					result.Parent is TestLineupRecord ? string.Join(";", ((TestLineupRecord)result.Parent).DistributionList.ToArray())
					: string.Empty);
				resultsDetails.Show();
			}
		}

		/// <summary>
		/// Exits the scheduler - prompts the user before exiting
		/// </summary>
		private void ExitScheduler()
		{
			//disallow other instances of exit scheduler
			if (mIsClosing)
				return;

			mIsClosing = true;
			var runningSuites = TestLineup.Where(x => x.Status == Enumerations.TestStatus.Running ||
											  x.Status == Enumerations.TestStatus.Pending ||
											  x.Status == Enumerations.TestStatus.Cancelling)
							 .Select(x => x.TestSuiteToRun.Name);

			//Check if existing dialogs are opened
			foreach (Window win in Application.Current.Windows)
			{
				string windowType = win.GetType().ToString();
				switch (windowType)
				{
					case "TestRunner.AdvancedScheduler.ScheduleLogDetails":
						if (DlkUserMessages.ShowQuestionYesNoWarning(this, DlkUserMessages.ASK_SCHEDULER_QUIT_VIEW_LOGS) == MessageBoxResult.No)
						{
							mIsClosing = false;
							return;
						}
						else
							win.Close();
						break;
					case "TestRunner.AdvancedScheduler.ResultsDetails":
						if (DlkUserMessages.ShowQuestionYesNoWarning(this, DlkUserMessages.ASK_SCHEDULER_QUIT_RESULT_DETAILS) == MessageBoxResult.No)
						{
							mIsClosing = false;
							return;
						}
						else
							win.Close();
						break;
					default:
						break;

				}
			}

			string warningMessage = DlkUserMessages.ASK_ADVANCED_SCHEDULER_EXIT;
			//if there are running suites, show it in the warning message
			if (runningSuites.Count() > 0)
			{
				warningMessage = string.Format(DlkUserMessages.ASK_ADVANCED_SCHEDULER_EXIT_WARNING, runningSuites.Count(), string.Join("\n", runningSuites));
			}

			if (mIsSchedulerLoaderClosing || DlkUserMessages.ShowQuestionYesNoWarning(this, warningMessage) == MessageBoxResult.Yes)
			{
				mIsExplicitClose = true;

				if(!DlkEnvironment.ProcessExists("TestRunner"))
				{
					Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
					if (chromeDriverProcesses != null)
					{
						string[] processString = chromeDriverProcesses.Select(x => x.ProcessName).ToArray();
						DlkEnvironment.KillProcesses(processString);
					}
				}

				Process.GetCurrentProcess().Kill();
			}
			mIsClosing = false;
		}

		private void RefreshSuites()
		{
			SearchForSuites.GlobalCollectionOfSuites = new List<KwDirItem>();
			mAllTestSuites = new DlkTestSuiteLoader().GetSuiteDirectories2(DlkEnvironment.mDirTestSuite, string.Empty, null);
			tvAllSuites.DataContext = AllSuites;
			//reset search string
			txtSearchSuite.Text = String.Empty;
			mSuiteOwners.Clear();
			mSuiteTags.Clear();
			InitializeSuitesAndTags();
		}

		private void GetSelectedItems(KwDirItem dirItem, BFFolder dragSourceFolder, TreeViewItem tvi, ref List<TreeViewSelectedItem> mTreeItemSelectionList)
		{
			try
			{
				if (IsCtrlPressed)
				{
					if (!AddToSelectionList(tvi, dragSourceFolder, dirItem, ref mTreeItemSelectionList))
					{
						if (mTreeItemSelectionList.Count > 0)
						{
							tvi.IsSelected = false;
						}
					}
				}
				else
				{
					ClearSelectionList(ref mTreeItemSelectionList);
					AddToSelectionList(tvi, dragSourceFolder, dirItem, ref mTreeItemSelectionList);
				}

				ToggleTreeViewButtons();
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private bool AddToSelectionList(TreeViewItem ItemToAdd, BFFolder dragSourceFolder, KwDirItem dirItem, ref List<TreeViewSelectedItem> mTreeItemSelectionList)
		{
			bool ret = false;
			if (!mTreeItemSelectionList.Any(x => x.dirItem.Path.Equals(dirItem.Path)))
			{
				mTreeItemSelectionList.Add(new TreeViewSelectedItem
				{
					tvi = ItemToAdd,
					dragSourceFolder = dragSourceFolder,
					dirItem = dirItem
				});
				/* style item to simulate selected item look */
				ItemToAdd.Background = Brushes.DodgerBlue;
				ItemToAdd.Foreground = Brushes.White;
				ret = true;
			}
			else /* remove if already there */
			{
				if (mTreeItemSelectionList.Count == 1)
				{
					ret = true;
				}
				else
				{
					TreeViewSelectedItem item = mTreeItemSelectionList.Where(x => x.tvi.Equals(ItemToAdd)).First();
					mTreeItemSelectionList.Remove(item);
					/* style item to simulate selected item look */
					ItemToAdd.Background = Brushes.White;
					ItemToAdd.Foreground = Brushes.Black;
				}
			}

			return ret;
		}

		private void ClearSelectionList(ref List<TreeViewSelectedItem> mTreeItemSelectionList)
		{
			foreach (var kvp in mTreeItemSelectionList)
			{
				/* revert styling of every item in list */
				kvp.tvi.Background = Brushes.White;
				kvp.tvi.Foreground = Brushes.Black;
			}
			mTreeItemSelectionList.Clear();
		}

		private bool IsCtrlPressed
		{
			get
			{
				return System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl)
					|| System.Windows.Input.Keyboard.IsKeyDown(Key.RightCtrl);
			}
		}

		private bool CheckFavoritesItemExist(string itemName, TreeViewItem selectedItem = null, bool includeFiles = true)
		{
			itemName = itemName.ToLower();
			List<KwDirItem> dirItem = new List<KwDirItem>();

			if ((selectedItem != null) && (selectedItem.Header as BFFolder != null))
			{
				var folder = selectedItem.Header as BFFolder;
				dirItem = folder.DirItems;
			}
			else if ((selectedItem != null) && (selectedItem.Header as BFFile != null))
			{
				ItemsControl parent = GetTreeViewItemParent(selectedItem);
				if (parent is TreeViewItem)
				{
					var folder = (parent as TreeViewItem).Header as BFFolder;
					dirItem = folder.DirItems;
				}
				else
				{
					dirItem = Favorites.ToList();
				}
			}
			else
			{
				dirItem = Favorites.ToList();
			}

			foreach (var item in dirItem)
			{
				if (item is BFFolder)
				{
					if (item.Name.ToLower() == itemName)
					{
						return true;
					}
				}
				else if (includeFiles && item is BFFile)
				{
					if (item.Name.ToLower() == itemName)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Create available custom colors for ColorPicker 
		/// </summary>
		private void PopulateColorList()
		{
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.White, "White"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.LightGray, "Light Gray"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.LightCoral, "Light Coral"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.PeachPuff, "Peach Puff"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.SandyBrown, "Sandy Brown"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.LightSalmon, "Light Salmon"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.Orange, "Orange"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.Khaki, "Khaki"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.Yellow, "Yellow"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.OliveDrab, "Olive Drab"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.LightGreen, "Light Green"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.Aquamarine, "Aquamarine"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.PaleTurquoise, "Pale Turquoise"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.LightBlue, "Light Blue"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.RoyalBlue, "Royal Blue"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.SteelBlue, "Steel Blue"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.MediumOrchid, "Medium Orchid"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.Plum, "Plum"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.HotPink, "Hot Pink"));
			ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.LightPink, "Light Pink"));
			ColorPickerMenu.AvailableColors = ColorList;
		}

		/// <summary>
		/// Open Suite Editor Form using the given suite path
		/// </summary>
		/// <param name="suitePath">File path of the suite to be opened</param>
		private void OpenSelectedSuite(string suitePath)
		{
			if (string.IsNullOrEmpty(suitePath))
			{
				DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_FILE_TO_OPEN);
				return;
			}

			//show suite editor
			SuiteEditorForm suiteEditorForm;

			bool showSetupMessage = KeywordLoaderSingleton.Instance.IsKeywordDirectoriesNull();
			if (showSetupMessage)
			{
				Modal modalBox = new Modal(this, "Setting up suite editor...");
				modalBox.Show();
				suiteEditorForm = new SuiteEditorForm();
				modalBox.Close();
			}
			else
			{
				suiteEditorForm = new SuiteEditorForm();
			}

			suiteEditorForm.Owner = this;
			suiteEditorForm.LoadedSuite = suitePath;
			suiteEditorForm.ShowDialog();
		}

		/// <summary>
		/// Loads favorites tree for the current product
		/// </summary>
		private void LoadFavoritesTree()
		{
			mControllerPresenter.LoadFavoritesTree();
			tvFavorites.DataContext = Favorites;
		}

		/// <summary>
		/// Toggle buttons on Favorites and Suites Tree View
		/// </summary>
		private void ToggleTreeViewButtons()
		{
			//Favorites
			if (mTreeItemSelectionList_Favorites.Count == 1 && tvFavorites.SelectedItem != null)
			{
				btn_RenameFavoritesFolder.IsEnabled = mTreeItemSelectionList_Favorites[0].dirItem is BFFolder ? true : false;
				btn_EditItem.IsEnabled = mTreeItemSelectionList_Favorites[0].dirItem is BFFolder ? false : true;
				btn_DeleteItem.IsEnabled = true;
			}
			else if (mTreeItemSelectionList_Favorites.Count > 0 && tvFavorites.SelectedItem != null)
			{
				btn_RenameFavoritesFolder.IsEnabled = false;
				btn_EditItem.IsEnabled = false;
				btn_DeleteItem.IsEnabled = true;
			}
			else
			{
				btn_RenameFavoritesFolder.IsEnabled = false;
				btn_EditItem.IsEnabled = false;
				btn_DeleteItem.IsEnabled = false; 
			}

			//Suites
			if (mTreeItemSelectionList_AllSuites.Count == 1)
			{
				btnEditSuite.IsEnabled = mTreeItemSelectionList_AllSuites[0].dirItem is BFFolder ? false : true;
			}
			else
			{
				btnEditSuite.IsEnabled = false;
			}
		}

		/// <summary>
		/// Toggle buttons on Agent Lineup
		/// </summary>
		private void ToggleAgentLineupButtons()
		{
			bool isEnabled = dgAgentLineup.Items.Count != 0 && dgAgentLineup.SelectedItems.Count > 0;
			btnAgentDelete.IsEnabled = isEnabled;
			btnAgentEnable.IsEnabled = isEnabled;
			btnAgentGetLatest.IsEnabled = isEnabled;
		}

		/// <summary>
		/// Checks whether entered dates on Filter date fields are valid or not
		/// </summary>
		private void ValidateDateEntries(bool IsFromField, bool IsControllerField)
		{
			string datePickerText = IsControllerField ? (IsFromField ? mDateControllerFromOriginalString : mDateControllerToOriginalString) : (IsFromField ? mDateAgentFromOriginalString : mDateAgentToOriginalString);
			if (!String.IsNullOrEmpty(datePickerText))
			{
				DateTime dateToCheck;
				bool hasErrors = false;
				CultureInfo cultureInfo = CultureInfo.InvariantCulture;
				List<string> validDateFormats = new List<string>();
				validDateFormats.Add("M/dd/yyyy");
				validDateFormats.Add("MM/dd/yyyy");
				validDateFormats.Add("M/d/yyyy");
				if (DateTime.TryParseExact(datePickerText, validDateFormats.ToArray(), cultureInfo, DateTimeStyles.AssumeLocal, out dateToCheck))
				{
					if (dateToCheck > DateTime.Today)
					{
						DlkUserMessages.ShowWarning(DlkUserMessages.WRN_DATE_GREATER_THAN_CURRENT_DATE);
						hasErrors = true;
					}
				}
				else
				{
					if (DateTime.TryParse(datePickerText, out DateTime dateOutput))
					{
						DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_FILTER_DATE_INVALID_FORMAT);
						hasErrors = true;
					}
					
				}
				if (hasErrors)
				{
					if (IsFromField)
					{
						if (IsControllerField)
						{
							dtControllerHistoryFrom.Text = "";
							mDateControllerFromOriginalString = "";
						}
						else
						{
							dtAgentHistoryFrom.Text = "";
							mDateAgentFromOriginalString = "";
						}
					}
					else
					{
						if (IsControllerField)
						{
							dtControllerHistoryTo.Text = "";
							mDateControllerToOriginalString = "";
						}
						else
						{
							dtAgentHistoryTo.Text = "";
							mDateAgentToOriginalString = "";
						}
					}
					return;
				}
				if (!String.IsNullOrEmpty(IsControllerField ? (IsFromField ? dtControllerHistoryTo.Text : dtControllerHistoryFrom.Text) : (IsFromField ? dtAgentHistoryTo.Text : dtAgentHistoryFrom.Text)))
				{
					if (IsControllerField)
					{
						GetControllerSuiteHistory();
						if (!mControllerPresenter.FilterControllerHistory(dtControllerHistoryFrom.Text, dtControllerHistoryTo.Text))
						{
							DlkUserMessages.ShowWarning(DlkUserMessages.INF_OVERLAP_DATE);
							dtControllerHistoryFrom.Text = "";
							dtControllerHistoryTo.Text = "";
							mDateControllerFromOriginalString = "";
							mDateControllerToOriginalString = "";
						}
					}
					else
					{
						GetAgentHistory();
					}
				}
			}
		}

		#endregion

		#region Properties
		public List<Agent> AvailableAgents
		{
			get
			{
				if (AgentsPool.Count > 0)
				{
					var agentList = new List<Agent> { new Agent(Agent.LOCAL_MACHINE, Agent.AGENT_TYPE_LOCAL),
													  new Agent(Agent.ANY_AGENT_NAME, Agent.AGENT_TYPE_NETWORK)};
					agentList.AddRange(AgentsPool.Where(x => !x.Disabled).Select(x => new Agent(x.Name, Agent.AGENT_TYPE_NETWORK)));

					return agentList;
				}
				else
				{
					return new List<Agent> { new Agent(Agent.LOCAL_MACHINE, Agent.AGENT_TYPE_LOCAL) };
				}
			}
		}
	
		public ListCollectionView AgentsListView
		{
			get
			{
				List<IAgentList> agentList = AvailableAgents.ToList<IAgentList>();
				agentList.AddRange(AgentsGroupPool.Where(x => x.Name != AgentGroup.ALL_GROUP_NAME).ToList<IAgentList>());
				var allAgents = new ListCollectionView(agentList);

				allAgents.GroupDescriptions.Add(new PropertyGroupDescription("AgentType"));
				return allAgents;
			}
		}
		
		public List<string> EnvironmentList
		{
			get
			{
				var environmentList = new List<string>();
				environmentList.Add(TestLineupRecord.DEFAULT_ENVIRONMENT);
				environmentList.AddRange(new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords.Select(x => x.mID));

				try
				{
					if (dgTestLineup.SelectedItem != null)
					{
						var selectedRecord = dgTestLineup.SelectedItem as TestLineupRecord;
						if (selectedRecord.IsDifferentProduct && !environmentList.Contains(selectedRecord.Environment))
						{
							environmentList.Add(selectedRecord.Environment);
						}
					}
				}
				catch
				{
					//do nothing.. selecteditem throws invalid operation exception everytime bg worker updates the lineup
				}

				return environmentList;
			}
		}

		public List<string> BrowserList
		{   
			get
			{
				var browserList = new List<string>();
				browserList.Add(TestLineupRecord.DEFAULT_BROWSER);
				browserList.AddRange(DlkEnvironment.mAvailableBrowsers.Select(x => x.Alias));
				return browserList;
			}
		}

		public ListCollectionView AllBrowsers
		{
			get
			{
				ObservableCollection<DlkBrowser> browserBuffer = new ObservableCollection<DlkBrowser>();

				mAllBrowsers = new ListCollectionView(browserBuffer);

				browserBuffer.Add(new DlkBrowser(TestLineupRecord.DEFAULT_BROWSER));

				foreach (string browserName in mWebBrowsers)
				{
					browserBuffer.Add(new DlkBrowser("Web", browserName));
				}
				foreach (string browserName in mRemoteBrowsers)
				{
					browserBuffer.Add(new DlkBrowser("Remote", browserName));
				}
				foreach (string browserName in mMobileBrowsers)
				{
					browserBuffer.Add(new DlkBrowser("Mobile", browserName));
				}

				mAllBrowsers.GroupDescriptions.Add(new PropertyGroupDescription("BrowserType"));

				return mAllBrowsers;
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

		public List<int> NumberOfRunsSource
		{
			get
			{
				return new List<int> { 1, 2, 3 };
			}
		}

		public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> ColorList
		{
			get{ return colorList; }
			set { colorList = value; }
		}
#endregion

#region IMPLEMENTATIONS
		//IControllerView implemented properties
		public ObservableCollection<TestLineupRecord> TestLineup
		{
			get
			{
				return mTestLineup;
			}
			set
			{
				mTestLineup = AddTestLineupListener(value);
			}
		}
		
		public TestLineupStatus TestLineupStatus
		{
			get => mTestLineupStatus;
			set => mTestLineupStatus = value;
		}

		public bool IsSaveInProgress
		{
			get;
			set;
		}

		public ObservableCollection<KwDirItem> Favorites
		{
			get
			{
				return mTestSuiteFavorites;
			}
			set
			{
				mTestSuiteFavorites = value;
			}
		}

		public List<Common.KwDirItem> AllSuites
		{
			get
			{
				if (mAllTestSuites == null)
				{
					SearchForSuites.GlobalCollectionOfSuites = new List<KwDirItem>();
					mAllTestSuites = new DlkTestSuiteLoader().GetSuiteDirectories2(DlkEnvironment.mDirTestSuite, string.Empty, null);
				}
				return mAllTestSuites;
			}
		}

		public List<KwDirItem> FilteredFavorites
		{
			get
			{
				return mFilteredFavorites;
			}
			set
			{
				if (value != null)
				{
					mFilteredFavorites = value;
				}
			}
		}

		public List<KwDirItem> FilteredSuites
		{
			get { return mFilteredTestSuites; }
			set
			{
				if (value != null)
				{
					mFilteredTestSuites = value;
				}
			}
		}

		public ObservableCollection<ExecutionHistory> History
		{
			get
			{
				return mTestSuiteExecutionHistory;
			}
		}

		//IScheduleRunnerView
		public bool AllowExecute
		{
			get
			{
				return !this.IsSaveInProgress;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Updates the schedule record inthe testlineup during runtime
		/// </summary>
		/// <param name="newRecord"></param>
		public void UpdateScheduleRecord(TestLineupRecord newRecord)
		{
			if (TestLineup.Any(x => x.Id == newRecord.Id))
			{
				var currentRecord = TestLineup.First(x => x.Id == newRecord.Id);
								
				// after a suite has finished running, update results
				if (currentRecord.Status != newRecord.Status &&
					(newRecord.Status == Enumerations.TestStatus.Failed || newRecord.Status == Enumerations.TestStatus.Passed || newRecord.Status == Enumerations.TestStatus.Cancelled))
				{
					//update its lastrunresult property in real time
					mControllerPresenter.AddLastSuiteResultToTestLineupRecord(currentRecord);

					//update history grid
					Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
					{
						TestLineupRecord suite = (TestLineupRecord)dgTestLineup.SelectedItem;
						if (suite != null && suite.Id == newRecord.Id)
						{
							mControllerPresenter.UpdateHistoryGrid(currentRecord);
						}
					}));
				}

				//update the row fields
				currentRecord.AssignedAgentName = newRecord.AssignedAgentName;
				currentRecord.CurrentRun = newRecord.CurrentRun;
				currentRecord.Status = newRecord.Status;

				UpdateRecordIsDisabled(currentRecord);
			}
		}

		/// <summary>
		/// Will add property change listener to every record
		/// </summary>
		/// <param name="testLineup"></param>
		/// <returns></returns>
		private ObservableCollection<TestLineupRecord> AddTestLineupListener(ObservableCollection<TestLineupRecord> testLineup)
        {
			foreach (var item in testLineup) item.PropertyChanged += TestLineupRecord_PropertyChanged;

			return testLineup;
		}

		/// <summary>
		/// Collection Change Listener for TestLineupRecords
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TestLineupRecord_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
				foreach (TestLineupRecord item in e.NewItems)
					item.PropertyChanged += TestLineupRecord_PropertyChanged;

			if (e.OldItems != null)
				foreach (TestLineupRecord item in e.OldItems)
					item.PropertyChanged -= TestLineupRecord_PropertyChanged;
		}

		/// <summary>
		/// Property Change Listener for TestLineupRecord
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TestLineupRecord_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.ToLower() == "status")
				mTestLineupStatusPresenter.LogRecord((TestLineupRecord)sender);
		}

		/// <summary>
		/// This Method will check if the local machine is currently running another suite
		/// </summary>
		/// <returns></returns>
		public bool CheckIfLocalMachineBusy() => mTestLineup
			.Any(t => t.AssignedAgentName == Agent.LOCAL_MACHINE && 
				t.Status == Enumerations.TestStatus.Running);

		/// <summary>
		/// Get status of schedule
		/// </summary>
		/// <returns></returns>
		public Enumerations.TestStatus GetScheduleStatus(TestLineupRecord newRecord)
		{
			var record = TestLineup.FirstOrDefault(x => x.Id == newRecord.Id);
			if (record != null)
			{
				return record.Status;
			}

			return Enumerations.TestStatus.None;
		}

		/// <summary>
		/// Get suite history of agents
		/// </summary>
		private void GetAgentHistory()
		{
			Agent target = dgAgentLineup.SelectedItem as Agent;
			if (target != null)
			{
				if (chkAgentHistoryFilter.IsChecked == true)
				{
					if(!FilterAgentHistory(target.ResultsHistory.ToList()))
					{
						DlkUserMessages.ShowWarning(DlkUserMessages.INF_OVERLAP_DATE);
						dtAgentHistoryFrom.Text = "";
						dtAgentHistoryTo.Text = "";
						mDateAgentFromOriginalString = "";
						mDateAgentToOriginalString = "";
						dgAgentHistory.ItemsSource = target.ResultsHistory;
					}
				}
				else
				{
					dgAgentHistory.ItemsSource = target.ResultsHistory;
				}
			}
		}

		/// <summary>
		/// Filtering of agent history grid based user date entry
		/// </summary>
		/// <param name="history">Agent history current list</param>
		private bool FilterAgentHistory(List<ExecutionHistory> history)
		{
			DateTime dateFrom = DateTime.Parse(dtAgentHistoryFrom.Text);
			DateTime dateTo = DateTime.Parse(dtAgentHistoryTo.Text);

			int result = DateTime.Compare(dateFrom, dateTo);
			if (result <= 0)
			{
				ObservableCollection<ExecutionHistory> historyList = new ObservableCollection<ExecutionHistory>();
				foreach (var item in history)
				{
					DateTime infoDate = DateTime.Parse(item.ExecutionDate);
					if (infoDate >= dateFrom && infoDate <= dateTo)
					{
						historyList.Add(item);
					}
				}

				historyList.Sort(observableCollection => observableCollection.OrderByDescending(item => DateTime.ParseExact(item.ExecutionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
																				.ThenByDescending(item => DateTime.ParseExact(item.StartTime, "hh:mm:ss.ff tt", CultureInfo.InvariantCulture)));
				dgAgentHistory.ItemsSource = historyList;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Get list of ready agents
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Agent> GetAvailableAgents()
		{
			return mAgentsPool.Where(x => x.Status == Enumerations.AgentStatus.Ready);
		}

		/// <summary>
		/// Get list of ready agents within a group
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Agent> GetAvailableAgentsInGroup(string groupName)
		{
			var group = mAgentsGroupPool.FirstOrDefault(x => x.Name == groupName);
			if (group != null)
				return mAgentsPool.Where(x => x.Status == Enumerations.AgentStatus.Ready && group.Members.Contains(x.Name));

			return new List<Agent>();
		}

		/// <summary>
		/// Remove agent from the pool
		/// </summary>
		/// <param name="agent"></param>
		public void RemoveAgent(Agent agent)
		{
			mAgentsPool.Remove(agent);

			//remove the agent from each group
			foreach (var group in mAgentsGroupPool)
			{
				if (group.Members.Any(x => x == agent.Name))
					group.Members.Remove(agent.Name);
			}
			//update filtered grid if selected item is a group
			var selectedGroup = dgAgentGroups.SelectedItem as AgentGroup;
			if (selectedGroup != null && selectedGroup.Name != AgentGroup.ALL_GROUP_NAME)
			{
				FilterAgentGrid(selectedGroup, false);
			}
		}

		//IAgentsView
		public ObservableCollection<Agent> AgentsPool
		{
			get { return mAgentsPool; }
			set { mAgentsPool = value; }
		}

		public ObservableCollection<AgentGroup> AgentsGroupPool
		{
			get { return mAgentsGroupPool; }
			set { mAgentsGroupPool = value; }
		}

		public ObservableCollection<ExecutionHistory> AgentHistory
		{
			get
			{
				return mAgentHistory;
			}
			set
			{
				mAgentHistory = value;
			}
		}

		/// <summary>
		/// Get suite name from suite id
		/// </summary>
		/// <param name="SuiteId"></param>
		/// <returns></returns>
		public string GetSuiteName(string SuiteId)
		{
			var lineup = TestLineup.FirstOrDefault(x => x.Id == SuiteId);

			if (lineup != null)
			{
				return lineup.TestSuiteToRun.Name;
			}
			return string.Empty;
		}

		/// <summary>
		/// Checks whether URL is blacklisted - displays warning if not
		/// </summary>
		/// <param name="EnvironmentName">Environment name</param>
		/// <returns>True if environment has blacklisted URL, False if not</returns>
		private bool CheckEnvironmentIfBlacklisted(string EnvironmentName, TestLineupRecord record)
		{
			if (mIsInitialTestCheck)
			{
				mLoader.mTestCheckLoaderWorker.ReportProgress(Convert.ToInt32(mLoaderProgress), string.Format(DlkUserMessages.INF_SCHEDULER_LOADER_TEST_BLACKLIST_CHECK, (mCurrentTestCount + 1).ToString(), mTotalTestCount[mCurrentSuiteCount].ToString(), (mCurrentSuiteCount + 1).ToString(), mTotalTestCount.Count().ToString()));
				mCurrentTestCount++;
			}
			bool isBlacklisted = false;
			string environmentNameToBeAssigned = "";
			if (mLoginRecords.Any(x => x.mID == EnvironmentName))
			{
				if (DlkEnvironment.IsURLBlacklist(EnvironmentName, true))
				{
					environmentNameToBeAssigned = EnvironmentName;
					isBlacklisted = true;
					if (mIsInitialTestCheck)
					{
						// prevent progress bar overflow
						bool mExceedsOverallProgress = mLoaderProgress + mSuiteUnitCount > OVERALL_PROGRESS_PERCENTAGE;
						mCurrentSuiteBlockCount += mSuiteUnitCount;
						mLoaderProgress = mExceedsOverallProgress ? OVERALL_PROGRESS_PERCENTAGE : CALCULATE_TEST_COUNT_PROGRESS_PERCENTAGE + mCurrentSuiteBlockCount;
						mLoader.mTestCheckLoaderWorker.ReportProgress(Convert.ToInt32(mLoaderProgress), string.Format(DlkUserMessages.INF_SCHEDULER_LOADER_BLACKLISTED_URL_FOUND, EnvironmentName, (mCurrentSuiteCount + 1).ToString(), mTotalTestCount.Count().ToString()));
						Thread.Sleep(500);
					}
				}
			}
			if (mIsInitialTestCheck && !isBlacklisted)
			{
				// prevent tests progress from exceeding suite allotted progress bar count
				bool mExceedsSuiteProgress = mCurrentSuiteUnitCount + mTestUnitCount > mSuiteUnitCount;
				mCurrentSuiteBlockCount = mExceedsSuiteProgress && !mHasAddedToBlockCount ? mCurrentSuiteBlockCount += mSuiteUnitCount : mCurrentSuiteBlockCount;
				mHasAddedToBlockCount = mExceedsSuiteProgress;
				bool mExceedsOverallProgress = mLoaderProgress + mTestUnitCount > OVERALL_PROGRESS_PERCENTAGE;
				mLoaderProgress = mExceedsOverallProgress ? 100 : mExceedsSuiteProgress ? CALCULATE_TEST_COUNT_PROGRESS_PERCENTAGE + mCurrentSuiteBlockCount : mLoaderProgress + mTestUnitCount;
				mCurrentSuiteUnitCount += mTestUnitCount;
			}
			record.NonDefaultBlacklistedEnvironment = environmentNameToBeAssigned;
			return isBlacklisted;
		}

		/// <summary>
		/// Checks suite for any blacklisted URL in its tests
		/// </summary>
		/// <param name="record">Target suite</param>
		/// <returns>True if any test has blacklisted URL, False if not</returns>
		private bool CheckSuiteURLsIfBlacklisted(TestLineupRecord record)
		{
			List<string> testEnvs = new List<string>();
			try
			{
				if (File.Exists(record.TestSuiteToRun.Path))
				{
					XDocument suiteDoc = XDocument.Load(record.TestSuiteToRun.Path);
					var data = from doc in suiteDoc.Descendants("test")
							   select new
							   {
								   environment = doc.Attribute("environment").Value
							   };
					foreach (var val in data)
					{
						testEnvs.Add(val.environment);
					}
				}
			}
			catch
			{
				// skip specific suite check and progress bar to next suite if there are errors in suite file
				if (mIsInitialTestCheck)
				{
					mCurrentSuiteBlockCount = mCurrentSuiteBlockCount += mSuiteUnitCount;
					bool mExceedsOverallProgress = mLoaderProgress + mSuiteUnitCount > OVERALL_PROGRESS_PERCENTAGE;
					mLoaderProgress = mExceedsOverallProgress ? OVERALL_PROGRESS_PERCENTAGE : mLoaderProgress + mSuiteUnitCount;
					mLoader.mTestCheckLoaderWorker.ReportProgress(Convert.ToInt32(mLoaderProgress), string.Format(DlkUserMessages.INF_SCHEDULER_LOADER_CORRUPT_SUITE, record.TestSuiteToRun.Name));
					Thread.Sleep(500);
					mCurrentSuiteUnitCount += mTestUnitCount;
				}
			}
			if (testEnvs.Any(x => CheckEnvironmentIfBlacklisted(x, record)))
			{
				return true;
			}
			if (!mHasAddedToBlockCount)
			{
				mCurrentSuiteBlockCount += mSuiteUnitCount;
			}
			return false;
		}
		#endregion

#region EVENT HANDLERS

		private void ColorPickerMenu_Closed(object sender, RoutedEventArgs e)
		{
			List<TestLineupRecord> selectedLineups = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();
			foreach (TestLineupRecord lineup in TestLineup)
			{
				if (selectedLineups.Contains(lineup))
				{
					lineup.RowColor = ColorPickerMenu.SelectedColor.Value;
				}
			}
		}

		private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
		{
			mNotifyIcon.Visible = false;
			mNotifyIcon.Dispose();
		}

		private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, e.Exception);
			e.Handled = true;
		}

		private void NotifyIcon_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				this.Visibility = Visibility.Visible;
				this.WindowState = System.Windows.WindowState.Normal;
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void ExitMenuItem_Click(object Sender, EventArgs e)
		{
			try
			{
				ExitScheduler();
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// Get the original value of recurrence before editing
		/// </summary>
		/// <param name="sender">Object sender</param>
		/// <param name="e">Event arguments</param>
		private void ComboBox_RecurrenceGotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				ComboBox cbBoxRecurrence = (ComboBox)sender;
				schedRecurrencePrevVal = cbBoxRecurrence.Text;
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// Validation of recurrence value when edited on the table
		/// </summary>
		/// <param name="sender">Object sender</param>
		/// <param name="e">Event arguments</param>
		private void ComboBox_RecurrenceLostFocus(object sender, RoutedEventArgs e)
		{
			ComboBox cbBoxRecurrence = (ComboBox)sender;
			try
			{
				if (string.IsNullOrEmpty(cbBoxRecurrence.Text))
				{
					throw new Exception(DlkUserMessages.ERR_BLANK_RECURRENCE_CHANGE_DEFAULT);
				}
				else
				{
					bool rFound = false;
					foreach (var recurrenceVal in RecurrenceTypes)
					{
						if (cbBoxRecurrence.Text.Equals(recurrenceVal))
						{
							rFound = true;
							schedRecurrencePrevVal = null;
							break;
						}
					}

					if(!rFound)
						throw new Exception(DlkUserMessages.ERR_INVALID_RECURRENCE_VALUE);
				}
			}
			catch (Exception ex)
			{
				cbBoxRecurrence.Text = !String.IsNullOrEmpty(schedRecurrencePrevVal) ? schedRecurrencePrevVal : cbBoxRecurrence.Text;
				schedRecurrencePrevVal = null;
				DlkUserMessages.ShowError(ex.Message);
			}
		}

		//Controller events
		private void GetControllerSuiteHistory()
		{
			TestLineupRecord suite = new TestLineupRecord();

			if(mIsEditMode)
				suite = (TestLineupRecord)dgTestLineup.SelectedItem;
			else
				suite = (TestLineupRecord)dgLastRunResult.SelectedItem;

			if (suite != null)
				mControllerPresenter.UpdateHistoryGrid(suite);
		}

		private void StartSaveWorker()
		{
			/* file saver */
			mFileSaveWorker = new DlkBackgroundWorkerWithAbort();
			mFileSaveWorker.DoWork += mFileSaveWorker_DoWork;
			mFileSaveWorker.RunWorkerAsync();
		}

		private void AbortSaveWorker()
		{
			if (mFileSaveWorker != null)
			{
				mFileSaveWorker.Abort();
				mFileSaveWorker.Dispose();
				mFileSaveWorker = null;
			}
		}

		private bool HasEnvironment(string warningTitle)
		{
			if (EnvironmentList.Count == 1)
			{
				DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, warningTitle);
				return false;
			}
			return true;
		}

		private void mFileSaveWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				Thread.Sleep(3000);
				_busy.WaitOne();
				try
				{
					this.mControllerPresenter.SaveLineup();
				}
				catch (Exception ex)
				{
					DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
				}
			}
		}

		private void StartExecuteWorker()
		{
			mExecuteWorker = new DlkBackgroundWorkerWithAbort();
			mExecuteWorker.DoWork += mExecuteWorker_DoWork;
			mExecuteWorker.RunWorkerAsync();
		}

		private void AbortExecuteWorker()
		{
			if (mExecuteWorker != null)
			{
				mExecuteWorker.Abort();
				mExecuteWorker.Dispose();
				mExecuteWorker = null;
			}
		}

		private void mExecuteWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			int currentMin = -1;
			while (true)
			{
				Thread.Sleep(2000);
				_busy.WaitOne();
				try
				{
					//update execution queue every minute
					if (currentMin != DateTime.Now.Minute)
					{
						mExecutePresenter.LoadSchedulesForExecution(EnvironmentList.Count);
						currentMin = DateTime.Now.Minute;
					}

					mExecutePresenter.ExecuteQueueItemsOnLocalMachine();
					mExecutePresenter.ExecuteQueueItemsOnAgents();

					Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
					{
						btnLineupLoad.IsEnabled = !mExecutePresenter.IsTestRunning();
						btnLineupSaveAs.IsEnabled = !mExecutePresenter.IsTestRunning();
					}));

				}
				catch (Exception ex)
				{
					DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
				}
			}
		}

		//Agent events
		private void StartAgentWorker()
		{
			mAgentWorker = new DlkBackgroundWorkerWithAbort();
			mAgentWorker.DoWork += mAgentWorker_DoWork;
			mAgentWorker.RunWorkerAsync();
		}

		private void AbortAgentWorker()
		{
			if (mAgentWorker != null)
			{
				mAgentWorker.Abort();
				mAgentWorker.Dispose();
				mAgentWorker = null;
			}
		}

		private void mAgentWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				foreach (Agent agent in AgentsPool)
				{
					if (!agent.Disabled)
					{
						ThreadPool.QueueUserWorkItem(delegate
						{
							try
							{
								mAgentPresenter.UpdateStatus(agent.Name);
							}
							catch (Exception ex)
							{
                                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                            }
						});
					}
				}

				Thread.Sleep(8000);
			}
		}

		private void StartTestBlacklistCheckWorker()
		{
			mTestBlacklistCheckWorker = new DlkBackgroundWorkerWithAbort();
			mTestBlacklistCheckWorker.DoWork += mTestBlacklistCheckWorker_DoWork;
			mTestBlacklistCheckWorker.RunWorkerAsync();
		}

		private void AbortTestBlacklistCheckWorker()
		{
			if (mTestBlacklistCheckWorker != null)
			{
				mTestBlacklistCheckWorker.Abort();
				mTestBlacklistCheckWorker.Dispose();
				mTestBlacklistCheckWorker = null;
			}
		}

		private void mTestBlacklistCheckWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				Thread.Sleep(1000);
				try
				{
					DlkEnvironment.InitializeBlacklistedURLs();
					CheckQueueEnvironmentsForBlacklisted();
					CheckSuiteDefaultsForBlacklisted();
				}
				catch (Exception ex)
				{
					DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
				}
			}
		}

		//Load events
		private void StartDirectoryLoader()
		{
			mLoadDirectoryWorker = new DlkBackgroundWorkerWithAbort();
			mLoadDirectoryWorker.DoWork += mLoadDirectoryWorker_DoWork;
			mLoadDirectoryWorker.RunWorkerAsync();
		}

		private void AbortLoadDirectoryWorker()
		{
			if (mLoadDirectoryWorker != null)
			{
				mLoadDirectoryWorker.Abort();
				mLoadDirectoryWorker.Dispose();
				mLoadDirectoryWorker = null;
			}
		}
        
        private void ConfigFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            string currentProduct = DlkConfigHandler.GetConfigValue("currentapp");

            ((FileSystemWatcher)sender).EnableRaisingEvents = false; //this is to prevent the issue of the file system watcher that calls the method 2 times.
            if (!mIsLoaded)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    if (!mInitialApp.Equals(int.Parse(currentProduct)))
                    {
                        if (DlkUserMessages.ShowQuestionYesNoWarning(this, DlkUserMessages.ASK_ADVANCED_SCHEDULER_RELOAD, "Application Change") == MessageBoxResult.Yes)
                        {
                            ProductSelection productSelection = new ProductSelection();
                            productSelection.LoadFromTR(currentProduct);
                            mInitialApp = int.Parse(currentProduct);
							var prodName = DlkTestRunnerSettingsHandler
								.ApplicationList.FirstOrDefault(a => a.ID == currentProduct);
							if (prodName == null) mApplicationName = "Undefined";
							mApplicationName = prodName.DisplayName;
							DlkEnvironment.mCurrentProductName = prodName.DisplayName;
							RefreshSuites();
							LoadFavoritesTree();

							//update twice to works..
							dgTestLineup.CommitEdit();
							dgTestLineup.CommitEdit();

							dgTestLineup.Items.Refresh();

							if (dgTestLineup.SelectedItems.Count > 0)
							{
								btnLineupEdit.IsEnabled = !dgTestLineup.SelectedItems.Cast<TestLineupRecord>().First().IsDifferentProduct;
							}
						}
                    }
                });

                mIsLoaded = true;
            }

            ((FileSystemWatcher)sender).EnableRaisingEvents = true;
            mIsLoaded = false;

        }

        private void mLoadDirectoryWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			//load tests
			KeywordLoaderSingleton.Instance.GetKeywordDirectories(DlkEnvironment.mDirTests, null);
			//load object store
			DlkDynamicObjectStoreHandler.Instance.Initialize();
		}

		/// <summary>
		/// Gets clicked context menu item text
		/// </summary>
		/// <param name="sender">Clicked context menu item</param>
		private string ProcessMenuItem(object sender)
		{
			if (sender is MenuItem)
			{
				IEnumerable<MenuItem> menuItems = null;
				var mi = (MenuItem)sender;
				if (mi.Parent is ContextMenu)
					menuItems = ((ContextMenu)mi.Parent).Items.OfType<MenuItem>();
				if (mi.Parent is MenuItem)
					menuItems = ((MenuItem)mi.Parent).Items.OfType<MenuItem>();
				if (menuItems != null)
					foreach (var item in menuItems)
					{
						if (item.IsCheckable && item == mi)
						{
							item.IsChecked = true;
						}
						else if (item.IsCheckable && item != mi)
						{
							item.IsChecked = false;
						}
					}
				return mi.Header.ToString();
			}
			else
			{
				return null;
			}
		}

		private void dtControllerHistoryFrom_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
		{
			DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_FILTER_DATE_INVALID);
		}

		private void dtControllerHistoryTo_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
		{
			DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_FILTER_DATE_INVALID);
		}

		private void dtAgentHistoryFrom_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
		{
			DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_FILTER_DATE_INVALID);
		}

		private void dtAgentHistoryTo_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
		{
			DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_FILTER_DATE_INVALID);
		}

		private void SearchTypeMenuItem_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string SearchName = ProcessMenuItem(sender);
				switch (SearchName)
				{
					case MENU_TEXT_SUITESONLY:
						mSearchType = KwDirItem.SearchType.TestsSuitesOnly;
						break;
					case MENU_TEXT_OWNERSONLY:
						mSearchType = KwDirItem.SearchType.Owner;
						break;
					case MENU_TEXT_TAGSONLY:
						mSearchType = KwDirItem.SearchType.TagsOnly;
						break;
					case MENU_TEXT_SUITESANDTAGS:
						mSearchType = KwDirItem.SearchType.TestsSuitesAndTags;
						break;
					default:
						return;
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}
		#endregion

		#region UI HANDLERS

		private void btnEditSuite_Click(object sender, RoutedEventArgs e)
		{
			var selectedSuite = mTreeItemSelectionList_AllSuites[0].dirItem;
			if (selectedSuite != null)
				OpenSelectedSuite(selectedSuite.Path);
		}

		private void btnFavoritesEditItem_Click(object sender, RoutedEventArgs e)
		{
			var selectedFavorite = mTreeItemSelectionList_Favorites[0].dirItem;
			if (selectedFavorite != null)
				OpenSelectedSuite(selectedFavorite.Path);
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				if (mIsExplicitClose)
				{
					PauseSchedulerWorker();
					mNotifyIcon.Visible = false;
					mNotifyIcon.Dispose();
					AbortSaveWorker();
					AbortExecuteWorker();
					AbortAgentWorker();
					AbortLoadDirectoryWorker();
					AbortTestBlacklistCheckWorker();
					tvAllSuites.DataContext = null;
					Dispatcher.ShutdownStarted -= Dispatcher_ShutdownStarted;
					Dispatcher.UnhandledException -= Dispatcher_UnhandledException; 
				}
				else
				{
					e.Cancel = true;
					this.Visibility = Visibility.Hidden;
					mNotifyIcon.ShowBalloonTip(5000, "Scheduler", DlkUserMessages.INF_PROCESS_RUNNING_BACKGROUND, System.Windows.Forms.ToolTipIcon.Info);
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
				CheckIfSchedulerIsReadonly();

				StartSaveWorker();
				StartExecuteWorker();
				StartAgentWorker();
				StartDirectoryLoader();
				StartTestBlacklistCheckWorker();

				//resume worker
				_busy.Set();
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void FileMenu_Hide(object sender, RoutedEventArgs e)
		{
			mIsExplicitClose = false;
			this.Close();
		}

		private void FileMenu_Exit(object sender, RoutedEventArgs e)
		{
			ExitScheduler();
		}

		private void FileMenu_Export(object sender, RoutedEventArgs e)
		{
			ExportReportsDialog exportDialog = new ExportReportsDialog(this);
			exportDialog.ShowDialog();
		}

		private void FileMenu_LaunchSchedulingAgent(object sender, RoutedEventArgs e)
		{
			string path = DlkEnvironment.mDirTools + @"\TestRunner\SchedulingAgent.exe";
			if (File.Exists(path))
			{
				if (DlkProcess.IsProcessRunning("SchedulingAgent", false))
				{
					DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULING_AGENT_RUNNING);
					return;
				}
				else
				{
					DlkProcess.RunProcess(path, "", DlkEnvironment.mDirTools, false, 0);
				}
			}
			else
			{
#if DEBUG
				DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULING_AGENT_MISSING);
#else
				DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULING_AGENT_MISSING_EXTERNAL);
#endif

			}
		}

		private void FileMenu_GetLatest(object sender, RoutedEventArgs e)
		{
			Modal modalBox = new Modal(this, "Getting latest product files...");
			modalBox.Show();

			//Get latest products folder on controller machine
			DlkSourceControlHandler.GetFiles(DlkEnvironment.mDirProduct, "/recursive /overwrite");
			RefreshSuites();

			modalBox.Close();
		}

		private void linkOptions_Click(object sender, RoutedEventArgs e)
		{
			var selectedLineups = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();
			TestLineupOptionsDialog tlSetDlg = new TestLineupOptionsDialog(selectedLineups);
			tlSetDlg.Owner = this;
			if ((bool)tlSetDlg.ShowDialog())
			{
				DlkUserMessages.ShowInfo(DlkUserMessages.INF_SCHEDULER_LINEUP_OPTIONS_UPDATED);
			}
		}

		//--All suites
		private void tvAllSuites_GetSelectedItem(object sender, RoutedEventArgs e)
		{
			TreeViewItem tvi = e.OriginalSource as TreeViewItem;
			KwDirItem dirItem = CopyKwDirItem(tvAllSuites.SelectedItem as KwDirItem);

			BFFolder dragSourceFolder = null;
			ItemsControl parent = GetTreeViewItemParent(tvi);
			if (parent is TreeViewItem) dragSourceFolder = (parent as TreeViewItem).Header as BFFolder;
			if (dragSourceFolder == null) dragSourceFolder = new BFFolder();

			GetSelectedItems(dirItem, dragSourceFolder, tvi, ref mTreeItemSelectionList_AllSuites);
		}

		/// <summary>
		/// Pass new instance
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvAllSuitesItem_MouseMove(object sender, MouseEventArgs e)
		{
			TreeViewItem tvi = sender as TreeViewItem;
			if (tvi != null && e.LeftButton == MouseButtonState.Pressed)
			{
				DragDrop.DoDragDrop(tvi, new DataObject("dragSource", "allsuites"), DragDropEffects.Copy);
			}
		}

		private void btn_changeProject_Click(object sender, RoutedEventArgs e)
		{
			ProductSelection productSelection = new ProductSelection();
			if ((bool)productSelection.ShowDialog())
			{
				mApplicationName = ((DlkTargetApplication)productSelection.SelectedProduct).Name;
				RefreshSuites();
			}
		}

		private void btnQueueFromAll_Click(object sender, RoutedEventArgs e)
		{
			if (EnvironmentList.Count == 1)
			{
				DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, "Queue failed");
				return;
			}

			if (mIsSchedulerFileReadonly)
				return;

			foreach (var item in mTreeItemSelectionList_AllSuites)
			{
				if (item.dirItem != null && item.dirItem is BFFile)
				{
					QueueSelectedTreeItem(item.dirItem);
				}
				else if (item.dirItem != null && item.dirItem is BFFolder)
				{
					var folder = item.dirItem as BFFolder;
					IterateFolder(folder, QueueSelectedTreeItem);
				}
			}
			CheckQueueEnvironmentsForBlacklisted();

			SetLineupButtonIsEnabled();
		}

		private void btnQueueFromFavorites_Click(object sender, RoutedEventArgs e)
		{
			if (!HasEnvironment(QUEUE_FAILED) || mIsSchedulerFileReadonly)
				return;

			foreach (var item in mTreeItemSelectionList_Favorites)
			{
				if (item.dirItem != null && item.dirItem is BFFile)
				{
					QueueSelectedTreeItem(item.dirItem);
				}
				else if (item.dirItem != null && item.dirItem is BFFolder)
				{
					var folder = item.dirItem as BFFolder;
					IterateFolder(folder, QueueSelectedTreeItem);
				}
			}
			CheckQueueEnvironmentsForBlacklisted();
			SetLineupButtonIsEnabled();
		}

		private void btnSearchSuite_Click(object sender, RoutedEventArgs e)
		{
			if (txtSearchSuite.IsVisible)
			{
				txtSearchSuite.Visibility = Visibility.Collapsed;
				txtSearchSuite.Text = "";
				txtSearchSuite.Background = Brushes.LemonChiffon;
				tvAllSuites.DataContext = AllSuites;
			}
			else
			{
				txtSearchSuite.Visibility = Visibility.Visible;
				txtSearchSuite.Focus();
			}
		}

		private void btnRefreshSuite_Click(object sender, RoutedEventArgs e)
		{
			RefreshSuites();
		}

		private void btnSearchFavorites_Click(object sender, RoutedEventArgs e)
		{
			if (txtSearchFavorites.IsVisible)
			{
				txtSearchFavorites.Visibility = Visibility.Collapsed;
				txtSearchFavorites.Text = "";
				txtSearchFavorites.Background = Brushes.LemonChiffon;
				tvFavorites.DataContext = Favorites;
			}
			else
			{
				txtSearchFavorites.Visibility = Visibility.Visible;
				txtSearchFavorites.Focus();
			}
		}

		private void ToolBar_Loaded(object sender, RoutedEventArgs e)
		{
			ToolBar toolBar = sender as ToolBar;

			var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
			if (mainPanelBorder != null)
			{
				mainPanelBorder.Margin = new Thickness(0);
			}
		}

		private void rdoAll_Checked(object sender, RoutedEventArgs e)
		{
			try
			{
				RadioButton rb = sender as RadioButton;

				switch (rb.Tag.ToString())
				{
					case "Controller":
						SelectTab(0);
						break;
					case "Agents":
						SelectTab(1);
						break;
					case "Tests":
						SelectTab(2);
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// Handler for search timer. Default time is 2.5s.
		/// This event handler fires the auto-search feature, letter per letter, when the time alloted for typing is consumed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchSuite_KeyUp(object sender, KeyEventArgs e)
		{
			SetupSearchSuites(txtSearchSuite_typingFinished);
		}

		/// <summary>
		/// Event handler where the timer has elapsed (meaning that it assumes that the user is finished typing)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchSuite_typingFinished(object sender, System.EventArgs e)
		{
			try
			{
				String mTestFilter = txtSearchSuite.Text;
				
				if (String.IsNullOrEmpty(mTestFilter))
				{
					txtSearchSuite.Background = Brushes.LemonChiffon;
					tvAllSuites.DataContext = AllSuites;
				}
				else if (mSearchType != KwDirItem.SearchType.Owner)
				{
					if (DlkString.IsSearchValid(mTestFilter))
					{
						txtSearchSuite.Background = Brushes.LemonChiffon;
						LoadSuiteList(DlkEnvironment.mDirTestSuite, mTestFilter, SearchOption.AllDirectories);
					}
					else
					{
						tvAllSuites.DataContext = null;
						txtSearchSuite.Background = Brushes.LightPink;
					}
				}
				else
				{
					txtSearchSuite.Background = Brushes.LemonChiffon;
					LoadSuiteList(DlkEnvironment.mDirTestSuite, mTestFilter, SearchOption.AllDirectories);
				}

				// set to null, re-instantiate timer in KeyUp
				if (mTxtSearchSuitesTimer != null)
				{
					mTxtSearchSuitesTimer.Stop();
					mTxtSearchSuitesTimer.Dispose();
					mTxtSearchSuitesTimer = null;
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		//--Favorites
		private void tvFavorites_GetSelectedItem(object sender, RoutedEventArgs e)
		{
			TreeViewItem tvi = e.OriginalSource as TreeViewItem;
			mSelectedFavorite = tvi;
			KwDirItem dirItem = CopyKwDirItem(tvFavorites.SelectedItem as KwDirItem); 

			BFFolder dragSourceFolder = null;
			ItemsControl parent = GetTreeViewItemParent(tvi);
			if (parent is TreeViewItem) dragSourceFolder = (parent as TreeViewItem).Header as BFFolder;
			if (dragSourceFolder == null) dragSourceFolder = new BFFolder();

			GetSelectedItems(dirItem, dragSourceFolder, tvi, ref mTreeItemSelectionList_Favorites);
		}

		private void tvFavorites_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Right)
			{
				foreach (var item in mTreeItemSelectionList_Favorites)
				{
					item.tvi.IsSelected = false;
					item.tvi.Background = Brushes.White;
					item.tvi.Foreground = Brushes.Black;
				}

				mTreeItemSelectionList_Favorites.Clear();

				//disable buttons after clearing items from selection list.
				ToggleTreeViewButtons();
				mSelectedFavorite = null;
			}
		}

		/// <summary>
		/// Pass new instance
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFavoritesItem_MouseMove(object sender, MouseEventArgs e)
		{
			TreeViewItem tvi = sender as TreeViewItem;
			if (tvi != null && e.LeftButton == MouseButtonState.Pressed)
			{
				DragDrop.DoDragDrop(tvi, new DataObject("dragSource", "favorites"), DragDropEffects.Copy);
			}
		}

		private void txtSearchFavorite_KeyUp(object sender, KeyEventArgs e)
		{
			SetupSearchFavorites(txtSearchFavorite_typingFinished);
		}

		/// <summary>
		/// Event handler where the timer has elapsed (meaning that it assumes that the user is finished typing)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchFavorite_typingFinished(object sender, System.EventArgs e)
		{
			try
			{
				String mTestFilter = txtSearchFavorites.Text;

				if (!string.IsNullOrEmpty(mTestFilter))
				{
					// populate filteredSuites
					SearchFavorites(mTestFilter);
					tvFavorites.DataContext = FilteredFavorites;
				}
				else
				{
					tvFavorites.DataContext = Favorites;
					txtSearchFavorites.Background = Brushes.LemonChiffon;
				}
				// set to null, re-instantiate timer in KeyUp
				if (mTxtSearchFavoritesTimer != null)
				{
					mTxtSearchFavoritesTimer.Stop();
					mTxtSearchFavoritesTimer.Dispose();
					mTxtSearchFavoritesTimer = null;
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// tvFavorites_Drop is the event handler for general item drop into the tree view. it is different from tvFavoritesFolder_Drop where it capture only drop to items event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFavorites_Drop(object sender, DragEventArgs e)
		{
			if (!mIsDropTargetATreeViewItem)
			{
				string source = e.Data.GetData("dragSource") as string;
				List<TreeViewSelectedItem> mList = new List<TreeViewSelectedItem>();

				if (source == "favorites")
					mList = mTreeItemSelectionList_Favorites;
				else if (source == "allsuites")
					mList = mTreeItemSelectionList_AllSuites;

				foreach (var item in mList)
				{
					if (CheckFavoritesItemExist(item.dirItem.Name))
					{
						DlkUserMessages.ShowInfo(string.Format(DlkUserMessages.INF_FOLDER_ALREADY_EXIST_SPECIFIED, item.dirItem.Name));
						continue;
					}

					var dirItem = CopyKwDirItem(item.dirItem);
					if (dirItem != null)
					{
						AddToFavoritesTree(dirItem);

						//if source is favorites, cut/remove the original item
						if (source != null && (string)source == "favorites")
						{
							var sourceFolderObject = item.dragSourceFolder;
							BFFolder sourceFolder = sourceFolderObject as BFFolder;
							//remove from drag source folder
							RemoveFromFavoritesTree(sourceFolder, dirItem);
						}
					}
				}     
			}
			mControllerPresenter.SaveFavoritesTreeToFile();
			tvFavorites.Items.Refresh();
			ToggleTreeViewButtons();
			//if currently searching - refresh search list after drop
			if (!string.IsNullOrEmpty(txtSearchFavorites.Text))
			{
				SearchFavorites(txtSearchFavorites.Text);
				tvFavorites.DataContext = FilteredFavorites;
			}
		}

		/// <summary>
		/// reset value to false everytime something is dragged to the treeview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFavorites_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				//reset bool every time we enter the treeview
				mIsDropTargetATreeViewItem = false;
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// tvFavorites_Drop will be fired after this event fires, we change the boolean value to determine whether the code inside tvFavorites_Drop will run or not.
		/// This event handles drops to folders only.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFavoritesFolder_Drop(object sender, DragEventArgs e)
		{
			// check if drop target is a content presenter
			if (e.Source.GetType() == typeof(ContentPresenter))
			{
				string source = e.Data.GetData("dragSource") as string;
				List<TreeViewSelectedItem> mList = new List<TreeViewSelectedItem>();

				if (source == "favorites")
					mList = mTreeItemSelectionList_Favorites;
				else if (source == "allsuites")
					mList = mTreeItemSelectionList_AllSuites;

				foreach (var item in mList.ToList())
				{
					var tvFolderDropDestination = sender as TreeViewItem;
					BFFolder dropDestination = tvFolderDropDestination.DataContext as BFFolder;

					if (dropDestination.Name == "test suite")
					{
						e.Effects = DragDropEffects.None;
						return;
					}

					if (CheckFavoritesItemExist(item.dirItem.Name, tvFolderDropDestination))
					{
						DlkUserMessages.ShowInfo(string.Format(DlkUserMessages.INF_FOLDER_ALREADY_EXIST_SPECIFIED, item.dirItem.Name));
						continue;
					}

					var dirItem = CopyKwDirItem(item.dirItem);
					if (dirItem != null)
					{
						// get target folder, return null if drop destination is a file
						//BFFolder dropDestination = tvFolderDropDestination.DataContext as BFFolder;

						// if drop destination is null, it is a file, get parent folder
						if (dropDestination == null)
						{
							ItemsControl parent = GetTreeViewItemParent(tvFolderDropDestination);
							if (parent is TreeViewItem)
							{
								dropDestination = (parent as TreeViewItem).Header as BFFolder;
							}
							else
							{
								//if parent is not treeview, means we are on base path, just return and let tvFavorites_Drop handle it
								return;
							}
						}

						//if source is favorites, cut/remove the original item
						if (source != null && source == "favorites")
						{
							var sourceFolderObject = item.dragSourceFolder;
							BFFolder sourceFolder = sourceFolderObject as BFFolder;

							//if trying to drag folder to a destination within himself, stop it
							if (IsDestinationWithinDraggedItem(dirItem, dropDestination))
							{
								mIsDropTargetATreeViewItem = true; //this is to block add on tvFavorites_Drop
								return;
							}

							//remove from drag source folder
							RemoveFromFavoritesTree(sourceFolder, dirItem);
						}

						//once we get the destination path, add the item in
						AddItemToFolderInFavoritesTree(dropDestination, dirItem);

					}
				}

				mIsDropTargetATreeViewItem = true; //this is to block add on tvFavorites_Drop
				tvFavorites.Items.Refresh();
				ToggleTreeViewButtons();
			}
		}

		/// <summary>
		/// This is to capture the treeviewitem of currently selected item in tvfavorites
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFavorites_ItemSelected(object sender, RoutedEventArgs e)
		{
			TreeViewItem tvi = e.OriginalSource as TreeViewItem;
			mSelectedFavorite = tvi;
		}

		/// <summary>
		/// Adds a folder to the Favorites tree, folder name must be unique
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFavoritesAddFolder_Click(object sender, RoutedEventArgs e)
		{
			var inputBox = new DialogBox("Add a Favorite Folder", "Folder Name:", true);
			inputBox.Owner = this;
			if (inputBox.ShowDialog() == true)
			{
				var foldername = inputBox.TextBoxValue.Trim();
				if (!String.IsNullOrWhiteSpace(foldername))
				{
					if (CheckFavoritesItemExist(foldername, mSelectedFavorite, false))
					{
						DlkUserMessages.ShowError(DlkUserMessages.ERR_FOLDER_NAME_ALREADY_EXISTS);
						return;
					}

					if (tvFavorites.SelectedItem as BFFolder != null)
					{
						// add folder inside folder
						AddItemToFolderInFavoritesTree(tvFavorites.SelectedItem as BFFolder, new BFFolder()
						{
							Name = foldername,
							Path = foldername // we only need a temp path
						});
						tvFavorites.Items.Refresh();
					}
					else if (tvFavorites.SelectedItem as BFFile != null)
					{
						ItemsControl parent = GetTreeViewItemParent(mSelectedFavorite);
						if (parent is TreeViewItem)
						{
							var folder = (parent as TreeViewItem).Header as BFFolder;
							AddItemToFolderInFavoritesTree(folder, new BFFolder()
							{
								Name = foldername,
								Path = foldername 
							});
							tvFavorites.Items.Refresh();
						}
						else
						{
							AddToFavoritesTree(new BFFolder()
							{
								Name = foldername,
								Path = foldername
							});
						}
					}
					else
					{
						AddToFavoritesTree(new BFFolder()
						{
							Name = foldername,
							Path = foldername
						});
					}
					mControllerPresenter.SaveFavoritesTreeToFile();
				}
				else
				{
					DlkUserMessages.ShowInfo(DlkUserMessages.INF_SCHEDULER_ADD_FAVORITES_FOLDER_EMPTY);
				}
			}
		}

		/// <summary>
		/// Rename a favorites folder, folder name must be unique
		/// </summary>
		/// <param name="sender">Sender Object</param>
		/// <param name="e">Event arguments</param>
		private void btn_RenameFavoritesFolder_Click(object sender, RoutedEventArgs e)
		{
			var inputBox = new DialogBox("Rename a Favorites Folder", "Folder Name:", true, mTreeItemSelectionList_Favorites[0].dirItem.Name);
			inputBox.Owner = this;
			if (inputBox.ShowDialog() == true)
			{
				var foldername = inputBox.TextBoxValue;

				if (string.IsNullOrEmpty(foldername))
				{
					DlkUserMessages.ShowWarning(DlkUserMessages.INF_FOLDERNAME_NOT_EMPTY);
					return;
				}
				var item = mTreeItemSelectionList_Favorites[0];

				var parent = GetTreeViewItemParent(item.tvi);
				if (CheckFavoritesItemExist(foldername, parent as TreeViewItem, false))
				{
					DlkUserMessages.ShowError(DlkUserMessages.ERR_FOLDER_NAME_ALREADY_EXISTS);
					return;
				}

				if (parent is TreeViewItem)
				{
					var parentFolder = (parent as TreeViewItem).Header as BFFolder;
					var itemToChange = parentFolder.DirItems.FirstOrDefault(x => x.Name.Equals(item.dirItem.Name) && x.Path.Equals(item.dirItem.Path));
					if (itemToChange != null)
					{
						itemToChange.Name = foldername;
						itemToChange.Path = foldername;
					}
				}
				else if (parent is TreeView)
				{
					var itemToChange = Favorites.FirstOrDefault(x => x.Name.Equals(item.dirItem.Name) && x.Path.Equals(item.dirItem.Path));
					if (itemToChange != null)
					{
						itemToChange.Name = foldername;
						itemToChange.Path = foldername;
					}
				}

				tvFavorites.Items.Refresh();
				mControllerPresenter.SaveFavoritesTreeToFile();
			}
		}

		/// <summary>
		/// Deletes a folder from the Favorites tree, each folder must be unique to allow deletion of folders because we need to be able to identify which folder to delete.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFavoritesDeleteItem_Click(object sender, RoutedEventArgs e)
		{
			if (mTreeItemSelectionList_Favorites != null && mTreeItemSelectionList_Favorites.Count() > 0)
			{
				if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_SCHEDULER_DELETE_FAVORITES, "Delete") == MessageBoxResult.Yes)
				{
					foreach (var item in mTreeItemSelectionList_Favorites.ToList())
					{
						var parent = GetTreeViewItemParent(item.tvi);
						if (parent is TreeViewItem)
						{
							var parentFolder = (parent as TreeViewItem).Header as BFFolder;
							var itemToRemove = parentFolder.DirItems.Where(x => x.Name.Equals(item.dirItem.Name)).FirstOrDefault();
							parentFolder.DirItems.Remove(itemToRemove);
						}
						else if (parent is TreeView)
						{
							var itemToRemove = Favorites.Where(x => x.Name.Equals(item.dirItem.Name)).FirstOrDefault();
							Favorites.Remove(itemToRemove);
						}
					}

					mTreeItemSelectionList_Favorites.Clear();
					tvFavorites.Items.Refresh();
					mControllerPresenter.SaveFavoritesTreeToFile();

					//if currently searching - refresh search list after drop
					if (!string.IsNullOrEmpty(txtSearchFavorites.Text))
					{
						SearchFavorites(txtSearchFavorites.Text);
						tvFavorites.DataContext = FilteredFavorites;
					}
					ToggleTreeViewButtons();
				}
			}
		}

		//--Test Lineup
		private void toggleBtnEditMode_Checked(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dgTestLineup != null)
				{
					dgLastRunResult.Visibility = Visibility.Collapsed;
					dgTestLineup.Visibility = Visibility.Visible;
				}

				SetLineupButtonIsEnabled();
				mIsEditMode = true;
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void toggleBtnEditMode_Unchecked(object sender, RoutedEventArgs e)
		{
			try
			{
				dgLastRunResult.Visibility = Visibility.Visible;
				dgTestLineup.Visibility = Visibility.Collapsed;

				SetLineupButtonIsEnabled();
				mIsEditMode = false;
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void dgTestLineup_Drop(object sender, DragEventArgs e)
		{
			if (!HasEnvironment(QUEUE_FAILED) || mIsSchedulerFileReadonly)
				return;

			string source = e.Data.GetData("dragSource") as string;
			List<TreeViewSelectedItem> mList = new List<TreeViewSelectedItem>();

			if (source == "favorites")
				mList = mTreeItemSelectionList_Favorites;
			else if (source == "allsuites")
				mList = mTreeItemSelectionList_AllSuites;

			foreach (var item in mList)
			{
				var kwDirItem = item.dirItem; 
				if (kwDirItem != null && kwDirItem is BFFile)
				{
					QueueSelectedTreeItem(kwDirItem);                    
				}
				else if (kwDirItem != null && kwDirItem is BFFolder)
				{
					var folder = kwDirItem as BFFolder;
					IterateFolder(folder, QueueSelectedTreeItem);
				}
			}

			SetLineupButtonIsEnabled();
			CheckQueueEnvironmentsForBlacklisted();
		}

		private void dgTestLineup_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;
			if (dgTestLineup.Columns.IndexOf(e.Column) == COL_INDEX_STARTTIME)
			{
				ContentPresenter presenter = e.Column.GetCellContent(e.Row) as ContentPresenter;
				TimePickerCtrl tpckr = presenter.ContentTemplate.FindName("tpckrStartTime", presenter) as TimePickerCtrl;

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

			if (tlr.GroupID == tlr.Id)
			{
				foreach (var record in TestLineup.Where(x => x.GroupID == tlr.GroupID))
				{
					switch (dgTestLineup.Columns.IndexOf(e.Column))
					{
						case COL_INDEX_STARTTIME:
							record.StartTime = tlr.StartTime;
							break;
						case COL_INDEX_SCHEDULE:
							record.Schedule = tlr.Schedule;
							break;
						default:
							break;
					}
				}
			}
		}

		private void dgTestLineup_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
		{
			if (dgTestLineup.Columns.IndexOf(e.Column) == COL_INDEX_STARTTIME)
			{
				ContentPresenter presenter = e.Column.GetCellContent(e.Row) as ContentPresenter;
				TimePickerCtrl tpckr = presenter.ContentTemplate.FindName("tpckrStartTime", presenter) as TimePickerCtrl;
				TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;

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

		private void dgTestLineupAgent_DropDownClosed(object sender, EventArgs e)
		{
			TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;
			if (tlr.GroupID == tlr.Id)
			{
				foreach (var record in TestLineup.Where(x => x.GroupID == tlr.GroupID))
				{
					IAgentList agent = AvailableAgents.FirstOrDefault(x => x != null && x.Name == tlr.RunningAgent.Name);
					if (agent == null)
						agent = AgentsGroupPool.FirstOrDefault(x => x.Name == tlr.RunningAgent.Name);

					record.RunningAgent = agent;
				}
			}
		}

		private void dgTestLineupEnvironment_DropDownClosed(object sender, EventArgs e)
		{
			TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;
			if (tlr.Environment == TestLineupRecord.DEFAULT_ENVIRONMENT)
			{
				tlr.IsBlacklisted = false;
				tlr.NonDefaultBlacklistedEnvironment = String.Empty;
			}
			CheckQueueEnvironmentsForBlacklisted();
		}

		private void dgTestLineupBrowser_DropDownClosed(object sender, EventArgs e)
		{
			TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;
			if (tlr.GroupID == tlr.Id)
			{
				foreach (var record in TestLineup.Where(x => x.GroupID == tlr.GroupID))
				{
					record.Browser = tlr.Browser;
				}
			}
			CheckQueueEnvironmentsForBlacklisted();
		}

		private void dgTestLineupRecurrence_DropDownClosed(object sender, EventArgs e)
		{
			TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;
			if (tlr.GroupID == tlr.Id)
			{
				foreach (var record in TestLineup.Where(x => x.GroupID == tlr.GroupID))
				{
					record.Recurrence = tlr.Recurrence;
				}
			}
			if (mIsBlacklistWarningDisplayed)
			{
				mIsBlacklistWarningDisplayed = false;
			}
		}

		private void LineupEnabled_Click(object sender, RoutedEventArgs e)
		{
			TestLineupRecord tlr = dgTestLineup.SelectedItem as TestLineupRecord;
			if (tlr.GroupID == tlr.Id)
			{
				foreach (var record in TestLineup.Where(x => x.GroupID == tlr.GroupID))
				{
					if (record.Dependent == "false") // no condition
					{
						record.Enabled = tlr.Enabled;
					}
					else
					{
						record.Enabled = (tlr.Enabled && !record.Execute) ? record.Enabled : tlr.Enabled;
					}
				}
			}
			CheckQueueEnvironmentsForBlacklisted();
		}

		private void dgTestLineup_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if (dgTestLineup.SelectedItems.Count > 0)
			{
				var lastSelectedLineUp = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().Last();

				SetLineupButtonIsEnabled();
				ColorPickerMenu.SelectedColor = lastSelectedLineUp.RowColor;
			}

			if (dgTestLineup.SelectedItems.Count == 1) /* load history only if 1 row selected to eliminate unneccesary load time */
			{
				GetControllerSuiteHistory();
			}

			if (chkControllerHistoryFilter.IsChecked == true)
			{
				if (!mControllerPresenter.FilterControllerHistory(dtControllerHistoryFrom.Text, dtControllerHistoryTo.Text))
				{
					DlkUserMessages.ShowWarning(DlkUserMessages.INF_OVERLAP_DATE);
					dtControllerHistoryFrom.Text = "";
					dtControllerHistoryTo.Text = "";
				}
			}
		}

		private void dgTestLineupContextMenu_Opening(object sender, ContextMenuEventArgs e)
		{
			var selectedLineups = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();
			//menu is ungroup if selected items are all in the same group id
			bool ungroup = selectedLineups.All(x => x.IsInGroup) && selectedLineups.Select(x => x.GroupID).Distinct().Count() == 1;
			GroupLineUpMenu.Header = ungroup ? "Ungroup" : "Group";
			//disable grouping if selected only 1 item
			GroupLineUpMenu.IsEnabled = ungroup ? true : selectedLineups.Count > 1;
			//dont allow grouping if readonly
			if (mIsSchedulerFileReadonly)
				GroupLineUpMenu.IsEnabled = false;
			SetConditionMenu.IsEnabled = ungroup;

			//Run Now context menu
			var currentSched = selectedLineups.First();
			if (selectedLineups.Any(x => x.Status == Enumerations.TestStatus.Running || x.Status == Enumerations.TestStatus.Pending))
			{
				RunNowMenu.Header = RUN_NOW_CANCEL;
				RunNowMenu.IsEnabled = true;
				EditMenu.IsEnabled = false;
			}
			else
			{
				bool hasGroupMember = selectedLineups.Any(x => x.IsInGroup);
				bool IsDifferentGroup = false;
				if (hasGroupMember)
				{
					string groupID = selectedLineups.First().GroupID;
					IsDifferentGroup = selectedLineups.Any(x => x.GroupID != groupID);
				}
				
				RunNowMenu.Header = RUN_NOW;
				RunNowMenu.IsEnabled = selectedLineups.Count == 1 &&        //more than 1 selected 
							!(currentSched.Status == Enumerations.TestStatus.Running || currentSched.Status == Enumerations.TestStatus.Pending || currentSched.Status == Enumerations.TestStatus.Cancelling) &&  //current item is not running
							!IsAnyItemInGroupRunning(currentSched);       //group is not running
				EditMenu.IsEnabled = !(currentSched.Status == Enumerations.TestStatus.Running || currentSched.Status == Enumerations.TestStatus.Pending || currentSched.Status == Enumerations.TestStatus.Cancelling) &&  //current item is not running
							!IsAnyItemInGroupRunning(currentSched) && !mIsSchedulerFileReadonly //group is not running
							&& (hasGroupMember ? !IsDifferentGroup : true);     //only same group members allowed. 
			}

			ColorPickerMenu.IsEnabled = selectedLineups.Count > 0 ? true : false;
		}

		private void GroupLineUpMenu_Click(object sender, RoutedEventArgs e)
		{
			IsSaveInProgress = true;
			var selectedLineups = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();

			//If one of the test is currently on pending, running or cancelling, stop grouping 

			int pending = 0;
			int running = 0;
			int cancelling = 0;

			foreach(var sl in selectedLineups)
            {
				if (sl.Status == Enumerations.TestStatus.Pending) pending++;
				if (sl.Status == Enumerations.TestStatus.Running) running++;
				if (sl.Status == Enumerations.TestStatus.Cancelling) cancelling++;
            }

			string issueList = string.Empty;

			if (running > 0) issueList = "running";
			else if (pending > 0) issueList = "pending";
			else if (cancelling > 0) issueList = "cancelling";

			if (pending > 0 || running > 0 || cancelling > 0)
			{
				IsSaveInProgress = false;
				DlkUserMessages.ShowInfo(string.Format(DlkUserMessages.INF_TEST_CANNOT_GROUP, issueList), "Grouping cannot proceed.");
				return;
			}

			if (selectedLineups.All(x => !x.IsInGroup))
			{
				//all items are not grouped -> group them together
				CreateGroupingForSelectedLineUp(selectedLineups);
			}
			else if (selectedLineups.Select(x => x.GroupID).Distinct().Count() == 2 && selectedLineups.Any(x => string.IsNullOrEmpty(x.GroupID)))
			{
				//append ungrouped item into another group
				var groupId = selectedLineups.First(x => x.IsInGroup).GroupID;
				AddIntoGrouping(groupId);
			}
			else if (selectedLineups.Select(x => x.GroupID).Distinct().Count() == 1)
			{
				//ungroup the whole group
				if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_SCHEDULER_DELETE_GROUP, "Delete Existing Group?") == MessageBoxResult.Yes)
				{
					UngroupGrouping(selectedLineups);
				}
			}
			else
			{
				//more than 1 group -> ungroup previous groups then group selected items
				if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_SCHEDULER_ADD_NEW_GROUP, "Save New Group?") == MessageBoxResult.Yes)
				{
					UngroupGrouping(selectedLineups);
					CreateGroupingForSelectedLineUp(selectedLineups);
				}
			}
			//refresh UI
			mControllerPresenter.RefreshLineUpImage();
			IsSaveInProgress = false;
		}

		private void SetConditionMenu_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<TestLineupRecord> selectedLineup = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();
				if (selectedLineup.Any(x => String.IsNullOrEmpty(x.GroupID))) // part of selected items is not part of any group
				{
					// error: part of selected is not part of any group
					return;
				}
				foreach (TestLineupRecord rec in selectedLineup)
				{
					if (selectedLineup.Any(x => x.GroupID != rec.GroupID)) // part of selected items is not part of the group
					{
						// error: choose only one group
						return;
					}
				}
				CreateSchedulerConditionDialog execConditionDlg = new CreateSchedulerConditionDialog(GetConditionGroupLineup(selectedLineup.First().GroupID), this);
				execConditionDlg.Owner = this;

				if ((bool)execConditionDlg.ShowDialog())
				{
					mControllerPresenter.RefreshLineUpImage();
				}
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void RunNowMenu_Click(object sender, RoutedEventArgs e)
		{
			//save to make sure run now is up to date
			if (!IsSaveInProgress)
			{
				this.mControllerPresenter.SaveLineup();
			}

			bool success = true;
			if (RunNowMenu.Header.ToString() == RUN_NOW)
			{
				TestLineupRecord suite = (TestLineupRecord)dgTestLineup.SelectedItem;
					success = mExecutePresenter.RunNow(suite, DateTime.Now.ToString());
			}
			else //cancel test
			{
				var runningLineup = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().Where(x => x.Status == Enumerations.TestStatus.Running || x.Status == Enumerations.TestStatus.Pending);
				//non grouped running items
				var listToCancel = runningLineup.Where(x => !x.IsInGroup);
				foreach (var item in listToCancel)
				{
					mExecutePresenter.CancelTest(item);
				}
				//grouped running items
				var listInGroupToCancel = runningLineup.Where(x => x.IsInGroup).GroupBy(x => x.GroupID).Select(x => x.First());
				foreach (var item in listInGroupToCancel)
				{
					mExecutePresenter.CancelTest(item);
				}
			}

			if (!success)
			{
				if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_ALL_SCHEDULER_AGENTS_ARE_BUSY) == MessageBoxResult.Yes)
				{
					mExecutePresenter.PendSchedulesForRunNow((TestLineupRecord)dgTestLineup.SelectedItem);
				}
			}
		}

		private void EditMenu_Click(object sender, RoutedEventArgs e)
		{
			var selectedItems = dgTestLineup.SelectedItems.Cast<TestLineupRecord>().ToList();			
			bool hasGroupMember = selectedItems.Any(x => x.IsInGroup);
			var rowEditorDialog = new TestLineupRowEditorDialog(this, selectedItems, AgentsListView, EnvironmentList, AllBrowsers, hasGroupMember);
			rowEditorDialog.Owner = this;
			rowEditorDialog.ShowDialog();
		}

		private void btnLineupEdit_Click(object sender, RoutedEventArgs e)
		{
			if (dgTestLineup.SelectedItems.Count >= 1)
			{
				TestLineupRecord record = (TestLineupRecord)dgTestLineup.SelectedItem;
				var suitePath = record.TestSuiteToRun.Path;

				if (string.IsNullOrEmpty(suitePath))
				{
					DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_FILE_TO_OPEN);
					return;
				}

				//show suite editor
				SuiteEditorForm suiteEditorForm;

				bool showSetupMessage = KeywordLoaderSingleton.Instance.IsKeywordDirectoriesNull();
				if (showSetupMessage)
				{
					Modal modalBox = new Modal(this, "Setting up suite editor...");
					modalBox.Show();
					suiteEditorForm = new SuiteEditorForm();
					modalBox.Close();
				}
				else
				{
					suiteEditorForm = new SuiteEditorForm();
				}

				suiteEditorForm.Owner = this;
				suiteEditorForm.LoadedSuite = suitePath;
				suiteEditorForm.ShowDialog();
			}
		}

		private void btnLineupDelete_Click(object sender, RoutedEventArgs e)
		{
			IsSaveInProgress = true;

			if (dgTestLineup.SelectedItem != null)
			{
				string suiteName = "these suites";
				if (dgTestLineup.SelectedItems.Count == 1)
				{
					TestLineupRecord record = (TestLineupRecord)dgTestLineup.SelectedItem;
					suiteName = record.TestSuiteToRun.Name;
				}

				if (DlkUserMessages.ShowQuestionYesNo(string.Format(DlkUserMessages.ASK_SCHEDULER_REMOVE_LINEUP, suiteName), "Delete schedule") == MessageBoxResult.Yes)
				{
					var uniqueGroupIds = new List<string>();
					while (dgTestLineup.SelectedItems.Count > 0)
					{
						TestLineupRecord record = (TestLineupRecord)dgTestLineup.SelectedItem;
						if (record.Status != Enumerations.TestStatus.Pending && record.Status != Enumerations.TestStatus.Running && !IsAnyItemInGroupRunning(record))
						{
							if (record.IsInGroup && !uniqueGroupIds.Contains(record.GroupID))
							{
								uniqueGroupIds.Add(record.GroupID); //store unique groupids
							}
							TestLineup.Remove(record);
						}
						else
						{
							DataGridRow row = dgTestLineup.ItemContainerGenerator.ContainerFromItem(dgTestLineup.SelectedItem) as DataGridRow;
							row.IsSelected = false;
						}
					}

					//refresh groupings
					foreach (var groupid in uniqueGroupIds)
					{
						var groupItems = TestLineup.Where(x => x.GroupID == groupid).ToList();
						if (groupItems != null)
						{
							if (groupItems.Count() == 1)
								UngroupGrouping(groupItems); 
							else if (groupItems.Count() > 1)
								CreateGroupingForSelectedLineUp(groupItems); 
						}
					}
					if (uniqueGroupIds.Count > 0)
						mControllerPresenter.RefreshLineUpImage();

					//reset history grid
					mTestSuiteExecutionHistory.Clear();

					SetLineupButtonIsEnabled();
				}
			}

			IsSaveInProgress = false;
		}

		private void btnLineupUp_Click(object sender, RoutedEventArgs e)
		{
			IsSaveInProgress = true;

			var itemSelected = (TestLineupRecord)dgTestLineup.SelectedItem;
			var indexSelected = TestLineup.IndexOf(itemSelected);

			//Cannot move up - more than 1 item selected, is running/pending, first item in grid 
			if (dgTestLineup.SelectedItems.Count != 1 ||
				itemSelected.Status == Enumerations.TestStatus.Running ||
				indexSelected == 0 ||
				IsAnyItemInGroupRunning(itemSelected, false))
			{
				IsSaveInProgress = false;
				return;
			}

			//move whole group up
			if (itemSelected.Id == itemSelected.GroupID)
			{
				if (TestLineup[indexSelected - 1].IsInGroup)
				{
					//change position with another group
					var topGroupIndex = TestLineup.IndexOf(TestLineup.First(x => x.GroupID == TestLineup[indexSelected - 1].GroupID));
					MoveGroupPosition(TestLineup[indexSelected].GroupID, topGroupIndex);
				}
				else
				{
					MoveGroupPosition(TestLineup[indexSelected].GroupID, indexSelected - 1);
				}

				IsSaveInProgress = false;
				return;
			}

			//swap with group above
			if (TestLineup[indexSelected - 1].IsInGroup && !TestLineup[indexSelected].IsInGroup)
			{
				//swap individual item position with a group
				MoveGroupPosition(TestLineup[indexSelected - 1].GroupID, indexSelected);

				IsSaveInProgress = false;
				return;
			}

			//move single item up as group head
			if (itemSelected.IsInGroup && TestLineup[indexSelected - 1].Id == TestLineup[indexSelected - 1].GroupID)
			{
				SetAsGroupHead(itemSelected);

				IsSaveInProgress = false;
				return;
			}

			//move individual item up
			if (itemSelected.IsInGroup)
			{
				//move within a group
				TestLineup.Move(indexSelected, indexSelected - 1);
				var temp = TestLineup[indexSelected].GroupImage;
				TestLineup[indexSelected].GroupImage = TestLineup[indexSelected - 1].GroupImage;
				TestLineup[indexSelected - 1].GroupImage = temp;
			}
			else
			{
				//check pending scheudles to make sure presenter agrees with us
				if (TrySwitchPendingSchedules(TestLineup[indexSelected], TestLineup[indexSelected - 1]))
				{
					TestLineup.Move(indexSelected, indexSelected - 1);
				}
			}

			IsSaveInProgress = false;
		}

		private void btnLineupDown_Click(object sender, RoutedEventArgs e)
		{
			IsSaveInProgress = true;

			var itemSelected = (TestLineupRecord)dgTestLineup.SelectedItem;
			var indexSelected = TestLineup.IndexOf(itemSelected);

			//Cannot move down - more than 1 item selected, is running/pending, last item in grid 
			if (dgTestLineup.SelectedItems.Count != 1 ||
				itemSelected.Status == Enumerations.TestStatus.Running ||
				indexSelected == TestLineup.Count - 1 ||
				IsAnyItemInGroupRunning(itemSelected, false) ||
				(itemSelected.IsInGroup && itemSelected == TestLineup.Last(x => x.GroupID == itemSelected.GroupID))) //do not move down if last item of a group
			{
				IsSaveInProgress = false;
				return;
			}

			//move whole group down
			if (itemSelected.Id == itemSelected.GroupID)
			{
				//move whole group
				var nextIndex = TestLineup.IndexOf(TestLineup.Last(x => x.GroupID == TestLineup[indexSelected].GroupID)) + 1;
				if (TestLineup.Count > nextIndex)
				{
					if (TestLineup[nextIndex].IsInGroup)
					{
						//change position with another group
						MoveGroupPosition(TestLineup[nextIndex].GroupID, indexSelected);
					}
					else
					{
						MoveGroupPosition(TestLineup[indexSelected].GroupID, nextIndex);
					}
				}

				IsSaveInProgress = false;
				return;
			}

			//swap with group below
			if (TestLineup[indexSelected + 1].IsInGroup && !TestLineup[indexSelected].IsInGroup)
			{
				MoveGroupPosition(TestLineup[indexSelected + 1].GroupID, indexSelected);

				IsSaveInProgress = false;
				return;
			}

			//move individual item down
			if (itemSelected.IsInGroup)
			{
				//move within a group
				TestLineup.Move(indexSelected, indexSelected + 1);
				var temp = TestLineup[indexSelected].GroupImage;
				TestLineup[indexSelected].GroupImage = TestLineup[indexSelected + 1].GroupImage;
				TestLineup[indexSelected + 1].GroupImage = temp;
			}
			else
			{
				//check pending scheudles to make sure presenter agrees with us
				if (TrySwitchPendingSchedules(TestLineup[indexSelected], TestLineup[indexSelected + 1]))
				{
					TestLineup.Move(indexSelected, indexSelected + 1);
				}
			}

			IsSaveInProgress = false;
		}

		private void btnLineupLoad_Click(object sender, RoutedEventArgs e)
		{
			//pause background worker
			PauseSchedulerWorker();

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Xml files (*.xml)|*.xml";
			openFileDialog.InitialDirectory = Path.Combine(DlkEnvironment.mDirProductsRoot, "Common\\Scheduler");
			if (openFileDialog.ShowDialog() == true)
			{
				if (EnvironmentList.Count == 1)
				{
					DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, "Queue failed");
					_busy.Set();
					return;
				}

				var fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
				txtSchedulerFileName.Text = string.Format("({0})", fileName);
				DlkAdvancedSchedulerFileHandler.SetSchedulesFilePath(openFileDialog.FileName);
				mControllerPresenter.LoadLineup();
				dgTestLineup.ItemsSource = TestLineup;
				dgLastRunResult.ItemsSource = TestLineup;

				CheckIfSchedulerIsReadonly();

				//if no agents in pool, set all loaded lineup agents to local machine
				if (AgentsPool.Count == 0)
					mControllerPresenter.ChangeAgentsInAllLineup(AvailableAgents.FirstOrDefault(x => x.Name == Agent.LOCAL_MACHINE));
			}

			//resume background worker
			_busy.Set();
		}

		private void btnNewLineUp_Click(object sender, RoutedEventArgs e)
		{
			//pause background worker
			PauseSchedulerWorker();

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Save New As";
			saveFileDialog.Filter = "Xml files (*.xml)|*.xml";
			saveFileDialog.InitialDirectory = Path.Combine(DlkEnvironment.mDirProductsRoot, "Common\\Scheduler");
			if (saveFileDialog.ShowDialog() == true)
			{
				var fileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
				txtSchedulerFileName.Text = string.Format("({0})", fileName);
				DlkAdvancedSchedulerFileHandler.SetSchedulesFilePath(saveFileDialog.FileName);
				mControllerPresenter.LoadLineup();
				dgTestLineup.ItemsSource = TestLineup;
				dgLastRunResult.ItemsSource = TestLineup;
			}

			//resume background worker
			_busy.Set();
		}

		private void btnLineupSaveAs_Click(object sender, RoutedEventArgs e)
		{
			//pause background worker
			PauseSchedulerWorker();

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Xml files (*.xml)|*.xml";
			saveFileDialog.InitialDirectory = Path.Combine(DlkEnvironment.mDirProductsRoot, "Common\\Scheduler");
			if (saveFileDialog.ShowDialog() == true)
			{
				var fileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
				txtSchedulerFileName.Text = string.Format("({0})", fileName);
				DlkAdvancedSchedulerFileHandler.SetSchedulesFilePath(saveFileDialog.FileName);
			}

			//resume background worker
			_busy.Set();
		}

		/// <summary>
		/// Handler for History grid row double click event
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void dgTestHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left) // don't do Right double-click
			{
				ViewHistoryResults(dgTestHistory);
			}
		}

		private void chkControllerHistoryFilter_Click(object sender, RoutedEventArgs e)
		{
			if (chkControllerHistoryFilter.IsChecked == true)
			{
				dtControllerHistoryFrom.IsEnabled = true;
				dtControllerHistoryTo.IsEnabled = true;
			}
			else
			{
				dtControllerHistoryFrom.IsEnabled = false;
				dtControllerHistoryTo.IsEnabled = false;

				dtControllerHistoryFrom.Text = "";
				dtControllerHistoryTo.Text = "";

				GetControllerSuiteHistory();
			}
		}

		private void dtControllerHistoryFrom_LostFocus(object sender, RoutedEventArgs e)
		{
			ValidateDateEntries(true, true);
		}

		private void dtControllerHistoryTo_LostFocus(object sender, RoutedEventArgs e)
		{
			ValidateDateEntries(false, true);
		}

		private void dtControllerHistoryFrom_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (dtControllerHistoryFrom.IsKeyboardFocusWithin)
			{
				mDateControllerFromOriginalString = dtControllerHistoryFrom.Text;
			}
		}

		private void dtControllerHistoryTo_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (dtControllerHistoryTo.IsKeyboardFocusWithin)
			{
				mDateControllerToOriginalString = dtControllerHistoryTo.Text;
			}
		}

		/// <summary>
		/// Handler for History grid View results context menu item
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void mnuViewDetails_Click(object sender, RoutedEventArgs e)
		{
			ViewHistoryResults(dgTestHistory);
		}

		/// <summary>
		/// Handler for History grid in deleting item/s
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void mnuDeleteHistoryItem_Click(object sender, RoutedEventArgs e)
		{
			if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_HISTORY_ITEM, "Delete") == MessageBoxResult.Yes)
			{
				TestLineupRecord suite = (TestLineupRecord)dgTestLineup.SelectedItem;
				foreach (ExecutionHistory item in dgTestHistory.SelectedItems.Cast<ExecutionHistory>().ToList())
				{
					mControllerPresenter.DeleteHistoryGridItem(suite, item);
				}
			}
		}

		//agents page
		private void btnAgentAdd_Click(object sender, RoutedEventArgs e)
		{
			var inputBox = new DialogBox("Add Agent", DlkUserMessages.INF_SCHEDULER_ENTER_AGENT_NAME, true) { Owner = this };
			if (inputBox.ShowDialog() == true)
			{
				if (string.IsNullOrEmpty(inputBox.TextBoxValue))
				{
					DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_SCHEDULER_EMPTY_AGENT_NAME, inputBox.TextBoxValue.ToUpper()), "Agent Name Error");
					return;
				}

				else if (!DlkString.IsAgentNameValid(inputBox.TextBoxValue))
				{
					DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_INVALID_AGENT_NAME, "Agent Name Error");
					return;
				}

				Agent agent = new Agent(inputBox.TextBoxValue, Agent.AGENT_TYPE_NETWORK)
				{
					Id = inputBox.TextBoxValue,
					Status = Enumerations.AgentStatus.Offline
				};

				if (mAgentsPool.Any(item => String.Equals(item.Name, inputBox.TextBoxValue, StringComparison.OrdinalIgnoreCase)))
				{
					DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_SCHEDULER_DUPLICATE_AGENT_NAME, inputBox.TextBoxValue.ToUpper()), "Agent Name Error");
				}
				else if (mAgentsGroupPool.Any(item => String.Equals(item.Name, inputBox.TextBoxValue, StringComparison.OrdinalIgnoreCase)) ||
							String.Equals(Agent.ANY_AGENT_NAME, inputBox.TextBoxValue, StringComparison.OrdinalIgnoreCase) ||
							String.Equals(Agent.LOCAL_MACHINE, inputBox.TextBoxValue, StringComparison.OrdinalIgnoreCase))
				{
					DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_SCHEDULER_CONFLICT_AGENT_NAME, inputBox.TextBoxValue.ToUpper()), "Agent Name Error");
				}
				else
				{
					mAgentsPool.Add(agent);
					mAgentPresenter.SaveAgents();
				}
			}

			ToggleAgentLineupButtons();
		}

		private void btnAgentDelete_Click(object sender, RoutedEventArgs e)
		{
			if (dgAgentLineup.SelectedItems.Count > 0)
			{
				var selectedAgents = dgAgentLineup.SelectedItems.Cast<Agent>().Where(x => x.Status != Enumerations.AgentStatus.Busy && x.Status != Enumerations.AgentStatus.Reserved);
				var totalItems = selectedAgents.Count();

				if (totalItems > 0)
				{
					if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_REMOVE_AGENT, "Delete Confirmation") == MessageBoxResult.Yes)
					{
						foreach (var agent in selectedAgents.ToList())
						{
							//remove agent
							var success = mExecutePresenter.RemoveAgent(agent);
							if (success)
							{
								mControllerPresenter.ChangeAgentsInLineup(agent.Name, AvailableAgents.FirstOrDefault(x => x.Name == Agent.LOCAL_MACHINE));
							}
						}

						if (mAgentsPool.Count == 0)
						{
							//if no agents left in pool, set all agents to local machine
							mControllerPresenter.ChangeAgentsInAllLineup(AvailableAgents.FirstOrDefault(x => x.Name == Agent.LOCAL_MACHINE));
						}

						mAgentPresenter.SaveAgents();
					}
				}
				else
				{
					DlkUserMessages.ShowError(DlkUserMessages.ERR_DELETE_USED_AGENT);
				}
			}

			ToggleAgentLineupButtons();
		}

		private void btnAgentEnable_Click(object sender, RoutedEventArgs e)
		{
			if (dgAgentLineup.SelectedItems.Count > 0)
			{
				var selectedAgents = dgAgentLineup.SelectedItems.Cast<Agent>().Where(x => x.Status != Enumerations.AgentStatus.Busy && x.Status != Enumerations.AgentStatus.Reserved);
				var totalItems = selectedAgents.Count();

				if (totalItems > 0)
				{
					var canEnableCount = selectedAgents.Count(x => x.Status == Enumerations.AgentStatus.Disabled);
					if (canEnableCount == totalItems)
					{
						mExecutePresenter.SetAgentsDisabled(selectedAgents, false);
					}
					else if (canEnableCount == 0)
					{
						mExecutePresenter.SetAgentsDisabled(selectedAgents, true);
					}
					else
					{
						var reply = DlkUserMessages.ShowQuestionYesNoCancel(DlkUserMessages.ASK_DISABLE_AGENT, "Enable/Disable agents");
						if (reply == MessageBoxResult.Yes)
						{
							mExecutePresenter.SetAgentsDisabled(selectedAgents.Where(x => x.Disabled), false);
						}
						else if (reply == MessageBoxResult.No)
						{
							mExecutePresenter.SetAgentsDisabled(selectedAgents.Where(x => !x.Disabled), true);
						}
					}

					mAgentPresenter.SaveAgents();
				}
				else
				{
					DlkUserMessages.ShowError(DlkUserMessages.ERR_DISABLE_USED_AGENT);
				}
			}
		}

		private void btnAgentGetLatest_Click(object sender, RoutedEventArgs e)
		{
			if (dgAgentLineup.SelectedItems.Count > 0)
			{
				var selectedAgents = dgAgentLineup.SelectedItems.Cast<Agent>().Where(x => x.Status == Enumerations.AgentStatus.Ready ||
																							x.Status == Enumerations.AgentStatus.Warning ||
																							x.Status == Enumerations.AgentStatus.Error);

				if (selectedAgents.Count() > 0)
				{
					mExecutePresenter.GetLatestAgents(selectedAgents, DlkEnvironment.mProductFolder);
				}
			}
		}

		private void btnAgentGroupAdd_Click(object sender, RoutedEventArgs e)
		{
			var inputBox = new DialogBox("Add Agent Group", DlkUserMessages.INF_SCHEDULER_NEW_AGENT_GROUP_NAME, true) { Owner = this };
			if (inputBox.ShowDialog() == true)
			{
				if (ValidateAgentGroupName(inputBox.TextBoxValue))
				{
					AgentGroup group = new AgentGroup(inputBox.TextBoxValue);
					mAgentsGroupPool.Add(group);
					mAgentPresenter.SaveAgents();
				}
			}
		}

		private void btnAgentGroupEdit_Click(object sender, RoutedEventArgs e)
		{
			var selectedGroup = dgAgentGroups.SelectedItem as AgentGroup;
			if (selectedGroup != null && selectedGroup.Name != AgentGroup.ALL_GROUP_NAME)
			{
				textblockAgentGroupName.Visibility = System.Windows.Visibility.Collapsed;
				panelAgentGroupName.Visibility = System.Windows.Visibility.Visible;
				txtboxAgentGroupName.Text = selectedGroup.Name;
				//agent groups ui
				dgAgentGroups.IsEnabled = false;
				btnAgentGroupAdd.IsEnabled = false;
				btnAgentGroupEdit.IsEnabled = false;
				btnAgentGroupDelete.IsEnabled = false;
				//edit mode
				FilterAgentGrid(selectedGroup, true);
				dgAgentLineup.Columns[0].Visibility = System.Windows.Visibility.Visible;
			}
		}

		private void btnAgentGroupSave_Click(object sender, RoutedEventArgs e)
		{
			var selectedGroup = dgAgentGroups.SelectedItem as AgentGroup;
			if (selectedGroup != null && selectedGroup.Name != AgentGroup.ALL_GROUP_NAME)
			{
				//save agent group name
				if (selectedGroup.Name.Equals(txtboxAgentGroupName.Text) 
					||ValidateAgentGroupName(txtboxAgentGroupName.Text))
				{
					var originalName = selectedGroup.Name;
					selectedGroup.Name = txtboxAgentGroupName.Text;
					//reset schedules with selected group
					mControllerPresenter.ChangeAgentsInLineup(originalName, selectedGroup);
				}
				else
				{
					return;
				}
				//update group members
				selectedGroup.Members = mAgentsFilteredPool.Where(x => x.IsInGroup).Select(x => x.Name).ToList();
				//save
				mAgentPresenter.SaveAgents();

				//reset ui
				textblockAgentGroupName.Visibility = System.Windows.Visibility.Visible;
				textblockAgentGroupName.Text = selectedGroup.Name;
				panelAgentGroupName.Visibility = System.Windows.Visibility.Collapsed;
				//agent groups ui
				dgAgentGroups.IsEnabled = true;
				btnAgentGroupAdd.IsEnabled = true;
				btnAgentGroupEdit.IsEnabled = true;
				btnAgentGroupDelete.IsEnabled = true;
				//turn off edit mode
				FilterAgentGrid(selectedGroup, false);
				dgAgentLineup.Columns[0].Visibility = System.Windows.Visibility.Collapsed;

				//prompt
				DlkUserMessages.ShowInfo(DlkUserMessages.INF_SCHEDULER_UPDATE_AGENT_GROUP);
			}
		}

		private void btnAgentGroupCancel_Click(object sender, RoutedEventArgs e)
		{
			var selectedGroup = dgAgentGroups.SelectedItem as AgentGroup;
			if (selectedGroup != null && selectedGroup.Name != AgentGroup.ALL_GROUP_NAME)
			{
				//reset ui
				textblockAgentGroupName.Visibility = System.Windows.Visibility.Visible;
				textblockAgentGroupName.Text = selectedGroup.Name;
				panelAgentGroupName.Visibility = System.Windows.Visibility.Collapsed;
				//agent groups ui
				dgAgentGroups.IsEnabled = true;
				btnAgentGroupAdd.IsEnabled = true;
				btnAgentGroupEdit.IsEnabled = true;
				btnAgentGroupDelete.IsEnabled = true;
				//turn off edit mode
				FilterAgentGrid(selectedGroup, false);
				dgAgentLineup.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
			}
		}

		private void btnAgentGroupDelete_Click(object sender, RoutedEventArgs e)
		{
			var selectedGroup = dgAgentGroups.SelectedItem as AgentGroup;
			if (selectedGroup != null && selectedGroup.Name != AgentGroup.ALL_GROUP_NAME)
			{
				if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_REMOVE_AGENT_GROUP, "Delete Confirmation") == MessageBoxResult.Yes)
				{
					if (mTestLineup.Any(x => x.RunningAgent.AgentType == AgentGroup.AGENT_TYPE_GROUPS && x.RunningAgent.Name == selectedGroup.Name &&
											(x.Status == Enumerations.TestStatus.Pending || x.Status == Enumerations.TestStatus.Running)))
					{
						DlkUserMessages.ShowError(DlkUserMessages.ERR_DELETE_USED_AGENT_GROUP);
						return;
					}

					mAgentsGroupPool.Remove(mAgentsGroupPool.FirstOrDefault(x => x.Name == selectedGroup.Name));
					mAgentPresenter.SaveAgents();

					//return selection to ALL
					dgAgentGroups.SelectedIndex = 0;

					//reset schedules with selected group
					mControllerPresenter.ChangeAgentsInLineup(selectedGroup.Name, AvailableAgents.FirstOrDefault(x => x.Name == Agent.LOCAL_MACHINE));
				}
			}
		}

		private void dgAgentGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedGroup = dgAgentGroups.SelectedItem as AgentGroup;
			if (selectedGroup != null)
			{
				if (selectedGroup.Name == AgentGroup.ALL_GROUP_NAME)
				{
					btnAgentGroupEdit.Visibility = System.Windows.Visibility.Collapsed;
					btnAgentGroupDelete.Visibility = System.Windows.Visibility.Collapsed;
					dgAgentLineup.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
					btnAgentAdd.Visibility = System.Windows.Visibility.Visible;
					btnAgentDelete.Visibility = System.Windows.Visibility.Visible;
					textblockAgentGroupName.Text = "Agents";

					//show the complete list of agent
					dgAgentLineup.ItemsSource = AgentsPool;
				}
				else
				{
					btnAgentGroupEdit.Visibility = System.Windows.Visibility.Visible;
					btnAgentGroupDelete.Visibility = System.Windows.Visibility.Visible;
					btnAgentAdd.Visibility = System.Windows.Visibility.Collapsed;
					btnAgentDelete.Visibility = System.Windows.Visibility.Collapsed;
					textblockAgentGroupName.Text = selectedGroup.Name;

					//filter the agents in group
					FilterAgentGrid(selectedGroup, false);
				}
			}
		}

		/// <summary>
		/// Handler for selection changed event of Agents grid
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void dgAgentLineup_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			GetAgentHistory();
			ToggleAgentLineupButtons();
		}

		/// <summary>
		/// Handler for Agent History grid View results context menu item
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void mnuViewAgentResultsDetails_Click(object sender, RoutedEventArgs e)
		{
			ViewHistoryResults(dgAgentHistory);
		}

		/// <summary>
		/// Handler for deleting an item in Agent history list
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event argument</param>
		private void mnuDeleteAgentHistoryItem_Click(object sender, RoutedEventArgs e)
		{
			if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_HISTORY_ITEM, "Delete") == MessageBoxResult.Yes)
			{
				Agent agent = dgAgentLineup.SelectedItem as Agent;
				mAgentPresenter.DeleteHistoryGridItem(dgAgentHistory.SelectedItems.Cast<ExecutionHistory>().ToList());
				dgAgentHistory.ItemsSource = agent.ResultsHistory;
			}
		}

		/// <summary>
		/// Handler for Agent History grid row double click event
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void dgAgentHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left) // don't do Right double-click
			{
				ViewHistoryResults(dgAgentHistory);
			}
		}

		/// <summary>
		/// Handler in filtering history based on date input
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">Event arguments</param>
		private void chkAgentHistoryFilter_Click(object sender, RoutedEventArgs e)
		{
			if (chkAgentHistoryFilter.IsChecked == true)
			{
				dtAgentHistoryFrom.IsEnabled = true;
				dtAgentHistoryTo.IsEnabled = true;
			}
			else
			{
				dtAgentHistoryFrom.IsEnabled = false;
				dtAgentHistoryTo.IsEnabled = false;

				dtAgentHistoryFrom.Text = "";
				dtAgentHistoryTo.Text = "";

				GetAgentHistory();
			}
		}

		private void dtAgentHistoryFrom_LostFocus(object sender, RoutedEventArgs e)
		{
			ValidateDateEntries(true, false);
		}

		private void dtAgentHistoryTo_LostFocus(object sender, RoutedEventArgs e)
		{
			ValidateDateEntries(false, false);
		}

		private void dtAgentHistoryFrom_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (dtAgentHistoryFrom.IsKeyboardFocusWithin)
			{
				mDateAgentFromOriginalString = dtAgentHistoryFrom.Text;
			}
		}

		private void dtAgentHistoryTo_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (dtAgentHistoryTo.IsKeyboardFocusWithin)
			{
				mDateAgentToOriginalString = dtAgentHistoryTo.Text;
			}
		}

		/// <summary>
		/// This method will close the current dislayed status
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCloseMessage_Click(object sender, RoutedEventArgs e) => mTestLineupStatus.Clear();

		private void btnViewLogsLink_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string scheduleLogsPath = mTestLineupStatus.GetLogsLocation();
				ScheduleLogDetails scheduleLogDetails = new ScheduleLogDetails(scheduleLogsPath);
				scheduleLogDetails.ShowDialog();
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		private void btnSetSearchType_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				OpenContextMenu(sender);
			}
			catch (Exception ex)
			{
				DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
			}
		}

		/// <summary>
		/// Opens context menu of object
		/// </summary>
		/// <param name="buttonSender">Parent object of context menu</param>
		private void OpenContextMenu(object buttonSender)
		{
			var addButton = buttonSender as FrameworkElement;
			if (addButton != null)
			{
				addButton.ContextMenu.IsOpen = true;
			}
		}

		private void OnSchedulesChanged(object sender, FileSystemEventArgs e)
		{
			if (e.ChangeType != WatcherChangeTypes.Changed && !e.FullPath.Contains("LoginConfig.xml")) return;
			try
            {
				var schedDoc = XDocument.Load(e.FullPath);
				var envs = schedDoc.Descendants("login").ToList();
				if (envs.Count > 0)
				{
					Dictionary<string, string> notFoundEnv = new Dictionary<string, string>();
					foreach (var test in TestLineup)
					{
						var env = envs.FirstOrDefault(r => r.Attribute("id")?.Value == test.Environment);
						if (env == null && test.Environment != TestLineupRecord.DEFAULT_ENVIRONMENT)
						{
							if(!notFoundEnv.ContainsKey(test.TestSuiteToRun.Name))
								notFoundEnv.Add(test.TestSuiteToRun.Name, test.Environment);

							test.Environment = TestLineupRecord.DEFAULT_ENVIRONMENT;
						}
					}
					if (notFoundEnv.Count > 0)
						DlkUserMessages.ShowInfo(string.Format(DlkUserMessages.INF_SCHEDULE_SET_TO_DEFAULT,
							notFoundEnv.FirstOrDefault().Value,
							string.Join("\n", notFoundEnv.Select(nf => nf.Key))
						));
				}
			} catch { }
		}

		#endregion

		private class SuiteFile
		{
			/// <summary>
			/// Suite file name
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Suite full path
			/// </summary>
			public string FullName { get; set; }

			/// <summary>
			/// Suite file directory name
			/// </summary>
			public string DirectoryName { get; set; }

			/// <summary>
			/// Suite owner
			/// </summary>
			public string Owner { get; set; }

			/// <summary>
			/// Suite tags
			/// </summary>
			public List<DlkTag> Tags { get; set; } = new List<DlkTag>();
		}
	}

	public class DropdownBlacklistConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool ret = false;
			if (value.ToString() != TestLineupRecord.DEFAULT_ENVIRONMENT)
			{
				List<DlkLoginConfigRecord> mLoginRecords = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords.ToList();
				if (mLoginRecords.Any(x => x.mID == value.ToString())) // check if environment exists
				{
					if (DlkEnvironment.URLBlacklist != null)
					{
						DlkLoginConfigHandler mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, value.ToString());
						ret = DlkEnvironment.URLBlacklist.Any(x => DlkEnvironment.IsSameURL(mLoginConfigHandler.mUrl, x));
					}
				}
			}
			return (ret ? Visibility.Visible : Visibility.Hidden);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
