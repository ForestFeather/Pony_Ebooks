// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - PastTweetsTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:10 AM, 25/01/2015
// //  Created Date: 4:56 AM, 25/01/2015
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
    /// <summary>   Past tweets tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/25/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.TabViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.IPastTweetsTabViewModel"/>
    ///=================================================================================================
    public class PastTweetsTabViewModel : TabViewModel, IPastTweetsTabViewModel {
        #region Constructors

        ///=================================================================================================
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/25/2015. </remarks>
        ///=================================================================================================
        public PastTweetsTabViewModel( ) {
            this.PastTweets = new ObservableCollection<MarkovTweet>( );
            this.Title = "Past Tweets";
        }

        #endregion

        #region IPastTweetsTabViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets the past tweets. </summary>
        ///
        /// <value> The past tweets. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IPastTweetsTabViewModel.PastTweets"/>
        ///=================================================================================================
        public ObservableCollection<MarkovTweet> PastTweets { get; private set; }

        ///=================================================================================================
        /// <summary>   Adds a tweet to 'time'. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/25/2015. </remarks>
        ///
        /// <param name="text"> The text. </param>
        /// <param name="time"> Date/Time of the time. </param>
        ///
        /// <seealso cref="M:Pony_Ebooks.ViewModels.IPastTweetsTabViewModel.AddTweet(string,DateTime)"/>
        ///=================================================================================================
        public void AddTweet( string text, DateTime time ) {
            this.PastTweets.Insert( 0, new MarkovTweet { Text = text, Time = time } );
        }

        #endregion
    }
}
