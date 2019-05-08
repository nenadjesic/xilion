using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class ReferenceConvention : IReferenceConvention
    {
        #region IReferenceConvention Members

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Name + "ID");
            instance.ForeignKey(
                String.Format("FK_{0}_{1}", instance.EntityType.Name, instance.Property.PropertyType.Name));
            instance.Not.Nullable();
        }

        #endregion
    }
}