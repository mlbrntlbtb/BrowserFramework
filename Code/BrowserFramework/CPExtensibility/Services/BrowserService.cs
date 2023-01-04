using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using Recorder.Model;
using Recorder.Presenter;
using Recorder.View;
using CostpointLib;
using TestRunner.Common;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CPExtensibility.ViewModel;
using System.Collections.ObjectModel;
using CommonLib.DlkControls;

namespace CPExtensibility.Services
{
    /// <summary>
    /// Contains functionality to be able to interact with the browser.
    /// This will use some code that is being used in DlkEnvironment.
    /// This class also interfaces between the ViewModel in the CPExtensibilityMainForm and the MainPresenter in TestRunner.Recorder.Presenter
    /// </summary>
    public class BrowserService 
    {
        CPExtensibilityMainFormViewModel extensibilityToolViewModel = null;
        CP_MainPresenter testCapturePresenter = null;

        /// <summary>
        /// The ViewModel of the extensibility tool will act as the Presenter of Test capture
        /// </summary>
        /// <param name="view"></param>
        public BrowserService(CPExtensibilityMainFormViewModel view)
        {
            this.extensibilityToolViewModel = view;
            this.testCapturePresenter = new CP_MainPresenter(this.extensibilityToolViewModel);
        }
        #region PRIVATE FIELDS
        private bool mCancellationPending = false;
        private BackgroundWorker mInspector;
        private BackgroundWorker mBrowserService;
        private bool mPauseState;
        private string _recordingEnvironment = null;
        private string mCurrentWindow = string.Empty;
        private string prevHandler;
        private bool _IsInit;
        private DlkLoginConfigHandler mLoginConfigHandler;
        private string _screenToAutoNavigate;
        private bool _isLoginOngoing = true;
        private bool _freshBrowserInstance = true;
        #endregion

        #region PUBLIC PROPERTIES
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

        public string RecordingEnvironment
        {
            get
            {
                return _recordingEnvironment;
            }
            set
            {
                _recordingEnvironment = value;
            }
        }

        public string ScreenToAutoNavigate
        {
            get
            {
                return _screenToAutoNavigate;
            }
            set
            {
                _screenToAutoNavigate = value;
            }
        }

        #endregion

        /// <summary>
        /// Starts a Firefox browser for control mapping purposes
        /// </summary>
        private void StartBrowser()
        {
            try
            {
                extensibilityToolViewModel.ViewStatus = Enumerations.ViewStatus.AutoLogin;

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
                    this.testCapturePresenter.StartBrowser();
                    PrevHandler = DlkEnvironment.AutoDriver.CurrentWindowHandle;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
     
        /// <summary>
        /// Start recording engine code in the test capture.
        /// </summary>
        public void StartInspecting()
        {
            try
            {
                mCancellationPending = false;
                mInspector = null;
                mInspector = new BackgroundWorker();
                mInspector.DoWork += m_Inspector_DoWork;
                mInspector.RunWorkerCompleted += m_Inspector_RunWorkerCompleted;
                mInspector.ProgressChanged += m_Inspector_ProgressChanged;
                mInspector.WorkerReportsProgress = true;
                mInspector.RunWorkerAsync();
                mPauseState = false;
                extensibilityToolViewModel.RecordingStarted();
            }
            catch 
            {
                // ignore
            }
        }

        /// <summary>
        /// Turns a captured element to a mapped control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Inspector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                this.extensibilityToolViewModel.NewAction = testCapturePresenter.mAction;
            }
            catch
            {
                // ignore
            }
        }

