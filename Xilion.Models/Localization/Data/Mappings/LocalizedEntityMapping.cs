using FluentNHibernate.Mapping;

namespace Xilion.Models.Localization.Data.Mappings
{
    public class LocalizedEntityMapping : ClassMap<LocalizedEntity>
    {
        public LocalizedEntityMapping()
        {
            Table("Xilion_LocalizedEntity");
            Cache.ReadWrite();
            CompositeId()
                .ComponentCompositeIdentifier(x => x.ID)
                .KeyProperty(x => x.ID.Culture)
                .KeyProperty(x => x.ID.EntityID)
                .KeyProperty(x => x.ID.Property)
                .KeyProperty(x => x.ID.Type);
            Map(x => x.Value);
        }
    }
}