using System;
using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    public class EntityConfigurationElement : ConfigurationElement
    {
        /// <summary>
        ///   Gets or sets a name of label group.
        /// </summary>
        [ConfigurationProperty("labelGroup")]
        public virtual string LabelGroup
        {
            get { return (string) base["labelGroup"]; }
            set { base["labelGroup"] = value; }
        }

        [ConfigurationProperty("type")]
        public Type Type
        {
            get { return (Type) this["type"]; }
            set { this["Type"] = value; }
        }

        /// <summary>
        ///   Gets the list of meta fields.
        /// </summary>
        [ConfigurationProperty("meta")]
        public MetaDataConfigurationElementCollection MetaDefinition
        {
            get { return (MetaDataConfigurationElementCollection) this["meta"]; }
        }
    }
}