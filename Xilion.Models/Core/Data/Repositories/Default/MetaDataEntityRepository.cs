using System.Linq;
using Lucene.Net.Search;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;
using FluentNHibernate.Data;

namespace Xilion.Models.Core.Data.Repositories.Default
{
    public abstract class MetaDataEntityRepository<T> : Repository<T> where T : MetaDataEntity
    {
        protected MetaDataEntityRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        //protected override Sort GetLuceneSort(SortingInfo sorting)
        //{
        //    string propertyName = sorting.OrderByProperty;

        //    MetaDataPropertyDefinition metaDataProperty =
        //        CmsContext.Current.GetMetaDataFor<Entity>().SingleOrDefault(x => x.Name == propertyName);
        //    if (metaDataProperty == null)
        //        return base.GetLuceneSort(sorting);

        //    if (metaDataProperty.IsLocalized)
        //        propertyName = metaDataProperty.GetFullName();

        //    // If property is both analyzed and stored, we search in stored field
        //    if (metaDataProperty.IsAnalyzed && metaDataProperty.IsStored)
        //        propertyName += MetaDataPropertyDefinition.StoredSuffix;

        //    return new Sort(new SortField(propertyName.ToLowerInvariant(), 3, !sorting.IsAscending));
        //}
    }
}