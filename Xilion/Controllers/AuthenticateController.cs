using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xilion.Common;
using Xilion.Interface;
using Xilion.Models;
using Xilion.ViewModels;
using Xilion.Framework;
using Xilion.Models.User.Core;
using Xilion.Models.Roles.Core;

namespace Xilion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _users;
        private readonly IRoleService _role;
        public AuthenticateController(IOptions<AppSettings> appSettings, IUserService users, IRoleService role)
        {
            _users = users;
            _appSettings = appSettings.Value;
            _role = role;
        }
    
        // POST: api/Authenticate
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequestViewModel value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginstatus = _users.GetAuth(value.UserName, EncryptionLibrary.EncryptText(value.Password));

                    if (loginstatus !=null)
                    {
                        var userdetails = _users.GetCurrent(value.UserName);
                        //var roledetaočs = _r
                        if (userdetails != null)
                        {
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                new Claim(ClaimTypes.Name, userdetails.Id.ToString())
                                }),
                                Expires = DateTime.UtcNow.AddDays(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            value.Token = tokenHandler.WriteToken(token);

                            // remove password before returning
                            value.Password = null;
                            value.Usertype = 1;

                            return Ok(value);
                        }
                        else
                        {
                            value.Password = null;
                            value.Usertype = 0;
                            return Ok(value);
                        }
                    }
                    value.Password = null;
                    value.Usertype = 0;
                    return Ok(value);
                }
                value.Password = null;
                value.Usertype = 0;
                return Ok(value);
            }
            catch (Exception)
            {

                throw;
            }
        }
 
    }
}
