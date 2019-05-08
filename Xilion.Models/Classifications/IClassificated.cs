using System.Collections.Generic;

namespace Xilion.Models.Classifications
{
    public interface IClassificated
    {
        IList<Classification> Classifications { get; set; }
    }
}