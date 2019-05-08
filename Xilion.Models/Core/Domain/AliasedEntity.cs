using NHibernate.Search.Attributes;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// An <see cref="MetaDataEntity"/> that has alias.
    /// </summary>
    public abstract class AliasedEntity : MetaDataEntity, IAliased
    {
        #region IAliased Members

        /// <summary>
        /// Gets or sets the alias. Alias is a Users-friendly id of the entity, used when generating URLs.
        /// </summary>
        [Field(Name = "alias", Index = Index.UnTokenized, Store = Store.Yes)]
        public virtual string Alias { get; set; }

        #endregion
    }
}