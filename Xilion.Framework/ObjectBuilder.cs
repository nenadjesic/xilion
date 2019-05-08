using System;
using System.ComponentModel;

namespace Xilion.Framework
{
    public class ObjectBuilder
    {
        public static T BuildObjectValue<T>(object value, T defaultValue = default(T))
        {
            try
            {
                object converted = BuildObjectValue(value, typeof (T), defaultValue);
                return (T) converted;
            }
            catch (InvalidCastException cex)
            {
                InvalidCastException exc = cex;
                return default(T);
            }
        }

        public static object BuildObjectValue(object value, Type type, object defaultValue)
        {
            bool isInstanceOf = type.IsInstanceOfType(value);
            Type elementType = type;

            if (value != null)
            {
                bool checkGenericConversion = false;

                if (type.IsGenericType &&
                    (type.GetGenericTypeDefinition() == typeof (Nullable<>)))
                {
                    elementType = type.GetGenericArguments()[0];
                    checkGenericConversion = true;
                }

                if (value is string)
                    value = ConvertFromStringToElementType((string) value, elementType);

                if (value is Int32 || value is Int64)
                    value = ConvertFromStringToElementType(value.ToString(), elementType);

                if (checkGenericConversion)
                {
                    Type valueType = value.GetType();
                    if (elementType != valueType)
                        throw new InvalidOperationException("Invalid object conversion");
                }
            }
            else if (!isInstanceOf)
                value = defaultValue;

            return value;
        }

        private static object ConvertFromStringToElementType(string value, Type type)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);

            try
            {
                return converter.ConvertFromInvariantString(value);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid object conversion", ex);
            }
        }
    }
}