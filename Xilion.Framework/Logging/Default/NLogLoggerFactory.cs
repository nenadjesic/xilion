namespace Xilion.Framework.Logging.Default
{
    /// <summary>
    /// <see cref="NLog"/> implementation of <see cref="ILoggerFactory"/>. 
    /// </summary>
    public class NLogLoggerFactory : ILoggerFactory
    {
        #region ILoggerFactory Members

        /// <summary>
        /// Gets the logger with the given name.
        /// </summary>
        /// <param name="name">Name of the logger.</param>
        /// <returns>A new instance of the logger with the given name.</returns>
        public ILogger GetLogger(string name)
        {
            return new NLogLogger(NLog.LogManager.GetLogger(name));
        }

        #endregion
    }
}