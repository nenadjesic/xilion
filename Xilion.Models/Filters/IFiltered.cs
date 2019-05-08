using System.Collections.Generic;

namespace Xilion.Models.Filters
{
    public interface IFiltered
    {
        IEnumerable<Filter> Filters { get; set; }
    }
}