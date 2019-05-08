using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xilion.Models.Core.Applications;
using Xilion.Models.Localization;
using Xilion.Framework;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// Contains the list of metadata properties and allows entities to expand (add more properties) dynamically.
    /// Metadata properties can also be localized.
    /// </summary>
    [Serializable]
    public class MetaData
    {
        private readonly Type _type;
        private IList<MetaDataProperty> _properties = new List<MetaDataProperty>();

        /// <summary>
        /// Creates a new instance of MetaData class for the given type.
        /// </summary>
        /// <param name="type">Entity type that the metadata will be stored in.</param>
        public MetaData(Type type)
        {
            _type = type;
        }

        /// <summary>
        /// Gets or sets the value indicating that this meta data object is changed (ie. some of the property values 
        /// are changed).
        /// </summary>
        /// HACK: This is a hack to make sure that any change to metadata properties are causing the object to be 
        /// marked as changed within NHibernate.
        public bool IsChanged { get; set; }

        /// <summary>
        /// Gets or sets the list of metadata properties.
        /// </summary>
        internal IList<MetaDataProperty> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }

        /// <summary>
        /// Gets the metadata property with the given name.
        /// </summary>
        /// <param name="name">Name of the metadata property to retrieve.</param>
        /// <returns>Metadata property instance.</returns>
        protected virtual MetaDataProperty this[string name]
        {
            get { return Properties.SingleOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Gets the list of all MetaDataPropertyDefinitions for this type.
        /// </summary>
        /// <returns></returns>
        internal IList<MetaDataPropertyDefinition> GetPropertyDefinitions()
        {
            return CmsContext.Current.GetMetaDataFor(_type);
        }

        /// <summary>
        /// Gets the value of the metadata property with the given name and culture.
        /// </summary>
        /// <param name="name">Name of the property to retrieve the value for.</param>
        /// <param name="culture">Culture version to retrieve.</param>
        /// <returns>A metadata property value.</returns>
        public object GetValueCulture(string name, CultureInfo culture)
        {
            return GetValueCulture<object>(name, culture);
        }

        /// <summary>
        /// Gets the strongly typed value of the metadata property with the given name and culture.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="name">Name of the property to retrieve the value for.</param>
        /// <param name="culture">Culture version to retrieve.</param>
        /// <returns>A metadata property value.</returns>
        public virtual T GetValueCulture<T>(string name, CultureInfo culture)
        {
            MetaDataProperty property = GetProperty(name, culture, true);

            return property == null
                       ? default(T)
                       : ObjectBuilder.BuildObjectValue(property.Value, default(T));
        }


        /// <summary>
        /// Gets the value of the metadata property with the given name and culture.
        /// </summary>
        /// <param name="name">Name of the property to retrieve the value for.</param>
        /// <param name="culture">Culture version to retrieve.</param>
        /// <returns>A metadata property value.</returns>
        public object GetValue(string name, CultureInfo culture = null)
        {
            return GetValue<object>(name, culture);
        }

        /// <summary>
        /// Gets the strongly typed value of the metadata property with the given name and culture.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="name">Name of the property to retrieve the value for.</param>
        /// <param name="culture">Culture version to retrieve.</param>
        /// <returns>A metadata property value.</returns>
        public virtual T GetValue<T>(string name, CultureInfo culture = null) 
        {
            //if (culture = "sr")
            //    culture = "sr-Cyrl-CS";
            MetaDataProperty property = GetProperty(name, culture, true);

            return property == null
                       ? default(T)
                       : ObjectBuilder.BuildObjectValue(property.Value, default(T));
        }

        public virtual T GetValueC<T>(string name, CultureInfo culture) /* OVDJE IDE == NULL*/
        {
            MetaDataProperty property = GetProperty(name, culture, true);

            return property == null
                       ? default(T)
                       : ObjectBuilder.BuildObjectValue(property.Value, default(T));
        }
        /// <summary>
        /// Checks whether the property with the given name has culture version, or if there is any property with the 
        /// given culture version if name is null (default).
        /// </summary>
        /// <param name="name">Name of the property to check.</param>
        /// <param name="culture">Culture to check existence for.</param>
        /// <returns>true if there is a culture version of metadata property; false otherwise.</returns>
        public bool HasCultureVersion(string name = null, CultureInfo culture = null)
        {
            int cultureId = (culture ?? LocalizationManager.CurrentContentCulture).LCID;

            return name == null
                       ? Properties.Any(x => x.CultureID == cultureId)
                       : Properties.Any(
                           x => x.Name.ToLowerInvariant() == name.ToLowerInvariant() && x.CultureID == cultureId);
        }

        /// <summary>
        /// Checks whether the property with the given name is defined.
        /// </summary>
        /// <param name="name">Name of the property to check.</param>
        /// <returns>true if there is a property with the given name; false otherwise.</returns>
        public virtual bool IsPropertyDefined(string name)
        {
            return GetPropertyDefinition(name) != null;
        }

        /// <summary>
        /// Sets the value of the given metadata property.
        /// </summary>
        /// <param name="name">Name of the metadata property to set.</param>
        /// <param name="value">New property value.</param>
        /// <param name="culture">Culture to set the value for. Use null for InvariantCulture in non-localized 
        /// properties and CurrentContentCulture for localized ones.</param>
        public void SetValue(string name, object value, CultureInfo culture = null)
        {
            SetValue<object>(name, value, culture);
        }

        /// <summary>
        /// Sets the value of the given metadata property.
        /// </summary>
        /// <typeparam name="T">Type of the value to set.</typeparam>
        /// <param name="name">Name of the metadata property to set.</param>
        /// <param name="value">New property value.</param>
        /// <param name="culture">Culture to set the value for. Use null for InvariantCulture in non-localized 
        /// properties and CurrentContentCulture for localized ones.</param>
        public virtual void SetValue<T>(string name, T value, CultureInfo culture = null)
        {
            if (!IsPropertyDefined(name))
                throw new Exception(String.Format(
                    "Type '{0}' does not contains definition for name '{1}'", _type, name));

            MetaDataProperty property = GetProperty(name, culture, true);
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

        public virtual void SetValueNull<T>(string name, T value, CultureInfo culture = null)
        {
            if (!IsPropertyDefined(name))
                throw new Exception(String.Format(
                    "Type '{0}' does not contains definition for name '{1}'", _type, name));

            MetaDataProperty property = GetProperty(name, culture, true);
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

        private MetaDataPropertyDefinition GetPropertyDefinition(string name)
        {
            IList<MetaDataPropertyDefinition> metaDefinition = CmsContext.Current.GetMetaDataFor(_type);
            return metaDefinition.SingleOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        private MetaDataProperty GetProperty(string name, CultureInfo culture, bool ensureExist)
        {
            MetaDataPropertyDefinition propertyDefinition = GetPropertyDefinition(name);

            if (propertyDefinition == null)
                throw new CmsException("MetaData property '" + name + "' does not exist.");

            MetaDataProperty property;

            if (propertyDefinition.IsLocalized)
            {

                CultureInfo cul = LocalizationManager.CurrentContentCulture;
                if (cul.Name.ToString() == "sr")
                {
                    CultureInfo cult = new CultureInfo("sr-Cyrl-CS");
                    culture = cult;
                }
                else
                {
                    if (culture == null)
                        culture = LocalizationManager.CurrentContentCulture; //Sauti.FrameCore.Localization.CurrentContentCulture;
                }


                property = Properties
                    .SingleOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                          x.CultureID.Equals(culture.LCID));

            }
            else
            {
                if (culture == null)
                    culture = CultureInfo.InvariantCulture;

                property = Properties.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                // HACK: Preserves backward compatibility with old databases, 
                // and forces non-localized properties to have invariant culture id (0).
                if (property != null && property.CultureID != culture.LCID)
                    property.CultureID = culture.LCID;
            }

            if (property == null && ensureExist)
            {
                property = new MetaDataProperty {CultureID = culture.LCID, Name = name};
                Properties.Add(property);
            }

            return property;
        }
    }
}