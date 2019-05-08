using System.Linq;
using Xilion.Models.Media.Documents;
using Xilion.Framework.Data;

namespace Xilion.Models.Media.Data.Default
{
    public class DocumentRepository : MediaItemRepository<DocumentItem>, IDocumentRepository
    {
        public DocumentRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        #region IDocumentRepository Members

        public override IQueryable<DocumentItem> GetAll()
        {
            return base.GetAll().Where(x => x.Library.LibraryScope == LibraryScope.Users);
        }

        #endregion
    }
}