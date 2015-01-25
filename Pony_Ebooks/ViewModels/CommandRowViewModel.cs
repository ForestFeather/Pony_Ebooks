// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - CommandRowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:28 AM, 25/01/2015
// //  Created Date: 8:30 AM, 24/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

using log4net;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Command row view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.ViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.ICommandRowViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ICommandRowViewModel"/>
    ///=================================================================================================
    public class CommandRowViewModel : ViewModel, ICommandRowViewModel {
        #region Fields and Constants

        /// <summary>   The log. </summary>
        private static readonly ILog _log = LogManager.GetLogger( typeof( CommandRowViewModel ) );

        #endregion

        /// <summary>   Manager for markov. </summary>
        private readonly IMarkovManager _markovManager;

        /// <summary>   The timer control. </summary>
        private readonly ITimerControl _timerControl;

        /// <summary>   Manager for tweet. </summary>
        private readonly ITweetManager _tweetManager;

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="timerControl">     The timer control. </param>
        /// <param name="markovManager">    Manager for markov. </param>
        /// <param name="tweetManager">     Manager for tweet. </param>
        ///=================================================================================================
        public CommandRowViewModel( ITimerControl timerControl, IMarkovManager markovManager, ITweetManager tweetManager ) {
            this._timerControl = timerControl;
            this._markovManager = markovManager;
            this._tweetManager = tweetManager;

            this.PostChainCommand = new RelayCommand( this.PostChain );
            this.NewChainCommand = new RelayCommand( this.NewChain );
            this.NewTimeCommand = new RelayCommand( this.NewTime );
        }

        #endregion

        #region ICommandRowViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets the post chain command. </summary>
        ///
        /// <value> The post chain command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICommandRowViewModel.PostChainCommand"/>
        /// <seealso cref="P:Pony_Ebooks.ICommandRowViewModel.PostChainCommand"/>
        ///=================================================================================================
        public IRelayCommand PostChainCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the new chain command. </summary>
        ///
        /// <value> The new chain command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICommandRowViewModel.NewChainCommand"/>
        /// <seealso cref="P:Pony_Ebooks.ICommandRowViewModel.NewChainCommand"/>
        ///=================================================================================================
        public IRelayCommand NewChainCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the new time command. </summary>
        ///
        /// <value> The new time command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICommandRowViewModel.NewTimeCommand"/>
        /// <seealso cref="P:Pony_Ebooks.ICommandRowViewModel.NewTimeCommand"/>
        ///=================================================================================================
        public IRelayCommand NewTimeCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the post action. </summary>
        ///
        /// <value> The post action. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICommandRowViewModel.PostAction"/>
        ///=================================================================================================
        public Action<object> PostAction { get; set; }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Creates a new time. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void NewTime( object obj ) {
            this._timerControl.NewTriggerTime( );
        }

        ///=================================================================================================
        /// <summary>   Creates a new chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void NewChain( object obj ) {
            this._markovManager.GenerateNewChain( );
        }

        ///=================================================================================================
        /// <summary>   Posts a chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void PostChain( object obj ) {
            this.PostAction( obj );
        }

        #endregion
    }
}
