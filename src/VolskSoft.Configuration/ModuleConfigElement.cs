namespace VolskSoft.Bibliotheca.Configuration
{
    using System.Configuration;

    public class ModuleConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name { get { return (string)this["name"]; } }
    }
}