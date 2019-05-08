using FluentNHibernate.Mapping;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Data.Mappings.Conventions;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data.Mappings.Conventions;
using Xilion.Framework.Data.Types;
using Xilion.Framework.Domain;
using Xilion.Framework.Extensions;

namespace Xilion.Models.Core.Data.Mappings
{
    /// <summary>
    /// Represents base entity ClassMap fr common mappngs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CmsEntityMap<T> : ClassMap<T> where T : Entity
    {
        protected CmsEntityMap()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            MapId();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            MapTrackable();
            MapMetaData();
            MapClassification();
            MapLabeled();
            MapAliased();
            MapWorkflow();
            //MapSecured();
            MapLockable();
            MapOrdered();
            
        }

        protected virtual void MapId()
        {
            Id(x => x.Id);
        }

        private void MapTrackable()
        {
            if (!typeof (T).Implements<ITrackable>()) return;

            Map(x => ((ITrackable) x).CreatedBy);
            Map(x => ((ITrackable) x).CreatedOn);
            Map(x => ((ITrackable) x).UpdatedBy);
            Map(x => ((ITrackable) x).UpdatedOn);
        }

        private void MapMetaData()
        {
            if (!typeof (T).Implements<IHaveMetaData>()) return;

            Map(x => ((IHaveMetaData) x).MetaData)
                .Column("MetaData")
                .CustomType<MetaDataType<T>>()
                .Length(4001);
        }

        private void MapClassification()
        {
            if (!typeof(T).Implements<IClassificated>()) return;

            HasManyToMany(x => ((IClassificated)x).Classifications)
                .Table(TableNameConvention.Prefix + "ClassificatedEntity")
                .ParentKeyColumn("EntityID")
                .ForeignKeyConstraintNames("FK_ClassificatedEntity_Entity", null)
                //.Cascade.AllDeleteOrphan()
                .Cache.ReadWrite();
        }

        private void MapLabeled()
        {
            if (!typeof(T).Implements<ILabeled>()) return;

            HasManyToMany(x => ((ILabeled)x).Labels)
                .Table(TableNameConvention.Prefix + "LabeledEntity")
                .ParentKeyColumn("EntityID")
                .ForeignKeyConstraintNames("FK_LabeledEntity_Entity", null)
                .Cascade.AllDeleteOrphan()
                .Cache.ReadWrite();
        }

        private void MapAliased()
        {
            if (!typeof (T).Implements<IAliased>()) return;

            Map(x => ((IAliased)x).Alias).Length(2000);
        }

        private void MapWorkflow()
        {
            if (!typeof (T).Implements<IHaveWorkflow>()) return;

            Map(x => ((IHaveWorkflow) x).PublishedOn);
            Map(x => ((IHaveWorkflow) x).ExpiresOn).Nullable();
            Map(x => ((IHaveWorkflow) x).Status).CustomType(typeof (EnumerationType<WorkflowStatus>));
        }

        //private void MapSecured()
        //{
        //    if (!typeof (T).Implements<ISecured>()) return;

        //    Map(x => ((ISecured) x).Permissions)
        //        .Column("Permissions")
        //        .CustomType<PermissionsType>()
        //        .Length(4001);
        //}

        private void MapLockable()
        {
            if (!typeof (T).Implements<ILockable>()) return;

            Map(x => ((ILockable) x).LockedBy);
            Map(x => ((ILockable) x).LockedOn).Nullable();
        }

        private void MapOrdered()
        {
            if (!typeof (T).Implements<IOrdered>()) return;

            Map(x => ((IOrdered)x).Ordinal);
        }
    }
}