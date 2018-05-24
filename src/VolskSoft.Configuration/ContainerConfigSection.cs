namespace VolskSoft.Bibliotheca.Configuration
{
    using System.Configuration;

    /// <inheritdoc />
    /// <summary>
    /// Configuration for IoC container
    /// </summary>
    public sealed class ContainerConfigSection : ConfigurationSection
    {
        private const string LIBS_SECTION_NAME = "libs";
        private const string MODULES_SECTION_NAME = "modules";

        /// <summary>
        /// Gets a value indicating that application [is executable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [app is executable]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("loadFromExecutable", IsRequired = false)]
        public bool LoadFromExecutable { get { return (bool)this["loadFromExecutable"]; } }

        [ConfigurationProperty(LIBS_SECTION_NAME, IsRequired = false)]
        public LibsConfigElementCollection Libs
        {
            get
            {
                return this[LIBS_SECTION_NAME] as LibsConfigElementCollection;
            }
            set
            {
                this[LIBS_SECTION_NAME] = value;
            }
        }

        [ConfigurationProperty(MODULES_SECTION_NAME, IsRequired = false)]
        public ModuleConfigElementElementCollection Modules
        {
            get
            {
                return this[MODULES_SECTION_NAME] as ModuleConfigElementElementCollection;
            }
            set
            {
                this[MODULES_SECTION_NAME] = value;
            }
        }
    }
}