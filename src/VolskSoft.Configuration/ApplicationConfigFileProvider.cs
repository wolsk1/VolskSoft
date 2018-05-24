namespace VolskSoft.Bibliotheca.Configuration
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    /// <inheritdoc />
    /// <summary>
    /// Reads configuration from application configuration file's section.
    /// </summary>
    public class ApplicationConfigFileProvider : SettingsProviderBase
    {
        /// <summary>
        /// Name of the Xml node in configuration document to look for.
        /// </summary>
        private const string SectionNameAttributeName = "section-name";

        /// <summary>
        /// Name of the section for later usage.
        /// </summary>
        private string sectionName;

        /// <summary>
        /// Override this in child class to implement custom initialization.
        /// </summary>
        /// <param name="configurationSection">The section.</param>
        protected override void MemberwiseInitialize(ConfigurationSection configurationSection)
        {
            sectionName = GetSettingsValue(SectionNameAttributeName);

            if (string.IsNullOrEmpty(this.sectionName))
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Missing parameter \"{0}\".",
                        SectionNameAttributeName));
            }

            if (!(ConfigurationManager.GetSection(sectionName) is NameValueCollection values))
            {
                throw new SectionNotFoundException(sectionName);
            }

            var settingsCollection = new SettingsCollection();

            foreach (var key in values.AllKeys)
            {
                settingsCollection.Add(key, values.Get(key));
            }

            Values = settingsCollection;
        }
    }
}