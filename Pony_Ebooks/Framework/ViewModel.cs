// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:25 PM, 23/01/2015
// //  Created Date: 8:20 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion

namespace Pony_Ebooks.Framework {
    ///=================================================================================================
    /// <summary>   View model. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
    ///
    /// <seealso cref="T:Pony_Ebooks.Framework.IViewModel"/>
    ///=================================================================================================
    public class ViewModel : IViewModel {

        /// <summary>   The title. </summary>
        private string _title;

        #region IViewModel Members

        /// <summary>   Event queue for all listeners interested in PropertyChanged events. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///=================================================================================================
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///=================================================================================================
        public void Dispose( ) {
            this.OnDispose( true );
        }

        ///=================================================================================================
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        ///
        /// <seealso cref="P:Pony_Ebooks.Framework.IViewModel.Title"/>
        ///=================================================================================================
        public string Title {
            get { return this._title; }
            set {
                this._title = value;
                this.OnPropertyChanged( );
            }
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Executes the dispose action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
        ///
        /// <param name="onDispose">    true to on dispose. </param>
        ///=================================================================================================
        protected virtual void OnDispose( bool onDispose ) {
            if( onDispose ) {
                // Managed resources
            }

            // Unmanaged resources
        }

        ///=================================================================================================
        /// <summary>   Executes the property changed action. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/23/2015. </remarks>
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
