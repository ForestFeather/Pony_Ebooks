// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MainWindowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:49 PM, 31/01/2015
// //  Created Date: 7:11 AM, 25/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;
using Pony_Ebooks.Models;

#endregion

namespace Pony_Ebooks.ViewModels {
    ///=================================================================================================
    /// <summary>   Main window view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.ViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.ViewModels.IMainWindowViewModel"/>
    /// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged"/>
    ///=================================================================================================
    public class MainWindowViewModel : ViewModel, IMainWindowViewModel {

        /// <summary>   The selected tab. </summary>
        private ITabViewModel _selectedTab;

        #region IMainWindowViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets the current status view model. </summary>
        ///
        /// <value> The current status view model. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.CurrentStatusViewModel"/>
        ///=================================================================================================
        public ICurrentStatusViewModel CurrentStatusViewModel { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the command row view model. </summary>
        ///
        /// <value> The command row view model. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.CommandRowViewModel"/>
        ///=================================================================================================
        public ICommandRowViewModel CommandRowViewModel { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the manager for tweet. </summary>
        ///
        /// <value> The tweet manager. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.TweetManager"/>
        ///=================================================================================================
        public ITweetManager TweetManager { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the tab view models. </summary>
        ///
        /// <value> The tab view models. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.TabViewModels"/>
        ///=================================================================================================
        public ObservableCollection<ITabViewModel> TabViewModels { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the selected tab. </summary>
        ///
        /// <value> The selected tab. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.SelectedTab"/>
        ///=================================================================================================
        public ITabViewModel SelectedTab {
            get { return this._selectedTab; }
            set {
                this._selectedTab = value;
                this.OnPropertyChanged( );
            }
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the manager for markov. </summary>
        ///
        /// <value> The markov manager. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.MarkovManager"/>
        ///=================================================================================================
        public IMarkovManager MarkovManager { get; private set; }

        ///=================================================================================================
        /// <summary>   Gets or sets the timer control. </summary>
        ///
        /// <value> The timer control. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.ViewModels.IMainWindowViewModel.TimerControl"/>
        ///=================================================================================================
        public ITimerControl TimerControl { get; private set; }

        ///=================================================================================================
        /// <summary>   Initializes this object. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/24/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:Pony_Ebooks.ViewModels.IMainWindowViewModel.Initialize()"/>
        ///=================================================================================================
        public bool Initialize( ) {
            var files = new Dictionary<string, bool> { { "S1.txt", true } };

            this.MarkovManager = new MarkovManager( files )
                                     {
                                         MarkovOrder = 1,
                                         MarkovWeight = 1,
                                         MinChars = 16,
                                         MaxChars = 128
                                     };
            this.TimerControl = new TimerControl( )
                                    {
                                        MinSeconds = 300,
                                        MaxSeconds = 10800
                                    };
            this.TweetManager = new TweetManager( );

            // Tabs
            IPastTweetsTabViewModel pastTweets = new PastTweetsTabViewModel( );
            IMarkovTabViewModel markovTab = new MarkovTabViewModel( this.MarkovManager );

            this.CurrentStatusViewModel = new CurrentStatusViewModel( this.MarkovManager, this.TimerControl );
            this.CommandRowViewModel = new CommandRowViewModel(
                this.TimerControl, this.MarkovManager, this.TweetManager );
            this.TabViewModels = new ObservableCollection<ITabViewModel>
                                     {
                                         pastTweets,
                                         markovTab
                                     };

            // Timer event last, after all tabs/etc have been loaded
            this.TimerControl.TimerEvent = this.CommandRowViewModel.PostAction = o =>
                                                                                     {
                                                                                         pastTweets.AddTweet(
                                                                                             this.MarkovManager
                                                                                                 .NextChain,
                                                                                             this.TimerControl
                                                                                                 .TriggerTime );
                                                                                         this.TimerControl
                                                                                             .NewTriggerTime( );
                                                                                         this.TweetManager.Post(
                                                                                             this.MarkovManager
                                                                                                 .NextChain );
                                                                                         this.MarkovManager
                                                                                             .GenerateNewChain( );
                                                                                     };

            return this.MarkovManager.Initialize( ) && markovTab.Initialize( ) && this.TimerControl.Initialize( ) &&
                   this.TimerControl.Start( );
        }

        #endregion
    }
}
