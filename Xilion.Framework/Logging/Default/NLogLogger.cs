using System;
using NLog;

namespace Xilion.Framework.Logging.Default
{
    /// <summary>
    /// <see cref="NLog"/> implementation of <see cref="ILogger"/>.
    /// </summary>
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        /// <summary>
        /// Creates a new instance of <see cref="NLogLogger"/> class.
        /// </summary>
        /// <param name="logger"></param>
        public NLogLogger(Logger logger)
        {
            _logger = logger;
        }

        #region ILogger Members

        /// <summary>
        /// Checks if debug level logging is enabled for this logger.
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        /// <summary>
        /// Checks if error level logging is enabled for this logger.
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        /// <summary>
        /// Checks if fatal level logging is enabled for this logger.
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        /// <summary>
        /// Checks if info level logging is enabled for this logger.
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        /// <summary>
        /// Checks if trace level logging is enabled for this logger.
        /// </summary>
        public bool IsTraceEnabled
        {
            get { return _logger.IsTraceEnabled; }
        }

        /// <summary>
        /// Checks if warn level logging is enabled for this logger.
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        /// <summary>
        /// Log a message object with the debug level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Log a message object with the debug level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        public void Debug(Exception exception, object message)
        {
            _logger.DebugException(message.ToString(), exception);
        }

        /// <summary>
        /// Log a formatted message with the debug level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public void DebugFormat(
            Exception exception, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            if (exception == null)
                _logger.Debug(formatProvider, format, arguments);
            else
                _logger.DebugException(String.Format(formatProvider, format, arguments), exception);
        }

        /// <summary>
        /// Log a message object with the error level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Error(object message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Log a message object with the error level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        public void Error(Exception exception, object message)
        {
            _logger.ErrorException(message.ToString(), exception);
        }

        /// <summary>
        /// Log a formatted message with the error level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public void ErrorFormat(
            Exception exception, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            if (exception == null)
                _logger.Error(formatProvider, format, arguments);
            else
                _logger.ErrorException(String.Format(formatProvider, format, arguments), exception);
        }

        /// <summary>
        /// Log a message object with the fatal level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        /// <summary>
        /// Log a message object with the fatal level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        public void Fatal(Exception exception, object message)
        {
            _logger.FatalException(message.ToString(), exception);
        }

        /// <summary>
        /// Log a formatted message with the fatal level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public void FatalFormat(
            Exception exception, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            if (exception == null)
                _logger.Fatal(formatProvider, format, arguments);
            else
                _logger.Fatal(String.Format(formatProvider, format, arguments), exception);
        }

        /// <summary>
        /// Log a message object with the info level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Info(object message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Log a message object with the info level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        public void Info(Exception exception, object message)
        {
            _logger.InfoException(message.ToString(), exception);
        }

        /// <summary>
        /// Log a formatted message with the info level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public void InfoFormat(
            Exception exception, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            if (exception == null)
                _logger.Info(formatProvider, format, arguments);
            else
                _logger.InfoException(String.Format(formatProvider, format, arguments), exception);
        }

        /// <summary>
        /// Log a message object with the trace level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Trace(object message)
        {
            _logger.Trace(message);
        }

        /// <summary>
        /// Log a message object with the trace level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        public void Trace(Exception exception, object message)
        {
            _logger.TraceException(message.ToString(), exception);
        }

        /// <summary>
        /// Log a formatted message with the trace level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public void TraceFormat(
            Exception exception, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            if (exception == null)
                _logger.Trace(formatProvider, format, arguments);
            else
                _logger.Trace(String.Format(formatProvider, format, arguments), exception);
        }

        /// <summary>
        /// Log a message object with the warn level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Log a message object with the warn level including the exception passed as a parameter.
        /// </summary>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="message">The message object to log.</param>
        public void Warn(Exception exception, object message)
        {
            _logger.Warn(message.ToString(), exception);
        }

        /// <summary>
        /// Log a formatted message with the warn level including the exception passed as a parameter.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">The format of the message object to log. <see cref="string.Format(string,object[])"/>
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <param name="arguments">The list of format arguments.</param>
        public void WarnFormat(
            Exception exception, IFormatProvider formatProvider, string format, params object[] arguments)
        {
            if (exception == null)
                _logger.Warn(formatProvider, format, arguments);
            else
                _logger.Warn(String.Format(formatProvider, format, arguments), exception);
        }

        #endregion
    }
}