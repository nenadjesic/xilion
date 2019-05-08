using System;

namespace Xilion.Framework.Logging
{
    /// <summary>
    /// Represents an application logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Checks if debug level logging is enabled for this logger.
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Checks if error level logging is enabled for this logger.
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Checks if fatal level logging is enabled for this logger.
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        /// Checks if info level logging is enabled for this logger.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Checks if trace level logging is enabled for this logger.
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Checks if warn level logging is enabled for this logger.
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Log a message object with the debug level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Debug(object message);

        /// <summary>
        /// Log a message object with the debug level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        void Debug(Exception exception, object message);

        /// <summary>
        /// Log a formatted message with the debug level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] arguments);

        /// <summary>
        /// Log a message object with the error level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Error(object message);

        /// <summary>
        /// Log a message object with the error level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        void Error(Exception exception, object message);

        /// <summary>
        /// Log a formatted message with the error level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] arguments);

        /// <summary>
        /// Log a message object with the fatal level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Fatal(object message);

        /// <summary>
        /// Log a message object with the fatal level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        void Fatal(Exception exception, object message);

        /// <summary>
        /// Log a formatted message with the fatal level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] arguments);

        /// <summary>
        /// Log a message object with the info level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Info(object message);

        /// <summary>
        /// Log a message object with the info level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        void Info(Exception exception, object message);

        /// <summary>
        /// Log a formatted message with the info level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] arguments);

        /// <summary>
        /// Log a message object with the trace level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Trace(object message);

        /// <summary>
        /// Log a message object with the trace level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        void Trace(Exception exception, object message);

        /// <summary>
        /// Log a formatted message with the trace level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        void TraceFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] arguments);

        /// <summary>
        /// Log a message object with the warn level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Warn(object message);

        /// <summary>
        /// Log a message object with the warn level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        void Warn(Exception exception, object message);

        /// <summary>
        /// Log a formatted message with the warn level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] arguments);
    }
}