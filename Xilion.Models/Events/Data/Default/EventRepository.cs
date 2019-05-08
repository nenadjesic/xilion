using Xilion.Models.Core.Data.Repositories.Default;
using Xilion.Framework.Data;
using Xilion.Models.Events;

namespace Xilion.Models.Events.Data.Default
{
    public class EventRepository : MetaDataEntityRepository<Event>
    {
        /// <summary>
        /// Creates a new instance of repository initialized with session builder object.
        /// </summary>
        /// <param name = "sessionBuilder"></param>
        public EventRepository(ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {
        }

        public string GenerateAlias(string input)
        {
            throw new System.NotImplementedException();
        }

        public Event GetByAlias(string alias)
        {
            throw new System.NotImplementedException();
        }
    }
}