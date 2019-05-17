using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Metadata;
using NHibernate.Stat;

namespace Xilon.Framework.Data.Search
{
    public class SessionFactorySearchWrapper : ISessionFactory
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionFactorySearchWrapper(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISessionFactory InternalFactory
        {
            get { return _sessionFactory; }
        }

        #region ISessionFactory Members

        public ISession OpenSession()
        {
            ISession session = _sessionFactory.OpenSession();
            return session;
        }

        [Obsolete]
        public ISession OpenSession(DbConnection conn)
        {
            ISession session = _sessionFactory.OpenSession(conn);
            return session;
        }

        [Obsolete]
        public ISession OpenSession(IInterceptor sessionLocalInterceptor)
        {
            ISession session = _sessionFactory.OpenSession(sessionLocalInterceptor);
            return session;
        }

        [Obsolete]
        public ISession OpenSession(DbConnection conn, IInterceptor sessionLocalInterceptor)
        {
            ISession session = _sessionFactory.OpenSession(conn, sessionLocalInterceptor);
            return session;
        }

        public void Dispose()
        {
            _sessionFactory.Dispose();
        }

        public IClassMetadata GetClassMetadata(Type persistentClass)
        {
            return _sessionFactory.GetClassMetadata(persistentClass);
        }

        public IClassMetadata GetClassMetadata(string entityName)
        {
            return _sessionFactory.GetClassMetadata(entityName);
        }

        public ICollectionMetadata GetCollectionMetadata(string roleName)
        {
            return _sessionFactory.GetCollectionMetadata(roleName);
        }

        public IDictionary<string, IClassMetadata> GetAllClassMetadata()
        {
            return _sessionFactory.GetAllClassMetadata();
        }

        public IDictionary<string, ICollectionMetadata> GetAllCollectionMetadata()
        {
            return _sessionFactory.GetAllCollectionMetadata();
        }

        public void Close()
        {
            _sessionFactory.Close();
        }

        public void Evict(Type persistentClass)
        {
            _sessionFactory.Evict(persistentClass);
        }

        public void Evict(Type persistentClass, object id)
        {
            _sessionFactory.Evict(persistentClass, id);
        }

        public void EvictEntity(string entityName)
        {
            _sessionFactory.EvictEntity(entityName);
        }

        public void EvictEntity(string entityName, object id)
        {
            _sessionFactory.EvictEntity(entityName, id);
        }

        public void EvictCollection(string roleName)
        {
            _sessionFactory.EvictCollection(roleName);
        }

        public void EvictCollection(string roleName, object id)
        {
            _sessionFactory.EvictCollection(roleName, id);
        }

        public void EvictQueries()
        {
            _sessionFactory.EvictQueries();
        }

        public void EvictQueries(string cacheRegion)
        {
            _sessionFactory.EvictQueries(cacheRegion);
        }

        public IStatelessSession OpenStatelessSession()
        {
            return _sessionFactory.OpenStatelessSession();
        }

        public IStatelessSession OpenStatelessSession(DbConnection connection)
        {
            return _sessionFactory.OpenStatelessSession(connection);
        }

        public FilterDefinition GetFilterDefinition(string filterName)
        {
            return _sessionFactory.GetFilterDefinition(filterName);
        }

        public ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        public IStatistics Statistics
        {
            get { return _sessionFactory.Statistics; }
        }

        public bool IsClosed
        {
            get { return _sessionFactory.IsClosed; }
        }

        public ICollection<string> DefinedFilterNames
        {
            get { return _sessionFactory.DefinedFilterNames; }
        }

        #endregion

        //private static ISession WrapSession(ISession session)
        //{
        //    return NHibernate.Search.Search.CreateFullTextSession(session);
        //}

        public Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.CloseAsync(cancellationToken);
        }

        public Task EvictAsync(Type persistentClass, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictAsync(persistentClass, cancellationToken);
        }

        public Task EvictAsync(Type persistentClass, object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictAsync(persistentClass, id, cancellationToken);
        }

        public Task EvictEntityAsync(string entityName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictEntityAsync(entityName, cancellationToken);
        }

        public Task EvictEntityAsync(string entityName, object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictEntityAsync(entityName, id, cancellationToken);
        }

        public Task EvictCollectionAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictCollectionAsync(roleName, cancellationToken);
        }

        public Task EvictCollectionAsync(string roleName, object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictCollectionAsync(roleName, id, cancellationToken);
        }

        public Task EvictQueriesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictQueriesAsync(cancellationToken);
        }

        public Task EvictQueriesAsync(string cacheRegion, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _sessionFactory.EvictQueriesAsync(cacheRegion, cancellationToken);
        }

        public NHibernate.ISessionBuilder WithOptions()
        {
            return _sessionFactory.WithOptions();
        }


        public IStatelessSessionBuilder WithStatelessOptions()
        {
            return _sessionFactory.WithStatelessOptions();
        }

    }
}