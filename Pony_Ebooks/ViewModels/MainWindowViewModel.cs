// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MainWindowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:00 PM, 21/01/2015
// //  Created Date: 5:29 AM, 21/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

using Pony_Ebooks.Markov;

using Tweetinvi;

#endregion

namespace Pony_Ebooks {
    ///=================================================================================================
    /// <summary>   Main window view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///
    /// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged"/>
    ///=================================================================================================
    public class MainWindowViewModel : INotifyPropertyChanged {

        /// <summary>   The next chain. </summary>
        private string _nextChain;

        /// <summary>   Time of the next tweet. </summary>
        private string _nextTweetTime;

        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the next chain. </summary>
        ///
        /// <value> The next chain. </value>
        ///=================================================================================================
        public string NextChain {
            get { return this._nextChain; }
            set {
                this._nextChain = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the markov generator. </summary>
        ///
        /// <value> The markov generator. </value>
        ///=================================================================================================
        public MarkovChain<string> MarkovGenerator { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets source text. </summary>
        ///
        /// <value> The source text. </value>
        ///=================================================================================================
        public string SourceText { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the filename of the source text file. </summary>
        ///
        /// <value> The filename of the source text file. </value>
        ///=================================================================================================
        public string SourceTextFileName { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the previous tweets. </summary>
        ///
        /// <value> The previous tweets. </value>
        ///=================================================================================================
        public ObservableCollection<StringObject> PreviousTweets { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the post timer. </summary>
        ///
        /// <value> The post timer. </value>
        ///=================================================================================================
        public Timer PostTimer { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the random generator. </summary>
        ///
        /// <value> The random generator. </value>
        ///=================================================================================================
        public Random RandomGenerator { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the time of the next tweet. </summary>
        ///
        /// <value> The time of the next tweet. </value>
        ///=================================================================================================
        public string NextTweetTime {
            get { return this._nextTweetTime; }
            set {
                this._nextTweetTime = value;
                this.OnPropertyChanged( );
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>   Occurs when a property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        public bool Initialize( ) {
            return this.InitVars( ) && this.InitMarkov( ) && this.InitTimer( );
        }

        ///=================================================================================================
        /// <summary>   Initialises the variables. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        private bool InitVars( ) {
            this.RandomGenerator = new Random( );
            this.SourceTextFileName = "S1.txt";
            this.PreviousTweets = new ObservableCollection<StringObject>( );

            return true;
        }

        ///=================================================================================================
        /// <summary>   Initialises the timer. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        private bool InitTimer( ) {
            this.PostTimer = new Timer( this.TimerCallbackMethod, null, new TimeSpan( 0, 5, 0 ), new TimeSpan( ) );

            return true;
        }

        ///=================================================================================================
        /// <summary>   Timer callback method. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="state">    The state. </param>
        ///=================================================================================================
        private void TimerCallbackMethod( object state ) {
            this.PublishTweet( );

            this.GenerateNewChainAndTime( );
        }

        ///=================================================================================================
        /// <summary>   Publish tweet. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///=================================================================================================
        public void PublishTweet( ) {
            // Post Markov's NextChain
            Tweet.PublishTweet( this.NextChain );
            var s = string.Format(
                "{0} {1}", this.NextChain, DateTime.Now.ToString( "MM'/'dd'/'yyyy HH':'mm':'ss.fff" ) );
            Console.WriteLine( s );
            this.PreviousTweets.Insert( 0, new StringObject { Value = s } );
        }

        ///=================================================================================================
        /// <summary>   Generates a new chain and time. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///=================================================================================================
        public void GenerateNewChainAndTime( ) {
            // Generate Markov to fit twitter length
            var next = this.GenerateTwitterMarkov( );

            // Set Markov
            this.NextChain = next;
            Console.WriteLine( next );

            // Update the timer
            var hours = this.RandomGenerator.Next( 1, 2 );
            var minutes = this.RandomGenerator.Next( 0, 59 );
            var seconds = this.RandomGenerator.Next( 0, 59 );
            var timeSpan = new TimeSpan( hours, minutes, seconds );
            this.NextTweetTime = ( DateTime.Now + timeSpan ).ToString( "MM'/'dd'/'yyyy HH':'mm':'ss.fff" );
            this.PostTimer.Change( timeSpan, new TimeSpan( ) );
        }

        ///=================================================================================================
        /// <summary>   Initialises the markov. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        private bool InitMarkov( ) {
            this.SourceText = File.ReadAllText( this.SourceTextFileName );
            this.MarkovGenerator = new MarkovChain<string>( 1 );

            if( string.IsNullOrEmpty( this.SourceText ) ) {
                Console.WriteLine( "No text in generator file to load!" );
                return false;
            }

            string[] lines = this.SourceText.Split( new[] { Environment.NewLine }, StringSplitOptions.None );

            foreach( var line in lines ) {
                var words = line.Split( new[] { " " }, StringSplitOptions.None );
                this.MarkovGenerator.Add( words );
            }

            this.NextChain = this.GenerateTwitterMarkov( );

            return true;
        }

        ///=================================================================================================
        /// <summary>   Generates a twitter markov. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   The twitter markov. </returns>
        ///=================================================================================================
        private string GenerateTwitterMarkov( ) {
            int len = 0;
            string output;
            do {
                var words = this.MarkovGenerator.Chain( ).ToList( );
                output = string.Empty;
                for( int i = 0; i < words.Count; i++ ) {
                    output += words[ i ] + ( i == words.Count - 1 ? "" : " " );
                }

                len = output.ToCharArray( ).Length;
            } while( len >= 120 ||
                     len <= 12 );

            return output;
        }

        ///=================================================================================================
        /// <summary>   Executes the property changed action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="propertyName"> (Optional) name of the property. </param>
        ///=================================================================================================
        protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null ) {
            var handler = this.PropertyChanged;
            if( handler != null ) {
                handler( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        #endregion
    }

    ///=================================================================================================
    /// <summary>   String object. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///=================================================================================================
    public class StringObject {
        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the value. </summary>
        ///
        /// <value> The value. </value>
        ///=================================================================================================
        public string Value { get; set; }

        #endregion
    }
}
