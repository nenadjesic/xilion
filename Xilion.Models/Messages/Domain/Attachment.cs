using System;
using Xilion.Models.Messages.Enumerator;
using Xilion.Framework.Domain;

namespace Xilion.Models.Messages.Domain
{
    public class Attachment : Entity
    {
        /// <summary>
        /// Gets or sets provider type
        /// </summary>
       // public virtual ProviderType Provider { get; set; }

        /// <summary>
        /// Gets or sets documetn unique identifier
        /// </summary>
        public virtual String ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets message where it belongs
        /// </summary>
        public virtual Message Message { get; set; }
    }
}
