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
using Xilion.Models.Roles.Core;
using Xilion.ViewModels;

namespace Xilion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateRoleController : ControllerBase
    {
        private readonly IRoleService _roleServices;

        public CreateRoleController(IRoleService roleServices)
        {
            _roleServices = roleServices;
        }

        // GET: api/CreateRole
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            try
            {
                return _roleServices.GetRoles();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: api/CreateRole/5
        [HttpGet("{id}", Name = "GetRole")]
        public Role Get(int id)
        {
            try
            {
                return _roleServices.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: api/CreateRole
        [HttpPost]
        public HttpResponseMessage Post([FromBody] RoleViewModel roleViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (_roleServices.CheckRoleExits(roleViewModel.RoleName) != null)
                    {
                        var response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.Conflict
                        };

                        return response;
                    }
                    else
                    {
                        var temprole = AutoMapper.Mapper.Map<Role>(roleViewModel);

                        _roleServices.Save(temprole);

                        var response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK
                        };

                        return response;
                    }
                }
                else
                {
                    var response = new HttpResponseMessage()
                    {

                        StatusCode = HttpStatusCode.BadRequest
                    };

                    return response;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/CreateRole/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                var temprole = AutoMapper.Mapper.Map<Role>(roleViewModel);
                _roleServices.Save(temprole);

                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };

                return response;
            }
            catch (Exception)
            {
                var response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.BadRequest
                };

                return response;

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(Role role)
        {
            try
            {

                var result = _roleServices.DeleteRole(role);

                if (result)
                {
                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                    return response;
                }
                else
                {
                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
