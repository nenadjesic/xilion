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
    [Table("Role")]
    public class Role : TrackableEntity
    {
        [Required(ErrorMessage = "Enter Role name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public virtual string RoleName { get; set; }

        public virtual bool Status { get; set; }
    }
}
