using System.Collections.Generic;

namespace Xilion.Models.Site
{
    public class PageTemplate : SiteEntity
    {
        private IList<Page> _pages = new List<Page>();

        public virtual string Content { get; set; }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }
    }
}