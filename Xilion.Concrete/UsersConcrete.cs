﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilion.Interface;
using Xilion.Models;
using Xilion.Framework;
using Xilion.Models.User.Core;
using Xilion.ViewModels;
using Xilion.Models.User.Data;
using Xilion.Models.Roles.Core;

namespace Xilion.Concrete
{
    public class UsersConcrete 
    {

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UsersConcrete(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public bool CheckUsersExits(string username)
        {
            var result = _userService.GetCurrent(username);

            return result != null ? true : false;
        }

        public bool AuthenticateUsers(string username, string password)
        {
            var user = _userService.GetAuth(username, password);

            return user != null ? true : false;
        }

        public LoginResponse GetUserDetailsbyCredentials(string username, string password)
        {
            try
            {
                var result = (from user in _userService.GetAll()
                              join userinrole in _roleService.GetUserRoles() on user.Id equals userinrole.UserId
                              where user.UserName == username && user.Password == password

                              select new LoginResponse
                              {
                                  UserId = user.Id,
                                  RoleId = userinrole.RoleId,
                                  Status = user.Status,
                                  UserName = user.UserName
                                  
                              }).SingleOrDefault();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteUser(Users user)
        {
            _userService.DeleteUser(user);
            return true;            
        }

        public IList<Users> GetAllUsers()
        {
            var result = _userService.GetAll();

            return result;
        }

        public Users GetUserbyId(int userId)
        {
            var result = _userService.GetById(userId);
            return result;
        }

        public bool InsertUser(Users user)
        {
            _userService.Save(user);
            return true;
        }

        public bool UpdateUser(Users user)
        {
            _userService.Save(user);
            return true;
        }
    }
}
