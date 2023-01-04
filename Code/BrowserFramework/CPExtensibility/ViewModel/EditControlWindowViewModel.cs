using CPExtensibility.Commands;
using CPExtensibility.Messages;
using CPExtensibility.Model;
using CPExtensibility.Services;
using CPExtensibility.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPExtensibility.ViewModel
{
    public class EditControlWindowViewModel : INotifyPropertyChanged
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
        public EditControlWindowViewModel()
        {
            LoadCommands();
            Messenger.Default.Register<RequestToEditControls>(this, OnRequestToEdit, "EDIT CONTROLS");
        }

    
        #endregion

        #region FIELDS

        private ObservableCollection<Control> _controlsToBeEdited;
        private string _containerName = string.Empty;
        private string _firstControlName;
        private int _controlCount;
        private bool _isCtrlRenameEnabled = false;

        #endregion

        #region PROPERTIES TO BE BINDED TO THE VIEW

        public ObservableCollection<Control> ControlsToBeEdited
        {
            get { return _controlsToBeEdited != null? _controlsToBeEdited : new ObservableCollection<Control>(); }
            set 
            { 
                _controlsToBeEdited = value;
                ControlCount = _controlsToBeEdited.Count;
                DefaultControlName = IsControlCountEqualToOne() ? ControlsToBeEdited[0].ControlName : "";
                OnPropertyChanged("ControlsToBeEdited");
            }
        }

        public int ControlCount
        {
            get
            {
                return _controlCount;
            }
            set
            {
                _controlCount = value;
                OnPropertyChanged("ControlCount");
            }
        }

        public string ContainerName
        {
            get
            {
                return _containerName;
            }
            set
            {
                if (ControlCount > 0)
                {
                    _containerName = value;
                    //foreach (var item in ControlsToBeEdited)
                    //{
                    //    item.ControlName = _containerName + "_" + item.ControlName; //add underscore
                    //}
                }
            }
        }

        /// <summary>
        /// Gets and sets the control name of the first control
        /// </summary>
        public string DefaultControlName
        {
            get
            {
                return _firstControlName; 
            }
            set
            {
                _firstControlName = value;
                if (IsControlCountEqualToOne())
                {
                    ControlsToBeEdited[0].ControlName = _firstControlName;
                } 
                OnPropertyChanged("DefaultControlName");
            }
        }

        /// <summary>
        /// Binded to the UI to enable/disable the UI depending on the control count. If the control count of the passed controls is only equal to 1, then enable the textbox.
        /// Otherwise, we disable the textbox because we will not support renaming of multiple controls at once. They must edit each control name one at a time - 
        /// BUT they can add a 'prefix' or 'grouping' using the container name.
        /// </summary>
        public bool IsControlRenameEnabled 
        { 
            get 
            {
                return _isCtrlRenameEnabled;
            } 
            set
            {
                _isCtrlRenameEnabled = value;
                OnPropertyChanged("IsControlRenameEnabled");
            }
        }
        #endregion


        #region COMMANDS
        public ICommand ContinueEditCommand
        {
            get;
            set;
        }

        public ICommand CloseEditCommand
        {
            get;
            set;
        }
        #endregion

        #region METHODS
        private void LoadCommands()
        {
            ContinueEditCommand = new CustomCommand(ContinueEditControls, IsCommandUsable);
            CloseEditCommand = new CustomCommand(Close, IsCommandUsable);
        }
        #endregion

        #region MESSENGER ACTIONS
        private void OnRequestToEdit(RequestToEditControls obj)
        {
            if (obj.ControlsToEdit != null)
            {
                this.ControlsToBeEdited = obj.ControlsToEdit;
            }
        }
        #endregion 

        #region COMMAND ACTIONS
        /// <summary>
        /// This method will pass the information from the EditScreenWindow view using the data binded to the properties to the CPExtensibilityMainFormViewModel 
        /// </summary>
        /// <param name="obj"></param>
        private void ContinueEditControls(object obj)
        {
            // obj is the control name
            Close(obj);
            if (string.IsNullOrEmpty(ContainerName))
            {
                Messenger.Default.Send<ControlsEdited>(new ControlsEdited(ControlsToBeEdited), "EDITED THE CONTROLS");
            }
            else
            {
                Messenger.Default.Send<ControlsEdited>(new ControlsEdited(ControlsToBeEdited, ContainerName), "EDITED THE CONTROLS");
            }
        }

        /// <summary>
        /// Closes the window using the DialogService utiliy
        /// </summary>
        /// <param name="obj"></param>
        private void Close(object obj)
        {
            DialogService.CloseEditControlsDialog();
        }

        /// <summary>
        /// Just a boolean method that will always return true to be used if a command can be executed.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsCommandUsable(object obj)
        {
            return true;
        }

        /// <summary>
        /// Allow editing of controls only if there is one control.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsControlCountEqualToOne(object obj = null)
        {
            var result = ControlsToBeEdited.Count == 1;
            IsControlRenameEnabled = result;
            return result;
        }
        #endregion
    }
}
