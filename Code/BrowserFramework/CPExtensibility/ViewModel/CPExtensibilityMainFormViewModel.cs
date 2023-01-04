using CommonLib.DlkHandlers;
using Recorder.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecorderAlias = Recorder;
using CPExtensibility.Commands;
using CPExtensibility.Messages;
using CPExtensibility.Model;
using CPExtensibility.Services;
using CPExtensibility.Utility;
using System.Windows.Media;
using System.Threading;
using CommonLib.DlkRecords;
using System.Collections.ObjectModel;

namespace CPExtensibility.ViewModel
{
    /// <summary>
    /// This class will serve as a middleman between our main user interface (view) and our model
    /// </summary>
    public class CPExtensibilityMainFormViewModel : INotifyPropertyChanged, IMainView
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        
        #region CONSTRUCTOR
        public CPExtensibilityMainFormViewModel()
        {
            Initialize();
            LoadCommands();
            Messenger.Default.Register<EditScreenCreated>(this, OnEditScreen, "EDIT SCREEN");
            Messenger.Default.Register<NewScreenCreated>(this, OnNewScreenCreate, "ADD SCREEN");
            Messenger.Default.Register<ControlsEdited>(this, OnEditOfControls, "EDITED THE CONTROLS");
        }
        #endregion
        
        #region PRIVATE_FIELDS
        private Screen _screen;
        private string _statusbarText;
        private Brush _statusbarBackground;
        private string _screenfilePath;
        private string _environment;
        public bool isDirty = false;
        // services
        private BrowserService bs = null;

        // backing fields of implemented properties
        private RecorderAlias.Model.Enumerations.ViewStatus _viewStatus = RecorderAlias.Model.Enumerations.ViewStatus.Default;
        private RecorderAlias.Model.Action mAction;
        private RecorderAlias.Model.Enumerations.KeywordType _keywordType;
        private List<RecorderAlias.Model.Action> _actions;
        private int _currentBlock;
        private RecorderAlias.Model.Enumerations.ControlType[] supportedTypesToMap = new RecorderAlias.Model.Enumerations.ControlType[]
                        {
                            // add supported control types here
                            RecorderAlias.Model.Enumerations.ControlType.TextBox,
                            RecorderAlias.Model.Enumerations.ControlType.TextArea,
                            RecorderAlias.Model.Enumerations.ControlType.CheckBox,
                            RecorderAlias.Model.Enumerations.ControlType.ComboBox,
                            RecorderAlias.Model.Enumerations.ControlType.RadioButton,
                            RecorderAlias.Model.Enumerations.ControlType.Link,
                            RecorderAlias.Model.Enumerations.ControlType.Table,
                            RecorderAlias.Model.Enumerations.ControlType.Tab,
                            RecorderAlias.Model.Enumerations.ControlType.Button
                        };
        #endregion

        #region COMMANDS
        // this section will handle the user interactions in the view
        public ICommand AddScreenCommand
        {
            get;
            set;
        }

        public ICommand EditScreenCommand
        {
            get;
            set;
        }

        public ICommand SpawnBrowserCommand
        {
            get;
            set;
        }

        public ICommand SelectionChangedCommand 
        { 
            get; 
            set; 
        }

        public ICommand EditSelectedControlsCommand
        {
            get;
            set;
        }

        public ICommand DeleteSelectedControlsCommand
        {
            get;
            set;
        }

        public ICommand SaveScreenCommand
        {
            get;
            set;
        }

        public ICommand LoadToolBarCommand 
        { 
            get; 
            set; 
        }

        public ICommand HighlightSelectedControlsCommand
        {
            get;
            set;
        }

        public ICommand SelectRowCommand
        {
            get;
            set;
        }

        public ICommand ShowUserGuideCommand
        {
            get;
            set;
        }

        public ICommand CloseToolCommand
        {
            get;
            set;
        }
        #endregion

        #region PUBLIC PROPERTIES TO BE BINDED IN THE VIEW

        public Screen Screen
        {
            get 
            {
                if (_screen == null)
                {
                    _screen = new Screen();
                    return _screen;
                }
                else
                {
                    return _screen;
                }
            }
            set 
            {
                _screen = value;
                OnPropertyChanged("Screen");
            }
        }

