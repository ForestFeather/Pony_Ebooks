// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IInitialStatesTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:51 AM, 08/02/2015
// //  Created Date: 6:25 AM, 08/02/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Interface for initial states view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
    ///
    /// <seealso cref="T:ITabViewModel"/>
    ///=================================================================================================
    public interface IInitialStatesTabViewModel : ITabViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets a list of states of the initials. </summary>
        ///
        /// <value> The initial states. </value>
        ///=================================================================================================
        ObservableCollection<string> InitialStates { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the selected state. </summary>
        ///
        /// <value> The selected state. </value>
        ///=================================================================================================
        string SelectedState { get; set; }

        ///=================================================================================================
        /// <summary>
        ///     Gets or sets a value indicating whether this object use specified initial state.
        /// </summary>
        ///
        /// <value> true if use specified initial state, false if not. </value>
        ///=================================================================================================
        bool UseSpecifiedInitialState { get; set; }

        ///=================================================================================================
        /// <summary>   Gets the refresh states command. </summary>
        ///
        /// <value> The refresh states command. </value>
        ///=================================================================================================
        IRelayCommand RefreshStatesCommand { get; }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Initialize( );

        #endregion
    }
}
