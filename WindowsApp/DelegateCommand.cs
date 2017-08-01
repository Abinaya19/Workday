using System;
using System.Windows.Input;

namespace Workday
{
    /// <summary>
    /// Class that represents a command.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private bool _canExecute;
        private Action _execute;

        #region ICommand

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Returns true if the command can be executed.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        /// <summary>
        /// Executes the action associated with the command.
        /// </summary>
        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion

        /// <summary>
        /// Sets whether this command can be executed.
        /// </summary>
        public bool CanExecuteValue {

            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private DelegateCommand(bool canExecute)
        {
            _canExecute = canExecute;
        }

        /// <summary>
        /// Creates a <see cref="DelegateCommand"/> for an action.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Whether the command can execute.</param>
        public static DelegateCommand Create(Action execute, bool canExecute = true)
        {
            var delegateCommand = new DelegateCommand(canExecute);
            delegateCommand._execute = execute;

            return delegateCommand;
        }
    }
}
