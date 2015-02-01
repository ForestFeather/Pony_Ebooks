// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IMarkovTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:52 PM, 27/01/2015
// //  Created Date: 9:46 PM, 27/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Interface for markov tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/27/2015. </remarks>
    ///
    /// <seealso cref="T:ITabViewModel"/>
    ///=================================================================================================
    public interface IMarkovTabViewModel : ITabViewModel {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum characters. </summary>
        ///
        /// <value> The minimum characters. </value>
        ///=================================================================================================
        int MinChars { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum characters. </summary>
        ///
        /// <value> The maximum characters. </value>
        ///=================================================================================================
        int MaxChars { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the weight. </summary>
        ///
        /// <value> The weight. </value>
        ///=================================================================================================
        int Weight { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the order. </summary>
        ///
        /// <value> The order. </value>
        ///=================================================================================================
        int Order { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets a value indicating whether the sources was loaded. </summary>
        ///
        /// <value> true if sources loaded, false if not. </value>
        ///=================================================================================================
        bool SourcesLoaded { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets source texts. </summary>
        ///
        /// <value> The source texts. </value>
        ///=================================================================================================
        ObservableCollection<Pair<string, bool>> SourceTexts { get; set; }

        ///=================================================================================================
        /// <summary>   Gets the save settings command. </summary>
        ///
        /// <value> The save settings command. </value>
        ///=================================================================================================
        IRelayCommand SaveSettingsCommand { get; }

        ///=================================================================================================
        /// <summary>   Gets the reload settings command. </summary>
        ///
        /// <value> The reload settings command. </value>
        ///=================================================================================================
        IRelayCommand ReloadSettingsCommand { get; }

        ///=================================================================================================
        /// <summary>   Gets the add source command. </summary>
        ///
        /// <value> The add source command. </value>
        ///=================================================================================================
        IRelayCommand AddSourceCommand { get; }

        ///=================================================================================================
        /// <summary>   Gets the load selected sources command. </summary>
        ///
        /// <value> The load selected sources command. </value>
        ///=================================================================================================
        IRelayCommand LoadSelectedSourcesCommand { get; }

        ///=================================================================================================
        /// <summary>   Gets the remove selected sources command. </summary>
        ///
        /// <value> The remove selected sources command. </value>
        ///=================================================================================================
        IRelayCommand RemoveSelectedSourcesCommand { get; }

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
