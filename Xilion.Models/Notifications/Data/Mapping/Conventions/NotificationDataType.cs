using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using Xilion.Models.Core.Domain;
using Xilion.Models.Notifications.Domain;
using Xilion.Framework.Data.Types;
using Xilion.Framework.Serialization;
using System.Data.Common;
using NHibernate.Engine;

namespace Xilion.Models.Notifications.Data.Mapping.Conventions
{
    public class NotificationDataType<T> : ImmutableUsersType<Notification>
    {
        #region Overrides of ImmutableUsersType<Notification>

        /// <summary>
        /// The SQL types for the columns mapped by this type. 
        /// </summary>
        public override SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.GetString(4001)}; }
        }

        /// <summary>
        /// Retrieve an instance of the mapped class from a JDBC resultset.
        ///             Implementors should handle possibility of null values.
        /// </summary>
        /// <param name="rs">a IDataReader</param><param name="names">column names</param><param name="owner">the containing entity</param>
        /// <returns/>
        /// <exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var serializedProperties = NHibernateUtil.String.NullSafeGet(rs, names[0],session,owner) as string;

            var data = new NotificationData
                           {
                               Properties = serializedProperties != null
                                                ? Serializer.Default()
                                                      .Deserialize<IList<MetaDataProperty>>(serializedProperties)
                                                : new List<MetaDataProperty>(),
                               IsChanged = false
                           };
            return data;
        }

        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        ///             Implementors should handle possibility of null values.
        ///             A multi-column type should be written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">a IDbCommand</param><param name="value">the object to write</param><param name="index">command parameter index</param><exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var metaData = value as NotificationData;

            object valueToSet =
                metaData == null
                    ? DBNull.Value
                    : (object) Serializer.Default()
                                   .Serialize<IList<MetaDataProperty>>(metaData.Properties);

            if (metaData != null) metaData.IsChanged = false;
            NHibernateUtil.String.NullSafeSet(cmd, valueToSet, index,session);
        }

        // HACK to make sure that NHibernate saves when some of the metadata properties are changed and no other entity
        // property is changed.
        // The best way would be for this type to be Mutable, but there's a problem with NHibernate Search throwing
        // exception deep within it's core. Try to make this type Mutable after NHibernate and/or NHibernate Search
        // upgrade.
        protected override bool IsChanged(object x, object y)
        {
            var current = (NotificationData) x;
            return current.IsChanged;
        }

        #endregion
    }
}