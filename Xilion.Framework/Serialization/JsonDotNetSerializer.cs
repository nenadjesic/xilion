using System.Text;
using Newtonsoft.Json;

namespace Xilion.Framework.Serialization
{
    public class JsonDotNetSerializer : ISerializer
    {
        // ReSharper disable CoVariantArrayConversion
        private static readonly JsonConverter[] _converters = new JsonConverter[]
                                                                  {
                                                                      new EnumerationJsonConverter(),
                                                                      new longJsonConverter()
                                                                  };

        // ReSharper restore CoVariantArrayConversion

        #region ISerializer Members

        public string Serialize<T>(object objectToSerialize, Encoding encoding)
        {
            return JsonConvert.SerializeObject(objectToSerialize, Formatting.None, _converters);
        }

        public T Deserialize<T>(string source, Encoding encoding)
        {
            return JsonConvert.DeserializeObject<T>(source, _converters);
        }

        #endregion
    }
}