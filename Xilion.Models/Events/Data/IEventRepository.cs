using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Events;

namespace Xilion.Models.Events.Data
{
    public interface IEventRepository : IAliasedEntityRepository<Event>
    {

    }
}