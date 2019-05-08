using System.Collections.Generic;
using Xilion.Framework.Domain;

namespace Xilion.Models.Messages.Domain
{
    /// <summary>
    /// Represent an converzation
    /// </summary>
    public class Conversation : TrackableEntity
    {
        private IList<ConversationMember> _members = new List<ConversationMember>();
        private IList<Message> _messages = new List<Message>();

        /// <summary>
        /// Gets or sets conversation members.
        /// </summary>
        public virtual IList<ConversationMember> Members
        {
            get { return _members; }
            protected set { _members = value; }
        }

        /// <summary>
        /// Gets or sets conversation messages.
        /// </summary>
        public virtual IList<Message> Messages
        {
            get { return _messages; }
            protected set { _messages = value; }
        }
    }
}
