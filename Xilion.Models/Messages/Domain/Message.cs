using System;
using System.Collections.Generic;

using Xilion.Framework.Domain;

namespace Xilion.Models.Messages.Domain
{
    /// <summary>
    /// Represents the message.
    /// </summary>
    public class Message : TrackableEntity
    {
        private IList<Attachment> _attachments = new List<Attachment>();

        /// <summary>
        /// Gets or sets Users who sent the message.
        /// </summary>
        public virtual Users Sender { get; set; }

        /// <summary>
        /// Gets or sets message content.
        /// </summary>
        public virtual String Content { get; set; }

        /// <summary>
        /// Gets or sets conversation this message belongs to.
        /// </summary>
        public virtual Conversation Conversation { get; set; }

        /// <summary>
        /// Gets or sets list of attachments for message.
        /// </summary>
        public virtual IList<Attachment> Attachments
        {
            get { return _attachments; }
            protected set { _attachments = value; }
        }
    }
}
