using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Core.Data.Mappings.Conventions
{
    /// <summary>
    /// Represents FluentHibernate convention for <see cref="Settings"/> types.
    /// </summary>
    public class SettingsPropertiesTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType(typeof (SettingsPropertiesType));
        }

        #endregion

        #region IPropertyConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof (IList<SettingsProperty>));
        }

        #endregion
    }
}