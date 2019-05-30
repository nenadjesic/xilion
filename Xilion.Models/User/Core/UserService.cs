using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Documents;
using WebMatrix.WebData;
using Xilion.Models.User.Data;
using Microsoft.AspNetCore.Http;
using Xilion.Models.Classifications;
using HttpContext = Xilion.Framework.Web.HttpContext;

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

        /// <summary>
        ///   Gets current loged user.
        /// </summary>
        /// <returns> </returns>
        public Users Current()
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
        }

        /// <summary>
        ///   Gets user by account username.
        /// </summary>
        /// <param name="username"> </param>
        /// <returns> </returns>
        public Users GetAuth(string username, string password)
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == username && x.Password == password);
        }

        /// <summary>
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="email"> </param>
        /// <param name="password"> </param>
        public void Save(Users user, string email, string password)
        {
            Save(user);
            WebSecurity.CreateAccount(user.UserName, password);
        }
    }
}