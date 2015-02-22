// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MainWindow.xaml.cs 
// // 
// //  Last Changed By: ForestFeather - 
// //  Last Changed Date: 4:46 AM, 22/02/2015
// //  Created Date: 5:54 PM, 11/02/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Windows;

using Pony_Ebooks.ViewModels;

#endregion

namespace Pony_Ebooks {
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interaction logic for MainWindow.xaml. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
    ///
    /// <seealso cref="T:System.Windows.Window"/>
    ///-------------------------------------------------------------------------------------------------
    public partial class MainWindow : Window {
        #region Constructors

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///-------------------------------------------------------------------------------------------------
        public MainWindow( ) {
            this.InitializeComponent( );
            this.ViewModel = new MainWindowViewModel( );
            this.ViewModel.Initialize( );
            this.DataContext = this.ViewModel;
        }

        #endregion

        #region Properties

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the view model. </summary>
        ///
        /// <value> The view model. </value>
        ///-------------------------------------------------------------------------------------------------
        public MainWindowViewModel ViewModel { get; set; }

        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by MainWindow for on closed events. </summary>
        ///
        /// <remarks>   Forest Feather, 2/22/2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ///-------------------------------------------------------------------------------------------------
        private void MainWindow_OnClosed( object sender, EventArgs e ) {
            var vm = this.DataContext as MainWindowViewModel;

            if( vm != null ) {
                vm.Dispose( );
            }
        }

    }
}
