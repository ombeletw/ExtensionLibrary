/*
 * Author: Wim Ombelets
 * Date: 2014-04-01
 * https://github.com/ombeletw/GenericXMLObjectSerializer
 */

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ExtensionLibrary.Xml.Serialization
{
    /// <summary>
    /// Serialize / Deserialize any given object to and from XML.
    /// </summary>
    public static class XMLObjectSerializer
    {
        private static XmlWriterSettings settings = null;

        /// <summary>
        /// Serializes the specified object of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj) where T : class
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();

            using (XmlWriter xmlwriter = XmlWriter.Create(writer, settings))
            {
                xs.Serialize(xmlwriter, obj);
            }

            //reset XmlWriterSettings
            settings = null;

            return writer.ToString();
        }

        /// <summary>
        /// Serializes the specified object of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="prettyPrint">if set to <c>true</c> [write indented XML].</param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj, bool prettyPrint) where T : class
        {
            //create settings if needed
            if (prettyPrint)
            {
                settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
            }

            return Serialize<T>(obj);
        }

        /// <summary>
        /// Deserializes the specified serialized object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObj">The serialized object.</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string serializedObj) where T : class
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            T result;

            using (TextReader reader = new StringReader(serializedObj))
            {
                result = xs.Deserialize(reader) as T;
            }

            return result;
        }
    }
}
