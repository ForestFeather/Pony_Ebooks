// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - InitialStatesTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:38 AM, 08/02/2015
// //  Created Date: 6:32 AM, 08/02/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Initial states view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.TabViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.IInitialStatesViewModel"/>
    ///=================================================================================================
    public class InitialStatesTabViewModel : TabViewModel, IInitialStatesViewModel {

        /// <summary>   Manager for markov. </summary>
        private readonly IMarkovManager _markovManager;

        /// <summary>   List of states of the initials. </summary>
        private ObservableCollection<string> _initialStates;

        /// <summary>   The selected state. </summary>
        private string _selectedState;

        /// <summary>   true to use specified initial state. </summary>
        private bool _useSpecifiedInitialState;

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <param name="markovManager">    Manager for markov. </param>
        ///=================================================================================================
        public InitialStatesTabViewModel( IMarkovManager markovManager ) {
            this._markovManager = markovManager;
        }

        #endregion

        #region IInitialStatesViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets a list of states of the initials. </summary>
        ///
        /// <value> The initial states. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IInitialStatesViewModel.InitialStates"/>
        ///=================================================================================================
        public ObservableCollection<string> InitialStates {
            get { return this._initialStates; }
            set {
                this._initialStates = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the selected state. </summary>
        ///
        /// <value> The selected state. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IInitialStatesViewModel.SelectedState"/>
        ///=================================================================================================
        public string SelectedState {
            get { return this._selectedState; }
            set {
                this._selectedState = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>
        ///     Gets or sets a value indicating whether this object use specified initial state.
        /// </summary>
        ///
        /// <value> true if use specified initial state, false if not. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IInitialStatesViewModel.UseSpecifiedInitialState"/>
        ///=================================================================================================
        public bool UseSpecifiedInitialState {
            get { return this._useSpecifiedInitialState; }
            set {
                this._useSpecifiedInitialState = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the refresh states command. </summary>
        ///
        /// <value> The refresh states command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IInitialStatesViewModel.RefreshStatesCommand"/>
        ///=================================================================================================
        public IRelayCommand RefreshStatesCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.ViewModels.IInitialStatesViewModel.Initialize()"/>
        ///=================================================================================================
        public bool Initialize( ) {
            this.RefreshStatesCommand = new RelayCommand( this.GenerateStates );

            this.GenerateStates( null );
            return true;
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Generates the states. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void GenerateStates( object obj ) {
            this.InitialStates = new ObservableCollection<string>( this._markovManager.GetInitialChains( ) );
            this.SelectedState = null;
        }

        #endregion
    }
}
