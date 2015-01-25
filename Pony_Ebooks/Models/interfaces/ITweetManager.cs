// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ITweetManager.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:33 AM, 24/01/2015
// //  Created Date: 8:33 AM, 24/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Interface for tweet manager. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///=================================================================================================
    public interface ITweetManager {
        #region Members

        ///=================================================================================================
        /// <summary>   Post this message. </summary>
        ///
        /// <param name="text"> The text. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Post( string text );

        #endregion
    }
}
