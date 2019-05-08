using System.Collections.Generic;
using Xilion.Models;
using Xilion.ViewModels;

namespace Xilion.Interface
{
    public interface IUsers
    {
        bool InsertUsers(Users Users);
        bool CheckUsersExits(string Username);
        Users GetUsersbyId(int Usersid);
        bool DeleteUsers(int Usersid);
        bool UpdateUsers(Users role);
        List<Users> GetAllUsers();
        bool AuthenticateUsers(string Username, string password);
        LoginResponse GetUserDetailsbyCredentials(string Username, string password);
    }
}