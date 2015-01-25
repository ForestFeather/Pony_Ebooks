// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - TimerControl.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 7:51 AM, 24/01/2015
// //  Created Date: 7:22 AM, 24/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

using log4net;

#endregion

namespace Pony_Ebooks.Models {
    ///=================================================================================================
    /// <summary>   Timer control. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Models.ITimerControl"/>
    ///=================================================================================================
    public class TimerControl : ITimerControl {
        #region Fields and Constants

        /// <summary>   The log. </summary>
        private static readonly ILog _log = LogManager.GetLogger( typeof( TimerControl ) );

        #endregion

        /// <summary>   The random. </summary>
        private Random _rand;

        /// <summary>   The time remaining. </summary>
        private TimeSpan _timeRemaining;

        /// <summary>   The timer. </summary>
        private DispatcherTimer _timer;

        /// <summary>   Time of the trigger. </summary>
        private DateTime _triggerTime;

        #region ITimerControl Members

        ///=================================================================================================
        /// <summary>   Gets or sets the time remaining. </summary>
        ///
        /// <value> The time remaining. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.ITimerControl.TimeRemaining"/>
        ///=================================================================================================
        public TimeSpan TimeRemaining {
            get { return this._timeRemaining; }
            private set {
                this._timeRemaining = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the time of the trigger. </summary>
        ///
        /// <value> The time of the trigger. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.ITimerControl.TriggerTime"/>
        ///=================================================================================================
        public DateTime TriggerTime {
            get { return this._triggerTime; }
            private set {
                this._triggerTime = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum seconds. </summary>
        ///
        /// <value> The minimum seconds. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.ITimerControl.MinSeconds"/>
        ///=================================================================================================
        public int MinSeconds { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum seconds. </summary>
        ///
        /// <value> The maximum seconds. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.ITimerControl.MaxSeconds"/>
        ///=================================================================================================
        public int MaxSeconds { get; set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the timer event. </summary>
        ///
        /// <value> The timer event. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Models.ITimerControl.TimerEvent"/>
        ///=================================================================================================
        public Action<object> TimerEvent { get; set; }

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.ITimerControl.Initialize()"/>
        ///=================================================================================================
        public bool Initialize( ) {
            this._rand = new Random();
            this._timer = new DispatcherTimer { Interval = new TimeSpan( 0, 0, 1 ), IsEnabled = false };
            this._timer.Tick += this.TimerOnTick;
            this.NewTriggerTime( );

            return true;
        }

        ///=================================================================================================
        /// <summary>   Starts this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.ITimerControl.Start()"/>
        ///=================================================================================================
        public bool Start( ) {
            this._timer.IsEnabled = true;
            this._timer.Start( );

            return true;
        }

        ///=================================================================================================
        /// <summary>   Stops this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.ITimerControl.Stop()"/>
        ///=================================================================================================
        public bool Stop( ) {
            this._timer.Stop( );
            this._timer.IsEnabled = false;

            return true;
        }

        ///=================================================================================================
        /// <summary>   Creates a new trigger time. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.ITimerControl.NewTriggerTime()"/>
        ///=================================================================================================
        public void NewTriggerTime( ) {
            this.NewTriggerTime( this.MinSeconds, this.MaxSeconds );
        }

        ///=================================================================================================
        /// <summary>   Creates a new trigger time. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="minSeconds">   The minimum seconds. </param>
        /// <param name="maxSeconds">   The maximum seconds. </param>
        ///
        /// <seealso cref="M:Pony_Ebooks.Models.ITimerControl.NewTriggerTime(int,int)"/>
        ///=================================================================================================
        public void NewTriggerTime( int minSeconds, int maxSeconds ) {
            var seconds = this._rand.Next( minSeconds, maxSeconds );
            var timespan = new TimeSpan( 0, 0, seconds );
            this.TimeRemaining = timespan;
            this.TriggerTime = DateTime.Now + timespan;
        }

        /// <summary>   Event queue for all listeners interested in PropertyChanged events. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Timer on tick. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="sender">       Source of the event. </param>
        /// <param name="eventArgs">    Event information. </param>
        ///=================================================================================================
        private void TimerOnTick( object sender, EventArgs eventArgs ) {
            if( this.TimeRemaining != TimeSpan.Zero ) {
                this.TimeRemaining -= new TimeSpan( 0, 0, 1 );
                return;
            }
            if( this.TimerEvent != null ) {
                _log.Info( "Executing the Timer Event and generating new trigger time." );
                this.TimerEvent( eventArgs );
            }

            this.NewTriggerTime( );
        }

        ///=================================================================================================
        /// <summary>   Executes the property changed action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <param name="propertyName"> (Optional) name of the property. </param>
        ///=================================================================================================
        protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null ) {
            var handler = this.PropertyChanged;
            if( handler == null ) {
                return;
            }
            var args = new PropertyChangedEventArgs( propertyName );
            handler( this, args );
        }

        #endregion
    }
}
