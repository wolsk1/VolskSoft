namespace VolskSoft.Bibliotheca.Configuration
{
    using System.Collections.Specialized;

    /// <summary>
    /// Contains information about settings provider read from application configuration file.
    /// </summary>
    public class ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public NameValueCollection Values { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether configuration provider must be called on startup.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if configuration provider must be called on startup; otherwise, <c>false</c>.
        /// </value>
        public bool ShouldReadOnStartup { get; set; }
    }
}