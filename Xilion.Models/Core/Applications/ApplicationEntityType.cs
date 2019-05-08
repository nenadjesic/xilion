using System;
using System.Collections.Generic;

namespace Xilion.Models.Core.Applications
{
    /// <summary>
    ///   Contains various settings for a single application entity type.
    /// </summary>
    public class ApplicationEntityType
    {
        public ApplicationEntityType(Type type)
        {
            Type = type;
            Properties = new List<MetaDataPropertyDefinition>();
        }

        /// <summary>
        /// Gets or sets the Type of entity.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets or sets list of entity meta data properties.
        /// </summary>
        public IList<MetaDataPropertyDefinition> Properties { get; set; }

        /// <summary>
        ///   Gets or sets label group for entity. If not set parent IApplication value is used.
        /// </summary>
        public string LabelGroup { get; set; }

        /// <summary>
        /// Add all meta data properties from the given list.
        /// </summary>
        /// <param name="properties">A list of meta data properties to add.</param>
        public void AddProperties(IEnumerable<MetaDataPropertyDefinition> properties)
        {
            foreach (MetaDataPropertyDefinition property in properties)
                Properties.Add(property);
        }
    }
}