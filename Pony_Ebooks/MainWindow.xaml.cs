// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MainWindow.xaml.cs 
// // 
// //  Copyright 2011-2013
// //  WR Medical Electronics Company
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:26 AM, 19/01/2015
// //  Created Date: 7:10 PM, 18/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Linq;
using System.Windows;

using Tweetinvi;

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
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Event handler. Called by ButtonBase for on click events. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/19/2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Routed event information. </param>
        ///=================================================================================================
        private void ButtonBase_OnClick( object sender, RoutedEventArgs e ) {
            var tweets = Timeline.GetHomeTimeline( );

            var output = tweets.Aggregate(
                string.Empty,
                ( current, tweet ) =>
                current + string.Format( "{0}\r\n{1}, {2}\r\n", tweet.Text, tweet.Creator, tweet.CreatedAt ) );

            this._textblock.Text = output;
        }

        #endregion
    }
}
