using System.Collections.Generic;
using Xilion.Models.Site;

namespace Xilion.Models.Web.Mvc
{
    public class PageResourceContext
    {
        private IDictionary<string, string> _attributes;

        public PageResourceContext(string tag)
        {
            Tag = tag;
            _attributes = new Dictionary<string, string>();
        }
        public virtual PageResourceType ResourceType { get; set; }
        public virtual PageResourceScope Scope { get; set; }
        public string Tag { get; protected set; }
        public IDictionary<string, string> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
    }
}