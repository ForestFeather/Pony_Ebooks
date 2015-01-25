// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - CurrentStatusViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:25 AM, 24/01/2015
// //  Created Date: 8:13 AM, 24/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Current status view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.ViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.ICurrentStatusViewModel"/>
    ///=================================================================================================
    public class CurrentStatusViewModel : ViewModel, ICurrentStatusViewModel {

        /// <summary>   The countdown timer. </summary>
        private string _countdownTimer;

        /// <summary>   The next chain. </summary>
        private string _nextChain;

        /// <summary>   Time of the post. </summary>
        private string _postTime;

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="manager">  The manager. </param>
        /// <param name="control">  The control. </param>
        ///=================================================================================================
        public CurrentStatusViewModel( IMarkovManager manager, ITimerControl control ) {
            this.MarkovManager = manager;
            this.TimerControl = control;

            this.MarkovManager.PropertyChanged += ( sender, args ) =>
                                                      {
                                                          if( args.PropertyName == "NextChain" ) {
                                                              this.NextChain = this.MarkovManager.NextChain;
                                                          }
                                                      };
            this.TimerControl.PropertyChanged += ( sender, args ) =>
                                                     {
                                                         if( args.PropertyName == "TimeRemaining" ) {
                                                             this.CountdownTimer =
                                                                 this.TimerControl.TimeRemaining.ToString( );
                                                         }
                                                         if( args.PropertyName == "TriggerTime" ) {
                                                             this.PostTime =
                                                                 this.TimerControl.TriggerTime.ToShortTimeString( );
                                                         }
                                                     };
        }

        #endregion

        #region ICurrentStatusViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets the timer control. </summary>
        ///
        /// <value> The timer control. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICurrentStatusViewModel.TimerControl"/>
        ///=================================================================================================
        public ITimerControl TimerControl { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the manager for markov. </summary>
        ///
        /// <value> The markov manager. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICurrentStatusViewModel.MarkovManager"/>
        ///=================================================================================================
        public IMarkovManager MarkovManager { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the next chain. </summary>
        ///
        /// <value> The next chain. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICurrentStatusViewModel.NextChain"/>
        ///=================================================================================================
        public string NextChain {
            get { return this._nextChain; }
            set {
                this._nextChain = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the time of the post. </summary>
        ///
        /// <value> The time of the post. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICurrentStatusViewModel.PostTime"/>
        ///=================================================================================================
        public string PostTime {
            get { return this._postTime; }
            set {
                this._postTime = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the countdown timer. </summary>
        ///
        /// <value> The total number of down timer. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ICurrentStatusViewModel.CountdownTimer"/>
        ///=================================================================================================
        public string CountdownTimer {
            get { return this._countdownTimer; }
            set {
                this._countdownTimer = value;
                this.OnPropertyChanged( );
            }
        }

        #endregion
    }
}
