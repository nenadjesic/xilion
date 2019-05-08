using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Services
{
    public abstract class AliasedEntityService<T> : CmsService<T> where T : AliasedEntity
    {
        private readonly IAliasedEntityRepository<T> _aliasedEntityRepository;

        protected AliasedEntityService(IAliasedEntityRepository<T> aliasedEntityRepository)
            : base(aliasedEntityRepository)
        {
            _aliasedEntityRepository = aliasedEntityRepository;

        }

        public virtual string GenerateAlias(string input)
        {
            return _aliasedEntityRepository.GenerateAlias(input);
        }

        public virtual T GetByAlias(string alias)
        {
            return _aliasedEntityRepository.GetByAlias(alias);
        }
    }
}