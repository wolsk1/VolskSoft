namespace VolskSoft.Bibliotheca.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(LibConfigElement))]
    public sealed class LibsConfigElementCollection : ConfigurationElementCollection
    {
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals("dll", StringComparison.InvariantCultureIgnoreCase);
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
                return "dll";
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LibConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LibConfigElement)element).Pattern;
        }

        public LibConfigElement this[int idx]
        {
            get { return (LibConfigElement)BaseGet(idx); }
        }
    }
}