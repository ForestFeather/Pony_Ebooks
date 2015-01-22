// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - RandomWrapper.cs 
// // 
// //  Copyright 2011-2013
// //  WR Medical Electronics Company
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:47 AM, 21/01/2015
// //  Created Date: 6:46 AM, 21/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;

#endregion

namespace Pony_Ebooks.Markov {
    ///=================================================================================================
    /// <summary>
    ///     Wraps an instance of <see cref="System.Random"/> to provide the <see cref="IRandom"/>
    ///     interface.
    /// </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Markov.IRandom"/>
    ///=================================================================================================
    public class RandomWrapper : IRandom {

        /// <summary>   Holds the instance of <see cref="System.Random"/> being wrapped. </summary>
        private readonly Random rand;

        #region Constructors

        ///=================================================================================================
        /// <summary>
        ///     Initializes a new instance of the RandomWrapper class, wrapping a given
        ///     <see cref="System.Random"/> instance.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="rand"> The instance of <see cref="System.Random"/> to wrap. </param>
        ///=================================================================================================
        public RandomWrapper( Random rand ) {
            if( rand == null ) {
                throw new ArgumentNullException( "rand" );
            }

            this.rand = rand;
        }

        #endregion

        #region IRandom Members

        ///=================================================================================================
        /// <summary>   Returns a nonnegative random number less than the specified maximum. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="maxValue"> The exclusive upper bound of the random number to be generated.
        ///                         maxValue must be greater than or equal to zero. </param>
        ///
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to zero, and less than maxValue; that is,
        ///     the range of return values ordinarily includes zero but not maxValue. However, if
        ///     maxValue equals zero, maxValue is returned.
        /// </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Markov.IRandom.IRandom.Next(int)"/>
        ///=================================================================================================
        int IRandom.Next( int maxValue ) {
            return this.rand.Next( maxValue );
        }

        #endregion
    }
}
