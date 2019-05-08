using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Data
{
    public class EntityAliasListFieldBridge : IFieldBridge
    {
        #region Implementation of IFieldBridge

        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var enumeration = value as IEnumerable;
            if (enumeration == null) return;

            var entities = enumeration.OfType<IAliased>();

            var fieldValue = String.Join(" ",
                                         entities.Select(x => x.Alias.ToString(CultureInfo.InvariantCulture).ToLower()).
                                             ToArray());

            var field = new Field(name, fieldValue, store, index);
            if (boost != null) field.SetBoost(boost.Value);

            document.Add(field);
        }

        #endregion
    }
}