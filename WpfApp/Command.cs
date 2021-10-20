using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// Implementation of ICommand interface.
    /// </summary>
    public class Command : ICommand
    {
        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;

        /// <summary>
        /// Constructor of command objects
        /// </summary>
        /// <param name="Execute">Action which should be executed</param>
        /// <param name="CanExecute">Condition under which command can be executed</param>
        public Command(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        /// <summary>
        /// Action which should be executed
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _Execute(parameter);
        /// <summary>
        /// Condition under which command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
    }
}
