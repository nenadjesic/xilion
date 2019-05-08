using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.SqlTypes;
using Xilion.Models.Messages.Domain;
using StructureMap;
using System.Data.Common;
using NHibernate.Engine;
using Xilion.Framework.Data.Types;

namespace Xilion.Models.Messages.Data.Mapping.Conventions
{
    public class AttachmentProviderType : ImmutableUsersType<IAttachmentProvider>
    {
        #region Overrides of ImmutableUsersType<IAttachmentProvider>
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
            var providerName = NHibernateUtil.String.NullSafeGet(rs, names[0],session) as string;

            return Container.GetInstance<IAttachmentProvider>(providerName);
        }

        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        ///             Implementors should handle possibility of null values.
        ///             A multi-column type should be written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">a IDbCommand</param><param name="value">the object to write</param><param name="index">command parameter index</param><exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var type = value as IAttachmentProvider;
            if(type == null)
                throw new NoNullAllowedException("ProvideraName property of attachment cannot be null");

            var providerName = type.GetType().Name;

            NHibernateUtil.String.NullSafeSet(cmd, providerName, index,session);
        }

        #endregion
    }
}
