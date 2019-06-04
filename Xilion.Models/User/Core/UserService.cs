using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Documents;
using WebMatrix.WebData;
using Xilion.Models.User.Data;
using Microsoft.AspNetCore.Http;
using Xilion.Models.Classifications;
using HttpContext = Xilion.Framework.Web.HttpContext;
using System.Collections.Generic;

namespace Xilion.Models.User.Core
{
    /// <summary>
    ///   Represent service working with <see cref="User" />
    /// </summary>
    public class UserService : CmsService<Users>, IUserService
    {
        private static bool _initialized;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }


        public UserSettings Settings
        {
            get { return (UserSettings)CmsContext.Current.GetApplication<UserApplication>().GetSettings(); }
        }

        public Users Current()
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
        }

        public Users GetAuth(string username, string password)
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == username && x.Password == password);
        }

        public Users GetCurrent(string username)
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == username);
        }

        public Users GetById(int userId)
        {
            return _userRepository.Query().SingleOrDefault(x => x.Id == userId);
        }

        public bool DeleteUser(Users entity)
        {
            if (entity.IsPersistent)
                return false;
            base.Delete(entity);
            return true;
        }

        public IQueryable<Users> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public override IList<Users> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public override void Save(Users user)
        {
            Save(user);
        }
    }
}