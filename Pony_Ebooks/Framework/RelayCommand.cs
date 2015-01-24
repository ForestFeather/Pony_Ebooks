// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - RelayCommand.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:31 PM, 23/01/2015
// //  Created Date: 8:26 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Windows.Input;

#endregion

namespace Pony_Ebooks.Framework {
    ///=================================================================================================
    /// <summary>   Relay command. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.IRelayCommand"/>
    ///=================================================================================================
    public class RelayCommand : IRelayCommand {

        /// <summary>   The can execute. </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>   The execute. </summary>
        private readonly Action<object> _execute;

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <param name="execute">  The execute. </param>
        ///=================================================================================================
        private RelayCommand( Action<object> execute ) {
            this._execute = execute;
        }

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <param name="execute">      The execute. </param>
        /// <param name="canExecute">   The can execute. </param>
        ///=================================================================================================
        private RelayCommand( Action<object> execute, Predicate<object> canExecute ) {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion

        #region IRelayCommand Members

        ///=================================================================================================
        /// <summary>   Determine if we can execute. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <param name="parameter">    The parameter. </param>
        ///
        /// <returns>   true if we can execute, false if not. </returns>
        ///=================================================================================================
        public bool CanExecute( object parameter ) {
            return this._canExecute == null || this._canExecute( parameter );
        }

        ///=================================================================================================
        /// <summary>   Executes the given parameter. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <param name="parameter">    The parameter. </param>
        ///=================================================================================================
        public void Execute( object parameter ) {
            this._execute( parameter );
        }

        /// <summary>   Event queue for all listeners interested in CanExecuteChanged events. </summary>
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion
    }
}
