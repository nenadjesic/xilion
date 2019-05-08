using System.Globalization;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;

namespace Xilion.Framework.Data.Search
{
    public class EnumerationFieldBridge : IFieldBridge
    {
        #region IFieldBridge Members

        public void Set(
            string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var enumeration = value as Enumeration;
            if (enumeration == null) return;

            int fieldValue = enumeration.Value;

            var field = new Field(name, fieldValue.ToString(CultureInfo.InvariantCulture), store, index);
            if (boost != null) field.SetBoost(boost.Value);

            document.Add(field);
        }

        #endregion
    }
}