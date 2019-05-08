namespace Xilion.Framework.Logging
{
    /// <summary>
    /// Provides a factory methods for creating new logger objects. Don't use this interface directly.  Use 
    /// <see cref="LogManager"/> instead. Only developers creating their own <see cref="ILogger"/> implementations
    /// should implement it.
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Gets the logger with the given name.
        /// </summary>
        /// <param name="name">Name of the logger.</param>
        /// <returns>A new instance of the logger with the given name.</returns>
        ILogger GetLogger(string name);
    }
}