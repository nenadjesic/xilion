using System;

namespace Xilion.Models.Notifications.Models
{
    public class UsersBasic
    {
        /// <summary>
        /// Gets or sets Users unique identifier.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets Users first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Users last name.
        /// </summary>
        public string LastName { get; set; }
    }
}