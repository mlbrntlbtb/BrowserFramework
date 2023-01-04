using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using Model = Recorder.Model;
using Recorder.Presenter;
using Recorder.View;
using System.Collections.Specialized;
using OpenQA.Selenium;
using TestRunner.Common;
using Recorder;
using System.Collections.ObjectModel;
using CommonLib.DlkControls;
using Recorder.Model;

namespace TestRunner.Recorder
{
    /// <summary>
    /// Interaction logic for TestCapture.xaml
    /// </summary>
    public partial class TestCapture : IMainView
    {
        #region PRIVATE MEMBERS
        private MainPresenter mPresenter;
        private List<Model.Action> mActionList = new List<Model.Action>();
        private Model.Action mNewAction;
        private Model.Step mNewStep = new Model.Step();
        private int mCurrentBlock = 0;
        private ObservableCollection<DlkActionRecord> mActionRecord = new ObservableCollection<DlkActionRecord>();
        private List<DlkTestStepRecord> mScriptRecord = new List<DlkTestStepRecord>();
        private string mCurrentWindow = string.Empty;
        private string prevHandler = string.Empty;
        bool _IsInit;
        private bool mIsVerify = false;
        private MainWindow _owner;
        //private List<Model.Variable> mVariables = new List<Model.Variable>();
        public DlkTest mLoadedTest;
        private String mLoadedFile = "";
        private static String mTemplate = DlkEnvironment.mDirTests + "template.dat";
        private bool isChanged = false;
        private String stepNoFailed = string.Empty;
        private DlkLoginConfigHandler mLoginConfigHandler;
        private bool bFilePathValid = true;
        private bool isIDDifferent = false;
        private string previousControlID = "";

        #endregion

        #region PROPERTIES
        public string PlaybackEnvironment = null;
        public string RecordingEnvironment = null;
        public string PlaybackBrowser = null;
        public int PlaybackInstance = 1;
        public MainPresenter Presenter
        {
            get
            {
                return mPresenter;
            }
        }


        public int CurrentBlock
        {
            get
            {
                return mCurrentBlock;
            }
            set
            {
                mCurrentBlock = value;
            }
        }

        private Model.Enumerations.VerifyType mVerifyType = Model.Enumerations.VerifyType.VerifyContent;
        public Model.Enumerations.VerifyType VerifyType
        {
            get
            {
                return mVerifyType;
            }
            set
            {
                mVerifyType = value;
            }
        }

        private Enumerations.GetValueType mGetValueType = Model.Enumerations.GetValueType.AssignValue;
        /// <summary>
        /// Get or Set the current assign value type option
        /// </summary>
        public Model.Enumerations.GetValueType GetValueType
        {
            get
            {
                return mGetValueType;
            }
            set
            {
                mGetValueType = value;
            }
        }

