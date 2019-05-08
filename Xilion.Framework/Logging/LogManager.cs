using System;
using Xilion.Framework.Logging.Default;

namespace Xilion.Framework.Logging
{
    public static class LogManager
    {
        public static Func<ILoggerFactory> Factory = () => new NLogLoggerFactory();

        /// <summary>
        /// Gets the logger with the given name.
        /// </summary>
        /// <param name="name">Name of the logger.</param>
        /// <returns>A new instance of the logger with the given name.</returns>
        public static ILogger GetLogger(string name)
        {
            return Factory().GetLogger(name);
        }

        /// <summary>
        /// Gets the logger for the given type.
        /// </summary>
        /// <param name="type">Type to get the logger for.</param>
        /// <returns>A new instance of the logger for the given type.</returns>
        public static ILogger GetLogger(Type type)
        {
            return Factory().GetLogger(type.FullName);
        }

        /// <summary>
        /// Gets the logger for the given type.
        /// </summary>
        /// <returns>A new instance of the logger for the given type.</returns>
        public static ILogger GetLogger<T>()
        {
            return GetLogger(typeof (T));
        }
    }
}