using System;

namespace Xilion.Framework.Logging
{
    public static class LogExtensions
    {
        /// <summary>
        /// Log a formatted message with the debug level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void DebugFormat(this ILogger logger, string format, params object[] arguments)
        {
            logger.DebugFormat(null, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the debug level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. 
        /// <see cref="string.Format(IFormatProvider,string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void DebugFormat(
            this ILogger logger, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            logger.DebugFormat(null, formatProvider, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the debug level including the exception passed as a parameter.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void DebugFormat(
            this ILogger logger, string format, Exception exception, params object[] arguments)
        {
            logger.DebugFormat(exception, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the error level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void ErrorFormat(this ILogger logger, string format, params object[] arguments)
        {
            logger.ErrorFormat(null, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the error level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. 
        /// <see cref="string.Format(IFormatProvider,string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void ErrorFormat(
            this ILogger logger, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            logger.ErrorFormat(null, formatProvider, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the error level including the exception passed as a parameter.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void ErrorFormat(
            this ILogger logger, string format, Exception exception, params object[] arguments)
        {
            logger.ErrorFormat(exception, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the fatal level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void FatalFormat(this ILogger logger, string format, params object[] arguments)
        {
            logger.FatalFormat(null, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the fatal level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. 
        /// <see cref="string.Format(IFormatProvider,string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void FatalFormat(
            this ILogger logger, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            logger.FatalFormat(null, formatProvider, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the fatal level including the exception passed as a parameter.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void FatalFormat(
            this ILogger logger, string format, Exception exception, params object[] arguments)
        {
            logger.FatalFormat(exception, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the info level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void InfoFormat(this ILogger logger, string format, params object[] arguments)
        {
            logger.InfoFormat(null, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the info level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. 
        /// <see cref="string.Format(IFormatProvider,string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void InfoFormat(
            this ILogger logger, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            logger.InfoFormat(null, formatProvider, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the info level including the exception passed as a parameter.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void InfoFormat(
            this ILogger logger, string format, Exception exception, params object[] arguments)
        {
            logger.InfoFormat(exception, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the trace level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void TraceFormat(this ILogger logger, string format, params object[] arguments)
        {
            logger.TraceFormat(null, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the trace level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. 
        /// <see cref="string.Format(IFormatProvider,string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void TraceFormat(
            this ILogger logger, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            logger.TraceFormat(null, formatProvider, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the trace level including the exception passed as a parameter.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void TraceFormat(
            this ILogger logger, string format, Exception exception, params object[] arguments)
        {
            logger.TraceFormat(exception, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the warn level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void WarnFormat(this ILogger logger, string format, params object[] arguments)
        {
            logger.WarnFormat(null, null, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the warn level.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. 
        /// <see cref="string.Format(IFormatProvider,string,object[])"/>
        /// </param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void WarnFormat(
            this ILogger logger, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            logger.WarnFormat(null, formatProvider, format, arguments);
        }

        /// <summary>
        /// Log a formatted message with the warn level including the exception passed as a parameter.
        /// </summary>
        /// <param name="logger">Logger object to log this message.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public static void WarnFormat(
            this ILogger logger, string format, Exception exception, params object[] arguments)
        {
            logger.WarnFormat(exception, null, format, arguments);
        }
    }
}