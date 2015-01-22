// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ChainState.cs 
// // 
// //  Copyright 2011-2013
// //  WR Medical Electronics Company
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:45 AM, 21/01/2015
// //  Created Date: 6:45 AM, 21/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Pony_Ebooks.Markov {
    ///=================================================================================================
    /// <summary>   Represents a state in a Markov chain. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///
    /// <typeparam name="T">    The type of the constituent parts of each state in the Markov chain. </typeparam>
    ///
    /// <seealso cref="T:System.IEquatable{Markov.ChainState{T}}"/>
    ///=================================================================================================
    public class ChainState<T> : IEquatable<ChainState<T>> {

        /// <summary>   The items. </summary>
        private readonly T[] items;

        #region Constructors

        ///=================================================================================================
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChainState&lt;T&gt;"/> class with the
        ///     specified items.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="items">    An <see cref="IEnumerable{T}"/> of items to be copied as a
        ///                         single state. </param>
        ///=================================================================================================
        public ChainState( IEnumerable<T> items ) {
            if( items == null ) {
                throw new ArgumentNullException( "items" );
            }

            this.items = items.ToArray( );
        }

        ///=================================================================================================
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChainState&lt;T&gt;"/> class with the
        ///     specified items.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="items">    A <see cref="T:T[]"/> of items to be copied as a single state. </param>
        ///=================================================================================================
        public ChainState( T[] items ) {
            if( items == null ) {
                throw new ArgumentNullException( "items" );
            }

            this.items = new T[items.Length];
            Array.Copy( items, this.items, items.Length );
        }

        #endregion

        #region IEquatable<ChainState<T>> Members

        ///=================================================================================================
        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="other">    An object to compare with this object. </param>
        ///
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other"/> parameter; otherwise,
        ///     false.
        /// </returns>
        ///=================================================================================================
        public bool Equals( ChainState<T> other ) {
            if( (object) other == null ) {
                return false;
            }

            if( this.items.Length != other.items.Length ) {
                return false;
            }

            for( int i = 0; i < this.items.Length; i++ ) {
                if( !this.items[ i ].Equals( other.items[ i ] ) ) {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>
        ///     Determines whether two specified instances of <see cref="ChainState&lt;T&gt;"/> are equal.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="a">    A <see cref="ChainState&lt;T&gt;"/>. </param>
        /// <param name="b">    A <see cref="ChainState&lt;T&gt;"/>. </param>
        ///
        /// <returns>
        ///     true if <paramref name="a"/> and <paramref name="b"/> represent the same state; otherwise,
        ///     false.
        /// </returns>
        ///=================================================================================================
        public static bool operator ==( ChainState<T> a, ChainState<T> b ) {
            if( ReferenceEquals( a, b ) ) {
                return true;
            }

            if( ( (object) a == null ) ||
                ( (object) b == null ) ) {
                return false;
            }

            return a.Equals( b );
        }

        ///=================================================================================================
        /// <summary>
        ///     Determines whether two specified instances of <see cref="ChainState&lt;T&gt;"/> are not
        ///     equal.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="a">    A <see cref="ChainState&lt;T&gt;"/>. </param>
        /// <param name="b">    A <see cref="ChainState&lt;T&gt;"/>. </param>
        ///
        /// <returns>
        ///     true if <paramref name="a"/> and <paramref name="b"/> do not represent the same state;
        ///     otherwise, false.
        /// </returns>
        ///=================================================================================================
        public static bool operator !=( ChainState<T> a, ChainState<T> b ) {
            return !( a == b );
        }

        ///=================================================================================================
        /// <summary>   Returns the hash code for this instance. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   A 32-bit signed integer hash code. </returns>
        ///
        /// <seealso cref="M:System.Object.GetHashCode()"/>
        ///=================================================================================================
        public override int GetHashCode( ) {
            var code = this.items.Length.GetHashCode( );

            for( int i = 0; i < this.items.Length; i++ ) {
                code ^= this.items[ i ].GetHashCode( );
            }

            return code;
        }

        ///=================================================================================================
        /// <summary>
        ///     Determines whether the specified <see cref="object"/> is equal to the current
        ///     <see cref="object"/>.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="obj">  The <see cref="object"/> to compare with the current
        ///                     <see cref="object"/>. </param>
        ///
        /// <returns>
        ///     true if the specified <see cref="object"/> is equal to the current <see cref="object"/>;
        ///     otherwise, false.
        /// </returns>
        ///
        /// <seealso cref="M:System.Object.Equals(object)"/>
        ///=================================================================================================
        public override bool Equals( object obj ) {
            if( obj == null ) {
                return false;
            }

            return this.Equals( obj as ChainState<T> );
        }

        #endregion
    }
}
