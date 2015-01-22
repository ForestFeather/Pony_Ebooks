// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MainWindow.xaml.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:00 PM, 21/01/2015
// //  Created Date: 7:10 PM, 18/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Windows;

#endregion

namespace Pony_Ebooks {
    ///=================================================================================================
    /// <summary>   Interaction logic for MainWindow.xaml. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
    ///
    /// <seealso cref="T:System.Windows.Window"/>
    ///=================================================================================================
    public partial class MainWindow : Window {
        #region Constructors

        ///=================================================================================================
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///=================================================================================================
        public MainWindow( ) {
            this.InitializeComponent( );
            this.ViewModel = new MainWindowViewModel( );
            this.ViewModel.Initialize( );
            this.DataContext = this.ViewModel;
        }

        #endregion

        #region Properties

        ///=================================================================================================
        /// <summary>   Gets or sets the view model. </summary>
        ///
        /// <value> The view model. </value>
        ///=================================================================================================
        public MainWindowViewModel ViewModel { get; set; }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Generates. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///=================================================================================================
        private void Generate( object sender, RoutedEventArgs e ) {
            this.ViewModel.GenerateNewChainAndTime( );
        }

        ///=================================================================================================
        /// <summary>   Post this message. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///=================================================================================================
        private void Post( object sender, RoutedEventArgs e ) {
            this.ViewModel.PublishTweet( );
        }

        #endregion
    }
}
