using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xilion.ViewModels
{
    public class AssignRolesViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long UserRoleId { get; set; }
    }
}
