using Xilion.Framework.Data.Repositories;
using Xilion.Framework.Domain;

namespace Xilion.Models.Core.Data.Repositories
{
    public interface IAliasedEntityRepository<T> : IRepository<T> where T : Entity
    {
        string GenerateAlias(string input);
        T GetByAlias(string alias);
    }
}