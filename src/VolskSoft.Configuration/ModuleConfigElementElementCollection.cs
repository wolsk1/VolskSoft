namespace VolskSoft.Bibliotheca.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ModuleConfigElement))]
    public sealed class ModuleConfigElementElementCollection : ConfigurationElementCollection
    {
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals("module", StringComparison.InvariantCultureIgnoreCase);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "module";
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleConfigElement)element).Name;
        }

        public ModuleConfigElement this[int idx]
        {
            get { return (ModuleConfigElement)BaseGet(idx); }
        }
    }
}