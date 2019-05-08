using System;
using System.Data;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Xilion.Framework.Data.Types
{
    public abstract class ImmutableUsersType<T> : IUserType
    {
        #region IUsersType Members

        public bool IsMutable
        {
            get { return false; }
        }

        public Type ReturnedType
        {
            get { return typeof (T); }
        }

        public abstract SqlType[] SqlTypes { get; }

        public abstract object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner);
        public abstract void NullSafeSet(DbCommand st, object value, int index, ISessionImplementor session); 

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        bool IUserType.Equals(object x, object y)
        {
            if (IsChanged(x, y))
                return false;

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        #endregion

        protected virtual bool IsChanged(object x, object y)
        {
            return false;
        }
    }
}