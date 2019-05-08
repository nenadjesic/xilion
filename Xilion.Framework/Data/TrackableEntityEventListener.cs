using System;
using System.Threading;
using System.Web;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Proxy;
using Xilion.Framework.Domain;
using Xilion.Framework.Logging;

namespace Xilion.Framework.Data
{
    public class TrackableEntityEventListener : DefaultSaveOrUpdateEventListener
    {
        private static readonly ILogger _log = LogManager.GetLogger<TrackableEntityEventListener>();
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        protected override object EntityIsPersistent(SaveOrUpdateEvent e)
        {
            if (e.Entity is INHibernateProxy) return base.EntityIsPersistent(e);

            var trackable = e.Entity as ITrackable;
            if (trackable != null)
            {
                // First make sure there are any dirty properties on object:
                // http://nhforge.org/wikis/howtonh/finding-dirty-properties-in-nhibernate.aspx
                var sessionImplementation = e.Session.GetSessionImplementation();
                var oldState = e.Entry.LoadedState;
                var currentState = e.Entry.Persister.GetPropertyValues(e.Entity);

                if (oldState == null ||
                    e.Entry.Persister.FindDirty(currentState, oldState, e.Entity, sessionImplementation) != null)
                {
                    var time = DateTime.Now;
                    var name = GetUsersName();

                    _log.DebugFormat("Saving update tracking information for entity of type '{0}'.",
                                     trackable.GetType());

                    trackable.UpdatedBy = name;
                    trackable.UpdatedOn = time;
                }
            }

            return base.EntityIsPersistent(e);
        }

        protected override object EntityIsTransient(SaveOrUpdateEvent e)
        {
            var trackable = e.Entity as ITrackable;
            if (trackable != null)
            {
                var time = DateTime.Now;
                var name = GetUsersName();

                _log.DebugFormat("Saving insert tracking information for entity of type '{0}'.", trackable.GetType());

                if (String.IsNullOrWhiteSpace(trackable.CreatedBy))
                    trackable.CreatedBy = name;
                if (trackable.CreatedOn == DateTime.MinValue)
                    trackable.CreatedOn = time;

                trackable.UpdatedBy = name;
                trackable.UpdatedOn = time;
            }

            return base.EntityIsTransient(e);
        }

        private static string GetUsersName()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var Usersname = Thread.CurrentPrincipal.Identity.Name;
            if (httpContext.User != null)
                Usersname = httpContext.User.Identity.Name;

            return Usersname;
        }
    }
}