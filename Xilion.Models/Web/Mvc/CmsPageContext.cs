using System.Collections.Generic;

namespace Xilion.Models.Web.Mvc
{
    /// <summary>
    /// Represents page context used to render entire page.
    /// </summary>
    public class CmsPageContext
    {
        private CmsPageContextMode _mode = CmsPageContextMode.View;
        private IList<CmsWidgetContext> _widgets = new List<CmsWidgetContext>();
        private IList<PageResourceContext> _resources = new List<PageResourceContext>();

        public CmsPageContext() : this(CmsPageContextMode.View)
        {
        }

        public CmsPageContext(CmsPageContextMode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Gets or sets list if page widgets
        /// </summary>
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

        /// <summary>
        /// Gets or sets <see cref="CmsPageContextMode"/>
        /// </summary>
        public CmsPageContextMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        /// <summary>
        /// Gets or sets page title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets page alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets page template context
        /// </summary>
        public CmsPageTemplateContext Template { get; set; }

        /// <summary>
        /// Gets or sets page parent context
        /// </summary>
        public CmsPageContext Parent { get; set; }
    }
}