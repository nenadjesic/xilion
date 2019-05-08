using System.Collections.Generic;

namespace Xilion.Models.Classifications
{
    /// <summary>
    ///   Represents an entity that can be labeled (tagged, categorized).
    /// </summary>
    public interface ILabeled

    {
        IList<Label> Labels { get; }
    }
}