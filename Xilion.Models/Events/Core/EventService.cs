using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core.Domain;
using Xilion.Models.Core.Services;
using Xilion.Models.Events;
using Xilion.Models.Events.Data;

namespace Xilion.Models.Events.Core
{
    public class EventService : CmsService<Event>
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
            : base(eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Gets all events by their status.
        /// </summary>
        public IQueryable<Event> GetAllByStatus(params WorkflowStatus[] statuses)
        {
            return _eventRepository.Query()
                .Where(x => statuses.Contains(x.Status));
        }


        /// <summary>
        /// Gets all incoming events.
        /// </summary>
        public IQueryable<Event> GetIncomingEvents()
        {
            return _eventRepository.Query()
                .Where(x => x.StartsOn > DateTime.Now.Date)
                .OrderByDescending(x => x.StartsOn);
        }

        /// <summary>
        /// Gets all events by their parent.
        /// </summary>
        public IList<Event> GetByParent(IHaveEvent parent)
        {
            return _eventRepository.Query().Where(x => x.Parent == parent).ToList();
        }

        public override void Save(Event entity)
        {
            entity.PublishedOn = DateTime.Now;
            entity.SessionId = entity.Title;
            entity.SessionToken = entity.Title;
            base.Save(entity);
        }
    }
}