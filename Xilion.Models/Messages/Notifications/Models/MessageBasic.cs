using System;

namespace Xilion.Models.Messages.Notifications.Models
{
    public class MessageBasic
    {
        /// <summary>
        /// Gets or setd message unique identifier.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets message text.
        /// </summary>
        public String Content { get; set; }
    }
}
