using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPExtensibility.Commands
{
    /// <summary>
    /// Handles events and actions such as user clicks in the view. We implement our custom Commands in our viewmodels and then bind these commands to the view for them to be executed
    /// when the user interacts with the UI controls.
    /// This is basically a "generic" way to be able to handle commands. 
    /// Another way to handle commands is to implement a concrete class per command such as ClickCommand, SelectionChangedCommand, etc. But I chose this method because it's more efficient
    /// </summary>
    public class CustomCommand : ICommand
    {
        #region PRIVATE FIELDS
        private Action<object> execute;
        private Predicate<object> canExecute;
        #endregion

        #region CONSTRUCTOR
        public CustomCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region IMPLEMENTED MEMBERS
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion

    }
}
