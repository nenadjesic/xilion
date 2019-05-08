using Xilion.Models.Core.Data.Repositories;

namespace Xilion.Models.Media.Data
{
    public interface IMediaItemRepository<T> : IWorkflowEntityRepository<T> where T : MediaItem
    {
        string GenerateTitle(string fileName);
    }
}