        /// <summary>
        /// This DoWork method will perform the "Capturing" functionality being used by the Test Capture. 
        /// We just change how the captured element is processed in the m_Inspector_ProgressChanged method above.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Inspector_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // do not inspect while login is not finished
                while (_isLoginOngoing)
                {
                    Thread.Sleep(500);
                }
                // reuse code
                if (_freshBrowserInstance) // if first browser since app opened, instantiate worker thread
                {
                    _freshBrowserInstance = false;
                    testCapturePresenter.m_Inspector_DoWork(sender, e);
                }
            }
            catch 
            {
                // ignore
            }
           
        }

        /// <summary>
        /// Change StatusBar text to notify the user that the tool is no longer mapping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Inspector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                extensibilityToolViewModel.RecordingStopped();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Starts a browser and calls the method to start the Autologin and Autonavigate to screen
        /// </summary>
        public void RecordingSetup()
        {
            try
            {
                if (RecordingEnvironment != null)
                {
                    mBrowserService = new BackgroundWorker();
                    mBrowserService.DoWork += mBrowserService_DoWork;
                    mBrowserService.RunWorkerAsync();
                }
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Close any existing browsers, opens a new browser, autologin, auto-navigate to Costpoint application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBrowserService_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                testCapturePresenter.Quit();
                if (!_freshBrowserInstance)
                {
                    testCapturePresenter.PauseState = true;                    
                }
                StartBrowser();
                if (AutoLogin())
                {
                    NavigateToAppScreen();
                }
                this.extensibilityToolViewModel.ViewStatus = Enumerations.ViewStatus.Recording;
                _isLoginOngoing = false;
                Thread.Sleep(2000);
                testCapturePresenter.PauseState = false;
            }
            catch
            {
                // swallow
            }
           
        }

        /// <summary>
        /// This will use the existing keywords available in CostpointLib to navigate to a certain screen.
        /// </summary>
        private void NavigateToAppScreen()
        {
            try
            {
                extensibilityToolViewModel.ViewStatus = Enumerations.ViewStatus.AutoLogin;

                DlkObjectStoreFileControlRecord mSearchApplicationsControlRecord = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("CP7Main", "SearchApplications");
                CostpointLib.DlkControls.DlkTextBox searchApplicationsTxtBox = new CostpointLib.DlkControls.DlkTextBox("SearchApplications", mSearchApplicationsControlRecord.mSearchMethod, mSearchApplicationsControlRecord.mSearchParameters);
                searchApplicationsTxtBox.Set(ScreenToAutoNavigate);

                DlkObjectStoreFileControlRecord mSearchApplicationsResultListControlRecord = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("CP7Main", "SearchAppResultList");
                CostpointLib.DlkControls.DlkSearchAppResultList searchApplicationsResultList = new CostpointLib.DlkControls.DlkSearchAppResultList("ResultList", mSearchApplicationsResultListControlRecord.mSearchMethod, mSearchApplicationsResultListControlRecord.mSearchParameters);
                searchApplicationsResultList.SelectContains(ScreenToAutoNavigate);
            }
            catch 
            {
                // ignore exceptions. program flow will go here when the user enters a screen name that is not an actual app in costpoint.
            }
        }

        /// <summary>
        /// This will use the existing autologin feature in CostpointLib
        /// </summary>
        /// <returns></returns>
        private bool AutoLogin()
        {
            extensibilityToolViewModel.ViewStatus = Enumerations.ViewStatus.AutoLogin;

            bool ret = false;
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
                        CostpointLib.System.DlkCostpointFunctionHandler.ExecuteFunction("CP7Login", "", "Login",
                        new String[] { mLoginConfigHandler.mUser, 
                    mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
                        ret = true;
                    }
                }
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
            }
            return ret;
        }

        /// <summary>
        /// Add a blinking highlight to a control that disappears after a few blinks
        /// </summary>
        /// <param name="observableCollection"></param>
        public void HighlightSelectedControls(ObservableCollection<Model.Control> observableCollection)
        {
            foreach (var ctrl in observableCollection)
            {
                try
                {
                    DlkBaseControl ctl = DlkInspect.GetControlViaXPath(ctrl.SearchParameter);

                    // special case for checkbox/radiobutton:
                    if (ctrl.ControlType.ToLower() == "checkbox")
                    {
                        ctl = CPExtensibility.Model.Control.GetDescendantControlLabel(Recorder.Model.Enumerations.ControlType.CheckBox, ctl);
                    }
                    else if (ctrl.ControlType.ToLower() == "radiobutton")
                    {
                        ctl = CPExtensibility.Model.Control.GetDescendantControlLabel(Recorder.Model.Enumerations.ControlType.RadioButton, ctl);
                    }

                    ctl.Highlight(false);
                }
                catch
                {

                } 
            }
        }
    }
}
