// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - Pair.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 10:19 PM, 31/01/2015
// //  Created Date: 10:17 PM, 31/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

using System;

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Pair. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    /// <typeparam name="U">    Generic type parameter. </typeparam>
    ///=================================================================================================
    [Serializable]
    public class Pair<T, U> {
        #region Constructors

        ///=================================================================================================
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///=================================================================================================
        public Pair( ) { }

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="item1">    The first item. </param>
        /// <param name="item2">    The second item. </param>
        ///=================================================================================================
        public Pair( T item1, U item2 ) {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        #endregion

        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the item 1. </summary>
        ///
        /// <value> The item 1. </value>
        ///=================================================================================================
        public T Item1 { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the item 2. </summary>
        ///
        /// <value> The item 2. </value>
        ///=================================================================================================
        public U Item2 { get; set; }

        #endregion
    }
}
