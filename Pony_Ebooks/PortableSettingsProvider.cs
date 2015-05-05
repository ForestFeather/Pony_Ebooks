// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - PortableSettingsProvider.cs 
// // 
// //  Last Changed By: ForestFeather - 
// //  Last Changed Date: 7:55 PM, 04/05/2015
// //  Created Date: 7:53 PM, 04/05/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace Pony_Ebooks {
    public class PortableSettingsProvider : SettingsProvider {

        // Define some static strings later used in our XML creation
        // XML Root node
        private const string XMLROOT = "configuration";
        // Configuration declaration node
        private const string CONFIGNODE = "configSections";
        // Configuration section group declaration node
        private const string GROUPNODE = "sectionGroup";
        // User section node
        private const string USERNODE = "userSettings";
        // Application Specific Node
        private readonly string APPNODE = Assembly.GetExecutingAssembly( ).GetName( ).Name + ".Properties.Settings";
        private XmlDocument xmlDoc = null;
        // Override the ApplicationName property, returning the solution name.  No need to set anything, we just need to
        // retrieve information, though the set method still needs to be defined.
        public override string ApplicationName {
            get { return ( Assembly.GetExecutingAssembly( ).GetName( ).Name ); }
            set { return; }
        }

        private XmlDocument XMLConfig {
            get {
                // Check if we already have accessed the XML config file. If the xmlDoc object is empty, we have not.
                if( this.xmlDoc == null ) {
                    this.xmlDoc = new XmlDocument( );

                    // If we have not loaded the config, try reading the file from disk.
                    try {
                        this.xmlDoc.Load( Path.Combine( this.GetAppPath( ), this.GetSettingsFilename( ) ) );
                    }

                        // If the file does not exist on disk, catch the exception then create the XML template for the file.
                    catch( Exception ) {
                        // XML Declaration
                        // <?xml version="1.0" encoding="utf-8"?>
                        XmlDeclaration dec = this.xmlDoc.CreateXmlDeclaration( "1.0", "utf-8", null );
                        this.xmlDoc.AppendChild( dec );

                        // Create root node and append to the document
                        // <configuration>
                        XmlElement rootNode = this.xmlDoc.CreateElement( XMLROOT );
                        this.xmlDoc.AppendChild( rootNode );

                        // Create Configuration Sections node and add as the first node under the root
                        // <configSections>
                        XmlElement configNode = this.xmlDoc.CreateElement( CONFIGNODE );
                        this.xmlDoc.DocumentElement.PrependChild( configNode );

                        // Create the user settings section group declaration and append to the config node above
                        // <sectionGroup name="userSettings"...>
                        XmlElement groupNode = this.xmlDoc.CreateElement( GROUPNODE );
                        groupNode.SetAttribute( "name", USERNODE );
                        groupNode.SetAttribute( "type", "System.Configuration.UserSettingsGroup" );
                        configNode.AppendChild( groupNode );

                        // Create the Application section declaration and append to the groupNode above
                        // <section name="AppName.Properties.Settings"...>
                        XmlElement newSection = this.xmlDoc.CreateElement( "section" );
                        newSection.SetAttribute( "name", this.APPNODE );
                        newSection.SetAttribute( "type", "System.Configuration.ClientSettingsSection" );
                        groupNode.AppendChild( newSection );

                        // Create the userSettings node and append to the root node
                        // <userSettings>
                        XmlElement userNode = this.xmlDoc.CreateElement( USERNODE );
                        this.xmlDoc.DocumentElement.AppendChild( userNode );

                        // Create the Application settings node and append to the userNode above
                        // <AppName.Properties.Settings>
                        XmlElement appNode = this.xmlDoc.CreateElement( this.APPNODE );
                        userNode.AppendChild( appNode );
                    }
                }
                return this.xmlDoc;
            }
        }

        // Override the Initialize method
        public override void Initialize( string name, NameValueCollection config ) {
            base.Initialize( this.ApplicationName, config );
        }

        // Simply returns the name of the settings file, which is the solution name plus ".config"
        public virtual string GetSettingsFilename( ) {
            return this.ApplicationName + ".config";
        }

        // Gets current executable path in order to determine where to read and write the config file
        public virtual string GetAppPath( ) {
            return new FileInfo( Assembly.GetExecutingAssembly( ).Location ).DirectoryName;
        }

        // Retrieve settings from the configuration file
        public override SettingsPropertyValueCollection GetPropertyValues( SettingsContext sContext,
                                                                           SettingsPropertyCollection settingsColl ) {
            // Create a collection of values to return
            SettingsPropertyValueCollection retValues = new SettingsPropertyValueCollection( );

            // Create a temporary SettingsPropertyValue to reuse
            SettingsPropertyValue setVal;

            // Loop through the list of settings that the application has requested and add them
            // to our collection of return values.
            foreach( SettingsProperty sProp in settingsColl ) {
                setVal = new SettingsPropertyValue( sProp );
                setVal.IsDirty = false;
                setVal.SerializedValue = this.GetSetting( sProp );
                retValues.Add( setVal );
            }
            return retValues;
        }

        // Save any of the applications settings that have changed (flagged as "dirty")
        public override void SetPropertyValues( SettingsContext sContext, SettingsPropertyValueCollection settingsColl ) {
            // Set the values in XML
            foreach( SettingsPropertyValue spVal in settingsColl ) {
                this.SetSetting( spVal );
            }

            // Write the XML file to disk
            try {
                this.XMLConfig.Save( Path.Combine( this.GetAppPath( ), this.GetSettingsFilename( ) ) );
            } catch( Exception ex ) {
                // Create an informational message for the user if we cannot save the settings.
                // Enable whichever applies to your application type.

                // Uncomment the following line to enable a MessageBox for forms-based apps
                MessageBox.Show(
                    ex.Message,
                    "Error writting configuration file to disk",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error );

                // Uncomment the following line to enable a console message for console-based apps
                //Console.WriteLine("Error writing configuration file to disk: " + ex.Message);
            }
        }

        // Retrieve values from the configuration file, or if the setting does not exist in the file, 
        // retrieve the value from the application's default configuration
        private object GetSetting( SettingsProperty setProp ) {
            object retVal;
            try {
                // Search for the specific settings node we are looking for in the configuration file.
                // If it exists, return the InnerText or InnerXML of its first child node, depending on the setting type.

                // If the setting is serialized as a string, return the text stored in the config
                if( setProp.SerializeAs.ToString( ) == "String" ) {
                    return
                        this.XMLConfig.SelectSingleNode( "//setting[@name='" + setProp.Name + "']" )
                            .FirstChild.InnerText;
                }

                // If the setting is stored as XML, deserialize it and return the proper object.  This only supports
                // StringCollections at the moment - I will likely add other types as I use them in applications.
                else {
                    string settingType = setProp.PropertyType.ToString( );
                    string xmlData =
                        this.XMLConfig.SelectSingleNode( "//setting[@name='" + setProp.Name + "']" ).FirstChild.InnerXml;
                    XmlSerializer xs = new XmlSerializer( typeof( string[] ) );
                    string[] data = (string[]) xs.Deserialize( new XmlTextReader( xmlData, XmlNodeType.Element, null ) );

                    switch( settingType ) {
                        case "System.Collections.Specialized.StringCollection":
                            StringCollection sc = new StringCollection( );
                            sc.AddRange( data );
                            return sc;
                        default:
                            return "";
                    }
                }
            } catch( Exception ) {
                // Check to see if a default value is defined by the application.
                // If so, return that value, using the same rules for settings stored as Strings and XML as above
                if( ( setProp.DefaultValue != null ) ) {
                    if( setProp.SerializeAs.ToString( ) == "String" ) {
                        retVal = setProp.DefaultValue.ToString( );
                    } else {
                        string settingType = setProp.PropertyType.ToString( );
                        string xmlData = setProp.DefaultValue.ToString( );
                        XmlSerializer xs = new XmlSerializer( typeof( string[] ) );
                        string[] data =
                            (string[]) xs.Deserialize( new XmlTextReader( xmlData, XmlNodeType.Element, null ) );

                        switch( settingType ) {
                            case "System.Collections.Specialized.StringCollection":
                                StringCollection sc = new StringCollection( );
                                sc.AddRange( data );
                                return sc;

                            default:
                                return "";
                        }
                    }
                } else {
                    retVal = "";
                }
            }
            return retVal;
        }

        private void SetSetting( SettingsPropertyValue setProp ) {
            // Define the XML path under which we want to write our settings if they do not already exist
            XmlNode SettingNode = null;

            try {
                // Search for the specific settings node we want to update.
                // If it exists, return its first child node, (the <value>data here</value> node)
                SettingNode = this.XMLConfig.SelectSingleNode( "//setting[@name='" + setProp.Name + "']" ).FirstChild;
            } catch( Exception ) {
                SettingNode = null;
            }

            // If we have a pointer to an actual XML node, update the value stored there
            if( ( SettingNode != null ) ) {
                if( setProp.Property.SerializeAs.ToString( ) == "String" ) {
                    SettingNode.InnerText = setProp.SerializedValue.ToString( );
                } else {
                    // Write the object to the config serialized as Xml - we must remove the Xml declaration when writing
                    // the value, otherwise .Net's configuration system complains about the additional declaration.
                    SettingNode.InnerXml =
                        setProp.SerializedValue.ToString( )
                               .Replace( @"<?xml version=""1.0"" encoding=""utf-16""?>", "" );
                }
            } else {
                // If the value did not already exist in this settings file, create a new entry for this setting

                // Search for the application settings node (<Appname.Properties.Settings>) and store it.
                XmlNode tmpNode = this.XMLConfig.SelectSingleNode( "//" + this.APPNODE );

                // Create a new settings node and assign its name as well as how it will be serialized
                XmlElement newSetting = this.xmlDoc.CreateElement( "setting" );
                newSetting.SetAttribute( "name", setProp.Name );

                if( setProp.Property.SerializeAs.ToString( ) == "String" ) {
                    newSetting.SetAttribute( "serializeAs", "String" );
                } else {
                    newSetting.SetAttribute( "serializeAs", "Xml" );
                }

                // Append this node to the application settings node (<Appname.Properties.Settings>)
                tmpNode.AppendChild( newSetting );

                // Create an element under our named settings node, and assign it the value we are trying to save
                XmlElement valueElement = this.xmlDoc.CreateElement( "value" );
                if( setProp.Property.SerializeAs.ToString( ) == "String" ) {
                    valueElement.InnerText = setProp.SerializedValue.ToString( );
                } else {
                    // Write the object to the config serialized as Xml - we must remove the Xml declaration when writing
                    // the value, otherwise .Net's configuration system complains about the additional declaration
                    valueElement.InnerXml =
                        setProp.SerializedValue.ToString( )
                               .Replace( @"<?xml version=""1.0"" encoding=""utf-16""?>", "" );
                }

                //Append this new element under the setting node we created above
                newSetting.AppendChild( valueElement );
            }
        }

    }
}
