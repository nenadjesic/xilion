using System;
using System.Web;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Extensions
{
    /// <summary>
    /// Contains extensions for <see cref="ILockable"/> objects.
    /// </summary>
    public static class LockableExtensions
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Checks whether the given lockable is locked.
        /// </summary>
        /// <param name="lockable">ILockable instance to check.</param>
        /// <returns>true if lockable is locked; false otherwise.</returns>
        public static bool IsLocked(this ILockable lockable)
        {
            return lockable.LockedOn != null &&
                   lockable.LockedOn > DateTime.Now.AddMinutes(-5) &&
                   lockable.LockedBy != _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        /// <summary>
        /// Locks the given lockable object.
        /// </summary>
        /// <param name="lockable">ILockable instance to lock.</param>
        public static void Lock(this ILockable lockable)
        {
            if (IsLocked(lockable)) return;

            lockable.LockedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            lockable.LockedOn = DateTime.Now;
        }

        /// <summary>
        /// Unlocks the given lockable object.
        /// </summary>
        /// <param name="lockable">ILockable instance to lock.</param>
        public static void Unlock(this ILockable lockable)
        {
            lockable.LockedBy = String.Empty;
            lockable.LockedOn = null;
        }
    }
}