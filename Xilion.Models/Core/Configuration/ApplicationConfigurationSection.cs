using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    public class ApplicationConfigurationSection : ConfigurationSection
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

        /// <summary>
        ///   Gets the list of meta fields.
        /// </summary>
        [ConfigurationProperty("meta")]
        public MetaDataConfigurationElementCollection MetaDefinition
        {
            get { return (MetaDataConfigurationElementCollection) this["meta"]; }
        }

        /// <summary>
        ///   Gets the list of entities.
        /// </summary>
        [ConfigurationProperty("entities")]
        public EntityConfigurationElementCollection Entities
        {
            get { return (EntityConfigurationElementCollection) this["entities"]; }
        }
    }
}