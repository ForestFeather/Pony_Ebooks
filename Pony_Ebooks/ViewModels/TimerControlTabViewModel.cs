// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - TimerControlTabViewModel.cs 
// // 
// //  Last Changed By: ForestFeather - 
// //  Last Changed Date: 4:36 AM, 22/02/2015
// //  Created Date: 5:54 PM, 11/02/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.ComponentModel;

using log4net;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;
using Pony_Ebooks.Properties;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Timer control tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.TabViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.ITimerControlTabViewModel"/>
    ///=================================================================================================
    public class TimerControlTabViewModel : TabViewModel, ITimerControlTabViewModel {

        /// <summary>   The log. </summary>
        private static readonly ILog _log = LogManager.GetLogger( typeof( TimerControlTabViewModel ) );

        /// <summary>   The maximum in hours. </summary>
        private int _maxHours;

        /// <summary>   The maximum in minutes. </summary>
        private int _maxMinutes;

        /// <summary>   The maximum in seconds. </summary>
        private int _maxSeconds;

        /// <summary>   The minimum in hours. </summary>
        private int _minHours;

        /// <summary>   The minimum in minutes. </summary>
        private int _minMinutes;

        /// <summary>   The minimum in seconds. </summary>
        private int _minSeconds;

        /// <summary>   The timer control. </summary>
        protected ITimerControl _timerControl;

        /// <summary>   The total maximum timespan. </summary>
        private TimeSpan _totalMaxTimespan;

        /// <summary>   The total minimum timespan. </summary>
        private TimeSpan _totalMinTimespan;

        #region Constructors

        ///=================================================================================================
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <param name="timerControl"> The timer control. </param>
        ///=================================================================================================
        public TimerControlTabViewModel( ITimerControl timerControl ) {
            this._timerControl = timerControl;
            this.Title = "Timer Control";
        }

        #endregion

        #region Overrides of ViewModel

        ///=================================================================================================
        /// <summary>   Executes the dispose action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <param name="onDispose">    true to on dispose. </param>
        ///=================================================================================================
        protected override void OnDispose( bool onDispose ) {
            base.OnDispose( onDispose );

            if( onDispose ) {
                // Managed

            }

            // Unmanaged
            Settings.Default.TimerMinSeconds = this._timerControl.MinSeconds;
            Settings.Default.TimerMaxSeconds = this._timerControl.MaxSeconds;
            Settings.Default.Save();
            _log.Info("Saved Timer settings.");
        }

        #endregion

        #region ITimerControlTabViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum seconds. </summary>
        ///
        /// <value> The minimum seconds. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.MinSeconds"/>
        ///=================================================================================================
        public int MinSeconds {
            get { return this._minSeconds; }
            set {
                this._minSeconds = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum seconds. </summary>
        ///
        /// <value> The maximum seconds. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.MaxSeconds"/>
        ///=================================================================================================
        public int MaxSeconds {
            get { return this._maxSeconds; }
            set {
                this._maxSeconds = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum minutes. </summary>
        ///
        /// <value> The minimum minutes. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.MinMinutes"/>
        ///=================================================================================================
        public int MinMinutes {
            get { return this._minMinutes; }
            set {
                this._minMinutes = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum minutes. </summary>
        ///
        /// <value> The maximum minutes. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.MaxMinutes"/>
        ///=================================================================================================
        public int MaxMinutes {
            get { return this._maxMinutes; }
            set {
                this._maxMinutes = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the minimum hours. </summary>
        ///
        /// <value> The minimum hours. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.MinHours"/>
        ///=================================================================================================
        public int MinHours {
            get { return this._minHours; }
            set {
                this._minHours = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the maximum hours. </summary>
        ///
        /// <value> The maximum hours. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.MaxHours"/>
        ///=================================================================================================
        public int MaxHours {
            get { return this._maxHours; }
            set {
                this._maxHours = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the total minimum timespan. </summary>
        ///
        /// <value> The total number of minimum timespan. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.TotalMinTimespan"/>
        ///=================================================================================================
        public TimeSpan TotalMinTimespan {
            get { return this._totalMinTimespan; }
            set {
                this._totalMinTimespan = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the total maximum timespan. </summary>
        ///
        /// <value> The total number of maximum timespan. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.TotalMaxTimespan"/>
        ///=================================================================================================
        public TimeSpan TotalMaxTimespan {
            get { return this._totalMaxTimespan; }
            set {
                this._totalMaxTimespan = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the set new timespans command. </summary>
        ///
        /// <value> The set new timespans command. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.SetNewTimespansCommand"/>
        ///=================================================================================================
        public IRelayCommand SetNewTimespansCommand { get; private set; }

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.ViewModels.ITimerControlTabViewModel.Initialize()"/>
        ///=================================================================================================
        public bool Initialize( ) {
            this.SetNewTimespansCommand = new RelayCommand( this.OnNewTimespans, this.CanNewTimespans );

            this.PropertyChanged += this.OnTimerSettingsChanged;

            // Load Timer Settings
            this._timerControl.MinSeconds = Settings.Default.TimerMinSeconds;
            this._timerControl.MaxSeconds = Settings.Default.TimerMaxSeconds;

            this._totalMinTimespan = new TimeSpan( 0, 0, this._timerControl.MinSeconds );
            this._minSeconds = this.TotalMinTimespan.Seconds;
            this._minMinutes = this.TotalMinTimespan.Minutes;
            this._minHours = this.TotalMinTimespan.Hours;

            this._totalMaxTimespan = new TimeSpan( 0, 0, this._timerControl.MaxSeconds );
            this._maxSeconds = this.TotalMaxTimespan.Seconds;
            this._maxMinutes = this.TotalMaxTimespan.Minutes;
            this._maxHours = this.TotalMaxTimespan.Hours;

            return true;
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Raises the property changed event. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information to send to registered event handlers. </param>
        ///=================================================================================================
        private void OnTimerSettingsChanged( object sender, PropertyChangedEventArgs e ) {
            switch( e.PropertyName ) {
                case "MinSeconds":
                case "MinMinutes":
                case "MinHours":
                case "MaxSeconds":
                case "MaxMinutes":
                case "MaxHours":
                    this.UpdateTimespanLimits( );
                    break;
            }
        }

        ///=================================================================================================
        /// <summary>   Updates the timespan limits. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///=================================================================================================
        private void UpdateTimespanLimits( ) {
            this.IsDirty = true;
            this.TotalMinTimespan = new TimeSpan( this.MinHours, this.MinMinutes, this.MinSeconds );
            this.TotalMaxTimespan = new TimeSpan( this.MaxHours, this.MaxMinutes, this.MaxSeconds );
        }

        ///=================================================================================================
        /// <summary>   Determine if we can new timespans. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///
        /// <returns>   true if we can new timespans, false if not. </returns>
        ///=================================================================================================
        private bool CanNewTimespans( object obj ) {
            return this.IsDirty;
        }

        ///=================================================================================================
        /// <summary>   Executes the new timespans action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 2/8/2015. </remarks>
        ///
        /// <param name="obj">  The object. </param>
        ///=================================================================================================
        private void OnNewTimespans( object obj ) {
            this._timerControl.MinSeconds = (int) this.TotalMinTimespan.TotalSeconds;
            this._timerControl.MaxSeconds = (int) this.TotalMaxTimespan.TotalSeconds;

            this.IsDirty = false;
        }

        #endregion
    }
}
