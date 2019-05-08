using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Messages.Domain;

namespace Xilion.Models.Messages.Data.Mapping
{
    public class AttachmentMap : CmsEntityMap<Attachment>
    {
        public AttachmentMap()
        {
           // Map(x => x.Provider);
            Map(x => x.ProviderKey);

            References(x => x.Message);
        }
    }
}
