using System;
using Xilion.Framework.Queries;

namespace Xilion.Models.Core.Queries
{
    public class TrackableQuery : Query
    {
        public TrackableQuery()
        {
            AddProperty(Properties.CreatedBy).SetOperator(QueryOperator.StartsWith);
            AddProperty(Properties.CreatedOn).SetOperator(QueryOperator.Between);
            AddProperty(Properties.UpdatedOn).SetOperator(QueryOperator.Between);
        }

        public virtual string Owner { get; set; }

        public string CreatedBy
        {
            get { return GetValue<string>(Properties.CreatedBy); }
            set { SetValue(Properties.CreatedBy, value); }
        }

        public DateTime? CreatedOnFrom
        {
            get { return GetRangeFromValue<DateTime?>(Properties.CreatedOn); }
            set { SetRangeFromValue(Properties.CreatedOn, value); }
        }

        public DateTime? CreatedOnTo
        {
            get { return GetRangeToValue<DateTime?>(Properties.CreatedOn); }
            set { SetRangeToValue(Properties.CreatedOn, value); }
        }

        public DateTime? UpdatedOnFrom
        {
            get { return GetRangeFromValue<DateTime?>(Properties.UpdatedOn); }
            set { SetRangeFromValue(Properties.UpdatedOn, value); }
        }

        public DateTime? UpdatedOnTo
        {
            get { return GetRangeToValue<DateTime?>(Properties.UpdatedOn); }
            set { SetRangeToValue(Properties.UpdatedOn, value); }
        }


        #region Nested type: Properties

        private static class Properties
        {
            public const string CreatedBy = "CreatedBy";
            public const string CreatedOn = "CreatedOn";
            public const string UpdatedOn = "UpdatedOn";
        }

        #endregion
    }
}