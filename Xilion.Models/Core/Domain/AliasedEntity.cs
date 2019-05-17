
namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// An <see cref="MetaDataEntity"/> that has alias.
    /// </summary>
    public abstract class AliasedEntity : MetaDataEntity, IAliased
    {
        #region IAliased Members

        public virtual string Alias { get; set; }
        #endregion
    }
}