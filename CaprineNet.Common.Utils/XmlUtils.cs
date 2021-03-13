#region License
/*
    CaprineNet.Common.Utils
    Copyright(C) 2021 Caprine Logic
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.
    
    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
*/
#endregion License

using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;

namespace CaprineNet.Common.Utils
{
    public static class XmlUtils
    {
        private static readonly XmlWriterSettings defaultSettings = new XmlWriterSettings()
        {
            Indent = true,
            IndentChars = "\t",
            OmitXmlDeclaration = true
        };

        /// <summary>
        /// Serializes a class <paramref name="object"/> to an XML file at the defined <paramref name="path"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="object"></param>
        /// <param name="settings"><see cref="XmlWriterSettings">XmlWriterSettings</see> to use for this operation. Use null to use the <see cref="defaultSettings">default settings.</see></param>
        public static void Serialize<T>(string path, T @object, XmlWriterSettings settings = null)
        {
            if(settings == null)
            {
                settings = defaultSettings;
            }

            using(var sw = new StreamWriter(path))
            using(var writer = XmlWriter.Create(sw, settings))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, @object);
            }
        }

        /// <summary>
        /// Deserializes a file by its <paramref name="path"/> to a class object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        public static T Deserialize<T>(string path)
        {
            Ensure.Exists(new FileInfo(path));

            using(var sr = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
