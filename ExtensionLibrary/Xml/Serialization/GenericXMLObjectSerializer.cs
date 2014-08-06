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
using System.Xml.XPath;
using System.Xml.Xsl;
using ExtensionLibrary.Strings;

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
            if (obj != null && nameSpaces != null)
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
            return null;
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
            if (obj != null && nameSpaces != null)
            {
                //create settings if needed
                if (prettyPrint)
                {
                    settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.IndentChars = "  ";
                }
                return Serialize<T>(obj, nameSpaces);
            }

            return null;
        }

        /// <summary>
        /// Deserializes the specified serialized object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObj">The serialized object.</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string serializedObj) where T : class
        {
            if (serializedObj.IsNotNullOrEmpty())
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T result;

                using (TextReader reader = new StringReader(serializedObj))
                    result = xs.Deserialize(reader) as T;

                return result;
            }

            return null;
        }

        /// <summary>
        /// Validates XML against the given xsd
        /// </summary>
        /// <param name="xmlFilePath">The path where the xml is located</param>
        /// <param name="xsdFilePath">The path where the xsd is located</param>
        /// <returns></returns>
        public static bool ValidateXml(this string xmlFilePath, string xsdFilePath)
        {
            if (xmlFilePath.IsNotNullOrEmpty() && xsdFilePath.IsNotNullOrEmpty())
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
            }

            return false;
        }

        /// <summary>
        /// Transforms the given XML string with the given stylesheet
        /// </summary>
        /// <param name="xslpath">Path of XSL</param>
        /// <returns></returns>
        public static string Transform(this string xmlString, string xslpath)
        {
            string output = String.Empty;

            if (xmlString.IsNotNullOrEmpty() && xslpath.IsNotNullOrEmpty())
            {
                try
                {
                    StringReader rdr = new StringReader(xmlString);
                    XPathDocument myXPathDoc = new XPathDocument(rdr);

                    var myXslTrans = new XslCompiledTransform();

                    myXslTrans.Load(xslpath);

                    StringWriter sw = new StringWriter();
                    XmlWriter xwo = XmlWriter.Create(sw, settings);

                    myXslTrans.Transform(myXPathDoc, null, xwo);
                    output = sw.ToString();

                    settings = null;
                    xwo.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return output;
        }
    }
}
