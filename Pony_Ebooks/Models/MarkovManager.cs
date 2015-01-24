﻿// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MarkovManager.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 7:21 AM, 24/01/2015
// //  Created Date: 9:21 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Pony_Ebooks.Markov;

using log4net;

#endregion

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Manager for markovs. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Models.IMarkovManager"/>
    ///=================================================================================================
    public class MarkovManager : IMarkovManager {
        #region Fields and Constants

        /// <summary>   The log. </summary>
        private static readonly ILog _log = LogManager.GetLogger( typeof( MarkovManager ) );

        #endregion

        /// <summary>   Source files. </summary>
        private readonly IDictionary<string, bool> _sourceFiles;

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="sourceFiles">  Source files. </param>
        ///=================================================================================================
        public MarkovManager( IDictionary<string, bool> sourceFiles ) {
            this._sourceFiles = sourceFiles;
            _log.Info( "Instantiated Markov Manager with " + this._sourceFiles.Count + " sources." );
        }

        #endregion

        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the markov chain. </summary>
        ///
        /// <value> The markov chain. </value>
        ///=================================================================================================
        protected MarkovChain<string> MarkovChain { get; set; }

        #endregion

        #region IMarkovManager Members

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum characters. </summary>
        ///
        /// <value> The minimum characters. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.MinChars"/>
        ///=================================================================================================
        public int MinChars { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum characters. </summary>
        ///
        /// <value> The maximum characters. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.MaxChars"/>
        ///=================================================================================================
        public int MaxChars { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the markov weight. </summary>
        ///
        /// <value> The markov weight. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.MarkovWeight"/>
        ///=================================================================================================
        public int MarkovWeight { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the markov order. </summary>
        ///
        /// <value> The markov order. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.MarkovOrder"/>
        ///=================================================================================================
        public int MarkovOrder { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the next chain. </summary>
        ///
        /// <value> The next chain. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.NextChain"/>
        ///=================================================================================================
        public string NextChain { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the previous chain. </summary>
        ///
        /// <value> The previous chain. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.PreviousChain"/>
        ///=================================================================================================
        public string PreviousChain { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets source texts. </summary>
        ///
        /// <value> The source texts. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.IMarkovManager.SourceTexts"/>
        ///=================================================================================================
        public IList<Tuple<string, bool, string>> SourceTexts { get; set; }

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.IMarkovManager.Initialize()"/>
        ///=================================================================================================
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

            // Preload a chain and return if we can
            if( this.SourceTexts.Count == 0 ) {
                return false;
            }
            this.NextChain = this.GenerateNewChain( );
            return true;
        }

        ///=================================================================================================
        /// <summary>   Generates a new chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <returns>   The new chain. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.IMarkovManager.GenerateNewChain()"/>
        ///=================================================================================================
        public string GenerateNewChain( ) {
            int len;
            int count = 0;
            string output;

            do {
                var words = this.MarkovChain.Chain( ).ToList( );
                output = string.Empty;
                for( int i = 0; i < words.Count; i++ ) {
                    output += words[ i ] + ( i == words.Count - 1 ? "" : " " );
                }

                len = output.ToCharArray( ).Length;
                count++;
            } while( len >= this.MaxChars ||
                     len <= this.MinChars );

            _log.Info( "Generated valid chain in " + count + " tries." );

            this.PreviousChain = this.NextChain;
            this.NextChain = output;

            return output;
        }

        ///=================================================================================================
        /// <summary>   Determines if we can regenerate sources. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.IMarkovManager.RegenerateSources()"/>
        ///=================================================================================================
        public bool RegenerateSources( ) {
            // Wipe current chains
            this.MarkovChain = new MarkovChain<string>( this.MarkovOrder );

            // Regen all items
            foreach( var sourceFile in this.SourceTexts ) {
                this.AddSource( sourceFile.Item1, sourceFile.Item2 );
            }

            return this.SourceTexts.Count == 0;
        }

        ///=================================================================================================
        /// <summary>   Adds a source to 'loadNow'. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="fileName"> Filename of the file. </param>
        /// <param name="loadNow">  true to load now. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.IMarkovManager.AddSource(string,bool)"/>
        ///=================================================================================================
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

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Parse source text. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="text"> The text. </param>
        ///=================================================================================================
        private void ParseSourceText( string text ) {
            var wordCount = 0;
            var lines = text.Split( new[] { Environment.NewLine }, StringSplitOptions.None );

            // Split each sentence and add to Markov system
            foreach( var words in lines.Select( line => line.Split( new[] { " " }, StringSplitOptions.None ) ) ) {
                this.MarkovChain.Add( words );
                wordCount += words.Length;
            }

            _log.Info(
                "Added source text to generator composed of " + lines.Length + " lines with a total of " + wordCount +
                " word tokens." );
        }

        #endregion
    }
}
