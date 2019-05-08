using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Site.Data.Mappings.Conventions
{
    /// <summary>
    /// Represents FluentHibernate convention for mapping <see cref="MetaData"/> types.
    /// </summary>
    public class DynamicDataTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        private static readonly Type _openType = typeof(DynamicDataType);

        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType(_openType);
        }

        #endregion

        #region IPropertyConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof(DynamicData));
        }

        #endregion
    }
}