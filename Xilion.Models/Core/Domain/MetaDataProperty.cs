using System;
using System.Globalization;

namespace Xilion.Models.Core.Domain
{
    //[DataContract]
    /// <summary>
    /// Represents single metadata property.
    /// </summary>
    public class MetaDataProperty
    {
        /// <summary>
        ///   Gets or sets the name of meta field.
        /// </summary>
        //[DataMember]
        public string Name { get; set; }

        /// <summary>
        ///   Gets or sets meta data value.
        /// </summary>
        //[DataMember]
        public virtual object Value { get; set; }

        /// <summary>
        ///   Gets or sets meta data property culture id.
        /// </summary>
        //[DataMember]
        public virtual int CultureID { get; set; }

        //[IgnoreDataMember]
        /// <summary>
        /// Gets metadata property culture (if localized). 
        /// For non localized properties <see cref="CultureInfo.InvariantCulture"/> is used.
        /// </summary>
        public virtual CultureInfo Culture
        {
            get { return CultureID == 0 ? CultureInfo.InvariantCulture : new CultureInfo(CultureID); }
        }

        /// <summary>
        /// Gets the full name of the metadata property, with MetaData or MetaData.CultureName prefix depending on 
        /// whether the property is localized or not.
        /// </summary>
        public virtual string FullName
        {
            get
            {
                return String.Format("MetaData{0}.{1}",
                                     Culture.Name == String.Empty || CultureID == 0
                                         ? String.Empty
                                         : String.Format(".{0}", Culture.Name),
                                     Name);
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current 
        /// <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; 
        /// otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current 
        /// <see cref="T:System.Object"/>.</param>
        public override bool Equals(object obj)
        {
            if (!(obj is MetaDataProperty))
                throw new InvalidCastException();
            var mci = (MetaDataProperty) obj;
            return (Name == mci.Name);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}