using System.Collections.Generic;
using System.Linq;
using Xilion.Framework.Domain;
using Xilion.Framework.Queries;

namespace Xilion.Framework.Data.Repositories
{
    /// <summary>
    /// Base repository interface for entities with custom ID type.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Delete the entity from the data source.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Delete the entity with the given id from the data source.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete.</param>
        void Delete(object entityId);

        /// <summary>
        /// Gets the list of all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets the entity with the given id.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <returns>An entity with the given id.</returns>
        T GetById(long id);

 
        /// <summary>
        /// Gets the last revision of an entity with the given id.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <returns>An entity with the given id.</returns>
        T GetPreviousRevision(long id);

        /// <summary>
        /// Gets the given revision of an entity with the given id.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <param name="revisionNumber">Revision number to find.</param>
        /// <returns>An entity with the given id.</returns>
        T GetRevision(long id, long revisionNumber);

        /// <summary>
        /// Gets the unfiltered LINQ query result ready for other LINQ operations.
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> result.</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Saves the given entity to the data source.
        /// </summary>
        /// <param name="entity">Entity to save.</param>
        void Save(T entity);

        /// <summary>
        /// Performs the search operation using the given query.
        /// </summary>
        /// <param name="query">Query to perform the search with.</param>
        /// <returns>A list of all entities matching the given query.</returns>
        IEnumerable<T> Search(Query query);
    }
}