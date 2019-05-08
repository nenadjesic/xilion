using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xilion.Framework
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void IsNotNegative(int parameter, string parameterName)
        {
            if (parameter < 0)
                throw new ArgumentOutOfRangeException(
                    parameterName, String.Format("Parameter '{0}' cannot be negative", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotNegative(float parameter, string parameterName)
        {
            if (parameter < 0f)
                throw new ArgumentOutOfRangeException(
                    parameterName, String.Format("Parameter '{0}' cannot be negative", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
                throw new ArgumentNullException(
                    parameterName, String.Format("Parameter '{0}' cannot be null", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty<T>(T[] parameter, string parameterName)
        {
            IsNotNull(parameter, parameterName);
            if (parameter.Length == 0)
                throw new ArgumentException(String.Format("Array '{0}' cannot be empty", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty<T>(ICollection<T> parameter, string parameterName)
        {
            IsNotNull(parameter, parameterName);
            if (parameter.Count == 0)
                throw new ArgumentException(String.Format("Collection '{0}' cannot be empty", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter ?? string.Empty))
                throw new ArgumentException(String.Format("Parameter '{0}' cannot be null or empty", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotVirtualPath(string parameter, string parameterName)
        {
            IsNotNullOrEmpty(parameter, parameterName);
            if (!parameter.StartsWith("~/", StringComparison.Ordinal))
                throw new ArgumentException("Source must be virtual path starts with '~/'", parameterName);
        }

        [DebuggerStepThrough]
        public static void IsNotZeroOrNegative(int parameter, string parameterName)
        {
            if (parameter <= 0)
                throw new ArgumentOutOfRangeException(
                    parameterName, String.Format("Parameter '{0}' cannot be zero or negative", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsNotlongEmpty(long parameter, string parameterName)
        {
            if (parameter == 0)
                throw new ArgumentException(String.Format("long '{0}' cannot be empty", parameterName));
        }

        [DebuggerStepThrough]
        public static void IsOfType<T>(object parameter, string parameterName)
        {
            if (parameter != null && !(parameter is T))
                throw new ArgumentException(
                    String.Format("Parameter '{0}' must be of type '{1}'", parameterName, typeof (T)));
        }
    }
}