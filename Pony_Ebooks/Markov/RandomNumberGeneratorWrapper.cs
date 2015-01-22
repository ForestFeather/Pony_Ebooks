// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - RandomNumberGeneratorWrapper.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:00 PM, 21/01/2015
// //  Created Date: 6:48 AM, 21/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Security.Cryptography;

#endregion

namespace Pony_Ebooks.Markov {
    ///=================================================================================================
    /// <summary>
    ///     Wraps an instance of <see cref="System.Security.Cryptography.RandomNumberGenerator"/> to
    ///     provide the <see cref="IRandom"/> interface.
    /// </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Markov.IRandom"/>
    ///=================================================================================================
    public class RandomNumberGeneratorWrapper : IRandom {

        ///=================================================================================================
        /// <summary>
        ///     Holds the instance of <see cref="System.Security.Cryptography.RandomNumberGenerator"/>
        ///     being wrapped.
        /// </summary>
        ///=================================================================================================
        private readonly RandomNumberGenerator rand;

        #region Constructors

        ///=================================================================================================
        /// <summary>
        ///     Initializes a new instance of the RandomNumberGeneratorWrapper class, wrapping a given
        ///     <see cref="System.Security.Cryptography.RandomNumberGenerator"/> instance.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="rand"> The instance of
        ///                     <see cref="System.Security.Cryptography.RandomNumberGenerator"/> to wrap. </param>
        ///=================================================================================================
        public RandomNumberGeneratorWrapper( RandomNumberGenerator rand ) {
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
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
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
            if( maxValue < 0 ) {
                throw new ArgumentOutOfRangeException( "maxValue" );
            }

            if( maxValue == 0 ) {
                return 0;
            }

            ulong chop = ulong.MaxValue - ( ulong.MaxValue % (ulong) maxValue );

            ulong rand;
            do {
                rand = this.NextUlong( );
            } while( rand >= chop );

            return (int) ( rand % (ulong) maxValue );
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>
        ///     Reads sixty-four bits of data from the wrapped
        ///     <see cref="System.Security.Cryptography.RandomNumberGenerator"/> instance, and converts
        ///     them to a <see cref="System.UInt64"/>.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   A random <see cref="System.UInt64"/>. </returns>
        ///=================================================================================================
        private ulong NextUlong( ) {
            byte[] data = new byte[8];
            this.rand.GetBytes( data );
            return BitConverter.ToUInt64( data, 0 );
        }

        #endregion
    }
}
