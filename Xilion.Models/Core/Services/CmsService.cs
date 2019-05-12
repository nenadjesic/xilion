using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Xilion.Models.Core.Domain;
using Xilion.Models.Core.Exceptions;
using Xilion.Models.Core.Extensions;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data.Repositories;
using Xilion.Framework.Domain;
using Xilion.Framework.Extensions;
using Xilion.Framework.Queries;
using Xilion.Framework.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using HttpContext = Xilion.Framework.Web.HttpContext;

namespace Xilion.Models.Core.Services
{
    public abstract class CmsService<T> where T : Entity
    {
        private readonly IRepository<T> _repository;

        protected CmsService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        /// <summary>
        ///   Deletes multiple entities from persitance.
        /// </summary>
        /// <param name="ids"> List of entity identifiers to delete. </param>
        public virtual void Delete(params long[] ids)
        {
            foreach (var id in ids)
                _repository.Delete(id);
        }

        /// <summary>
        ///   Deletes multiple entities from persitance.
        /// </summary>
        /// <param name="entities"> List of entities. </param>
        public virtual void Delete(IList<Entity> entities)
        {
            foreach (var entity in entities)
                _repository.Delete(entity);
        }

        /// <summary>
        ///   Get list of all exsisting entities of type T.
        /// </summary>
        /// <returns> </returns>
        public virtual IList<T> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        /// <summary>
        ///   Gets entity by its unique identifier.
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public virtual T GetById(long id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        ///   Gets entity by its unique idenftifier for editing by marking it as a locked.
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public virtual T GetByIdForEditing(long id)
        {
            var entity = _repository.GetById(id);

            var lockable = entity as ILockable;
            if (lockable != null && !lockable.IsLocked())
            {
                lockable.Lock();
                Save(entity);
            }

            return entity;
        }

        /// <summary>
        ///   Gets entity by its unique alias. It will work only for entities implementing IAliase interface.
        /// </summary>
        /// <param name="alias"> </param>
        /// <returns> </returns>
        public virtual T GetByAlias(string alias)
        {
            if (!typeof (T).Implements<IAliased>()) return null;

            var query = _repository.Query().Where(x => ((IAliased) x).Alias.ToLower() == alias.ToLower());
            return query.FirstOrDefault();
        }

        /// <summary>
        ///   Gets a value indicates if alias is already taken.
        /// </summary>
        /// <param name="alias"> </param>
        /// <returns> </returns>
        public virtual bool IsAliasTaken(string alias)
        {
            return String.IsNullOrWhiteSpace(alias) || _repository.Query().Any(x => ((IAliased) x).Alias == alias);
        }

        /// <summary>
        ///   Saves entity to persistance. It will also unlock locked entity and set ITrackable properties.
        /// </summary>
        /// <param name="entity"> ENtity to save. </param>
        public virtual void Save(T entity)
        {
            // TODO: Uncomment this when all permissions are defined properly
            //if (!entity.IsPersistent && !IsUsersAllowed(entity, AccessRight.Create))
            //    throw new UnauthorizedAccessException(
            //        String.Format("You don't have necessary permissions for creating new {0}", typeof (T).Name));
            //if (entity.IsPersistent && !IsUsersAllowed(entity, AccessRight.Modify))
            //    throw new UnauthorizedAccessException(
            //        String.Format("You don't have necessary permissions for modifying {0}", typeof(T)));

            BeforeSave(entity);

            // Workflow
            var wf = entity as IHaveWorkflow;
            if (wf != null)
            {
                if (wf.Status == null)
                    wf.Status = WorkflowStatus.Draft;
                if (wf.PublishedOn < DateTime.Now.AddYears(-50))
                    wf.PublishedOn = DateTime.Now;
            }
            // Lockables
            var lockable = entity as ILockable;
            if (lockable != null) lockable.Unlock();

            // Trackables
            var trackable = entity as ITrackable;
            if (trackable != null)
            {
                if (String.IsNullOrEmpty(trackable.CreatedBy))
                    trackable.UpdatedBy =
                        trackable.CreatedBy =
                        String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name)
                            ? "system"
                            : HttpContext.Current.User.Identity.Name;

                if (trackable.CreatedOn < DateTime.Now.AddYears(-50))
                    trackable.UpdatedOn = trackable.CreatedOn = DateTime.Now;
            }

            _repository.Save(entity);
        }

        public virtual IEnumerable<T> Search(Query query)
        {
            // if TrackableQuery
            var trackableQuery = query as TrackableQuery;
            if(trackableQuery != null)
            {
                if (trackableQuery.Owner == "$")
                    trackableQuery.CreatedBy = HttpContext.Current.User.Identity.Name;
            }
            // if WorkflowQuery
            var workflowQuery = query as WorkflowQuery<T>;
            if(workflowQuery != null)
            {
                if (workflowQuery.Scheduled) workflowQuery.PublishedOnFrom = DateTime.Now;
            }



            return _repository.Search(query);
        }

        public virtual T Unlock(long id, bool save)
        {
            var entity = _repository.GetById(id);
            var lockable = entity as ILockable;

            if (lockable != null)
            {
                lockable.Unlock();
                if (save) _repository.Save(entity);
            }

            return entity;
        }

        /// <summary>
        ///   Generates unique alias.
        /// </summary>
        /// <param name="input"> </param>
        /// <returns> </returns>
        public string GenerateAlias(string input)
        {
            var startingValue = GenerateAliasString(input);

            string[] protectedWords = ConfigurationManager.AppSettings["ProtectedWords"].Split(';');

            foreach (var protectedWord in protectedWords)
            {
                if(startingValue.ToLowerInvariant().Equals(protectedWord.ToLowerInvariant()))
                {
                    throw new AliasException(String.Format("{0}  is protected word and can not be used", protectedWord));
                }
            }

            if (String.IsNullOrEmpty(startingValue))
                return String.Empty;

            var alias = startingValue;
            var count = 1;

            while (IsAliasTaken(alias))
            {
                alias = String.Concat(startingValue, "-", count);
                count++;
            }

            return alias;
        }

        protected virtual void BeforeSave(T entity)
        {
            // do something only if override
        }

        #region Private methods

        private static string GenerateAliasString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return String.Empty;

            var result = input.ToLowerInvariant()
                .Replace('č', 'c')
                .Replace('ć', 'c')
                .Replace('š', 's')
                .Replace('ž', 'z')
                .Replace('đ', 'd');
            result = Regex.Replace(result, @"[^a-z0-9\-]+", "-");
            result = Regex.Replace(result, @"-{2,}", "-");
            result = Regex.Replace(result, @"-$", "");

            return result;
        }

        #endregion
    }
}