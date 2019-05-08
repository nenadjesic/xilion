using System;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;

namespace Xilion.Models.Core.Data.Repositories
{
    public interface IWorkflowEntityRepository<T> : IAliasedEntityRepository<T> where T : Entity
    {
        void SetStatus(WorkflowStatus status, params long[] ids);
    }
}