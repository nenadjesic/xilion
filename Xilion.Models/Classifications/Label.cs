using System.Collections.Generic;
using NHibernate.Envers.Configuration.Attributes;
using NHibernate.Search.Attributes;
using Xilion.Models.Core.Domain;
using Xilion.Models.Core.Data;

namespace Xilion.Models.Classifications
{
    [Indexed]
    public class Label : AliasedEntity, IHierarchy
    {
        private IList<Label> _children = new List<Label>();

        /// <summary>
        ///   Gets or sets classification this item belongs to.
        /// </summary>
        [Field(Name = "classification")]
        [FieldBridge(typeof (EntityAliasFieldBridge))]
        public virtual Classification Classification { get; set; }

        /// <summary>
        ///   Gets or sets classification friendly localized name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///   Gets or sets item color.
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        ///   Gets or sets item Parent.
        /// </summary>
        public virtual Label Parent { get; set; }

        /// <summary>
        ///   Gets or sets list of children for item.
        /// </summary>
        [NotAudited]
        public virtual IList<Label> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public virtual bool HasChildren
        {
            get { return Children.Count > 0; }
        }

        #region IHierarchy Members

        IEnumerable<IHierarchy> IHierarchy.Children
        {
            get { return Children; }
            set { Children = (IList<Label>) value; }
        }

        IHierarchy IHierarchy.Parent
        {
            get { return Parent; }
            set { Parent = (Label) value; }
        }

       
        #endregion

        #region Implementation of IOrdered

        public virtual int Ordinal { get; set; }

        #endregion

     
    }
}