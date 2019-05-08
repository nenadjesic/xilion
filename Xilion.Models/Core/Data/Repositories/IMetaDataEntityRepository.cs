using Xilion.Models.Core.Domain;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Core.Data.Repositories
{
    public interface IMetaDataEntityRepository<T> : IRepository<T> where T : MetaDataEntity
    {
    }
}