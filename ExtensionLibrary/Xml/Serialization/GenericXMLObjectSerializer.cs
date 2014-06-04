/*
 * Author: Wim Ombelets, Nicolas Pierre
 * Date: 2014-04-01
 * https://github.com/ombeletw/GenericXMLObjectSerializer
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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
        public static string Serialize<T>(this T obj, XmlSerializerNamespaces nameSpaces) where T : class
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();

            using (XmlWriter xmlwriter = XmlWriter.Create(writer, settings))
            {
                xs.Serialize(xmlwriter, obj, nameSpaces);
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
        public static string Serialize<T>(this T obj, bool prettyPrint, XmlSerializerNamespaces nameSpaces) where T : class
        {
            //create settings if needed
            if (prettyPrint)
            {
                settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
            }

            return Serialize<T>(obj,nameSpaces);
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

        /// <summary>
        /// Validates XML against the given xsd
        /// </summary>
        /// <param name="xmlFilePath">The path where the xml is located</param>
        /// <param name="xsdFilePath">The path where the xsd is located</param>
        /// <returns></returns>
        public static bool ValidateXml(this string xmlFilePath, string xsdFilePath)
        {
            try
            {
                using (StreamReader s = new StreamReader(xmlFilePath, true))
                {
                    var xDoc = XDocument.Load(s);
                    var schema = new XmlSchemaSet();
                    schema.Add(null, xsdFilePath);

                    xDoc.Validate(schema, (o, e) =>
                    {
                        throw new XmlSchemaValidationException(e.Message);
                    });

                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return false;
        }
    }
}
