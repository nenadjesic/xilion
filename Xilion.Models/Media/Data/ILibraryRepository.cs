using Xilion.Framework.Data.Repositories;
using Xilion.Models.Core.Domain;
using System;

namespace Xilion.Models.Media.Data
{
    public interface ILibraryRepository : IRepository<Library>
    {
        void SetStatus(WorkflowStatus status, long[] ids);

        void SetStatus(WorkflowStatus status, long id);
    }
}