using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data;

namespace Xilion.Models.Core.Data.Repositories.Default
{
    public class WorkflowEntityRepository<T> : AliasedEntityRepository<T>, IWorkflowEntityRepository<T>
        where T : WorkflowEntity
    {
        public WorkflowEntityRepository(Xilion.Framework.Data.ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        #region IWorkflowEntityRepository<T> Members

        public void SetStatus(WorkflowStatus status, params long[] ids)
        {
            IQueryable<T> entities = GetSession().Query<T>().Where(x => ids.Contains(x.Id));
            using (ITransaction transaction = GetSession().BeginTransaction())
            {
                foreach (T entity in entities)
                {
                    entity.Status = status;
                    GetSession().SaveOrUpdate(entity);
                }
                transaction.Commit();
            }
        }

        #endregion
    }
}