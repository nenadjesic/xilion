using System.Collections.Generic;

namespace Xilion.Models.Core.Configuration
{
    public class MetaDataConfigurationElementComparer : IEqualityComparer<MetaDataConfigurationElement>
    {
        #region IEqualityComparer<MetaDataConfigurationElement> Members

        public bool Equals(MetaDataConfigurationElement x, MetaDataConfigurationElement y)
        {
            return x.Name.ToLower() == y.Name.ToLower();
        }

        public int GetHashCode(MetaDataConfigurationElement obj)
        {
            return obj.Name.GetHashCode();
        }

        #endregion
    }
}