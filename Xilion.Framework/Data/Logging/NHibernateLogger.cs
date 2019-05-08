using System;
using NHibernate;
using Xilion.Framework.Logging;

namespace Xilion.Framework.Data.Logging
{
    public class NHibernateLogger : IInternalLogger
    {
        private readonly ILogger _logger;

        public NHibernateLogger(ILogger logger)
        {
            _logger = logger;
        }

        #region IInternalLogger Members

        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void ErrorFormat(string format, params object[] arguments)
        {
            _logger.ErrorFormat(format, arguments);
        }

        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        public void DebugFormat(string format, params object[] arguments)
        {
            _logger.DebugFormat(format, arguments);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }

        public void InfoFormat(string format, params object[] arguments)
        {
            _logger.InfoFormat(format, arguments);
        }

        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        public void WarnFormat(string format, params object[] arguments)
        {
            _logger.WarnFormat(format, arguments);
        }

        public void Warn(object message, Exception exception)
        {
            _logger.Warn(exception, message);
        }

        public void Info(object message, Exception exception)
        {
            _logger.Info(exception, message);
        }

        public void Debug(object message, Exception exception)
        {
            _logger.Debug(exception, message);
        }

        public void Fatal(object message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }

        public void Error(object message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        #endregion
    }
}