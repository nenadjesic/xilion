using System.Web;
using Xilion.Models.Site;
using Xilion.Models.Site.Extensions;

namespace Xilion.Models.Web
{
    public class CmsSiteMapNode : SiteMapNode
    {
        private readonly Page _page;

        public CmsSiteMapNode(SiteMapProvider provider, Page page)
            : base(provider, page.ID.ToString())
        {
            _page = page;
        }

        public Page Page
        {
            get { return _page; }
        }

        public override string Description
        {
            get { return _page.Description; }
        }

        public override string Title
        {
            get { return _page.Title; }
        }

        public override string Url
        {
            get { return _page.Url(); }
        }
    }
}