using System.Collections.Generic;
using System.Linq;

namespace Xilion.Models.User.Core
{
    public interface IUserService
    {
        UserSettings Settings { get; }

        Users Current();
        bool DeleteUser(Users entity);
        IList<Users> GetAll();
        Users GetAuth(string username, string password);
        Users GetById(int userId);
        Users GetCurrent(string username);
        IQueryable<Users> GetUsers();
        void Save(Users user);
    }
}