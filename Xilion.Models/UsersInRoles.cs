using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilion.Framework.Domain;

namespace Xilion.Models
{
    [Table("UsersInRoles")]
    public class UsersInRoles : TrackableEntity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
