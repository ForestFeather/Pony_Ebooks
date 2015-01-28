// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - TabViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 5:14 AM, 28/01/2015
// //  Created Date: 7:11 AM, 25/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.Windows;

#endregion

namespace Pony_Ebooks.Framework {
    ///=================================================================================================
    /// <summary>   Tab view model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.ViewModel"/>
    /// <seealso cref="T:Pony_Ebooks.Framework.ITabViewModel"/>
    ///=================================================================================================
    public class TabViewModel : ViewModel, ITabViewModel {

        /// <summary>   true if this object is dirty. </summary>
        private bool _isDirty;

        #region ITabViewModel Members

        ///=================================================================================================
        /// <summary>   Gets or sets a value indicating whether this object is dirty. </summary>
        ///
        /// <value> true if this object is dirty, false if not. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Framework.ITabViewModel.IsDirty"/>
        ///=================================================================================================
        public bool IsDirty {
            get { return this._isDirty; }
            set {
                this._isDirty = value;
                this.OnPropertyChanged( );
            }
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Executes the save action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/28/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        protected bool OnSave( ) {
            if( this.IsDirty ) {
                var result = MessageBox.Show(
                    "You have unsaved changes, do you wish to save them first?",
                    "Save Changes",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question );
                if( result == MessageBoxResult.Yes ) {
                    return this.SaveChanges( );
                }
            }

            return this.IgnoreChanges( );
        }

        ///=================================================================================================
        /// <summary>   Determines if we can ignore changes. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/28/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        protected virtual bool IgnoreChanges( ) {
            return true;
        }

        ///=================================================================================================
        /// <summary>   Saves the changes. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/28/2015. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///=================================================================================================
        protected virtual bool SaveChanges( ) {
            return true;
        }

        #endregion
    }
}
