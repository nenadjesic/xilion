using System;
using System.Collections.Generic;
using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    public class MetaDataConfigurationElementCollection
        : ConfigurationElementCollection, IEnumerable<MetaDataConfigurationElement>
    {
        public MetaDataConfigurationElement this[int index]
        {
            get { return (MetaDataConfigurationElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemove(index);
                base.BaseAdd(index, value);
            }
        }

        public new MetaDataConfigurationElement this[string key]
        {
            get { return (MetaDataConfigurationElement) BaseGet(key); }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemove(key);
                base.BaseAdd(value);
            }
        }

        #region IEnumerable<MetaDataConfigurationElement> Members

        public new IEnumerator<MetaDataConfigurationElement> GetEnumerator()
        {
            int count = Count;

            for (int i = 0; i < count; i++)
            {
                yield return BaseGet(i) as MetaDataConfigurationElement;
            }
        }

        #endregion

        protected override ConfigurationElement CreateNewElement()
        {
            return new MetaDataConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var meta = (MetaDataConfigurationElement) element;
            // ReSharper disable PossiblyMistakenUseOfParamsMethod
            return String.Concat(meta.Name);
            // ReSharper restore PossiblyMistakenUseOfParamsMethod
        }

        internal MetaDataConfigurationElementCollection GetApplicationElements()
        {
            var coll = new MetaDataConfigurationElementCollection();
            IEnumerator<MetaDataConfigurationElement> enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                MetaDataConfigurationElement element = enumerator.Current;
                if (element != null)
                    coll.BaseAdd(element);
            }
            return coll;
        }

        public MetaDataConfigurationElementCollection GetLocalized()
        {
            var coll = new MetaDataConfigurationElementCollection();
            IEnumerator<MetaDataConfigurationElement> enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                MetaDataConfigurationElement element = enumerator.Current;
                if (element != null && element.Localized)
                    //if (element.ApplicationName.Equals(appName, StringComparison.OrdinalIgnoreCase))
                    coll.BaseAdd(element);
            }
            return coll;
        }
    }
}