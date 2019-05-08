using Xilion.Models.Core.Services;
using Xilion.Models.Events;
using Xilion.Models.Events.Data;

namespace Xilion.Models.Events.Core
{
    public class EventTeamService : CmsService<EventTeam>
    {
        private readonly IEventTeamRepository _eventTeamRepository;

        public EventTeamService(IEventTeamRepository eventTeamRepository)
            : base(eventTeamRepository)
        {
            _eventTeamRepository = eventTeamRepository;
        }
    }
}