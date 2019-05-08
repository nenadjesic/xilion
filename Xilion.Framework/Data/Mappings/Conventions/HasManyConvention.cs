using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        #region IHasManyConvention Members

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(String.Format("FK_{0}_{1}", instance.ChildType.Name, instance.EntityType.Name));
            instance.Key.Column(instance.EntityType.Name + "ID");
            instance.Cascade.AllDeleteOrphan();
            instance.Inverse();
        }

        #endregion
    }
}