using System.Text;

namespace Xilion.Framework.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(object objectToSerialize, Encoding encoding);
        T Deserialize<T>(string source, Encoding encoding);
    }
}