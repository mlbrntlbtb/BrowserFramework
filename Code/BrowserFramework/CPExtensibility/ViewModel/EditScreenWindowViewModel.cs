﻿using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CPExtensibility.Commands;
using CPExtensibility.Messages;
using CPExtensibility.Utility;
using CPExtensibility.Services;

namespace CPExtensibility.ViewModel
{
    /// <summary>
    /// This class will serve as the viewmodel for the edit existing screen window/view
    /// </summary>
    public class EditScreenWindowViewModel : INotifyPropertyChanged
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

        #region PRIVATE FIELDS
        private ObservableCollection<string> _environments = new ObservableCollection<string>();
        private ObservableCollection<string> _screens;
        private string _screenname = string.Empty;
        private string _selectedEnvironment = string.Empty;
        #endregion

        #region PUBLIC PROPERTIES TO BE BINDED IN THE UI
        public string SelectedEnvironment
        {
            get
            {
                return string.IsNullOrEmpty(_selectedEnvironment) ? Environments.FirstOrDefault() : _selectedEnvironment;
            }
            set
            {
                _selectedEnvironment = value;
                OnPropertyChanged("SelectedEnvironment");
            }
        }

        public ObservableCollection<string> Environments
        {
            get
            {
                return _environments;
            }
        }

        public string ScreenName
        {
            get
            {
                return string.IsNullOrEmpty(_screenname) ? Screens.FirstOrDefault() : _screenname;
            }
            set 
            { 
                _screenname = value; OnPropertyChanged("ScreenName"); 
            }
        }

        public ObservableCollection<string> Screens
        {
            get
            {
                return _screens;
            }
            set
            {
                _screens = value;
            }
        }


        #endregion

        #region CONSTRUCTOR
        public EditScreenWindowViewModel()
        {
            Initialize();
        }
        #endregion
        
        #region METHODS
        /// <summary>
        /// This method will initialize the values for the private fields
        /// </summary>
        private void Initialize()
        {
            _environments = GetEnvironments();
            _screens = GetScreens();
            LoadCommands();
        }

        /// <summary>
        /// Just a local implementation that will return the Screens on the chosen product. This method reuses the already available code in CommonLib
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<string> GetScreens()
        {
            return DlkDynamicObjectStoreHandler.Instance.Screens.ToObservableCollection();
        }

        /// <summary>
        /// Just a local implementation that will return the Environments of the chosen product. This method reuses the already available code in CommonLib
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<string> GetEnvironments()
        {
            DlkLoginConfigHandler loginHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile);
            ObservableCollection<string> ret = new ObservableCollection<string>();
            foreach (DlkLoginConfigRecord rec in loginHandler.mLoginConfigRecords)
            {
                ret.Add(rec.mID);
            }
            return new ObservableCollection<string>(ret.OrderBy(str => str).ToList<string>());
        }

        /// <summary>
        /// This command initializes the commands that will be executed when the user interacts with the user interface
        /// </summary>
        private void LoadCommands()
        {
            ContinueEditCommand = new CustomCommand(ContinueEdit, IsCommandUsable);
            CloseCommand = new CustomCommand(Close, IsCommandUsable);
        }

        private void Close(object obj)
        {
            DialogService.CloseEditScreenDialog();
        }
        #endregion

        #region COMMANDS
        // this section will handle the user interactions in the view

        // command to execute afte clicking the Continue button
        public ICommand ContinueEditCommand
        {
            get;
            set;
        }

        // command to execute after clicking the Close button
        public ICommand CloseCommand
        {
            get;
            set;
        }

        #endregion

        #region COMMAND ACTIONS
        /// <summary>
        /// This method will pass the information from the EditScreenWindow view using the data binded to the properties to the CPExtensibilityMainFormViewModel 
        /// </summary>
        /// <param name="obj"></param>
        private void ContinueEdit(object obj)
        {
            if (!String.IsNullOrEmpty(ScreenName))
            {
                Messenger.Default.Send<EditScreenCreated>(new EditScreenCreated(ScreenName, SelectedEnvironment), "EDIT SCREEN");
            }
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
        #endregion
    }
}
