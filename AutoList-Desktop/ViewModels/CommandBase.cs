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
    /// Executes an action
    /// </summary>
    public class CommandBase : ICommand
    {
        private readonly Action _action;

        public CommandBase(Action action) { _action = action; }

        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter) { _action(); }

        public event EventHandler CanExecuteChanged;
    }
}
