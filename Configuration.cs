using System;
using System.Reflection;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Configuration;

namespace Cool
{
    /// <summary>
    /// ConfigurationSettings allows app settings and sections to be stored in files besides app.config or web.config.
    /// </summary>
    public class ConfigurationSettings
    {

        /// <summary>
        /// Caches application settings.
        /// </summary>
        /// <remarks>Key: App setting key; 
        /// Value: App setting value.
        /// </remarks>
        private HybridDictionary _configInfo;

        /// <summary>
        /// Caches path to config file.
        /// </summary>
        private string _path;

        /// <summary>
        /// Used to watch for changes in config file.
        /// </summary>
        private FileSystemWatcher _configWatcher = null;

        /// <summary>
        /// Used to cache the config file as an XmlDocument.
        /// </summary>
        private XmlDocument _xmlDoc = null;

        /// <summary>
        /// Gets the name of the config file.
        /// </summary>
        /// <value>The name of the config file.</value>
        public string ConfigFileName
        {
            get
            {
                return this._path;
            }
        }


        /// <summary>
        /// Gets the <see cref="String"/> with the specified key.
        /// </summary>
        /// <value></value>
        public string this[string key]
        {
            get
            {
                if (this._configInfo == null)
                {
                    this.LoadSettings();
                }
                if (this._configInfo.Contains(key))
                {
                    return Convert.ToString(this._configInfo[key]);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// Allows calling application to subscribe to the Event that something has changed in the configuration settings.
        /// </summary>
        public event EventHandler NotifyCaller;
        // Wire up the event

        /// <summary>
        /// Raise the Event NotifyCaller.
        /// </summary>
        protected void OnNotifyCaller()
        {
            if (NotifyCaller != null)
            {
                NotifyCaller(this, EventArgs.Empty);

            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettings"/> class. This will set the configuration file to the GetCallingAssembly filename with a .config suffix.
        /// </summary>
        /// <remarks>Attempts to get the path to the config
        /// file and start the watcher on that file.</remarks>
        public ConfigurationSettings()
        {
            try
            {
                // gets the calling assembly, which is likely shadow copied,
                // so we get the AssemblyName instance, specifying we want the
                // original codebase, and then we replace the URL format with
                // UNC format
                this._path = Assembly.GetCallingAssembly().GetName(false).CodeBase.Replace("file:///", "").Replace("/", "\\") + ".config";
                if (!File.Exists(this._path))
                {
                    throw new ApplicationException(string.Format("The Calling Assembly '{0}' does not exist.", this._path));

                    // GetEntryAssembly was tried, but it was null from a WindowsService for some unknown reason
                    //this._path = Assembly.GetEntryAssembly().GetName(false).CodeBase.Replace("file:///", "").Replace("/", "\\") + ".config";
                }

                this.ConstructorCommon();

            }
            finally
            {
            }

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettings"/> class.  This constructor allows the caller to specify any .config file.
        /// </summary>
        /// <param name="configFileName">Name of the config file.</param>
        public ConfigurationSettings(string configFileName)
        {

            // gets the calling assembly, which is likely shadow copied,
            // so we get the AssemblyName instance, specifying we want the
            // original codebase, and then we replace the URL format with
            // UNC format
            this._path = configFileName;
            this.ConstructorCommon();


        }






        /// <summary>
        /// Common code needed by the Constructor(s).
        /// </summary>
        private void ConstructorCommon()
        {

            //this.WriteOutDebugTextFile (" The _path file name is " + this._path );
            //this.WriteOutAssemblyFileNames();


            if (!File.Exists(this._path))
            {
                throw new ApplicationException(string.Format("The file '{0}' does not exist", this._path));
            }

            // create the watcher for the config file
            this._configWatcher = new FileSystemWatcher();
            this._configWatcher.Path = this._path.Substring(0, this._path.LastIndexOf("\\"));
            this._configWatcher.Filter = this._path.Substring(this._path.LastIndexOf("\\") + 1);
            // filter to watch only for writes
            this._configWatcher.NotifyFilter = NotifyFilters.LastWrite;
            // handle only changed and created events
            this._configWatcher.Changed += new FileSystemEventHandler(this.ConfigChanged);
            this._configWatcher.Created += new FileSystemEventHandler(this.ConfigChanged);
            this._configWatcher.EnableRaisingEvents = true;

        }





        #region "Custom Object Handler"

        /// <summary>
        /// Create the concrete object using the information found in the specified section name.  Mimicks the IConfigurationSectionHandler.Create method.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>object which should cast to the correct type</returns>
        public object Create(string sectionName)
        {
            XmlNode xn = this.GetSectionMetaData(sectionName);

            if (null == xn)
            {
                throw new ApplicationException(string.Format("The file {0} did not contain the section '{1}'", this._path, sectionName));
            }

            XmlElement root = (XmlElement)xn;

            string name = string.Empty;
            string type = string.Empty;

            string currentXpath = "name";
            //this name should always be the same as the SectionName, but this makes sure I guess.  You can comment it out if you'd like.
            name = root.Attributes[currentXpath].Value;

            currentXpath = "type";
            type = root.Attributes[currentXpath].Value;

            //create an array, with the ";" delimiter
            string[] typeValues = type.Split(',');

            if (null == typeValues || typeValues.Length != 2)
            {
                throw new ApplicationException(string.Format("The type attibute should contain 2 items seperated by a comma.  The invalid value is {0}.", type));
            }

            string assemblyName = typeValues[1];
            string className = typeValues[0];

            IConfigurationSectionHandler icsh = this.CreateHandler(className, assemblyName);

            XmlNode sectionNode = this.GetSection(name);

            object returnObject = icsh.Create(this, this, sectionNode);

            return returnObject;

        }

        /// <summary>
        /// Creates the concrete IConfigurationSectionHandler using reflection.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        private IConfigurationSectionHandler CreateHandler(string className, string assemblyName)
        {
            IConfigurationSectionHandler returnObject = null;
            Assembly assem = Assembly.Load(assemblyName);
            if (null != assem)
            {
                Type objectType = assem.GetType(className, true, true);
                //The use of "homeState" is here... to show how the CreateInstance can have non default constructors.
                //Notice the second argument is an array of objects.. the array of objects ... needs to match one of the contructor-method-signatures
                returnObject = (IConfigurationSectionHandler)Activator.CreateInstance(objectType, null);
            }
            return returnObject;
        }

        /// <summary>
        /// Gets the config data as an XML document.
        /// </summary>
        /// <returns></returns>
        private XmlDocument GetLatestXmlDocument()
        {
            if (null == this._xmlDoc)
            {
                this._xmlDoc = new XmlDocument();
                this._xmlDoc.Load(this._path);
            }
            return this._xmlDoc;
        }


        /// <summary>
        /// Gets the section data as an XmlNode.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        private System.Xml.XmlNode GetSection(string sectionName)
        {

            XmlDocument xdoc = null;
            try
            {
                xdoc = GetLatestXmlDocument();
                string xpath = "configuration/" + sectionName;
                return xdoc.SelectSingleNode(xpath);
            }
            //			catch(Exception ex) 
            //			{
            //				Console.WriteLine (ex.Message );
            //			}
            finally
            {

            }

        }

        /// <summary>
        /// Gets the section meta data used to create the concrete IConfigurationSectionHandler.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        private System.Xml.XmlNode GetSectionMetaData(string sectionName)
        {
            XmlDocument xdoc = null;
            try
            {
                xdoc = GetLatestXmlDocument();
                string xpath = "//configuration/configSections/section[@name='" + sectionName + "']";
                return xdoc.SelectSingleNode(xpath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }


        #endregion


        /// <summary>
        /// Loads the expected config file and parses the
        /// app settings into the config cache.
        /// </summary>
        private void LoadSettings()
        {
            // initialize the cache
            this._configInfo = new HybridDictionary(10);
            // check for file
            if (File.Exists(this._path))
            {
                // load file into xml doc
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(this._path);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(String.Format("Could not load '{0}' into an XML document.", this._path), ex);
                }
                // for each app setting
                foreach (XmlNode node in doc.SelectNodes("/configuration/appSettings/add"))
                {
                    // if a key attribute exists
                    if (node.Attributes["key"] != null)
                    {


                        // TO DO perhaps replace this by removing and adding the "fresh value"
                        // check to see if key exists already in cache
                        if (this._configInfo.Contains(node.Attributes["key"].Value))
                        {
                            //throw  new ApplicationException( String.Format( "Configuration for '{0}' already contains key '{1}'.", this._path, node.Attributes["key"].Value));
                            continue;
                        }




                        // if value is not null, add this 
                        // name-value pair to the cache
                        if (node.Attributes["value"] != null)
                        {
                            this._configInfo.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
                        }
                    }
                }
            }

        }


        private string FullPathString = String.Empty;

        private DateTime TimeFired;
        /// <summary>
        /// Handles reloading the config when the file changes.
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="e">Args</param>
        private void ConfigChanged(object source, FileSystemEventArgs e)
        {

            // nasty bug in FileSystemWatcher fires twice (in about 4 ms) on changed file. This is a workaround...
            if (e.FullPath.ToUpper() == FullPathString && TimeFired.Subtract(DateTime.Now).TotalMilliseconds < 50)
            {
                return;
            }


            // set the values of the fullpath and time of the event fired to check / prevent dupe firings
            FullPathString = e.FullPath.ToUpper();
            TimeFired = DateTime.Now;


            // wait for any locks to be released
            System.Threading.Thread.Sleep(1500);

            //set the XmlDocument to null, so it will be freshly reloaded if needed
            this._xmlDoc = null;



            // reload settings
            this.LoadSettings();
            // Trigger our notification event
            this.OnNotifyCaller();
        }
    }
}
