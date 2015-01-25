// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IPastTweetsTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:05 AM, 25/01/2015
// //  Created Date: 5:02 AM, 25/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Interface for past tweets tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/25/2015. </remarks>
    ///
    /// <seealso cref="T:ITabViewModel"/>
    ///=================================================================================================
    public interface IPastTweetsTabViewModel : ITabViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets the past tweets. </summary>
        ///
        /// <value> The past tweets. </value>
        ///=================================================================================================
        ObservableCollection<MarkovTweet> PastTweets { get; }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Adds a tweet to 'time'. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/25/2015. </remarks>
        ///
        /// <param name="text"> The text. </param>
        /// <param name="time"> Date/Time of the time. </param>
        ///=================================================================================================
        void AddTweet( string text, DateTime time );

        #endregion
    }
}
