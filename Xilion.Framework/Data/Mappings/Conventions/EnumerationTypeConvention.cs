using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Xilion.Framework.Data.Types;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class EnumerationTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        private static readonly Type _openType = typeof (EnumerationType<>);

        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            Type closedType = _openType.MakeGenericType(instance.Property.PropertyType);

            instance.CustomType(closedType);
        }

        #endregion

        #region IPropertyConventionAcceptance Members

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => typeof (Enumeration).IsAssignableFrom(x.Property.PropertyType));
        }

        #endregion
    }
}