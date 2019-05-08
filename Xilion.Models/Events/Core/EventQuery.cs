using System;
using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Models.Articles;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;
using Xilion.Models.Events;

namespace Xilion.Models.Events.Core
{
    public class EventQuery : WorkflowQuery<Event>
    {
        private readonly ICmsContext _cmsContext;

        public EventQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("CreatedOn", SortOrder.Descending);
            Paging = new PagerInfo(1, 20);
            SetQ(Properties.Title).SetIsLocalized(true);
            AddProperty(Properties.StartsOn).SetOperator(QueryOperator.Between);
            AddProperty(Properties.EndsOn).SetOperator(QueryOperator.Between);
            AddProperty(Properties.Location);
        }

        private EventSettings Settings
        {
            get { return (EventSettings)_cmsContext.GetApplication<EventApplication>().GetSettings(); }
        }


        public static EventQuery Default
        {
            get { return new EventQuery(CmsContext.Current); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }

        public string Location
        {
            get { return GetValue<string>(Properties.Location); }
            set { SetValue(Properties.Location, value); }
        }

        public DateTime? StartsOnFrom
        {
            get { return GetRangeFromValue<DateTime?>(Properties.StartsOn); }
            set { SetRangeFromValue(Properties.StartsOn, value); }
        }

        public DateTime? StartsOnTo
        {
            get { return GetRangeToValue<DateTime?>(Properties.StartsOn); }
            set { SetRangeToValue(Properties.StartsOn, value); }
        }

        public DateTime? EndsOnFrom
        {
            get { return GetRangeFromValue<DateTime?>(Properties.EndsOn); }
            set { SetRangeFromValue(Properties.EndsOn, value); }
        }

        public DateTime? EndsOnTo
        {
            get { return GetRangeToValue<DateTime?>(Properties.EndsOn); }
            set { SetRangeToValue(Properties.EndsOn, value); }
        }

        #region Nested type: Properties

        private static class Properties
        {
            public const string Title = "MetaData.Title";
            public const string StartsOn = "startson";
            public const string EndsOn = "endson";
            public const string Location = "location";
        }

        #endregion
    }
}