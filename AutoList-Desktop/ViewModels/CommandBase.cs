using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoList_Desktop.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// A basic command that executes an action
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to run
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// The Event that fired when the can execute values changes
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action) { _action = action; }

        /// <inheritdoc />
        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) { return true;}

        public void Execute(object parameter) { _action(); }
    }
}
