using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;
using Xilion.Models.Events;

namespace Xilion.Models.Events.Data.Default
{
    public class EventTeamRepository : Repository<EventTeam>
    {
        public EventTeamRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}