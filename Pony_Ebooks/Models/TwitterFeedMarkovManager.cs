// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - TwitterFeedMarkovManager.cs 
// // 
// //  Last Changed By: ForestFeather - 
// //  Last Changed Date: 6:11 AM, 05/05/2015
// //  Created Date: 5:24 AM, 05/05/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using log4net;

using Pony_Ebooks.Markov;

using Tweetinvi;
using Tweetinvi.Core.Interfaces;

namespace Pony_Ebooks.Models {

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Manager for twitter feed markovs. </summary>
    ///
    /// <remarks>   Forest Feather, 5/5/2015. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class TwitterFeedMarkovManager : IMarkovManager {

        /// <summary>   The log. </summary>
        private static readonly ILog _log = LogManager.GetLogger( typeof( TwitterFeedMarkovManager ) );

        /// <summary>   Source files. </summary>
        private readonly IDictionary<string, bool> _sourceFiles;

        /// <summary>   List of identifiers for the last tweets. </summary>
        private long[] _lastTweetIds;

        /// <summary>   The next chain. </summary>
        private string _nextChain;

        /// <summary>   The previous chain. </summary>
        private string _previousChain;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <param name="sourceFiles">  Source files. </param>
        ///-------------------------------------------------------------------------------------------------

        public TwitterFeedMarkovManager( IDictionary<string, bool> sourceFiles ) {
            this._sourceFiles = sourceFiles;
            _log.Info( "Instantiated Markov Manager with " + this._sourceFiles.Count + " sources." );
            this.PropertyChanged += ( sender, args ) => { this.UpdateTwitterTimeline( ); };
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the markov chain. </summary>
        ///
        /// <value> The markov chain. </value>
        ///-------------------------------------------------------------------------------------------------

        protected MarkovChain<string> MarkovChain { get; set; }

        /// <summary>   Event queue for all listeners interested in PropertyChanged events. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the minimum characters. </summary>
        ///
        /// <value> The minimum characters. </value>
        ///-------------------------------------------------------------------------------------------------

        public int MinChars { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the maximum characters. </summary>
        ///
        /// <value> The maximum characters. </value>
        ///-------------------------------------------------------------------------------------------------

        public int MaxChars { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the markov weight. </summary>
        ///
        /// <value> The markov weight. </value>
        ///-------------------------------------------------------------------------------------------------

        public int MarkovWeight { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the markov order. </summary>
        ///
        /// <value> The markov order. </value>
        ///-------------------------------------------------------------------------------------------------

        public int MarkovOrder { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the next chain. </summary>
        ///
        /// <value> The next chain. </value>
        ///-------------------------------------------------------------------------------------------------

        public string NextChain {
            get { return this._nextChain; }
            set {
                this._nextChain = value;
                this.OnPropertyChanged( );
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the previous chain. </summary>
        ///
        /// <value> The previous chain. </value>
        ///-------------------------------------------------------------------------------------------------

        public string PreviousChain {
            get { return this._previousChain; }
            set {
                this._previousChain = value;
                this.OnPropertyChanged( );
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets source texts. </summary>
        ///
        /// <value> The source texts. </value>
        ///-------------------------------------------------------------------------------------------------

        public IList<Tuple<string, bool, string>> SourceTexts { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool Initialize( ) {
            this.SourceTexts = new List<Tuple<string, bool, string>>( );
            this.MinChars = this.MinChars < 1 ? 1 : this.MinChars;
            this.MaxChars = this.MaxChars < this.MinChars ? this.MinChars + 1 : this.MaxChars;
            this.MarkovOrder = this.MarkovOrder == 0 ? 1 : this.MarkovOrder;

            _log.Debug( "Markov n-gram order set to " + this.MarkovOrder + "." );
            this.MarkovChain = new MarkovChain<string>( this.MarkovOrder );

            foreach( var sourceFile in this._sourceFiles ) {
                this.AddSource( sourceFile.Key, sourceFile.Value );
            }

            _lastTweetIds = new long[0];

            // Get twitter stream
            this.UpdateTwitterTimeline( );

            // Preload a chain and return if we can
            //if( this.SourceTexts.Count == 0 ) {
            //    return false;
            //}
            this.NextChain = this.GenerateNewChain( );
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Generates a new chain. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <returns>   The new chain. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string GenerateNewChain( ) {
            return this.GenerateNewChain( null );
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Generates a new chain. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <param name="startChain">   The start chain. </param>
        ///
        /// <returns>   The new chain. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string GenerateNewChain( string startChain ) {
            int len;
            int count = 0;
            string output;

            // Get initial starting chain set
            var startState = string.IsNullOrEmpty( startChain )
                                 ? Enumerable.Empty<string>( )
                                 : startChain.Split( ' ' ).ToArray( );

            do {
                var words = this.MarkovChain.Chain( startState ).ToList( );
                output = string.IsNullOrEmpty( startChain ) ? string.Empty : startChain + " ";
                for( int i = 0; i < words.Count; i++ ) {
                    output += words[ i ] + ( i == words.Count - 1 ? "" : " " );
                }

                len = output.ToCharArray( ).Length;
                count++;
            } while( len >= this.MaxChars ||
                     len <= this.MinChars );

            _log.Info( "Generated valid chain in " + count + " tries. Starting chain: " + startState );

            this.PreviousChain = this.NextChain;
            this.NextChain = output;

            return output;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determines if we can regenerate sources. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool RegenerateSources( ) {
            // Wipe current chains
            this.MarkovChain = new MarkovChain<string>( this.MarkovOrder );

            // Regen all items
            // Create a parameter for queries with specific parameters
            this.UpdateTwitterTimeline( );

            return this.SourceTexts.Count == 0;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a source to 'loadNow'. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <param name="fileName"> Filename of the file. </param>
        /// <param name="loadNow">  true to load now. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool AddSource( string fileName, bool loadNow ) {
            string text;
            try {
                // try to load text for inclusion in SourceTexts
                text = File.ReadAllText( fileName );
            } catch( Exception e ) {
                _log.Error( "Unable to load source file " + fileName + ", caught exception: ", e );
                return false;
            }

            // Get into the source texts
            this.SourceTexts.Add( new Tuple<string, bool, string>( fileName, loadNow, text ) );
            if( loadNow ) {
                this.ParseSourceText( text );
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the initial chains in this collection. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to get the initial chains in this collection.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public IEnumerable<string> GetInitialChains( ) {
            var states = this.MarkovChain.GetInitialStates( );
            var orderedStates = states.OrderByDescending( kvp => kvp.Value );
            return orderedStates.Select( state => string.Join( " ", state.Key ) ).ToList( );
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Updates the twitter timeline. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void UpdateTwitterTimeline( ) {
            var timelineParameter = Timeline.CreateHomeTimelineRequestParameter( );
            timelineParameter.ExcludeReplies = true;
            timelineParameter.TrimUser = true;
            timelineParameter.MaximumNumberOfTweetsToRetrieve = 250;
            var tweets = Timeline.GetHomeTimeline( timelineParameter );
            var enumerable = tweets as ITweet[] ?? tweets.ToArray( );
            foreach( var tweet in enumerable.Where( tweet => !this._lastTweetIds.Contains( tweet.Id ) ) ) {
                this.AddSource( tweet );
            }

            this._lastTweetIds = enumerable.Select( tweet => tweet.Id ).ToArray( );
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a source to 'loadNow'. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <param name="fileName"> Filename of the file. </param>
        ///-------------------------------------------------------------------------------------------------

        private void AddSource( ITweet fileName ) {
            var wordCount = 0;
            var lines = fileName.Text.Split( new[] { Environment.NewLine }, StringSplitOptions.None );

            // Split each sentence and add to Markov system
            foreach(
                var words in
                    lines.Select(
                        line =>
                        line.Trim( )
                            .Split( new[] { " " }, StringSplitOptions.None )
                            .Where( word => !word.StartsWith( "http" ) && !word.StartsWith( "@" ) )
                            .ToArray( ) ) ) {
                this.MarkovChain.Add( words );
                wordCount += words.Length;
            }

            _log.Info(
                "Added source text to generator composed of " + lines.Length + " lines with a total of " + wordCount +
                " word tokens." );
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Parse source text. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <param name="text"> The text. </param>
        ///-------------------------------------------------------------------------------------------------

        private void ParseSourceText( string text ) {
            var wordCount = 0;
            var lines = text.Split( new[] { Environment.NewLine }, StringSplitOptions.None );

            // Split each sentence and add to Markov system
            foreach( var words in lines.Select( line => line.Trim( ).Split( new[] { " " }, StringSplitOptions.None ) ) ) {
                this.MarkovChain.Add( words );
                wordCount += words.Length;
            }

            _log.Info(
                "Added source text to generator composed of " + lines.Length + " lines with a total of " + wordCount +
                " word tokens." );
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the property changed action. </summary>
        ///
        /// <remarks>   Forest Feather, 5/5/2015. </remarks>
        ///
        /// <param name="propertyName"> Name of the property. </param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null ) {
            var handler = this.PropertyChanged;
            if( handler == null ) {
                return;
            }
            var args = new PropertyChangedEventArgs( propertyName );
            handler( this, args );
        }

    }
}
