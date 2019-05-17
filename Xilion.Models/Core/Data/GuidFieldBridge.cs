using Lucene.Net.Documents;

using Xilion.Framework.Domain;

namespace Xilion.Models.Core.Data
{
    public class longFieldBridge : IFieldBridge
    {
        #region IFieldBridge Members

        public void Set(
            string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var entity = value as Entity;
            if (entity == null) return;

            string fieldValue = entity.Id.ToString();

            var field = new Field(name, fieldValue, store, index);
            if (boost != null) field.SetBoost(boost.Value);

            document.Add(field);
        }

        #endregion
    }
}