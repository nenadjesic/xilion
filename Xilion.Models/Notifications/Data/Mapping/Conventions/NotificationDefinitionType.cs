using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using Xilion.Models.Notifications.Definitions;
using Xilion.Models.Notifications.Domain;
using Xilion.Framework.Data.Types;
using SM = StructureMap;
using System.Data.Common;
using NHibernate.Engine;
using StructureMap;

namespace Xilion.Models.Notifications.Data.Mapping.Conventions
{
    public class NotificationDefinitionType : ImmutableUsersType<INotificationDefinition>
    {
        #region Overrides of ImmutableUsersType<INotificationDefinition>
        public static IContainer Container { get; set; }
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
            var typeName = NHibernateUtil.String.NullSafeGet(rs, names[0],session) as string;
            var definition = Container.GetInstance<INotificationDefinition>(typeName);
            return definition;
        }

        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        ///             Implementors should handle possibility of null values.
        ///             A multi-column type should be written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">a IDbCommand</param><param name="value">the object to write</param><param name="index">command parameter index</param><exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var type = value as INotificationDefinition;
            if (type == null)
                throw new NoNullAllowedException("DefinitionName property of Question cannot be null");

            string typeName = type.GetType().Name;

            NHibernateUtil.String.NullSafeSet(cmd, typeName, index,session);
        }

        #endregion
    }
}