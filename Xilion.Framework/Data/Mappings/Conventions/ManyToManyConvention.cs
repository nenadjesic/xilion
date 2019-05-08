using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class ManyToManyConvention : IHasManyToManyConvention
    {
        #region IHasManyToManyConvention Members

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IManyToManyCollectionInstance instance)
        {
            string tableName =
                String.Format("{0}{1}{2}", TableNameConvention.Prefix, instance.EntityType.Name, instance.ChildType.Name);
            instance.Table(tableName);

            instance.Key.ForeignKey(
                String.Format("FK_{0}_{1}", tableName, instance.EntityType.Name));
            // How to map foreign key constraint name for child column ??

            instance.Key.Column(instance.EntityType.Name + "ID");
            instance.Relationship.Column(instance.ChildType.Name + "ID");
        }

        #endregion
    }
}