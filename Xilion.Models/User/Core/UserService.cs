using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Documents;
using WebMatrix.WebData;
using Xilion.Models.User.Data;
using Microsoft.AspNetCore.Http;
using Xilion.Models.Classifications;

namespace Xilion.Models.User.Core
{
    /// <summary>
    ///   Represent service working with <see cref="User" />
    /// </summary>
    public class UserService : CmsService<Users>
    {
        private static bool _initialized;
        private readonly IUserRepository _userRepository;
        private static IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : base(userRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
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
            return _userRepository.Query().SingleOrDefault(x => x.UserName == _httpContextAccessor.HttpContext.User.Identity.Name);
        }

        /// <summary>
        ///   Gets user by account username.
        /// </summary>
        /// <param name="username"> </param>
        /// <returns> </returns>
        public Users Get(string username)
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == username);
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

        //[System.Obsolete]
        //public IQueryable<Users> GetByLabel(Label label)
        //{
        //    return _userRepository.Query()
        //        .Where(x => x.Labels.Contains(label));
        //}
    }
}