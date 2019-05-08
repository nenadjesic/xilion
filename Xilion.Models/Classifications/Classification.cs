using System.Collections.Generic;
using NHibernate.Search.Attributes;
using Xilion.Framework.Data.Search;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Classifications
{
    [Indexed]
    public class Classification : AliasedEntity
    {
        private IList<Label> _labels = new List<Label>();

        /// <summary>
        ///   Gets or sets <see cref="ClassificationType" /> of the classification.
        /// </summary>
        [Field(Name = "classificationtype")]
        [FieldBridge(typeof(EnumerationFieldBridge))]
        public virtual ClassificationType ClassificationType { get; set; }

        /// <summary>
        ///   Gets or sets a value indicates if item is system item. System items are predefined
        /// by application and cannot be deleteed.
        /// </summary>
        public virtual bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets classification Name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets classification item name used to display i interface.
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        /// Gets a list of classification labels.
        /// </summary>
        /// 
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            protected set { _labels = value; }
        }
    }
}