        private Model.Enumerations.ViewStatus mStatus = Model.Enumerations.ViewStatus.Default;
        public Model.Enumerations.ViewStatus ViewStatus
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (System.Action)(() =>
                {
                    switch (mStatus)
                    {
                        case Model.Enumerations.ViewStatus.ActionDetected:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_ACTION_DETECTED;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.ActionDetected_Continue:
                            this.Status.Text = Model.Constants.STATUS_READY;
                            this.SubStatus.Text = Model.Constants.STATUS_ACTION_DETECTED_BUT_CONT;
                            this.StatusBar.Background = Brushes.LightGreen;
                            break;
                        case Model.Enumerations.ViewStatus.Playback:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_TEST_PLAYBACK_ONGOING;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.AutoLogin:
                            this.Status.Text = Model.Constants.STATUS_AUTOLOGIN;
                            this.SubStatus.Text = string.Empty;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.Recording:
                            this.Status.Text = Model.Constants.STATUS_READY;
                            this.SubStatus.Text = Model.Constants.STATUS_WAITING_FOR_USER_INPUT;
                            this.StatusBar.Background = Brushes.LightGreen;
                            break;
                        case Model.Enumerations.ViewStatus.GetValueFinished:
                            this.Status.Text = Model.Constants.STATUS_READY;
                            this.SubStatus.Text = Model.Constants.STATUS_WAITING_FOR_USER_INPUT;
                            this.StatusBar.Background = Brushes.LightGreen;
                            optGetValue.IsChecked = false;
                            break;
                        case Model.Enumerations.ViewStatus.Paused:
                            this.Status.Text = Model.Constants.STATUS_PAUSED;
                            this.SubStatus.Text = Model.Constants.STATUS_DEFAULT;
                            this.StatusBar.Background = Brushes.SandyBrown;
                            break;
                        case Model.Enumerations.ViewStatus.Resuming:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_RESUMING_RECORDING;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.Stopping:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_STOPPING_RECORDING;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.InitializingDefault:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_INITIALIZING_DEFAULT;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;

                        case Model.Enumerations.ViewStatus.InitializingVerify:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_INITIALIZING_VERIFY;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.ReadyToVerify:
                        case Model.Enumerations.ViewStatus.VerifyFinished:
                            this.Status.Text = Model.Constants.STATUS_READY_TO_VERIFY;
                            this.SubStatus.Text = Model.Constants.STATUS_WAITING_FOR_USER_INPUT;
                            this.StatusBar.Background = Brushes.Yellow;
                            break;
                        case Model.Enumerations.ViewStatus.Verifying:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_VERIFYING;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.VerifyNotSupported:
                            this.Status.Text = Model.Constants.STATUS_INFO;
                            this.SubStatus.Text = Model.Constants.STATUS_VERIFY_NOT_SUPPORTED;
                            this.StatusBar.Background = Brushes.MistyRose;
                            break;
                        case Model.Enumerations.ViewStatus.InitializingAssignValue:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_INITIALIZING_ASSIGNVALUE;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.ReadyToGetValue:
                            this.Status.Text = Model.Constants.STATUS_READY_TO_ASSIGN_VALUE;
                            this.SubStatus.Text = Model.Constants.STATUS_WAITING_FOR_USER_INPUT;
                            this.StatusBar.Background = Brushes.RoyalBlue;
                            break;
                        case Model.Enumerations.ViewStatus.GettingValue:
                            this.Status.Text = Model.Constants.STATUS_WAIT;
                            this.SubStatus.Text = Model.Constants.STATUS_ASSIGNING_VALUE;
                            this.StatusBar.Background = Brushes.LemonChiffon;
                            break;
                        case Model.Enumerations.ViewStatus.UnhandledAlertDetected:
                            this.Status.Text = Model.Constants.STATUS_INFO;
                            this.SubStatus.Text = Model.Constants.STATUS_UNEXPECTED_ALERT_DETECTED;
                            this.StatusBar.Background = Brushes.MistyRose;
                            break;
                        default:
                            this.Status.Text = Model.Constants.STATUS_DEFAULT;
                            this.SubStatus.Text = Model.Constants.STATUS_DEFAULT;
                            this.StatusBar.Background = Brushes.White;
                            break;
                    }
                    UpdateButtonMenuStates();
                }));
            }
        }

        public List<Model.Action> Actions
        {
            get
            {
                return mActionList;
            }
            set
            {
                mActionList = value;
            }
        }

        public List<Model.Variable> Variables
        {
            get
            {
                return Model.Variable.AllVariables;
            }
            set
            {
                Model.Variable.AllVariables = value;
            }
        }


        public string PrevHandler
        {
            get { return prevHandler; }
            set
            {
                if (!_IsInit || prevHandler == "")
                {
                    prevHandler = value;
                    _IsInit = true;
                }
            }
        }

        public Model.Action NewAction
        {
            get
            {
                return mNewAction;
            }
            set
            {
                mNewAction = value;

                /* Deep copy reference types */
                // Control
                Model.Control controlToAddToMyList = new Model.Control();
                controlToAddToMyList.Descriptor = mNewAction.Target.Descriptor;
                controlToAddToMyList.AncestorFormIndex = mNewAction.Target.AncestorFormIndex;
                controlToAddToMyList.Type = mNewAction.Target.Type;
                controlToAddToMyList.PopRefId = mNewAction.Target.PopRefId;
                // if action is just a keystroke or simple button click, use the returned value by the javascript code
                // otherwise for clicks and other action types, get the new value (since it can change after click)
                // add conditions as necessary
                if (mNewAction.Type == Model.Enumerations.ActionType.Keystroke || mNewAction.Target.Type == Model.Enumerations.ControlType.Button 
                    || mNewAction.Target.Type == Model.Enumerations.ControlType.TextBox || mNewAction.Target.Type == Model.Enumerations.ControlType.Dialog)
                {
                    // save time by not getting the latest value since we know that keystrokes/textboxes will always get the latest value using the javascript code.
                    // button texts should not change as well. (?)
                    controlToAddToMyList.Value = mNewAction.Target.Value;
                }
                else
                {
                    controlToAddToMyList.Value = mPresenter.GetValue(mNewAction.Target.Base, mNewAction.Target.Type, mNewAction.Target.Id, mNewAction.Target.Class, mNewAction.Target.Value);
                }
                controlToAddToMyList.Base = mNewAction.Target.Base;
                controlToAddToMyList.Id = mNewAction.Target.Id;
                controlToAddToMyList.Class = mNewAction.Target.Class;
                // Action
                Model.Action actionToAddToMyList = new Model.Action();
                actionToAddToMyList.Target = controlToAddToMyList;
                actionToAddToMyList.Type = mNewAction.Type;
                actionToAddToMyList.Screen = mNewAction.Screen;
                actionToAddToMyList.Block = mNewAction.Block;
                actionToAddToMyList.AlertResponse = mNewAction.AlertResponse;

                /* Create record to be binced to UI */
                DlkActionRecord actionRec = new DlkActionRecord();
                actionRec.mBlock = mNewAction.Block;
                actionRec.mAction = Enum.GetName(typeof(Model.Enumerations.ActionType), mNewAction.Type);
                actionRec.mScreenTarget = mNewAction.Screen.Name;
                actionRec.mTargetType = Enum.GetName(typeof(Model.Enumerations.ControlType), mNewAction.Target.Type);
                actionRec.mDescType = Enum.GetName(typeof(Model.Enumerations.DescriptorType), mNewAction.Target.Descriptor.Type);
                actionRec.mDescValue = mNewAction.Target.Descriptor.Value;
                actionRec.mValue = controlToAddToMyList.Value;

                DlkBaseControl targetControl = new DlkBaseControl("Target Control", mNewAction.Target.Base.mElement);
                if (!targetControl.IsElementStale())
                {
                    string newTableID = mNewAction.Target.Base.mElement.GetAttribute("id");

                    switch (actionRec.mTargetType)
                    {
                        case Model.Constants.CP_CONTROL_TYPE_TAB:
                            string[] tabNavigationButton = new string[] { "<<", ">>" };
                            isIDDifferent = previousControlID != newTableID && !string.IsNullOrEmpty(previousControlID)
                                && (tabNavigationButton.Contains(actionRec.mValue) || tabNavigationButton.Contains(NewStep.Parameters));
                            previousControlID = newTableID;
                            break;
                        case Model.Constants.CP_CONTROL_TYPE_TABLE:
                            isIDDifferent = previousControlID != newTableID && !string.IsNullOrEmpty(previousControlID);
                            previousControlID = newTableID;
                            break;
                        default:
                            isIDDifferent = false;
                            previousControlID = "";
                            break;
                    }
                }

                /* Cache */
                Actions.Add(actionToAddToMyList);

                /* Bind data to UI */
                mActionRecord.Add(actionRec);
                /* Convert current action block to step */
                mPresenter.StartConverting(mCurrentBlock);

                if (IsVerify)
                {
                    ViewStatus = Model.Enumerations.ViewStatus.ReadyToVerify;
                }
                else
                {
                    ViewStatus = Model.Enumerations.ViewStatus.Recording;
                }
            }
        }

        public Model.Step NewStep
        {
            get
            {
                return mNewStep;
            }
            set
            {
                if (mNewStep == null)
                {
                    // do nothing
                }
                else if (mPresenter.IgnoreNewStep(value))
                {
                    return;
                }

                DlkTestStepRecord scriptRec = new DlkTestStepRecord();
                scriptRec.mExecute = value.Execute;
                scriptRec.mScreen= value.Screen;
                scriptRec.mControl = value.Control;
                scriptRec.mKeyword = value.Keyword;
                scriptRec.mParameters = new List<string>();
                scriptRec.mParameters.Add(value.Parameters);

                if (mScriptRecord.Count == 0 || isIDDifferent  || (mNewStep.Block != value.Block && !RepeatedControlKeyword(value, mNewStep)))
                {
                    scriptRec.mStepNumber = mScriptRecord.Count + 1;
                    mScriptRecord.Add(scriptRec);
                    if (this.ViewStatus == Model.Enumerations.ViewStatus.ActionDetected) // Just to quickly change status for clicks [NOTE: No perf gain, this is just an illusion that it is faster]
                    {
                        ViewStatus = Model.Enumerations.ViewStatus.Recording;
                    }
                }
                else
                {
                    scriptRec.mStepNumber = mScriptRecord[mScriptRecord.Count - 1].mStepNumber;
                    mScriptRecord[mScriptRecord.Count - 1] = scriptRec;
                }

                isChanged = true;
                mNewStep = value;
                dgScript.DataContext = mScriptRecord;
                dgScript.Items.Refresh();
            }
        }

        /// <summary>
        /// Validation for repeated keyword on the same control
        /// </summary>
        /// <param name="lastStep">previous step</param>
        /// <param name="newStep">current step</param>
        /// <returns></returns>
        private bool RepeatedControlKeyword(Step lastStep, Step newStep)
        {
            bool repeated = false;
            switch (newStep.Keyword)
            {
                case Constants.CP_KEYWORD_ASSIGNPARTIALVALUE:
                case Constants.CP_KEYWORD_ASSIGNVALUETOVARIABLE:
                case Constants.CP_KEYWORD_VERIFYREADONLY:
                case Constants.CP_KEYWORD_VERIFYEXISTS:
                case Constants.CP_KEYWORD_VERIFYTEXT:
                case Constants.CP_KEYWORD_VERIFYVALUE:
                    if (lastStep.Screen == newStep.Screen && lastStep.Control == newStep.Control && lastStep.Keyword == newStep.Keyword)
                    {
                        repeated = true;
                    }
                    break;
                default:
                    break;
            }
            return repeated;
        }

        private string StepColumnValue(int StepIndex, int ColumnIndex)
        {
            string ret = string.Empty;
            switch (ColumnIndex)
            {
                case 0:
                    ret = mScriptRecord[StepIndex].mStepNumber.ToString();
                    break;
                case 1:
                    ret = mScriptRecord[StepIndex].mExecute.ToString();
                    break;
                case 2:
                    ret = mScriptRecord[StepIndex].mScreen.ToString();
                    break;
                case 3:
                    ret = mScriptRecord[StepIndex].mControl.ToString();
                    break;
                case 4:
                    ret = mScriptRecord[StepIndex].mKeyword.ToString();
                    break;
                case 5:
                    ret = mScriptRecord[StepIndex].mParameterString.ToString();
                    break;
                case 6:
                    ret = mScriptRecord[StepIndex].mStepDelay.ToString();
                    break;
            }
            return ret;
        }

        public bool IsVerify
        {
            get
            {
                return mIsVerify;
            }
            set
            {
                mIsVerify = value;
            }
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Update IsEnabled states and other UI properties of menu an toolbar buttons
        /// </summary>
        private void UpdateButtonMenuStates()
        {
            /* Set initial values of some items.. has special handling for Paused state */
            string recImageSrc = "Resources/recordstart.ico";
            string recLabel = "Record";

            /* nullable and local simply to allow for three states w/ corresponding UI changes 
            (1) recording, (2) not recording, (3) irrelevant [do not change anything] */
            bool? bRecording = null;

            switch(this.ViewStatus)
            {
                case Model.Enumerations.ViewStatus.Recording:
                case Model.Enumerations.ViewStatus.AutoLogin:
                case Model.Enumerations.ViewStatus.Playback:
                case Model.Enumerations.ViewStatus.Resuming:
                    bRecording = true;
                    break;
                case Model.Enumerations.ViewStatus.Default:
                    bRecording = false;
                    break;
                case Model.Enumerations.ViewStatus.Paused:
                    recImageSrc = "Resources/recordresume.ico";
                    recLabel = "Resume";
                    bRecording = false;
                    break;
                default:
                    /* Do nothing. Do not update button states for all other view status not listed above */
                    break;
            }

            /* Set image in Record/Resume button */
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/" + recImageSrc, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            recImage.Source = bitmapImage;
            btnStartRecording.ToolTip = recLabel;
            recordLabel.Text = recLabel;
            mniRecord.Header = recLabel;

            /* Change button states for certain ViewStatus */
            if (bRecording != null)
            {
                bool isRecording = (bool)bRecording;
                btnStartRecording.IsEnabled = !isRecording;
                UpdateStepButtonStates(!isRecording);
                btnExport.IsEnabled = !isRecording;
                btnImport.IsEnabled = !isRecording;
                mniImport.IsEnabled = !isRecording;
                mniRecord.IsEnabled = !isRecording;
                mniExport.IsEnabled = !isRecording;
                btnPauseRecording.IsEnabled = isRecording;
                btnStopRecording.IsEnabled = ViewStatus == Model.Enumerations.ViewStatus.Paused ? true : isRecording;
                optVerify.IsEnabled = isRecording;
                btnSetVerifyType.IsEnabled = isRecording;
                btnGetValueType.IsEnabled = isRecording;
                optGetValue.IsEnabled = isRecording;
                mniPause.IsEnabled = isRecording;
                mniStop.IsEnabled = ViewStatus == Model.Enumerations.ViewStatus.Paused ? true : isRecording;
            }
        }

        private void ClearGrids()
        {
            mScriptRecord.Clear();
            mActionRecord.Clear();
            Actions.Clear();
            dgActions.Items.Refresh();
            dgScript.Items.Refresh();
            CurrentBlock = 0;

            /* Clear loaded file/test */
            mLoadedFile = string.Empty;
            mLoadedTest = null;
        }

        private void RefreshGrid()
        {
            dgActions.ItemsSource = mActionRecord;
            dgScript.ItemsSource = mScriptRecord;
            dgActions.Items.Refresh();
            dgScript.Items.Refresh();
        }

        private void DeleteStep(int selectedIndex)
        {
            int currentStepIndex = selectedIndex;

            // Adjust step number of succeeding step if any
            if (currentStepIndex + 1 < mScriptRecord.Count)
            {
                for (int idx = currentStepIndex + 1; idx < mScriptRecord.Count; idx++)
                {
                    mScriptRecord.ElementAt(idx).mStepNumber--;
                }
            }
            mScriptRecord.RemoveAt(currentStepIndex);
        }

        private void LoadTest(String TestPath)
        {
            /* Clear UI, start from scratch */
            ClearGrids();

            // load the test
            mLoadedFile = TestPath;

            if (mLoadedFile == "")
            {
                mLoadedFile = mTemplate;
            }
            if (!File.Exists(mLoadedFile))
            {
                DlkUserMessages.ShowError(this, DlkUserMessages.ERR_FILE_NOT_FOUND + mLoadedFile);
                return;
            }
            mLoadedTest = new DlkTest(mLoadedFile);

            foreach (DlkTestStepRecord tsr in mLoadedTest.mTestSteps)
            {
                mScriptRecord.Add(tsr);
            }

            dgScript.DataContext = mScriptRecord;
            dgScript.ItemsSource = mScriptRecord;
            dgScript.Items.Refresh();
        }

        private bool SaveTest()
        {
            string path;
            return SaveTest(out path);
        }

        private bool SaveTest(out string path)
        {
            bool ret = false;
            path = string.Empty;
            try
            {
                DlkTest myTest = new DlkTest(Path.Combine(DlkEnvironment.mDirTests, "template.dat"));

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.InitialDirectory = DlkEnvironment.mDirTests;
                sfd.FileName = "NewTest.xml";
                sfd.Filter = "Xml Files (*.xml)|*.xml";
                sfd.AddExtension = true;
                sfd.DefaultExt = ".xml";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    myTest.mTestName = Path.GetFileNameWithoutExtension(sfd.FileName);
                    myTest.mTestDescription = "Auto-generated test";
                    //myTest.mTestSteps.Clear();
                    foreach (DlkTestStepRecord tsr in mScriptRecord)
                    {
                        DlkTestStepRecord step = new DlkTestStepRecord();
                        step.mStepNumber = tsr.mStepNumber;
                        step.mExecute = tsr.mControl == "UNKNOWN" ? "False" : tsr.mExecute;
                        step.mScreen = tsr.mScreen;
                        step.mControl = tsr.mControl;
                        step.mKeyword = tsr.mKeyword;
                        //List<string> prms = new List<string>();
                        //prms.Add(tsr.mParameters);
                        step.mParameters = tsr.mParameters;
                        step.mStepDelay = 0;
                        step.mStepElapsedTime = myTest.mTestSteps.First().mStepElapsedTime;
                        step.mStepEnd = myTest.mTestSteps.First().mStepEnd;
                        step.mStepLogMessages = new List<DlkLoggerRecord>();
                        step.mStepStart = myTest.mTestSteps.First().mStepStart;
                        step.mStepStatus = myTest.mTestSteps.First().mStepStatus;

                        myTest.mTestSteps.Add(step);
                    }                    

                    if (mLoadedTest != null)
                    {
                        myTest.Data = mLoadedTest.Data;
                        myTest.mTestName = mLoadedTest.mTestName;
                        myTest.mTestDescription = mLoadedTest.mTestDescription;
                        myTest.mTestAuthor = mLoadedTest.mTestAuthor;
                        myTest.mLinks = mLoadedTest.mLinks;
                        myTest.mTags = mLoadedTest.mTags;
                    }

                    myTest.mTestSteps.RemoveAt(0);       
                    myTest.WriteTest(sfd.FileName, true);
                    DlkUserMessages.ShowInfo(this, DlkUserMessages.INF_SAVE_SUCCESSFUL + sfd.FileName, "Save");
                    isChanged = false;
                    path = sfd.FileName;
                    if (DlkTest.IsValidTest(sfd.FileName))
                    {
                        LoadTest(sfd.FileName);
                    }
                    else
                    {
                        return false;
                    }
                    ret = true;
                }
            }
            catch
            {

            }
            return ret;
        }

        private bool ExecutePlayback()
        {
            bool ret = true;

            DlkEnvironment.mKeepBrowserOpen = true;
            mPresenter.SelectedBrowser = PlaybackBrowser;
            StartBrowser();
            string relativePath = mLoadedTest.mTestPath.Replace(DlkEnvironment.mDirTests, "").Trim('\\');
            //DlkTestRunnerApi.ExecuteTest(mLoadedTest.mTestPath, false, "default_no_login", relativePath, 1, "Firefox");
            DlkTestRunnerApi.ExecuteTestPlayback(mLoadedTest.mTestPath, false, PlaybackEnvironment, relativePath, PlaybackInstance, PlaybackBrowser);         
            while (DlkTestRunnerApi.mExecutionStatus == "running")
            {
                Thread.Sleep(500);
            }

            System.Drawing.Point newPoint = new System.Drawing.Point();
            try
            {
                System.Drawing.Point browserPt = DlkEnvironment.AutoDriver.Manage().Window.Position;
                System.Drawing.Size browserSize = DlkEnvironment.AutoDriver.Manage().Window.Size;
                newPoint = new System.Drawing.Point(browserPt.X + browserSize.Width / 2, browserPt.Y + browserSize.Height / 2);
            }
            catch
            {
                /* Do nothing */
            }
            
            if (IsPlaybackSuccessful())
            {
                DlkUserMessages.ShowInfo(newPoint, DlkUserMessages.INF_PLAYBACK_FINISHED);
            }
            else
            {
                DlkUserMessages.ShowWarning(newPoint, DlkUserMessages.INF_PLAYBACK_FAILED + stepNoFailed + ". \n \n" + DlkUserMessages.INF_PLAYBACK_PAUSED);
                ret = false;
            }
            if (File.Exists(DlkEnvironment.mDirTests + "TempTestFile.xml"))
            {
                File.Delete(DlkEnvironment.mDirTests + "TempTestFile.xml");
            }
            return ret;
        }

        private bool IsPlaybackSuccessful()
        {
            stepNoFailed = string.Empty;
            /* Find execution results */
            if (Directory.Exists(DlkTestRunnerApi.mResultsPath))
            {
                DirectoryInfo mDir = new DirectoryInfo(System.IO.Path.Combine(DlkEnvironment.mDirTestResults,
                    DlkTestRunnerApi.mResultsPath));
                FileInfo file = mDir.GetFiles("*.xml").First();
                DlkTest executedTest = new DlkTest(file.FullName);

                if (executedTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONMSG").Count > 0)
                {
                    stepNoFailed = "test setup";
                    return false;
                }

                foreach (DlkTestStepRecord step in executedTest.mTestSteps)
                {
                    /* If a step failed, get the failing stepnumber */
                    if (step.mStepStatus == "Failed")
                    {
                        stepNoFailed = "step " + step.mStepNumber.ToString();
                        break;
                    }
                }
            }

            if (String.IsNullOrEmpty(stepNoFailed))
            {
                return true;
            }
            else
            {
                return false ;
            }
        }

        private Boolean ExecuteAutoLogin()
        {
            if (RecordingEnvironment != null)
            {
                DlkEnvironment.mKeepBrowserOpen = true;
                StartBrowser();
                Thread.Sleep(500);
                if (!AutoLogin())
                {
                    return false;
                }
                return true;
            }
            return true;
            
        }

        private Boolean AutoLogin()
        {
            try
            {

                if (DlkEnvironment.mLoginConfig == "skip")
                {
                    mLoginConfigHandler = null;
                }
                else
                {                                   
                    mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, RecordingEnvironment);
                }

                if (mLoginConfigHandler.mUrl != null)
                {
                    DlkEnvironment.AutoDriver.Url = mLoginConfigHandler.mUrl;
                    if (!String.IsNullOrEmpty(mLoginConfigHandler.mUser) && !String.IsNullOrEmpty(mLoginConfigHandler.mPassword))
                    {
                        mPresenter.AutoLogin(mLoginConfigHandler);
                    }
                }
                return true;
            }
            catch
            {
                System.Drawing.Point newPoint = new System.Drawing.Point();
                try
                {
                    System.Drawing.Point browserPt = DlkEnvironment.AutoDriver.Manage().Window.Position;
                    System.Drawing.Size browserSize = DlkEnvironment.AutoDriver.Manage().Window.Size;
                    newPoint = new System.Drawing.Point(browserPt.X + browserSize.Width / 2, browserPt.Y + browserSize.Height / 2);
                }
                catch
                {
                    /* Do nothing */
                }

                DlkUserMessages.ShowWarning(newPoint, DlkUserMessages.ERR_AUTOLOGIN_FAIL);
                return false;
            }
        }

        private void openContextMenu(object buttonSender)
        {
            var addButton = buttonSender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

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

        /// <summary>
        /// Update IsEnabled states of MoveUp and MoveDown toolbar buttons
        /// </summary>
        private void UpdateMoveStepButtonStates()
        {
            int currSelectedIndex = dgScript.SelectedIndex;
            btnMoveUp.IsEnabled = IsPausedOrStopped() && (dgScript.SelectedItem != null ? currSelectedIndex > 0 : dgScript.SelectedIndex!=-1) && dgScript.Items.Count > 0;
            btnMoveDown.IsEnabled = IsPausedOrStopped() && (dgScript.SelectedItem != null ? currSelectedIndex < dgScript.Items.Count - 1 : dgScript.SelectedIndex != -1) && dgScript.Items.Count > 0;
        }

        /// <summary>
        /// Update IsEnabled states of Test Capture step action toolbar buttons
        /// </summary>
        private void UpdateStepButtonStates(bool isEnabled)
        {
            btnClearScript.IsEnabled = isEnabled && dgScript.Items.Count > 0;
            btnDeleteRow.IsEnabled = isEnabled && dgScript.Items.Count > 0;
            btnEditRow.IsEnabled = isEnabled && dgScript.Items.Count > 0;
            UpdateMoveStepButtonStates();
        }

        /// <summary>
        /// Checks if Test Capture is Paused or Stopped
        /// </summary>
        /// <returns>Returns TRUE if Test Capture is Paused or Default mode</returns>
        private bool IsPausedOrStopped()
        {
            return (ViewStatus == Model.Enumerations.ViewStatus.Paused || ViewStatus == Model.Enumerations.ViewStatus.Default);
        }

        /// <summary>
        /// This method handles the horizontal grid splitter from going beyond the window height
        /// </summary>
        private void UpdateGridSplitterHeights()
        {
            R0.MaxHeight = Math.Min(TCGrid.ActualHeight, TCGrid.ActualHeight) - (R2.MinHeight + 5);
        }

        #endregion

        #region EVENT HANDLERS

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Initialize();

                /* Add event handlers everytime a new row is added */
                CollectionView actionsCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(dgActions.Items);
                ((INotifyCollectionChanged)actionsCollectionView).CollectionChanged += new NotifyCollectionChangedEventHandler(dgActions_ScrollToBottom);
                CollectionView scriptCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(dgScript.Items);
                ((INotifyCollectionChanged)scriptCollectionView).CollectionChanged += new NotifyCollectionChangedEventHandler(dgScript_ScrollToBottom);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Import(object snder, RoutedEventArgs e)
        {
            try
            {
                bool bCont = true;

                using (var ofDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    ofDialog.InitialDirectory = DlkEnvironment.mDirTests;
                    ofDialog.Filter = "XML files (*.xml)|*.xml";
                    ofDialog.Title = "Import Test";
                    ofDialog.Multiselect = false;

                    if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        if (dgScript.Items.Count != 0)
                        {
                            bCont = DlkUserMessages.ShowQuestionOkCancel(this, DlkUserMessages.ASK_IMPORT_WITH_EXISTING_SCRIPT, "Import") 
                                == MessageBoxResult.OK;
                        }

                        if (bCont)
                        {
                            StopRecording(null, null);
                            if (DlkTest.IsValidTest(ofDialog.FileName))
                            {
                                bFilePathValid = ofDialog.FileName.Contains(DlkEnvironment.mDirTests);
                                if (!bFilePathValid)
                                {
                                    string testCopyPath = DlkEnvironment.mDirTests + "TempTestFile.xml";
                                    File.Copy(ofDialog.FileName, testCopyPath);
                                    File.SetAttributes(testCopyPath, FileAttributes.Normal);
                                    LoadTest(testCopyPath);
                                    isChanged = false;
                                }
                                else
                                {
                                    LoadTest(ofDialog.FileName);
                                    //bIsImport = true;
                                    isChanged = false;
                                }
                            }
                            else
                            {
                                DlkUserMessages.ShowError(this, DlkUserMessages.ERR_TEST_XML_INVALID);
                                return;
                            }
                        }
                        UpdateStepButtonStates(true);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgScript.HasItems)
                {
                    string path;
                    SaveTest(out path);
                    _owner.RefreshInMemoryTestTree(path);
                }
                else
                {
                    DlkUserMessages.ShowError(this, "There's no script to export.");
                }
                
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ClearScript(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgScript.HasItems || dgActions.HasItems)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNo(this, DlkUserMessages.ASK_CLEAR_TEST);
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            ClearGrids();
                            UpdateStepButtonStates(false);
                            break;
                        case MessageBoxResult.No:
                            //do nothing
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

        private void StartRecording(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bExecutePlaybackSuccessful = true;
                bool playbackExeceuted = false;
         
                /* Check if view is status is default (stopped) and step grid has items */
                if (ViewStatus == Model.Enumerations.ViewStatus.Default && dgScript.HasItems)
                {
                    /*Ask user if they want to replay the test step until the last recorded test step or  do it manually */
                    TestPlaybackDialog tpDialog = new TestPlaybackDialog(true, isChanged);
                    tpDialog.Owner = this;
                    if (tpDialog.ShowDialog() == true)
                    {
                        if (tpDialog.mChoice.Equals("playback"))
                        {
                            if (isChanged) /* Ask to save if file change detected */
                            {
                                if (!SaveTest()) // do not continue if Save has been aborted or invalid file
                                {
                                    return;
                                }
                            }
                            /* Get playback settings */
                            ViewStatus = Model.Enumerations.ViewStatus.Playback;
                            TestPlaybackParametersDialog dlg = new TestPlaybackParametersDialog(_owner.EnvironmentIDs.ToList(),
                                _owner.AllBrowsers.Cast<DlkBrowser>().ToList().FindAll(x => x.Name == "Firefox" || x.Name == "Chrome" || x.Name == "Edge"),
                                (mLoadedTest.Data.Records.Count == 0 || mLoadedTest.Data.Records.First().Values.Count == 0) ? 1
                                : mLoadedTest.Data.Records.First().Values.Count, "Test Playback");
                            dlg.Owner = this;

                            if ((bool)dlg.ShowDialog() == true)
                            {
                                if (DlkEnvironment.IsURLBlacklist(PlaybackEnvironment))
                                {
                                    DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_URL_BLACKLIST, PlaybackEnvironment));
                                    ViewStatus = Model.Enumerations.ViewStatus.Default;
                                    return;
                                }
                                else
                                {
                                    playbackExeceuted = true;
                                    bExecutePlaybackSuccessful = ExecutePlayback();
                                }
                            }
                            else // playback cancelled
                            {
                                ViewStatus = Model.Enumerations.ViewStatus.Default;
                                return;
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (System.Action)(() =>
                                {
                                    if (tpDialog.mChoice.Equals("clear")) /* No playback, check if clear initiated */
                                    {
                                        ClearGrids();
                                    }
                                    else
                                    {
                                        RefreshGrid();
                                    }
                                }));
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                bool bAutoLoginSuccessful = true;

                ///* do not attempt to autologin if remuing from paused OR playback was performed */
                if (ViewStatus != Model.Enumerations.ViewStatus.Paused && !playbackExeceuted)
                {
                    EnvironmentSelectionDialog envDlg = new EnvironmentSelectionDialog(
                        _owner.AllBrowsers.Cast<DlkBrowser>().ToList()
                        .FindAll(x => x.Name == "Firefox" || x.Name == "Chrome" || x.Name == "Edge")
                        , _owner.EnvironmentIDs.ToList());
                    envDlg.Owner = this;
                    ViewStatus = Model.Enumerations.ViewStatus.AutoLogin;
                    if ((bool)envDlg.ShowDialog() == true)
                    {
                        if (DlkEnvironment.IsURLBlacklist(RecordingEnvironment))
                        {
                            DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_URL_BLACKLIST, RecordingEnvironment));
                            ViewStatus = Model.Enumerations.ViewStatus.Default;                           
                            return;
                        }
                        else
                        {
                            mPresenter.SelectedBrowser = envDlg.BrowserChoice;
                            bAutoLoginSuccessful = ExecuteAutoLogin();
                        }
                    }
                    else
                    {
                        ViewStatus = Model.Enumerations.ViewStatus.Default;
                        return;
                    }
                }

                StartBrowser();
                DlkEditingStateChecker.m_Active = true;
                mPresenter.StartInspecting();

                /* autologin failed or playback failed, pause capture */
                if (!bExecutePlaybackSuccessful || !bAutoLoginSuccessful)
                {
                    PauseRecording(null, null);
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void PauseRecording(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkEditingStateChecker.m_Active = false;
                mPresenter.StopInspecting(true);
                ViewStatus = Model.Enumerations.ViewStatus.Paused;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        
        private void StopRecording(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkEditingStateChecker.m_Active = false;
                dgScript.Items.Refresh();
                mPresenter.StopInspecting();
                //ViewStatus = Model.Enumerations.ViewStatus.Default;
                mPresenter.Quit();
                _IsInit = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void VerifyObject(object sender, RoutedEventArgs e)
        {
            try
            {
                VerifyStarted();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void StartBrowser()
        {
            try
            {
                try
                {
                    //assign autodriver's current windowhandle
                    mCurrentWindow = DlkEnvironment.AutoDriver.CurrentWindowHandle;
                }
                catch (UnhandledAlertException)
                {
                    //do not set current window to null to avoid spawning new browser
                    mCurrentWindow = PrevHandler;
                }
                catch
                {
                    //do nothing
                    mCurrentWindow = null;
                }

                //if windowhandle is empty, firefox browser hasn't opened yet. Start a new session.
                if (String.IsNullOrEmpty(mCurrentWindow))
                {
                    mPresenter.StartBrowser();
                    PrevHandler = DlkEnvironment.AutoDriver.CurrentWindowHandle;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void CloseBrowser(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    //assign autodriver's current windowhandle
                    mCurrentWindow = DlkEnvironment.AutoDriver.CurrentWindowHandle;
                }
                catch
                {
                    //do nothing
                    mCurrentWindow = null;
                }

                //if windowhandle is not empty, firefox browser is existing. close the current browser.
                if (!String.IsNullOrEmpty(mCurrentWindow))
                {
                    mPresenter.Quit();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void dgActions_ScrollToBottom(object sender, EventArgs e)
        {
            try
            {
                if (dgActions.Items.Count > 0)
                {
                    var border = VisualTreeHelper.GetChild(dgActions, 0) as Decorator;
                    if (border != null)
                    {
                        var scroll = border.Child as ScrollViewer;
                        if (scroll != null) scroll.ScrollToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgScript_ScrollToBottom(object sender, EventArgs e)
        {
            try
            {
                if (dgScript.Items.Count > 0 && (ViewStatus == Model.Enumerations.ViewStatus.Recording || ViewStatus == Model.Enumerations.ViewStatus.VerifyFinished))
                {
                    var border = VisualTreeHelper.GetChild(dgScript, 0) as Decorator;
                    if (border != null)
                    {
                        var scroll = border.Child as ScrollViewer;
                        if (scroll != null) scroll.ScrollToEnd();
                    }
                }
                else if (dgScript.SelectedItem != null)
                {
                    dgScript.ScrollIntoView(dgScript.SelectedItem);
                }
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
                OnClose(sender, e);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mniHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mPresenter.OpenHelp();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate and retrieve multiple selected item
                var selectedItems = dgScript.SelectedItems;
                List<int> selectedItemsIndex = new List<int>();
                bool isMultiselect = selectedItems.Count > 1;

                // Retrieve current index of selected items
                foreach (var selectedItem in selectedItems)
                {
                    selectedItemsIndex.Add(dgScript.Items.IndexOf(selectedItem));
                }
                selectedItemsIndex.Sort(); 
                selectedItemsIndex.Reverse(); // Re-arrange indexes in descending order


                // Remove selected items
                bool askOnce = false;
                bool deleteStep = false;
                foreach (var selectedIndex in selectedItemsIndex)
                {
                    int currSelectedIndex = selectedIndex;

                    //Ask to delete steps once for multiselect delete
                    if (!askOnce)
                    {
                        string msg = !isMultiselect? DlkUserMessages.ASK_DELETE_STEP + mScriptRecord.ElementAt(currSelectedIndex).mStepNumber.ToString() + "?" : DlkUserMessages.ASK_DELETE_SELECTED_STEPS;
                        MessageBoxResult res = DlkUserMessages.ShowQuestionYesNo(this, msg);

                        switch (res)
                        {
                            case MessageBoxResult.Yes:
                                deleteStep = true;
                                break;
                            case MessageBoxResult.No:
                                //do nothing
                                break;
                            default:
                                break;
                        }
                        askOnce = true;
                    }

                    if (deleteStep)
                    {
                        DeleteStep(currSelectedIndex);
                        dgScript.Items.Refresh();
                        this.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);

                        if (dgScript.Items.Count == 0)
                        {
                            UpdateStepButtonStates(false);
                        }
                        else if (currSelectedIndex == dgScript.Items.Count)
                        {
                            dgScript.SelectedIndex = currSelectedIndex - 1;
                        }
                        else
                        {
                            dgScript.SelectedIndex = currSelectedIndex;
                        }
                        isChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSetEnvironment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openContextMenu(sender);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSetVerifyType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openContextMenu(sender);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnGetValueType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openContextMenu(sender);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void VerifyTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string VerifyName = ProcessMenuItem(sender);
                mPresenter.SetVerifyType(VerifyName);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ValueTypeItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string getValueName = ProcessMenuItem(sender);
                mPresenter.SetGetValueType(getValueName);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Click handler for Edit Row tool bar button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Process only if a row is selected */
                if (dgScript.Items.Count > 0 && dgScript.SelectedItem != null)
                {
                    EditStep edit = new EditStep(this, dgScript.SelectedItem as DlkTestStepRecord);
                    if ((bool)edit.ShowDialog())
                    {
                        dgScript.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Click handler for Move Row Up tool bar button
        /// </summary>
        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int currSelectedIndex = dgScript.SelectedIndex;
                if (dgScript.Items.Count > 0 && dgScript.SelectedItem != null && currSelectedIndex > 0)
                {
                    DlkTestStepRecord testToMove = mScriptRecord.ElementAt(currSelectedIndex);
                    testToMove.mStepNumber--;
                    mScriptRecord.ElementAt(currSelectedIndex-1).mStepNumber++;
                    mScriptRecord[currSelectedIndex] = mScriptRecord[currSelectedIndex - 1];
                    mScriptRecord[currSelectedIndex - 1] = testToMove;
                    dgScript.Items.Refresh();
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Click handler for Move Row Down tool bar button
        /// </summary>
        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int currSelectedIndex = dgScript.SelectedIndex;
                if (dgScript.Items.Count > 0 && dgScript.SelectedItem != null && currSelectedIndex < dgScript.Items.Count-1)
                {
                    DlkTestStepRecord testToMove = mScriptRecord.ElementAt(currSelectedIndex);
                    testToMove.mStepNumber++;
                    mScriptRecord.ElementAt(currSelectedIndex + 1).mStepNumber--;
                    mScriptRecord[currSelectedIndex] = mScriptRecord[currSelectedIndex + 1];
                    mScriptRecord[currSelectedIndex + 1] = testToMove;
                    dgScript.Items.Refresh();
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Selected cell event handler for script grid
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Parameter object</param>
        private void dgScript_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                UpdateMoveStepButtonStates();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for resize of grid control
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event params</param>
        private void TCGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSplitterHeights();
        }

        /// <summary>
        /// Event handler for dragging the grid splitter in the main window
        /// </summary>
        /// <param name="sender">sender object<</param>
        /// <param name="e">event params</param>
        private void GridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            UpdateGridSplitterHeights();
        }

        /// <summary>
        /// Handler for window size changed event
        /// </summary>
        /// <param name="sender">sender object<</param>
        /// <param name="e">event params</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateGridSplitterHeights();
        }
        #endregion

        #region PUBLIC METHODS
        public TestCapture(MainWindow Owner)
        {
            _owner = Owner;

            // Set start position
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            this.Left = _owner.Left;
            this.Top = _owner.Top;

            InitializeComponent();
            mLoadedTest = null;

            DlkEditingStateChecker.m_ScriptRecord = mScriptRecord;
        }

        //public void UpdateStatus(Model.Enumerations.ViewStatus FormStatus)
        //{
        //    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
        //    {
        //        switch (FormStatus)
        //        {
        //            case Model.Enumerations.ViewStatus.ActionDetected:
        //                this.Status.Text = Model.Constants.STATUS_WAIT;
        //                this.SubStatus.Text = Model.Constants.STATUS_ACTION_DETECTED;
        //                this.StatusBar.Background = Brushes.LemonChiffon;
        //                break;
        //            case Model.Enumerations.ViewStatus.ActionDetected_Continue:
        //                this.Status.Text = Model.Constants.STATUS_READY;
        //                this.SubStatus.Text = Model.Constants.STATUS_ACTION_DETECTED_BUT_CONT;
        //                this.StatusBar.Background = Brushes.LightGreen;
        //                break;
        //            case Model.Enumerations.ViewStatus.StepAdded:
        //                this.Status.Text = Model.Constants.STATUS_WAIT;
        //                this.SubStatus.Text = Model.Constants.STATUS_STEP_ADDED;
        //                this.StatusBar.Background = Brushes.LemonChiffon;
        //                break;
        //            case Model.Enumerations.ViewStatus.Playback_Ongoing:
        //                this.Status.Text = Model.Constants.STATUS_WAIT;
        //                this.SubStatus.Text = Model.Constants.STATUS_TEST_PLAYBACK_ONGOING;
        //                this.StatusBar.Background = Brushes.LemonChiffon;
        //                break;
        //            case Model.Enumerations.ViewStatus.AutoLogin:
        //                this.Status.Text = Model.Constants.STATUS_WAIT;
        //                this.SubStatus.Text = Model.Constants.STATUS_PERFORM_AUTOLOGIN;
        //                this.StatusBar.Background = Brushes.LemonChiffon;
        //                break;
        //            case Model.Enumerations.ViewStatus.Ready:
        //                this.Status.Text = Model.Constants.STATUS_READY;
        //                this.SubStatus.Text = Model.Constants.STATUS_DEFAULT;
        //                this.StatusBar.Background = Brushes.LightGreen;
        //                break;
        //            case Model.Enumerations.ViewStatus.Paused:
        //                this.Status.Text = Model.Constants.STATUS_PAUSED;
        //                this.SubStatus.Text = Model.Constants.STATUS_DEFAULT;
        //                this.StatusBar.Background = Brushes.SandyBrown;
        //                break;
        //            case Model.Enumerations.ViewStatus.Verifying:
        //                this.Status.Text = Model.Constants.STATUS_READY_TO_VERIFY;
        //                this.SubStatus.Text = Model.Constants.STATUS_DEFAULT;
        //                this.StatusBar.Background = Brushes.Yellow;
        //                break;
        //            case Model.Enumerations.ViewStatus.VerifyNotSupported:
        //                this.Status.Text = Model.Constants.STATUS_INFO;
        //                this.SubStatus.Text = Model.Constants.STATUS_VERIFY_NOT_SUPPORTED;
        //                this.StatusBar.Background = Brushes.MistyRose;
        //                break;
        //            case Model.Enumerations.ViewStatus.UnhandledAlertDetected:
        //                this.Status.Text = Model.Constants.STATUS_INFO;
        //                this.SubStatus.Text = Model.Constants.STATUS_UNEXPECTED_ALERT_DETECTED;
        //                this.StatusBar.Background = Brushes.MistyRose;
        //                break;
        //            default:
        //                this.Status.Text = Model.Constants.STATUS_DEFAULT;
        //                this.SubStatus.Text = Model.Constants.STATUS_DEFAULT;
        //                this.StatusBar.Background = Brushes.White;
        //                break;
        //        }
        //    }));
        //}

        public void Initialize()
        {
            mPresenter = AppClassFactory.GetMainPresenter(this);
            if (mPresenter == null)
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_UNSUPPORTED_TEST_CAPTURE_APPLICATION);
                Close();
            }
            dgActions.DataContext = mActionRecord;

            Variables = new List<Model.Variable>();
            dgVariables.DataContext = Variables;

            string[] verifyType = mPresenter.GetVerifyTypes();
            string[] valueType = mPresenter.GetValueTypes();

            for (int countType = 0; countType < verifyType.Length; countType++)
            {
                MenuItem verifyItem = new MenuItem();
                verifyItem.Header = verifyType[countType];
                verifyItem.IsCheckable = true;
                if (countType == 0)
                {
                    verifyItem.IsChecked = true;
                }
                verifyItem.Click += VerifyTypeMenuItem_Click;
                cmnuVerifyTypes.Items.Add(verifyItem); 
            }

            for (int countType = 0; countType < valueType.Length; countType++)
            {
                MenuItem valueTypeItem = new MenuItem();
                valueTypeItem.Header = valueType[countType];
                valueTypeItem.IsCheckable = true;
                if (countType == 0)
                {
                    valueTypeItem.IsChecked = true;
                }
                valueTypeItem.Click += ValueTypeItem_Click;
                cmnuGetValueTypes.Items.Add(valueTypeItem);
            }

            ViewStatus = Model.Enumerations.ViewStatus.Default;
        }

        public void RecordingStopped()
        {
            ViewStatus = Model.Enumerations.ViewStatus.Default;
        }

        public void RecordingStarted()
        {
            ViewStatus = Model.Enumerations.ViewStatus.Recording;
        }

        public void ResetVerifyMode()
        {            //reset verify type value
            VerifyType = Model.Enumerations.VerifyType.VerifyContent;
            //reset verify type context menu UI
            foreach (MenuItem item in cmnuVerifyTypes.Items)
            {
                if (item.Header.ToString() == Model.Constants.VERIFYTYPE_VERIFYCONTENT.ToString())
                    item.IsChecked = true;
                else
                    item.IsChecked = false;
            }
        }

        public void VerifyStopped()
        {
            // tell the recorder that we are not verifying anymore.
            mIsVerify = false;
            mPresenter.SetVerifyStatus(false);
        }

        public void VerifyStarted()
        {
            mIsVerify = true;
            mPresenter.SetVerifyStatus(true);
        }

        void KeyWordChanged_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.Primitives.ToggleButton src = sender as System.Windows.Controls.Primitives.ToggleButton;

                if (src == optGetValue)
                {
                    /* Notify Assign Value started */
                    ViewStatus = Model.Enumerations.ViewStatus.InitializingAssignValue;

                    optVerify.IsChecked = false;
                    this.KeywordType = Model.Enumerations.KeywordType.GetValue;
                }
                else if (src == optVerify)
                {
                    /* Notify Verify initialized */
                    ViewStatus = Model.Enumerations.ViewStatus.InitializingVerify;

                    optGetValue.IsChecked = false;
                    this.KeywordType = Model.Enumerations.KeywordType.Verify;
                    this.VerifyStarted();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        void KeyWordChanged_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (optGetValue.IsChecked == false && optVerify.IsChecked == false)
                {
                    /* Notify default status starting */
                    ViewStatus = Model.Enumerations.ViewStatus.InitializingDefault;
                }

                this.VerifyStopped();
                this.KeywordType = Model.Enumerations.KeywordType.Set;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ChangeKeyword(Model.Enumerations.KeywordType Keyword)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        public void VariablesUpdated()
        {
            //mVariables.SetInUse(Name);
            this.dgVariables.Items.Refresh();
        }

        public void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.ViewStatus != Model.Enumerations.ViewStatus.Default)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancel(this, DlkUserMessages.ASK_TEST_CAPTURE_SAVE_BEFORE_EXIT);
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            StopRecording(null, null);
                            SaveTest();
                            mPresenter.SetVerifyStatus(false);
                            e.Cancel = false;
                            break;
                        case MessageBoxResult.No:
                            StopRecording(null, null);
                            CloseBrowser(null, null);
                            e.Cancel = false;
                            break;
                        case MessageBoxResult.Cancel:
                            e.Cancel = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (isChanged && dgScript.HasItems)
                    {
                        MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancel(this, DlkUserMessages.ASK_TEST_CAPTURE_SAVE_SCRIPT_BEFORE_EXIT);
                        switch (res)
                        {
                            case MessageBoxResult.Yes:
                                StopRecording(null, null);
                                SaveTest();
                                mPresenter.SetVerifyStatus(false);
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
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

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

        public Dispatcher UIDispatcher
        {
            get { return Application.Current.Dispatcher; }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                //VariablesUpdated();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ToolBar toolBar = sender as ToolBar;
                var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
                if (overflowGrid != null)
                {
                    overflowGrid.Visibility = Visibility.Collapsed;
                }

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

        private Model.Enumerations.KeywordType mKeywordType = Model.Enumerations.KeywordType.Set;

        public Model.Enumerations.KeywordType KeywordType
        {
            get
            {
                return mKeywordType;
            }
            set
            {
                mKeywordType = value;
                /* refresh UI */
                //switch (mKeywordType)
                //{
                //    case Model.Enumerations.KeywordType.Set:
                //        optVerify.IsChecked = false;
                //        optGetValue.IsChecked = false;
                //        break;
                //    case Model.Enumerations.KeywordType.Verify:
                //        //optSet.IsChecked = false;
                //        optGetValue.IsChecked = false;
                //        break;
                //    case Model.Enumerations.KeywordType.GetValue:
                //        //optSet.IsChecked = false;
                //        optVerify.IsChecked = false;
                //        break;
                //}
            }
        }
    }

    public class DlkEditingStateChecker : IValueConverter
    {
        public static List<DlkTestStepRecord> m_ScriptRecord = new List<DlkTestStepRecord>();
        public static bool m_Active = true;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string value2 = value.ToString();
            if (!String.IsNullOrEmpty(value2))
            {
                if (m_Active && int.Parse(value2) == m_ScriptRecord.Count)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            else
            {
                return "False";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
