using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using System.Threading;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using Recorder.Model;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using OpenQA.Selenium;
using Recorder.View;
using TestRunner.Common;

namespace Recorder.Presenter
{
    public abstract class MainPresenter
    {
        #region PRIVATE MEMBERS
        protected IMainView mView;
        protected bool mIsVerify = false;
        protected System.Drawing.Point browserPt;
        protected System.Drawing.Size browserSize;
        protected Inspector mInspectorService = AppClassFactory.GetInspector();

        private Model.Step mStep;
        private BackgroundWorker mInspector;
        private bool mCancellationPending = false;
        private string previousScreen = "";
        private List<DlkObjectStoreFileControlRecord> previousLst = null;
        #endregion

        #region PUBLIC PROPERTIES
        public Model.Action mAction;
        public bool PauseState { get; set; }
        public string SelectedBrowser { get; set; }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Convert list of actions into step
        /// </summary>
        /// <param name="Actions">The list of actions to be converted into step</param>
        /// <returns>Returns a step supplied with the list of actions</returns>
        private Model.Step ConvertActionBlock(List<Model.Action> Actions)
        {
            Model.Step ret = new Step();
            ret.Execute = Constants.EXECUTE_DFLT_VAL;
            ret.Delay = Constants.DELAY_DFLT_VAL;
            ret.Block = Actions.Last().Block;
            
            if (Actions.Last().Type == Enumerations.ActionType.Verify)
            {
                ret = VerifyActionBlock(Actions);
                mView.ViewStatus = Enumerations.ViewStatus.VerifyFinished;
            }
            else if (Actions.Last().Type == Enumerations.ActionType.AssignValue)
            {
                ret = AssignValueActionBlock(Actions);
                mView.ViewStatus = Enumerations.ViewStatus.GetValueFinished;
            }
            else
            {
                ret = PrimaryActionBlock(Actions);
            }
            return ret;
        }

