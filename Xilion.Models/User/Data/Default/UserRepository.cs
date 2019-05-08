using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.User.Data.Default
{
    public class UserRepository: Repository<Users>, IUserRepository
    {
        public UserRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
