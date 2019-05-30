using Xilion.Models.User.Data;

namespace Xilion.Models.User.Core
{
    public interface IUserService : IUserRepository
    {
        UserSettings Settings { get; }

        Users Current();
        Users GetAuth(string username, string password);
        void Save(Users user, string email, string password);
    }
}