using System;
using Xilion.Framework.Domain;

namespace Xilion.Models.Messages.Domain
{
    /// <summary>
    /// Represent message state and use for chack if Users read message
    /// </summary>
    public class MessageState : Entity
    {
        /// <summary>
        /// Gets or sets message fot this state
        /// </summary>
        public virtual Message Message { get; set; }

        /// <summary>
        /// Gets or sets Users
        /// </summary>
        public virtual ConversationMember Member { get; set; }

        /// <summary>
        /// Gets or sets is Users read message
        /// </summary>
        public virtual bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets date and time when Users read message
        /// </summary>
        public virtual DateTime ReadDate { get; set; }
    }
}
