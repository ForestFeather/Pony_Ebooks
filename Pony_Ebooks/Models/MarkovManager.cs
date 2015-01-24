// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MarkovManager.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:45 PM, 23/01/2015
// //  Created Date: 9:21 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.Generic;

using Pony_Ebooks.Markov;

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
            return string.Empty;
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
            return true;
        }

        #endregion
    }
}
