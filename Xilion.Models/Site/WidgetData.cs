using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core.Domain;
using Xilion.Framework;

namespace Xilion.Models.Site
{
    public class WidgetData
    {
        private IList<MetaDataProperty> _properties = new List<MetaDataProperty>();

        public bool IsChanged { get; set; }

        public void Add(string name, object value)
        {
            _properties.Add(new MetaDataProperty
                                {
                                    Name = name,
                                    Value = value
                                });
        }

        public void Remove(string name)
        {
            var property = _properties.SingleOrDefault(x => x.Name == name);
            if(property != null)
                _properties.Remove(property);
        }

        /// <summary>
        /// Gets or sets the list of widget meta properties.
        /// </summary>
        internal IList<MetaDataProperty> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }


        /// <summary>
        /// Gets the strongly typed value of the metadata property with the given name and culture.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="name">Name of the property to retrieve the value for.</param>
        /// <returns>A metadata property value.</returns>
        public virtual T GetValue<T>(string name)
        {
            MetaDataProperty property = GetProperty(name, true);

            return property == null
                       ? default(T)
                       : ObjectBuilder.BuildObjectValue(property.Value, default(T));
        }

        /// <summary>
        /// Sets the value of the given metadata property.
        /// </summary>
        /// <typeparam name="T">Type of the value to set.</typeparam>
        /// <param name="name">Name of the metadata property to set.</param>
        /// <param name="value">New property value.</param>
        public virtual void SetValue<T>(string name, T value)
        {
            MetaDataProperty property = GetProperty(name, true);
            object oldValue = property.Value;
            var newValue = ObjectBuilder.BuildObjectValue<T>(value);

            property.Value = newValue;

            // ReSharper disable CompareNonConstrainedGenericWithNull
            bool changed = oldValue == null
                               ? newValue != null
                               : !oldValue.Equals(newValue);
            // ReSharper restore CompareNonConstrainedGenericWithNull

            IsChanged = IsChanged || changed;
        }

        private MetaDataProperty GetProperty(string name, bool ensureExist)
        {
            MetaDataProperty property =
                Properties.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (property == null && ensureExist)
            {
                property = new MetaDataProperty {Name = name};
                Properties.Add(property);
            }

            return property;
        }
    }
}