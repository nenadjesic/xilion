using Microsoft.AspNetCore.Routing;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Web.Mvc
{
    /// <summary>
    /// Represents class used to render page widget.
    /// </summary>
    public class CmsWidgetContext : IOrdered
    {
        /// <summary>
        /// Gets or sets widget unique identifier.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets section name this widget belongs to.
        /// </summary>
        public string Section { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if this widget belongs to template.
        /// Widgets belongs to template are rendered on every page using that template.
        /// </summary>
        public bool IsTemplateWidget { get; set; }

        /// <summary>
        /// Gets or sets context of page this widget belongs to.
        /// </summary>
        public CmsPageContext PageContext { get; set; }

        /// <summary>
        /// Gets or sets all widget data.
        /// </summary>
        public RouteValueDictionary RouteData { get; set; }

        #region IOrdered Members

        /// <summary>
        /// Gets or sets widget order in section.
        /// </summary>
        public int Ordinal { get; set; }

        #endregion
    }
}