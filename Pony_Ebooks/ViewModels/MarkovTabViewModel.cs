// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MarkovTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:55 PM, 27/01/2015
// //  Created Date: 9:53 PM, 27/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

using log4net;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Markov tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/27/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.TabViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.IMarkovTabViewModel"/>
    ///=================================================================================================
    public class MarkovTabViewModel : TabViewModel, IMarkovTabViewModel {

        private static readonly ILog _log = LogManager.GetLogger( typeof( MarkovTabViewModel ) );

        private IMarkovManager _markovManager;

        /// <summary>   The maximum characters. </summary>
        private int _maxChars;

        /// <summary>   The minimum characters. </summary>
        private int _minChars;

        /// <summary>   The order. </summary>
        private int _order;

        /// <summary>   true if sources loaded. </summary>
        private bool _sourcesLoaded;

        /// <summary>   The weight. </summary>
        private int _weight;

        public MarkovTabViewModel( IMarkovManager markovManager ) {
            _markovManager = markovManager;
        }


        #region IMarkovTabViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum characters. </summary>
        ///
        /// <value> The minimum characters. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.MinChars"/>
        ///=================================================================================================
        public int MinChars {
            get { return this._minChars; }
            set {
                this._minChars = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum characters. </summary>
        ///
        /// <value> The maximum characters. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.MaxChars"/>
        ///=================================================================================================
        public int MaxChars {
            get { return this._maxChars; }
            set {
                this._maxChars = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the weight. </summary>
        ///
        /// <value> The weight. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.Weight"/>
        ///=================================================================================================
        public int Weight {
            get { return this._weight; }
            set {
                this._weight = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the order. </summary>
        ///
        /// <value> The order. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.Order"/>
        ///=================================================================================================
        public int Order {
            get { return this._order; }
            set {
                this._order = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets a value indicating whether the sources was loaded. </summary>
        ///
        /// <value> true if sources loaded, false if not. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.SourcesLoaded"/>
        ///=================================================================================================
        public bool SourcesLoaded {
            get { return this._sourcesLoaded; }
            set {
                this._sourcesLoaded = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets source texts. </summary>
        ///
        /// <value> The source texts. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.SourceTexts"/>
        ///=================================================================================================
        public ObservableCollection<Tuple<string, bool>> SourceTexts { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the save settings command. </summary>
        ///
        /// <value> The save settings command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.SaveSettingsCommand"/>
        ///=================================================================================================
        public IRelayCommand SaveSettingsCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the reload settings command. </summary>
        ///
        /// <value> The reload settings command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.ReloadSettingsCommand"/>
        ///=================================================================================================
        public IRelayCommand ReloadSettingsCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the add source command. </summary>
        ///
        /// <value> The add source command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.AddSourceCommand"/>
        ///=================================================================================================
        public IRelayCommand AddSourceCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the load selected sources command. </summary>
        ///
        /// <value> The load selected sources command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.LoadSelectedSourcesCommand"/>
        ///=================================================================================================
        public IRelayCommand LoadSelectedSourcesCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the remove selected sources command. </summary>
        ///
        /// <value> The remove selected sources command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMarkovTabViewModel.RemoveSelectedSourcesCommand"/>
        ///=================================================================================================
        public IRelayCommand RemoveSelectedSourcesCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/27/2015. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.ViewModels.IMarkovTabViewModel.Initialize()"/>
        ///=================================================================================================
        public bool Initialize( ) {
            // Set vars from base
            if (!LoadVarsFromSource()) {
                _log.Error( "Unable to load values from source into viewmodel." );
                return false;
            }
            

            // Load commands
            SaveSettingsCommand = new RelayCommand( OnSaveSettings, CanSaveSettings );
            ReloadSettingsCommand = new RelayCommand( OnReloadSettings );
            AddSourceCommand = new RelayCommand(OnAddSource);
            LoadSelectedSourcesCommand = new RelayCommand( OnLoadSelectedSources, CanLoadSelectedSources );
            RemoveSelectedSourcesCommand = new RelayCommand( OnRemoveSelectedSources, CanRemoveSelectedSources );


            return true;
        }

        private bool CanRemoveSelectedSources( object obj ) {
            throw new NotImplementedException( );
        }

        private void OnRemoveSelectedSources( object obj ) {
            throw new NotImplementedException( );
        }

        private bool CanLoadSelectedSources( object obj ) {
            throw new NotImplementedException( );
        }

        private void OnLoadSelectedSources( object obj ) {
            throw new NotImplementedException( );
        }

        private void OnAddSource( object obj ) {
            throw new NotImplementedException( );
        }

        private void OnReloadSettings( object obj ) {
            throw new NotImplementedException( );
        }

        private bool CanSaveSettings( object obj ) {
            throw new NotImplementedException( );
        }

        private void OnSaveSettings( object obj ) {
            throw new NotImplementedException( );
        }

        private bool LoadVarsFromSource( ) {
            try {
                MinChars = _markovManager.MinChars;
                MaxChars = _markovManager.MaxChars;
                Weight = _markovManager.MarkovWeight;
                Order = _markovManager.MarkovOrder;

            } catch( Exception e ) {
                _log.Error( "Error setting values.", e );
                return false;
            }

            return true;
        }

        #endregion
    }
}
