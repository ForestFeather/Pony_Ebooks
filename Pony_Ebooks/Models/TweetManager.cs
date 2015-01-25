// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - TweetManager.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:39 AM, 24/01/2015
// //  Created Date: 8:38 AM, 24/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using Tweetinvi;

#endregion

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Manager for tweets. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Models.ITweetManager"/>
    ///=================================================================================================
    public class TweetManager : ITweetManager {
        #region ITweetManager Members

        ///=================================================================================================
        /// <summary>   Post this message. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="text"> The text. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.ITweetManager.Post(string)"/>
        ///=================================================================================================
        public bool Post( string text ) {
            // UNCOMMENT ME WHEN DONE TESTING
            //Tweet.PublishTweet( text );
            return true;
        }

        #endregion
    }
}
