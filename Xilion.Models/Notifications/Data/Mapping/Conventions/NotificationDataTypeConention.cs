using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Xilion.Models.Notifications.Domain;

namespace Xilion.Models.Notifications.Data.Mapping.Conventions
{
    public class NotificationDataTypeConention : IPropertyConvention, IPropertyConventionAcceptance
    {
        private static readonly Type _Type = typeof (NotificationDataType<>);

        #region Implementation of IConvention<IPropertyInspector,IPropertyInstance>

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IPropertyInstance instance)
        {
            Type closedType = _Type.MakeGenericType(instance.EntityType);
            instance.CustomType(closedType);
        }

        #endregion

        #region Implementation of IConventionAcceptance<IPropertyInspector>

        /// <summary>
        /// Whether this convention will be applied to the target.
        /// </summary>
        /// <param name="criteria">Instace that could be supplied</param>
        /// <returns>
        /// Apply on this target?
        /// </returns>
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof (NotificationData));
        }

        #endregion
    }
}