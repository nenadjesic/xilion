using System.Collections.Generic;
using NHibernate.Search.Attributes;
using Xilion.Models.Core.Data;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;

namespace Xilion.Models.Filters
{
    public class FilterItem : Entity, IHierarchy, IHaveMetaData
    {
        private MetaData _metaData;

        public FilterItem()
        {
            _metaData = new MetaData(GetType());
        }

        public virtual Filter Filter { get; set; }

        public virtual FilterItem Parent { get; set; }

        public virtual IList<FilterItem> Children { get; set; }

        public virtual string Title
        {
            get { return MetaData.GetValue<string>("Title"); }
            set { MetaData.SetValueNull("Title", value); }
        }

        #region IHaveMetaData Members

        /// <summary>
        /// Gets or sets the entity metadata - a collection of dynamic properties that can be localized.
        /// </summary>
        [Field(Name = "metadata"), FieldBridge(typeof (MetaDataFieldBridge))]
        public virtual MetaData MetaData
        {
            get { return _metaData; }
            protected set { _metaData = value; }
        }

        #endregion

        #region IHierarchy Members

        public int Ordinal { get; set; }

        IEnumerable<IHierarchy> IHierarchy.Children
        {
            get { return Children; }
            set { Children = (IList<FilterItem>) value; }
        }

        IHierarchy IHierarchy.Parent
        {
            get { return Parent; }
            set { Parent = (FilterItem) value; }
        }

        #endregion
    }
}