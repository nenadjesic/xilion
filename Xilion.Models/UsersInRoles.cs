using NHibernate.Envers.Configuration.Attributes;
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

    public class UsersInRoles: TrackableEntity
    {
        public virtual Users User { get; set; }
        public virtual Role Role { get; set; }
    }
}
