// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ICurrentStatusViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:19 AM, 24/01/2015
// //  Created Date: 8:38 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Interface for current status view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///=================================================================================================
    public interface ICurrentStatusViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets the timer control. </summary>
        ///
        /// <value> The timer control. </value>
        ///=================================================================================================
        ITimerControl TimerControl { get; }

        ///=================================================================================================
        /// <summary>   Gets the manager for markov. </summary>
        ///
        /// <value> The markov manager. </value>
        ///=================================================================================================
        IMarkovManager MarkovManager { get; }

        ///=================================================================================================
        /// <summary>   Gets or sets the next chain. </summary>
        ///
        /// <value> The next chain. </value>
        ///=================================================================================================
        string NextChain { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the time of the post. </summary>
        ///
        /// <value> The time of the post. </value>
        ///=================================================================================================
        string PostTime { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the countdown timer. </summary>
        ///
        /// <value> The total number of down timer. </value>
        ///=================================================================================================
        string CountdownTimer { get; set; }

        #endregion
    }
}
