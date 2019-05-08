using System;

namespace Xilion.Framework.Serialization
{
    public static class Serializer
    {
        public static Func<ISerializer> Default = () => new JsonDotNetSerializer();
    }
}