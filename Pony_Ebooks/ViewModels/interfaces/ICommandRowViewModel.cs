// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ICommandRowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 7:02 AM, 08/02/2015
// //  Created Date: 7:11 AM, 25/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;

using Pony_Ebooks.Framework;

#endregion

namespace Pony_Ebooks.ViewModels {
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

        ///=================================================================================================
        /// <summary>   Gets or sets the post action. </summary>
        ///
        /// <value> The post action. </value>
        ///=================================================================================================
        Action<object> PostAction { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the generate chain action. </summary>
        ///
        /// <value> The generate chain action. </value>
        ///=================================================================================================
        Action<object> GenerateChainAction { get; set; }

        #endregion
    }
}
