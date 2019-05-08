using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;

namespace Xilion.Framework.Data.Types
{
    public class CultureInfoType : ImmutableUsersType<CultureInfo>
    {
        public override SqlType[] SqlTypes
        {
            // ReSharper disable CoVariantArrayConversion
            get { return new[] {SqlTypeFactory.GetString(20)}; }
            // ReSharper restore CoVariantArrayConversion
        }

        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var value = NHibernateUtil.String.NullSafeGet(rs, names[0],session) as string;
            return value == null ? null : new CultureInfo(value);
        }

        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var cultureInfo = value as CultureInfo;

            object valueToSet = cultureInfo == null
                                    ? DBNull.Value
                                    : (object) cultureInfo.Name;

            NHibernateUtil.String.NullSafeSet(cmd, valueToSet, index,session);
        }
    }
}