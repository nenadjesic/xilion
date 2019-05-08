using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xilion.Framework.Data;
using NHibernate;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Data.Repositories.Default
{
    public abstract class AliasedEntityRepository<T>
        : MetaDataEntityRepository<T>, IAliasedEntityRepository<T> where T : AliasedEntity
    {
        /// <summary>
        /// Creates a new instance of repository initialized with session builder object.
        /// </summary>
        /// <param name="sessionBuilder"></param>
        protected AliasedEntityRepository(Xilion.Framework.Data.ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        #region IAliasedEntityRepository<T> Members

        public virtual T GetByAlias(string alias)
        {
            IQueryable<T> query = Query().Where(x => x.Alias.ToLower() == alias.ToLower());

            return query.FirstOrDefault();
        }

        public string GenerateAlias(string input)
        {
            string startingValue = GenerateAliasString(input);
            if (String.IsNullOrEmpty(startingValue))
                return String.Empty;

            string alias = startingValue;
            int count = 1;

            while (IsAliasTaken(alias))
            {
                alias = String.Concat(startingValue, "-", count);
                count++;
            }

            return alias;
        }

        #endregion

        protected override void OnSaving(T entity, ISession session)
        {
            if (Query().Any(x => x.Alias == entity.Alias && x.Id != entity.Id))
                entity.Alias = GenerateAlias(entity.Alias);

            base.OnSaving(entity, session);
        }

        public virtual bool IsAliasTaken(string alias)
        {
            return String.IsNullOrWhiteSpace(alias) || Query<T>().Any(x => x.Alias == alias);
        }

        private static string GenerateAliasString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return String.Empty;

            string result = input.ToLowerInvariant()
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
    }
}