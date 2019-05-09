using Lucene.Net.Util;
using Xilion.Models.Core.Data.Mappings;
using Xilion.Framework.Data.Mappings.Conventions;
using Xilion.Models;

namespace Xilion.Models.User.Data.Mapping
{
    public class UserMap : CmsEntityMap<Users>
    {
        public UserMap()
        {
            Map(x => x.UserName);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.FullName);
            Map(x => x.Password);
            Map(x => x.Status);
            Map(x => x.Deactived);
            References(x => x.Avatar).Nullable();
        }
    }
}
