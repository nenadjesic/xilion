using FluentNHibernate.Mapping;
using Xilion.Framework.Domain;

namespace Xilion.Framework.Data.Mappings
{
    public class AuditRevisionMap : ClassMap<AuditRevision>
    {
        public AuditRevisionMap()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Id(x => x.Id).GeneratedBy.Identity().Column("ID");
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            Map(x => x.RevisionDate);
        }
    }
}