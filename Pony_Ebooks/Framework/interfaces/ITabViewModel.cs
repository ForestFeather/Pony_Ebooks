// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ITabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:09 AM, 28/01/2015
// //  Created Date: 7:11 AM, 25/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

namespace Pony_Ebooks.Framework {
    ///=================================================================================================
    /// <summary>   Interface for tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///
    /// <seealso cref="T:IViewModel"/>
    ///=================================================================================================
    public interface ITabViewModel : IViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets a value indicating whether this object is dirty. </summary>
        ///
        /// <value> true if this object is dirty, false if not. </value>
        ///=================================================================================================
        bool IsDirty { get; set; }

        #endregion
    }
}
