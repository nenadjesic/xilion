using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class PropertyConvention : IPropertyConvention
    {
        #region IPropertyConvention Members

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }

        #endregion
    }
}