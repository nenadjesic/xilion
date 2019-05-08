using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Xilion.Framework.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///   Compresses the given string instance with <c>GZip</c> compression method.
        /// </summary>
        /// <param name="instance"> A string instance to compress. </param>
        /// <returns> A compressed string instance. </returns>
        [DebuggerStepThrough]
        public static string Compress(this string instance)
        {
            byte[] buffer2;
            Guard.IsNotNullOrEmpty(instance, "instance");

            byte[] bytes = Encoding.UTF8.GetBytes(instance);
            using (var stream = new MemoryStream())
            {
                using (var stream2 = new GZipStream(stream, CompressionMode.Compress))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                }
                buffer2 = stream.ToArray();
            }

            var dst = new byte[buffer2.Length + 4];
            Buffer.BlockCopy(buffer2, 0, dst, 4, buffer2.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, dst, 0, 4);
            return Convert.ToBase64String(dst);
        }

        /// <summary>
        ///   Check whether any of the given values are contained in input string.
        /// </summary>
        /// <param name="input"> Input string to search in. </param>
        /// <param name="values"> Values to find. </param>
        /// <returns> True if at least one of the values is found, false otherwise. </returns>
        public static bool ContainsAny(this string input, string[] values)
        {
            if (String.IsNullOrWhiteSpace(input)) return false;

            return values.Any(input.Contains);
        }

        /// <summary>
        ///   Decompresses the given <c>GZip</c> compressed string.
        /// </summary>
        /// <param name="instance"> Compressed string instance. </param>
        /// <returns> A decompressed string instance. </returns>
        [DebuggerStepThrough]
        public static string Decompress(this string instance)
        {
            byte[] buffer2;
            Guard.IsNotNullOrEmpty(instance, "instance");

            byte[] buffer = Convert.FromBase64String(instance);
            using (var stream = new MemoryStream())
            {
                int num = BitConverter.ToInt32(buffer, 0);
                stream.Write(buffer, 4, buffer.Length - 4);
                buffer2 = new byte[num];
                stream.Seek(0L, SeekOrigin.Begin);
                using (var stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    stream2.Read(buffer2, 0, buffer2.Length);
                }
            }

            return Encoding.UTF8.GetString(buffer2);
        }

        /// <summary>
        ///   Formats string using names of objects
        /// </summary>
        /// <param name="format"> A composite format string. </param>
        /// <param name="source"> Object used for injection </param>
        /// <returns> A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args. </returns>
        public static string FormatWith(this string format, object source)
        {
            return FormatWith(format, null, source);
        }

        /// <summary>
        ///   Formats string using names of objects
        /// </summary>
        /// <param name="format"> A composite format string. </param>
        /// <param name="provider"> Format provider used for formating </param>
        /// <param name="source"> Object used for injection </param>
        /// <returns> A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args. </returns>
        public static string FormatWith(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            var r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
                              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            var values = new List<object>();
            string rewrittenFormat = r.Replace(format, delegate(Match m)
                                                           {
                                                               Group startGroup = m.Groups["start"];
                                                               Group propertyGroup = m.Groups["property"];
                                                               Group formatGroup = m.Groups["format"];
                                                               Group endGroup = m.Groups["end"];

                                                               values.Add((propertyGroup.Value == "0")
                                                                              ? source
                                                                              : DataBinder.Eval(source,
                                                                                                propertyGroup.Value));

                                                               return new string('{', startGroup.Captures.Count) +
                                                                      (values.Count - 1) + formatGroup.Value
                                                                      + new string('}', endGroup.Captures.Count);
                                                           });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }

        /// <summary>
        ///   Compares the two string instances with case insensitive comparison.
        /// </summary>
        /// <param name="first"> A string instance to compare. </param>
        /// <param name="second"> A second string instance to compare. </param>
        /// <returns> true if the instances are equal, false otherwise. </returns>
        [DebuggerStepThrough]
        public static bool IsCaseInsensitiveEqual(this string first, string second)
        {
            return String.Compare(first, second, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        ///   Compares the two string instances with case sensitive comparison.
        /// </summary>
        /// <param name="first"> A string instance to compare. </param>
        /// <param name="second"> A second string instance to compare. </param>
        /// <returns> true if the instances are equal, false otherwise. </returns>
        [DebuggerStepThrough]
        public static bool IsCaseSensitiveEqual(this string first, string second)
        {
            return String.CompareOrdinal(first, second) == 0;
        }

        /// <summary>
        ///   Replaces the new line characters "\n" with &lt;br /&gt; tags.
        /// </summary>
        /// <param name="value"> Value to replace. </param>
        /// <returns> A string with br tags. </returns>
        public static string ReplaceNewLinesWithBrTags(this string value)
        {
            return String.IsNullOrWhiteSpace(value) ? String.Empty : value.Replace("\n", "<br />");
        }

        /// <summary>
        ///   Strips the HTML tags from the given string value leaving the plain text.
        /// </summary>
        /// <param name="value"> String value to strip. </param>
        /// <returns> A clear text value. </returns>
        public static string StripHtml(this string value)
        {
            return String.IsNullOrWhiteSpace(value) ? String.Empty : Regex.Replace(value, @"<(.|\n)*?>", " ");
        }

        /// <summary>
        ///   Finds url formats and replace it for HTML links
        /// </summary>
        /// <param name="input"> Input string to format. </param>
        /// <returns> Formated string. </returns>
        public static string FormatHtml(this string input)
        {
            string output = input;
            var regxNewLine = new Regex(@"\s*(<[^>]+>)\s*");
            foreach (Match match in regxNewLine.Matches(output))
            {
                output = output.Replace(match.Value, "");
            }

            var regx =
                new Regex(
                    "http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*([a-zA-Z0-9\\?\\#\\=\\/]){1})?",
                    RegexOptions.IgnoreCase);

            MatchCollection mactches = regx.Matches(output);

            output = mactches.Cast<Match>().Aggregate(output,
                                                      (current, match) =>
                                                      current.Replace(match.Value,
                                                                      "<a href='" + match.Value + "' target='blank'>" +
                                                                      match.Value + "</a>"));
            return output.Replace("\n", "<br />");
        }

        /// <summary>
        ///   Takes a given number of characters from the start of given string value.
        /// </summary>
        /// <param name="value"> Value to process. </param>
        /// <param name="numberOfCharacters"> Number of characters to take. </param>
        /// <param name="suffix"> A suffix to append to resulting string. </param>
        /// <returns> A string containing the first number characters of the given string. </returns>
        public static string Take(this string value, int numberOfCharacters, string suffix = null)
        {
            string result = String.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length <= numberOfCharacters)
                    result = value;
                else
                {
                    result = value.Substring(0, numberOfCharacters);
                    if (suffix != null)
                        result = String.Concat(result, suffix);
                }
            }

            return result;
        }

        /// <summary>
        ///   Converts the string to boolean value.
        /// </summary>
        /// <param name="value"> Value to convert. </param>
        /// <returns> A boolean result. </returns>
        public static bool ToBoolean(this string value)
        {
            return ToBoolean(value, false);
        }

        /// <summary>
        ///   Converts the string to boolean value or returns default value if the conversion could not be performed.
        /// </summary>
        /// <param name="value"> Value to convert. </param>
        /// <param name="defaultValue"> Default value to return if conversion could not be performed. </param>
        /// <returns> A boolean result. </returns>
        public static bool ToBoolean(this string value, bool defaultValue)
        {
            bool flag;
            return !Boolean.TryParse(value, out flag) ? defaultValue : flag;
        }

        /// <summary>
        ///   Converts the string instance to enum type, or returns default value if the conversion could not be performed.
        /// </summary>
        /// <typeparam name="T"> Enum type. </typeparam>
        /// <param name="instance"> String value to convert. </param>
        /// <param name="defaultValue"> Default value to return if conversion could not be performed. </param>
        /// <returns> An enumeration value. </returns>
        [DebuggerStepThrough]
        public static T ToEnum<T>(this string instance, T defaultValue) where T : struct
        {
            T result;
            return Enum.TryParse(instance, true, out result) ? result : defaultValue;
        }

        /// <summary>
        ///   Converts the given string value to MD5 hash code.
        /// </summary>
        /// <param name="value"> A string value to convert. </param>
        /// <returns> MD5 hash of the given value. </returns>
        public static string ToMd5(this string value)
        {
            Guard.IsNotNull(value, "value");
            MD5 algorithm = MD5.Create();

            byte[] data = Encoding.ASCII.GetBytes(value);
            data = algorithm.ComputeHash(data);

            var md5 = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                md5.Append(data[i].ToString("x2").ToLower());

            return md5.ToString();
        }

        public static string TitleFromFileName(this string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return String.Empty;
         
            string result = fileName.ToLowerInvariant()
                .Replace('_', ' ')
                .Replace('-', ' ');
            result = Path.GetFileNameWithoutExtension(result);
            result = Regex.Replace(result, @"\s+", " ");
            return result;
        }
    }
}