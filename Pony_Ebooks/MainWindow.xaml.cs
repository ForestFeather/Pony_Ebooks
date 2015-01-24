// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MainWindow.xaml.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:40 AM, 24/01/2015
// //  Created Date: 7:10 PM, 18/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Windows;

using Pony_Ebooks.ViewModels;

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
    }
}
