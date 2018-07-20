using System;
using System.Windows.Input;

namespace AutoCADLIGUI.Framework
{
    /// <inheritdoc />
    /// <summary>
    ///     A wrapper for the ICommand interface
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        /// <summary>
        ///     Delegates the command to an action
        /// </summary>
        /// <param name="action">The function that you want to delegate</param>
        public DelegateCommand(Action action)
        {
            _action = action;
        }

        /// <summary>
        ///     Executes the function that was passed to the <see cref="DelegateCommand" /> function
        /// </summary>
        /// <param name="parameter">a parameter if required</param>
        public void Execute(object parameter)
        {
            _action();
        }

        /// <summary>
        ///     Can the function be Executed
        /// </summary>
        /// <param name="parameter">a parameter if required</param>
        /// <returns>This will always return true</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        // an action
        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning disable 67
    }
}