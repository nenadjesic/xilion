using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.Classifications.Data.Mappings
{
    public class ClassificationMap : CmsEntityMap<Classification>
    {
        public ClassificationMap()
        {
            Map(x => x.IsSystem);
            Map(x => x.ClassificationType);
            HasMany(x => x.Labels).KeyColumn("ClassificationID")
                .ForeignKeyConstraintName("FK_Classification_Label")
                .Cascade.Delete();
        }
    }
}