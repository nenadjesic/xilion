using System.Text;

namespace Xilion.Framework.Serialization
{
    public static class SerializerExtensions
    {
        public static string Serialize<T>(this ISerializer serializer, T objectToSerialize)
        {
            return serializer.Serialize<T>(objectToSerialize, Encoding.UTF8);
        }

        public static T Deserialize<T>(this ISerializer serializeer, string source)
        {
            return serializeer.Deserialize<T>(source, Encoding.UTF8);
        }
    }
}