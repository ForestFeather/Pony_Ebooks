// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - App.xaml.cs 
// // 
// //  Copyright 2011-2013
// //  WR Medical Electronics Company
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 9:14 PM, 18/01/2015
// //  Created Date: 7:10 PM, 18/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

#region Imported Namespaces

using System;
using System.Windows;

using Pony_Ebooks.Properties;

using Tweetinvi;
using Tweetinvi.Core.Interfaces.oAuth;

#endregion

namespace Pony_Ebooks {
    ///=================================================================================================
    /// <summary>   Interaction logic for App.xaml. </summary>
    ///
    /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
    ///
    /// <seealso cref="T:System.Windows.Application"/>
    ///=================================================================================================
    public partial class App : Application {
        #region Members

        ///=================================================================================================
        /// <summary>   Event handler. Called by App for on startup events. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Startup event information. </param>
        ///=================================================================================================
        private void App_OnStartup( object sender, StartupEventArgs e ) {
            var credentials = this.InitTwitter( );
            if( credentials == null ) {
                this.EndProgram( "Unable to get valid credentials.  Goodbye!" );
            }

            Console.WriteLine( "So we got credentials, whoo!  Let's go kiddies." );
        }

        ///=================================================================================================
        /// <summary>   Initialises the twitter. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///
        /// <returns>   . </returns>
        ///=================================================================================================
        private IOAuthCredentials InitTwitter( ) {
            string consumerKey;
            string consumerSecret;
            this.GetConsumerKeys( out consumerKey, out consumerSecret );

            string accessTokenKey;
            string accessTokenSecret;
            this.GetAccessTokens( consumerKey, consumerSecret, out accessTokenKey, out accessTokenSecret );

            // Setup your application credentials
            TwitterCredentials.ApplicationCredentials = TwitterCredentials.CreateCredentials(
                accessTokenKey, accessTokenSecret, consumerKey, consumerSecret );

            Console.WriteLine( "Successfully connected to the credentials system!" );

            // Return credentials
            return TwitterCredentials.ApplicationCredentials;
        }

        ///=================================================================================================
        /// <summary>   Gets access tokens. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///
        /// <param name="consumerKey">          [out] The consumer key. </param>
        /// <param name="consumerSecret">       [out] The consumer secret. </param>
        /// <param name="accessTokenKey">       [out] The access token key. </param>
        /// <param name="accessTokenSecret">    [out] The access token secret. </param>
        ///=================================================================================================
        private void GetAccessTokens( string consumerKey,
                                      string consumerSecret,
                                      out string accessTokenKey,
                                      out string accessTokenSecret ) {
            accessTokenKey = Settings.Default.AccessToken_Key;
            accessTokenSecret = Settings.Default.AccessToken_Secret;

            if( !string.IsNullOrEmpty( accessTokenKey ) &&
                !string.IsNullOrEmpty( accessTokenSecret ) ) {
                Console.WriteLine( "Found cached access tokens, continuing." );
                return;
            }

            var applicationCredentials = CredentialsCreator.GenerateApplicationCredentials(
                consumerKey, consumerSecret );
            var url = CredentialsCreator.GetAuthorizationURL( applicationCredentials );
            Console.WriteLine( "Go on : {0}", url );
            Console.WriteLine( "When presented enter the authorization code: " );

            var verifierCode = Console.ReadLine( );

            // Here we provide the entire URL where the user has been redirected
            var newCredentials = CredentialsCreator.GetCredentialsFromVerifierCode(
                verifierCode, applicationCredentials );
            Console.WriteLine( "Access Token = {0}", newCredentials.AccessToken );
            Console.WriteLine( "Access Token Secret = {0}", newCredentials.AccessTokenSecret );

            Settings.Default.AccessToken_Key = newCredentials.AccessToken;
            Settings.Default.AccessToken_Secret = newCredentials.AccessTokenSecret;
            Settings.Default.Save( );

            accessTokenKey = Settings.Default.AccessToken_Key;
            accessTokenSecret = Settings.Default.AccessToken_Secret;
        }

        ///=================================================================================================
        /// <summary>   Gets consumer keys. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///
        /// <param name="consumerKey">      [out] The consumer key. </param>
        /// <param name="consumerSecret">   [out] The consumer secret. </param>
        ///=================================================================================================
        private void GetConsumerKeys( out string consumerKey, out string consumerSecret ) {
            consumerKey = Settings.Default.Consumer_Key;
            consumerSecret = Settings.Default.Consumer_Secret;
            if( !string.IsNullOrEmpty( consumerKey ) &&
                !string.IsNullOrEmpty( consumerSecret ) ) {
                return;
            }
            // Enter consumer keys and secrets
            Console.WriteLine( "Enter Consumer Key: " );
            var tmpKey = Console.ReadLine( );

            Console.WriteLine( "Enter Consumer Secret: " );
            var tmpSecret = Console.ReadLine( );

            if( tmpKey != null &&
                tmpSecret != null ) {
                Settings.Default.Consumer_Key = tmpKey.Trim( );
                Settings.Default.Consumer_Secret = tmpSecret.Trim( );
            } else {
                this.EndProgram( "Please re-run program and re-enter Consumer Information." );
            }

            Settings.Default.Save( );
            consumerKey = Settings.Default.Consumer_Key;
            consumerSecret = Settings.Default.Consumer_Secret;
        }

        ///=================================================================================================
        /// <summary>   Ends a program. </summary>
        ///
        /// <remarks>   Collin O' Connor, 1/18/2015. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///=================================================================================================
        private void EndProgram( string message ) {
            Console.WriteLine( message );
            Console.WriteLine( "Press any key to terminate the program." );
            Console.ReadKey( );
            Current.Shutdown( -1 );
        }

        #endregion
    }
}
