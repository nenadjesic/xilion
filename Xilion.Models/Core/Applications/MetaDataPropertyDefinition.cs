using System;
using System.Globalization;
using Xilion.Models.Localization;

namespace Xilion.Models.Core.Applications
{
    public class MetaDataPropertyDefinition
    {
        public const string StoredSuffix = ".stored";

        public MetaDataPropertyDefinition(string name)
        {
            Name = name;
            Type = typeof (string);
            DefaultValue = String.Empty;
            Indexed = true;
            IsAnalyzed = true;
            IsLocalized = false;
            IsStored = false;
        }

        /// <summary>
        ///   Gets or sets the name of meta field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   Gets or sets the type of meta field.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        ///   Gets or sets default value for meta field.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is indexed.
        /// </summary>
        public bool Indexed { get; set; }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is analyzed - split to words and indexed.
        /// </summary>
        public bool IsAnalyzed { get; set; }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is localized.
        /// </summary>
        public bool IsLocalized { get; set; }

        /// <summary>
        ///   Gets or sets a value that indicates if this field is stored - saved as-is in lucene index.
        /// </summary>
        public bool IsStored { get; set; }

        /// <summary>
        /// Gets the full name of the metadata property, with MetaData or MetaData.CurrentCultureName prefix depending 
        /// on whether the property is localized or not.
        /// </summary>
        public virtual string GetFullName()
        {
            return GetFullName(LocalizationManager.CurrentContentCulture);
        }

        /// <summary>
        /// Gets the full name of the metadata property, with MetaData or MetaData.CultureName prefix depending 
        /// on whether the property is localized or not.
        /// </summary>
        public virtual string GetFullName(CultureInfo culture)
        {
            return String.Format("MetaData{0}.{1}",
                                 IsLocalized
                                     ? String.Format(".{0}", culture.Name)
                                     : String.Empty,
                                 Name);
        }
    }
}