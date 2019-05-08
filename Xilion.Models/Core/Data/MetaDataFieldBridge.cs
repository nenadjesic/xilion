using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Data
{
    /// <summary>
    /// Represents field bridget used by NHibernate search to properly index MetaData types.
    /// </summary>
    public class MetaDataFieldBridge : IFieldBridge
    {
        #region IFieldBridge Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="document"></param>
        /// <param name="store"></param>
        /// <param name="index"></param>
        /// <param name="boost"></param>
        /// <exception cref="Exception"></exception>
        public void Set(
            string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var metaData = value as MetaData;
            if (metaData == null)
                throw new Exception(
                    "MetaDataFieldBridge can only be used for properties of type MetaData.");

            foreach (MetaDataPropertyDefinition property in metaData.GetPropertyDefinitions())
            {
                if (!property.Indexed) continue;
                string propertyName = property.Name.ToLowerInvariant();

                if (!property.IsLocalized)
                {
                    MetaDataProperty item =
                        metaData.Properties.FirstOrDefault(x => x.Name.ToLowerInvariant() == propertyName);
                    if (item == null) continue;

                    AddField(
                        document, item.FullName, GetPropertyValue(item), property.IsAnalyzed, property.IsStored, boost);
                }
                else
                {
                    IEnumerable<MetaDataProperty> items =
                        metaData.Properties.Where(x => x.Name.ToLowerInvariant() == propertyName);

                    foreach (MetaDataProperty item in items)
                    {
                        AddField(document, item.FullName, GetPropertyValue(item), property.IsAnalyzed,
                                 property.IsStored, boost);
                    }
                }
            }
        }

        #endregion

        private static void AddField(
            Document document, string name, string value, bool isAnalyzed, bool isStored, float? boost)
        {
            if (isAnalyzed)
            {
                var field = new Field(name.ToLowerInvariant(), value, Field.Store.NO, Field.Index.ANALYZED);
                field.SetBoost(boost ?? 0);
                document.Add(field);

                // If property is both analyzed and stored, it's indexed to two fields
                // one analyzed, with regular name and another stored with .stored suffix
                if (isStored)
                {
                    field = new Field(name.ToLowerInvariant() + MetaDataPropertyDefinition.StoredSuffix, value,
                                      Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS);
                    field.SetBoost(boost ?? 0);
                    document.Add(field);
                }
            }
            else if (isStored)
            {
                var field =
                    new Field(name.ToLowerInvariant(), value, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.SetBoost(boost ?? 0);
                document.Add(field);
            }
        }

        private static string GetPropertyValue(MetaDataProperty property)
        {
            return property == null || property.Value == null ? String.Empty : property.Value.ToString();
        }
    }
}