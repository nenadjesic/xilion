using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Xilion.Framework.Serialization
{
    public class DataContractJsonSerializer : ISerializer
    {
        #region ISerializer Members

        public string Serialize<T>(object objectToSerialize, Encoding encoding)
        {
            var ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof (T));
            var ms = new MemoryStream();
            ser.WriteObject(ms, objectToSerialize);

            string str = encoding.GetString(ms.ToArray());
            ms.Close();
            ms.Dispose();
            return str;
        }

        public T Deserialize<T>(string source, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(source);
            XmlDictionaryReader jsonReader = JsonReaderWriterFactory.CreateJsonReader(bytes,
                                                                                      XmlDictionaryReaderQuotas.Max);
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof (T));
            var graph = (T) serializer.ReadObject(jsonReader);
            jsonReader.Close();
            return graph;
        }

        #endregion
    }
}