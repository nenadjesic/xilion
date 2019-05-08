namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// Represents an entity that has metadata.
    /// </summary>
    public interface IHaveMetaData
    {
        /// <summary>
        /// Gets or sets the entity metadata - a collection of dynamic properties that can be localized.
        /// </summary>
        MetaData MetaData { get; }
    }
}