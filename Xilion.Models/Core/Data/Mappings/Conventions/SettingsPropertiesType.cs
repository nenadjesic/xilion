using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using Xilion.Models.Core.Settings;
using Xilion.Framework.Data.Types;
using Xilion.Framework.Serialization;
using NHibernate.Engine;
using System.Data.Common;

namespace Xilion.Models.Core.Data.Mappings.Conventions
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingsPropertiesType : ImmutableUsersType<SettingsProperties>
    {
        public override SqlType[] SqlTypes
        {
            // ReSharper disable CoVariantArrayConversion
            get { return new[] {SqlTypeFactory.GetString(4001)}; }
            // ReSharper restore CoVariantArrayConversion
        }

        public override object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var settingsPropertyList = NHibernateUtil.String.NullSafeGet(rs, names[0],session) as string;

            var result = new SettingsProperties();
            if (settingsPropertyList != null)
                result.Properties = Serializer.Default().Deserialize<IList<SettingsProperty>>(settingsPropertyList);
            return result;
        }

        public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var settingsProperties = value as SettingsProperties;

            object valueToSet =DBNull.Value;
            if(settingsProperties != null)
            {
                valueToSet = (object)Serializer.Default().Serialize<IList<SettingsProperty>>(settingsProperties.Properties);
            }
           
            if (settingsProperties != null)
                settingsProperties.IsChanged = false;

            NHibernateUtil.String.NullSafeSet(cmd, valueToSet, index,session);
        }

        // HACK to make sure that NHibernate saves when some of the metadata properties are changed and no other entity
        // property is changed.
        // The best way would be for this type to be Mutable, but there's a problem with NHibernate Search throwing
        // exception deep within it's core. Try to make this type Mutable after NHibernate and/or NHibernate Search
        // upgrade.
        protected override bool IsChanged(object x, object y)
        {
            var current = (SettingsProperties) x;
            return current.IsChanged;
        }
    }
}