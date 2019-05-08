using System.Security.Principal;

namespace Xilion.Models.Core.Security
{
    /// <summary>
    /// Represents a secured object with a set of permissions.
    /// </summary>
    public interface ISecured
    {
        /// <summary>
        /// Gets or sets the permissions for this secured object.
        /// </summary>
        Permissions Permissions { get; set; }


    }
}