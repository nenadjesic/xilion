using System;
using System.Collections.Generic;

using Xilion.Models.Classifications;
using Xilion.Models.Media.Images;
using Xilion.Models.Core.Data;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;

namespace Xilion.Models.Newsletters
{
  
    public class Newsletter : Entity, IHaveWorkflow 

    {

        /// <summary>
        ///   Gets or sets korisnik ime
        /// </summary>
        public virtual string Ime { get; set; }

        /// <summary>
        ///   Gets or sets korisnik prezime
        /// </summary>
        public virtual string Prezime { get; set; }


        /// <summary>
        ///   Get puno ime i prezime
        /// </summary>

        public virtual string ImePrezime
        {
            get { return String.Format("{0} {1}", Ime, Prezime); }
        }
      
     
        /// <summary>
        ///   Postavi mail
        /// </summary>
        public virtual string Email { get; set; }


        /// <summary>
        /// Gets slika radnika.
        /// </summary>
        public virtual ImageItem Avatar { get; set; }


        /// <summary>
        /// Gets or sets date and time when blog post is published.
        /// </summary>
        public virtual DateTime PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets date and time when blog post expires.
        /// </summary>
        public virtual DateTime? ExpiresOn { get; set; }
        /// <summary>
        /// Gets or sets status
        /// </summary>
        public virtual WorkflowStatus Status { get; set; }


    }
}