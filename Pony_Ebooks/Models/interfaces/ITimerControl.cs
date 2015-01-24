// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ITimerControl.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:32 AM, 24/01/2015
// //  Created Date: 9:47 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;

#endregion

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Interface for timer control. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///=================================================================================================
    public interface ITimerControl {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets the time remaining. </summary>
        ///
        /// <value> The time remaining. </value>
        ///=================================================================================================
        TimeSpan TimeRemaining { get; }

        ///=================================================================================================
        /// <summary>   Gets the time of the trigger. </summary>
        ///
        /// <value> The time of the trigger. </value>
        ///=================================================================================================
        DateTime TriggerTime { get; }

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

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Initialize( );

        ///=================================================================================================
        /// <summary>   Starts this object. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Start( );

        ///=================================================================================================
        /// <summary>   Stops this object. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Stop( );

        /// <summary>   Creates a new trigger time. </summary>
        void NewTriggerTime( );

        ///=================================================================================================
        /// <summary>   Creates a new trigger time. </summary>
        ///
        /// <param name="minSeconds">   The minimum seconds. </param>
        /// <param name="maxSeconds">   The maximum seconds. </param>
        ///=================================================================================================
        void NewTriggerTime( int minSeconds, int maxSeconds );

        #endregion
    }
}
