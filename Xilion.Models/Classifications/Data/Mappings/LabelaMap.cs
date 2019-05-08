using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.Classifications.Data.Mappings
{
    public class LabelMap : CmsEntityMap<Label>
    {
        public LabelMap()
        {
            Cache.ReadWrite();

            Map(x => x.Color).Nullable();
            References(x => x.Classification);
            References(x => x.Parent).Nullable();
            HasMany(x => x.Children).KeyColumn("ParentID")
                .ForeignKeyConstraintName("FK_Label_LabelChild")
                .Cascade.Delete()
                .KeyNullable();
        }
    }
}