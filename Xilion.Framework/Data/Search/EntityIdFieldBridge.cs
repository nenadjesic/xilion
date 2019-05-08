using Lucene.Net.Documents;
using NHibernate.Search.Bridge;
using Xilion.Framework.Domain;

namespace Xilion.Framework.Data.Search
{
    public class EntityIdFieldBridge : IFieldBridge
    {
        #region IFieldBridge Members

        public void Set(
            string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var entity = value as Entity;
            if (entity == null) return;

            string fieldValue = entity.Id.ToString().ToLower();

            var field = new Field(name, fieldValue, store, index);
            if (boost != null) field.SetBoost(boost.Value);

            document.Add(field);
        }

        #endregion
    }
}