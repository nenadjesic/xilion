using System.Collections.Generic;
using Xilion.Models.Core.Applications;


namespace Xilion.Models.Core
{
    public interface ICmsContext
    {
        IEnumerable<IApplication> Applications { get; }
        void Initialize(IApplication[] applications);
    }
}