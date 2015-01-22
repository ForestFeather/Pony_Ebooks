// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - MarkovChain.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 6:00 PM, 21/01/2015
// //  Created Date: 6:43 AM, 21/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

#endregion

namespace Pony_Ebooks.Markov {
    ///=================================================================================================
    /// <summary>   Builds and walks interconnected states based on a weighted probability. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
    ///
    /// <typeparam name="T">    The type of the constituent parts of each state in the Markov chain. </typeparam>
    ///=================================================================================================
    public class MarkovChain<T>
        where T : IEquatable<T> {

        /// <summary>   The items. </summary>
        private readonly Dictionary<ChainState<T>, Dictionary<T, int>> items =
            new Dictionary<ChainState<T>, Dictionary<T, int>>( );

        /// <summary>   The order. </summary>
        private readonly int order;

        /// <summary>   The terminals. </summary>
        private readonly Dictionary<ChainState<T>, int> terminals = new Dictionary<ChainState<T>, int>( );

        #region Constructors

        ///=================================================================================================
        /// <summary>   Initializes a new instance of the MarkovChain class. </summary>
        ///
        /// <remarks>
        ///     <para>The <paramref name="order"/> of a generator indicates the depth of its internal
        ///     state.  A generator with an order of 1 will choose items based on the previous item, a
        ///     generator with an order of 2 will choose items based on the previous 2 items, and so
        ///     on.</para>
        ///     <para>Zero is not classically a valid order value, but it is allowed here.  Choosing a
        ///     zero value has the effect that every state is equivalent to the starting state, and so
        ///     items will be chosen based on their total frequency.</para>
        /// </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
        ///
        /// <param name="order">    Indicates the desired order of the
        ///                         <see cref="Markov.MarkovChain&lt;T&gt;"/>. </param>
        ///=================================================================================================
        public MarkovChain( int order ) {
            if( order < 0 ) {
                throw new ArgumentOutOfRangeException( "order" );
            }

            this.order = order;
        }

        #endregion

        #region Members

        ///=================================================================================================
        /// <summary>   Adds the items to the generator with a weight of one. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="items">    The items to add to the generator. </param>
        ///=================================================================================================
        public void Add( IEnumerable<T> items ) {
            this.Add( items, 1 );
        }

        ///=================================================================================================
        /// <summary>   Adds the items to the generator with the weight specified. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="items">    The items to add to the generator. </param>
        /// <param name="weight">   The weight at which to add the items. </param>
        ///=================================================================================================
        public void Add( IEnumerable<T> items, int weight ) {
            Queue<T> previous = new Queue<T>( );
            foreach( var item in items ) {
                var key = new ChainState<T>( previous );

                this.Add( key, item, weight );

                previous.Enqueue( item );
                if( previous.Count > this.order ) {
                    previous.Dequeue( );
                }
            }

            var terminalKey = new ChainState<T>( previous );
            this.terminals[ terminalKey ] = this.terminals.ContainsKey( terminalKey )
                                                ? weight + this.terminals[ terminalKey ]
                                                : weight;
        }

        ///=================================================================================================
        /// <summary>   Adds the item to the generator, with the specified items preceding it. </summary>
        ///
        /// <remarks>
        ///     See <see cref="Markov.MarkovChain&lt;T&gt;.Add(IEnumerable&lt;T&gt;, T, int)"/> for
        ///     remarks.
        /// </remarks>
        ///
        /// <param name="previous"> The items preceding the item. </param>
        /// <param name="item">     The item to add. </param>
        ///=================================================================================================
        public void Add( IEnumerable<T> previous, T item ) {
            var state = new Queue<T>( previous );
            while( state.Count > this.order ) {
                state.Dequeue( );
            }

            this.Add( new ChainState<T>( state ), item, 1 );
        }

        ///=================================================================================================
        /// <summary>   Adds the item to the generator, with the specified state preceding it. </summary>
        ///
        /// <remarks>
        ///     See <see cref="Markov.MarkovChain&lt;T&gt;.Add(ChainState&lt;T&gt;, T, int)"/> for
        ///     remarks.
        /// </remarks>
        ///
        /// <param name="state">    The state preceding the item. </param>
        /// <param name="next">     The item to add. </param>
        ///=================================================================================================
        public void Add( ChainState<T> state, T next ) {
            this.Add( state, next, 1 );
        }

        ///=================================================================================================
        /// <summary>
        ///     Adds the item to the generator, with the specified items preceding it and the specified
        ///     weight.
        /// </summary>
        ///
        /// <remarks>
        ///     This method does not add all of the preceding states to the generator. Notably, the empty
        ///     state is not added, unless the <paramref name="previous"/> parameter is empty.
        /// </remarks>
        ///
        /// <param name="previous"> The items preceding the item. </param>
        /// <param name="item">     The item to add. </param>
        /// <param name="weight">   The weight of the item to add. </param>
        ///=================================================================================================
        public void Add( IEnumerable<T> previous, T item, int weight ) {
            var state = new Queue<T>( previous );
            while( state.Count > this.order ) {
                state.Dequeue( );
            }

            this.Add( new ChainState<T>( state ), item, weight );
        }

        ///=================================================================================================
        /// <summary>
        ///     Adds the item to the generator, with the specified state preceding it and the specified
        ///     weight.
        /// </summary>
        ///
        /// <remarks>
        ///     This adds the state as-is.  The state may not be reachable if, for example, the number of
        ///     items in the state is greater than the order of the generator, or if the combination of
        ///     items is not available in the other states of the generator.
        /// </remarks>
        ///
        /// <param name="state">    The state preceding the item. </param>
        /// <param name="next">     The item to add. </param>
        /// <param name="weight">   The weight of the item to add. </param>
        ///=================================================================================================
        public void Add( ChainState<T> state, T next, int weight ) {
            Dictionary<T, int> weights;
            if( !this.items.TryGetValue( state, out weights ) ) {
                weights = new Dictionary<T, int>( );
                this.items.Add( state, weights );
            }

            weights[ next ] = weights.ContainsKey( next )
                                  ? weight + weights[ next ]
                                  : weight;
        }

        ///=================================================================================================
        /// <summary>   Gets the items from the generator that follow from an empty state. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <returns>   A dictionary of the items and their weight. </returns>
        ///=================================================================================================
        public Dictionary<T, int> GetInitialStates( ) {
            var startState = new ChainState<T>( Enumerable.Empty<T>( ) );

            Dictionary<T, int> weights;
            if( this.items.TryGetValue( startState, out weights ) ) {
                return new Dictionary<T, int>( weights );
            }

            return null;
        }

        ///=================================================================================================
        /// <summary>
        ///     Gets the items from the generator that follow from the specified items preceding it.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="previous"> The items preceding the items of interest. </param>
        ///
        /// <returns>   A dictionary of the items and their weight. </returns>
        ///=================================================================================================
        public Dictionary<T, int> GetNextStates( IEnumerable<T> previous ) {
            var state = new Queue<T>( previous );
            while( state.Count > this.order ) {
                state.Dequeue( );
            }

            return this.GetNextStates( new ChainState<T>( state ) );
        }

        ///=================================================================================================
        /// <summary>
        ///     Gets the items from the generator that follow from the specified state preceding it.
        /// </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="state">    The state preceding the items of interest. </param>
        ///
        /// <returns>   A dictionary of the items and their weight. </returns>
        ///=================================================================================================
        public Dictionary<T, int> GetNextStates( ChainState<T> state ) {
            Dictionary<T, int> weights;
            if( this.items.TryGetValue( state, out weights ) ) {
                return new Dictionary<T, int>( weights );
            }

            return null;
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Assumes an empty starting state. </remarks>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( ) {
            return this.Chain( Enumerable.Empty<T>( ), new RandomWrapper( new Random( ) ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="previous"> The items preceding the first item in the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( IEnumerable<T> previous ) {
            return this.Chain( previous, new RandomWrapper( new Random( ) ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Assumes an empty starting state. </remarks>
        ///
        /// <param name="seed"> The seed for the random number generator, used as the random number
        ///                     source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( int seed ) {
            return this.Chain( Enumerable.Empty<T>( ), new RandomWrapper( new Random( seed ) ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="previous"> The items preceding the first item in the chain. </param>
        /// <param name="seed">     The seed for the random number generator, used as the random number
        ///                         source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( IEnumerable<T> previous, int seed ) {
            return this.Chain( previous, new RandomWrapper( new Random( seed ) ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Assumes an empty starting state. </remarks>
        ///
        /// <param name="rand"> The random number source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( Random rand ) {
            return this.Chain( Enumerable.Empty<T>( ), new RandomWrapper( rand ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="previous"> The items preceding the first item in the chain. </param>
        /// <param name="rand">     The random number source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( IEnumerable<T> previous, Random rand ) {
            return this.Chain( previous, new RandomWrapper( rand ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Assumes an empty starting state. </remarks>
        ///
        /// <param name="rand"> The random number source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( RandomNumberGenerator rand ) {
            return this.Chain( Enumerable.Empty<T>( ), new RandomNumberGeneratorWrapper( rand ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="previous"> The items preceding the first item in the chain. </param>
        /// <param name="rand">     The random number source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( IEnumerable<T> previous, RandomNumberGenerator rand ) {
            return this.Chain( previous, new RandomNumberGeneratorWrapper( rand ) );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Assumes an empty starting state. </remarks>
        ///
        /// <param name="rand"> The random number source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( IRandom rand ) {
            return this.Chain( Enumerable.Empty<T>( ), rand );
        }

        ///=================================================================================================
        /// <summary>   Randomly walks the chain. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/21/2015. </remarks>
        ///
        /// <param name="previous"> The items preceding the first item in the chain. </param>
        /// <param name="rand">     The random number source for the chain. </param>
        ///
        /// <returns>   An <see cref="IEnumerable&lt;T&gt;"/> of the items chosen. </returns>
        ///=================================================================================================
        public IEnumerable<T> Chain( IEnumerable<T> previous, IRandom rand ) {
            Queue<T> state = new Queue<T>( previous );
            while( true ) {
                while( state.Count > this.order ) {
                    state.Dequeue( );
                }

                var key = new ChainState<T>( state );

                Dictionary<T, int> weights;
                if( !this.items.TryGetValue( key, out weights ) ) {
                    yield break;
                }

                int terminalWeight;
                this.terminals.TryGetValue( key, out terminalWeight );

                var total = weights.Sum( w => w.Value );
                var value = rand.Next( total + terminalWeight ) + 1;

                if( value > total ) {
                    yield break;
                }

                var currentWeight = 0;
                foreach( var nextItem in weights ) {
                    currentWeight += nextItem.Value;
                    if( currentWeight >= value ) {
                        yield return nextItem.Key;
                        state.Enqueue( nextItem.Key );
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
