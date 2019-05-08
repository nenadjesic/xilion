using System.Collections.Generic;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHierarchy : IOrdered
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IHierarchy> Children { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IHierarchy Parent { get; set; }
    }
}