using System;
using System.ComponentModel;
using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    /// <summary>
    ///   Represents meta field definition in web.config file.
    /// </summary>
    public class MetaDataConfigurationElement : ConfigurationElement
    {
        /// <summary>
        ///   Gets or sets the name of meta field. Must be unique for some application
        /// </summary>
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty((string) this["name"]))
                    throw new Exception("meta field name cannot be empty");

                return (string) this["name"];
            }
            set { this["name"] = value; }
        }

        [TypeConverter(typeof (TypeNameConverter))]
        [ConfigurationProperty("type")]
        public Type Type
        {
            get { return (Type) this["type"]; }
            set { this["type"] = value; }
        }

        /// <summary>
        ///   Gets or sets default value for meta field
        /// </summary>
        [ConfigurationProperty("defaultValue")]
        public string DefaultValue
        {
            get { return (string) this["defaultValue"]; }
            set { this["defaultValue"] = value; }
        }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is indexed
        /// </summary>
        [ConfigurationProperty("indexed", DefaultValue = false)]
        public bool Indexed
        {
            get { return (bool) this["indexed"]; }
            set { this["indexed"] = value; }
        }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is analyzed - split to words and indexed.
        /// </summary>
        [ConfigurationProperty("analyzed", DefaultValue = true)]
        public bool Analyzed
        {
            get { return (bool) this["analyzed"]; }
            set { this["analyzed"] = value; }
        }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is stored - saved as-is in lucene index.
        /// </summary>
        [ConfigurationProperty("stored")]
        public bool Stored
        {
            get { return (bool) this["stored"]; }
            set { this["stored"] = value; }
        }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is localized
        /// </summary>
        [ConfigurationProperty("localized", DefaultValue = false)]
        public bool Localized
        {
            get { return (bool) this["localized"]; }
            set { this["localized"] = value; }
        }

        /// <summary>
        ///   Gets or sets the operator to use for search if this field is indexed.
        /// </summary>
        [ConfigurationProperty("operator")]
        public string Operator
        {
            get { return (string) this["operator"]; }
            set { this["operator"] = value; }
        }
    }
}