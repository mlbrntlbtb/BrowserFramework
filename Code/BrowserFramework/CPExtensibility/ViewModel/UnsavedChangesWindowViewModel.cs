using CPExtensibility.Commands;
using CPExtensibility.Messages;
using CPExtensibility.Services;
using CPExtensibility.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CPExtensibility.ViewModel
{
    public class UnsavedChangesWindowViewModel
    {
        #region PRIVATE FIELDS
       
        #endregion

        #region CONSTRUCTOR
        public UnsavedChangesWindowViewModel()
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
            LoadCommands();
        }

        /// <summary>
        /// This command initializes the commands that will be executed when the user interacts with the user interface
        /// </summary>
        private void LoadCommands()
        {
            ContinueExitCommand = new CustomCommand(ContinueExit, IsCommandUsable);
            CloseCommand = new CustomCommand(GoBackToMainFormWindow, IsCommandUsable);
        }
      
        #endregion

        #region COMMANDS
        // this section will handle the user interactions in the view

        // command to execute afte clicking the Continue button
        public ICommand ContinueExitCommand
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
        /// This method will terminate the entire process
        /// </summary>
        /// <param name="obj"></param>
        private void ContinueExit(object obj)
        {
            Environment.Exit(0);
        }

        private void GoBackToMainFormWindow(object obj)
        {
            DialogService.CloseUnsavedChangesDialog();
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
