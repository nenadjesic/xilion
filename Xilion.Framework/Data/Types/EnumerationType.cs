using System;
using System.Data;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;

namespace Xilion.Framework.Data.Types
{
    public class EnumerationType<T> : ImmutableUsersType<Enumeration> where T : Enumeration
    {
        public override SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.Int32}; }
        }

        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var value = NHibernateUtil.Int32.NullSafeGet(rs, names[0],session) as int?;
            return value == null ? null : Enumeration.FromValue<T>(value.Value);
        }

        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var enumeration = value as Enumeration;

            object valueToSet = enumeration == null
                                    ? DBNull.Value
                                    : (object) enumeration.Value;

            NHibernateUtil.Int32.NullSafeSet(cmd, valueToSet, index,session);
        }
    }
}