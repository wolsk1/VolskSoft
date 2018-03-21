namespace VolskSoft.Bibliotheca.Xml
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public static class XmlProvider
    {
        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            string draftXml;

            using (var writer = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(obj.GetType());
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                serializer.Serialize(writer, obj, ns);
                draftXml = writer.ToString();
            }

            return draftXml;
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(XmlNode xml)
        {
            if (xml == null)
            {
                return default(T);
            }

            var serializer = new XmlSerializer(typeof(T));
            var reader = new XmlNodeReader(xml);
            var obj = (T)serializer.Deserialize(reader);

            return obj;
        }

        /// <summary>
        /// Deserializes the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static T DeserializeElement<T>(XElement element)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = element.CreateReader())
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Removes the XML header.
        /// </summary>
        /// <param name="xmlRequestString">The XML request string.</param>
        /// <returns></returns>
        public static string RemoveXmlHeader(this string xmlRequestString)
        {
            return Regex.Replace(xmlRequestString, @"<\?[^>]*>" + Environment.NewLine, String.Empty);
        }

        public static int AsInt(XmlNode node)
        {
            return node != null
               ? Convert.ToInt32(node.Value, CultureInfo.InvariantCulture)
               : 0;
        }

        public static decimal AsDecimal(XmlNode node)
        {
            return node != null
                ? Convert.ToDecimal(
                    node.Value.Replace(',', '.'),
                    new NumberFormatInfo { NumberDecimalSeparator = "." })
                : 0;
        }
    }
}