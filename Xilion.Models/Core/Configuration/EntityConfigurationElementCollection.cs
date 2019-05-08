using System.Collections.Generic;
using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    public class EntityConfigurationElementCollection
        : ConfigurationElementCollection, IEnumerable<EntityConfigurationElement>
    {
        #region IEnumerable<EntityConfigurationElement> Members

        public new IEnumerator<EntityConfigurationElement> GetEnumerator()
        {
            int count = Count;

            for (int i = 0; i < count; i++)
            {
                yield return BaseGet(i) as EntityConfigurationElement;
            }
        }

        #endregion

        protected override ConfigurationElement CreateNewElement()
        {
            return new EntityConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EntityConfigurationElement) element).Type;
        }
    }
}