        public string Environment
        {
            get
            {
                return _environment;
            }
            set
            {
                _environment = value;
            }
        }

        public string ScreenFilePath
        {
            get
            {
                return _screenfilePath;
            }
            set
            {
                _screenfilePath = value;
            }
        }

        public RecorderAlias.Presenter.MainPresenter Presenter
        {
            get { throw new NotImplementedException(); }
        }

        public List<RecorderAlias.Model.Action> Actions
        {
            get
            {
                return _actions;
            }
            set
            {
                _actions = value;
            }
        }

        public List<RecorderAlias.Model.Variable> Variables
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Render the interacted control in the UI as a mapped control
        /// </summary>
        public RecorderAlias.Model.Action NewAction
        {
            get
            {
                return mAction;
            }
            set
            {
                mAction = value;
                if (mAction != null)
                {
                   // generate the xpath based on the supported control types.
                    string ctlXPath = Control.ConstructXPath(mAction);
                    if (! string.IsNullOrEmpty(ctlXPath))
                    {
                        string ctlType = Control.GetControlType(mAction.Target.Type);
                        DlkObjectStoreFileControlRecord ofcr = new DlkObjectStoreFileControlRecord(Control.GetDescendantControlName(ctlType, mAction), ctlType, "xpath", ctlXPath);
                        DescendantControl dc = new DescendantControl(ofcr)
                        {
                            ControlName = ofcr.mKey,
                            ControlType = ofcr.mControlType,
                            SearchMethod = ofcr.mSearchMethod,
                            SearchParameter = ofcr.mSearchParameters
                        };

                        if (supportedTypesToMap.Any( item => item == mAction.Target.Type))
                        {
                            if (Screen.AddControl(dc))
                            {
                                ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.Recording;
                                Screen.SelectedControl = dc;
                                isDirty = true;
                            }
                            else
                            {
                                ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.ControlAlreadyExists;
                            }
                        }
                        else
                        {
                            ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.UnsupportedControlType;
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(ctlXPath))
                    {
                        // if xpath is an empty string, this means that the control type is not supported.
                        // display to the UI that the interacted control type is not supported.
                        ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.UnsupportedControlType;
                    }
                }
               
            }
        }

        public RecorderAlias.Model.Step NewStep
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int CurrentBlock
        {
            get
            {
                return _currentBlock;
            }
            set
            {
                _currentBlock = value;
            }
        }

        public RecorderAlias.Model.Enumerations.VerifyType VerifyType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public RecorderAlias.Model.Enumerations.ViewStatus ViewStatus
        {
            get
            {
                return _viewStatus;
            }
            set
            {
                _viewStatus = value;
                switch (_viewStatus)
                {
                    case RecorderAlias.Model.Enumerations.ViewStatus.Recording:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_READY, RecorderAlias.Model.Constants.STATUS_WAITING_FOR_USER_INPUT);
                        StatusBarBackground = Brushes.LightGreen;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.ActionDetected:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_WAIT, RecorderAlias.Model.Constants.STATUS_ACTION_DETECTED);
                        StatusBarBackground = Brushes.LemonChiffon;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.AutoLogin:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_WAIT, RecorderAlias.Model.Constants.STATUS_PERFORM_AUTOLOGIN);
                        StatusBarBackground = Brushes.LemonChiffon;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.ControlAlreadyExists:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_ERROR, RecorderAlias.Model.Constants.STATUS_ALREADY_MAPPED);
                        StatusBarBackground = Brushes.Red;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.Paused:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_PAUSED, RecorderAlias.Model.Constants.STATUS_DEFAULT);
                        StatusBarBackground = Brushes.SandyBrown;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.DuplicateControlName:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_ERROR, RecorderAlias.Model.Constants.DUPLICATE_CONTROLNAME);
                        StatusBarBackground = Brushes.Red;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.UnsupportedControlType:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_ERROR, RecorderAlias.Model.Constants.CONTROL_TYPE_UNSUPPORTED);
                        StatusBarBackground = Brushes.Red;
                        break;
                    case RecorderAlias.Model.Enumerations.ViewStatus.Saved:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_OK, RecorderAlias.Model.Constants.STATUS_SCREEN_SAVED);
                        StatusBarBackground = Brushes.LightGreen;
                        break;
                    default:
                        StatusBarText = string.Format("{0} {1}", RecorderAlias.Model.Constants.STATUS_DEFAULT, RecorderAlias.Model.Constants.STATUS_DEFAULT);
                        StatusBarBackground = Brushes.White;
                        break;
                }
            }
        }

        public string StatusBarText
        {
            get
            {
                return _statusbarText;
            }
            set
            {
                _statusbarText = value;
                OnPropertyChanged("StatusBarText");
            }
        }

        public Brush StatusBarBackground
        {
            get
            {
                return _statusbarBackground;
            }
            set
            {
                _statusbarBackground = value;
                OnPropertyChanged("StatusBarBackground");
            }
        }

        public RecorderAlias.Model.Enumerations.KeywordType KeywordType
        {
            get
            {
                return _keywordType;
            }
            set
            {
                _keywordType = value;
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Just a standard initialize
        /// </summary>
        public void Initialize()
        {
            Screen.LoadScreens();
            //Screen = new Screen("CP7Login");
            Actions = new List<RecorderAlias.Model.Action>();
        }

        /// <summary>
        /// Loads the commands. A command is something that we will execute whenever an action is performed on a WPF control.
        /// </summary>
        private void LoadCommands()
        {
            AddScreenCommand = new CustomCommand(AddScreen, IsCommandUsable);
            EditScreenCommand = new CustomCommand(EditScreen, IsCommandUsable);
            SpawnBrowserCommand = new CustomCommand(SpawnBrowser, IsCommandUsable);
            SelectionChangedCommand = new CustomCommand(UpdateSelectedItemsInViewModel, IsCommandUsable);
            DeleteSelectedControlsCommand = new CustomCommand(DeleteSelectedItems, IsSelectedItemCountValid);
            EditSelectedControlsCommand = new CustomCommand(EditSelectedItems, IsSelectedItemCountValid);
            SaveScreenCommand = new CustomCommand(SaveScreen, AreControlNamesUnique);
            LoadToolBarCommand = new CustomCommand(LoadToolBar, IsCommandUsable);
            HighlightSelectedControlsCommand = new CustomCommand(HighlightSelectedControls, IsSelectedItemCountValid);
            ShowUserGuideCommand = new CustomCommand(ShowUserGuide, IsCommandUsable);
            CloseToolCommand = new CustomCommand(CloseTool, IsCommandUsable);
        }
        #endregion

        #region COMMAND ACTIONS
        /// <summary>
        /// A Custom command that spawns a browser
        /// </summary>
        /// <param name="obj"></param>
        private void SpawnBrowser(object obj)
        {
            if (bs == null)
            {
                bs = new BrowserService(this);                
            }
            // consider moving these lines to the constructor of BrowserService
            bs.RecordingEnvironment = this.Environment;
            bs.ScreenToAutoNavigate = this.Screen.ScreenName;
            bs.RecordingSetup();
            bs.StartInspecting();
        }

        /// <summary>
        /// A custom command that shows the edit screen dialog
        /// </summary>
        /// <param name="obj"></param>
        private void EditScreen(object obj)
        {
            DialogService.ShowEditScreenDialog();
        }

        /// <summary>
        /// A custom command that shows the add screen dialog
        /// </summary>
        /// <param name="obj"></param>
        private void AddScreen(object obj)
        {
            DialogService.ShowNewScreenDialog();
        }

        /// <summary>
        /// Saves the screen in memory to an object store file of the same name.
        /// </summary>
        /// <param name="obj"></param>
        private void SaveScreen(object obj)
        {
            this.Screen.SaveScreen(this.Screen.ScreenName);
            ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.Saved;
            isDirty = false;
        }

        /// <summary>
        /// Deletes the selected controls from the screen. This automatically notifies the UI for changes due to INotifyPropertyChanged. No need to refresh.
        /// </summary>
        /// <param name="obj"></param>
        private void EditSelectedItems(object obj)
        {
            // pass data
            DialogService.ShowEditControlsDialog(Screen.SelectedControls);
        }

        /// <summary>
        /// Deletes the selected controls from the screen. This automatically notifies the UI for changes due to INotifyPropertyChanged. No need to refresh.
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteSelectedItems(object obj)
        {
            this.Screen.RemoveSelectedItems();
            isDirty = true;
        }

        private void UpdateSelectedItemsInViewModel(object obj)
        {
            // obj is the parameter passed here from the xaml - in this case, it is an IList of objects. we cast the type of the items in the list to the "Control" class. 
            var objec = (obj as IList<object>).Cast<Control>().ToList();
            if (objec != null)
            {
                Screen.SelectedControls = objec.ToObservableCollection();
            }
        }

        /// <summary>
        /// Consider moving the business logic to Model
        /// </summary>
        /// <param name="obj"></param>
        private void OnEditOfControls(ControlsEdited obj)
        {
            Screen.EditExistingControls(obj);
            isDirty = true;
        }
   
        /// <summary>
        /// Removes the button at the right side of the toolbar
        /// </summary>
        /// <param name="obj"></param>
        private void LoadToolBar(object obj)
        {
            try
            {
                System.Windows.Controls.ToolBar toolBar = obj as System.Windows.Controls.ToolBar;

                var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as System.Windows.FrameworkElement;
                if (mainPanelBorder != null)
                {
                    mainPanelBorder.Margin = new System.Windows.Thickness(0);
                }
            }
            catch
            {
                /* Swallow */
            }
        }

        private void HighlightSelectedControls(object obj)
        {
            try
            {
                if (bs != null && !string.IsNullOrWhiteSpace(bs.RecordingEnvironment))
                {
                    bs.HighlightSelectedControls(Screen.SelectedControls);
                }
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Shows the user manual in pdf form
        /// </summary>
        /// <param name="obj"></param>
        private void ShowUserGuide(object obj)
        {
            try
            {
                CPExtensibilityUtility.OpenUserGuidePDF();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Closes the app
        /// </summary>
        /// <param name="obj"></param>
        private void CloseTool(object obj)
        {
            System.Environment.Exit(0);
        }

        /* COMMAND USABLE CHECKS*/
        private bool IsSelectedItemCountValid(object obj)
        {
            return Screen.SelectedControls.Count > 0;
        }

        private bool IsCommandUsable(object obj)
        {
            return true;
        }

        private bool AreControlNamesUnique(object obj)
        {
            var unique = Screen.AreControlNamesUnique();
            if (!unique)
            {
                var duplicates = Screen.ExistingControls.GroupBy(x => x.ControlName).Where(g => g.Count() > 1);
                ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.DuplicateControlName;
                StatusBarText += " The following controls must be renamed:";
                
                foreach (var control in duplicates)
                {
                    StatusBarText += " " + control.Key + ",";
                }
                StatusBarText = StatusBarText.TrimEnd(new char[] { ',', ' ' });
            }
            return unique;
        }

        private bool AreThereUnsavedChanges(object obj)
        {
            if (isDirty)
            {
                DialogService.ShowUnsavedChangesDialog();
            }
            return isDirty;
        }

        /* IMPLEMENTED PROPERTIES */
        public System.Windows.Threading.Dispatcher UIDispatcher
        {
            get { throw new NotImplementedException(); }
        }

        public void RecordingStarted()
        {
            ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.Recording;
        }

        public void RecordingStopped()
        {
            ViewStatus = RecorderAlias.Model.Enumerations.ViewStatus.Default;
        }

        public void VariablesUpdated()
        {
            throw new NotImplementedException();
        }

        public void VerifyStarted()
        {
            throw new NotImplementedException();
        }

        public void ResetVerifyMode()
        {
            throw new NotImplementedException();
        }

        public void OnClose(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region MESSENGER RECEIVE ACTIONS THAT WILL EXECUTE WHEN OTHER VIEWMODELS SEND A MESSAGE TO THIS VIEWMODEL
        private void OnNewScreenCreate(NewScreenCreated obj)
        {
            Screen = new Screen(obj.Screen);
            this.Environment = obj.Environment;
            this.ScreenFilePath = Screen.FilePath;
            DialogService.CloseNewScreenDialog();
            this.SpawnBrowserCommand.Execute(obj);
        }

        private void OnEditScreen(EditScreenCreated obj)
        {
            Screen = new Screen(obj.Screen);
            this.Environment = obj.Environment;
            this.ScreenFilePath = Screen.FilePath;
            DialogService.CloseEditScreenDialog();
            this.SpawnBrowserCommand.Execute(obj);
        }
        #endregion

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (AreThereUnsavedChanges(sender))
            {
                e.Cancel = true;
            }
        }
    }
}
