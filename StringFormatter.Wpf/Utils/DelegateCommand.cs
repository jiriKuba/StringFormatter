﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StringFormatter.Wpf.Utils
{
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Action to be performed when this command is executed
        /// </summary>
        private readonly Action<object> executionAction;

        /// <summary>
        /// Predicate to determine if the command is valid for execution
        /// </summary>
        private readonly Predicate<object> canExecutePredicate;

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// The command will always be valid for execution.
        /// </summary>
        /// <param name="execute">The delegate to call on execution</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="execute">The delegate to call on execution</param>
        /// <param name="canExecute">The predicate to determine if command is valid for execution</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.executionAction = execute;
            this.canExecutePredicate = canExecute;
        }

        /// <summary>
        /// Raised when CanExecute is changed
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the delegate backing this DelegateCommand
        /// </summary>
        /// <param name="parameter">parameter to pass to predicate</param>
        /// <returns>True if command is valid for execution</returns>
        public bool CanExecute(object parameter)
        {
            return canExecutePredicate?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Executes the delegate backing this DelegateCommand
        /// </summary>
        /// <param name="parameter">parameter to pass to delegate</param>
        /// <exception cref="InvalidOperationException">Thrown if CanExecute returns false</exception>
        public void Execute(object parameter)
        {
            if (!this.CanExecute(parameter))
            {
                throw new InvalidOperationException("The command is not valid for execution, check the CanExecute method before attempting to execute.");
            }
            this.executionAction(parameter);
        }
    }
}
