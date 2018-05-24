namespace VolskSoft.Bibliotheca.Configuration
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// Base class for all settings providers.
    /// </summary>
    public class SettingsProviderBase : ISettingsProvider
    {
        /// <summary>
        /// Reference to configuration section object.
        /// </summary>
        private ConfigurationSection section;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsProviderBase"/> class.
        /// </summary>
        public SettingsProviderBase()
        {
            Values = new SettingsCollection();
        }

        /// <inheritdoc />
        public SettingsCollection Values { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets provider's context string. (Used for visualization in error messages).
        /// </summary>
        public virtual string Context
        {
            get
            {
                return "";
            }
        }

        /// <inheritdoc />
        public object this[string key]
        {
            get
            {
                return Values[key];
            }
        }

        /// <inheritdoc />
        public object GetValue(string key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            foreach (string s in Values.Keys)
            {
                if (s.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public void Reload()
        {
            Values.Clear();
            MemberwiseInitialize(section);
        }

        /// <inheritdoc />
        public void Initialize(ConfigurationSection configurationSection)
        {
            section = configurationSection;
            MemberwiseInitialize(section);
        }
        
        protected virtual void MemberwiseInitialize(ConfigurationSection configurationSection)
        {
        }

        /// <summary>
        /// Gets the settings value either from section or settings context.
        /// </summary>
        /// <param name="key">The name of the settings key to get value from.</param>
        /// <returns>Value of the key</returns>
        /// <exception cref="InvalidOperationException">If key is not found.</exception>
        /// <remarks>Method is primary used by providers to look for initialization parameters needed by provider.</remarks>
        protected string GetSettingsValue(string key)
        {
            var value = section.Values.Get(key);

            if (!string.IsNullOrEmpty(value))
                return value;

            return Settings.Contains(key)
                ? Settings.GetString(key)
                : throw new InvalidOperationException("Missing parameter \"" + key + "\".");
        }

        /// <summary>
        /// Determines whether section or settings context contains specified key.
        /// </summary>
        /// <param name="key">The name of the settings key.</param>
        /// <returns>
        /// 	<c>true</c> if section or settings context contains specified key; otherwise, <c>false</c>.
        /// </returns>
        protected bool ContainsSettingsValue(string key)
        {
            var value = section.Values.Get(key);

            return !string.IsNullOrEmpty(value) 
                   || Settings.Contains(key);
        }
    }
}