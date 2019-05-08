using System;
using Xilion.Models.Core.Domain;
using Xilion.Framework;
using Xilion.Framework.Domain;
using Xilion.Framework.Queries;
using System.Globalization;

namespace Xilion.Models.Core.Queries
{
    public class WorkflowQuery<T> : MetaDataQuery<T> where T : Entity
    {
        public WorkflowQuery(ICmsContext cmsContext) : base(cmsContext)
        {
            AddProperty(Properties.Statuses).SetList();
            AddProperty(Properties.NotStatus);
            AddProperty(Properties.PublishedOn).SetOperator(QueryOperator.Between);
            AddProperty(Properties.ExpiresOn).SetOperator(QueryOperator.Between);
            SetValue(Properties.NotStatus, WorkflowStatus.Deleted.Value.ToString());
        }

        public string[] Statuses
        {
            get { return GetValue<string[]>(Properties.Statuses); }
            set {  SetValue(Properties.Statuses, value); }
        }

        public WorkflowStatus NotStatus
        {
            get
            {
                var value = GetValue<string>(Properties.NotStatus);
                return String.IsNullOrWhiteSpace(value)
                           ? null
                           : Enumeration.FromValue<WorkflowStatus>(Int32.Parse(value));
            }
            set { SetValue(Properties.NotStatus, value == null ? String.Empty : value.Value.ToString()); }
        }

        public bool Scheduled { get; set; }

        public DateTime? PublishedOnFrom
        {
            get { return GetRangeFromValue<DateTime?>(Properties.PublishedOn); }
            set { SetRangeFromValue(Properties.PublishedOn, value); }
        }

        public DateTime? PublishedOnTo
        {
            get { return GetRangeToValue<DateTime?>(Properties.PublishedOn); }
            set { SetRangeToValue(Properties.PublishedOn, value); }
        }

        public DateTime? ExpiresOnFrom
        {
            get { return GetRangeFromValue<DateTime?>(Properties.ExpiresOn); }
            set { SetRangeFromValue(Properties.ExpiresOn, value); }
        }

        public DateTime? ExpiresOnTo
        {
            get { return GetRangeToValue<DateTime?>(Properties.ExpiresOn); }
            set { SetRangeToValue(Properties.ExpiresOn, value); }
        }

      

        #region Nested type: Properties

        private static class Properties
        {
            public const string PublishedOn = "PublishedOn";
            public const string ExpiresOn = "ExpiresOn";
            public const string Statuses = "Status";
            public const string NotStatus = "-Status";
        }

        #endregion
    }
}