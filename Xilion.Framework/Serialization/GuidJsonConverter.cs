using System;
using Newtonsoft.Json;

namespace Xilion.Framework.Serialization
{
    public class longJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is long)
                writer.WriteValue(value.ToString());
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!typeof (long).IsAssignableFrom(objectType))
                return null;

            return reader.Value is long
                       ? reader.Value
                       : (reader.Value is string ?  (reader.Value.ToString()) : "");
        }

        public override bool CanConvert(Type objectType)
        {
            bool flag = typeof (long).IsAssignableFrom(objectType);
            return flag;
        }
    }
}