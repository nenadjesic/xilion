using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Search;
using NHibernate;
using NHibernate.Envers;
using NHibernate.Envers.Exceptions;
using NHibernate.Linq;
using NHibernate.Search;
using Xilion.Framework.Domain;
using Xilion.Framework.Logging;
using Query =Xilion.Framework.Queries.Query;

namespace Xilion.Framework.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        // ReSharper disable StaticFieldInGenericType
        private static readonly ILogger _logger = LogManager.GetLogger<Repository<T>>();
        // ReSharper restore StaticFieldInGenericType

        private readonly ISessionBuilder _sessionBuilder;

        /// <summary>
        /// Creates a new instance of repository initialized with session builder object.
        /// </summary>
        /// <param name="sessionBuilder"></param>
        protected Repository(ISessionBuilder sessionBuilder)
        {
            _sessionBuilder = sessionBuilder;
        }

        #region IRepository<T> Members

        /// <summary>
        /// Delete the entity from the data source.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        public void Delete(T entity)
        {
            Guard.IsNotNull(entity, "entity");

            _logger.DebugFormat("Deleting entity '{0}' of type '{1}'", entity.Id, typeof (T));

            ISession session = GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                OnDeleted(entity.Id, session);
                transaction.Commit();

                CacheDependency.Current.Notify(typeof(T));
            }
        }

        /// <summary>
        /// Delete the entity with the given id from the data source.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete.</param>
        public void Delete(object entityId)
        {
            Guard.IsNotNull(entityId, "entityId");

            _logger.DebugFormat("Deleting entity '{0}' of type '{1}'", entityId, typeof (T));

            ISession session = GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(String.Format("from {0} entity where entity.id = '{1}'", typeof (T).Name, entityId));
                OnDeleted(entityId, session);
                transaction.Commit();
                CacheDependency.Current.Notify(typeof(T));
            }
        }

        /// <summary>
        /// Gets the list of all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        public virtual IQueryable<T> GetAll()
        {
            _logger.DebugFormat("Getting all entities of type: {0}.", typeof (T));

            return Query();
        }

        /// <summary>
        /// Gets the entity with the given id.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <returns>An entity with the given id.</returns>
        public T GetById(long id)
        {
            Guard.IsNotNull(id, "id");

            _logger.DebugFormat("Getting a '{0}' entity with id '{1}'.", typeof (T), id);

            return GetSession().Get<T>(id);
        }

        /// <summary>
        /// Gets the last revision of an entity with the given id.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <returns>An entity with the given id.</returns>
        public T GetPreviousRevision(long id)
        {
            Guard.IsNotNull(id, "id");

            long lastRevisionNumber;

            try
            {
                lastRevisionNumber = GetSession().Auditer()
                    .GetRevisionNumberForDate(DateTime.Today.AddDays(1));
            }
            catch (RevisionDoesNotExistException ex)
            {
                _logger.Warn(ex, "There are no revisions available.");
                lastRevisionNumber = 0;
            }

            if (lastRevisionNumber <= 1) return null;

            return GetRevision(id, lastRevisionNumber - 1);
        }

        /// <summary>
        /// Gets the given revision of an entity with the given id.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <param name="revisionNumber">Revision number to find.</param>
        /// <returns>An entity with the given id.</returns>
        public T GetRevision(long id, long revisionNumber)
        {
            Guard.IsNotNull(id, "id");
            return GetSession().Auditer()
                .Find<T>(id, revisionNumber);
        }

        /// <summary>
        /// Gets the unfiltered LINQ query result ready for other LINQ operations.
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> result.</returns>
        public virtual IQueryable<T> Query()
        {
            return Query<T>();
        }

        /// <summary>
        /// Saves the given entity to the data source.
        /// </summary>
        /// <param name="entity">Entity to save.</param>
        public virtual void Save(T entity)
        {
            Guard.IsNotNull(entity, "entity");

            _logger.DebugFormat("Saving a '{0}' entity with id '{1}'.", typeof (T), entity.Id);

            ISession session = GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                if (entity.IsPersistent)
                    OnSaving(entity, session);

                session.SaveOrUpdate(entity);

               transaction.Commit();

                 CacheDependency.Current.Notify(typeof(T));
            }
        }

        /// <summary>
        /// Performs the search operation using the given query.
        /// </summary>
        /// <param name="query">Query to perform the search with.</param>
        /// <returns>A list of all entities matching the given query.</returns>
        public IEnumerable<T> Search(Query query)
        {
            Guard.IsNotNull(query, "query");

            _logger.DebugFormat("Searching for entities of type: {0}.", typeof (T));

            var session = GetSession() as IFullTextSession;

            if (session == null)
            {
                _logger.Error("Full text search isn't enabled.");
                throw new Exception("Full text search isn't enabled.");
            }

            string queryText = query.GetOrCreateQuery();
            if (String.IsNullOrWhiteSpace(queryText))
            {
                if (query.IsEmpty())
                    return new T[] {};

                queryText = "createdon:2*";
            }
            else if (!queryText.Contains("createdon:") && query.HasNegateProperty())
                queryText = "createdon:2* AND " + queryText;                                      /*POSLIJE 2* DODATI AND */

            IFullTextQuery fullTextQuery = session.CreateFullTextQuery<T>(queryText);

            if (query.Sorting != null && query.Sorting.OrderByProperty != null)
            {
                _logger.DebugFormat(
                    "Sorting search results by {0} {1}.", query.Sorting.OrderByProperty, query.Sorting.SortOrder);
                fullTextQuery.SetSort(GetLuceneSort(query.Sorting));
            }
            if (query.Paging != null)
            {
                IFullTextQuery pagingQuery = session.CreateFullTextQuery<T>(queryText);
                query.Paging.TotalCount = pagingQuery.ResultSize;

                _logger.DebugFormat(
                    "Paging search results: page {0} of total items {1}.",
                    query.Paging.PageNumber, query.Paging.TotalCount);
                fullTextQuery.SetFirstResult(query.Paging.StartIndex).SetMaxResults(query.Paging.PageSize);
            }

            return fullTextQuery.List().Cast<T>();
        }

        #endregion

        /// <summary>
        /// Gets the unfiltered LINQ query result ready for other LINQ operations.
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> result.</returns>
        public virtual IQueryable<TU> Query<TU>()
        {
            return GetSession().Query<TU>();
        }

        /// <summary>
        /// Gets the session from session builder.
        /// </summary>
        /// <returns>An <see cref="ISession"/> instance.</returns>
        protected ISession GetSession()
        {
            return _sessionBuilder.GetSession();
        }

        /// <summary>
        /// Executes before the actual save operation allowing inherited classes to save some other data.
        /// </summary>
        /// <param name="entity">Entity being saved.</param>
        /// <param name="session">Current <c>NHibernate</c> session.</param>
        protected virtual void OnSaving(T entity, ISession session)
        {
        }

        /// <summary>
        /// Executes after the actual delete operation allowing inherited classes to delete some other data.
        /// </summary>
        /// <param name="entityId">Entity being saved.</param>
        /// <param name="session">Current <c>NHibernate</c> session.</param>
        protected virtual void OnDeleted(object entityId, ISession session)
        {
        }

        /// <summary>
        /// Creates a Lucene Sort object from a SortingInfo object.
        /// </summary>
        /// <param name="sorting">SortingInfo instance to convert to Lucene Sort object.</param>
        /// <returns>An instance of Lucene Sort object.</returns>
        protected virtual Sort GetLuceneSort(SortingInfo sorting)
        {
            return new Sort(new SortField(sorting.OrderByProperty.ToLowerInvariant(), 3, !sorting.IsAscending));
        }
    }
}