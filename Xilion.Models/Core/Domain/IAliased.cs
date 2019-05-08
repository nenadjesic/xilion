namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// Represents an entity that has alias. Aliases are used as a Users-friendly ids of the entity, when generating 
    /// URLs.
    /// </summary>
    public interface IAliased
    {
        /// <summary>
        /// Gets or sets the alias. Alias is a Users-friendly id of the entity, used when generating URLs.
        /// </summary>
        string Alias { get; set; }
    }
}