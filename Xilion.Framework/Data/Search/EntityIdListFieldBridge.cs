using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;
using Xilion.Framework.Domain;

namespace Xilion.Framework.Data.Search
{
    public class EntityIdListFieldBridge : IFieldBridge
    {
        #region IFieldBridge Members

        public void Set(
            string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var enumeration = value as IEnumerable;
            if (enumeration == null) return;

            IEnumerable<Entity> entities = enumeration.OfType<Entity>();

            string fieldValue = String.Join(" ", entities.Select(x => x.Id.ToString().ToLower()).ToArray());

            var field = new Field(name, fieldValue, store, index);
            if (boost != null) field.SetBoost(boost.Value);

            document.Add(field);
        }

        #endregion
    }
}