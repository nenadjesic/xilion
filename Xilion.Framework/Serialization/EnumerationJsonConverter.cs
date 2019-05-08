using System;
using Newtonsoft.Json;

namespace Xilion.Framework.Serialization
{
    public class EnumerationJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enumeration = value as Enumeration;
            writer.WriteValue(enumeration == null ? 0 : enumeration.Value);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!typeof (Enumeration).IsAssignableFrom(objectType))
                return null;

            return Enumeration.FromValue(objectType, Convert.ToInt32(reader.Value));
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (Enumeration).IsAssignableFrom(objectType);
        }
    }
}