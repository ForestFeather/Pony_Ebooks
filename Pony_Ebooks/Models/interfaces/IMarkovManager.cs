// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IMarkovManager.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:47 AM, 24/01/2015
// //  Created Date: 5:33 AM, 24/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.Generic;

#endregion

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Interface for markov manager. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///=================================================================================================
    public interface IMarkovManager {
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
        /// <summary>   Gets or sets the markov weight. </summary>
        ///
        /// <value> The markov weight. </value>
        ///=================================================================================================
        int MarkovWeight { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the markov order. </summary>
        ///
        /// <value> The markov order. </value>
        ///=================================================================================================
        int MarkovOrder { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the next chain. </summary>
        ///
        /// <value> The next chain. </value>
        ///=================================================================================================
        string NextChain { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the previous chain. </summary>
        ///
        /// <value> The previous chain. </value>
        ///=================================================================================================
        string PreviousChain { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets source texts. </summary>
        ///
        /// <value> The source texts. </value>
        ///=================================================================================================
        IList<Tuple<string, bool, string>> SourceTexts { get; set; }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool Initialize( );

        ///=================================================================================================
        /// <summary>   Generates a new chain. </summary>
        ///
        /// <returns>   The new chain. </returns>
        ///=================================================================================================
        string GenerateNewChain( );

        ///=================================================================================================
        /// <summary>   Determines if we can regenerate sources. </summary>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool RegenerateSources( );

        ///=================================================================================================
        /// <summary>   Adds a source to 'loadNow'. </summary>
        ///
        /// <param name="fileName"> Filename of the file. </param>
        /// <param name="loadNow">  true to load now. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        bool AddSource( string fileName, bool loadNow );

        #endregion
    }
}
