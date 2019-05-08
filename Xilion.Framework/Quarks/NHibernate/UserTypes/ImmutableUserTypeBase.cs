using System;
using System.Data;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Quarks.NHibernate.UserTypes
{
	/// <summary>
	/// Base IUserType implementation for immutable value types.
	/// </summary>
	[Serializable]
	public abstract class ImmutableUserTypeBase<TReturnedType> : IUserType
	{
		public abstract SqlType[] SqlTypes { get; }

        public abstract object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner);
        public abstract void NullSafeSet(DbCommand st, object value, int index, ISessionImplementor session);

        public new bool Equals(object x, object y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;
			return x.Equals(y);
		}

		public int GetHashCode(object x)
		{
			return x == null
					   ? typeof(TReturnedType).GetHashCode() + 473 // I don't know why it is 473.
					   : x.GetHashCode();
		}

		/// <summary>
		/// Objects are immutable, DeepCopy(value) always returns value.
		/// </summary>
		public object DeepCopy(object value)
		{
			return value;
		}

		public object Replace(object original, object target, object owner)
		{
			return DeepCopy(original);
		}

		public object Assemble(object cached, object owner)
		{
			return DeepCopy(cached);
		}

		public object Disassemble(object value)
		{
			return DeepCopy(value);
		}

		public Type ReturnedType
		{
			get { return typeof(TReturnedType); }
		}

		public bool IsMutable
		{
			get { return false; }
		}
	}
}
