using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        #region IIdConvention Members

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IIdentityInstance instance)
        {
            if (instance.Name != "ID" && instance.Name != "Id") return;

            instance.Column("Id");

            if (instance.Type.GetUnderlyingSystemType() == typeof (long))
                instance.GeneratedBy.Identity();
        }

        #endregion
    }
}