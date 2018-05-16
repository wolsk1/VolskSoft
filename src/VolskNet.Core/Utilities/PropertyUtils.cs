namespace VolskSoft.Bibliotheca
{
    using System;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    
    public static class PropertyUtils
    {
        /// <summary>
        /// Gets the object property names.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static List<PropertyInfo> GetProperties(Type type, BindingFlags bindingFlags = BindingFlags.Public)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            //TODO extract property info extraction in seperate method
            var propertyInfos = type.GetProperties(bindingFlags);

            if (propertyInfos.Any())
            {
                return propertyInfos.ToList();
            }

            return type.GetRuntimeProperties()
                .ToList();
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="classObject">The class object.</param>
        /// <param name="propName">Name of the property.</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object classObject, string propName)
        {
            if (classObject == null)
            {
                throw new ArgumentNullException("classObject");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new ArgumentNullException("propName");
            }

            var type = classObject.GetType();
            var propertyInfo = type.GetProperty(propName);

            return propertyInfo != null ? propertyInfo.GetValue(classObject) : null;
        }
    }
}