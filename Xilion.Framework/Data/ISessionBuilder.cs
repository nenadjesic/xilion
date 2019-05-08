using NHibernate;

namespace Xilion.Framework.Data
{
    /// <summary>
    /// Represents a builder for <c>NHibernate</c> session objects.
    /// </summary>
    public interface ISessionBuilder
    {
        /// <summary>
        /// Closes the current session object from http context. This should be called on end request.
        /// </summary>
        void CloseSession();

        /// <summary>
        /// Gets the new session object avoiding the one from http context.
        /// </summary>
        /// <returns><c>NHibernate</c> session object.</returns>
        ISession GetNewSession();

        /// <summary>
        /// Gets the session object from current http context, or creates a new one if none exists.
        /// </summary>
        /// <returns><c>NHibernate</c> session object.</returns>
        ISession GetSession();

        /// <summary>
        /// Gets the stateless session useful for batch inserts or updates.
        /// </summary>
        /// <returns><see cref="NHibernate"/> stateless session object.</returns>
        IStatelessSession GetStatelessSession();
    }
}