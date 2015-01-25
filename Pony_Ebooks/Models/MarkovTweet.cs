// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MarkovTweet.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:44 PM, 23/01/2015
// //  Created Date: 9:43 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;

#endregion

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Markov tweet. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///=================================================================================================
    public class MarkovTweet {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the text. </summary>
        ///
        /// <value> The text. </value>
        ///=================================================================================================
        public string Text { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the Date/Time of the time. </summary>
        ///
        /// <value> The time. </value>
        ///=================================================================================================
        public DateTime Time { get; set; }

        #endregion
    }
}
