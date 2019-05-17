using Lucene.Net.Documents;

using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Data
{
    public class EntityAliasFieldBridge : IFieldBridge
    {
        #region IFieldBridge Members

        public void Set(
            string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var entity = value as IAliased;
            if (entity == null) return;

            var fieldValue = entity.Alias.ToLower();

            var field = new Field(name, fieldValue, store, index);
            if (boost != null) field.SetBoost(boost.Value);

            document.Add(field);
        }

        #endregion
    }
}