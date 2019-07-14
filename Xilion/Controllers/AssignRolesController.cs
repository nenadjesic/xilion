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
using Xilion.ViewModels;

namespace Xilion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignRolesController : ControllerBase
    {
        private readonly IUsersInRoles _usersInRoles;
        public AssignRolesController(IUsersInRoles usersInRoles)
        {
            _usersInRoles = usersInRoles;
        }


        // GET: api/UsersInRoles
        [HttpGet]
        public IEnumerable<AssignRolesViewModel> Get()
        {
            try
            {
                return _usersInRoles.GetAssignRoles();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
