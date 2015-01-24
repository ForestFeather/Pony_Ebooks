// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IMainWindowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:56 AM, 24/01/2015
// //  Created Date: 8:37 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Interface for main window view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///=================================================================================================
    public interface IMainWindowViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets the current status view model. </summary>
        ///
        /// <value> The current status view model. </value>
        ///=================================================================================================
        ICurrentStatusViewModel CurrentStatusViewModel { get; }

        ///=================================================================================================
        /// <summary>   Gets the command row view model. </summary>
        ///
        /// <value> The command row view model. </value>
        ///=================================================================================================
        ICommandRowViewModel CommandRowViewModel { get; }

        ///=================================================================================================
        /// <summary>   Gets the manager for tweet. </summary>
        ///
        /// <value> The tweet manager. </value>
        ///=================================================================================================
        ITweetManager TweetManager { get; }

        ///=================================================================================================
        /// <summary>   Gets the tab view models. </summary>
        ///
        /// <value> The tab view models. </value>
        ///=================================================================================================
        ObservableCollection<ITabViewModel> TabViewModels { get; }

        ///=================================================================================================
        /// <summary>   Gets the manager for markov. </summary>
        ///
        /// <value> The markov manager. </value>
        ///=================================================================================================
        IMarkovManager MarkovManager { get; }

        ///=================================================================================================
        /// <summary>   Gets the timer control. </summary>
        ///
        /// <value> The timer control. </value>
        ///=================================================================================================
        ITimerControl TimerControl { get; }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Initialize( );

        #endregion
    }
}
