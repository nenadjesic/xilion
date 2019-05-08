using System;
using System.Collections.Generic;
using System.Linq;

namespace Xilion.Models.Core.Domain
{
    public class DynamicData
    {
        private IList<DynamicDataProperty> _properties = new List<DynamicDataProperty>();

        public bool IsChanged { get; set; }

        public IList<DynamicDataProperty> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }

        protected virtual DynamicDataProperty this[string name]
        {
            get { return GetProperty(name, true); }
        }

        public virtual string GetValue(string name)
        {
            DynamicDataProperty property = GetProperty(name, true);

            return property == null
                       ? String.Empty
                       : property.Value;
        }

        public virtual void SetValue(string name, string value)
        {
            DynamicDataProperty property = GetProperty(name, true);
            object oldValue = property.Value;
            string newValue = value;

            property.Value = newValue;

            // ReSharper disable CompareNonConstrainedGenericWithNull
            bool changed = oldValue == null
                               ? newValue != null
                               : !oldValue.Equals(newValue);
            // ReSharper restore CompareNonConstrainedGenericWithNull

            IsChanged = IsChanged || changed;
        }

        private DynamicDataProperty GetProperty(string name, bool ensureExist)
        {
            DynamicDataProperty property =
                Properties.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (property == null && ensureExist)
            {
                property = new DynamicDataProperty {Name = name};
                Properties.Add(property);
            }

            return property;
        }
    }

    public class DynamicDataProperty
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}