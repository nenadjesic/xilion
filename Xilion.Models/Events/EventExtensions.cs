using System;

namespace Xilion.Models.Events
{
    /// <summary>
    /// Represents Event Extensions object.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Gets value that indicates if event is active.
        /// </summary>
        public static bool IsActive(this Event eventInfo)
        {
            return eventInfo.StartsOn >= DateTime.Today;
        }
    }
}