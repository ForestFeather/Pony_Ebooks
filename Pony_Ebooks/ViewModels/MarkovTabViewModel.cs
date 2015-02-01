// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MarkovTabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 10:26 PM, 31/01/2015
// //  Created Date: 9:53 PM, 27/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.ObjectModel;

using Microsoft.Win32;

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
        #region Fields and Constants

        /// <summary>   The log. </summary>
        private static readonly ILog _log = LogManager.GetLogger( typeof( MarkovTabViewModel ) );

        #endregion

        /// <summary>   Manager for markov. </summary>
        private readonly IMarkovManager _markovManager;

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

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="markovManager">    Manager for markov. </param>
        ///=================================================================================================
        public MarkovTabViewModel( IMarkovManager markovManager ) {
            this._markovManager = markovManager;
            this.SourceTexts = new ObservableCollection<Pair<string, bool>>( );
            this.Title = "Markov Control";
        }

        #endregion

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
        public ObservableCollection<Pair<string, bool>> SourceTexts { get; set; }

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
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.ViewModels.IMarkovTabViewModel.Initialize()"/>
        ///=================================================================================================
        public bool Initialize( ) {
            // Set vars from base
            if( !this.LoadVarsFromSource( ) ) {
                _log.Error( "Unable to load values from source into viewmodel." );
                return false;
            }

            // Load commands
            this.SaveSettingsCommand = new RelayCommand( this.OnSaveSettings, this.CanSaveSettings );
            this.ReloadSettingsCommand = new RelayCommand( this.OnReloadSettings );
            this.AddSourceCommand = new RelayCommand( this.OnAddSource );
            this.LoadSelectedSourcesCommand = new RelayCommand(
                this.OnLoadSelectedSources, this.CanLoadSelectedSources );
            this.RemoveSelectedSourcesCommand = new RelayCommand(
                this.OnRemoveSelectedSources, this.CanRemoveSelectedSources );

            return true;
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Determine if we can remove selected sources. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///
        /// <returns>   true if we can remove selected sources, false if not. </returns>
        ///=================================================================================================
        private bool CanRemoveSelectedSources( object obj ) {
            return true;
        }

        ///=================================================================================================
        /// <summary>   Executes the remove selected sources action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void OnRemoveSelectedSources( object obj ) {
            // Get rid of them from the root
            this._markovManager.SourceTexts.Clear( );
            foreach( var sourceText in this.SourceTexts ) {
                if( !sourceText.Item2 ) {
                    this._markovManager.AddSource( sourceText.Item1, true );
                } else {
                    this.SourceTexts.Remove( sourceText );
                }
            }
        }

        ///=================================================================================================
        /// <summary>   Determine if we can load selected sources. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///
        /// <returns>   true if we can load selected sources, false if not. </returns>
        ///=================================================================================================
        private bool CanLoadSelectedSources( object obj ) {
            return true;
        }

        ///=================================================================================================
        /// <summary>   Executes the load selected sources action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void OnLoadSelectedSources( object obj ) {
            // Save sources and load them
            this._markovManager.SourceTexts.Clear( );
            foreach( var sourceText in this.SourceTexts ) {
                this._markovManager.AddSource( sourceText.Item1, sourceText.Item2 );
            }
        }

        ///=================================================================================================
        /// <summary>   Executes the add source action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void OnAddSource( object obj ) {
            var fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Text files (*.txt)|*.txt";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileDialog.Multiselect = true;

            if( fileDialog.ShowDialog( ) != true ) {
                return;
            }

            // Load each source text item
            foreach( var filename in fileDialog.FileNames ) this.SourceTexts.Add( new Pair<string, bool>( filename, true ) );
        }

        ///=================================================================================================
        /// <summary>   Executes the reload settings action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void OnReloadSettings( object obj ) {
            this.LoadVarsFromSource( );
            this.IsDirty = false;
        }

        ///=================================================================================================
        /// <summary>   Determine if we can save settings. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///
        /// <returns>   true if we can save settings, false if not. </returns>
        ///=================================================================================================
        private bool CanSaveSettings( object obj ) {
            return this.IsDirty;
        }

        ///=================================================================================================
        /// <summary>   Executes the save settings action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void OnSaveSettings( object obj ) {
            try {
                this._markovManager.MinChars = this.MinChars;
                this._markovManager.MaxChars = this.MaxChars;
                this._markovManager.MarkovWeight = this.Weight;
                this._markovManager.MarkovOrder = this.Order;
            } catch( Exception e ) {
                _log.Error( "Error saving values.", e );
                return;
            }

            // Save sources
            this._markovManager.SourceTexts.Clear( );
            foreach( var sourceText in this.SourceTexts ) {
                this._markovManager.AddSource( sourceText.Item1, sourceText.Item2 );
            }


            this.IsDirty = false;
        }

        ///=================================================================================================
        /// <summary>   Loads variables from source. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/31/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        private bool LoadVarsFromSource( ) {
            try {
                // Load static
                this.MinChars = this._markovManager.MinChars;
                this.MaxChars = this._markovManager.MaxChars;
                this.Weight = this._markovManager.MarkovWeight;
                this.Order = this._markovManager.MarkovOrder;

                // Load sources
                this.SourceTexts.Clear( );
                foreach( var sourceText in this._markovManager.SourceTexts ) {
                    this.SourceTexts.Add( new Pair<string, bool>( sourceText.Item1, sourceText.Item2 ) );
                }
            } catch( Exception e ) {
                _log.Error( "Error setting values.", e );
                return false;
            }

            return true;
        }

        #endregion
    }
}
