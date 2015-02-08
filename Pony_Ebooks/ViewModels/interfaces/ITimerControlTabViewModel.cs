// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ITimerControlTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 11:24 AM, 08/02/2015
// //  Created Date: 11:14 AM, 08/02/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;

using Pony_Ebooks.Framework;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Interface for timer control tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
    ///
    /// <seealso cref="T:ITabViewModel"/>
    ///=================================================================================================
    public interface ITimerControlTabViewModel : ITabViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum seconds. </summary>
        ///
        /// <value> The minimum seconds. </value>
        ///=================================================================================================
        int MinSeconds { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum seconds. </summary>
        ///
        /// <value> The maximum seconds. </value>
        ///=================================================================================================
        int MaxSeconds { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum minutes. </summary>
        ///
        /// <value> The minimum minutes. </value>
        ///=================================================================================================
        int MinMinutes { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum minutes. </summary>
        ///
        /// <value> The maximum minutes. </value>
        ///=================================================================================================
        int MaxMinutes { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum hours. </summary>
        ///
        /// <value> The minimum hours. </value>
        ///=================================================================================================
        int MinHours { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum hours. </summary>
        ///
        /// <value> The maximum hours. </value>
        ///=================================================================================================
        int MaxHours { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the total minimum timespan. </summary>
        ///
        /// <value> The total number of minimum timespan. </value>
        ///=================================================================================================
        TimeSpan TotalMinTimespan { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the total maximum timespan. </summary>
        ///
        /// <value> The total number of maximum timespan. </value>
        ///=================================================================================================
        TimeSpan TotalMaxTimespan { get; set; }

        ///=================================================================================================
        /// <summary>   Gets the set new timespans command. </summary>
        ///
        /// <value> The set new timespans command. </value>
        ///=================================================================================================
        IRelayCommand SetNewTimespansCommand { get; }

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
