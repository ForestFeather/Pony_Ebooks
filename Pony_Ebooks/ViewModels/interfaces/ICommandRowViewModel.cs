﻿// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ICommandRowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:07 PM, 23/01/2015
// //  Created Date: 8:38 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using Pony_Ebooks.Framework;

#endregion

namespace Pony_Ebooks {
    ///=================================================================================================
    /// <summary>   Interface for command row view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///=================================================================================================
    public interface ICommandRowViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets the post chain command. </summary>
        ///
        /// <value> The post chain command. </value>
        ///=================================================================================================
        IRelayCommand PostChainCommand { get; }

        ///=================================================================================================
        /// <summary>   Gets the new chain command. </summary>
        ///
        /// <value> The new chain command. </value>
        ///=================================================================================================
        IRelayCommand NewChainCommand { get; }

        ///=================================================================================================
        /// <summary>   Gets the new time command. </summary>
        ///
        /// <value> The new time command. </value>
        ///=================================================================================================
        IRelayCommand NewTimeCommand { get; }

        #endregion
    }
}