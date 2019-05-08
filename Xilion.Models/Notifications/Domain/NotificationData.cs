using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xilion.Models.Core.Domain;
using Xilion.Framework;

namespace Xilion.Models.Notifications.Domain
{
    /// <summary>
    /// Contains the list of notification properties.
    /// </summary>
    [Serializable]
    public class NotificationData
    {
        private IList<MetaDataProperty> _properties = new List<MetaDataProperty>();

        /// <summary>
        ///   Gets or sets the list of metadata properties.
        /// </summary>
        internal IList<MetaDataProperty> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }

        /// <summary>
        ///   Gets or sets the value indicating that this meta data object is changed (ie. some of the property values are changed).
        /// </summary>
        /// HACK: This is a hack to make sure that any change to metadata properties are causing the object to be 
        /// marked as changed within NHibernate.
        public bool IsChanged { get; set; }

        /// <summary>
        ///   Gets the value of the metadata property with the given name and culture.
        /// </summary>
        /// <param name="name"> Name of the property to retrieve the value for. </param>
        /// <returns> A metadata property value. </returns>
        public object GetValue(string name)
        {
            return GetValue<object>(name);
        }

        /// <summary>
        ///   Gets the strongly typed value of the metadata property with the given name and culture.
        /// </summary>
        /// <typeparam name="T"> Type of the value to retrieve. </typeparam>
        /// <param name="name"> Name of the property to retrieve the value for. </param>
        /// <returns> A metadata property value. </returns>
        public T GetValue<T>(string name)
        {
            var property = GetProperty(name);

            return property == null
                       ? default(T)
                       : ObjectBuilder.BuildObjectValue(property.Value, default(T));
        }

        /// <summary>
        ///   Sets the value of the given metadata property.
        /// </summary>
        /// <param name="name"> Name of the metadata property to set. </param>
        /// <param name="value"> New property value. </param>
        /// <param name="culture"> Culture to set the value for. Use null for InvariantCulture in non-localized properties and CurrentContentCulture for localized ones. </param>
        public void SetValue(string name, object value, CultureInfo culture = null)
        {
            SetValue(name, value);
        }

        /// <summary>
        ///   Sets the value of the given metadata property.
        /// </summary>
        /// <typeparam name="T"> Type of the value to set. </typeparam>
        /// <param name="name"> Name of the metadata property to set. </param>
        /// <param name="value"> New property value. </param>
        public void SetValue<T>(string name, T value)
        {
            var property = GetProperty(name);
            var oldValue = property.Value;
            T newValue = default(T);
            string stringValue = null;
            if (value is long)
            {
                stringValue = value.ToString();
                property.Value = stringValue;
            }
            else
            {
                newValue = ObjectBuilder.BuildObjectValue<T>(value);
                property.Value = newValue;
            }

            // ReSharper disable CompareNonConstrainedGenericWithNull
            var changed = oldValue == null
                              ? newValue != null || stringValue != null
                              : !oldValue.Equals(newValue);
            // ReSharper restore CompareNonConstrainedGenericWithNull

            IsChanged = IsChanged || changed;
        }

        private MetaDataProperty GetProperty(string name)
        {
            var property = Properties.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            // HACK: Preserves backward compatibility with old databases, 
            // and forces non-localized properties to have invariant culture id (0).
            if (property != null && property.CultureID != CultureInfo.InvariantCulture.LCID)
                property.CultureID = CultureInfo.InvariantCulture.LCID;

            if (property == null)
            {
                property = new MetaDataProperty { CultureID = CultureInfo.InvariantCulture.LCID, Name = name };
                Properties.Add(property);
            }

            return property;
        }
    }
}
