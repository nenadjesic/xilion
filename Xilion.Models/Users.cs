using Xilion.Framework.Domain;
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

namespace Xilion.Models
{
    [Table("Users")]
    public class Users : TrackableEntity
    {
        //[Obsolete]
        //private IList<Label> _labels = new List<Label>();
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Status { get; set; }
        public bool Deactived { get; set; }

        //[Obsolete]
        //public virtual IList<Label> Labels
        //{
        //    get { return _labels; }
        //    set { _labels = value; }
        //}
        public ImageItem Avatar { get; set; }
    }

}
