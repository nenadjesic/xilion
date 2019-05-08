using System;
using NHibernate;
using Xilion.Framework.Logging;
using ILoggerFactory = NHibernate.ILoggerFactory;

namespace Xilion.Framework.Data.Logging
{
    public class NHibernateLoggerFactory : ILoggerFactory
    {
        #region ILoggerFactory Members

        public IInternalLogger LoggerFor(string keyName)
        {
            return new NHibernateLogger(LogManager.GetLogger(keyName));
        }

        public IInternalLogger LoggerFor(Type type)
        {
            return new NHibernateLogger(LogManager.GetLogger(type));
        }

        #endregion
    }
}