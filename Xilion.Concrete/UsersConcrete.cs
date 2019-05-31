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

namespace Xilion.Concrete
{
    public class UsersConcrete : IUsers
    {

        private readonly DatabaseContext _context;
        private readonly IUserService _userService;
        public UsersConcrete(DatabaseContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public bool CheckUsersExits(string username)
        {
            var result = (from user in _context.Users
                          where user.UserName == username
                          select user).Count();

            return result > 0 ? true : false;
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
                var result = (from user in _context.Users
                    join userinrole in _context.UsersInRoles on user.Id equals userinrole.UserId
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


        public bool DeleteUsers(int userId)
        {
            var removeuser = (from user in _context.Users
                              where user.Id == userId
                              select user).FirstOrDefault();
            if (removeuser != null)
            {
                _context.Users.Remove(removeuser);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<Users> GetAllUsers()
        {
            var result = (from user in _context.Users
                          where user.Status == true
                          select user).ToList();

            return result;
        }

        public Users GetUsersbyId(int userId)
        {
            var result = (from user in _context.Users
                          where user.Id == userId
                          select user).FirstOrDefault();

            return result;
        }

        public bool InsertUsers(Users user)
        {
            _context.Users.Add(user);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateUsers(Users user)
        {
            _context.Entry(user).Property(x => x.Email).IsModified = true;
            _context.Entry(user).Property(x => x.Status).IsModified = true;
            _context.Entry(user).Property(x => x.FullName).IsModified = true;
            _context.Entry(user).Property(x => x.Password).IsModified = true;

            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
