using System.Collections.Generic;

namespace Xilion.Models.Web.Mvc
{
    public class CmsPageTemplateContext
    {
        private IList<PageResourceContext> _resources = new List<PageResourceContext>();
        private IList<CmsWidgetContext> _widgets = new List<CmsWidgetContext>();

        public IList<CmsWidgetContext> Widgets
        {
            get { return _widgets; }
            set { _widgets = value; }
        }

        /// <summary>
        /// Gets or sets list of page context resources.
        /// </summary>
        public IList<PageResourceContext> Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }

        public CmsPageContextMode Mode { get; set; }
        public string Title { get; set; }
    }
}