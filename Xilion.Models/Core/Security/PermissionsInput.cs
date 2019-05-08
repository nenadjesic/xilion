using System;
using System.Collections.Generic;

namespace Xilion.Models.Core.Security
{
    public class PermissionsInput
    {
        public string Role { get; set; }
        public long? SecuredId { get; set; }
        private IList<PermissionInput> _permissions = new List<PermissionInput>();
        public IList<PermissionInput> Permissions 
        {
            get { return _permissions; }
            set { _permissions = value; }
        }

        private IEnumerable<string> _roles = new List<string>();
        public IEnumerable<string> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
    }
}