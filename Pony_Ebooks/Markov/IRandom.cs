// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IRandom.cs 
// // 
// //  Copyright 2011-2013
// //  WR Medical Electronics Company
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:47 AM, 21/01/2015
// //  Created Date: 6:47 AM, 21/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

namespace Pony_Ebooks.Markov {
    ///=================================================================================================
    /// <summary>
    ///     Represents the interface for a device that produces a sequence of numbers that meet
    ///     certain statistical requirements for randomness.
    /// </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///=================================================================================================
    public interface IRandom {
        #region Members

        ///=================================================================================================
        /// <summary>   Returns a nonnegative random number less than the specified maximum. </summary>
        ///
        /// <exception cref="System.ArgumentOutOfRangeException">   <paramref name="maxValue"/> is less
        ///                                                         than zero. </exception>
        ///
        /// <param name="maxValue"> The exclusive upper bound of the random number to be generated.
        ///                         maxValue must be greater than or equal to zero. </param>
        ///
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to zero, and less than maxValue; that is,
        ///     the range of return values ordinarily includes zero but not maxValue. However, if
        ///     maxValue equals zero, maxValue is returned.
        /// </returns>
        ///=================================================================================================
        int Next( int maxValue );

        #endregion
    }
}
