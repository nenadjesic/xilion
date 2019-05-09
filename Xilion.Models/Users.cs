﻿using Xilion.Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilion.Models.Media.Images;
using Xilion.Models.Classifications;
using Xilion.Models;
using NHibernate.Envers.Configuration.Attributes;
using NHibernate.Search.Attributes;

namespace Xilion.Models
{
    [Table("Users")]
    [Audited]
    [Indexed]
    public class Users : TrackableEntity
    {
        public virtual  string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual string FullName { get; set; }

        public virtual bool Status { get; set; }

        public virtual bool Deactived { get; set; }

        public virtual ImageItem Avatar { get; set; }
    }
}
