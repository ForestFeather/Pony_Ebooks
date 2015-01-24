// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:19 PM, 23/01/2015
// //  Created Date: 8:13 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.ComponentModel;

#endregion

namespace Pony_Ebooks.Framework {
    ///=================================================================================================
    /// <summary>   Interface for view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///
    /// <seealso cref="T:INotifyPropertyChanged"/>
    /// <seealso cref="T:IDisposable"/>
    ///=================================================================================================
    public interface IViewModel : INotifyPropertyChanged, IDisposable {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        ///=================================================================================================
        string Title { get; set; }

        #endregion
    }
}
