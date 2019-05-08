using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;
using Xilion.Models.Core.Domain;
using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;



namespace Xilion.Models.Media.Data.Default
{
    public class LibraryRepository : Repository<Library>, ILibraryRepository
    {
        public LibraryRepository(Framework.Data.ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {

        }
        public void SetStatus(WorkflowStatus status, params long[] ids)
        {
            IQueryable<Library> entities = GetSession().Query<Library>().Where(x => ids.Contains(x.Id));
            using (ITransaction transaction = GetSession().BeginTransaction())
            {
                foreach (Library entity in entities)
                {
                    entity.Status = status;
                    GetSession().SaveOrUpdate(entity);
                }
                transaction.Commit();
            }
        }

        public void SetStatus(WorkflowStatus status, long subscriberId)
        {
            Library entity = GetById(subscriberId);

            using (ITransaction transaction = GetSession().BeginTransaction())
            {
                entity.Status = status;

                Save(entity);
                transaction.Commit();
            }
        }
    }
}