using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xilion.Interface;
using Xilion.Models;

namespace Xilion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveRoleController : ControllerBase
    {

        private readonly IUsersInRoles _usersInRoles;
        public RemoveRoleController(IUsersInRoles usersInRoles)
        {
            _usersInRoles = usersInRoles;
        }

    }
}
