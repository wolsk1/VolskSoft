namespace VolskSoft.Bibliotheca.Configuration
{
    using System.Configuration;

    public class LibConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("pattern", IsRequired = true, IsKey = true)]
        public string Pattern { get { return (string)this["pattern"]; } }
    }
}