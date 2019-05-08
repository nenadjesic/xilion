using System;
using System.Text.RegularExpressions;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Media.Data.Default
{
    public class MediaItemRepository<T> : Repository<T>, IMediaItemRepository<T> where T : MediaItem
    {
        public MediaItemRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        #region Implementation of IAliasedEntityRepository<T>

        public string GenerateAlias(string input)
        {
            throw new NotImplementedException();
        }

        public T GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IWorkflowEntityRepository<T>

        public void SetStatus(WorkflowStatus status, params long[] ids)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IMediaItemRepository<T>

        public string GenerateTitle(string input)
        {
            if (String.IsNullOrEmpty(input))
                return String.Empty;

            string result = input.ToLowerInvariant()
                .Replace('_', ' ')
                .Replace('-', ' ');
            result = Regex.Replace(result, @"\s+", " ");
            return result;
        }

        #endregion
    }
}