        /// <summary>
        /// Handles alert dialog
        /// </summary>
        /// <param name="alert">The content of the alert dialog</param>
        private bool HandleAlert(string alert)
        {

            bool alertResponse = false;
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (System.Action)(() =>
                {
                    System.Drawing.Point newPoint = new System.Drawing.Point(browserPt.X + browserSize.Width / 2, browserPt.Y + browserSize.Height / 2);
                    UnexpectedModalDialog dlg = new UnexpectedModalDialog(alert, "Test Capture", newPoint, true);
                    alertResponse = (bool)dlg.ShowDialog();
                }));
                if (alertResponse)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                }
                else
                {
                    do
                    {
                        DlkEnvironment.AutoDriver.SwitchTo().Alert().Dismiss();
                    } while (DlkAlert.DoesAlertExist());
                }
            }
            catch
            {
                // ignored
            }

            return alertResponse;
        }
        #endregion

        #region PROTECTED METHODS
        /// <summary>
        /// Gets control readonly attribute
        /// </summary>
        /// <param name="TargetControl">Target control</param>
        /// <returns>TRUE string if readonly, FALSE is not readonly, TRUE string if evaluation fails</returns>
        protected string GetVerifyReadOnlyParameter(Model.Control TargetControl)
        {
            string ret = "TRUE";
            try
            {
                ret = TargetControl.Base != null ? TargetControl.Base.IsReadOnly().ToUpper() : "TRUE";
            }
            catch
            {
                /* Do nothing, return default value: TRUE */
            }
            return ret;
        }

        /// <summary>
        /// Checks if screen-control tandem has ID in files
        /// </summary>
        /// <param name="Screen">The object store to be referenced</param>
        /// <param name="DescriptorValue">Value of the descriptor or search method of the control</param>
        protected bool IsID(string Screen, string DescriptorValue)
        {
            bool ret = false;
            try
            {
                List<DlkObjectStoreFileControlRecord> lst = GetControlsFromFiles(Screen);

                // check for all with ID descriptors
                List<DlkObjectStoreFileControlRecord> lstAllIDs = lst.FindAll(y => y.mSearchParameters == DescriptorValue);
                ret = lstAllIDs.Count > 0;
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        /// <summary>
        /// Get all controls from object store files
        /// </summary>
        /// <param name="Screen">The object store to be referenced</param>
        /// <returns>Returns a list of controls from the given Screen</returns>
        protected List<DlkObjectStoreFileControlRecord> GetControlsFromFiles(string Screen)
        {
            DlkDynamicObjectStoreHandler osh = DlkDynamicObjectStoreHandler.Instance;
            List<DlkObjectStoreFileControlRecord> lst = null;
            
            lst = !previousScreen.Equals(Screen) ? osh.GetControlRecords(Screen) : previousLst;
            previousLst = lst;
            previousScreen = Screen;

            return lst; // this will never be null
        }

        /// <summary>
        /// Get descriptor type from string
        /// </summary>
        /// <param name="Input">The name of the control</param>
        /// <returns>Returns the descriptor type based on the name of the control</returns>
        protected Enumerations.DescriptorType GetDescriptorTypeFromString(string Input)
        {
            Enumerations.DescriptorType ret = Enumerations.DescriptorType.Unknown;
            foreach (Enumerations.DescriptorType dt in
                (Enumerations.DescriptorType[])Enum.GetValues(typeof(Enumerations.DescriptorType)))
            {
                if (Input.ToLower() == Enum.GetName(typeof(Model.Enumerations.DescriptorType), dt).ToLower())
                {
                    ret = dt;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get attribute value of control
        /// </summary>
        /// <param name="Target">The control being clicked</param>
        /// <param name="AttributeName">Attribute name of the clicked control</param>
        /// <returns>Returns the attribute value of the control based on its attribute name</returns>
        protected string GetControlAttributeValue(Recorder.Model.Control Target, string AttributeName)
        {
            string ret = string.Empty;
            try
            {
                ret = Target.Base.GetAttributeValue(AttributeName);
            }
            catch
            {
                ret = string.Empty;
            }
            return ret;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Open Test Capture reference guide
        /// </summary>
        public void OpenHelp()
        {
            try
            {
                string fileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location),
                    "TestCapture.pdf");
                DlkProcess.RunProcess(fileName, "", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, -1);
            }
            catch
            {
                /* Do nothing */
            }
        }

        /// <summary>
        /// Start recording browser -> Firefox
        /// </summary>
        public void StartBrowser()
        {
            DlkEnvironment.mBrowser = SelectedBrowser;
            DlkEnvironment.StartBrowser(true);
            DlkEnvironment.AutoDriver.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 0, 5);
        }

        /// <summary>
        /// Quit recorder activities
        /// </summary>
        public void Quit()
        {
            mView.ViewStatus = Enumerations.ViewStatus.Default;
            try
            {
                DlkEnvironment.CloseSession();
            }
            catch
            {
                /* Do nothing */
            }
        }
     
        /// <summary>
        /// Start recording engine
        /// </summary>
        public void StartInspecting()
        {
            mCancellationPending = false;
            if (!PauseState)
            {
                mInspector = new BackgroundWorker();
                mInspector.DoWork += m_Inspector_DoWork;
                mInspector.RunWorkerCompleted += m_Inspector_RunWorkerCompleted;
                mInspector.ProgressChanged += m_Inspector_ProgressChanged;
                mInspector.WorkerReportsProgress = true;
                mInspector.RunWorkerAsync();
            }
            else
            {
                mView.ViewStatus = Enumerations.ViewStatus.Resuming;
            }

            PauseState = false;
            mView.RecordingStarted();
        }

        /// <summary>
        /// Start conversion of current action blocks
        /// </summary>
        /// <param name="BlockToConvert"></param>
        public void StartConverting(int BlockToConvert)
        {

            List<Model.Action> src = new List<Model.Action>(from itm in this.mView.Actions
                                                            where itm.Block == BlockToConvert
                                                            select new Model.Action
                                                            {
                                                                Target = itm.Target,
                                                                Screen = itm.Screen,
                                                                Type = itm.Type,
                                                                Block = itm.Block,
                                                                AlertResponse = itm.AlertResponse
                                                            });
            if (src.Count != 0)
            {
                // converts a block to a step - the step is the one that you see in the grid with the control/kw/parameter columns
                mStep = ConvertActionBlock(src);
                if (mStep != null)
                {
                    this.mView.NewStep = mStep;
                }
            }
        }

        /// <summary>
        /// Stop recording engine
        /// </summary>
        public void StopInspecting(bool Pause = false)
        {
            if (!Pause)
            {
                mView.ViewStatus = Enumerations.ViewStatus.Stopping;
            }
            mCancellationPending = true;
            PauseState = Pause;
        }

        /// <summary>
        /// Sets Verify Flag
        /// </summary>
        public void SetVerifyStatus(bool VerifyStarted)
        {
            mIsVerify = VerifyStarted;
        }


        public void SetVerifyType(string VerifyName)
        {
            switch (VerifyName)
            {
                case Model.Constants.VERIFYTYPE_VERIFYCONTENT:
                    mView.VerifyType = Model.Enumerations.VerifyType.VerifyContent;
                    break;
                case Model.Constants.CP_KEYWORD_VERIFYEXISTS:
                    mView.VerifyType = Model.Enumerations.VerifyType.VerifyExists;
                    break;
                case Model.Constants.CP_KEYWORD_VERIFYREADONLY:
                    mView.VerifyType = Model.Enumerations.VerifyType.VerifyReadOnly;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Set the selected assign value type
        /// </summary>
        /// <param name="GetValueName">AssignValue type</param>
        public void SetGetValueType(string GetValueName)
        {
            switch (GetValueName)
            {
                case Constants.GETVALUETYPE_ASSIGNPARTIALVALUE:
                    mView.GetValueType = Enumerations.GetValueType.AssignPartialValue;
                    break;
                case Constants.GETVALUETYPE_ASSIGNVALUE:
                    mView.GetValueType = Enumerations.GetValueType.AssignValue;
                    break;
                default:
                    return;
            }
        }

        public string[] GetVerifyTypes()
        {
            return new string[] {
                Model.Constants.VERIFYTYPE_VERIFYCONTENT,
                Model.Constants.CP_KEYWORD_VERIFYEXISTS,
                Model.Constants.CP_KEYWORD_VERIFYREADONLY
            };
        }

        public string[] GetValueTypes()
        {
            return new string[]
            {
                Constants.GETVALUETYPE_ASSIGNVALUE,
                Constants.GETVALUETYPE_ASSIGNPARTIALVALUE
            };
        }
        #endregion

        #region ABSTRACT METHODS 
        // must override
        protected abstract void InterpretAction(DlkBaseControl control, DlkCapturedStep step, Enumerations.ActionType actionType);
        /// <summary>
        /// Verify actions in a step
        /// </summary>
        /// <param name="Actions">The list of actions to be converted into step</param>
        /// <returns>Returns a step where keyword is set to verify the identified control</returns>
        protected abstract Model.Step VerifyActionBlock(List<Model.Action> Actions);
        /// <summary>
        /// Assign values to actions in a step
        /// </summary>
        /// <param name="Actions">The list of actions in the step</param>
        /// <returns>Returns a step where keyword is set to assign values to identified control</returns>
        protected abstract Model.Step AssignValueActionBlock(List<Model.Action> Actions);
        /// <summary>
        /// Primary actions in a step
        /// </summary>
        /// <param name="Actions">The list of actions in the step</param>
        /// <returns>Returns a step where primary keyword is to identified control</returns>
        protected abstract Model.Step PrimaryActionBlock(List<Model.Action> Actions);
        /// <summary>
        /// Get block of control
        /// </summary>
        /// <param name="TargetAction">The control to be identified.</param>
        /// <returns>Returns the block number of this step</returns>
        protected abstract int GetBlock(Model.Action TargetAction);
        /// <summary>
        /// Handles alert response during pause and resume
        /// </summary>
        /// <param name="alert">The content of the alert dialog.</param>
        /// <param name="alertResponse">Sets if the dialog should appear or not</param>
        /// <param name="myControl">Provide a basic interface for the alert dialog</param>
        protected abstract void HandleAlertResponse(string alert, bool alertResponse, DlkBaseControl myControl);
        /// <summary>
        /// Get control value
        /// </summary>
        /// <param name="control">Name of the control</param>
        /// <param name="type">Type of the control</param>
        /// <returns>Returns the value of the specified control name and type</returns>
        public abstract string GetValue(DlkBaseControl control, Enumerations.ControlType type, string jsId = "", string jsClass = "", string jsValue = "");
        public abstract bool IgnoreNewStep(Step NewStep);
        public abstract void AutoLogin(DlkLoginConfigHandler loginInfo);
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// Recorder progress change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Inspector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage == 10)
                {
                    this.mView.ViewStatus = (Enumerations.ViewStatus)e.UserState;
                }
                else
                {
                    this.mView.NewAction = mAction;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Recorder completion event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Inspector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                mView.RecordingStopped();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Recorder record event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void m_Inspector_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // PERFORMANCE GAIN OPPORTUNITY: check if CONTROL/KW is same than previous CONTROL/KW and maybe skip for optimization
                while (true) // infinite loop
                {
                    try
                    {
                        string at;
                        bool bGettingValue = this.mView.KeywordType == Enumerations.KeywordType.GetValue;
                        bool bVerify = this.mView.KeywordType == Enumerations.KeywordType.Verify;
                        DlkCapturedStep mCurrentStep;
                        string alert;
                        DlkBaseControl myControl = mInspectorService.GetAccessedControl(out at, out alert, out mCurrentStep , bVerify, bGettingValue);

                        if (PauseState
                            || mView.ViewStatus == Enumerations.ViewStatus.Verifying
                            || mView.ViewStatus == Enumerations.ViewStatus.GettingValue)
                        {
                            if (!string.IsNullOrEmpty(alert) && DlkAlert.DoesAlertExist())
                            {
                                HandleAlert(alert);
                                mView.ViewStatus = Enumerations.ViewStatus.Paused;
                            }
                            continue;
                        }
                        else
                        {
                            /* Special handler for unexpected alerts */
                            if (!string.IsNullOrEmpty(alert) && DlkAlert.DoesAlertExist())
                            {
                                mView.ViewStatus = Enumerations.ViewStatus.UnhandledAlertDetected;
                                bool alertResponse = HandleAlert(alert);
                                mView.ViewStatus = Enumerations.ViewStatus.ActionDetected;
                                HandleAlertResponse(alert, alertResponse, myControl);
                                alert = string.Empty;
                            }
                            /* Cancellation was initiated by user */
                            else if (mCancellationPending)
                            {
                                mCancellationPending = false;
                                break;
                            }
                            /* Control cannot be determined */
                            else if (myControl == null && string.IsNullOrEmpty(mCurrentStep.ElementClass) && string.IsNullOrEmpty(mCurrentStep.ElementId))
                            {
                                switch (mView.KeywordType)
                                {
                                    case Enumerations.KeywordType.Verify:
                                        this.mView.ViewStatus = Enumerations.ViewStatus.ReadyToVerify;
                                        GetBlock(mAction);
                                        break;
                                    case Enumerations.KeywordType.GetValue:
                                        this.mView.ViewStatus = Enumerations.ViewStatus.ReadyToGetValue;
                                        break;
                                    case Enumerations.KeywordType.Set:
                                        this.mView.ViewStatus = Enumerations.ViewStatus.Recording;
                                        break;
                                }
                                continue;
                            }
                            /* No problem getting accessed control */
                            else
                            {
                                Enumerations.ActionType myAT;
                                switch (at)
                                {
                                    case Constants.ACTION_TYPE_CLICK:
                                        myAT = bGettingValue
                                            ? Enumerations.ActionType.AssignValue
                                            : Enumerations.ActionType.Click;
                                        break;
                                    case Constants.ACTION_TYPE_RIGHT_CLICK:
                                        myAT = Enumerations.ActionType.RightClick;
                                        break;
                                    case Constants.ACTION_TYPE_KEYSTROKE:
                                        myAT = Enumerations.ActionType.Keystroke;
                                        break;
                                    case Constants.ACTION_TYPE_HOVER:
                                        myAT = Enumerations.ActionType.Hover;
                                        break;
                                    case Constants.ACTION_TYPE_VERIFY:
                                        myControl.Highlight(false);
                                        myAT = Enumerations.ActionType.Verify;
                                        break;
                                    default:
                                        myAT = Enumerations.ActionType.Unknown;
                                        break;
                                }
                               
                                /* Update view status */
                                if (myAT == Model.Enumerations.ActionType.Click || myAT == Model.Enumerations.ActionType.RightClick)
                                {
                                    this.mView.ViewStatus = Model.Enumerations.ViewStatus.ActionDetected;
                                }
                                else if (myAT == Enumerations.ActionType.Keystroke)
                                {
                                    this.mView.ViewStatus = Model.Enumerations.ViewStatus.ActionDetected_Continue;
                                }
                                else if (myAT == Enumerations.ActionType.Verify)
                                {
                                    this.mView.ViewStatus = Model.Enumerations.ViewStatus.Verifying;
                                }
                                else if (myAT == Model.Enumerations.ActionType.AssignValue)
                                {
                                    myControl.Highlight(false, DlkBaseControl.HighlightColor.Blue);
                                    this.mView.ViewStatus = Model.Enumerations.ViewStatus.GettingValue;
                                }

                                /* Interpret Action */
                                InterpretAction(myControl, mCurrentStep, myAT);
                            }
                        }

                        var source = sender as BackgroundWorker;
                        source.ReportProgress(10, this.mView.ViewStatus);
                        Thread.Sleep(20); /* Sleep in between progress report so they won't overlap (progress is async) */
                        source.ReportProgress(20);

                        if (mCancellationPending && !PauseState)
                        {
                            break;
                        }
                        try
                        {
                            browserPt = DlkEnvironment.AutoDriver.Manage().Window.Position;
                            browserSize = DlkEnvironment.AutoDriver.Manage().Window.Size;
                        }
                        catch
                        {
                            //do nothing reuse previous sizes
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
