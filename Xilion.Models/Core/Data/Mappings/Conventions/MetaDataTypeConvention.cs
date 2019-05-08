using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Data.Mappings.Conventions
{
    /// <summary>
    /// Represents FluentHibernate convention for mapping <see cref="MetaData"/> types.
    /// </summary>
    public class MetaDataTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        private static readonly Type _openType = typeof (MetaDataType<>);

        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            Type closedType = _openType.MakeGenericType(instance.EntityType);
            instance.CustomType(closedType);
        }

        #endregion

        #region IPropertyConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof (MetaData));
        }

        #endregion
    }
}