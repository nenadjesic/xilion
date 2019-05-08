using System;
using System.Data;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using Xilion.Framework.Extensions;

namespace Xilion.Framework.Data.Types
{
    public class TimeZoneInfoType : ImmutableUsersType<TimeZoneInfo>
    {
        public override SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.Int32}; }
        }

        public override  object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var value = NHibernateUtil.Int32.NullSafeGet(rs, names[0], session) as int?;
            return value == null ? null : TimeZoneExtension.Instance.GetTimeZoneById(value.Value);
        }

        public override void NullSafeSet(DbCommand st, object value, int index, ISessionImplementor session)
        {
            var timeZoneInfo = value as TimeZoneInfo;

            object valueToSet = timeZoneInfo == null
                                    ? DBNull.Value
                                    : (object) TimeZoneExtension.Instance.GetTimeZoneId(timeZoneInfo);

            NHibernateUtil.Int32.NullSafeSet(st, valueToSet, index, session);
        }
    }
}