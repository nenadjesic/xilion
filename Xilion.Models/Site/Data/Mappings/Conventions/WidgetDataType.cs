using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data.Types;
using Xilion.Framework.Serialization;
using System.Data.Common;
using NHibernate.Engine;

namespace Xilion.Models.Site.Data.Mappings.Conventions
{
    /// <summary>
    /// Represents mapping convention type for MetData types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WidgetDataType : ImmutableUsersType<WidgetData>
    {
        public override SqlType[] SqlTypes
        {
            // ReSharper disable CoVariantArrayConversion
            get { return new[] {SqlTypeFactory.GetString(4001)}; }
            // ReSharper restore CoVariantArrayConversion
        }

        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var serializedProperties = NHibernateUtil.String.NullSafeGet(rs, names[0],session,owner) as string;

            return new WidgetData()
                       {
                           Properties = serializedProperties != null
                                            ? Serializer.Default()
                                                  .Deserialize<IList<MetaDataProperty>>(serializedProperties)
                                            : new List<MetaDataProperty>(),
                           IsChanged = false
                       };
        }

        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var widgetData = value as WidgetData;

            object valueToSet =
                widgetData == null
                    ? DBNull.Value
                    : (object) Serializer.Default()
                                   .Serialize<IList<MetaDataProperty>>(widgetData.Properties);

            if (widgetData != null) widgetData.IsChanged = false;
            NHibernateUtil.String.NullSafeSet(cmd, valueToSet, index,session);
        }

        // HACK to make sure that NHibernate saves when some of the WidgetData properties are changed and no other entity
        // property is changed.
        // The best way would be for this type to be Mutable, but there's a problem with NHibernate Search throwing
        // exception deep within it's core. Try to make this type Mutable after NHibernate and/or NHibernate Search
        // upgrade.
        protected override bool IsChanged(object x, object y)
        {
            var current = (WidgetData)x;
            return current.IsChanged;
        }
    }
}