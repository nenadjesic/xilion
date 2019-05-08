
using Xilion.Framework.Domain;

namespace Xilion.Models.Messages.Domain
{
    /// <summary>
    /// Represent memebrs in conversation
    /// </summary>
    public class ConversationMember : TrackableEntity
    {
        /// <summary>
        /// Gets or sets Users
        /// </summary>
        public virtual Users Users { get; set; }

        /// <summary>
        /// Gets or sets Conversation
        /// </summary>
        public virtual Conversation Conversation { get; set; }

        /// <summary>
        /// Gets or sets is Users leaved conversation
        /// </summary>
        public virtual bool IsLeaved { get; set; }
    }
}
