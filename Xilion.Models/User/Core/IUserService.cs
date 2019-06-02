using System.Collections.Generic;
using System.Linq;
using Xilion.Models.User.Data;

namespace Xilion.Models.User.Core
{
    public interface IUserService 
    {
        UserSettings Settings { get; }

        Users Current();

        Users GetAuth(string username, string password);

        Users GetCurrent(string username);

        Users GetById(int Id);

        void Save(Users user);

        List<Users> GetAll();

        void Delete(Users entity);
    }
}