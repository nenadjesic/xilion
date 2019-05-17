using System;
using System.Collections.Generic;
using Xilion.Models.Classifications;
using Xilion.Models.Media.Images;
using Xilion.Models.Core.Data;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;
using NHibernate.Envers.Configuration.Attributes;

namespace Xilion.Models.Karte
{
    /// <summary>
    ///  Propertisi od Karte
    /// </summary>
    public class Karta : MetaDataEntity
    {
        private IList<Label> _labels = new List<Label>();
        /// <summary>
        ///   Gets or sets karta Title
        /// </summary>
        public virtual string Title
        {
            get { return MetaData.GetValue<string>("Title"); }
            set { MetaData.SetValue("Title", value); }
        }

      
        /// <summary>
        ///   Gets or sets kratak opis tacke na karti
        /// </summary>
        public virtual string Summary
        {
            get { return MetaData.GetValue<string>("Summary"); }
            set { MetaData.SetValue("Summary", value); }
        }

        /// <summary>
        ///   Latitude
        /// </summary>
        public virtual decimal Latitude { get; set; }

        /// <summary>
        ///   Longitude
        /// </summary>
        public virtual decimal Longitude { get; set; }

       
        /// <summary>
        /// Gets ikonicu
        /// </summary>
        public virtual ImageItem Avatar { get; set; }

        #region Implementation of ILabeled


        [NotAudited]
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        #endregion
    }
}