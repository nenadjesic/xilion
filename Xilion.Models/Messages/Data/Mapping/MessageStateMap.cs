using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Messages.Domain;

namespace Xilion.Models.Messages.Data.Mapping
{
    public class MessageStateMap : CmsEntityMap<MessageState>
    {
        public MessageStateMap()
        {
            Map(x => x.IsRead);
            Map(x => x.ReadDate);

            References(x => x.Message);
            References(x => x.Member);
        }
    }
}
