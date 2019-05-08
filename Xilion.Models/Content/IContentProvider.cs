using System.Collections.Generic;
using Xilion.Framework.Queries;

namespace Xilion.Models.Content
{
    public interface IContentProvider
    {
        IContent Get(object id);
        IEnumerable<IContent> Query(Query query);
        void Create(IContent content);
        void Update(IContent content);
        void Upload(ContentContext context, ContentEventHandler handler);
        ContentContext Download(object id, ContentEventHandler handler);
        ContentContext Download(IContent content, ContentEventHandler handler);
    }
}