using System;

namespace Xilion.Framework.Extensions
{
    /// <summary>
    /// Primitive type extensions.
    /// </summary>
    public static class PrimitiveExtensions
    {
        /// <summary>
        /// Converts the object to string. If object is null, an empty string will be returned.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>A string value.</returns>
        public static string ToNullSafeString(this object value)
        {
            return value == null ? String.Empty : value.ToString();
        }

        /// <summary>
        /// Checks if the string is guid value.
        /// </summary>
        /// <param name="value">String value to check.</param>
        /// <returns>true if the given string value is guid, false otherwise.</returns>
        public static bool Islong(this string value)
        {
            long result;
            return long.TryParse(value, out result);
        }

        /// <summary>
        /// Convert file size to string with unit.
        /// </summary>
        /// <param name="size">File size in bytes</param>
        /// <returns>Formated size with unit.</returns>
        public static string ToFileSize(this long size)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(size);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }
            if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }
            if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }
            return string.Concat(bytes, " Bytes");
        }
    }
}